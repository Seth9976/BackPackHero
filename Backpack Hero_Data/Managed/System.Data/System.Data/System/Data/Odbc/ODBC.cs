using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x02000259 RID: 601
	internal static class ODBC
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x00087AD7 File Offset: 0x00085CD7
		internal static Exception ConnectionClosed()
		{
			return ADP.InvalidOperation(SR.GetString("The connection is closed."));
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00087AE8 File Offset: 0x00085CE8
		internal static Exception OpenConnectionNoOwner()
		{
			return ADP.InvalidOperation(SR.GetString("An internal connection does not have an owner."));
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00087AF9 File Offset: 0x00085CF9
		internal static Exception UnknownSQLType(ODBC32.SQL_TYPE sqltype)
		{
			return ADP.Argument(SR.GetString("Unknown SQL type - {0}.", new object[] { sqltype.ToString() }));
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00087B20 File Offset: 0x00085D20
		internal static Exception ConnectionStringTooLong()
		{
			return ADP.Argument(SR.GetString("Connection string exceeds maximum allowed length of {0}.", new object[] { 1024 }));
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00087B44 File Offset: 0x00085D44
		internal static ArgumentException GetSchemaRestrictionRequired()
		{
			return ADP.Argument(SR.GetString("The ODBC managed provider requires that the TABLE_NAME restriction be specified and non-null for the GetSchema indexes collection."));
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00087B55 File Offset: 0x00085D55
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("The {0} enumeration value, {1}, is not supported by the .Net Framework Odbc Data Provider.", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00087B8A File Offset: 0x00085D8A
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00087B9C File Offset: 0x00085D9C
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00087BAE File Offset: 0x00085DAE
		internal static InvalidOperationException NoMappingForSqlTransactionLevel(int value)
		{
			return ADP.DataAdapter(SR.GetString("No valid mapping for a SQL_TRANSACTION '{0}' to a System.Data.IsolationLevel enumeration value.", new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00087BD4 File Offset: 0x00085DD4
		internal static Exception NegativeArgument()
		{
			return ADP.Argument(SR.GetString("Invalid negative argument!"));
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00087BE5 File Offset: 0x00085DE5
		internal static Exception CantSetPropertyOnOpenConnection()
		{
			return ADP.InvalidOperation(SR.GetString("Can't set property on an open connection."));
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00087BF6 File Offset: 0x00085DF6
		internal static Exception CantEnableConnectionpooling(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to enable connection pooling...", new object[] { ODBC32.RetcodeToString(retcode) }));
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00087C16 File Offset: 0x00085E16
		internal static Exception CantAllocateEnvironmentHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to allocate an environment handle.", new object[] { ODBC32.RetcodeToString(retcode) }));
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00087C36 File Offset: 0x00085E36
		internal static Exception FailedToGetDescriptorHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to get descriptor handle.", new object[] { ODBC32.RetcodeToString(retcode) }));
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00087C56 File Offset: 0x00085E56
		internal static Exception NotInTransaction()
		{
			return ADP.InvalidOperation(SR.GetString("Not in a transaction"));
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00087C67 File Offset: 0x00085E67
		internal static Exception UnknownOdbcType(OdbcType odbctype)
		{
			return ADP.InvalidEnumerationValue(typeof(OdbcType), (int)odbctype);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000094D4 File Offset: 0x000076D4
		internal static void TraceODBC(int level, string method, ODBC32.RetCode retcode)
		{
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00087C79 File Offset: 0x00085E79
		internal static short ShortStringLength(string inputString)
		{
			return checked((short)ADP.StringLength(inputString));
		}

		// Token: 0x040013A0 RID: 5024
		internal const string Pwd = "pwd";
	}
}
