using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x0200028C RID: 652
	internal sealed class OdbcConnectionString : DbConnectionOptions
	{
		// Token: 0x06001C62 RID: 7266 RVA: 0x0008B0B0 File Offset: 0x000892B0
		internal OdbcConnectionString(string connectionString, bool validate)
			: base(connectionString, null, true)
		{
			if (!validate)
			{
				string text = null;
				int num = 0;
				this._expandedConnectionString = base.ExpandDataDirectories(ref text, ref num);
			}
			if ((validate || this._expandedConnectionString == null) && connectionString != null && 1024 < connectionString.Length)
			{
				throw ODBC.ConnectionStringTooLong();
			}
		}

		// Token: 0x04001565 RID: 5477
		private readonly string _expandedConnectionString;
	}
}
