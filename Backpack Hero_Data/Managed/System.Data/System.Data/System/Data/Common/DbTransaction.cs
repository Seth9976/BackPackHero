using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>The base class for a transaction. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200034B RID: 843
	public abstract class DbTransaction : MarshalByRefObject, IDbTransaction, IDisposable, IAsyncDisposable
	{
		/// <summary>Specifies the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x000B261F File Offset: 0x000B081F
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction, or a null reference if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000B261F File Offset: 0x000B081F
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060028EF RID: 10479
		protected abstract DbConnection DbConnection { get; }

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060028F0 RID: 10480
		public abstract IsolationLevel IsolationLevel { get; }

		/// <summary>Commits the database transaction.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060028F1 RID: 10481
		public abstract void Commit();

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060028F2 RID: 10482 RVA: 0x000B2627 File Offset: 0x000B0827
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">If true, this method releases all resources held by any managed objects that this <see cref="T:System.Data.Common.DbTransaction" /> references.</param>
		// Token: 0x060028F3 RID: 10483 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060028F4 RID: 10484
		public abstract void Rollback();

		// Token: 0x060028F5 RID: 10485 RVA: 0x000B2630 File Offset: 0x000B0830
		public virtual Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Commit();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000B2678 File Offset: 0x000B0878
		public virtual ValueTask DisposeAsync()
		{
			this.Dispose();
			return default(ValueTask);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000B2694 File Offset: 0x000B0894
		public virtual Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Rollback();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}
	}
}
