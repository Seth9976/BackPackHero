using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FB RID: 763
	internal abstract class DbConnectionFactory
	{
		// Token: 0x06002273 RID: 8819 RVA: 0x0009ECC2 File Offset: 0x0009CEC2
		protected DbConnectionFactory()
		{
			this._connectionPoolGroups = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>();
			this._poolsToRelease = new List<DbConnectionPool>();
			this._poolGroupsToRelease = new List<DbConnectionPoolGroup>();
			this._pruningTimer = this.CreatePruningTimer();
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002274 RID: 8820
		public abstract DbProviderFactory ProviderFactory { get; }

		// Token: 0x06002275 RID: 8821 RVA: 0x0009ECF8 File Offset: 0x0009CEF8
		public void ClearAllPools()
		{
			foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in this._connectionPoolGroups)
			{
				DbConnectionPoolGroup value = keyValuePair.Value;
				if (value != null)
				{
					value.Clear();
				}
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0009ED58 File Offset: 0x0009CF58
		public void ClearPool(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionPoolGroup connectionPoolGroup = this.GetConnectionPoolGroup(connection);
			if (connectionPoolGroup != null)
			{
				connectionPoolGroup.Clear();
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0009ED84 File Offset: 0x0009CF84
		public void ClearPool(DbConnectionPoolKey key)
		{
			ADP.CheckArgumentNull(key.ConnectionString, "key.ConnectionString");
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (this._connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
			{
				dbConnectionPoolGroup.Clear();
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00003DF6 File Offset: 0x00001FF6
		internal virtual DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0009EDB8 File Offset: 0x0009CFB8
		internal DbConnectionInternal CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup, DbConnectionOptions userOptions)
		{
			DbConnectionOptions connectionOptions = poolGroup.ConnectionOptions;
			DbConnectionPoolGroupProviderInfo providerInfo = poolGroup.ProviderInfo;
			DbConnectionPoolKey poolKey = poolGroup.PoolKey;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(connectionOptions, poolKey, providerInfo, null, owningConnection, userOptions);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.MakeNonPooledObject(owningConnection);
			}
			return dbConnectionInternal;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0009EDF4 File Offset: 0x0009CFF4
		internal DbConnectionInternal CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
		{
			DbConnectionPoolGroupProviderInfo providerInfo = pool.PoolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(options, poolKey, providerInfo, pool, owningObject, userOptions);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.MakePooledConnection(pool);
			}
			return dbConnectionInternal;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00003DF6 File Offset: 0x00001FF6
		internal virtual DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0009EE27 File Offset: 0x0009D027
		private Timer CreatePruningTimer()
		{
			return ADP.UnsafeCreateTimer(new TimerCallback(this.PruneConnectionPoolGroups), null, 240000, 30000);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0009EE48 File Offset: 0x0009D048
		protected DbConnectionOptions FindConnectionOptions(DbConnectionPoolKey key)
		{
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!string.IsNullOrEmpty(key.ConnectionString) && this._connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
			{
				return dbConnectionPoolGroup.ConnectionOptions;
			}
			return null;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0009EE7A File Offset: 0x0009D07A
		private static Task<DbConnectionInternal> GetCompletedTask()
		{
			Task<DbConnectionInternal> task;
			if ((task = DbConnectionFactory.s_completedTask) == null)
			{
				task = (DbConnectionFactory.s_completedTask = Task.FromResult<DbConnectionInternal>(null));
			}
			return task;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0009EE94 File Offset: 0x0009D094
		private DbConnectionPool GetConnectionPool(DbConnection owningObject, DbConnectionPoolGroup connectionPoolGroup)
		{
			if (connectionPoolGroup.IsDisabled && connectionPoolGroup.PoolGroupOptions != null)
			{
				DbConnectionPoolGroupOptions poolGroupOptions = connectionPoolGroup.PoolGroupOptions;
				DbConnectionOptions connectionOptions = connectionPoolGroup.ConnectionOptions;
				connectionPoolGroup = this.GetConnectionPoolGroup(connectionPoolGroup.PoolKey, poolGroupOptions, ref connectionOptions);
				this.SetConnectionPoolGroup(owningObject, connectionPoolGroup);
			}
			return connectionPoolGroup.GetConnectionPool(this);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0009EEE0 File Offset: 0x0009D0E0
		internal DbConnectionPoolGroup GetConnectionPoolGroup(DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolOptions, ref DbConnectionOptions userConnectionOptions)
		{
			if (string.IsNullOrEmpty(key.ConnectionString))
			{
				return null;
			}
			Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = this._connectionPoolGroups;
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!dictionary.TryGetValue(key, out dbConnectionPoolGroup) || (dbConnectionPoolGroup.IsDisabled && dbConnectionPoolGroup.PoolGroupOptions != null))
			{
				DbConnectionOptions dbConnectionOptions = this.CreateConnectionOptions(key.ConnectionString, userConnectionOptions);
				if (dbConnectionOptions == null)
				{
					throw ADP.InternalConnectionError(ADP.ConnectionError.ConnectionOptionsMissing);
				}
				if (userConnectionOptions == null)
				{
					userConnectionOptions = dbConnectionOptions;
				}
				if (poolOptions == null)
				{
					if (dbConnectionPoolGroup != null)
					{
						poolOptions = dbConnectionPoolGroup.PoolGroupOptions;
					}
					else
					{
						poolOptions = this.CreateConnectionPoolGroupOptions(dbConnectionOptions);
					}
				}
				lock (this)
				{
					dictionary = this._connectionPoolGroups;
					if (!dictionary.TryGetValue(key, out dbConnectionPoolGroup))
					{
						DbConnectionPoolGroup dbConnectionPoolGroup2 = new DbConnectionPoolGroup(dbConnectionOptions, key, poolOptions);
						dbConnectionPoolGroup2.ProviderInfo = this.CreateConnectionPoolGroupProviderInfo(dbConnectionOptions);
						Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary2 = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(1 + dictionary.Count);
						foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in dictionary)
						{
							dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
						}
						dictionary2.Add(key, dbConnectionPoolGroup2);
						dbConnectionPoolGroup = dbConnectionPoolGroup2;
						this._connectionPoolGroups = dictionary2;
					}
					return dbConnectionPoolGroup;
				}
			}
			if (userConnectionOptions == null)
			{
				userConnectionOptions = dbConnectionPoolGroup.ConnectionOptions;
			}
			return dbConnectionPoolGroup;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0009F030 File Offset: 0x0009D230
		private void PruneConnectionPoolGroups(object state)
		{
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (this._poolsToRelease.Count != 0)
				{
					foreach (DbConnectionPool dbConnectionPool in this._poolsToRelease.ToArray())
					{
						if (dbConnectionPool != null)
						{
							dbConnectionPool.Clear();
							if (dbConnectionPool.Count == 0)
							{
								this._poolsToRelease.Remove(dbConnectionPool);
							}
						}
					}
				}
			}
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				if (this._poolGroupsToRelease.Count != 0)
				{
					foreach (DbConnectionPoolGroup dbConnectionPoolGroup in this._poolGroupsToRelease.ToArray())
					{
						if (dbConnectionPoolGroup != null && dbConnectionPoolGroup.Clear() == 0)
						{
							this._poolGroupsToRelease.Remove(dbConnectionPoolGroup);
						}
					}
				}
			}
			lock (this)
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(connectionPoolGroups.Count);
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					if (keyValuePair.Value != null)
					{
						if (keyValuePair.Value.Prune())
						{
							this.QueuePoolGroupForRelease(keyValuePair.Value);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				this._connectionPoolGroups = dictionary;
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0009F1E8 File Offset: 0x0009D3E8
		internal void QueuePoolForRelease(DbConnectionPool pool, bool clearing)
		{
			pool.Shutdown();
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (clearing)
				{
					pool.Clear();
				}
				this._poolsToRelease.Add(pool);
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0009F240 File Offset: 0x0009D440
		internal void QueuePoolGroupForRelease(DbConnectionPoolGroup poolGroup)
		{
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				this._poolGroupsToRelease.Add(poolGroup);
			}
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0009F288 File Offset: 0x0009D488
		protected virtual DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0009F298 File Offset: 0x0009D498
		internal DbMetaDataFactory GetMetaDataFactory(DbConnectionPoolGroup connectionPoolGroup, DbConnectionInternal internalConnection)
		{
			DbMetaDataFactory dbMetaDataFactory = connectionPoolGroup.MetaDataFactory;
			if (dbMetaDataFactory == null)
			{
				bool flag = false;
				dbMetaDataFactory = this.CreateMetaDataFactory(internalConnection, out flag);
				if (flag)
				{
					connectionPoolGroup.MetaDataFactory = dbMetaDataFactory;
				}
			}
			return dbMetaDataFactory;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0009F2C6 File Offset: 0x0009D4C6
		protected virtual DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			throw ADP.NotSupported();
		}

		// Token: 0x06002287 RID: 8839
		protected abstract DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection);

		// Token: 0x06002288 RID: 8840
		protected abstract DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous);

		// Token: 0x06002289 RID: 8841
		protected abstract DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions options);

		// Token: 0x0600228A RID: 8842
		internal abstract DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection);

		// Token: 0x0600228B RID: 8843
		internal abstract DbConnectionInternal GetInnerConnection(DbConnection connection);

		// Token: 0x0600228C RID: 8844
		internal abstract void PermissionDemand(DbConnection outerConnection);

		// Token: 0x0600228D RID: 8845
		internal abstract void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup);

		// Token: 0x0600228E RID: 8846
		internal abstract void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x0600228F RID: 8847
		internal abstract bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from);

		// Token: 0x06002290 RID: 8848
		internal abstract void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x06002291 RID: 8849 RVA: 0x0009F2D0 File Offset: 0x0009D4D0
		internal bool TryGetConnection(DbConnection owningConnection, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, out DbConnectionInternal connection)
		{
			DbConnectionFactory.<>c__DisplayClass40_0 CS$<>8__locals1 = new DbConnectionFactory.<>c__DisplayClass40_0();
			CS$<>8__locals1.retry = retry;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.owningConnection = owningConnection;
			CS$<>8__locals1.userOptions = userOptions;
			CS$<>8__locals1.oldConnection = oldConnection;
			connection = null;
			int num = 10;
			int num2 = 1;
			for (;;)
			{
				CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
				DbConnectionPool connectionPool = this.GetConnectionPool(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup);
				if (connectionPool == null)
				{
					CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
					if (CS$<>8__locals1.retry != null)
					{
						break;
					}
					connection = this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
				}
				else
				{
					if (((SqlConnection)CS$<>8__locals1.owningConnection).ForceNewConnection)
					{
						connection = connectionPool.ReplaceConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.userOptions, CS$<>8__locals1.oldConnection);
					}
					else if (!connectionPool.TryGetConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.retry, CS$<>8__locals1.userOptions, out connection))
					{
						return false;
					}
					if (connection == null)
					{
						if (connectionPool.IsRunning)
						{
							goto Block_8;
						}
						Thread.Sleep(num2);
						num2 *= 2;
					}
				}
				if (connection != null || num-- <= 0)
				{
					goto IL_0268;
				}
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			Task<DbConnectionInternal>[] array = DbConnectionFactory.s_pendingOpenNonPooled;
			Task<DbConnectionInternal> task3;
			lock (array)
			{
				int i;
				for (i = 0; i < DbConnectionFactory.s_pendingOpenNonPooled.Length; i++)
				{
					Task task4 = DbConnectionFactory.s_pendingOpenNonPooled[i];
					if (task4 == null)
					{
						DbConnectionFactory.s_pendingOpenNonPooled[i] = DbConnectionFactory.GetCompletedTask();
						break;
					}
					if (task4.IsCompleted)
					{
						break;
					}
				}
				if (i == DbConnectionFactory.s_pendingOpenNonPooled.Length)
				{
					i = (int)((ulong)DbConnectionFactory.s_pendingOpenNonPooledNext % (ulong)((long)DbConnectionFactory.s_pendingOpenNonPooled.Length));
					DbConnectionFactory.s_pendingOpenNonPooledNext += 1U;
				}
				Task<DbConnectionInternal> task2 = DbConnectionFactory.s_pendingOpenNonPooled[i];
				Func<Task<DbConnectionInternal>, DbConnectionInternal> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate(Task<DbConnectionInternal> _)
					{
						Transaction currentTransaction = ADP.GetCurrentTransaction();
						DbConnectionInternal dbConnectionInternal2;
						try
						{
							ADP.SetCurrentTransaction(CS$<>8__locals1.retry.Task.AsyncState as Transaction);
							DbConnectionInternal dbConnectionInternal = CS$<>8__locals1.<>4__this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
							if (CS$<>8__locals1.oldConnection != null && CS$<>8__locals1.oldConnection.State == ConnectionState.Open)
							{
								CS$<>8__locals1.oldConnection.PrepareForReplaceConnection();
								CS$<>8__locals1.oldConnection.Dispose();
							}
							dbConnectionInternal2 = dbConnectionInternal;
						}
						finally
						{
							ADP.SetCurrentTransaction(currentTransaction);
						}
						return dbConnectionInternal2;
					});
				}
				task3 = task2.ContinueWith<DbConnectionInternal>(func, cancellationTokenSource.Token, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
				DbConnectionFactory.s_pendingOpenNonPooled[i] = task3;
			}
			if (CS$<>8__locals1.owningConnection.ConnectionTimeout > 0)
			{
				int num3 = CS$<>8__locals1.owningConnection.ConnectionTimeout * 1000;
				cancellationTokenSource.CancelAfter(num3);
			}
			task3.ContinueWith(delegate(Task<DbConnectionInternal> task)
			{
				cancellationTokenSource.Dispose();
				if (task.IsCanceled)
				{
					CS$<>8__locals1.retry.TrySetException(ADP.ExceptionWithStackTrace(ADP.NonPooledOpenTimeout()));
					return;
				}
				if (task.IsFaulted)
				{
					CS$<>8__locals1.retry.TrySetException(task.Exception.InnerException);
					return;
				}
				if (!CS$<>8__locals1.retry.TrySetResult(task.Result))
				{
					task.Result.DoomThisConnection();
					task.Result.Dispose();
				}
			}, TaskScheduler.Default);
			return false;
			Block_8:
			throw ADP.PooledOpenTimeout();
			IL_0268:
			if (connection == null)
			{
				throw ADP.PooledOpenTimeout();
			}
			return true;
		}

		// Token: 0x04001721 RID: 5921
		private Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> _connectionPoolGroups;

		// Token: 0x04001722 RID: 5922
		private readonly List<DbConnectionPool> _poolsToRelease;

		// Token: 0x04001723 RID: 5923
		private readonly List<DbConnectionPoolGroup> _poolGroupsToRelease;

		// Token: 0x04001724 RID: 5924
		private readonly Timer _pruningTimer;

		// Token: 0x04001725 RID: 5925
		private const int PruningDueTime = 240000;

		// Token: 0x04001726 RID: 5926
		private const int PruningPeriod = 30000;

		// Token: 0x04001727 RID: 5927
		private static uint s_pendingOpenNonPooledNext = 0U;

		// Token: 0x04001728 RID: 5928
		private static Task<DbConnectionInternal>[] s_pendingOpenNonPooled = new Task<DbConnectionInternal>[Environment.ProcessorCount];

		// Token: 0x04001729 RID: 5929
		private static Task<DbConnectionInternal> s_completedTask;
	}
}
