using System;

namespace System.Data.Odbc
{
	// Token: 0x02000257 RID: 599
	internal sealed class DbSchemaInfo
	{
		// Token: 0x06001B45 RID: 6981 RVA: 0x00003D55 File Offset: 0x00001F55
		internal DbSchemaInfo()
		{
		}

		// Token: 0x04001396 RID: 5014
		internal string _name;

		// Token: 0x04001397 RID: 5015
		internal string _typename;

		// Token: 0x04001398 RID: 5016
		internal Type _type;

		// Token: 0x04001399 RID: 5017
		internal ODBC32.SQL_TYPE? _dbtype;
	}
}
