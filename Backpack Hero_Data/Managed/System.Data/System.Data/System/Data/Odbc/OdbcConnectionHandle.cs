using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x02000289 RID: 649
	internal sealed class OdbcConnectionHandle : OdbcHandle
	{
		// Token: 0x06001C33 RID: 7219 RVA: 0x0008AC28 File Offset: 0x00088E28
		internal OdbcConnectionHandle(OdbcConnection connection, OdbcConnectionString constr, OdbcEnvironmentHandle environmentHandle)
			: base(ODBC32.SQL_HANDLE.DBC, environmentHandle)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			if (constr == null)
			{
				throw ADP.ArgumentNull("constr");
			}
			int connectionTimeout = connection.ConnectionTimeout;
			ODBC32.RetCode retCode = this.SetConnectionAttribute2(ODBC32.SQL_ATTR.LOGIN_TIMEOUT, (IntPtr)connectionTimeout, -5);
			string text = constr.UsersConnectionString(false);
			retCode = this.Connect(text);
			connection.HandleError(this, retCode);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0008AC8C File Offset: 0x00088E8C
		private ODBC32.RetCode AutoCommitOff()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				retCode = global::Interop.Odbc.SQLSetConnectAttrW(this, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_OFF, -5);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Transacted;
				}
			}
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0008ACDC File Offset: 0x00088EDC
		internal ODBC32.RetCode BeginTransaction(ref IsolationLevel isolevel)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			if (IsolationLevel.Unspecified != isolevel)
			{
				IsolationLevel isolationLevel = isolevel;
				ODBC32.SQL_TRANSACTION sql_TRANSACTION;
				ODBC32.SQL_ATTR sql_ATTR;
				if (isolationLevel <= IsolationLevel.ReadCommitted)
				{
					if (isolationLevel == IsolationLevel.Chaos)
					{
						throw ODBC.NotSupportedIsolationLevel(isolevel);
					}
					if (isolationLevel == IsolationLevel.ReadUncommitted)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
					if (isolationLevel == IsolationLevel.ReadCommitted)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.READ_COMMITTED;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
				}
				else
				{
					if (isolationLevel == IsolationLevel.RepeatableRead)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.REPEATABLE_READ;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
					if (isolationLevel == IsolationLevel.Serializable)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.SERIALIZABLE;
						sql_ATTR = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_007D;
					}
					if (isolationLevel == IsolationLevel.Snapshot)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.SNAPSHOT;
						sql_ATTR = ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION;
						goto IL_007D;
					}
				}
				throw ADP.InvalidIsolationLevel(isolevel);
				IL_007D:
				retCode = this.SetConnectionAttribute2(sql_ATTR, (IntPtr)((int)sql_TRANSACTION), -6);
				if (ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					isolevel = IsolationLevel.Unspecified;
				}
			}
			if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				retCode = this.AutoCommitOff();
				this._handleState = OdbcConnectionHandle.HandleState.TransactionInProgress;
			}
			return retCode;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0008AD90 File Offset: 0x00088F90
		internal ODBC32.RetCode CompleteTransaction(short transactionOperation)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
				base.DangerousAddRef(ref flag);
				retCode = this.CompleteTransaction(transactionOperation, this.handle);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return retCode;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0008ADD8 File Offset: 0x00088FD8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private ODBC32.RetCode CompleteTransaction(short transactionOperation, IntPtr handle)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
				{
					retCode = global::Interop.Odbc.SQLEndTran(base.HandleType, handle, transactionOperation);
					if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
					{
						this._handleState = OdbcConnectionHandle.HandleState.Transacted;
					}
				}
				if (OdbcConnectionHandle.HandleState.Transacted == this._handleState)
				{
					retCode = global::Interop.Odbc.SQLSetConnectAttrW(handle, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_ON, -5);
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			return retCode;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0008AE48 File Offset: 0x00089048
		private ODBC32.RetCode Connect(string connectionString)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				short num;
				retCode = global::Interop.Odbc.SQLDriverConnectW(this, ADP.PtrZero, connectionString, -3, ADP.PtrZero, 0, out num, 0);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			ODBC.TraceODBC(3, "SQLDriverConnectW", retCode);
			return retCode;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0008AEA0 File Offset: 0x000890A0
		protected override bool ReleaseHandle()
		{
			this.CompleteTransaction(1, this.handle);
			if (OdbcConnectionHandle.HandleState.Connected == this._handleState || OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
			{
				global::Interop.Odbc.SQLDisconnect(this.handle);
				this._handleState = OdbcConnectionHandle.HandleState.Allocated;
			}
			return base.ReleaseHandle();
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0008AEDB File Offset: 0x000890DB
		internal ODBC32.RetCode GetConnectionAttribute(ODBC32.SQL_ATTR attribute, byte[] buffer, out int cbActual)
		{
			return global::Interop.Odbc.SQLGetConnectAttrW(this, attribute, buffer, buffer.Length, out cbActual);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0008AEEC File Offset: 0x000890EC
		internal ODBC32.RetCode GetFunctions(ODBC32.SQL_API fFunction, out short fExists)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetFunctions(this, fFunction, out fExists);
			ODBC.TraceODBC(3, "SQLGetFunctions", retCode);
			return retCode;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0008AF0F File Offset: 0x0008910F
		internal ODBC32.RetCode GetInfo2(ODBC32.SQL_INFO info, byte[] buffer, out short cbActual)
		{
			return global::Interop.Odbc.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), out cbActual);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0008AF1E File Offset: 0x0008911E
		internal ODBC32.RetCode GetInfo1(ODBC32.SQL_INFO info, byte[] buffer)
		{
			return global::Interop.Odbc.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), ADP.PtrZero);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0008AF34 File Offset: 0x00089134
		internal ODBC32.RetCode SetConnectionAttribute2(ODBC32.SQL_ATTR attribute, IntPtr value, int length)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLSetConnectAttrW(this, attribute, value, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0008AF58 File Offset: 0x00089158
		internal ODBC32.RetCode SetConnectionAttribute3(ODBC32.SQL_ATTR attribute, string buffer, int length)
		{
			return global::Interop.Odbc.SQLSetConnectAttrW(this, attribute, buffer, length);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0008AF64 File Offset: 0x00089164
		internal ODBC32.RetCode SetConnectionAttribute4(ODBC32.SQL_ATTR attribute, IDtcTransaction transaction, int length)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLSetConnectAttrW(this, attribute, transaction, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x0400154E RID: 5454
		private OdbcConnectionHandle.HandleState _handleState;

		// Token: 0x0200028A RID: 650
		private enum HandleState
		{
			// Token: 0x04001550 RID: 5456
			Allocated,
			// Token: 0x04001551 RID: 5457
			Connected,
			// Token: 0x04001552 RID: 5458
			Transacted,
			// Token: 0x04001553 RID: 5459
			TransactionInProgress
		}
	}
}
