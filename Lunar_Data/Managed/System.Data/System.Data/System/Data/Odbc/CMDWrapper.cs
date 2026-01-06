using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000285 RID: 645
	internal sealed class CMDWrapper
	{
		// Token: 0x06001BA4 RID: 7076 RVA: 0x000893C1 File Offset: 0x000875C1
		internal CMDWrapper(OdbcConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x000893D0 File Offset: 0x000875D0
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x000893D8 File Offset: 0x000875D8
		internal bool Canceling
		{
			get
			{
				return this._canceling;
			}
			set
			{
				this._canceling = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000893E1 File Offset: 0x000875E1
		internal OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x000893E9 File Offset: 0x000875E9
		internal bool HasBoundColumns
		{
			set
			{
				this._hasBoundColumns = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000893F2 File Offset: 0x000875F2
		internal OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._stmt;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000893FA File Offset: 0x000875FA
		internal OdbcStatementHandle KeyInfoStatement
		{
			get
			{
				return this._keyinfostmt;
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00089402 File Offset: 0x00087602
		internal void CreateKeyInfoStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this._keyinfostmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0008941B File Offset: 0x0008761B
		internal void CreateStatementHandle()
		{
			this.DisposeStatementHandle();
			this._stmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00089434 File Offset: 0x00087634
		internal void Dispose()
		{
			if (this._dataReaderBuf != null)
			{
				this._dataReaderBuf.Dispose();
				this._dataReaderBuf = null;
			}
			this.DisposeStatementHandle();
			CNativeBuffer nativeParameterBuffer = this._nativeParameterBuffer;
			this._nativeParameterBuffer = null;
			if (nativeParameterBuffer != null)
			{
				nativeParameterBuffer.Dispose();
			}
			this._ssKeyInfoModeOn = false;
			this._ssKeyInfoModeOff = false;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00089488 File Offset: 0x00087688
		private void DisposeDescriptorHandle()
		{
			OdbcDescriptorHandle hdesc = this._hdesc;
			if (hdesc != null)
			{
				this._hdesc = null;
				hdesc.Dispose();
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000894AC File Offset: 0x000876AC
		internal void DisposeStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt = this._stmt;
			if (stmt != null)
			{
				this._stmt = null;
				stmt.Dispose();
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000894DC File Offset: 0x000876DC
		internal void DisposeKeyInfoStatementHandle()
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				this._keyinfostmt = null;
				keyinfostmt.Dispose();
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00089500 File Offset: 0x00087700
		internal void FreeStatementHandle(ODBC32.STMT stmt)
		{
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt2 = this._stmt;
			if (stmt2 != null)
			{
				try
				{
					ODBC32.RetCode retCode = stmt2.FreeStatement(stmt);
					this.StatementErrorHandler(retCode);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this._stmt = null;
						stmt2.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00089554 File Offset: 0x00087754
		internal void FreeKeyInfoStatementHandle(ODBC32.STMT stmt)
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				try
				{
					keyinfostmt.FreeStatement(stmt);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this._keyinfostmt = null;
						keyinfostmt.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0008959C File Offset: 0x0008779C
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			OdbcDescriptorHandle odbcDescriptorHandle = this._hdesc;
			if (this._hdesc == null)
			{
				odbcDescriptorHandle = (this._hdesc = new OdbcDescriptorHandle(this._stmt, attribute));
			}
			return odbcDescriptorHandle;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000895D0 File Offset: 0x000877D0
		internal string GetDiagSqlState()
		{
			string text;
			this._stmt.GetDiagnosticField(out text);
			return text;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000895EC File Offset: 0x000877EC
		internal void StatementErrorHandler(ODBC32.RetCode retcode)
		{
			if (retcode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				this._connection.HandleErrorNoThrow(this._stmt, retcode);
				return;
			}
			throw this._connection.HandleErrorNoThrow(this._stmt, retcode);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00089618 File Offset: 0x00087818
		internal void UnbindStmtColumns()
		{
			if (this._hasBoundColumns)
			{
				this.FreeStatementHandle(ODBC32.STMT.UNBIND);
				this._hasBoundColumns = false;
			}
		}

		// Token: 0x04001539 RID: 5433
		private OdbcStatementHandle _stmt;

		// Token: 0x0400153A RID: 5434
		private OdbcStatementHandle _keyinfostmt;

		// Token: 0x0400153B RID: 5435
		internal OdbcDescriptorHandle _hdesc;

		// Token: 0x0400153C RID: 5436
		internal CNativeBuffer _nativeParameterBuffer;

		// Token: 0x0400153D RID: 5437
		internal CNativeBuffer _dataReaderBuf;

		// Token: 0x0400153E RID: 5438
		private readonly OdbcConnection _connection;

		// Token: 0x0400153F RID: 5439
		private bool _canceling;

		// Token: 0x04001540 RID: 5440
		internal bool _hasBoundColumns;

		// Token: 0x04001541 RID: 5441
		internal bool _ssKeyInfoModeOn;

		// Token: 0x04001542 RID: 5442
		internal bool _ssKeyInfoModeOff;
	}
}
