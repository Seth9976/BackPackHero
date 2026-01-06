using System;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x0200029B RID: 667
	internal sealed class OdbcDescriptorHandle : OdbcHandle
	{
		// Token: 0x06001D27 RID: 7463 RVA: 0x0008EE40 File Offset: 0x0008D040
		internal OdbcDescriptorHandle(OdbcStatementHandle statementHandle, ODBC32.SQL_ATTR attribute)
			: base(statementHandle, attribute)
		{
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0008EE4C File Offset: 0x0008D04C
		internal ODBC32.RetCode GetDescriptionField(int i, ODBC32.SQL_DESC attribute, CNativeBuffer buffer, out int numericAttribute)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetDescFieldW(this, checked((short)i), attribute, buffer, (int)buffer.ShortLength, out numericAttribute);
			ODBC.TraceODBC(3, "SQLGetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0008EE7C File Offset: 0x0008D07C
		internal ODBC32.RetCode SetDescriptionField1(short ordinal, ODBC32.SQL_DESC type, IntPtr value)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0008EEA4 File Offset: 0x0008D0A4
		internal ODBC32.RetCode SetDescriptionField2(short ordinal, ODBC32.SQL_DESC type, HandleRef value)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}
	}
}
