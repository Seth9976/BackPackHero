using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000309 RID: 777
	internal sealed class DbConnectionPool
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x000A3490 File Offset: 0x000A1690
		internal DbConnectionPool(DbConnectionFactory connectionFactory, DbConnectionPoolGroup connectionPoolGroup, DbConnectionPoolIdentity identity, DbConnectionPoolProviderInfo connectionPoolProviderInfo)
		{
			if (identity != null && identity.IsRestricted)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToPoolOnRestrictedToken);
			}
			this._state = DbConnectionPool.State.Initializing;
			Random random = DbConnectionPool.s_random;
			lock (random)
			{
				this._cleanupWait = DbConnectionPool.s_random.Next(12, 24) * 10 * 1000;
			}
			this._connectionFactory = connectionFactory;
			this._connectionPoolGroup = connectionPoolGroup;
			this._connectionPoolGroupOptions = connectionPoolGroup.PoolGroupOptions;
			this._connectionPoolProviderInfo = connectionPoolProviderInfo;
			this._identity = identity;
			this._waitHandles = new DbConnectionPool.PoolWaitHandles();
			this._errorWait = 5000;
			this._errorTimer = null;
			this._objectList = new List<DbConnectionInternal>(this.MaxPoolSize);
			this._transactedConnectionPool = new DbConnectionPool.TransactedConnectionPool(this);
			this._poolCreateRequest = new WaitCallback(this.PoolCreateRequest);
			this._state = DbConnectionPool.State.Running;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000A35A4 File Offset: 0x000A17A4
		private int CreationTimeout
		{
			get
			{
				return this.PoolGroupOptions.CreationTimeout;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000A35B1 File Offset: 0x000A17B1
		internal int Count
		{
			get
			{
				return this._totalObjects;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000A35B9 File Offset: 0x000A17B9
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return this._connectionFactory;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x000A35C1 File Offset: 0x000A17C1
		internal bool ErrorOccurred
		{
			get
			{
				return this._errorOccurred;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000A35CB File Offset: 0x000A17CB
		private bool HasTransactionAffinity
		{
			get
			{
				return this.PoolGroupOptions.HasTransactionAffinity;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x000A35D8 File Offset: 0x000A17D8
		internal TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this.PoolGroupOptions.LoadBalanceTimeout;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000A35E8 File Offset: 0x000A17E8
		private bool NeedToReplenish
		{
			get
			{
				if (DbConnectionPool.State.Running != this._state)
				{
					return false;
				}
				int count = this.Count;
				if (count >= this.MaxPoolSize)
				{
					return false;
				}
				if (count < this.MinPoolSize)
				{
					return true;
				}
				int num = this._stackNew.Count + this._stackOld.Count;
				int waitCount = this._waitCount;
				return num < waitCount || (num == waitCount && count > 1);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000A364C File Offset: 0x000A184C
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000A3654 File Offset: 0x000A1854
		internal bool IsRunning
		{
			get
			{
				return DbConnectionPool.State.Running == this._state;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000A365F File Offset: 0x000A185F
		private int MaxPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MaxPoolSize;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000A366C File Offset: 0x000A186C
		private int MinPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MinPoolSize;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000A3679 File Offset: 0x000A1879
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._connectionPoolGroup;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000A3681 File Offset: 0x000A1881
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._connectionPoolGroupOptions;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000A3689 File Offset: 0x000A1889
		internal DbConnectionPoolProviderInfo ProviderInfo
		{
			get
			{
				return this._connectionPoolProviderInfo;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000A3691 File Offset: 0x000A1891
		internal bool UseLoadBalancing
		{
			get
			{
				return this.PoolGroupOptions.UseLoadBalancing;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000A369E File Offset: 0x000A189E
		private bool UsingIntegrateSecurity
		{
			get
			{
				return this._identity != null && DbConnectionPoolIdentity.NoIdentity != this._identity;
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000A36BC File Offset: 0x000A18BC
		private void CleanupCallback(object state)
		{
			while (this.Count > this.MinPoolSize && this._waitHandles.PoolSemaphore.WaitOne(0))
			{
				DbConnectionInternal dbConnectionInternal;
				if (!this._stackOld.TryPop(out dbConnectionInternal))
				{
					this._waitHandles.PoolSemaphore.Release(1);
					break;
				}
				bool flag = true;
				DbConnectionInternal dbConnectionInternal2 = dbConnectionInternal;
				lock (dbConnectionInternal2)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.DestroyObject(dbConnectionInternal);
				}
				else
				{
					dbConnectionInternal.SetInStasis();
				}
			}
			if (this._waitHandles.PoolSemaphore.WaitOne(0))
			{
				DbConnectionInternal dbConnectionInternal3;
				while (this._stackNew.TryPop(out dbConnectionInternal3))
				{
					this._stackOld.Push(dbConnectionInternal3);
				}
				this._waitHandles.PoolSemaphore.Release(1);
			}
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000A37A0 File Offset: 0x000A19A0
		internal void Clear()
		{
			List<DbConnectionInternal> objectList = this._objectList;
			DbConnectionInternal dbConnectionInternal;
			lock (objectList)
			{
				int count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						dbConnectionInternal.DoNotPoolThisConnection();
					}
				}
				goto IL_0057;
			}
			IL_0050:
			this.DestroyObject(dbConnectionInternal);
			IL_0057:
			if (!this._stackNew.TryPop(out dbConnectionInternal))
			{
				while (this._stackOld.TryPop(out dbConnectionInternal))
				{
					this.DestroyObject(dbConnectionInternal);
				}
				this.ReclaimEmancipatedObjects();
				return;
			}
			goto IL_0050;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000A3844 File Offset: 0x000A1A44
		private Timer CreateCleanupTimer()
		{
			return ADP.UnsafeCreateTimer(new TimerCallback(this.CleanupCallback), null, this._cleanupWait, this._cleanupWait);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000A3864 File Offset: 0x000A1A64
		private DbConnectionInternal CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			try
			{
				dbConnectionInternal = this._connectionFactory.CreatePooledConnection(this, owningObject, this._connectionPoolGroup.ConnectionOptions, this._connectionPoolGroup.PoolKey, userOptions);
				if (dbConnectionInternal == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateObjectReturnedNull);
				}
				if (!dbConnectionInternal.CanBePooled)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NewObjectCannotBePooled);
				}
				dbConnectionInternal.PrePush(null);
				List<DbConnectionInternal> list = this._objectList;
				lock (list)
				{
					if (oldConnection != null && oldConnection.Pool == this)
					{
						this._objectList.Remove(oldConnection);
					}
					this._objectList.Add(dbConnectionInternal);
					this._totalObjects = this._objectList.Count;
				}
				if (oldConnection != null)
				{
					DbConnectionPool pool = oldConnection.Pool;
					if (pool != null && pool != this)
					{
						list = pool._objectList;
						lock (list)
						{
							pool._objectList.Remove(oldConnection);
							pool._totalObjects = pool._objectList.Count;
						}
					}
				}
				this._errorWait = 5000;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				dbConnectionInternal = null;
				this._resError = ex;
				Timer timer = new Timer(new TimerCallback(this.ErrorCallback), null, -1, -1);
				try
				{
				}
				finally
				{
					this._waitHandles.ErrorEvent.Set();
					this._errorOccurred = true;
					this._errorTimer = timer;
					timer.Change(this._errorWait, this._errorWait);
				}
				if (30000 < this._errorWait)
				{
					this._errorWait = 60000;
				}
				else
				{
					this._errorWait *= 2;
				}
				throw;
			}
			return dbConnectionInternal;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000A3A2C File Offset: 0x000A1C2C
		private void DeactivateObject(DbConnectionInternal obj)
		{
			obj.DeactivateConnection();
			bool flag = false;
			bool flag2 = false;
			if (obj.IsConnectionDoomed)
			{
				flag2 = true;
			}
			else
			{
				lock (obj)
				{
					if (this._state == DbConnectionPool.State.ShuttingDown)
					{
						if (obj.IsTransactionRoot)
						{
							obj.SetInStasis();
						}
						else
						{
							flag2 = true;
						}
					}
					else if (obj.IsNonPoolableTransactionRoot)
					{
						obj.SetInStasis();
					}
					else if (obj.CanBePooled)
					{
						Transaction enlistedTransaction = obj.EnlistedTransaction;
						if (null != enlistedTransaction)
						{
							this._transactedConnectionPool.PutTransactedObject(enlistedTransaction, obj);
						}
						else
						{
							flag = true;
						}
					}
					else if (obj.IsTransactionRoot && !obj.IsConnectionDoomed)
					{
						obj.SetInStasis();
					}
					else
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.PutNewObject(obj);
				return;
			}
			if (flag2)
			{
				this.DestroyObject(obj);
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000A3B0C File Offset: 0x000A1D0C
		internal void DestroyObject(DbConnectionInternal obj)
		{
			if (!obj.IsTxRootWaitingForTxEnd)
			{
				List<DbConnectionInternal> objectList = this._objectList;
				lock (objectList)
				{
					this._objectList.Remove(obj);
					this._totalObjects = this._objectList.Count;
				}
				obj.Dispose();
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000A3B74 File Offset: 0x000A1D74
		private void ErrorCallback(object state)
		{
			this._errorOccurred = false;
			this._waitHandles.ErrorEvent.Reset();
			Timer errorTimer = this._errorTimer;
			this._errorTimer = null;
			if (errorTimer != null)
			{
				errorTimer.Dispose();
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000A3BB4 File Offset: 0x000A1DB4
		private Exception TryCloneCachedException()
		{
			if (this._resError == null)
			{
				return null;
			}
			SqlException ex = this._resError as SqlException;
			if (ex != null)
			{
				return ex.InternalClone();
			}
			return this._resError;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000A3BE8 File Offset: 0x000A1DE8
		private void WaitForPendingOpen()
		{
			DbConnectionPool.PendingGetConnection pendingGetConnection;
			do
			{
				bool flag = false;
				try
				{
					try
					{
					}
					finally
					{
						flag = Interlocked.CompareExchange(ref this._pendingOpensWaiting, 1, 0) == 0;
					}
					if (!flag)
					{
						break;
					}
					while (this._pendingOpens.TryDequeue(out pendingGetConnection))
					{
						if (!pendingGetConnection.Completion.Task.IsCompleted)
						{
							uint num;
							if (pendingGetConnection.DueTime == -1L)
							{
								num = uint.MaxValue;
							}
							else
							{
								num = (uint)Math.Max(ADP.TimerRemainingMilliseconds(pendingGetConnection.DueTime), 0L);
							}
							DbConnectionInternal dbConnectionInternal = null;
							bool flag2 = false;
							Exception ex = null;
							try
							{
								bool flag3 = true;
								bool flag4 = false;
								ADP.SetCurrentTransaction(pendingGetConnection.Completion.Task.AsyncState as Transaction);
								flag2 = !this.TryGetConnection(pendingGetConnection.Owner, num, flag3, flag4, pendingGetConnection.UserOptions, out dbConnectionInternal);
							}
							catch (Exception ex)
							{
							}
							if (ex != null)
							{
								pendingGetConnection.Completion.TrySetException(ex);
							}
							else if (flag2)
							{
								pendingGetConnection.Completion.TrySetException(ADP.ExceptionWithStackTrace(ADP.PooledOpenTimeout()));
							}
							else if (!pendingGetConnection.Completion.TrySetResult(dbConnectionInternal))
							{
								this.PutObject(dbConnectionInternal, pendingGetConnection.Owner);
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						Interlocked.Exchange(ref this._pendingOpensWaiting, 0);
					}
				}
			}
			while (this._pendingOpens.TryPeek(out pendingGetConnection));
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000A3D68 File Offset: 0x000A1F68
		internal bool TryGetConnection(DbConnection owningObject, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			uint num = 0U;
			bool flag = false;
			if (retry == null)
			{
				num = (uint)this.CreationTimeout;
				if (num == 0U)
				{
					num = uint.MaxValue;
				}
				flag = true;
			}
			if (this._state != DbConnectionPool.State.Running)
			{
				connection = null;
				return true;
			}
			bool flag2 = true;
			if (this.TryGetConnection(owningObject, num, flag, flag2, userOptions, out connection))
			{
				return true;
			}
			if (retry == null)
			{
				return true;
			}
			DbConnectionPool.PendingGetConnection pendingGetConnection = new DbConnectionPool.PendingGetConnection((this.CreationTimeout == 0) ? (-1L) : (ADP.TimerCurrent() + ADP.TimerFromSeconds(this.CreationTimeout / 1000)), owningObject, retry, userOptions);
			this._pendingOpens.Enqueue(pendingGetConnection);
			if (this._pendingOpensWaiting == 0)
			{
				new Thread(new ThreadStart(this.WaitForPendingOpen))
				{
					IsBackground = true
				}.Start();
			}
			connection = null;
			return false;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000A3E14 File Offset: 0x000A2014
		private bool TryGetConnection(DbConnection owningObject, uint waitForMultipleObjectsTimeout, bool allowCreate, bool onlyOneCheckConnection, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			Transaction transaction = null;
			if (this.HasTransactionAffinity)
			{
				dbConnectionInternal = this.GetFromTransactedPool(out transaction);
			}
			if (dbConnectionInternal == null)
			{
				Interlocked.Increment(ref this._waitCount);
				for (;;)
				{
					int num = 3;
					try
					{
						try
						{
						}
						finally
						{
							num = WaitHandle.WaitAny(this._waitHandles.GetHandles(allowCreate), (int)waitForMultipleObjectsTimeout);
						}
						switch (num)
						{
						case 0:
							Interlocked.Decrement(ref this._waitCount);
							dbConnectionInternal = this.GetFromGeneralPool();
							if (dbConnectionInternal != null && !dbConnectionInternal.IsConnectionAlive(false))
							{
								this.DestroyObject(dbConnectionInternal);
								dbConnectionInternal = null;
								if (onlyOneCheckConnection)
								{
									if (this._waitHandles.CreationSemaphore.WaitOne((int)waitForMultipleObjectsTimeout))
									{
										try
										{
											dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
											break;
										}
										finally
										{
											this._waitHandles.CreationSemaphore.Release(1);
										}
									}
									connection = null;
									return false;
								}
							}
							break;
						case 1:
							Interlocked.Decrement(ref this._waitCount);
							throw this.TryCloneCachedException();
						case 2:
							try
							{
								dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
							}
							catch
							{
								if (dbConnectionInternal == null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
								throw;
							}
							finally
							{
								if (dbConnectionInternal != null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
							}
							if (dbConnectionInternal == null && this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
							{
								allowCreate = false;
							}
							break;
						default:
							if (num == 258)
							{
								Interlocked.Decrement(ref this._waitCount);
								connection = null;
								return false;
							}
							Interlocked.Decrement(ref this._waitCount);
							throw ADP.InternalError(ADP.InternalErrorCode.UnexpectedWaitAnyResult);
						}
					}
					finally
					{
						if (2 == num)
						{
							this._waitHandles.CreationSemaphore.Release(1);
						}
					}
					if (dbConnectionInternal != null)
					{
						goto IL_0185;
					}
				}
				bool flag;
				return flag;
			}
			IL_0185:
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, transaction);
			}
			connection = dbConnectionInternal;
			return true;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000A4038 File Offset: 0x000A2238
		private void PrepareConnection(DbConnection owningObject, DbConnectionInternal obj, Transaction transaction)
		{
			lock (obj)
			{
				obj.PostPop(owningObject);
			}
			try
			{
				obj.ActivateConnection(transaction);
			}
			catch
			{
				this.PutObject(obj, owningObject);
				throw;
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000A4098 File Offset: 0x000A2298
		internal DbConnectionInternal ReplaceConnection(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, oldConnection);
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, oldConnection.EnlistedTransaction);
				oldConnection.PrepareForReplaceConnection();
				oldConnection.DeactivateConnection();
				oldConnection.Dispose();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000A40D4 File Offset: 0x000A22D4
		private DbConnectionInternal GetFromGeneralPool()
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (!this._stackNew.TryPop(out dbConnectionInternal) && !this._stackOld.TryPop(out dbConnectionInternal))
			{
				dbConnectionInternal = null;
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000A4108 File Offset: 0x000A2308
		private DbConnectionInternal GetFromTransactedPool(out Transaction transaction)
		{
			transaction = ADP.GetCurrentTransaction();
			DbConnectionInternal dbConnectionInternal = null;
			if (null != transaction && this._transactedConnectionPool != null)
			{
				dbConnectionInternal = this._transactedConnectionPool.GetTransactedObject(transaction);
				if (dbConnectionInternal != null)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						try
						{
							dbConnectionInternal.IsConnectionAlive(true);
							return dbConnectionInternal;
						}
						catch
						{
							this.DestroyObject(dbConnectionInternal);
							throw;
						}
					}
					if (!dbConnectionInternal.IsConnectionAlive(false))
					{
						this.DestroyObject(dbConnectionInternal);
						dbConnectionInternal = null;
					}
				}
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000A4180 File Offset: 0x000A2380
		private void PoolCreateRequest(object state)
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				if (!this._pendingOpens.IsEmpty && this._pendingOpensWaiting == 0)
				{
					new Thread(new ThreadStart(this.WaitForPendingOpen))
					{
						IsBackground = true
					}.Start();
				}
				this.ReclaimEmancipatedObjects();
				if (!this.ErrorOccurred && this.NeedToReplenish)
				{
					if (this.UsingIntegrateSecurity && !this._identity.Equals(DbConnectionPoolIdentity.GetCurrent()))
					{
						return;
					}
					int num = 3;
					try
					{
						try
						{
						}
						finally
						{
							num = WaitHandle.WaitAny(this._waitHandles.GetHandles(true), this.CreationTimeout);
						}
						if (2 == num)
						{
							if (!this.ErrorOccurred)
							{
								while (this.NeedToReplenish)
								{
									DbConnectionInternal dbConnectionInternal;
									try
									{
										dbConnectionInternal = this.CreateObject(null, null, null);
									}
									catch
									{
										break;
									}
									if (dbConnectionInternal == null)
									{
										break;
									}
									this.PutNewObject(dbConnectionInternal);
								}
							}
						}
						else if (258 == num)
						{
							this.QueuePoolCreateRequest();
						}
					}
					finally
					{
						if (2 == num)
						{
							this._waitHandles.CreationSemaphore.Release(1);
						}
					}
				}
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000A42A4 File Offset: 0x000A24A4
		internal void PutNewObject(DbConnectionInternal obj)
		{
			this._stackNew.Push(obj);
			this._waitHandles.PoolSemaphore.Release(1);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000A42C4 File Offset: 0x000A24C4
		internal void PutObject(DbConnectionInternal obj, object owningObject)
		{
			lock (obj)
			{
				obj.PrePush(owningObject);
			}
			this.DeactivateObject(obj);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000A4308 File Offset: 0x000A2508
		internal void PutObjectFromTransactedPool(DbConnectionInternal obj)
		{
			if (this._state == DbConnectionPool.State.Running && obj.CanBePooled)
			{
				this.PutNewObject(obj);
				return;
			}
			this.DestroyObject(obj);
			this.QueuePoolCreateRequest();
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000A4330 File Offset: 0x000A2530
		private void QueuePoolCreateRequest()
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				ThreadPool.QueueUserWorkItem(this._poolCreateRequest);
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000A4348 File Offset: 0x000A2548
		private bool ReclaimEmancipatedObjects()
		{
			bool flag = false;
			List<DbConnectionInternal> list = new List<DbConnectionInternal>();
			List<DbConnectionInternal> objectList = this._objectList;
			int num;
			lock (objectList)
			{
				num = this._objectList.Count;
				for (int i = 0; i < num; i++)
				{
					DbConnectionInternal dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						bool flag3 = false;
						try
						{
							Monitor.TryEnter(dbConnectionInternal, ref flag3);
							if (flag3 && dbConnectionInternal.IsEmancipated)
							{
								dbConnectionInternal.PrePush(null);
								list.Add(dbConnectionInternal);
							}
						}
						finally
						{
							if (flag3)
							{
								Monitor.Exit(dbConnectionInternal);
							}
						}
					}
				}
			}
			num = list.Count;
			for (int j = 0; j < num; j++)
			{
				DbConnectionInternal dbConnectionInternal2 = list[j];
				flag = true;
				dbConnectionInternal2.DetachCurrentTransactionIfEnded();
				this.DeactivateObject(dbConnectionInternal2);
			}
			return flag;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000A4434 File Offset: 0x000A2634
		internal void Startup()
		{
			this._cleanupTimer = this.CreateCleanupTimer();
			if (this.NeedToReplenish)
			{
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000A4450 File Offset: 0x000A2650
		internal void Shutdown()
		{
			this._state = DbConnectionPool.State.ShuttingDown;
			Timer cleanupTimer = this._cleanupTimer;
			this._cleanupTimer = null;
			if (cleanupTimer != null)
			{
				cleanupTimer.Dispose();
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000A447C File Offset: 0x000A267C
		internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
		{
			DbConnectionPool.TransactedConnectionPool transactedConnectionPool = this._transactedConnectionPool;
			if (transactedConnectionPool != null)
			{
				transactedConnectionPool.TransactionEnded(transaction, transactedObject);
			}
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000A449C File Offset: 0x000A269C
		private DbConnectionInternal UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection = null)
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (this.ErrorOccurred)
			{
				throw this.TryCloneCachedException();
			}
			if ((oldConnection != null || this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && (oldConnection != null || (this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				dbConnectionInternal = this.CreateObject(owningObject, userOptions, oldConnection);
			}
			return dbConnectionInternal;
		}

		// Token: 0x04001784 RID: 6020
		private const int MAX_Q_SIZE = 1048576;

		// Token: 0x04001785 RID: 6021
		private const int SEMAPHORE_HANDLE = 0;

		// Token: 0x04001786 RID: 6022
		private const int ERROR_HANDLE = 1;

		// Token: 0x04001787 RID: 6023
		private const int CREATION_HANDLE = 2;

		// Token: 0x04001788 RID: 6024
		private const int BOGUS_HANDLE = 3;

		// Token: 0x04001789 RID: 6025
		private const int ERROR_WAIT_DEFAULT = 5000;

		// Token: 0x0400178A RID: 6026
		private static readonly Random s_random = new Random(5101977);

		// Token: 0x0400178B RID: 6027
		private readonly int _cleanupWait;

		// Token: 0x0400178C RID: 6028
		private readonly DbConnectionPoolIdentity _identity;

		// Token: 0x0400178D RID: 6029
		private readonly DbConnectionFactory _connectionFactory;

		// Token: 0x0400178E RID: 6030
		private readonly DbConnectionPoolGroup _connectionPoolGroup;

		// Token: 0x0400178F RID: 6031
		private readonly DbConnectionPoolGroupOptions _connectionPoolGroupOptions;

		// Token: 0x04001790 RID: 6032
		private DbConnectionPoolProviderInfo _connectionPoolProviderInfo;

		// Token: 0x04001791 RID: 6033
		private DbConnectionPool.State _state;

		// Token: 0x04001792 RID: 6034
		private readonly ConcurrentStack<DbConnectionInternal> _stackOld = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04001793 RID: 6035
		private readonly ConcurrentStack<DbConnectionInternal> _stackNew = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04001794 RID: 6036
		private readonly ConcurrentQueue<DbConnectionPool.PendingGetConnection> _pendingOpens = new ConcurrentQueue<DbConnectionPool.PendingGetConnection>();

		// Token: 0x04001795 RID: 6037
		private int _pendingOpensWaiting;

		// Token: 0x04001796 RID: 6038
		private readonly WaitCallback _poolCreateRequest;

		// Token: 0x04001797 RID: 6039
		private int _waitCount;

		// Token: 0x04001798 RID: 6040
		private readonly DbConnectionPool.PoolWaitHandles _waitHandles;

		// Token: 0x04001799 RID: 6041
		private Exception _resError;

		// Token: 0x0400179A RID: 6042
		private volatile bool _errorOccurred;

		// Token: 0x0400179B RID: 6043
		private int _errorWait;

		// Token: 0x0400179C RID: 6044
		private Timer _errorTimer;

		// Token: 0x0400179D RID: 6045
		private Timer _cleanupTimer;

		// Token: 0x0400179E RID: 6046
		private readonly DbConnectionPool.TransactedConnectionPool _transactedConnectionPool;

		// Token: 0x0400179F RID: 6047
		private readonly List<DbConnectionInternal> _objectList;

		// Token: 0x040017A0 RID: 6048
		private int _totalObjects;

		// Token: 0x0200030A RID: 778
		private enum State
		{
			// Token: 0x040017A2 RID: 6050
			Initializing,
			// Token: 0x040017A3 RID: 6051
			Running,
			// Token: 0x040017A4 RID: 6052
			ShuttingDown
		}

		// Token: 0x0200030B RID: 779
		private sealed class TransactedConnectionList : List<DbConnectionInternal>
		{
			// Token: 0x06002392 RID: 9106 RVA: 0x000A4505 File Offset: 0x000A2705
			internal TransactedConnectionList(int initialAllocation, Transaction tx)
				: base(initialAllocation)
			{
				this._transaction = tx;
			}

			// Token: 0x06002393 RID: 9107 RVA: 0x000A4515 File Offset: 0x000A2715
			internal void Dispose()
			{
				if (null != this._transaction)
				{
					this._transaction.Dispose();
				}
			}

			// Token: 0x040017A5 RID: 6053
			private Transaction _transaction;
		}

		// Token: 0x0200030C RID: 780
		private sealed class PendingGetConnection
		{
			// Token: 0x06002394 RID: 9108 RVA: 0x000A4530 File Offset: 0x000A2730
			public PendingGetConnection(long dueTime, DbConnection owner, TaskCompletionSource<DbConnectionInternal> completion, DbConnectionOptions userOptions)
			{
				this.DueTime = dueTime;
				this.Owner = owner;
				this.Completion = completion;
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x06002395 RID: 9109 RVA: 0x000A454D File Offset: 0x000A274D
			// (set) Token: 0x06002396 RID: 9110 RVA: 0x000A4555 File Offset: 0x000A2755
			public long DueTime { get; private set; }

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06002397 RID: 9111 RVA: 0x000A455E File Offset: 0x000A275E
			// (set) Token: 0x06002398 RID: 9112 RVA: 0x000A4566 File Offset: 0x000A2766
			public DbConnection Owner { get; private set; }

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06002399 RID: 9113 RVA: 0x000A456F File Offset: 0x000A276F
			// (set) Token: 0x0600239A RID: 9114 RVA: 0x000A4577 File Offset: 0x000A2777
			public TaskCompletionSource<DbConnectionInternal> Completion { get; private set; }

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x0600239B RID: 9115 RVA: 0x000A4580 File Offset: 0x000A2780
			// (set) Token: 0x0600239C RID: 9116 RVA: 0x000A4588 File Offset: 0x000A2788
			public DbConnectionOptions UserOptions { get; private set; }
		}

		// Token: 0x0200030D RID: 781
		private sealed class TransactedConnectionPool
		{
			// Token: 0x0600239D RID: 9117 RVA: 0x000A4591 File Offset: 0x000A2791
			internal TransactedConnectionPool(DbConnectionPool pool)
			{
				this._pool = pool;
				this._transactedCxns = new Dictionary<Transaction, DbConnectionPool.TransactedConnectionList>();
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x0600239E RID: 9118 RVA: 0x000A45BB File Offset: 0x000A27BB
			internal int ObjectID
			{
				get
				{
					return this._objectID;
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x0600239F RID: 9119 RVA: 0x000A45C3 File Offset: 0x000A27C3
			internal DbConnectionPool Pool
			{
				get
				{
					return this._pool;
				}
			}

			// Token: 0x060023A0 RID: 9120 RVA: 0x000A45CC File Offset: 0x000A27CC
			internal DbConnectionInternal GetTransactedObject(Transaction transaction)
			{
				DbConnectionInternal dbConnectionInternal = null;
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				DbConnectionPool.TransactedConnectionList transactedConnectionList;
				lock (transactedCxns)
				{
					flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList);
				}
				if (flag)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
					lock (transactedConnectionList2)
					{
						int num = transactedConnectionList.Count - 1;
						if (0 <= num)
						{
							dbConnectionInternal = transactedConnectionList[num];
							transactedConnectionList.RemoveAt(num);
						}
					}
				}
				return dbConnectionInternal;
			}

			// Token: 0x060023A1 RID: 9121 RVA: 0x000A4668 File Offset: 0x000A2868
			internal void PutTransactedObject(Transaction transaction, DbConnectionInternal transactedObject)
			{
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> dictionary = this._transactedCxns;
				lock (dictionary)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
						lock (transactedConnectionList2)
						{
							transactedConnectionList.Add(transactedObject);
						}
					}
				}
				if (!flag)
				{
					Transaction transaction2 = null;
					DbConnectionPool.TransactedConnectionList transactedConnectionList3 = null;
					try
					{
						transaction2 = transaction.Clone();
						transactedConnectionList3 = new DbConnectionPool.TransactedConnectionList(2, transaction2);
						dictionary = this._transactedCxns;
						lock (dictionary)
						{
							DbConnectionPool.TransactedConnectionList transactedConnectionList;
							if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
							{
								DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
								lock (transactedConnectionList2)
								{
									transactedConnectionList.Add(transactedObject);
									return;
								}
							}
							transactedConnectionList3.Add(transactedObject);
							this._transactedCxns.Add(transaction2, transactedConnectionList3);
							transaction2 = null;
						}
					}
					finally
					{
						if (null != transaction2)
						{
							if (transactedConnectionList3 != null)
							{
								transactedConnectionList3.Dispose();
							}
							else
							{
								transaction2.Dispose();
							}
						}
					}
				}
			}

			// Token: 0x060023A2 RID: 9122 RVA: 0x000A47B4 File Offset: 0x000A29B4
			internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
			{
				int num = -1;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						bool flag2 = false;
						DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
						lock (transactedConnectionList2)
						{
							num = transactedConnectionList.IndexOf(transactedObject);
							if (num >= 0)
							{
								transactedConnectionList.RemoveAt(num);
							}
							if (0 >= transactedConnectionList.Count)
							{
								this._transactedCxns.Remove(transaction);
								flag2 = true;
							}
						}
						if (flag2)
						{
							transactedConnectionList.Dispose();
						}
					}
				}
				if (0 <= num)
				{
					this.Pool.PutObjectFromTransactedPool(transactedObject);
				}
			}

			// Token: 0x040017AA RID: 6058
			private Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> _transactedCxns;

			// Token: 0x040017AB RID: 6059
			private DbConnectionPool _pool;

			// Token: 0x040017AC RID: 6060
			private static int _objectTypeCount;

			// Token: 0x040017AD RID: 6061
			internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool.TransactedConnectionPool._objectTypeCount);
		}

		// Token: 0x0200030E RID: 782
		private sealed class PoolWaitHandles
		{
			// Token: 0x060023A3 RID: 9123 RVA: 0x000A4874 File Offset: 0x000A2A74
			internal PoolWaitHandles()
			{
				this._poolSemaphore = new Semaphore(0, 1048576);
				this._errorEvent = new ManualResetEvent(false);
				this._creationSemaphore = new Semaphore(1, 1);
				this._handlesWithCreate = new WaitHandle[] { this._poolSemaphore, this._errorEvent, this._creationSemaphore };
				this._handlesWithoutCreate = new WaitHandle[] { this._poolSemaphore, this._errorEvent };
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x060023A4 RID: 9124 RVA: 0x000A48F6 File Offset: 0x000A2AF6
			internal Semaphore CreationSemaphore
			{
				get
				{
					return this._creationSemaphore;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060023A5 RID: 9125 RVA: 0x000A48FE File Offset: 0x000A2AFE
			internal ManualResetEvent ErrorEvent
			{
				get
				{
					return this._errorEvent;
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000A4906 File Offset: 0x000A2B06
			internal Semaphore PoolSemaphore
			{
				get
				{
					return this._poolSemaphore;
				}
			}

			// Token: 0x060023A7 RID: 9127 RVA: 0x000A490E File Offset: 0x000A2B0E
			internal WaitHandle[] GetHandles(bool withCreate)
			{
				if (!withCreate)
				{
					return this._handlesWithoutCreate;
				}
				return this._handlesWithCreate;
			}

			// Token: 0x040017AE RID: 6062
			private readonly Semaphore _poolSemaphore;

			// Token: 0x040017AF RID: 6063
			private readonly ManualResetEvent _errorEvent;

			// Token: 0x040017B0 RID: 6064
			private readonly Semaphore _creationSemaphore;

			// Token: 0x040017B1 RID: 6065
			private readonly WaitHandle[] _handlesWithCreate;

			// Token: 0x040017B2 RID: 6066
			private readonly WaitHandle[] _handlesWithoutCreate;
		}
	}
}
