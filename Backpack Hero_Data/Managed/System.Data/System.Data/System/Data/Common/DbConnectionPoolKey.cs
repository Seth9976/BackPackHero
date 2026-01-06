using System;

namespace System.Data.Common
{
	// Token: 0x0200031A RID: 794
	internal class DbConnectionPoolKey : ICloneable
	{
		// Token: 0x0600250D RID: 9485 RVA: 0x000A7E2E File Offset: 0x000A602E
		internal DbConnectionPoolKey(string connectionString)
		{
			this._connectionString = connectionString;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000A7E3D File Offset: 0x000A603D
		protected DbConnectionPoolKey(DbConnectionPoolKey key)
		{
			this._connectionString = key.ConnectionString;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000A7E51 File Offset: 0x000A6051
		public virtual object Clone()
		{
			return new DbConnectionPoolKey(this);
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000A7E59 File Offset: 0x000A6059
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x000A7E61 File Offset: 0x000A6061
		internal virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000A7E6C File Offset: 0x000A606C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(DbConnectionPoolKey))
			{
				return false;
			}
			DbConnectionPoolKey dbConnectionPoolKey = obj as DbConnectionPoolKey;
			return dbConnectionPoolKey != null && this._connectionString == dbConnectionPoolKey._connectionString;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000A7EB2 File Offset: 0x000A60B2
		public override int GetHashCode()
		{
			if (this._connectionString != null)
			{
				return this._connectionString.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04001833 RID: 6195
		private string _connectionString;
	}
}
