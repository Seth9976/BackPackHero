using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002AA RID: 682
	internal sealed class OdbcStatementHandle : OdbcHandle
	{
		// Token: 0x06001DE9 RID: 7657 RVA: 0x0009216A File Offset: 0x0009036A
		internal OdbcStatementHandle(OdbcConnectionHandle connectionHandle)
			: base(ODBC32.SQL_HANDLE.STMT, connectionHandle)
		{
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00092174 File Offset: 0x00090374
		internal ODBC32.RetCode BindColumn2(int columnNumber, ODBC32.SQL_C targetType, HandleRef buffer, IntPtr length, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLBindCol(this, checked((ushort)columnNumber), targetType, buffer, length, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x000921A0 File Offset: 0x000903A0
		internal ODBC32.RetCode BindColumn3(int columnNumber, ODBC32.SQL_C targetType, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLBindCol(this, checked((ushort)columnNumber), targetType, ADP.PtrZero, ADP.PtrZero, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000921D0 File Offset: 0x000903D0
		internal ODBC32.RetCode BindParameter(short ordinal, short parameterDirection, ODBC32.SQL_C sqlctype, ODBC32.SQL_TYPE sqltype, IntPtr cchSize, IntPtr scale, HandleRef buffer, IntPtr bufferLength, HandleRef intbuffer)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLBindParameter(this, checked((ushort)ordinal), parameterDirection, sqlctype, (short)sqltype, cchSize, scale, buffer, bufferLength, intbuffer);
			ODBC.TraceODBC(3, "SQLBindParameter", retCode);
			return retCode;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00092204 File Offset: 0x00090404
		internal ODBC32.RetCode Cancel()
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLCancel(this);
			ODBC.TraceODBC(3, "SQLCancel", retCode);
			return retCode;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00092228 File Offset: 0x00090428
		internal ODBC32.RetCode CloseCursor()
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLCloseCursor(this);
			ODBC.TraceODBC(3, "SQLCloseCursor", retCode);
			return retCode;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0009224C File Offset: 0x0009044C
		internal ODBC32.RetCode ColumnAttribute(int columnNumber, short fieldIdentifier, CNativeBuffer characterAttribute, out short stringLength, out SQLLEN numericAttribute)
		{
			IntPtr intPtr;
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLColAttributeW(this, checked((short)columnNumber), fieldIdentifier, characterAttribute, characterAttribute.ShortLength, out stringLength, out intPtr);
			numericAttribute = new SQLLEN(intPtr);
			ODBC.TraceODBC(3, "SQLColAttributeW", retCode);
			return retCode;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00092288 File Offset: 0x00090488
		internal ODBC32.RetCode Columns(string tableCatalog, string tableSchema, string tableName, string columnName)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLColumnsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000922C8 File Offset: 0x000904C8
		internal ODBC32.RetCode Execute()
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLExecute(this);
			ODBC.TraceODBC(3, "SQLExecute", retCode);
			return retCode;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000922EC File Offset: 0x000904EC
		internal ODBC32.RetCode ExecuteDirect(string commandText)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLExecDirectW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLExecDirectW", retCode);
			return retCode;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00092310 File Offset: 0x00090510
		internal ODBC32.RetCode Fetch()
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLFetch(this);
			ODBC.TraceODBC(3, "SQLFetch", retCode);
			return retCode;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00092334 File Offset: 0x00090534
		internal ODBC32.RetCode FreeStatement(ODBC32.STMT stmt)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLFreeStmt(this, stmt);
			ODBC.TraceODBC(3, "SQLFreeStmt", retCode);
			return retCode;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00092358 File Offset: 0x00090558
		internal ODBC32.RetCode GetData(int index, ODBC32.SQL_C sqlctype, CNativeBuffer buffer, int cb, out IntPtr cbActual)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetData(this, checked((ushort)index), sqlctype, buffer, new IntPtr(cb), out cbActual);
			ODBC.TraceODBC(3, "SQLGetData", retCode);
			return retCode;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00092388 File Offset: 0x00090588
		internal ODBC32.RetCode GetStatementAttribute(ODBC32.SQL_ATTR attribute, out IntPtr value, out int stringLength)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetStmtAttrW(this, attribute, out value, ADP.PtrSize, out stringLength);
			ODBC.TraceODBC(3, "SQLGetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000923B4 File Offset: 0x000905B4
		internal ODBC32.RetCode GetTypeInfo(short fSqlType)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLGetTypeInfo(this, fSqlType);
			ODBC.TraceODBC(3, "SQLGetTypeInfo", retCode);
			return retCode;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000923D8 File Offset: 0x000905D8
		internal ODBC32.RetCode MoreResults()
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLMoreResults(this);
			ODBC.TraceODBC(3, "SQLMoreResults", retCode);
			return retCode;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000923FC File Offset: 0x000905FC
		internal ODBC32.RetCode NumberOfResultColumns(out short columnsAffected)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLNumResultCols(this, out columnsAffected);
			ODBC.TraceODBC(3, "SQLNumResultCols", retCode);
			return retCode;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00092420 File Offset: 0x00090620
		internal ODBC32.RetCode Prepare(string commandText)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLPrepareW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLPrepareW", retCode);
			return retCode;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00092444 File Offset: 0x00090644
		internal ODBC32.RetCode PrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLPrimaryKeysW(this, catalogName, ODBC.ShortStringLength(catalogName), schemaName, ODBC.ShortStringLength(schemaName), tableName, ODBC.ShortStringLength(tableName));
			ODBC.TraceODBC(3, "SQLPrimaryKeysW", retCode);
			return retCode;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0009247C File Offset: 0x0009067C
		internal ODBC32.RetCode Procedures(string procedureCatalog, string procedureSchema, string procedureName)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLProceduresW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName));
			ODBC.TraceODBC(3, "SQLProceduresW", retCode);
			return retCode;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000924B4 File Offset: 0x000906B4
		internal ODBC32.RetCode ProcedureColumns(string procedureCatalog, string procedureSchema, string procedureName, string columnName)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLProcedureColumnsW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLProcedureColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000924F4 File Offset: 0x000906F4
		internal ODBC32.RetCode RowCount(out SQLLEN rowCount)
		{
			IntPtr intPtr;
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLRowCount(this, out intPtr);
			rowCount = new SQLLEN(intPtr);
			ODBC.TraceODBC(3, "SQLRowCount", retCode);
			return retCode;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00092524 File Offset: 0x00090724
		internal ODBC32.RetCode SetStatementAttribute(ODBC32.SQL_ATTR attribute, IntPtr value, ODBC32.SQL_IS stringLength)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLSetStmtAttrW(this, (int)attribute, value, (int)stringLength);
			ODBC.TraceODBC(3, "SQLSetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00092548 File Offset: 0x00090748
		internal ODBC32.RetCode SpecialColumns(string quotedTable)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLSpecialColumnsW(this, ODBC32.SQL_SPECIALCOLS.ROWVER, null, 0, null, 0, quotedTable, ODBC.ShortStringLength(quotedTable), ODBC32.SQL_SCOPE.SESSION, ODBC32.SQL_NULLABILITY.NO_NULLS);
			ODBC.TraceODBC(3, "SQLSpecialColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00092578 File Offset: 0x00090778
		internal ODBC32.RetCode Statistics(string tableCatalog, string tableSchema, string tableName, short unique, short accuracy)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLStatisticsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), unique, accuracy);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000925B4 File Offset: 0x000907B4
		internal ODBC32.RetCode Statistics(string tableName)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLStatisticsW(this, null, 0, null, 0, tableName, ODBC.ShortStringLength(tableName), 0, 1);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000925E4 File Offset: 0x000907E4
		internal ODBC32.RetCode Tables(string tableCatalog, string tableSchema, string tableName, string tableType)
		{
			ODBC32.RetCode retCode = global::Interop.Odbc.SQLTablesW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), tableType, ODBC.ShortStringLength(tableType));
			ODBC.TraceODBC(3, "SQLTablesW", retCode);
			return retCode;
		}
	}
}
