using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000182 RID: 386
	internal class SqlConnectionPoolKey : DbConnectionPoolKey
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x0005C670 File Offset: 0x0005A870
		internal SqlConnectionPoolKey(string connectionString, SqlCredential credential, string accessToken)
			: base(connectionString)
		{
			this._credential = credential;
			this._accessToken = accessToken;
			this.CalculateHashCode();
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0005C68D File Offset: 0x0005A88D
		private SqlConnectionPoolKey(SqlConnectionPoolKey key)
			: base(key)
		{
			this._credential = key.Credential;
			this._accessToken = key.AccessToken;
			this.CalculateHashCode();
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0005C6B4 File Offset: 0x0005A8B4
		public override object Clone()
		{
			return new SqlConnectionPoolKey(this);
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0005C6C4 File Offset: 0x0005A8C4
		internal override string ConnectionString
		{
			get
			{
				return base.ConnectionString;
			}
			set
			{
				base.ConnectionString = value;
				this.CalculateHashCode();
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0005C6D3 File Offset: 0x0005A8D3
		internal SqlCredential Credential
		{
			get
			{
				return this._credential;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0005C6DB File Offset: 0x0005A8DB
		internal string AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0005C6E4 File Offset: 0x0005A8E4
		public override bool Equals(object obj)
		{
			SqlConnectionPoolKey sqlConnectionPoolKey = obj as SqlConnectionPoolKey;
			return sqlConnectionPoolKey != null && this._credential == sqlConnectionPoolKey._credential && this.ConnectionString == sqlConnectionPoolKey.ConnectionString && this._accessToken == sqlConnectionPoolKey._accessToken;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0005C72C File Offset: 0x0005A92C
		public override int GetHashCode()
		{
			return this._hashValue;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0005C734 File Offset: 0x0005A934
		private void CalculateHashCode()
		{
			this._hashValue = base.GetHashCode();
			if (this._credential != null)
			{
				this._hashValue = this._hashValue * 17 + this._credential.GetHashCode();
				return;
			}
			if (this._accessToken != null)
			{
				this._hashValue = this._hashValue * 17 + this._accessToken.GetHashCode();
			}
		}

		// Token: 0x04000C39 RID: 3129
		private int _hashValue;

		// Token: 0x04000C3A RID: 3130
		private SqlCredential _credential;

		// Token: 0x04000C3B RID: 3131
		private readonly string _accessToken;
	}
}
