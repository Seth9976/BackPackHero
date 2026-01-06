using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001A4 RID: 420
	internal sealed class SqlDelegatedTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00064A66 File Offset: 0x00062C66
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00064A70 File Offset: 0x00062C70
		internal SqlDelegatedTransaction(SqlInternalConnection connection, Transaction tx)
		{
			this._connection = connection;
			this._atomicTransaction = tx;
			this._active = false;
			IsolationLevel isolationLevel = tx.IsolationLevel;
			switch (isolationLevel)
			{
			case IsolationLevel.Serializable:
				this._isolationLevel = IsolationLevel.Serializable;
				return;
			case IsolationLevel.RepeatableRead:
				this._isolationLevel = IsolationLevel.RepeatableRead;
				return;
			case IsolationLevel.ReadCommitted:
				this._isolationLevel = IsolationLevel.ReadCommitted;
				return;
			case IsolationLevel.ReadUncommitted:
				this._isolationLevel = IsolationLevel.ReadUncommitted;
				return;
			case IsolationLevel.Snapshot:
				this._isolationLevel = IsolationLevel.Snapshot;
				return;
			default:
				throw SQL.UnknownSysTxIsolationLevel(isolationLevel);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00064B0D File Offset: 0x00062D0D
		internal Transaction Transaction
		{
			get
			{
				return this._atomicTransaction;
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00064B18 File Offset: 0x00062D18
		public void Initialize()
		{
			SqlInternalConnection connection = this._connection;
			SqlConnection connection2 = connection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (connection.IsEnlistedInTransaction)
				{
					connection.EnlistNull();
				}
				this._internalTransaction = new SqlInternalTransaction(connection, TransactionType.Delegated, null);
				connection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, null, this._isolationLevel, this._internalTransaction, true);
				if (connection.CurrentTransaction == null)
				{
					connection.DoomThisConnection();
					throw ADP.InternalError(ADP.InternalErrorCode.UnknownTransactionFailure);
				}
				this._active = true;
			}
			catch (OutOfMemoryException ex)
			{
				connection2.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				connection2.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				connection2.Abort(ex3);
				throw;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00064BD0 File Offset: 0x00062DD0
		internal bool IsActive
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00064BD8 File Offset: 0x00062DD8
		public byte[] Promote()
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			byte[] array = null;
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			Exception ex;
			try
			{
				SqlInternalConnection sqlInternalConnection = validConnection;
				lock (sqlInternalConnection)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Promote, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						array = this._connection.PromotedDTCToken;
						if (this._connection.IsGlobalTransaction)
						{
							if (SysTxForGlobalTransactions.SetDistributedTransactionIdentifier == null)
							{
								throw SQL.UnsupportedSysTxForGlobalTransactions();
							}
							if (!this._connection.IsGlobalTransactionsEnabledForServer)
							{
								throw SQL.GlobalTransactionsNotEnabled();
							}
							SysTxForGlobalTransactions.SetDistributedTransactionIdentifier.Invoke(this._atomicTransaction, new object[]
							{
								this,
								this.GetGlobalTxnIdentifierFromToken()
							});
						}
						ex = null;
					}
					catch (SqlException ex)
					{
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException ex)
					{
						validConnection.DoomThisConnection();
					}
				}
			}
			catch (OutOfMemoryException ex2)
			{
				connection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				connection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				connection.Abort(ex4);
				throw;
			}
			if (ex != null)
			{
				throw SQL.PromotionFailed(ex);
			}
			return array;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00064D2C File Offset: 0x00062F2C
		public void Rollback(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SqlInternalConnection sqlInternalConnection = validConnection;
				lock (sqlInternalConnection)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						this._active = false;
						this._connection = null;
						if (!this._internalTransaction.IsAborted)
						{
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						}
					}
					catch (SqlException)
					{
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException)
					{
						validConnection.DoomThisConnection();
					}
				}
				validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
				enlistment.Aborted();
			}
			catch (OutOfMemoryException ex)
			{
				connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				connection.Abort(ex3);
				throw;
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00064E30 File Offset: 0x00063030
		public void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (validConnection.IsConnectionDoomed)
				{
					SqlInternalConnection sqlInternalConnection = validConnection;
					lock (sqlInternalConnection)
					{
						this._active = false;
						this._connection = null;
					}
					enlistment.Aborted(SQL.ConnectionDoomed());
				}
				else
				{
					SqlInternalConnection sqlInternalConnection = validConnection;
					Exception ex;
					lock (sqlInternalConnection)
					{
						try
						{
							this.ValidateActiveOnConnection(validConnection);
							this._active = false;
							this._connection = null;
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, this._internalTransaction, true);
							ex = null;
						}
						catch (SqlException ex)
						{
							validConnection.DoomThisConnection();
						}
						catch (InvalidOperationException ex)
						{
							validConnection.DoomThisConnection();
						}
					}
					if (ex != null)
					{
						if (this._internalTransaction.IsCommitted)
						{
							enlistment.Committed();
						}
						else if (this._internalTransaction.IsAborted)
						{
							enlistment.Aborted(ex);
						}
						else
						{
							enlistment.InDoubt(ex);
						}
					}
					validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
					if (ex == null)
					{
						enlistment.Committed();
					}
				}
			}
			catch (OutOfMemoryException ex2)
			{
				connection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				connection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				connection.Abort(ex4);
				throw;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00064FB0 File Offset: 0x000631B0
		internal void TransactionEnded(Transaction transaction)
		{
			SqlInternalConnection connection = this._connection;
			if (connection != null)
			{
				SqlInternalConnection sqlInternalConnection = connection;
				lock (sqlInternalConnection)
				{
					if (this._atomicTransaction.Equals(transaction))
					{
						this._active = false;
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0006500C File Offset: 0x0006320C
		private SqlInternalConnection GetValidConnection()
		{
			SqlInternalConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ObjectDisposed(this);
			}
			return connection;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0006502C File Offset: 0x0006322C
		private void ValidateActiveOnConnection(SqlInternalConnection connection)
		{
			if (!this._active || connection != this._connection || connection.DelegatedTransaction != this)
			{
				if (connection != null)
				{
					connection.DoomThisConnection();
				}
				if (connection != this._connection && this._connection != null)
				{
					this._connection.DoomThisConnection();
				}
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00065084 File Offset: 0x00063284
		private Guid GetGlobalTxnIdentifierFromToken()
		{
			byte[] array = new byte[16];
			Array.Copy(this._connection.PromotedDTCToken, 4, array, 0, array.Length);
			return new Guid(array);
		}

		// Token: 0x04000D90 RID: 3472
		private static int _objectTypeCount;

		// Token: 0x04000D91 RID: 3473
		private readonly int _objectID = Interlocked.Increment(ref SqlDelegatedTransaction._objectTypeCount);

		// Token: 0x04000D92 RID: 3474
		private const int _globalTransactionsTokenVersionSizeInBytes = 4;

		// Token: 0x04000D93 RID: 3475
		private SqlInternalConnection _connection;

		// Token: 0x04000D94 RID: 3476
		private IsolationLevel _isolationLevel;

		// Token: 0x04000D95 RID: 3477
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04000D96 RID: 3478
		private Transaction _atomicTransaction;

		// Token: 0x04000D97 RID: 3479
		private bool _active;
	}
}
