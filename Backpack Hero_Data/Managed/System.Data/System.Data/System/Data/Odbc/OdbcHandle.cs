using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x0200029A RID: 666
	internal abstract class OdbcHandle : SafeHandle
	{
		// Token: 0x06001D20 RID: 7456 RVA: 0x0008EBE4 File Offset: 0x0008CDE4
		protected OdbcHandle(ODBC32.SQL_HANDLE handleType, OdbcHandle parentHandle)
			: base(IntPtr.Zero, true)
		{
			this._handleType = handleType;
			bool flag = false;
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (handleType != ODBC32.SQL_HANDLE.ENV)
				{
					if (handleType - ODBC32.SQL_HANDLE.DBC <= 1)
					{
						parentHandle.DangerousAddRef(ref flag);
						retCode = global::Interop.Odbc.SQLAllocHandle(handleType, parentHandle, out this.handle);
					}
				}
				else
				{
					retCode = global::Interop.Odbc.SQLAllocHandle(handleType, IntPtr.Zero, out this.handle);
				}
			}
			finally
			{
				if (flag && handleType - ODBC32.SQL_HANDLE.DBC <= 1)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle || retCode != ODBC32.RetCode.SUCCESS)
			{
				throw ODBC.CantAllocateEnvironmentHandle(retCode);
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0008EC9C File Offset: 0x0008CE9C
		internal OdbcHandle(OdbcStatementHandle parentHandle, ODBC32.SQL_ATTR attribute)
			: base(IntPtr.Zero, true)
		{
			this._handleType = ODBC32.SQL_HANDLE.DESC;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode statementAttribute;
			try
			{
				parentHandle.DangerousAddRef(ref flag);
				int num;
				statementAttribute = parentHandle.GetStatementAttribute(attribute, out this.handle, out num);
			}
			finally
			{
				if (flag)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle)
			{
				throw ODBC.FailedToGetDescriptorHandle(statementAttribute);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0008ED2C File Offset: 0x0008CF2C
		internal ODBC32.SQL_HANDLE HandleType
		{
			get
			{
				return this._handleType;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0007DD20 File Offset: 0x0007BF20
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0008ED34 File Offset: 0x0008CF34
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				ODBC32.SQL_HANDLE handleType = this.HandleType;
				if (handleType - ODBC32.SQL_HANDLE.ENV > 2)
				{
					if (handleType != ODBC32.SQL_HANDLE.DESC)
					{
					}
				}
				else
				{
					global::Interop.Odbc.SQLFreeHandle(handleType, handle);
				}
			}
			OdbcHandle parentHandle = this._parentHandle;
			this._parentHandle = null;
			if (parentHandle != null)
			{
				parentHandle.DangerousRelease();
			}
			return true;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0008ED98 File Offset: 0x0008CF98
		internal ODBC32.RetCode GetDiagnosticField(out string sqlState)
		{
			StringBuilder stringBuilder = new StringBuilder(6);
			short num;
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetDiagFieldW(this.HandleType, this, 1, 4, stringBuilder, checked((short)(2 * stringBuilder.Capacity)), out num);
			ODBC.TraceODBC(3, "SQLGetDiagFieldW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0008EDEC File Offset: 0x0008CFEC
		internal ODBC32.RetCode GetDiagnosticRecord(short record, out string sqlState, StringBuilder message, out int nativeError, out short cchActual)
		{
			StringBuilder stringBuilder = new StringBuilder(5);
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetDiagRecW(this.HandleType, this, record, stringBuilder, out nativeError, message, checked((short)message.Capacity), out cchActual);
			ODBC.TraceODBC(3, "SQLGetDiagRecW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x040015AC RID: 5548
		private ODBC32.SQL_HANDLE _handleType;

		// Token: 0x040015AD RID: 5549
		private OdbcHandle _parentHandle;
	}
}
