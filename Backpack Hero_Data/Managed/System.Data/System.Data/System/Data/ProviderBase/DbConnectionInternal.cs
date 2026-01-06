using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FE RID: 766
	internal abstract class DbConnectionInternal
	{
		// Token: 0x06002297 RID: 8855 RVA: 0x0009F69F File Offset: 0x0009D89F
		protected DbConnectionInternal()
			: this(ConnectionState.Open, true, false)
		{
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0009F6AA File Offset: 0x0009D8AA
		internal DbConnectionInternal(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
		{
			this._allowSetConnectionString = allowSetConnectionString;
			this._hidePassword = hidePassword;
			this._state = state;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0009F6D4 File Offset: 0x0009D8D4
		internal bool AllowSetConnectionString
		{
			get
			{
				return this._allowSetConnectionString;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0009F6DC File Offset: 0x0009D8DC
		internal bool CanBePooled
		{
			get
			{
				return !this._connectionIsDoomed && !this._cannotBePooled && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x0009F6FE File Offset: 0x0009D8FE
		protected internal bool IsConnectionDoomed
		{
			get
			{
				return this._connectionIsDoomed;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0009F706 File Offset: 0x0009D906
		internal bool IsEmancipated
		{
			get
			{
				return this._pooledCount < 1 && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0009F721 File Offset: 0x0009D921
		internal bool IsInPool
		{
			get
			{
				return this._pooledCount == 1;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0009F72C File Offset: 0x0009D92C
		protected internal object Owner
		{
			get
			{
				return this._owningObject.Target;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0009F739 File Offset: 0x0009D939
		internal DbConnectionPool Pool
		{
			get
			{
				return this._connectionPool;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0009F741 File Offset: 0x0009D941
		protected internal DbReferenceCollection ReferenceCollection
		{
			get
			{
				return this._referenceCollection;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060022A1 RID: 8865
		public abstract string ServerVersion { get; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual string ServerVersionNormalized
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x0009F749 File Offset: 0x0009D949
		public bool ShouldHidePassword
		{
			get
			{
				return this._hidePassword;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0009F751 File Offset: 0x0009D951
		public ConnectionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0009F759 File Offset: 0x0009D959
		internal void AddWeakReference(object value, int tag)
		{
			if (this._referenceCollection == null)
			{
				this._referenceCollection = this.CreateReferenceCollection();
				if (this._referenceCollection == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateReferenceCollectionReturnedNull);
				}
			}
			this._referenceCollection.Add(value, tag);
		}

		// Token: 0x060022A6 RID: 8870
		public abstract DbTransaction BeginTransaction(IsolationLevel il);

		// Token: 0x060022A7 RID: 8871 RVA: 0x0009F78C File Offset: 0x0009D98C
		public virtual void ChangeDatabase(string value)
		{
			throw ADP.MethodNotImplemented("ChangeDatabase");
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000094D4 File Offset: 0x000076D4
		internal virtual void PrepareForReplaceConnection()
		{
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void PrepareForCloseConnection()
		{
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected virtual object ObtainAdditionalLocksForClose()
		{
			return null;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void ReleaseAdditionalLocksForClose(object lockToken)
		{
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0009F798 File Offset: 0x0009D998
		protected virtual DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToConstructReferenceCollectionOnStaticObject);
		}

		// Token: 0x060022AD RID: 8877
		protected abstract void Deactivate();

		// Token: 0x060022AE RID: 8878 RVA: 0x0009F7A4 File Offset: 0x0009D9A4
		internal void DeactivateConnection()
		{
			if (!this._connectionIsDoomed && this.Pool.UseLoadBalancing && DateTime.UtcNow.Ticks - this._createTime.Ticks > this.Pool.LoadBalanceTimeout.Ticks)
			{
				this.DoNotPoolThisConnection();
			}
			this.Deactivate();
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0009F800 File Offset: 0x0009DA00
		protected internal void DoNotPoolThisConnection()
		{
			this._cannotBePooled = true;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0009F809 File Offset: 0x0009DA09
		protected internal void DoomThisConnection()
		{
			this._connectionIsDoomed = true;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0009F812 File Offset: 0x0009DA12
		protected internal virtual DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			return factory.GetMetaDataFactory(poolGroup, this).GetSchema(outerConnection, collectionName, restrictions);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0009F826 File Offset: 0x0009DA26
		internal void MakeNonPooledObject(object owningObject)
		{
			this._connectionPool = null;
			this._owningObject.Target = owningObject;
			this._pooledCount = -1;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0009F842 File Offset: 0x0009DA42
		internal void MakePooledConnection(DbConnectionPool connectionPool)
		{
			this._createTime = DateTime.UtcNow;
			this._connectionPool = connectionPool;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0009F858 File Offset: 0x0009DA58
		internal void NotifyWeakReference(int message)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Notify(message);
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0009F876 File Offset: 0x0009DA76
		internal virtual void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			if (!this.TryOpenConnection(outerConnection, connectionFactory, null, null))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0009EBDC File Offset: 0x0009CDDC
		internal virtual bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(this.State);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0009F88C File Offset: 0x0009DA8C
		internal virtual bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.MethodNotImplemented("TryReplaceConnection");
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0009F898 File Offset: 0x0009DA98
		protected bool TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (connectionFactory.SetInnerConnectionFrom(outerConnection, DbConnectionClosedConnecting.SingletonInstance, this))
			{
				DbConnectionInternal dbConnectionInternal = null;
				try
				{
					connectionFactory.PermissionDemand(outerConnection);
					if (!connectionFactory.TryGetConnection(outerConnection, retry, userOptions, this, out dbConnectionInternal))
					{
						return false;
					}
				}
				catch
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw;
				}
				if (dbConnectionInternal == null)
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
				}
				connectionFactory.SetInnerConnectionEvent(outerConnection, dbConnectionInternal);
				return true;
			}
			return true;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0009F90C File Offset: 0x0009DB0C
		internal void PrePush(object expectedOwner)
		{
			if (expectedOwner == null)
			{
				if (this._owningObject.Target != null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasOwner);
				}
			}
			else if (this._owningObject.Target != expectedOwner)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
			if (this._pooledCount != 0)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PushingObjectSecondTime);
			}
			this._pooledCount++;
			this._owningObject.Target = null;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0009F970 File Offset: 0x0009DB70
		internal void PostPop(object newOwner)
		{
			if (this._owningObject.Target != null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectHasOwner);
			}
			this._owningObject.Target = newOwner;
			this._pooledCount--;
			if (this.Pool != null)
			{
				if (this._pooledCount != 0)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectInPoolMoreThanOnce);
				}
			}
			else if (-1 != this._pooledCount)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.NonPooledObjectUsedMoreThanOnce);
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
		internal void RemoveWeakReference(object value)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Remove(value);
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal virtual bool IsConnectionAlive(bool throwOnException = false)
		{
			return true;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0009F9F2 File Offset: 0x0009DBF2
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x0009F9FC File Offset: 0x0009DBFC
		protected internal Transaction EnlistedTransaction
		{
			get
			{
				return this._enlistedTransaction;
			}
			set
			{
				Transaction enlistedTransaction = this._enlistedTransaction;
				if ((null == enlistedTransaction && null != value) || (null != enlistedTransaction && !enlistedTransaction.Equals(value)))
				{
					Transaction transaction = null;
					Transaction transaction2 = null;
					try
					{
						if (null != value)
						{
							transaction = value.Clone();
						}
						lock (this)
						{
							transaction2 = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, transaction);
							this._enlistedTransactionOriginal = value;
							value = transaction;
							transaction = null;
						}
					}
					finally
					{
						if (null != transaction2 && transaction2 != this._enlistedTransaction)
						{
							transaction2.Dispose();
						}
						if (null != transaction && transaction != this._enlistedTransaction)
						{
							transaction.Dispose();
						}
					}
					if (null != value)
					{
						this.TransactionOutcomeEnlist(value);
					}
				}
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0009FAE0 File Offset: 0x0009DCE0
		protected bool EnlistedTransactionDisposed
		{
			get
			{
				bool flag2;
				try
				{
					Transaction enlistedTransactionOriginal = this._enlistedTransactionOriginal;
					bool flag = enlistedTransactionOriginal != null && enlistedTransactionOriginal.TransactionInformation == null;
					flag2 = flag;
				}
				catch (ObjectDisposedException)
				{
					flag2 = true;
				}
				return flag2;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0009FB28 File Offset: 0x0009DD28
		internal bool IsTxRootWaitingForTxEnd
		{
			get
			{
				return this._isInStasis;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0000CD07 File Offset: 0x0000AF07
		protected virtual bool UnbindOnTransactionCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x00005AE9 File Offset: 0x00003CE9
		protected internal virtual bool IsNonPoolableTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal virtual bool IsTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0000CD07 File Offset: 0x0000AF07
		protected virtual bool ReadyToPrepareTransaction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060022C5 RID: 8901
		protected abstract void Activate(Transaction transaction);

		// Token: 0x060022C6 RID: 8902 RVA: 0x0009FB30 File Offset: 0x0009DD30
		internal void ActivateConnection(Transaction transaction)
		{
			this.Activate(transaction);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0009FB3C File Offset: 0x0009DD3C
		internal virtual void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			if (connectionFactory.SetInnerConnectionFrom(owningObject, DbConnectionOpenBusy.SingletonInstance, this))
			{
				lock (this)
				{
					object obj = this.ObtainAdditionalLocksForClose();
					try
					{
						this.PrepareForCloseConnection();
						DbConnectionPool pool = this.Pool;
						this.DetachCurrentTransactionIfEnded();
						if (pool != null)
						{
							pool.PutObject(this, owningObject);
						}
						else
						{
							this.Deactivate();
							this._owningObject.Target = null;
							if (this.IsTransactionRoot)
							{
								this.SetInStasis();
							}
							else
							{
								this.Dispose();
							}
						}
					}
					finally
					{
						this.ReleaseAdditionalLocksForClose(obj);
						connectionFactory.SetInnerConnectionEvent(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
					}
				}
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0009FBF0 File Offset: 0x0009DDF0
		internal virtual void DelegatedTransactionEnded()
		{
			if (1 != this._pooledCount)
			{
				if (-1 == this._pooledCount && !this._owningObject.IsAlive)
				{
					this.TerminateStasis(false);
					this.Deactivate();
					this.Dispose();
				}
				return;
			}
			this.TerminateStasis(true);
			this.Deactivate();
			DbConnectionPool pool = this.Pool;
			if (pool == null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectWithoutPool);
			}
			pool.PutObjectFromTransactedPool(this);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0009FC58 File Offset: 0x0009DE58
		public virtual void Dispose()
		{
			this._connectionPool = null;
			this._connectionIsDoomed = true;
			this._enlistedTransactionOriginal = null;
			Transaction transaction = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, null);
			if (transaction != null)
			{
				transaction.Dispose();
			}
		}

		// Token: 0x060022CA RID: 8906
		public abstract void EnlistTransaction(Transaction transaction);

		// Token: 0x060022CB RID: 8907 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void CleanupTransactionOnCompletion(Transaction transaction)
		{
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0009FC98 File Offset: 0x0009DE98
		internal void DetachCurrentTransactionIfEnded()
		{
			Transaction enlistedTransaction = this.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				bool flag;
				try
				{
					flag = enlistedTransaction.TransactionInformation.Status > TransactionStatus.Active;
				}
				catch (TransactionException)
				{
					flag = true;
				}
				if (flag)
				{
					this.DetachTransaction(enlistedTransaction, true);
				}
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0009FCE8 File Offset: 0x0009DEE8
		internal void DetachTransaction(Transaction transaction, bool isExplicitlyReleasing)
		{
			lock (this)
			{
				DbConnection dbConnection = (DbConnection)this.Owner;
				if (isExplicitlyReleasing || this.UnbindOnTransactionCompletion || dbConnection == null)
				{
					Transaction enlistedTransaction = this._enlistedTransaction;
					if (enlistedTransaction != null && transaction.Equals(enlistedTransaction))
					{
						this.EnlistedTransaction = null;
						if (this.IsTxRootWaitingForTxEnd)
						{
							this.DelegatedTransactionEnded();
						}
					}
				}
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0009FD68 File Offset: 0x0009DF68
		internal void CleanupConnectionOnTransactionCompletion(Transaction transaction)
		{
			this.DetachTransaction(transaction, false);
			DbConnectionPool pool = this.Pool;
			if (pool != null)
			{
				pool.TransactionEnded(transaction, this);
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0009FD90 File Offset: 0x0009DF90
		private void TransactionCompletedEvent(object sender, TransactionEventArgs e)
		{
			Transaction transaction = e.Transaction;
			this.CleanupTransactionOnCompletion(transaction);
			this.CleanupConnectionOnTransactionCompletion(transaction);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0009FDB2 File Offset: 0x0009DFB2
		private void TransactionOutcomeEnlist(Transaction transaction)
		{
			transaction.TransactionCompleted += this.TransactionCompletedEvent;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x0009FDC6 File Offset: 0x0009DFC6
		internal void SetInStasis()
		{
			this._isInStasis = true;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0009FDCF File Offset: 0x0009DFCF
		private void TerminateStasis(bool returningToPool)
		{
			this._isInStasis = false;
		}

		// Token: 0x04001733 RID: 5939
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x04001734 RID: 5940
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x04001735 RID: 5941
		private readonly bool _allowSetConnectionString;

		// Token: 0x04001736 RID: 5942
		private readonly bool _hidePassword;

		// Token: 0x04001737 RID: 5943
		private readonly ConnectionState _state;

		// Token: 0x04001738 RID: 5944
		private readonly WeakReference _owningObject = new WeakReference(null, false);

		// Token: 0x04001739 RID: 5945
		private DbConnectionPool _connectionPool;

		// Token: 0x0400173A RID: 5946
		private DbReferenceCollection _referenceCollection;

		// Token: 0x0400173B RID: 5947
		private int _pooledCount;

		// Token: 0x0400173C RID: 5948
		private bool _connectionIsDoomed;

		// Token: 0x0400173D RID: 5949
		private bool _cannotBePooled;

		// Token: 0x0400173E RID: 5950
		private DateTime _createTime;

		// Token: 0x0400173F RID: 5951
		private bool _isInStasis;

		// Token: 0x04001740 RID: 5952
		private Transaction _enlistedTransaction;

		// Token: 0x04001741 RID: 5953
		private Transaction _enlistedTransactionOriginal;
	}
}
