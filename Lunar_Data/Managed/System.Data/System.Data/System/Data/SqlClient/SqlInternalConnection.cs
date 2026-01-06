using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001B4 RID: 436
	internal abstract class SqlInternalConnection : DbConnectionInternal
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00068AAD File Offset: 0x00066CAD
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00068AB5 File Offset: 0x00066CB5
		internal string CurrentDatabase { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00068ABE File Offset: 0x00066CBE
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00068AC6 File Offset: 0x00066CC6
		internal string CurrentDataSource { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00068ACF File Offset: 0x00066CCF
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00068AD7 File Offset: 0x00066CD7
		internal SqlDelegatedTransaction DelegatedTransaction { get; set; }

		// Token: 0x06001513 RID: 5395 RVA: 0x00068AE0 File Offset: 0x00066CE0
		internal SqlInternalConnection(SqlConnectionString connectionOptions)
		{
			this._connectionOptions = connectionOptions;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00068AEF File Offset: 0x00066CEF
		internal SqlConnection Connection
		{
			get
			{
				return (SqlConnection)base.Owner;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00068AFC File Offset: 0x00066CFC
		internal SqlConnectionString ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001516 RID: 5398
		internal abstract SqlInternalTransaction CurrentTransaction { get; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00068B04 File Offset: 0x00066D04
		internal virtual SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001518 RID: 5400
		internal abstract SqlInternalTransaction PendingTransaction { get; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00068B0C File Offset: 0x00066D0C
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00068B14 File Offset: 0x00066D14
		internal override bool IsTransactionRoot
		{
			get
			{
				SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
				return delegatedTransaction != null && delegatedTransaction.IsActive;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00068B34 File Offset: 0x00066D34
		internal bool HasLocalTransaction
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.IsLocal;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00068B54 File Offset: 0x00066D54
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.HasParentTransaction;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00068B73 File Offset: 0x00066D73
		internal bool IsEnlistedInTransaction
		{
			get
			{
				return this._isEnlistedInTransaction;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600151E RID: 5406
		internal abstract bool IsLockedForBulkCopy { get; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600151F RID: 5407
		internal abstract bool IsKatmaiOrNewer { get; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00068B7B File Offset: 0x00066D7B
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x00068B83 File Offset: 0x00066D83
		internal byte[] PromotedDTCToken
		{
			get
			{
				return this._promotedDTCToken;
			}
			set
			{
				this._promotedDTCToken = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00068B8C File Offset: 0x00066D8C
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x00068B94 File Offset: 0x00066D94
		internal bool IsGlobalTransaction
		{
			get
			{
				return this._isGlobalTransaction;
			}
			set
			{
				this._isGlobalTransaction = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00068B9D File Offset: 0x00066D9D
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x00068BA5 File Offset: 0x00066DA5
		internal bool IsGlobalTransactionsEnabledForServer
		{
			get
			{
				return this._isGlobalTransactionEnabledForServer;
			}
			set
			{
				this._isGlobalTransactionEnabledForServer = value;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00068BAE File Offset: 0x00066DAE
		public override DbTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginSqlTransaction(iso, null, false);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00068BBC File Offset: 0x00066DBC
		internal virtual SqlTransaction BeginSqlTransaction(IsolationLevel iso, string transactionName, bool shouldReconnect)
		{
			SqlStatistics sqlStatistics = null;
			SqlTransaction sqlTransaction2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Connection.Statistics);
				this.ValidateConnectionForExecute(null);
				if (this.HasLocalTransactionFromAPI)
				{
					throw ADP.ParallelTransactionsNotSupported(this.Connection);
				}
				if (iso == IsolationLevel.Unspecified)
				{
					iso = IsolationLevel.ReadCommitted;
				}
				SqlTransaction sqlTransaction = new SqlTransaction(this, this.Connection, iso, this.AvailableInternalTransaction);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = shouldReconnect;
				this.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, transactionName, iso, sqlTransaction.InternalTransaction, false);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = false;
				sqlTransaction2 = sqlTransaction;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlTransaction2;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00068C5C File Offset: 0x00066E5C
		public override void ChangeDatabase(string database)
		{
			if (string.IsNullOrEmpty(database))
			{
				throw ADP.EmptyDatabaseName();
			}
			this.ValidateConnectionForExecute(null);
			this.ChangeDatabaseInternal(database);
		}

		// Token: 0x06001529 RID: 5417
		protected abstract void ChangeDatabaseInternal(string database);

		// Token: 0x0600152A RID: 5418 RVA: 0x00068C7C File Offset: 0x00066E7C
		protected override void CleanupTransactionOnCompletion(Transaction transaction)
		{
			SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.TransactionEnded(transaction);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00068C9A File Offset: 0x00066E9A
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new SqlReferenceCollection();
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00068CA4 File Offset: 0x00066EA4
		protected override void Deactivate()
		{
			try
			{
				SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
				if (sqlReferenceCollection != null)
				{
					sqlReferenceCollection.Deactivate();
				}
				this.InternalDeactivate();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				base.DoomThisConnection();
			}
		}

		// Token: 0x0600152D RID: 5421
		internal abstract void DisconnectTransaction(SqlInternalTransaction internalTransaction);

		// Token: 0x0600152E RID: 5422 RVA: 0x00068CF0 File Offset: 0x00066EF0
		public override void Dispose()
		{
			this._whereAbouts = null;
			base.Dispose();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00068D00 File Offset: 0x00066F00
		protected void Enlist(Transaction tx)
		{
			if (null == tx)
			{
				if (this.IsEnlistedInTransaction)
				{
					this.EnlistNull();
					return;
				}
				Transaction enlistedTransaction = base.EnlistedTransaction;
				if (enlistedTransaction != null && enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					this.EnlistNull();
					return;
				}
			}
			else if (!tx.Equals(base.EnlistedTransaction))
			{
				this.EnlistNonNull(tx);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00068D60 File Offset: 0x00066F60
		private void EnlistNonNull(Transaction tx)
		{
			bool flag = false;
			SqlDelegatedTransaction sqlDelegatedTransaction = new SqlDelegatedTransaction(this, tx);
			try
			{
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.EnlistPromotableSinglePhase == null)
					{
						flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
					}
					else
					{
						flag = (bool)SysTxForGlobalTransactions.EnlistPromotableSinglePhase.Invoke(tx, new object[]
						{
							sqlDelegatedTransaction,
							SqlInternalConnection._globalTransactionTMID
						});
					}
				}
				else
				{
					flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
				}
				if (flag)
				{
					this.DelegatedTransaction = sqlDelegatedTransaction;
				}
			}
			catch (SqlException ex)
			{
				if (ex.Class >= 20)
				{
					throw;
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser == null || parser.State != TdsParserState.OpenLoggedIn)
					{
						throw;
					}
				}
			}
			if (!flag)
			{
				byte[] array;
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.GetPromotedToken == null)
					{
						throw SQL.UnsupportedSysTxForGlobalTransactions();
					}
					array = (byte[])SysTxForGlobalTransactions.GetPromotedToken.Invoke(tx, null);
				}
				else
				{
					if (this._whereAbouts == null)
					{
						byte[] dtcaddress = this.GetDTCAddress();
						if (dtcaddress == null)
						{
							throw SQL.CannotGetDTCAddress();
						}
						this._whereAbouts = dtcaddress;
					}
					array = SqlInternalConnection.GetTransactionCookie(tx, this._whereAbouts);
				}
				this.PropagateTransactionCookie(array);
				this._isEnlistedInTransaction = true;
			}
			base.EnlistedTransaction = tx;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00068E8C File Offset: 0x0006708C
		internal void EnlistNull()
		{
			this.PropagateTransactionCookie(null);
			this._isEnlistedInTransaction = false;
			base.EnlistedTransaction = null;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00068EA4 File Offset: 0x000670A4
		public override void EnlistTransaction(Transaction transaction)
		{
			this.ValidateConnectionForExecute(null);
			if (this.HasLocalTransaction)
			{
				throw ADP.LocalTransactionPresent();
			}
			if (null != transaction && transaction.Equals(base.EnlistedTransaction))
			{
				return;
			}
			try
			{
				this.Enlist(transaction);
			}
			catch (OutOfMemoryException ex)
			{
				this.Connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this.Connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this.Connection.Abort(ex3);
				throw;
			}
		}

		// Token: 0x06001533 RID: 5427
		internal abstract void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest);

		// Token: 0x06001534 RID: 5428 RVA: 0x00068F40 File Offset: 0x00067140
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			SqlDataReader sqlDataReader = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				sqlDataReader = sqlReferenceCollection.FindLiveReader(command);
			}
			return sqlDataReader;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00068F68 File Offset: 0x00067168
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			SqlCommand sqlCommand = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				sqlCommand = sqlReferenceCollection.FindLiveCommand(stateObj);
			}
			return sqlCommand;
		}

		// Token: 0x06001536 RID: 5430
		protected abstract byte[] GetDTCAddress();

		// Token: 0x06001537 RID: 5431 RVA: 0x00068F90 File Offset: 0x00067190
		private static byte[] GetTransactionCookie(Transaction transaction, byte[] whereAbouts)
		{
			byte[] array = null;
			if (null != transaction)
			{
				array = TransactionInterop.GetExportCookie(transaction, whereAbouts);
			}
			return array;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void InternalDeactivate()
		{
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00068FB4 File Offset: 0x000671B4
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction = null)
		{
			if (breakConnection)
			{
				base.DoomThisConnection();
			}
			SqlConnection connection = this.Connection;
			if (connection != null)
			{
				connection.OnError(exception, breakConnection, wrapCloseInAction);
				return;
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
		}

		// Token: 0x0600153A RID: 5434
		protected abstract void PropagateTransactionCookie(byte[] transactionCookie);

		// Token: 0x0600153B RID: 5435
		internal abstract void ValidateConnectionForExecute(SqlCommand command);

		// Token: 0x04000E2E RID: 3630
		private readonly SqlConnectionString _connectionOptions;

		// Token: 0x04000E2F RID: 3631
		private bool _isEnlistedInTransaction;

		// Token: 0x04000E30 RID: 3632
		private byte[] _promotedDTCToken;

		// Token: 0x04000E31 RID: 3633
		private byte[] _whereAbouts;

		// Token: 0x04000E32 RID: 3634
		private bool _isGlobalTransaction;

		// Token: 0x04000E33 RID: 3635
		private bool _isGlobalTransactionEnabledForServer;

		// Token: 0x04000E34 RID: 3636
		private static readonly Guid _globalTransactionTMID = new Guid("1c742caf-6680-40ea-9c26-6b6846079764");

		// Token: 0x020001B5 RID: 437
		internal enum TransactionRequest
		{
			// Token: 0x04000E39 RID: 3641
			Begin,
			// Token: 0x04000E3A RID: 3642
			Promote,
			// Token: 0x04000E3B RID: 3643
			Commit,
			// Token: 0x04000E3C RID: 3644
			Rollback,
			// Token: 0x04000E3D RID: 3645
			IfRollback,
			// Token: 0x04000E3E RID: 3646
			Save
		}
	}
}
