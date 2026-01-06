using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for database-specific classes that represent commands. <see cref="Overload:System.Data.Common.DbCommand.ExecuteNonQueryAsync" /></summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000337 RID: 823
	public abstract class DbCommand : Component, IDbCommand, IDisposable, IAsyncDisposable
	{
		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600273A RID: 10042
		// (set) Token: 0x0600273B RID: 10043
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public abstract string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600273C RID: 10044
		// (set) Token: 0x0600273D RID: 10045
		public abstract int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.Common.DbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600273E RID: 10046
		// (set) Token: 0x0600273F RID: 10047
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract CommandType CommandType { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000AEF3E File Offset: 0x000AD13E
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x000AEF46 File Offset: 0x000AD146
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x000AEF3E File Offset: 0x000AD13E
		// (set) Token: 0x06002743 RID: 10051 RVA: 0x000AEF4F File Offset: 0x000AD14F
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = (DbConnection)value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002744 RID: 10052
		// (set) Token: 0x06002745 RID: 10053
		protected abstract DbConnection DbConnection { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002746 RID: 10054
		protected abstract DbParameterCollection DbParameterCollection { get; }

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002747 RID: 10055
		// (set) Token: 0x06002748 RID: 10056
		protected abstract DbTransaction DbTransaction { get; set; }

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a customized interface control.</summary>
		/// <returns>true, if the command object should be visible in a control; otherwise false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002749 RID: 10057
		// (set) Token: 0x0600274A RID: 10058
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		public abstract bool DesignTimeVisible { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000AEF5D File Offset: 0x000AD15D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000AEF5D File Offset: 0x000AD15D
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000AEF65 File Offset: 0x000AD165
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x000AEF6D File Offset: 0x000AD16D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Browsable(false)]
		public DbTransaction Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000AEF65 File Offset: 0x000AD165
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x000AEF76 File Offset: 0x000AD176
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = (DbTransaction)value;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002751 RID: 10065
		// (set) Token: 0x06002752 RID: 10066
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x06002753 RID: 10067 RVA: 0x000AEF84 File Offset: 0x000AD184
		internal void CancelIgnoreFailure()
		{
			try
			{
				this.Cancel();
			}
			catch (Exception)
			{
			}
		}

		/// <summary>Attempts to cancels the execution of a <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002754 RID: 10068
		public abstract void Cancel();

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002755 RID: 10069 RVA: 0x000AEFAC File Offset: 0x000AD1AC
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An IDbDataParameter object.</returns>
		// Token: 0x06002756 RID: 10070 RVA: 0x000AEFAC File Offset: 0x000AD1AC
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		// Token: 0x06002757 RID: 10071
		protected abstract DbParameter CreateDbParameter();

		/// <summary>Executes the command text against the connection.</summary>
		/// <returns>A task representing the operation.</returns>
		/// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior" />.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002758 RID: 10072
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		/// <summary>Executes a SQL statement against a connection object.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002759 RID: 10073
		public abstract int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600275A RID: 10074 RVA: 0x000AEFB4 File Offset: 0x000AD1B4
		public DbDataReader ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x0600275B RID: 10075 RVA: 0x000AEFB4 File Offset: 0x000AD1B4
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values. </summary>
		/// <returns>An <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600275C RID: 10076 RVA: 0x000AEFBD File Offset: 0x000AD1BD
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		// Token: 0x0600275D RID: 10077 RVA: 0x000AEFBD File Offset: 0x000AD1BD
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />, which executes a SQL statement against a connection object.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteNonQueryAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x0600275E RID: 10078 RVA: 0x000AEFC6 File Offset: 0x000AD1C6
		public Task<int> ExecuteNonQueryAsync()
		{
			return this.ExecuteNonQueryAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />. Providers should override with an appropriate implementation. The cancellation token may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token.  Exceptions thrown by <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbCommand object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x0600275F RID: 10079 RVA: 0x000AEFD4 File Offset: 0x000AD1D4
		public virtual Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<int>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<int> task;
			try
			{
				task = Task.FromResult<int>(this.ExecuteNonQuery());
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002760 RID: 10080 RVA: 0x000AF068 File Offset: 0x000AD268
		public Task<DbDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />. This method propagates a notification that operations should be canceled.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002761 RID: 10081 RVA: 0x000AF076 File Offset: 0x000AD276
		public Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		/// <summary>An asynchronous version of <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" />, which executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" /> and returns a <see cref="T:System.Data.Common.DbDataReader" />.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002762 RID: 10082 RVA: 0x000AF080 File Offset: 0x000AD280
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		/// <summary>Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteDbDataReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" />.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002763 RID: 10083 RVA: 0x000AF08E File Offset: 0x000AD28E
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteDbDataReaderAsync(behavior, cancellationToken);
		}

		/// <summary>Providers should implement this method to provide a non-default implementation for <see cref="Overload:System.Data.Common.DbCommand.ExecuteReader" /> overloads.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteReader" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token. Exceptions thrown by ExecuteReader will be communicated via the returned Task Exception property.This method accepts a cancellation token that can be used to request the operation to be cancelled early. Implementations may ignore this request.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">Options for statement execution and data retrieval.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		// Token: 0x06002764 RID: 10084 RVA: 0x000AF098 File Offset: 0x000AD298
		protected virtual Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<DbDataReader>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<DbDataReader> task;
			try
			{
				task = Task.FromResult<DbDataReader>(this.ExecuteReader(behavior));
			}
			catch (Exception ex)
			{
				task = Task.FromException<DbDataReader>(ex);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" />, which executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.Invokes <see cref="M:System.Data.Common.DbCommand.ExecuteScalarAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002765 RID: 10085 RVA: 0x000AF12C File Offset: 0x000AD32C
		public Task<object> ExecuteScalarAsync()
		{
			return this.ExecuteScalarAsync(CancellationToken.None);
		}

		/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" />. Providers should override with an appropriate implementation. The cancellation token may optionally be ignored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" /> method and returns a completed task, blocking the calling thread. The default implementation will return a cancelled task if passed an already cancelled cancellation token. Exceptions thrown by ExecuteScalar will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbCommand object until the returned Task is complete.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.Data.Common.DbException">An error occurred while executing the command text.</exception>
		// Token: 0x06002766 RID: 10086 RVA: 0x000AF13C File Offset: 0x000AD33C
		public virtual Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<object>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
				{
					((DbCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<object> task;
			try
			{
				task = Task.FromResult<object>(this.ExecuteScalar());
			}
			catch (Exception ex)
			{
				task = Task.FromException<object>(ex);
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
			}
			return task;
		}

		/// <summary>Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002767 RID: 10087
		public abstract object ExecuteScalar();

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002768 RID: 10088
		public abstract void Prepare();

		// Token: 0x06002769 RID: 10089 RVA: 0x000AF1D0 File Offset: 0x000AD3D0
		public virtual Task PrepareAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Prepare();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000AF218 File Offset: 0x000AD418
		public virtual ValueTask DisposeAsync()
		{
			base.Dispose();
			return default(ValueTask);
		}
	}
}
