using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Provides a way of reading a forward-only stream of data rows from a data source. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000290 RID: 656
	public sealed class OdbcDataReader : DbDataReader
	{
		// Token: 0x06001C94 RID: 7316 RVA: 0x0008B72C File Offset: 0x0008992C
		internal OdbcDataReader(OdbcCommand command, CMDWrapper cmdWrapper, CommandBehavior commandbehavior)
		{
			this._recordAffected = -1;
			this._row = -1;
			this._column = -1;
			this.ObjectID = Interlocked.Increment(ref OdbcDataReader.s_objectTypeCount);
			base..ctor();
			this._command = command;
			this._commandBehavior = commandbehavior;
			this._cmdText = command.CommandText;
			this._cmdWrapper = cmdWrapper;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0008B788 File Offset: 0x00089988
		private CNativeBuffer Buffer
		{
			get
			{
				CNativeBuffer dataReaderBuf = this._cmdWrapper._dataReaderBuf;
				if (dataReaderBuf == null)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return dataReaderBuf;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0008B7B6 File Offset: 0x000899B6
		private OdbcConnection Connection
		{
			get
			{
				if (this._cmdWrapper != null)
				{
					return this._cmdWrapper.Connection;
				}
				return null;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0008B7CD File Offset: 0x000899CD
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0008B7D5 File Offset: 0x000899D5
		internal OdbcCommand Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0008B7DE File Offset: 0x000899DE
		private OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._cmdWrapper.StatementHandle;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0008B7EB File Offset: 0x000899EB
		private OdbcStatementHandle KeyInfoStatementHandle
		{
			get
			{
				return this._cmdWrapper.KeyInfoStatement;
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0008B7F8 File Offset: 0x000899F8
		internal bool IsBehavior(CommandBehavior behavior)
		{
			return this.IsCommandBehavior(behavior);
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0008B801 File Offset: 0x00089A01
		internal bool IsCancelingCommand
		{
			get
			{
				return this._command != null && this._command.Canceling;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0008B818 File Offset: 0x00089A18
		internal bool IsNonCancelingCommand
		{
			get
			{
				return this._command != null && !this._command.Canceling;
			}
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0005EF00 File Offset: 0x0005D100
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid record set, 0; otherwise the number of columns in the current record. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0008B834 File Offset: 0x00089A34
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._noMoreResults)
				{
					return 0;
				}
				if (this._dataCache == null)
				{
					short num;
					ODBC32.RetCode retCode = this.FieldCountNoThrow(out num);
					if (retCode != ODBC32.RetCode.SUCCESS)
					{
						this.Connection.HandleError(this.StatementHandle, retCode);
					}
				}
				if (this._dataCache == null)
				{
					return 0;
				}
				return this._dataCache._count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0008B899 File Offset: 0x00089A99
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._hasRows == OdbcDataReader.HasRowsStatus.DontKnow)
				{
					this.Read();
					this._skipReadOnce = true;
				}
				return this._hasRows == OdbcDataReader.HasRowsStatus.HasRows;
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0008B8D0 File Offset: 0x00089AD0
		internal ODBC32.RetCode FieldCountNoThrow(out short cColsAffected)
		{
			if (this.IsCancelingCommand)
			{
				cColsAffected = 0;
				return ODBC32.RetCode.ERROR;
			}
			ODBC32.RetCode retCode = this.StatementHandle.NumberOfResultColumns(out cColsAffected);
			if (retCode == ODBC32.RetCode.SUCCESS)
			{
				this._hiddenColumns = 0;
				if (this.IsCommandBehavior(CommandBehavior.KeyInfo) && !this.Connection.ProviderInfo.NoSqlSoptSSNoBrowseTable && !this.Connection.ProviderInfo.NoSqlSoptSSHiddenColumns)
				{
					for (int i = 0; i < (int)cColsAffected; i++)
					{
						if (this.GetColAttribute(i, (ODBC32.SQL_DESC)1211, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE).ToInt64() == 1L)
						{
							this._hiddenColumns = (int)cColsAffected - i;
							cColsAffected = (short)i;
							break;
						}
					}
				}
				this._dataCache = new DbCache(this, (int)cColsAffected);
			}
			else
			{
				cColsAffected = 0;
			}
			return retCode;
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0008B979 File Offset: 0x00089B79
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0008B984 File Offset: 0x00089B84
		private SQLLEN GetRowCount()
		{
			if (!this.IsClosed)
			{
				SQLLEN sqllen;
				ODBC32.RetCode retCode = this.StatementHandle.RowCount(out sqllen);
				if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					return sqllen;
				}
			}
			return -1;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0008B9B6 File Offset: 0x00089BB6
		internal int CalculateRecordsAffected(int cRowsAffected)
		{
			if (0 <= cRowsAffected)
			{
				if (-1 == this._recordAffected)
				{
					this._recordAffected = cRowsAffected;
				}
				else
				{
					this._recordAffected += cRowsAffected;
				}
			}
			return this._recordAffected;
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected, or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0008B9E2 File Offset: 0x00089BE2
		public override int RecordsAffected
		{
			get
			{
				return this._recordAffected;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700053B RID: 1339
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="value">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700053C RID: 1340
		public override object this[string value]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(value));
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CA8 RID: 7336 RVA: 0x0008B9EA File Offset: 0x00089BEA
		public override void Close()
		{
			this.Close(false);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0008B9F4 File Offset: 0x00089BF4
		private void Close(bool disposing)
		{
			Exception ex = null;
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null && cmdWrapper.StatementHandle != null)
			{
				if (this.IsNonCancelingCommand)
				{
					this.NextResult(disposing, !disposing);
					if (this._command != null)
					{
						if (this._command.HasParameters)
						{
							this._command.Parameters.GetOutputValues(this._cmdWrapper);
						}
						cmdWrapper.FreeStatementHandle(ODBC32.STMT.CLOSE);
						this._command.CloseFromDataReader();
					}
				}
				cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.CLOSE);
			}
			if (this._command != null)
			{
				this._command.CloseFromDataReader();
				if (this.IsCommandBehavior(CommandBehavior.CloseConnection))
				{
					this._command.Parameters.RebindCollection = true;
					this.Connection.Close();
				}
			}
			else if (cmdWrapper != null)
			{
				cmdWrapper.Dispose();
			}
			this._command = null;
			this._isClosed = true;
			this._dataCache = null;
			this._metadata = null;
			this._schemaTable = null;
			this._isRead = false;
			this._hasRows = OdbcDataReader.HasRowsStatus.DontKnow;
			this._isValidResult = false;
			this._noMoreResults = true;
			this._noMoreRows = true;
			this._fieldNameLookup = null;
			this.SetCurrentRowColumnInfo(-1, 0);
			if (ex != null && !disposing)
			{
				throw ex;
			}
			this._cmdWrapper = null;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0008BB15 File Offset: 0x00089D15
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(true);
			}
		}

		/// <summary>Gets the name of the source data type.</summary>
		/// <returns>The name of the source data type.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CAB RID: 7339 RVA: 0x0008BB24 File Offset: 0x00089D24
		public override string GetDataTypeName(int i)
		{
			if (this._dataCache != null)
			{
				DbSchemaInfo schema = this._dataCache.GetSchema(i);
				if (schema._typename == null)
				{
					schema._typename = this.GetColAttributeStr(i, ODBC32.SQL_DESC.TYPE_NAME, ODBC32.SQL_COLUMN.TYPE_NAME, ODBC32.HANDLER.THROW);
				}
				return schema._typename;
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		// Token: 0x06001CAC RID: 7340 RVA: 0x0008BB6C File Offset: 0x00089D6C
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CAD RID: 7341 RVA: 0x0008BB7C File Offset: 0x00089D7C
		public override Type GetFieldType(int i)
		{
			if (this._dataCache != null)
			{
				DbSchemaInfo schema = this._dataCache.GetSchema(i);
				if (schema._type == null)
				{
					schema._type = this.GetSqlType(i)._type;
				}
				return schema._type;
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>A string that is the name of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CAE RID: 7342 RVA: 0x0008BBCC File Offset: 0x00089DCC
		public override string GetName(int i)
		{
			if (this._dataCache != null)
			{
				DbSchemaInfo schema = this._dataCache.GetSchema(i);
				if (schema._name == null)
				{
					schema._name = this.GetColAttributeStr(i, ODBC32.SQL_DESC.NAME, ODBC32.SQL_COLUMN.NAME, ODBC32.HANDLER.THROW);
					if (schema._name == null)
					{
						schema._name = "";
					}
				}
				return schema._name;
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="value">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CAF RID: 7343 RVA: 0x0008BC29 File Offset: 0x00089E29
		public override int GetOrdinal(string value)
		{
			if (this._fieldNameLookup == null)
			{
				if (this._dataCache == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(value);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0008BC5A File Offset: 0x00089E5A
		private int IndexOf(string value)
		{
			if (this._fieldNameLookup == null)
			{
				if (this._dataCache == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.IndexOf(value);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0008BC8B File Offset: 0x00089E8B
		private bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0008BC98 File Offset: 0x00089E98
		internal object GetValue(int i, TypeMap typemap)
		{
			ODBC32.SQL_TYPE sql_type = typemap._sql_type;
			if (sql_type != ODBC32.SQL_TYPE.SS_VARIANT)
			{
				switch (sql_type)
				{
				case ODBC32.SQL_TYPE.GUID:
					return this.internalGetGuid(i);
				case ODBC32.SQL_TYPE.WLONGVARCHAR:
				case ODBC32.SQL_TYPE.WVARCHAR:
				case ODBC32.SQL_TYPE.WCHAR:
				case ODBC32.SQL_TYPE.LONGVARCHAR:
				case ODBC32.SQL_TYPE.CHAR:
				case ODBC32.SQL_TYPE.VARCHAR:
					return this.internalGetString(i);
				case ODBC32.SQL_TYPE.BIT:
					return this.internalGetBoolean(i);
				case ODBC32.SQL_TYPE.TINYINT:
					return this.internalGetByte(i);
				case ODBC32.SQL_TYPE.BIGINT:
					return this.internalGetInt64(i);
				case ODBC32.SQL_TYPE.LONGVARBINARY:
				case ODBC32.SQL_TYPE.VARBINARY:
				case ODBC32.SQL_TYPE.BINARY:
					return this.internalGetBytes(i);
				case (ODBC32.SQL_TYPE)0:
				case (ODBC32.SQL_TYPE)9:
				case (ODBC32.SQL_TYPE)10:
				case ODBC32.SQL_TYPE.TIMESTAMP:
					break;
				case ODBC32.SQL_TYPE.NUMERIC:
				case ODBC32.SQL_TYPE.DECIMAL:
					return this.internalGetDecimal(i);
				case ODBC32.SQL_TYPE.INTEGER:
					return this.internalGetInt32(i);
				case ODBC32.SQL_TYPE.SMALLINT:
					return this.internalGetInt16(i);
				case ODBC32.SQL_TYPE.FLOAT:
				case ODBC32.SQL_TYPE.DOUBLE:
					return this.internalGetDouble(i);
				case ODBC32.SQL_TYPE.REAL:
					return this.internalGetFloat(i);
				default:
					switch (sql_type)
					{
					case ODBC32.SQL_TYPE.TYPE_DATE:
						return this.internalGetDate(i);
					case ODBC32.SQL_TYPE.TYPE_TIME:
						return this.internalGetTime(i);
					case ODBC32.SQL_TYPE.TYPE_TIMESTAMP:
						return this.internalGetDateTime(i);
					}
					break;
				}
				return this.internalGetBytes(i);
			}
			if (!this._isRead)
			{
				throw ADP.DataReaderNoData();
			}
			int num;
			if (this._dataCache.AccessIndex(i) == null && this.QueryFieldInfo(i, ODBC32.SQL_C.BINARY, out num))
			{
				ODBC32.SQL_TYPE sql_TYPE = (ODBC32.SQL_TYPE)this.GetColAttribute(i, (ODBC32.SQL_DESC)1216, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.THROW);
				return this.GetValue(i, TypeMap.FromSqlType(sql_TYPE));
			}
			return this._dataCache[i];
		}

		/// <summary>Gets the value of the column at the specified ordinal in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x0008BE0C File Offset: 0x0008A00C
		public override object GetValue(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null)
				{
					this._dataCache[i] = this.GetValue(i, this.GetSqlType(i));
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of type <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CB4 RID: 7348 RVA: 0x0008BE5C File Offset: 0x0008A05C
		public override int GetValues(object[] values)
		{
			if (this._isRead)
			{
				int num = Math.Min(values.Length, this.FieldCount);
				for (int i = 0; i < num; i++)
				{
					values[i] = this.GetValue(i);
				}
				return num;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0008BEA0 File Offset: 0x0008A0A0
		private TypeMap GetSqlType(int i)
		{
			DbSchemaInfo schema = this._dataCache.GetSchema(i);
			TypeMap typeMap;
			if (schema._dbtype == null)
			{
				schema._dbtype = new ODBC32.SQL_TYPE?((ODBC32.SQL_TYPE)this.GetColAttribute(i, ODBC32.SQL_DESC.CONCISE_TYPE, ODBC32.SQL_COLUMN.TYPE, ODBC32.HANDLER.THROW));
				typeMap = TypeMap.FromSqlType(schema._dbtype.Value);
				if (typeMap._signType)
				{
					bool flag = this.GetColAttribute(i, ODBC32.SQL_DESC.UNSIGNED, ODBC32.SQL_COLUMN.UNSIGNED, ODBC32.HANDLER.THROW).ToInt64() != 0L;
					typeMap = TypeMap.UpgradeSignedType(typeMap, flag);
					schema._dbtype = new ODBC32.SQL_TYPE?(typeMap._sql_type);
				}
			}
			else
			{
				typeMap = TypeMap.FromSqlType(schema._dbtype.Value);
			}
			this.Connection.SetSupportedType(schema._dbtype.Value);
			return typeMap;
		}

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CB6 RID: 7350 RVA: 0x0008BF58 File Offset: 0x0008A158
		public override bool IsDBNull(int i)
		{
			if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				return Convert.IsDBNull(this.GetValue(i));
			}
			object obj = this._dataCache[i];
			if (obj != null)
			{
				return Convert.IsDBNull(obj);
			}
			TypeMap sqlType = this.GetSqlType(i);
			if (sqlType._bufferSize > 0)
			{
				return Convert.IsDBNull(this.GetValue(i));
			}
			int num;
			return !this.QueryFieldInfo(i, sqlType._sql_c, out num);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CB7 RID: 7351 RVA: 0x0008BFC3 File Offset: 0x0008A1C3
		public override byte GetByte(int i)
		{
			return (byte)this.internalGetByte(i);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0008BFD4 File Offset: 0x0008A1D4
		private object internalGetByte(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.UTINYINT))
				{
					this._dataCache[i] = this.Buffer.ReadByte(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column as a character.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CB9 RID: 7353 RVA: 0x0008C031 File Offset: 0x0008A231
		public override char GetChar(int i)
		{
			return (char)this.internalGetChar(i);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0008C040 File Offset: 0x0008A240
		private object internalGetChar(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					this._dataCache[i] = this.Buffer.ReadChar(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 16-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CBB RID: 7355 RVA: 0x0008C09D File Offset: 0x0008A29D
		public override short GetInt16(int i)
		{
			return (short)this.internalGetInt16(i);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0008C0AC File Offset: 0x0008A2AC
		private object internalGetInt16(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.SSHORT))
				{
					this._dataCache[i] = this.Buffer.ReadInt16(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 32-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CBD RID: 7357 RVA: 0x0008C109 File Offset: 0x0008A309
		public override int GetInt32(int i)
		{
			return (int)this.internalGetInt32(i);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0008C118 File Offset: 0x0008A318
		private object internalGetInt32(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.SLONG))
				{
					this._dataCache[i] = this.Buffer.ReadInt32(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 64-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CBF RID: 7359 RVA: 0x0008C175 File Offset: 0x0008A375
		public override long GetInt64(int i)
		{
			return (long)this.internalGetInt64(i);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0008C184 File Offset: 0x0008A384
		private object internalGetInt64(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					string text = (string)this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.WCHAR, -3);
					this._dataCache[i] = long.Parse(text, CultureInfo.InvariantCulture);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>A Boolean that is the value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CC1 RID: 7361 RVA: 0x0008C1F6 File Offset: 0x0008A3F6
		public override bool GetBoolean(int i)
		{
			return (bool)this.internalGetBoolean(i);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0008C204 File Offset: 0x0008A404
		private object internalGetBoolean(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.BIT))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.BIT, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column as a single-precision floating-point number.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0008C25F File Offset: 0x0008A45F
		public override float GetFloat(int i)
		{
			return (float)this.internalGetFloat(i);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0008C270 File Offset: 0x0008A470
		private object internalGetFloat(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.REAL))
				{
					this._dataCache[i] = this.Buffer.ReadSingle(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x0008C2CC File Offset: 0x0008A4CC
		public DateTime GetDate(int i)
		{
			return (DateTime)this.internalGetDate(i);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0008C2DC File Offset: 0x0008A4DC
		private object internalGetDate(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_DATE))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_DATE, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CC7 RID: 7367 RVA: 0x0008C337 File Offset: 0x0008A537
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.internalGetDateTime(i);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0008C348 File Offset: 0x0008A548
		private object internalGetDateTime(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_TIMESTAMP))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_TIMESTAMP, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.Decimal" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CC9 RID: 7369 RVA: 0x0008C3A3 File Offset: 0x0008A5A3
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.internalGetDecimal(i);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0008C3B4 File Offset: 0x0008A5B4
		private object internalGetDecimal(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					string text = null;
					try
					{
						text = (string)this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.WCHAR, -3);
						this._dataCache[i] = decimal.Parse(text, CultureInfo.InvariantCulture);
					}
					catch (OverflowException ex)
					{
						this._dataCache[i] = text;
						throw ex;
					}
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column as a double-precision floating-point number.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CCB RID: 7371 RVA: 0x0008C448 File Offset: 0x0008A648
		public override double GetDouble(int i)
		{
			return (double)this.internalGetDouble(i);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0008C458 File Offset: 0x0008A658
		private object internalGetDouble(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.DOUBLE))
				{
					this._dataCache[i] = this.Buffer.ReadDouble(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column as a GUID.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CCD RID: 7373 RVA: 0x0008C4B4 File Offset: 0x0008A6B4
		public override Guid GetGuid(int i)
		{
			return (Guid)this.internalGetGuid(i);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0008C4C4 File Offset: 0x0008A6C4
		private object internalGetGuid(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.GUID))
				{
					this._dataCache[i] = this.Buffer.ReadGuid(0);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.String" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CCF RID: 7375 RVA: 0x0008C521 File Offset: 0x0008A721
		public override string GetString(int i)
		{
			return (string)this.internalGetString(i);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0008C530 File Offset: 0x0008A730
		private object internalGetString(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null)
				{
					CNativeBuffer buffer = this.Buffer;
					int num = buffer.Length - 4;
					int num2;
					if (this.GetData(i, ODBC32.SQL_C.WCHAR, buffer.Length - 2, out num2))
					{
						if (num2 <= num && -4 != num2)
						{
							string text = buffer.PtrToStringUni(0, Math.Min(num2, num) / 2);
							this._dataCache[i] = text;
							return text;
						}
						char[] array = new char[num / 2];
						StringBuilder stringBuilder = new StringBuilder(((num2 == -4) ? num : num2) / 2);
						int num3 = num;
						int num4 = ((-4 == num2) ? (-1) : (num2 - num3));
						bool data;
						do
						{
							int num5 = num3 / 2;
							buffer.ReadChars(0, array, 0, num5);
							stringBuilder.Append(array, 0, num5);
							if (num4 == 0)
							{
								break;
							}
							data = this.GetData(i, ODBC32.SQL_C.WCHAR, buffer.Length - 2, out num2);
							if (-4 != num2)
							{
								num3 = Math.Min(num2, num);
								if (0 < num4)
								{
									num4 -= num3;
								}
								else
								{
									num4 = 0;
								}
							}
						}
						while (data);
						this._dataCache[i] = stringBuilder.ToString();
					}
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.TimeSpan" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CD1 RID: 7377 RVA: 0x0008C658 File Offset: 0x0008A858
		public TimeSpan GetTime(int i)
		{
			return (TimeSpan)this.internalGetTime(i);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0008C668 File Offset: 0x0008A868
		private object internalGetTime(int i)
		{
			if (this._isRead)
			{
				if (this._dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_TIME))
				{
					this._dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_TIME, -1);
				}
				return this._dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0008C6C3 File Offset: 0x0008A8C3
		private void SetCurrentRowColumnInfo(int row, int column)
		{
			if (this._row != row || this._column != column)
			{
				this._row = row;
				this._column = column;
				this._sequentialBytesRead = 0L;
			}
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the particular buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field where the read operation is to start. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CD4 RID: 7380 RVA: 0x0008C6ED File Offset: 0x0008A8ED
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this.GetBytesOrChars(i, dataIndex, buffer, false, bufferIndex, length);
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array, starting at the particular buffer offset.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the row where the read operation is to start. </param>
		/// <param name="buffer">The buffer into which to copy data. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of characters to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008C6FD File Offset: 0x0008A8FD
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this.GetBytesOrChars(i, dataIndex, buffer, true, bufferIndex, length);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0008C710 File Offset: 0x0008A910
		private long GetBytesOrChars(int i, long dataIndex, Array buffer, bool isCharsBuffer, int bufferIndex, int length)
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderNoData();
			}
			if (!this._isRead)
			{
				throw ADP.DataReaderNoData();
			}
			if (dataIndex < 0L)
			{
				throw ADP.ArgumentOutOfRange("dataIndex");
			}
			if (bufferIndex < 0)
			{
				throw ADP.ArgumentOutOfRange("bufferIndex");
			}
			if (length < 0)
			{
				throw ADP.ArgumentOutOfRange("length");
			}
			string text = (isCharsBuffer ? "GetChars" : "GetBytes");
			this.SetCurrentRowColumnInfo(this._row, i);
			object obj;
			if (isCharsBuffer)
			{
				obj = (string)this._dataCache[i];
			}
			else
			{
				obj = (byte[])this._dataCache[i];
			}
			if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) || obj != null)
			{
				if (2147483647L < dataIndex)
				{
					throw ADP.ArgumentOutOfRange("dataIndex");
				}
				if (obj == null)
				{
					if (isCharsBuffer)
					{
						obj = (string)this.internalGetString(i);
					}
					else
					{
						obj = (byte[])this.internalGetBytes(i);
					}
				}
				int num = (isCharsBuffer ? ((string)obj).Length : ((byte[])obj).Length);
				if (buffer == null)
				{
					return (long)num;
				}
				if (length == 0)
				{
					return 0L;
				}
				if (dataIndex >= (long)num)
				{
					return 0L;
				}
				int num2 = Math.Min(num - (int)dataIndex, length);
				num2 = Math.Min(num2, buffer.Length - bufferIndex);
				if (num2 <= 0)
				{
					return 0L;
				}
				if (isCharsBuffer)
				{
					((string)obj).CopyTo((int)dataIndex, (char[])buffer, bufferIndex, num2);
				}
				else
				{
					Array.Copy((byte[])obj, (int)dataIndex, (byte[])buffer, bufferIndex, num2);
				}
				return (long)num2;
			}
			else if (buffer == null)
			{
				ODBC32.SQL_C sql_C = (isCharsBuffer ? ODBC32.SQL_C.WCHAR : ODBC32.SQL_C.BINARY);
				int num3;
				if (!this.QueryFieldInfo(i, sql_C, out num3))
				{
					if (isCharsBuffer)
					{
						throw ADP.InvalidCast();
					}
					return -1L;
				}
				else
				{
					if (isCharsBuffer)
					{
						return (long)(num3 / 2);
					}
					return (long)num3;
				}
			}
			else
			{
				if ((isCharsBuffer && dataIndex < this._sequentialBytesRead / 2L) || (!isCharsBuffer && dataIndex < this._sequentialBytesRead))
				{
					throw ADP.NonSeqByteAccess(dataIndex, this._sequentialBytesRead, text);
				}
				if (isCharsBuffer)
				{
					dataIndex -= this._sequentialBytesRead / 2L;
				}
				else
				{
					dataIndex -= this._sequentialBytesRead;
				}
				if (dataIndex > 0L && (long)this.readBytesOrCharsSequentialAccess(i, null, isCharsBuffer, 0, dataIndex) < dataIndex)
				{
					return 0L;
				}
				length = Math.Min(length, buffer.Length - bufferIndex);
				if (length > 0)
				{
					return (long)this.readBytesOrCharsSequentialAccess(i, buffer, isCharsBuffer, bufferIndex, (long)length);
				}
				int num4;
				if (isCharsBuffer && !this.QueryFieldInfo(i, ODBC32.SQL_C.WCHAR, out num4))
				{
					throw ADP.InvalidCast();
				}
				return 0L;
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0008C964 File Offset: 0x0008AB64
		private int readBytesOrCharsSequentialAccess(int i, Array buffer, bool isCharsBuffer, int bufferIndex, long bytesOrCharsLength)
		{
			int num = 0;
			long num2 = (isCharsBuffer ? checked(bytesOrCharsLength * 2L) : bytesOrCharsLength);
			CNativeBuffer buffer2 = this.Buffer;
			while (num2 > 0L)
			{
				int num3;
				int num4;
				bool flag;
				if (isCharsBuffer)
				{
					num3 = (int)Math.Min(num2, (long)(buffer2.Length - 4));
					flag = this.GetData(i, ODBC32.SQL_C.WCHAR, num3 + 2, out num4);
				}
				else
				{
					num3 = (int)Math.Min(num2, (long)(buffer2.Length - 2));
					flag = this.GetData(i, ODBC32.SQL_C.BINARY, num3, out num4);
				}
				if (!flag)
				{
					throw ADP.InvalidCast();
				}
				bool flag2 = false;
				if (num4 == 0)
				{
					break;
				}
				int num5;
				if (-4 == num4)
				{
					num5 = num3;
				}
				else if (num4 > num3)
				{
					num5 = num3;
				}
				else
				{
					num5 = num4;
					flag2 = true;
				}
				this._sequentialBytesRead += (long)num5;
				if (isCharsBuffer)
				{
					int num6 = num5 / 2;
					if (buffer != null)
					{
						buffer2.ReadChars(0, (char[])buffer, bufferIndex, num6);
						bufferIndex += num6;
					}
					num += num6;
				}
				else
				{
					if (buffer != null)
					{
						buffer2.ReadBytes(0, (byte[])buffer, bufferIndex, num5);
						bufferIndex += num5;
					}
					num += num5;
				}
				num2 -= (long)num5;
				if (flag2)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0008CA74 File Offset: 0x0008AC74
		private object internalGetBytes(int i)
		{
			if (this._dataCache.AccessIndex(i) == null)
			{
				int num = this.Buffer.Length - 4;
				int num2 = 0;
				int j;
				if (this.GetData(i, ODBC32.SQL_C.BINARY, num, out j))
				{
					CNativeBuffer buffer = this.Buffer;
					byte[] array;
					if (-4 != j)
					{
						array = new byte[j];
						this.Buffer.ReadBytes(0, array, num2, Math.Min(j, num));
						while (j > num)
						{
							this.GetData(i, ODBC32.SQL_C.BINARY, num, out j);
							num2 += num;
							buffer.ReadBytes(0, array, num2, Math.Min(j, num));
						}
					}
					else
					{
						List<byte[]> list = new List<byte[]>();
						int num3 = 0;
						do
						{
							int num4 = ((-4 != j) ? j : num);
							array = new byte[num4];
							num3 += num4;
							buffer.ReadBytes(0, array, 0, num4);
							list.Add(array);
						}
						while (-4 == j && this.GetData(i, ODBC32.SQL_C.BINARY, num, out j));
						array = new byte[num3];
						foreach (byte[] array2 in list)
						{
							array2.CopyTo(array, num2);
							num2 += array2.Length;
						}
					}
					this._dataCache[i] = array;
				}
			}
			return this._dataCache[i];
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0008CBC8 File Offset: 0x0008ADC8
		private SQLLEN GetColAttribute(int iColumn, ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId, ODBC32.HANDLER handler)
		{
			short num = 0;
			if (this.Connection == null || this._cmdWrapper.Canceling)
			{
				return -1;
			}
			OdbcStatementHandle statementHandle = this.StatementHandle;
			SQLLEN sqllen;
			ODBC32.RetCode retCode;
			if (this.Connection.IsV3Driver)
			{
				retCode = statementHandle.ColumnAttribute(iColumn + 1, (short)v3FieldId, this.Buffer, out num, out sqllen);
			}
			else
			{
				if (v2FieldId == (ODBC32.SQL_COLUMN)(-1))
				{
					return 0;
				}
				retCode = statementHandle.ColumnAttribute(iColumn + 1, (short)v2FieldId, this.Buffer, out num, out sqllen);
			}
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if (retCode == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
				{
					this.Connection.FlagUnsupportedColAttr(v3FieldId, v2FieldId);
				}
				if (handler == ODBC32.HANDLER.THROW)
				{
					this.Connection.HandleError(statementHandle, retCode);
				}
				return -1;
			}
			return sqllen;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008CC88 File Offset: 0x0008AE88
		private string GetColAttributeStr(int i, ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId, ODBC32.HANDLER handler)
		{
			short num = 0;
			CNativeBuffer buffer = this.Buffer;
			buffer.WriteInt16(0, 0);
			OdbcStatementHandle statementHandle = this.StatementHandle;
			if (this.Connection == null || this._cmdWrapper.Canceling || statementHandle == null)
			{
				return "";
			}
			ODBC32.RetCode retCode;
			if (this.Connection.IsV3Driver)
			{
				SQLLEN sqllen;
				retCode = statementHandle.ColumnAttribute(i + 1, (short)v3FieldId, buffer, out num, out sqllen);
			}
			else
			{
				if (v2FieldId == (ODBC32.SQL_COLUMN)(-1))
				{
					return null;
				}
				SQLLEN sqllen;
				retCode = statementHandle.ColumnAttribute(i + 1, (short)v2FieldId, buffer, out num, out sqllen);
			}
			if (retCode != ODBC32.RetCode.SUCCESS || num == 0)
			{
				if (retCode == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
				{
					this.Connection.FlagUnsupportedColAttr(v3FieldId, v2FieldId);
				}
				if (handler == ODBC32.HANDLER.THROW)
				{
					this.Connection.HandleError(statementHandle, retCode);
				}
				return null;
			}
			return buffer.PtrToStringUni(0, (int)(num / 2));
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008CD54 File Offset: 0x0008AF54
		private string GetDescFieldStr(int i, ODBC32.SQL_DESC attribute, ODBC32.HANDLER handler)
		{
			int num = 0;
			if (this.Connection == null || this._cmdWrapper.Canceling)
			{
				return "";
			}
			if (!this.Connection.IsV3Driver)
			{
				return null;
			}
			CNativeBuffer buffer = this.Buffer;
			using (OdbcDescriptorHandle odbcDescriptorHandle = new OdbcDescriptorHandle(this.StatementHandle, ODBC32.SQL_ATTR.APP_PARAM_DESC))
			{
				ODBC32.RetCode descriptionField = odbcDescriptorHandle.GetDescriptionField(i + 1, attribute, buffer, out num);
				if (descriptionField != ODBC32.RetCode.SUCCESS || num == 0)
				{
					if (descriptionField == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
					{
						this.Connection.FlagUnsupportedColAttr(attribute, ODBC32.SQL_COLUMN.COUNT);
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.Connection.HandleError(this.StatementHandle, descriptionField);
					}
					return null;
				}
			}
			return buffer.PtrToStringUni(0, num / 2);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008CE28 File Offset: 0x0008B028
		private bool QueryFieldInfo(int i, ODBC32.SQL_C sqlctype, out int cbLengthOrIndicator)
		{
			int num = 0;
			if (sqlctype == ODBC32.SQL_C.WCHAR)
			{
				num = 2;
			}
			return this.GetData(i, sqlctype, num, out cbLengthOrIndicator);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0008CE48 File Offset: 0x0008B048
		private bool GetData(int i, ODBC32.SQL_C sqlctype)
		{
			int num;
			return this.GetData(i, sqlctype, this.Buffer.Length - 4, out num);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0008CE6C File Offset: 0x0008B06C
		private bool GetData(int i, ODBC32.SQL_C sqlctype, int cb, out int cbLengthOrIndicator)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.IsCancelingCommand)
			{
				throw ADP.DataReaderNoData();
			}
			CNativeBuffer buffer = this.Buffer;
			ODBC32.RetCode data = this.StatementHandle.GetData(i + 1, sqlctype, buffer, cb, out intPtr);
			if (data != ODBC32.RetCode.SUCCESS)
			{
				if (data != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					if (data != ODBC32.RetCode.NO_DATA)
					{
						this.Connection.HandleError(this.StatementHandle, data);
					}
					else
					{
						if (sqlctype != ODBC32.SQL_C.WCHAR && sqlctype != ODBC32.SQL_C.BINARY)
						{
							this.Connection.HandleError(this.StatementHandle, data);
						}
						if (intPtr == (IntPtr)(-4))
						{
							intPtr = (IntPtr)0;
						}
					}
				}
				else if ((int)intPtr == -4)
				{
				}
			}
			this.SetCurrentRowColumnInfo(this._row, i);
			if (intPtr == (IntPtr)(-1))
			{
				this._dataCache[i] = DBNull.Value;
				cbLengthOrIndicator = 0;
				return false;
			}
			cbLengthOrIndicator = (int)intPtr;
			return true;
		}

		/// <summary>Advances the <see cref="T:System.Data.Odbc.OdbcDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CDF RID: 7391 RVA: 0x0008CF44 File Offset: 0x0008B144
		public override bool Read()
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("Read");
			}
			if (this.IsCancelingCommand)
			{
				this._isRead = false;
				return false;
			}
			if (this._skipReadOnce)
			{
				this._skipReadOnce = false;
				return this._isRead;
			}
			if (this._noMoreRows || this._noMoreResults || this.IsCommandBehavior(CommandBehavior.SchemaOnly))
			{
				return false;
			}
			if (!this._isValidResult)
			{
				return false;
			}
			ODBC32.RetCode retCode = this.StatementHandle.Fetch();
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if (retCode != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					if (retCode != ODBC32.RetCode.NO_DATA)
					{
						this.Connection.HandleError(this.StatementHandle, retCode);
					}
					else
					{
						this._isRead = false;
						if (this._hasRows == OdbcDataReader.HasRowsStatus.DontKnow)
						{
							this._hasRows = OdbcDataReader.HasRowsStatus.HasNoRows;
						}
					}
				}
				else
				{
					this.Connection.HandleErrorNoThrow(this.StatementHandle, retCode);
					this._hasRows = OdbcDataReader.HasRowsStatus.HasRows;
					this._isRead = true;
				}
			}
			else
			{
				this._hasRows = OdbcDataReader.HasRowsStatus.HasRows;
				this._isRead = true;
			}
			this._dataCache.FlushValues();
			if (this.IsCommandBehavior(CommandBehavior.SingleRow))
			{
				this._noMoreRows = true;
				this.SetCurrentRowColumnInfo(-1, 0);
			}
			else
			{
				this.SetCurrentRowColumnInfo(this._row + 1, 0);
			}
			return this._isRead;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0008D064 File Offset: 0x0008B264
		internal void FirstResult()
		{
			SQLLEN rowCount = this.GetRowCount();
			this.CalculateRecordsAffected(rowCount);
			short num;
			if (this.FieldCountNoThrow(out num) == ODBC32.RetCode.SUCCESS && num == 0)
			{
				this.NextResult();
				return;
			}
			this._isValidResult = true;
		}

		/// <summary>Advances the <see cref="T:System.Data.Odbc.OdbcDataReader" /> to the next result when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008D0A1 File Offset: 0x0008B2A1
		public override bool NextResult()
		{
			return this.NextResult(false, false);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0008D0AC File Offset: 0x0008B2AC
		private bool NextResult(bool disposing, bool allresults)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			bool flag = false;
			bool flag2 = this.IsCommandBehavior(CommandBehavior.SingleResult);
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("NextResult");
			}
			this._fieldNameLookup = null;
			if (this.IsCancelingCommand || this._noMoreResults)
			{
				return false;
			}
			this._isRead = false;
			this._hasRows = OdbcDataReader.HasRowsStatus.DontKnow;
			this._fieldNameLookup = null;
			this._metadata = null;
			this._schemaTable = null;
			int num = 0;
			OdbcErrorCollection odbcErrorCollection = null;
			ODBC32.RetCode retCode2;
			bool flag3;
			do
			{
				this._isValidResult = false;
				retCode2 = this.StatementHandle.MoreResults();
				flag3 = retCode2 == ODBC32.RetCode.SUCCESS || retCode2 == ODBC32.RetCode.SUCCESS_WITH_INFO;
				if (retCode2 == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this.Connection.HandleErrorNoThrow(this.StatementHandle, retCode2);
				}
				else if (!disposing && retCode2 != ODBC32.RetCode.NO_DATA && retCode2 != ODBC32.RetCode.SUCCESS)
				{
					if (odbcErrorCollection == null)
					{
						retCode = retCode2;
						odbcErrorCollection = new OdbcErrorCollection();
					}
					ODBC32.GetDiagErrors(odbcErrorCollection, null, this.StatementHandle, retCode2);
					num++;
				}
				if (!disposing && flag3)
				{
					num = 0;
					SQLLEN rowCount = this.GetRowCount();
					this.CalculateRecordsAffected(rowCount);
					if (!flag2)
					{
						short num2;
						this.FieldCountNoThrow(out num2);
						flag = num2 != 0;
						this._isValidResult = flag;
					}
				}
			}
			while ((!flag2 && flag3 && !flag) || (ODBC32.RetCode.NO_DATA != retCode2 && allresults && num < 2000) || (flag2 && flag3));
			if (retCode2 == ODBC32.RetCode.NO_DATA)
			{
				this._dataCache = null;
				this._noMoreResults = true;
			}
			if (odbcErrorCollection != null)
			{
				odbcErrorCollection.SetSource(this.Connection.Driver);
				OdbcException ex = OdbcException.CreateException(odbcErrorCollection, retCode);
				this.Connection.ConnectionIsAlive(ex);
				throw ex;
			}
			return flag3;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0008D238 File Offset: 0x0008B438
		private void BuildMetaDataInfo()
		{
			int fieldCount = this.FieldCount;
			OdbcDataReader.MetaData[] array = new OdbcDataReader.MetaData[fieldCount];
			bool flag = this.IsCommandBehavior(CommandBehavior.KeyInfo);
			List<string> list;
			if (flag)
			{
				list = new List<string>();
			}
			else
			{
				list = null;
			}
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = new OdbcDataReader.MetaData();
				array[i].ordinal = i;
				TypeMap typeMap = TypeMap.FromSqlType((ODBC32.SQL_TYPE)this.GetColAttribute(i, ODBC32.SQL_DESC.CONCISE_TYPE, ODBC32.SQL_COLUMN.TYPE, ODBC32.HANDLER.THROW));
				if (typeMap._signType)
				{
					bool flag2 = this.GetColAttribute(i, ODBC32.SQL_DESC.UNSIGNED, ODBC32.SQL_COLUMN.UNSIGNED, ODBC32.HANDLER.THROW).ToInt64() != 0L;
					typeMap = TypeMap.UpgradeSignedType(typeMap, flag2);
				}
				array[i].typemap = typeMap;
				array[i].size = this.GetColAttribute(i, ODBC32.SQL_DESC.OCTET_LENGTH, ODBC32.SQL_COLUMN.LENGTH, ODBC32.HANDLER.IGNORE);
				ODBC32.SQL_TYPE sql_TYPE = array[i].typemap._sql_type;
				if (sql_TYPE - ODBC32.SQL_TYPE.WLONGVARCHAR <= 2)
				{
					OdbcDataReader.MetaData metaData = array[i];
					metaData.size /= 2;
				}
				array[i].precision = (byte)this.GetColAttribute(i, (ODBC32.SQL_DESC)4, ODBC32.SQL_COLUMN.PRECISION, ODBC32.HANDLER.IGNORE);
				array[i].scale = (byte)this.GetColAttribute(i, (ODBC32.SQL_DESC)5, ODBC32.SQL_COLUMN.SCALE, ODBC32.HANDLER.IGNORE);
				array[i].isAutoIncrement = this.GetColAttribute(i, ODBC32.SQL_DESC.AUTO_UNIQUE_VALUE, ODBC32.SQL_COLUMN.AUTO_INCREMENT, ODBC32.HANDLER.IGNORE) == 1;
				array[i].isReadOnly = this.GetColAttribute(i, ODBC32.SQL_DESC.UPDATABLE, ODBC32.SQL_COLUMN.UPDATABLE, ODBC32.HANDLER.IGNORE) == 0;
				ODBC32.SQL_NULLABILITY sql_NULLABILITY = (ODBC32.SQL_NULLABILITY)this.GetColAttribute(i, ODBC32.SQL_DESC.NULLABLE, ODBC32.SQL_COLUMN.NULLABLE, ODBC32.HANDLER.IGNORE);
				array[i].isNullable = sql_NULLABILITY == ODBC32.SQL_NULLABILITY.NULLABLE;
				sql_TYPE = array[i].typemap._sql_type;
				if (sql_TYPE == ODBC32.SQL_TYPE.WLONGVARCHAR || sql_TYPE == ODBC32.SQL_TYPE.LONGVARBINARY || sql_TYPE == ODBC32.SQL_TYPE.LONGVARCHAR)
				{
					array[i].isLong = true;
				}
				else
				{
					array[i].isLong = false;
				}
				if (this.IsCommandBehavior(CommandBehavior.KeyInfo))
				{
					if (!this.Connection.ProviderInfo.NoSqlCASSColumnKey)
					{
						bool flag3 = this.GetColAttribute(i, (ODBC32.SQL_DESC)1212, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
						if (flag3)
						{
							array[i].isKeyColumn = flag3;
							array[i].isUnique = true;
							flag = false;
						}
					}
					array[i].baseSchemaName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.SCHEMA_NAME, ODBC32.SQL_COLUMN.OWNER_NAME, ODBC32.HANDLER.IGNORE);
					array[i].baseCatalogName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.CATALOG_NAME, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE);
					array[i].baseTableName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.BASE_TABLE_NAME, ODBC32.SQL_COLUMN.TABLE_NAME, ODBC32.HANDLER.IGNORE);
					array[i].baseColumnName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.BASE_COLUMN_NAME, ODBC32.SQL_COLUMN.NAME, ODBC32.HANDLER.IGNORE);
					if (this.Connection.IsV3Driver)
					{
						if (array[i].baseTableName == null || array[i].baseTableName.Length == 0)
						{
							array[i].baseTableName = this.GetDescFieldStr(i, ODBC32.SQL_DESC.BASE_TABLE_NAME, ODBC32.HANDLER.IGNORE);
						}
						if (array[i].baseColumnName == null || array[i].baseColumnName.Length == 0)
						{
							array[i].baseColumnName = this.GetDescFieldStr(i, ODBC32.SQL_DESC.BASE_COLUMN_NAME, ODBC32.HANDLER.IGNORE);
						}
					}
					if (array[i].baseTableName != null && !list.Contains(array[i].baseTableName))
					{
						list.Add(array[i].baseTableName);
					}
				}
				if ((array[i].isKeyColumn || array[i].isAutoIncrement) && sql_NULLABILITY == ODBC32.SQL_NULLABILITY.UNKNOWN)
				{
					array[i].isNullable = false;
				}
			}
			if (!this.Connection.ProviderInfo.NoSqlCASSColumnKey)
			{
				for (int j = fieldCount; j < fieldCount + this._hiddenColumns; j++)
				{
					bool flag3 = this.GetColAttribute(j, (ODBC32.SQL_DESC)1212, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
					if (flag3 && this.GetColAttribute(j, (ODBC32.SQL_DESC)1211, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1)
					{
						for (int k = 0; k < fieldCount; k++)
						{
							array[k].isKeyColumn = false;
							array[k].isUnique = false;
						}
					}
				}
			}
			this._metadata = array;
			if (this.IsCommandBehavior(CommandBehavior.KeyInfo))
			{
				if (list != null && list.Count > 0)
				{
					List<string>.Enumerator enumerator = list.GetEnumerator();
					OdbcDataReader.QualifiedTableName qualifiedTableName = new OdbcDataReader.QualifiedTableName(this.Connection.QuoteChar("GetSchemaTable"));
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						qualifiedTableName.Table = text;
						if (this.RetrieveKeyInfo(flag, qualifiedTableName, false) <= 0)
						{
							this.RetrieveKeyInfo(flag, qualifiedTableName, true);
						}
					}
					return;
				}
				OdbcDataReader.QualifiedTableName qualifiedTableName2 = new OdbcDataReader.QualifiedTableName(this.Connection.QuoteChar("GetSchemaTable"), this.GetTableNameFromCommandText());
				if (!string.IsNullOrEmpty(qualifiedTableName2.Table))
				{
					this.SetBaseTableNames(qualifiedTableName2);
					if (this.RetrieveKeyInfo(flag, qualifiedTableName2, false) <= 0)
					{
						this.RetrieveKeyInfo(flag, qualifiedTableName2, true);
					}
				}
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0008D69C File Offset: 0x0008B89C
		private DataTable NewSchemaTable()
		{
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = this.FieldCount;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(new DataColumn("ColumnName", typeof(string)));
			columns.Add(new DataColumn("ColumnOrdinal", typeof(int)));
			columns.Add(new DataColumn("ColumnSize", typeof(int)));
			columns.Add(new DataColumn("NumericPrecision", typeof(short)));
			columns.Add(new DataColumn("NumericScale", typeof(short)));
			columns.Add(new DataColumn("DataType", typeof(object)));
			columns.Add(new DataColumn("ProviderType", typeof(int)));
			columns.Add(new DataColumn("IsLong", typeof(bool)));
			columns.Add(new DataColumn("AllowDBNull", typeof(bool)));
			columns.Add(new DataColumn("IsReadOnly", typeof(bool)));
			columns.Add(new DataColumn("IsRowVersion", typeof(bool)));
			columns.Add(new DataColumn("IsUnique", typeof(bool)));
			columns.Add(new DataColumn("IsKey", typeof(bool)));
			columns.Add(new DataColumn("IsAutoIncrement", typeof(bool)));
			columns.Add(new DataColumn("BaseSchemaName", typeof(string)));
			columns.Add(new DataColumn("BaseCatalogName", typeof(string)));
			columns.Add(new DataColumn("BaseTableName", typeof(string)));
			columns.Add(new DataColumn("BaseColumnName", typeof(string)));
			foreach (object obj in columns)
			{
				((DataColumn)obj).ReadOnly = true;
			}
			return dataTable;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Odbc.OdbcDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CE5 RID: 7397 RVA: 0x0008D8EC File Offset: 0x0008BAEC
		public override DataTable GetSchemaTable()
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("GetSchemaTable");
			}
			if (this._noMoreResults)
			{
				return null;
			}
			if (this._schemaTable != null)
			{
				return this._schemaTable;
			}
			DataTable dataTable = this.NewSchemaTable();
			if (this.FieldCount == 0)
			{
				return dataTable;
			}
			if (this._metadata == null)
			{
				this.BuildMetaDataInfo();
			}
			DataColumn dataColumn = dataTable.Columns["ColumnName"];
			DataColumn dataColumn2 = dataTable.Columns["ColumnOrdinal"];
			DataColumn dataColumn3 = dataTable.Columns["ColumnSize"];
			DataColumn dataColumn4 = dataTable.Columns["NumericPrecision"];
			DataColumn dataColumn5 = dataTable.Columns["NumericScale"];
			DataColumn dataColumn6 = dataTable.Columns["DataType"];
			DataColumn dataColumn7 = dataTable.Columns["ProviderType"];
			DataColumn dataColumn8 = dataTable.Columns["IsLong"];
			DataColumn dataColumn9 = dataTable.Columns["AllowDBNull"];
			DataColumn dataColumn10 = dataTable.Columns["IsReadOnly"];
			DataColumn dataColumn11 = dataTable.Columns["IsRowVersion"];
			DataColumn dataColumn12 = dataTable.Columns["IsUnique"];
			DataColumn dataColumn13 = dataTable.Columns["IsKey"];
			DataColumn dataColumn14 = dataTable.Columns["IsAutoIncrement"];
			DataColumn dataColumn15 = dataTable.Columns["BaseSchemaName"];
			DataColumn dataColumn16 = dataTable.Columns["BaseCatalogName"];
			DataColumn dataColumn17 = dataTable.Columns["BaseTableName"];
			DataColumn dataColumn18 = dataTable.Columns["BaseColumnName"];
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[dataColumn] = this.GetName(i);
				dataRow[dataColumn2] = i;
				dataRow[dataColumn3] = (int)Math.Min(Math.Max(-2147483648L, this._metadata[i].size.ToInt64()), 2147483647L);
				dataRow[dataColumn4] = (short)this._metadata[i].precision;
				dataRow[dataColumn5] = (short)this._metadata[i].scale;
				dataRow[dataColumn6] = this._metadata[i].typemap._type;
				dataRow[dataColumn7] = this._metadata[i].typemap._odbcType;
				dataRow[dataColumn8] = this._metadata[i].isLong;
				dataRow[dataColumn9] = this._metadata[i].isNullable;
				dataRow[dataColumn10] = this._metadata[i].isReadOnly;
				dataRow[dataColumn11] = this._metadata[i].isRowVersion;
				dataRow[dataColumn12] = this._metadata[i].isUnique;
				dataRow[dataColumn13] = this._metadata[i].isKeyColumn;
				dataRow[dataColumn14] = this._metadata[i].isAutoIncrement;
				dataRow[dataColumn15] = this._metadata[i].baseSchemaName;
				dataRow[dataColumn16] = this._metadata[i].baseCatalogName;
				dataRow[dataColumn17] = this._metadata[i].baseTableName;
				dataRow[dataColumn18] = this._metadata[i].baseColumnName;
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			this._schemaTable = dataTable;
			return dataTable;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0008DCB4 File Offset: 0x0008BEB4
		internal int RetrieveKeyInfo(bool needkeyinfo, OdbcDataReader.QualifiedTableName qualifiedTableName, bool quoted)
		{
			int num = 0;
			IntPtr intPtr = IntPtr.Zero;
			if (this.IsClosed || this._cmdWrapper == null)
			{
				return 0;
			}
			this._cmdWrapper.CreateKeyInfoStatementHandle();
			CNativeBuffer buffer = this.Buffer;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag);
				ODBC32.RetCode retCode;
				if (needkeyinfo)
				{
					if (!this.Connection.ProviderInfo.NoSqlPrimaryKeys)
					{
						retCode = this.KeyInfoStatementHandle.PrimaryKeys(qualifiedTableName.Catalog, qualifiedTableName.Schema, qualifiedTableName.GetTable(quoted));
						if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
						{
							bool flag2 = false;
							buffer.WriteInt16(0, 0);
							retCode = this.KeyInfoStatementHandle.BindColumn2(4, ODBC32.SQL_C.WCHAR, buffer.PtrOffset(0, 256), (IntPtr)256, buffer.PtrOffset(256, IntPtr.Size).Handle);
							while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
							{
								intPtr = buffer.ReadIntPtr(256);
								string text = buffer.PtrToStringUni(0, (int)intPtr / 2);
								int num2 = this.GetOrdinalFromBaseColName(text);
								if (num2 == -1)
								{
									flag2 = true;
									break;
								}
								num++;
								this._metadata[num2].isKeyColumn = true;
								this._metadata[num2].isUnique = true;
								this._metadata[num2].isNullable = false;
								this._metadata[num2].baseTableName = qualifiedTableName.Table;
								if (this._metadata[num2].baseColumnName == null)
								{
									this._metadata[num2].baseColumnName = text;
								}
							}
							if (flag2)
							{
								OdbcDataReader.MetaData[] metadata = this._metadata;
								for (int i = 0; i < metadata.Length; i++)
								{
									metadata[i].isKeyColumn = false;
								}
							}
							retCode = this.KeyInfoStatementHandle.BindColumn3(4, ODBC32.SQL_C.WCHAR, buffer.DangerousGetHandle());
						}
						else if ("IM001" == this.Command.GetDiagSqlState())
						{
							this.Connection.ProviderInfo.NoSqlPrimaryKeys = true;
						}
					}
					if (num == 0)
					{
						this.KeyInfoStatementHandle.MoreResults();
						num += this.RetrieveKeyInfoFromStatistics(qualifiedTableName, quoted);
					}
					this.KeyInfoStatementHandle.MoreResults();
				}
				retCode = this.KeyInfoStatementHandle.SpecialColumns(qualifiedTableName.GetTable(quoted));
				if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					intPtr = IntPtr.Zero;
					buffer.WriteInt16(0, 0);
					retCode = this.KeyInfoStatementHandle.BindColumn2(2, ODBC32.SQL_C.WCHAR, buffer.PtrOffset(0, 256), (IntPtr)256, buffer.PtrOffset(256, IntPtr.Size).Handle);
					while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
					{
						intPtr = buffer.ReadIntPtr(256);
						string text = buffer.PtrToStringUni(0, (int)intPtr / 2);
						int num2 = this.GetOrdinalFromBaseColName(text);
						if (num2 != -1)
						{
							this._metadata[num2].isRowVersion = true;
							if (this._metadata[num2].baseColumnName == null)
							{
								this._metadata[num2].baseColumnName = text;
							}
						}
					}
					retCode = this.KeyInfoStatementHandle.BindColumn3(2, ODBC32.SQL_C.WCHAR, buffer.DangerousGetHandle());
					retCode = this.KeyInfoStatementHandle.MoreResults();
				}
			}
			finally
			{
				if (flag)
				{
					buffer.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0008DFEC File Offset: 0x0008C1EC
		private int RetrieveKeyInfoFromStatistics(OdbcDataReader.QualifiedTableName qualifiedTableName, bool quoted)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			int[] array = new int[16];
			int[] array2 = new int[16];
			int num = 0;
			int num2 = 0;
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num3 = 0;
			string text4 = string.Copy(qualifiedTableName.GetTable(quoted));
			ODBC32.RetCode retCode = this.KeyInfoStatementHandle.Statistics(text4);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				return 0;
			}
			CNativeBuffer buffer = this.Buffer;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag2);
				HandleRef handleRef = buffer.PtrOffset(0, 256);
				HandleRef handleRef2 = buffer.PtrOffset(256, 256);
				HandleRef handleRef3 = buffer.PtrOffset(512, 4);
				IntPtr handle = buffer.PtrOffset(520, IntPtr.Size).Handle;
				IntPtr handle2 = buffer.PtrOffset(528, IntPtr.Size).Handle;
				IntPtr handle3 = buffer.PtrOffset(536, IntPtr.Size).Handle;
				buffer.WriteInt16(256, 0);
				retCode = this.KeyInfoStatementHandle.BindColumn2(6, ODBC32.SQL_C.WCHAR, handleRef2, (IntPtr)256, handle2);
				retCode = this.KeyInfoStatementHandle.BindColumn2(8, ODBC32.SQL_C.SSHORT, handleRef3, (IntPtr)4, handle3);
				buffer.WriteInt16(512, 0);
				retCode = this.KeyInfoStatementHandle.BindColumn2(9, ODBC32.SQL_C.WCHAR, handleRef, (IntPtr)256, handle);
				while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
				{
					intPtr2 = buffer.ReadIntPtr(520);
					intPtr = buffer.ReadIntPtr(528);
					if (buffer.ReadInt16(256) != 0)
					{
						text = buffer.PtrToStringUni(0, (int)intPtr2 / 2);
						text2 = buffer.PtrToStringUni(256, (int)intPtr / 2);
						int num4 = (int)buffer.ReadInt16(512);
						if (this.SameIndexColumn(text3, text2, num4, num2))
						{
							if (!flag)
							{
								num4 = this.GetOrdinalFromBaseColName(text, qualifiedTableName.Table);
								if (num4 == -1)
								{
									flag = true;
								}
								else if (num2 < 16)
								{
									array[num2++] = num4;
								}
								else
								{
									flag = true;
								}
							}
						}
						else
						{
							if (!flag && num2 != 0 && (num == 0 || num > num2))
							{
								num = num2;
								for (int i = 0; i < num2; i++)
								{
									array2[i] = array[i];
								}
							}
							num2 = 0;
							text3 = text2;
							flag = false;
							num4 = this.GetOrdinalFromBaseColName(text, qualifiedTableName.Table);
							if (num4 == -1)
							{
								flag = true;
							}
							else
							{
								array[num2++] = num4;
							}
						}
					}
				}
				if (!flag && num2 != 0 && (num == 0 || num > num2))
				{
					num = num2;
					for (int j = 0; j < num2; j++)
					{
						array2[j] = array[j];
					}
				}
				if (num != 0)
				{
					for (int k = 0; k < num; k++)
					{
						int num5 = array2[k];
						num3++;
						this._metadata[num5].isKeyColumn = true;
						this._metadata[num5].isNullable = false;
						this._metadata[num5].isUnique = true;
						if (this._metadata[num5].baseTableName == null)
						{
							this._metadata[num5].baseTableName = qualifiedTableName.Table;
						}
						if (this._metadata[num5].baseColumnName == null)
						{
							this._metadata[num5].baseColumnName = text;
						}
					}
				}
				this._cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.UNBIND);
			}
			finally
			{
				if (flag2)
				{
					buffer.DangerousRelease();
				}
			}
			return num3;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0008E388 File Offset: 0x0008C588
		internal bool SameIndexColumn(string currentindexname, string indexname, int ordinal, int ncols)
		{
			return !string.IsNullOrEmpty(currentindexname) && (currentindexname == indexname && ordinal == ncols + 1);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0008E3A7 File Offset: 0x0008C5A7
		internal int GetOrdinalFromBaseColName(string columnname)
		{
			return this.GetOrdinalFromBaseColName(columnname, null);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0008E3B4 File Offset: 0x0008C5B4
		internal int GetOrdinalFromBaseColName(string columnname, string tablename)
		{
			if (string.IsNullOrEmpty(columnname))
			{
				return -1;
			}
			if (this._metadata != null)
			{
				int fieldCount = this.FieldCount;
				for (int i = 0; i < fieldCount; i++)
				{
					if (this._metadata[i].baseColumnName != null && columnname == this._metadata[i].baseColumnName)
					{
						if (string.IsNullOrEmpty(tablename))
						{
							return i;
						}
						if (tablename == this._metadata[i].baseTableName)
						{
							return i;
						}
					}
				}
			}
			return this.IndexOf(columnname);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0008E434 File Offset: 0x0008C634
		internal string GetTableNameFromCommandText()
		{
			if (this._command == null)
			{
				return null;
			}
			string text = this._cmdText;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			CStringTokenizer cstringTokenizer = new CStringTokenizer(text, this.Connection.QuoteChar("GetSchemaTable")[0], this.Connection.EscapeChar("GetSchemaTable"));
			int num;
			if (cstringTokenizer.StartsWith("select"))
			{
				num = cstringTokenizer.FindTokenIndex("from");
			}
			else if (cstringTokenizer.StartsWith("insert") || cstringTokenizer.StartsWith("update") || cstringTokenizer.StartsWith("delete"))
			{
				num = cstringTokenizer.CurrentPosition;
			}
			else
			{
				num = -1;
			}
			if (num == -1)
			{
				return null;
			}
			string text2 = cstringTokenizer.NextToken();
			text = cstringTokenizer.NextToken();
			if (text.Length > 0 && text[0] == ',')
			{
				return null;
			}
			if (text.Length == 2 && (text[0] == 'a' || text[0] == 'A') && (text[1] == 's' || text[1] == 'S'))
			{
				text = cstringTokenizer.NextToken();
				text = cstringTokenizer.NextToken();
				if (text.Length > 0 && text[0] == ',')
				{
					return null;
				}
			}
			return text2;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0008E55C File Offset: 0x0008C75C
		internal void SetBaseTableNames(OdbcDataReader.QualifiedTableName qualifiedTableName)
		{
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				if (this._metadata[i].baseTableName == null)
				{
					this._metadata[i].baseTableName = qualifiedTableName.Table;
					this._metadata[i].baseSchemaName = qualifiedTableName.Schema;
					this._metadata[i].baseCatalogName = qualifiedTableName.Catalog;
				}
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal OdbcDataReader()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001574 RID: 5492
		private OdbcCommand _command;

		// Token: 0x04001575 RID: 5493
		private int _recordAffected;

		// Token: 0x04001576 RID: 5494
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001577 RID: 5495
		private DbCache _dataCache;

		// Token: 0x04001578 RID: 5496
		private OdbcDataReader.HasRowsStatus _hasRows;

		// Token: 0x04001579 RID: 5497
		private bool _isClosed;

		// Token: 0x0400157A RID: 5498
		private bool _isRead;

		// Token: 0x0400157B RID: 5499
		private bool _isValidResult;

		// Token: 0x0400157C RID: 5500
		private bool _noMoreResults;

		// Token: 0x0400157D RID: 5501
		private bool _noMoreRows;

		// Token: 0x0400157E RID: 5502
		private bool _skipReadOnce;

		// Token: 0x0400157F RID: 5503
		private int _hiddenColumns;

		// Token: 0x04001580 RID: 5504
		private CommandBehavior _commandBehavior;

		// Token: 0x04001581 RID: 5505
		private int _row;

		// Token: 0x04001582 RID: 5506
		private int _column;

		// Token: 0x04001583 RID: 5507
		private long _sequentialBytesRead;

		// Token: 0x04001584 RID: 5508
		private static int s_objectTypeCount;

		// Token: 0x04001585 RID: 5509
		internal readonly int ObjectID;

		// Token: 0x04001586 RID: 5510
		private OdbcDataReader.MetaData[] _metadata;

		// Token: 0x04001587 RID: 5511
		private DataTable _schemaTable;

		// Token: 0x04001588 RID: 5512
		private string _cmdText;

		// Token: 0x04001589 RID: 5513
		private CMDWrapper _cmdWrapper;

		// Token: 0x02000291 RID: 657
		private enum HasRowsStatus
		{
			// Token: 0x0400158B RID: 5515
			DontKnow,
			// Token: 0x0400158C RID: 5516
			HasRows,
			// Token: 0x0400158D RID: 5517
			HasNoRows
		}

		// Token: 0x02000292 RID: 658
		internal sealed class QualifiedTableName
		{
			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0008E5C4 File Offset: 0x0008C7C4
			internal string Catalog
			{
				get
				{
					return this._catalogName;
				}
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0008E5CC File Offset: 0x0008C7CC
			internal string Schema
			{
				get
				{
					return this._schemaName;
				}
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0008E5D4 File Offset: 0x0008C7D4
			// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0008E5DC File Offset: 0x0008C7DC
			internal string Table
			{
				get
				{
					return this._tableName;
				}
				set
				{
					this._quotedTableName = value;
					this._tableName = this.UnQuote(value);
				}
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0008E5F2 File Offset: 0x0008C7F2
			internal string QuotedTable
			{
				get
				{
					return this._quotedTableName;
				}
			}

			// Token: 0x06001CF3 RID: 7411 RVA: 0x0008E5FA File Offset: 0x0008C7FA
			internal string GetTable(bool flag)
			{
				if (!flag)
				{
					return this.Table;
				}
				return this.QuotedTable;
			}

			// Token: 0x06001CF4 RID: 7412 RVA: 0x0008E60C File Offset: 0x0008C80C
			internal QualifiedTableName(string quoteChar)
			{
				this._quoteChar = quoteChar;
			}

			// Token: 0x06001CF5 RID: 7413 RVA: 0x0008E61C File Offset: 0x0008C81C
			internal QualifiedTableName(string quoteChar, string qualifiedname)
			{
				this._quoteChar = quoteChar;
				string[] array = OdbcDataReader.QualifiedTableName.ParseProcedureName(qualifiedname, quoteChar, quoteChar);
				this._catalogName = this.UnQuote(array[1]);
				this._schemaName = this.UnQuote(array[2]);
				this._quotedTableName = array[3];
				this._tableName = this.UnQuote(array[3]);
			}

			// Token: 0x06001CF6 RID: 7414 RVA: 0x0008E678 File Offset: 0x0008C878
			private string UnQuote(string str)
			{
				if (str != null && str.Length > 0)
				{
					char c = this._quoteChar[0];
					if (str[0] == c && str.Length > 1 && str[str.Length - 1] == c)
					{
						str = str.Substring(1, str.Length - 2);
					}
				}
				return str;
			}

			// Token: 0x06001CF7 RID: 7415 RVA: 0x0008E6D4 File Offset: 0x0008C8D4
			internal static string[] ParseProcedureName(string name, string quotePrefix, string quoteSuffix)
			{
				string[] array = new string[4];
				if (!string.IsNullOrEmpty(name))
				{
					bool flag = !string.IsNullOrEmpty(quotePrefix) && !string.IsNullOrEmpty(quoteSuffix);
					int i = 0;
					int num = 0;
					while (num < array.Length && i < name.Length)
					{
						int num2 = i;
						if (flag && name.IndexOf(quotePrefix, i, quotePrefix.Length, StringComparison.Ordinal) == i)
						{
							for (i += quotePrefix.Length; i < name.Length; i += quoteSuffix.Length)
							{
								i = name.IndexOf(quoteSuffix, i, StringComparison.Ordinal);
								if (i < 0)
								{
									i = name.Length;
									break;
								}
								i += quoteSuffix.Length;
								if (i >= name.Length || name.IndexOf(quoteSuffix, i, quoteSuffix.Length, StringComparison.Ordinal) != i)
								{
									break;
								}
							}
						}
						if (i < name.Length)
						{
							i = name.IndexOf(".", i, StringComparison.Ordinal);
							if (i < 0 || num == array.Length - 1)
							{
								i = name.Length;
							}
						}
						array[num] = name.Substring(num2, i - num2);
						i += ".".Length;
						num++;
					}
					int num3 = array.Length - 1;
					while (0 <= num3)
					{
						array[num3] = ((0 < num) ? array[--num] : null);
						num3--;
					}
				}
				return array;
			}

			// Token: 0x0400158E RID: 5518
			private string _catalogName;

			// Token: 0x0400158F RID: 5519
			private string _schemaName;

			// Token: 0x04001590 RID: 5520
			private string _tableName;

			// Token: 0x04001591 RID: 5521
			private string _quotedTableName;

			// Token: 0x04001592 RID: 5522
			private string _quoteChar;
		}

		// Token: 0x02000293 RID: 659
		private sealed class MetaData
		{
			// Token: 0x04001593 RID: 5523
			internal int ordinal;

			// Token: 0x04001594 RID: 5524
			internal TypeMap typemap;

			// Token: 0x04001595 RID: 5525
			internal SQLLEN size;

			// Token: 0x04001596 RID: 5526
			internal byte precision;

			// Token: 0x04001597 RID: 5527
			internal byte scale;

			// Token: 0x04001598 RID: 5528
			internal bool isAutoIncrement;

			// Token: 0x04001599 RID: 5529
			internal bool isUnique;

			// Token: 0x0400159A RID: 5530
			internal bool isReadOnly;

			// Token: 0x0400159B RID: 5531
			internal bool isNullable;

			// Token: 0x0400159C RID: 5532
			internal bool isRowVersion;

			// Token: 0x0400159D RID: 5533
			internal bool isLong;

			// Token: 0x0400159E RID: 5534
			internal bool isKeyColumn;

			// Token: 0x0400159F RID: 5535
			internal string baseSchemaName;

			// Token: 0x040015A0 RID: 5536
			internal string baseCatalogName;

			// Token: 0x040015A1 RID: 5537
			internal string baseTableName;

			// Token: 0x040015A2 RID: 5538
			internal string baseColumnName;
		}
	}
}
