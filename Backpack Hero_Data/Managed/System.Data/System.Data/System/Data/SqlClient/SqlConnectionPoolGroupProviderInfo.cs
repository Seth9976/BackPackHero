using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x02000181 RID: 385
	internal sealed class SqlConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x0005C53F File Offset: 0x0005A73F
		internal SqlConnectionPoolGroupProviderInfo(SqlConnectionString connectionOptions)
		{
			this._failoverPartner = connectionOptions.FailoverPartner;
			if (string.IsNullOrEmpty(this._failoverPartner))
			{
				this._failoverPartner = null;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0005C567 File Offset: 0x0005A767
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0005C56F File Offset: 0x0005A76F
		internal bool UseFailoverPartner
		{
			get
			{
				return this._useFailoverPartner;
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0005C578 File Offset: 0x0005A778
		internal void AliasCheck(string server)
		{
			if (this._alias != server)
			{
				lock (this)
				{
					if (this._alias == null)
					{
						this._alias = server;
					}
					else if (this._alias != server)
					{
						base.PoolGroup.Clear();
						this._alias = server;
					}
				}
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0005C5F0 File Offset: 0x0005A7F0
		internal void FailoverCheck(SqlInternalConnection connection, bool actualUseFailoverPartner, SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			if (this.UseFailoverPartner != actualUseFailoverPartner)
			{
				base.PoolGroup.Clear();
				this._useFailoverPartner = actualUseFailoverPartner;
			}
			if (!this._useFailoverPartner && this._failoverPartner != actualFailoverPartner)
			{
				lock (this)
				{
					if (this._failoverPartner != actualFailoverPartner)
					{
						this._failoverPartner = actualFailoverPartner;
					}
				}
			}
		}

		// Token: 0x04000C36 RID: 3126
		private string _alias;

		// Token: 0x04000C37 RID: 3127
		private string _failoverPartner;

		// Token: 0x04000C38 RID: 3128
		private bool _useFailoverPartner;
	}
}
