using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FF RID: 767
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x060022D4 RID: 8916 RVA: 0x0009FDF2 File Offset: 0x0009DFF2
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolKey = key;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
			this._state = 1;
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0009FE21 File Offset: 0x0009E021
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0009FE29 File Offset: 0x0009E029
		internal DbConnectionPoolKey PoolKey
		{
			get
			{
				return this._poolKey;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0009FE31 File Offset: 0x0009E031
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0009FE39 File Offset: 0x0009E039
		internal DbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			set
			{
				this._providerInfo = value;
				if (value != null)
				{
					this._providerInfo.PoolGroup = this;
				}
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0009FE51 File Offset: 0x0009E051
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x0009FE5C File Offset: 0x0009E05C
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x0009FE64 File Offset: 0x0009E064
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x0009FE6C File Offset: 0x0009E06C
		internal DbMetaDataFactory MetaDataFactory
		{
			get
			{
				return this._metaDataFactory;
			}
			set
			{
				this._metaDataFactory = value;
			}
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0009FE78 File Offset: 0x0009E078
		internal int Clear()
		{
			ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = null;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					concurrentDictionary = this._poolCollection;
					this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
				}
			}
			if (concurrentDictionary != null)
			{
				foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in concurrentDictionary)
				{
					DbConnectionPool value = keyValuePair.Value;
					if (value != null)
					{
						value.ConnectionFactory.QueuePoolForRelease(value, true);
					}
				}
			}
			return this._poolCollection.Count;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0009FF2C File Offset: 0x0009E12C
		internal DbConnectionPool GetConnectionPool(DbConnectionFactory connectionFactory)
		{
			DbConnectionPool dbConnectionPool = null;
			if (this._poolGroupOptions != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.NoIdentity;
				if (this._poolGroupOptions.PoolByIdentity)
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
					if (dbConnectionPoolIdentity.IsRestricted)
					{
						dbConnectionPoolIdentity = null;
					}
				}
				if (dbConnectionPoolIdentity != null && !this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
				{
					DbConnectionPoolGroup dbConnectionPoolGroup = this;
					lock (dbConnectionPoolGroup)
					{
						if (!this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
						{
							DbConnectionPoolProviderInfo dbConnectionPoolProviderInfo = connectionFactory.CreateConnectionPoolProviderInfo(this.ConnectionOptions);
							DbConnectionPool dbConnectionPool2 = new DbConnectionPool(connectionFactory, this, dbConnectionPoolIdentity, dbConnectionPoolProviderInfo);
							if (this.MarkPoolGroupAsActive())
							{
								dbConnectionPool2.Startup();
								this._poolCollection.TryAdd(dbConnectionPoolIdentity, dbConnectionPool2);
								dbConnectionPool = dbConnectionPool2;
							}
							else
							{
								dbConnectionPool2.Shutdown();
							}
						}
					}
				}
			}
			if (dbConnectionPool == null)
			{
				DbConnectionPoolGroup dbConnectionPoolGroup = this;
				lock (dbConnectionPoolGroup)
				{
					this.MarkPoolGroupAsActive();
				}
			}
			return dbConnectionPool;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000A0028 File Offset: 0x0009E228
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
			}
			return 1 == this._state;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000A0044 File Offset: 0x0009E244
		internal bool Prune()
		{
			bool flag2;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
					foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in this._poolCollection)
					{
						DbConnectionPool value = keyValuePair.Value;
						if (value != null)
						{
							if (!value.ErrorOccurred && value.Count == 0)
							{
								value.ConnectionFactory.QueuePoolForRelease(value, false);
							}
							else
							{
								concurrentDictionary.TryAdd(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
					this._poolCollection = concurrentDictionary;
				}
				if (this._poolCollection.Count == 0)
				{
					if (1 == this._state)
					{
						this._state = 2;
					}
					else if (2 == this._state)
					{
						this._state = 4;
					}
				}
				flag2 = 4 == this._state;
			}
			return flag2;
		}

		// Token: 0x04001742 RID: 5954
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x04001743 RID: 5955
		private readonly DbConnectionPoolKey _poolKey;

		// Token: 0x04001744 RID: 5956
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x04001745 RID: 5957
		private ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> _poolCollection;

		// Token: 0x04001746 RID: 5958
		private int _state;

		// Token: 0x04001747 RID: 5959
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x04001748 RID: 5960
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x04001749 RID: 5961
		private const int PoolGroupStateActive = 1;

		// Token: 0x0400174A RID: 5962
		private const int PoolGroupStateIdle = 2;

		// Token: 0x0400174B RID: 5963
		private const int PoolGroupStateDisabled = 4;
	}
}
