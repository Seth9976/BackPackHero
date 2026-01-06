using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Common
{
	/// <summary>Represents a connection to a database. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000339 RID: 825
	public abstract class DbConnection : Component, IDbConnection, IDisposable, IAsyncDisposable
	{
		/// <summary>Gets or sets the string used to open the connection.</summary>
		/// <returns>The connection string used to establish the initial connection. The exact contents of the connection string depend on the specific data source for this connection. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002771 RID: 10097
		// (set) Token: 0x06002772 RID: 10098
		[DefaultValue("")]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[RecommendedAsConfigurable(true)]
		public abstract string ConnectionString { get; set; }

		/// <summary>Gets the time to wait while establishing a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is determined by the specific type of connection that you are using.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000AF24D File Offset: 0x000AD44D
		public virtual int ConnectionTimeout
		{
			get
			{
				return 15;
			}
		}

		/// <summary>Gets the name of the current database after a connection is opened, or the database name specified in the connection string before the connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002774 RID: 10100
		public abstract string Database { get; }

		/// <summary>Gets the name of the database server to which to connect.</summary>
		/// <returns>The name of the database server to which to connect. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002775 RID: 10101
		public abstract string DataSource { get; }

		/// <summary>Gets the <see cref="T:System.Data.Common.DbProviderFactory" /> for this <see cref="T:System.Data.Common.DbConnection" />.</summary>
		/// <returns>A set of methods for creating instances of a provider's implementation of the data source classes.</returns>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected virtual DbProviderFactory DbProviderFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000AF251 File Offset: 0x000AD451
		internal DbProviderFactory ProviderFactory
		{
			get
			{
				return this.DbProviderFactory;
			}
		}

		/// <summary>Gets a string that represents the version of the server to which the object is connected.</summary>
		/// <returns>The version of the database. The format of the string returned depends on the specific type of connection you are using.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Data.Common.DbConnection.ServerVersion" /> was called while the returned Task was not completed and the connection was not opened after a call to <see cref="Overload:System.Data.Common.DbConnection.OpenAsync" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002778 RID: 10104
		[Browsable(false)]
		public abstract string ServerVersion { get; }

		/// <summary>Gets a string that describes the state of the connection.</summary>
		/// <returns>The state of the connection. The format of the string returned depends on the specific type of connection you are using.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002779 RID: 10105
		[Browsable(false)]
		public abstract ConnectionState State { get; }

		/// <summary>Occurs when the state of the event changes.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600277A RID: 10106 RVA: 0x000AF25C File Offset: 0x000AD45C
		// (remove) Token: 0x0600277B RID: 10107 RVA: 0x000AF294 File Offset: 0x000AD494
		public virtual event StateChangeEventHandler StateChange;

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		// Token: 0x0600277C RID: 10108
		protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600277D RID: 10109 RVA: 0x000AF2C9 File Offset: 0x000AD4C9
		public DbTransaction BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600277E RID: 10110 RVA: 0x000AF2D2 File Offset: 0x000AD4D2
		public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		/// <summary>Begins a database transaction.</summary>
		/// <returns>An object that represents the new transaction.</returns>
		// Token: 0x0600277F RID: 10111 RVA: 0x000AF2C9 File Offset: 0x000AD4C9
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object that represents the new transaction.</returns>
		/// <param name="isolationLevel">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
		// Token: 0x06002780 RID: 10112 RVA: 0x000AF2D2 File Offset: 0x000AD4D2
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
		/// <exception cref="T:System.Data.Common.DbException">The connection-level error that occurred while opening the connection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002781 RID: 10113
		public abstract void Close();

		/// <summary>Changes the current database for an open connection.</summary>
		/// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002782 RID: 10114
		public abstract void ChangeDatabase(string databaseName);

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002783 RID: 10115 RVA: 0x000AF2DB File Offset: 0x000AD4DB
		public DbCommand CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the connection.</returns>
		// Token: 0x06002784 RID: 10116 RVA: 0x000AF2DB File Offset: 0x000AD4DB
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateDbCommand();
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
		// Token: 0x06002785 RID: 10117
		protected abstract DbCommand CreateDbCommand();

		/// <summary>Enlists in the specified transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x06002786 RID: 10118 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual void EnlistTransaction(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002787 RID: 10119 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual DataTable GetSchema()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002788 RID: 10120 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual DataTable GetSchema(string collectionName)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002789 RID: 10121 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Raises the <see cref="E:System.Data.Common.DbConnection.StateChange" /> event.</summary>
		/// <param name="stateChange">A <see cref="T:System.Data.StateChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x0600278A RID: 10122 RVA: 0x000AF2E3 File Offset: 0x000AD4E3
		protected virtual void OnStateChange(StateChangeEventArgs stateChange)
		{
			if (this._suppressStateChangeForReconnection)
			{
				return;
			}
			StateChangeEventHandler stateChange2 = this.StateChange;
			if (stateChange2 == null)
			{
				return;
			}
			stateChange2(this, stateChange);
		}

		/// <summary>Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600278B RID: 10123
		public abstract void Open();

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />, which opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />. This method invokes the virtual method <see cref="M:System.Data.Common.DbConnection.OpenAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		// Token: 0x0600278C RID: 10124 RVA: 0x000AF300 File Offset: 0x000AD500
		public Task OpenAsync()
		{
			return this.OpenAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />. Providers should override with an appropriate implementation. The cancellation token can optionally be honored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbConnection.Open" /> call and returns a completed task. The default implementation will return a cancelled task if passed an already cancelled cancellationToken. Exceptions thrown by Open will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbConnection object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		// Token: 0x0600278D RID: 10125 RVA: 0x000AF310 File Offset: 0x000AD510
		public virtual Task OpenAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Open();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000AF358 File Offset: 0x000AD558
		protected virtual ValueTask<DbTransaction> BeginDbTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<DbTransaction>(Task.FromCanceled<DbTransaction>(cancellationToken));
			}
			ValueTask<DbTransaction> valueTask;
			try
			{
				valueTask = new ValueTask<DbTransaction>(this.BeginDbTransaction(isolationLevel));
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask<DbTransaction>(Task.FromException<DbTransaction>(ex));
			}
			return valueTask;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000AF3A8 File Offset: 0x000AD5A8
		public ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.BeginDbTransactionAsync(IsolationLevel.Unspecified, cancellationToken);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000AF3B2 File Offset: 0x000AD5B2
		public ValueTask<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.BeginDbTransactionAsync(isolationLevel, cancellationToken);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000AF3BC File Offset: 0x000AD5BC
		public virtual Task CloseAsync()
		{
			Task task;
			try
			{
				this.Close();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000AF3F0 File Offset: 0x000AD5F0
		public virtual ValueTask DisposeAsync()
		{
			base.Dispose();
			return default(ValueTask);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000AF40C File Offset: 0x000AD60C
		public virtual Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.ChangeDatabase(databaseName);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x04001917 RID: 6423
		internal bool _suppressStateChangeForReconnection;
	}
}
