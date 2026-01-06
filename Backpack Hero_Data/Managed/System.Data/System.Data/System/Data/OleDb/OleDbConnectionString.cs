using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x02000124 RID: 292
	[MonoTODO("OleDb is not implemented.")]
	internal sealed class OleDbConnectionString : DbConnectionOptions
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x0004F22E File Offset: 0x0004D42E
		internal OleDbConnectionString(string connectionString, bool validate)
			: base(connectionString, null)
		{
		}
	}
}
