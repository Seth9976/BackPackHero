using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x02000183 RID: 387
	internal sealed class SqlConnectionPoolProviderInfo : DbConnectionPoolProviderInfo
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0005C794 File Offset: 0x0005A994
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0005C79C File Offset: 0x0005A99C
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
			set
			{
				this._instanceName = value;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private string _instanceName;
	}
}
