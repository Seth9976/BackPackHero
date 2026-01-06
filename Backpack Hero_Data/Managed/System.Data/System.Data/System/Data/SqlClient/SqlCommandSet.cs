using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.SqlClient
{
	// Token: 0x02000175 RID: 373
	internal sealed class SqlCommandSet
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00059CFA File Offset: 0x00057EFA
		internal SqlCommandSet()
		{
			this._batchCommand = new SqlCommand();
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00059D18 File Offset: 0x00057F18
		private SqlCommand BatchCommand
		{
			get
			{
				SqlCommand batchCommand = this._batchCommand;
				if (batchCommand == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return batchCommand;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00059D37 File Offset: 0x00057F37
		internal int CommandCount
		{
			get
			{
				return this.CommandList.Count;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00059D44 File Offset: 0x00057F44
		private List<SqlCommandSet.LocalCommand> CommandList
		{
			get
			{
				List<SqlCommandSet.LocalCommand> commandList = this._commandList;
				if (commandList == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return commandList;
			}
		}

		// Token: 0x17000321 RID: 801
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x00059D63 File Offset: 0x00057F63
		internal int CommandTimeout
		{
			set
			{
				this.BatchCommand.CommandTimeout = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00059D71 File Offset: 0x00057F71
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00059D7E File Offset: 0x00057F7E
		internal SqlConnection Connection
		{
			get
			{
				return this.BatchCommand.Connection;
			}
			set
			{
				this.BatchCommand.Connection = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00059D8C File Offset: 0x00057F8C
		internal SqlTransaction Transaction
		{
			set
			{
				this.BatchCommand.Transaction = value;
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00059D9C File Offset: 0x00057F9C
		internal void Append(SqlCommand command)
		{
			ADP.CheckArgumentNull(command, "command");
			string commandText = command.CommandText;
			if (string.IsNullOrEmpty(commandText))
			{
				throw ADP.CommandTextRequired("Append");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text || commandType == CommandType.StoredProcedure)
			{
				SqlParameterCollection sqlParameterCollection = null;
				SqlParameterCollection parameters = command.Parameters;
				if (0 < parameters.Count)
				{
					sqlParameterCollection = new SqlParameterCollection();
					for (int i = 0; i < parameters.Count; i++)
					{
						SqlParameter sqlParameter = new SqlParameter();
						parameters[i].CopyTo(sqlParameter);
						sqlParameterCollection.Add(sqlParameter);
						if (!SqlCommandSet.s_sqlIdentifierParser.IsMatch(sqlParameter.ParameterName))
						{
							throw ADP.BadParameterName(sqlParameter.ParameterName);
						}
					}
					foreach (object obj in sqlParameterCollection)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj;
						object value = sqlParameter2.Value;
						byte[] array = value as byte[];
						if (array != null)
						{
							int offset = sqlParameter2.Offset;
							int size = sqlParameter2.Size;
							int num = array.Length - offset;
							if (size != 0 && size < num)
							{
								num = size;
							}
							byte[] array2 = new byte[Math.Max(num, 0)];
							Buffer.BlockCopy(array, offset, array2, 0, array2.Length);
							sqlParameter2.Offset = 0;
							sqlParameter2.Value = array2;
						}
						else
						{
							char[] array3 = value as char[];
							if (array3 != null)
							{
								int offset2 = sqlParameter2.Offset;
								int size2 = sqlParameter2.Size;
								int num2 = array3.Length - offset2;
								if (size2 != 0 && size2 < num2)
								{
									num2 = size2;
								}
								char[] array4 = new char[Math.Max(num2, 0)];
								Buffer.BlockCopy(array3, offset2, array4, 0, array4.Length * 2);
								sqlParameter2.Offset = 0;
								sqlParameter2.Value = array4;
							}
							else
							{
								ICloneable cloneable = value as ICloneable;
								if (cloneable != null)
								{
									sqlParameter2.Value = cloneable.Clone();
								}
							}
						}
					}
				}
				int num3 = -1;
				if (sqlParameterCollection != null)
				{
					for (int j = 0; j < sqlParameterCollection.Count; j++)
					{
						if (ParameterDirection.ReturnValue == sqlParameterCollection[j].Direction)
						{
							num3 = j;
							break;
						}
					}
				}
				SqlCommandSet.LocalCommand localCommand = new SqlCommandSet.LocalCommand(commandText, sqlParameterCollection, num3, command.CommandType);
				this.CommandList.Add(localCommand);
				return;
			}
			if (commandType == CommandType.TableDirect)
			{
				throw SQL.NotSupportedCommandType(commandType);
			}
			throw ADP.InvalidCommandType(commandType);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0005A004 File Offset: 0x00058204
		internal static void BuildStoredProcedureName(StringBuilder builder, string part)
		{
			if (part != null && 0 < part.Length)
			{
				if ('[' == part[0])
				{
					int num = 0;
					foreach (char c in part)
					{
						if (']' == c)
						{
							num++;
						}
					}
					if (1 == num % 2)
					{
						builder.Append(part);
						return;
					}
				}
				SqlServerEscapeHelper.EscapeIdentifier(builder, part);
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0005A064 File Offset: 0x00058264
		internal void Clear()
		{
			DbCommand batchCommand = this.BatchCommand;
			if (batchCommand != null)
			{
				batchCommand.Parameters.Clear();
				batchCommand.CommandText = null;
			}
			List<SqlCommandSet.LocalCommand> commandList = this._commandList;
			if (commandList != null)
			{
				commandList.Clear();
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0005A0A0 File Offset: 0x000582A0
		internal void Dispose()
		{
			SqlCommand batchCommand = this._batchCommand;
			this._commandList = null;
			this._batchCommand = null;
			if (batchCommand != null)
			{
				batchCommand.Dispose();
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0005A0CC File Offset: 0x000582CC
		internal int ExecuteNonQuery()
		{
			this.ValidateCommandBehavior("ExecuteNonQuery", CommandBehavior.Default);
			this.BatchCommand.BatchRPCMode = true;
			this.BatchCommand.ClearBatchCommand();
			this.BatchCommand.Parameters.Clear();
			for (int i = 0; i < this._commandList.Count; i++)
			{
				SqlCommandSet.LocalCommand localCommand = this._commandList[i];
				this.BatchCommand.AddBatchCommand(localCommand.CommandText, localCommand.Parameters, localCommand.CmdType);
			}
			return this.BatchCommand.ExecuteBatchRPCCommand();
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0005A157 File Offset: 0x00058357
		internal SqlParameter GetParameter(int commandIndex, int parameterIndex)
		{
			return this.CommandList[commandIndex].Parameters[parameterIndex];
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0005A170 File Offset: 0x00058370
		internal bool GetBatchedAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			error = this.BatchCommand.GetErrors(commandIdentifier);
			int? recordsAffected2 = this.BatchCommand.GetRecordsAffected(commandIdentifier);
			recordsAffected = recordsAffected2.GetValueOrDefault();
			return recordsAffected2 != null;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0005A1A8 File Offset: 0x000583A8
		internal int GetParameterCount(int commandIndex)
		{
			return this.CommandList[commandIndex].Parameters.Count;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0005A1C0 File Offset: 0x000583C0
		private void ValidateCommandBehavior(string method, CommandBehavior behavior)
		{
			if ((behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection)) != CommandBehavior.Default)
			{
				ADP.ValidateCommandBehavior(behavior);
				throw ADP.NotSupportedCommandBehavior(behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection), method);
			}
		}

		// Token: 0x04000BF9 RID: 3065
		private const string SqlIdentifierPattern = "^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\uff3f_@#\\$]*$";

		// Token: 0x04000BFA RID: 3066
		private static readonly Regex s_sqlIdentifierParser = new Regex("^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\uff3f_@#\\$]*$", RegexOptions.ExplicitCapture | RegexOptions.Singleline);

		// Token: 0x04000BFB RID: 3067
		private List<SqlCommandSet.LocalCommand> _commandList = new List<SqlCommandSet.LocalCommand>();

		// Token: 0x04000BFC RID: 3068
		private SqlCommand _batchCommand;

		// Token: 0x02000176 RID: 374
		private sealed class LocalCommand
		{
			// Token: 0x06001238 RID: 4664 RVA: 0x0005A1EC File Offset: 0x000583EC
			internal LocalCommand(string commandText, SqlParameterCollection parameters, int returnParameterIndex, CommandType cmdType)
			{
				this.CommandText = commandText;
				this.Parameters = parameters;
				this.ReturnParameterIndex = returnParameterIndex;
				this.CmdType = cmdType;
			}

			// Token: 0x04000BFD RID: 3069
			internal readonly string CommandText;

			// Token: 0x04000BFE RID: 3070
			internal readonly SqlParameterCollection Parameters;

			// Token: 0x04000BFF RID: 3071
			internal readonly int ReturnParameterIndex;

			// Token: 0x04000C00 RID: 3072
			internal readonly CommandType CmdType;
		}
	}
}
