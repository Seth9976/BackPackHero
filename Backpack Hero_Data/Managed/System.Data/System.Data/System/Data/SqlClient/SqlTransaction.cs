using System;
using System.Data.Common;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a Transact-SQL transaction to be made in a SQL Server database. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001E0 RID: 480
	public sealed class SqlTransaction : DbTransaction
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x00070574 File Offset: 0x0006E774
		internal SqlTransaction(SqlInternalConnection internalConnection, SqlConnection con, IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			this._isolationLevel = IsolationLevel.ReadCommitted;
			base..ctor();
			this._isolationLevel = iso;
			this._connection = con;
			if (internalTransaction == null)
			{
				this._internalTransaction = new SqlInternalTransaction(internalConnection, TransactionType.LocalFromAPI, this);
				return;
			}
			this._internalTransaction = internalTransaction;
			this._internalTransaction.InitParent(this);
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x000705C7 File Offset: 0x0006E7C7
		public new SqlConnection Connection
		{
			get
			{
				if (this.IsZombied)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000705D9 File Offset: 0x0006E7D9
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000705E1 File Offset: 0x0006E7E1
		internal SqlInternalTransaction InternalTransaction
		{
			get
			{
				return this._internalTransaction;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x000705E9 File Offset: 0x0006E7E9
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.ZombieCheck();
				return this._isolationLevel;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x000705F7 File Offset: 0x0006E7F7
		private bool IsYukonPartialZombie
		{
			get
			{
				return this._internalTransaction != null && this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0007060E File Offset: 0x0006E80E
		internal bool IsZombied
		{
			get
			{
				return this._internalTransaction == null || this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00070625 File Offset: 0x0006E825
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001705 RID: 5893 RVA: 0x0007064C File Offset: 0x0006E84C
		public override void Commit()
		{
			Exception ex = null;
			Guid guid = SqlTransaction.s_diagnosticListener.WriteTransactionCommitBefore(this._isolationLevel, this._connection, "Commit");
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Commit();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionCommitError(guid, this._isolationLevel, this._connection, ex, "Commit");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionCommitAfter(guid, this._isolationLevel, this._connection, "Commit");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0007070C File Offset: 0x0006E90C
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsZombied && !this.IsYukonPartialZombie)
			{
				this._internalTransaction.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001707 RID: 5895 RVA: 0x00070734 File Offset: 0x0006E934
		public override void Rollback()
		{
			Exception ex = null;
			Guid guid = SqlTransaction.s_diagnosticListener.WriteTransactionRollbackBefore(this._isolationLevel, this._connection, null, "Rollback");
			if (this.IsYukonPartialZombie)
			{
				this._internalTransaction = null;
				return;
			}
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackError(guid, this._isolationLevel, this._connection, null, ex, "Rollback");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackAfter(guid, this._isolationLevel, this._connection, null, "Rollback");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Rolls back a transaction from a pending state, and specifies the transaction or savepoint name.</summary>
		/// <param name="transactionName">The name of the transaction to roll back, or the savepoint to which to roll back. </param>
		/// <exception cref="T:System.ArgumentException">No transaction name was specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001708 RID: 5896 RVA: 0x00070808 File Offset: 0x0006EA08
		public void Rollback(string transactionName)
		{
			Exception ex = null;
			Guid guid = SqlTransaction.s_diagnosticListener.WriteTransactionRollbackBefore(this._isolationLevel, this._connection, transactionName, "Rollback");
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback(transactionName);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackError(guid, this._isolationLevel, this._connection, transactionName, ex, "Rollback");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackAfter(guid, this._isolationLevel, this._connection, transactionName, "Rollback");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Creates a savepoint in the transaction that can be used to roll back a part of the transaction, and specifies the savepoint name.</summary>
		/// <param name="savePointName">The name of the savepoint. </param>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001709 RID: 5897 RVA: 0x000708CC File Offset: 0x0006EACC
		public void Save(string savePointName)
		{
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._internalTransaction.Save(savePointName);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00070914 File Offset: 0x0006EB14
		internal void Zombie()
		{
			if (!(this._connection.InnerConnection is SqlInternalConnection) || this._isFromAPI)
			{
				this._internalTransaction = null;
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00070937 File Offset: 0x0006EB37
		private void ZombieCheck()
		{
			if (this.IsZombied)
			{
				if (this.IsYukonPartialZombie)
				{
					this._internalTransaction = null;
				}
				throw ADP.TransactionZombied(this);
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal SqlTransaction()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F31 RID: 3889
		private static readonly DiagnosticListener s_diagnosticListener = new DiagnosticListener("SqlClientDiagnosticListener");

		// Token: 0x04000F32 RID: 3890
		internal readonly IsolationLevel _isolationLevel;

		// Token: 0x04000F33 RID: 3891
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04000F34 RID: 3892
		private SqlConnection _connection;

		// Token: 0x04000F35 RID: 3893
		private bool _isFromAPI;
	}
}
