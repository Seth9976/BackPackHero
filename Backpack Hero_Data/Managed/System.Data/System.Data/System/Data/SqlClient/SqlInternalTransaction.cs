using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001BF RID: 447
	internal sealed class SqlInternalTransaction
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0006AC21 File Offset: 0x00068E21
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x0006AC29 File Offset: 0x00068E29
		internal bool RestoreBrokenConnection { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0006AC32 File Offset: 0x00068E32
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x0006AC3A File Offset: 0x00068E3A
		internal bool ConnectionHasBeenRestored { get; set; }

		// Token: 0x0600159A RID: 5530 RVA: 0x0006AC43 File Offset: 0x00068E43
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction)
			: this(innerConnection, type, outerTransaction, 0L)
		{
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0006AC50 File Offset: 0x00068E50
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction, long transactionId)
		{
			this._innerConnection = innerConnection;
			this._transactionType = type;
			if (outerTransaction != null)
			{
				this._parent = new WeakReference(outerTransaction);
			}
			this._transactionId = transactionId;
			this.RestoreBrokenConnection = false;
			this.ConnectionHasBeenRestored = false;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0006AC8B File Offset: 0x00068E8B
		internal bool HasParentTransaction
		{
			get
			{
				return TransactionType.LocalFromAPI == this._transactionType || (TransactionType.LocalFromTSQL == this._transactionType && this._parent != null);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0006ACAC File Offset: 0x00068EAC
		internal bool IsAborted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0006ACB7 File Offset: 0x00068EB7
		internal bool IsActive
		{
			get
			{
				return TransactionState.Active == this._transactionState;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0006ACC2 File Offset: 0x00068EC2
		internal bool IsCommitted
		{
			get
			{
				return TransactionState.Committed == this._transactionState;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0006ACCD File Offset: 0x00068ECD
		internal bool IsCompleted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState || TransactionState.Committed == this._transactionState || TransactionState.Unknown == this._transactionState;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0006ACEC File Offset: 0x00068EEC
		internal bool IsDelegated
		{
			get
			{
				return TransactionType.Delegated == this._transactionType;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0006ACF7 File Offset: 0x00068EF7
		internal bool IsDistributed
		{
			get
			{
				return TransactionType.Distributed == this._transactionType;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0006AD02 File Offset: 0x00068F02
		internal bool IsLocal
		{
			get
			{
				return TransactionType.LocalFromTSQL == this._transactionType || TransactionType.LocalFromAPI == this._transactionType;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0006AD18 File Offset: 0x00068F18
		internal bool IsOrphaned
		{
			get
			{
				return this._parent != null && this._parent.Target == null;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0006AD45 File Offset: 0x00068F45
		internal bool IsZombied
		{
			get
			{
				return this._innerConnection == null;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0006AD50 File Offset: 0x00068F50
		internal int OpenResultsCount
		{
			get
			{
				return this._openResultCount;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0006AD58 File Offset: 0x00068F58
		internal SqlTransaction Parent
		{
			get
			{
				SqlTransaction sqlTransaction = null;
				if (this._parent != null)
				{
					sqlTransaction = (SqlTransaction)this._parent.Target;
				}
				return sqlTransaction;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0006AD81 File Offset: 0x00068F81
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x0006AD89 File Offset: 0x00068F89
		internal long TransactionId
		{
			get
			{
				return this._transactionId;
			}
			set
			{
				this._transactionId = value;
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0006AD92 File Offset: 0x00068F92
		internal void Activate()
		{
			this._transactionState = TransactionState.Active;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0006AD9C File Offset: 0x00068F9C
		private void CheckTransactionLevelAndZombie()
		{
			try
			{
				if (!this.IsZombied && this.GetServerTransactionLevel() == 0)
				{
					this.Zombie();
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.Zombie();
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0006ADE4 File Offset: 0x00068FE4
		internal void CloseFromConnection()
		{
			SqlInternalConnection innerConnection = this._innerConnection;
			bool flag = true;
			try
			{
				innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag)
				{
					this.Zombie();
				}
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0006AE38 File Offset: 0x00069038
		internal void Commit()
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, null, false);
				this.ZombieParent();
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0006AE9C File Offset: 0x0006909C
		internal void Completed(TransactionState transactionState)
		{
			this._transactionState = transactionState;
			this.Zombie();
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0006AEAB File Offset: 0x000690AB
		internal int DecrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Decrement(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0006AEC2 File Offset: 0x000690C2
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0006AED1 File Offset: 0x000690D1
		private void Dispose(bool disposing)
		{
			if (disposing && this._innerConnection != null)
			{
				this._disposing = true;
				this.Rollback();
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0006AEEC File Offset: 0x000690EC
		private int GetServerTransactionLevel()
		{
			int num;
			using (SqlCommand sqlCommand = new SqlCommand("set @out = @@trancount", (SqlConnection)this._innerConnection.Owner))
			{
				sqlCommand.Transaction = this.Parent;
				SqlParameter sqlParameter = new SqlParameter("@out", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlCommand.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, "GetServerTransactionLevel");
				num = (int)sqlParameter.Value;
			}
			return num;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0006AF78 File Offset: 0x00069178
		internal int IncrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Increment(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0006AF8F File Offset: 0x0006918F
		internal void InitParent(SqlTransaction transaction)
		{
			this._parent = new WeakReference(transaction);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0006AFA0 File Offset: 0x000691A0
		internal void Rollback()
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
				this.Zombie();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.CheckTransactionLevelAndZombie();
				if (!this._disposing)
				{
					throw;
				}
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0006B010 File Offset: 0x00069210
		internal void Rollback(string transactionName)
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			if (string.IsNullOrEmpty(transactionName))
			{
				throw SQL.NullEmptyTransactionName();
			}
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, transactionName, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0006B07C File Offset: 0x0006927C
		internal void Save(string savePointName)
		{
			this._innerConnection.ValidateConnectionForExecute(null);
			if (string.IsNullOrEmpty(savePointName))
			{
				throw SQL.NullEmptyTransactionName();
			}
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Save, savePointName, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0006B0D8 File Offset: 0x000692D8
		internal void Zombie()
		{
			this.ZombieParent();
			SqlInternalConnection innerConnection = this._innerConnection;
			this._innerConnection = null;
			if (innerConnection != null)
			{
				innerConnection.DisconnectTransaction(this);
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0006B104 File Offset: 0x00069304
		private void ZombieParent()
		{
			if (this._parent != null)
			{
				SqlTransaction sqlTransaction = (SqlTransaction)this._parent.Target;
				if (sqlTransaction != null)
				{
					sqlTransaction.Zombie();
				}
				this._parent = null;
			}
		}

		// Token: 0x04000E88 RID: 3720
		internal const long NullTransactionId = 0L;

		// Token: 0x04000E89 RID: 3721
		private TransactionState _transactionState;

		// Token: 0x04000E8A RID: 3722
		private TransactionType _transactionType;

		// Token: 0x04000E8B RID: 3723
		private long _transactionId;

		// Token: 0x04000E8C RID: 3724
		private int _openResultCount;

		// Token: 0x04000E8D RID: 3725
		private SqlInternalConnection _innerConnection;

		// Token: 0x04000E8E RID: 3726
		private bool _disposing;

		// Token: 0x04000E8F RID: 3727
		private WeakReference _parent;
	}
}
