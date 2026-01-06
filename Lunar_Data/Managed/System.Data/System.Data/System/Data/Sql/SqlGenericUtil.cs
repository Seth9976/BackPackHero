using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x02000127 RID: 295
	internal sealed class SqlGenericUtil
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x00003D55 File Offset: 0x00001F55
		private SqlGenericUtil()
		{
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0004F566 File Offset: 0x0004D766
		internal static Exception NullCommandText()
		{
			return ADP.Argument(Res.GetString("Command parameter must have a non null and non empty command text."));
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0004F577 File Offset: 0x0004D777
		internal static Exception MismatchedMetaDataDirectionArrayLengths()
		{
			return ADP.Argument(Res.GetString("MetaData parameter array must have length equivalent to ParameterDirection array argument."));
		}
	}
}
