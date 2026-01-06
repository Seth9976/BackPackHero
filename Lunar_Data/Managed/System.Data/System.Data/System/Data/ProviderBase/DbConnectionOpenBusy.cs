using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F7 RID: 759
	internal sealed class DbConnectionOpenBusy : DbConnectionBusy
	{
		// Token: 0x06002267 RID: 8807 RVA: 0x0009EBFE File Offset: 0x0009CDFE
		private DbConnectionOpenBusy()
			: base(ConnectionState.Open)
		{
		}

		// Token: 0x0400171D RID: 5917
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionOpenBusy();
	}
}
