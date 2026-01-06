using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000A1 RID: 161
	internal sealed class FunctionNode : ExpressionNode
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x00030110 File Offset: 0x0002E310
		internal FunctionNode(DataTable table, string name)
			: base(table)
		{
			this._name = name;
			for (int i = 0; i < FunctionNode.s_funcs.Length; i++)
			{
				if (string.Compare(FunctionNode.s_funcs[i]._name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this._info = i;
					break;
				}
			}
			if (this._info < 0)
			{
				throw ExprException.UndefinedFunction(this._name);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00030178 File Offset: 0x0002E378
		internal void AddArgument(ExpressionNode argument)
		{
			if (!FunctionNode.s_funcs[this._info]._isVariantArgumentList && this._argumentCount >= FunctionNode.s_funcs[this._info]._argumentCount)
			{
				throw ExprException.FunctionArgumentCount(this._name);
			}
			if (this._arguments == null)
			{
				this._arguments = new ExpressionNode[1];
			}
			else if (this._argumentCount == this._arguments.Length)
			{
				ExpressionNode[] array = new ExpressionNode[this._argumentCount * 2];
				Array.Copy(this._arguments, 0, array, 0, this._argumentCount);
				this._arguments = array;
			}
			ExpressionNode[] arguments = this._arguments;
			int argumentCount = this._argumentCount;
			this._argumentCount = argumentCount + 1;
			arguments[argumentCount] = argument;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00030228 File Offset: 0x0002E428
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this.Check();
			if (FunctionNode.s_funcs[this._info]._id != FunctionId.Convert)
			{
				for (int i = 0; i < this._argumentCount; i++)
				{
					this._arguments[i].Bind(table, list);
				}
				return;
			}
			if (this._argumentCount != 2)
			{
				throw ExprException.FunctionArgumentCount(this._name);
			}
			this._arguments[0].Bind(table, list);
			if (this._arguments[1].GetType() == typeof(NameNode))
			{
				NameNode nameNode = (NameNode)this._arguments[1];
				this._arguments[1] = new ConstNode(table, ValueType.Str, nameNode._name);
			}
			this._arguments[1].Bind(table, list);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002B1D2 File Offset: 0x000293D2
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000302EC File Offset: 0x0002E4EC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			object[] array = new object[this._argumentCount];
			if (FunctionNode.s_funcs[this._info]._id == FunctionId.Convert)
			{
				if (this._argumentCount != 2)
				{
					throw ExprException.FunctionArgumentCount(this._name);
				}
				array[0] = this._arguments[0].Eval(row, version);
				array[1] = this.GetDataType(this._arguments[1]);
			}
			else if (FunctionNode.s_funcs[this._info]._id != FunctionId.Iif)
			{
				for (int i = 0; i < this._argumentCount; i++)
				{
					array[i] = this._arguments[i].Eval(row, version);
					if (FunctionNode.s_funcs[this._info]._isValidateArguments)
					{
						if (array[i] == DBNull.Value || typeof(object) == FunctionNode.s_funcs[this._info]._parameters[i])
						{
							return DBNull.Value;
						}
						if (array[i].GetType() != FunctionNode.s_funcs[this._info]._parameters[i])
						{
							if (FunctionNode.s_funcs[this._info]._parameters[i] == typeof(int) && ExpressionNode.IsInteger(DataStorage.GetStorageType(array[i].GetType())))
							{
								array[i] = Convert.ToInt32(array[i], base.FormatProvider);
							}
							else
							{
								if (FunctionNode.s_funcs[this._info]._id != FunctionId.Trim && FunctionNode.s_funcs[this._info]._id != FunctionId.Substring && FunctionNode.s_funcs[this._info]._id != FunctionId.Len)
								{
									throw ExprException.ArgumentType(FunctionNode.s_funcs[this._info]._name, i + 1, FunctionNode.s_funcs[this._info]._parameters[i]);
								}
								if (typeof(string) != array[i].GetType() && typeof(SqlString) != array[i].GetType())
								{
									throw ExprException.ArgumentType(FunctionNode.s_funcs[this._info]._name, i + 1, FunctionNode.s_funcs[this._info]._parameters[i]);
								}
							}
						}
					}
				}
			}
			return this.EvalFunction(FunctionNode.s_funcs[this._info]._id, array, row, version);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0003053E File Offset: 0x0002E73E
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0003054C File Offset: 0x0002E74C
		internal override bool IsConstant()
		{
			bool flag = true;
			for (int i = 0; i < this._argumentCount; i++)
			{
				flag = flag && this._arguments[i].IsConstant();
			}
			return flag;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00030584 File Offset: 0x0002E784
		internal override bool IsTableConstant()
		{
			for (int i = 0; i < this._argumentCount; i++)
			{
				if (!this._arguments[i].IsTableConstant())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000305B4 File Offset: 0x0002E7B4
		internal override bool HasLocalAggregate()
		{
			for (int i = 0; i < this._argumentCount; i++)
			{
				if (this._arguments[i].HasLocalAggregate())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000305E4 File Offset: 0x0002E7E4
		internal override bool HasRemoteAggregate()
		{
			for (int i = 0; i < this._argumentCount; i++)
			{
				if (this._arguments[i].HasRemoteAggregate())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00030614 File Offset: 0x0002E814
		internal override bool DependsOn(DataColumn column)
		{
			for (int i = 0; i < this._argumentCount; i++)
			{
				if (this._arguments[i].DependsOn(column))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00030648 File Offset: 0x0002E848
		internal override ExpressionNode Optimize()
		{
			for (int i = 0; i < this._argumentCount; i++)
			{
				this._arguments[i] = this._arguments[i].Optimize();
			}
			if (FunctionNode.s_funcs[this._info]._id == FunctionId.In)
			{
				if (!this.IsConstant())
				{
					throw ExprException.NonConstantArgument();
				}
			}
			else if (this.IsConstant())
			{
				return new ConstNode(base.table, ValueType.Object, this.Eval(), false);
			}
			return this;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000306BC File Offset: 0x0002E8BC
		private Type GetDataType(ExpressionNode node)
		{
			Type type = node.GetType();
			string text = null;
			if (type == typeof(NameNode))
			{
				text = ((NameNode)node)._name;
			}
			if (type == typeof(ConstNode))
			{
				text = ((ConstNode)node)._val.ToString();
			}
			if (text == null)
			{
				throw ExprException.ArgumentType(FunctionNode.s_funcs[this._info]._name, 2, typeof(Type));
			}
			Type type2 = Type.GetType(text);
			if (type2 == null)
			{
				throw ExprException.InvalidType(text);
			}
			return type2;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0003074C File Offset: 0x0002E94C
		private object EvalFunction(FunctionId id, object[] argumentValues, DataRow row, DataRowVersion version)
		{
			if (id != FunctionId.Charindex)
			{
				if (id != FunctionId.Len)
				{
					switch (id)
					{
					case FunctionId.Substring:
					{
						int num = (int)argumentValues[1] - 1;
						int num2 = (int)argumentValues[2];
						if (num < 0)
						{
							throw ExprException.FunctionArgumentOutOfRange("index", "Substring");
						}
						if (num2 < 0)
						{
							throw ExprException.FunctionArgumentOutOfRange("length", "Substring");
						}
						if (num2 == 0)
						{
							return string.Empty;
						}
						if (argumentValues[0] is SqlString)
						{
							argumentValues[0] = ((SqlString)argumentValues[0]).Value;
						}
						int length = ((string)argumentValues[0]).Length;
						if (num > length)
						{
							return DBNull.Value;
						}
						if (num + num2 > length)
						{
							num2 = length - num;
						}
						return ((string)argumentValues[0]).Substring(num, num2);
					}
					case FunctionId.IsNull:
						if (DataStorage.IsObjectNull(argumentValues[0]))
						{
							return argumentValues[1];
						}
						return argumentValues[0];
					case FunctionId.Iif:
						if (DataExpression.ToBoolean(this._arguments[0].Eval(row, version)))
						{
							return this._arguments[1].Eval(row, version);
						}
						return this._arguments[2].Eval(row, version);
					case FunctionId.Convert:
					{
						if (this._argumentCount != 2)
						{
							throw ExprException.FunctionArgumentCount(this._name);
						}
						if (argumentValues[0] == DBNull.Value)
						{
							return DBNull.Value;
						}
						Type type = (Type)argumentValues[1];
						StorageType storageType = DataStorage.GetStorageType(type);
						StorageType storageType2 = DataStorage.GetStorageType(argumentValues[0].GetType());
						if (storageType == StorageType.DateTimeOffset && storageType2 == StorageType.String)
						{
							return SqlConvert.ConvertStringToDateTimeOffset((string)argumentValues[0], base.FormatProvider);
						}
						if (StorageType.Object == storageType)
						{
							return argumentValues[0];
						}
						if (storageType == StorageType.Guid && storageType2 == StorageType.String)
						{
							return new Guid((string)argumentValues[0]);
						}
						if (!ExpressionNode.IsFloatSql(storageType2) || !ExpressionNode.IsIntegerSql(storageType))
						{
							return SqlConvert.ChangeType2(argumentValues[0], storageType, type, base.FormatProvider);
						}
						if (StorageType.Single == storageType2)
						{
							return SqlConvert.ChangeType2((float)SqlConvert.ChangeType2(argumentValues[0], StorageType.Single, typeof(float), base.FormatProvider), storageType, type, base.FormatProvider);
						}
						if (StorageType.Double == storageType2)
						{
							return SqlConvert.ChangeType2((double)SqlConvert.ChangeType2(argumentValues[0], StorageType.Double, typeof(double), base.FormatProvider), storageType, type, base.FormatProvider);
						}
						if (StorageType.Decimal == storageType2)
						{
							return SqlConvert.ChangeType2((decimal)SqlConvert.ChangeType2(argumentValues[0], StorageType.Decimal, typeof(decimal), base.FormatProvider), storageType, type, base.FormatProvider);
						}
						return SqlConvert.ChangeType2(argumentValues[0], storageType, type, base.FormatProvider);
					}
					case FunctionId.cInt:
						return Convert.ToInt32(argumentValues[0], base.FormatProvider);
					case FunctionId.cBool:
					{
						StorageType storageType2 = DataStorage.GetStorageType(argumentValues[0].GetType());
						if (storageType2 <= StorageType.Int32)
						{
							if (storageType2 == StorageType.Boolean)
							{
								return (bool)argumentValues[0];
							}
							if (storageType2 == StorageType.Int32)
							{
								return (int)argumentValues[0] != 0;
							}
						}
						else
						{
							if (storageType2 == StorageType.Double)
							{
								return (double)argumentValues[0] != 0.0;
							}
							if (storageType2 == StorageType.String)
							{
								return bool.Parse((string)argumentValues[0]);
							}
						}
						throw ExprException.DatatypeConvertion(argumentValues[0].GetType(), typeof(bool));
					}
					case FunctionId.cDate:
						return Convert.ToDateTime(argumentValues[0], base.FormatProvider);
					case FunctionId.cDbl:
						return Convert.ToDouble(argumentValues[0], base.FormatProvider);
					case FunctionId.cStr:
						return Convert.ToString(argumentValues[0], base.FormatProvider);
					case FunctionId.Abs:
					{
						StorageType storageType2 = DataStorage.GetStorageType(argumentValues[0].GetType());
						if (ExpressionNode.IsInteger(storageType2))
						{
							return Math.Abs((long)argumentValues[0]);
						}
						if (ExpressionNode.IsNumeric(storageType2))
						{
							return Math.Abs((double)argumentValues[0]);
						}
						throw ExprException.ArgumentTypeInteger(FunctionNode.s_funcs[this._info]._name, 1);
					}
					case FunctionId.In:
						throw ExprException.NYI(FunctionNode.s_funcs[this._info]._name);
					case FunctionId.Trim:
						if (DataStorage.IsObjectNull(argumentValues[0]))
						{
							return DBNull.Value;
						}
						if (argumentValues[0] is SqlString)
						{
							argumentValues[0] = ((SqlString)argumentValues[0]).Value;
						}
						return ((string)argumentValues[0]).Trim();
					case FunctionId.DateTimeOffset:
						if (argumentValues[0] == DBNull.Value || argumentValues[1] == DBNull.Value || argumentValues[2] == DBNull.Value)
						{
							return DBNull.Value;
						}
						switch (((DateTime)argumentValues[0]).Kind)
						{
						case DateTimeKind.Utc:
							if ((int)argumentValues[1] != 0 && (int)argumentValues[2] != 0)
							{
								throw ExprException.MismatchKindandTimeSpan();
							}
							break;
						case DateTimeKind.Local:
							if (DateTimeOffset.Now.Offset.Hours != (int)argumentValues[1] && DateTimeOffset.Now.Offset.Minutes != (int)argumentValues[2])
							{
								throw ExprException.MismatchKindandTimeSpan();
							}
							break;
						}
						if ((int)argumentValues[1] < -14 || (int)argumentValues[1] > 14)
						{
							throw ExprException.InvalidHoursArgument();
						}
						if ((int)argumentValues[2] < -59 || (int)argumentValues[2] > 59)
						{
							throw ExprException.InvalidMinutesArgument();
						}
						if ((int)argumentValues[1] == 14 && (int)argumentValues[2] > 0)
						{
							throw ExprException.InvalidTimeZoneRange();
						}
						if ((int)argumentValues[1] == -14 && (int)argumentValues[2] < 0)
						{
							throw ExprException.InvalidTimeZoneRange();
						}
						return new DateTimeOffset((DateTime)argumentValues[0], new TimeSpan((int)argumentValues[1], (int)argumentValues[2], 0));
					}
					throw ExprException.UndefinedFunction(FunctionNode.s_funcs[this._info]._name);
				}
				if (argumentValues[0] is SqlString)
				{
					if (((SqlString)argumentValues[0]).IsNull)
					{
						return DBNull.Value;
					}
					argumentValues[0] = ((SqlString)argumentValues[0]).Value;
				}
				return ((string)argumentValues[0]).Length;
			}
			else
			{
				if (DataStorage.IsObjectNull(argumentValues[0]) || DataStorage.IsObjectNull(argumentValues[1]))
				{
					return DBNull.Value;
				}
				if (argumentValues[0] is SqlString)
				{
					argumentValues[0] = ((SqlString)argumentValues[0]).Value;
				}
				if (argumentValues[1] is SqlString)
				{
					argumentValues[1] = ((SqlString)argumentValues[1]).Value;
				}
				return ((string)argumentValues[1]).IndexOf((string)argumentValues[0], StringComparison.Ordinal);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00030DD6 File Offset: 0x0002EFD6
		internal FunctionId Aggregate
		{
			get
			{
				if (this.IsAggregate)
				{
					return FunctionNode.s_funcs[this._info]._id;
				}
				return FunctionId.none;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00030DF4 File Offset: 0x0002EFF4
		internal bool IsAggregate
		{
			get
			{
				return FunctionNode.s_funcs[this._info]._id == FunctionId.Sum || FunctionNode.s_funcs[this._info]._id == FunctionId.Avg || FunctionNode.s_funcs[this._info]._id == FunctionId.Min || FunctionNode.s_funcs[this._info]._id == FunctionId.Max || FunctionNode.s_funcs[this._info]._id == FunctionId.Count || FunctionNode.s_funcs[this._info]._id == FunctionId.StDev || FunctionNode.s_funcs[this._info]._id == FunctionId.Var;
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00030E98 File Offset: 0x0002F098
		internal void Check()
		{
			Function function = FunctionNode.s_funcs[this._info];
			if (this._info < 0)
			{
				throw ExprException.UndefinedFunction(this._name);
			}
			if (FunctionNode.s_funcs[this._info]._isVariantArgumentList)
			{
				if (this._argumentCount < FunctionNode.s_funcs[this._info]._argumentCount)
				{
					if (FunctionNode.s_funcs[this._info]._id == FunctionId.In)
					{
						throw ExprException.InWithoutList();
					}
					throw ExprException.FunctionArgumentCount(this._name);
				}
			}
			else if (this._argumentCount != FunctionNode.s_funcs[this._info]._argumentCount)
			{
				throw ExprException.FunctionArgumentCount(this._name);
			}
		}

		// Token: 0x040006C8 RID: 1736
		internal readonly string _name;

		// Token: 0x040006C9 RID: 1737
		internal readonly int _info = -1;

		// Token: 0x040006CA RID: 1738
		internal int _argumentCount;

		// Token: 0x040006CB RID: 1739
		internal const int initialCapacity = 1;

		// Token: 0x040006CC RID: 1740
		internal ExpressionNode[] _arguments;

		// Token: 0x040006CD RID: 1741
		private static readonly Function[] s_funcs = new Function[]
		{
			new Function("Abs", FunctionId.Abs, typeof(object), true, false, 1, typeof(object), null, null),
			new Function("IIf", FunctionId.Iif, typeof(object), false, false, 3, typeof(object), typeof(object), typeof(object)),
			new Function("In", FunctionId.In, typeof(bool), false, true, 1, null, null, null),
			new Function("IsNull", FunctionId.IsNull, typeof(object), false, false, 2, typeof(object), typeof(object), null),
			new Function("Len", FunctionId.Len, typeof(int), true, false, 1, typeof(string), null, null),
			new Function("Substring", FunctionId.Substring, typeof(string), true, false, 3, typeof(string), typeof(int), typeof(int)),
			new Function("Trim", FunctionId.Trim, typeof(string), true, false, 1, typeof(string), null, null),
			new Function("Convert", FunctionId.Convert, typeof(object), false, true, 1, typeof(object), null, null),
			new Function("DateTimeOffset", FunctionId.DateTimeOffset, typeof(DateTimeOffset), false, true, 3, typeof(DateTime), typeof(int), typeof(int)),
			new Function("Max", FunctionId.Max, typeof(object), false, false, 1, null, null, null),
			new Function("Min", FunctionId.Min, typeof(object), false, false, 1, null, null, null),
			new Function("Sum", FunctionId.Sum, typeof(object), false, false, 1, null, null, null),
			new Function("Count", FunctionId.Count, typeof(object), false, false, 1, null, null, null),
			new Function("Var", FunctionId.Var, typeof(object), false, false, 1, null, null, null),
			new Function("StDev", FunctionId.StDev, typeof(object), false, false, 1, null, null, null),
			new Function("Avg", FunctionId.Avg, typeof(object), false, false, 1, null, null, null)
		};
	}
}
