using System;

namespace System.Data.ProviderBase
{
	// Token: 0x0200030F RID: 783
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000A4920 File Offset: 0x000A2B20
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x000A4928 File Offset: 0x000A2B28
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x040017B3 RID: 6067
		private DbConnectionPoolGroup _poolGroup;
	}
}
