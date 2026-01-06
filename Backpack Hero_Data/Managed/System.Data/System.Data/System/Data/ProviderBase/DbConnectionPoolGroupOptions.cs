using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000311 RID: 785
	internal sealed class DbConnectionPoolGroupOptions
	{
		// Token: 0x060023B3 RID: 9139 RVA: 0x000A4AE8 File Offset: 0x000A2CE8
		public DbConnectionPoolGroupOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity)
		{
			this._poolByIdentity = poolByIdentity;
			this._minPoolSize = minPoolSize;
			this._maxPoolSize = maxPoolSize;
			this._creationTimeout = creationTimeout;
			if (loadBalanceTimeout != 0)
			{
				this._loadBalanceTimeout = new TimeSpan(0, 0, loadBalanceTimeout);
				this._useLoadBalancing = true;
			}
			this._hasTransactionAffinity = hasTransactionAffinity;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000A4B3A File Offset: 0x000A2D3A
		public int CreationTimeout
		{
			get
			{
				return this._creationTimeout;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x000A4B42 File Offset: 0x000A2D42
		public bool HasTransactionAffinity
		{
			get
			{
				return this._hasTransactionAffinity;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000A4B4A File Offset: 0x000A2D4A
		public TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000A4B52 File Offset: 0x000A2D52
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x000A4B5A File Offset: 0x000A2D5A
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000A4B62 File Offset: 0x000A2D62
		public bool PoolByIdentity
		{
			get
			{
				return this._poolByIdentity;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000A4B6A File Offset: 0x000A2D6A
		public bool UseLoadBalancing
		{
			get
			{
				return this._useLoadBalancing;
			}
		}

		// Token: 0x040017BA RID: 6074
		private readonly bool _poolByIdentity;

		// Token: 0x040017BB RID: 6075
		private readonly int _minPoolSize;

		// Token: 0x040017BC RID: 6076
		private readonly int _maxPoolSize;

		// Token: 0x040017BD RID: 6077
		private readonly int _creationTimeout;

		// Token: 0x040017BE RID: 6078
		private readonly TimeSpan _loadBalanceTimeout;

		// Token: 0x040017BF RID: 6079
		private readonly bool _hasTransactionAffinity;

		// Token: 0x040017C0 RID: 6080
		private readonly bool _useLoadBalancing;
	}
}
