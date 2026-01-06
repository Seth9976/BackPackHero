using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F6 RID: 758
	internal sealed class DbConnectionClosedBusy : DbConnectionBusy
	{
		// Token: 0x06002265 RID: 8805 RVA: 0x0009EBE9 File Offset: 0x0009CDE9
		private DbConnectionClosedBusy()
			: base(ConnectionState.Closed)
		{
		}

		// Token: 0x0400171C RID: 5916
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedBusy();
	}
}
