using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F9 RID: 761
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x0600226E RID: 8814 RVA: 0x0009EC94 File Offset: 0x0009CE94
		private DbConnectionClosedNeverOpened()
			: base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x0400171F RID: 5919
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
