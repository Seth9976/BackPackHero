using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Threading;
using System.Xml;

// Token: 0x02000011 RID: 17
internal class SqlDependencyProcessDispatcher : MarshalByRefObject
{
	// Token: 0x0600009E RID: 158 RVA: 0x00003D5D File Offset: 0x00001F5D
	private SqlDependencyProcessDispatcher(object dummyVariable)
	{
		this._connectionContainers = new Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer>();
		this._sqlDependencyPerAppDomainDispatchers = new Dictionary<string, SqlDependencyPerAppDomainDispatcher>();
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00003D7B File Offset: 0x00001F7B
	public SqlDependencyProcessDispatcher()
	{
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D83 File Offset: 0x00001F83
	internal static SqlDependencyProcessDispatcher SingletonProcessDispatcher
	{
		get
		{
			return SqlDependencyProcessDispatcher.s_staticInstance;
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00003D8C File Offset: 0x00001F8C
	private static SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper GetHashHelper(string connectionString, out SqlConnectionStringBuilder connectionStringBuilder, out DbConnectionPoolIdentity identity, out string user, string queue)
	{
		connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
		{
			Pooling = false,
			Enlist = false,
			ConnectRetryCount = 0
		};
		if (queue != null)
		{
			connectionStringBuilder.ApplicationName = queue;
		}
		if (connectionStringBuilder.IntegratedSecurity)
		{
			identity = DbConnectionPoolIdentity.GetCurrent();
			user = null;
		}
		else
		{
			identity = null;
			user = connectionStringBuilder.UserID;
		}
		return new SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper(identity, connectionStringBuilder.ConnectionString, queue, connectionStringBuilder);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00003DF6 File Offset: 0x00001FF6
	public override object InitializeLifetimeService()
	{
		return null;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00003DFC File Offset: 0x00001FFC
	private void Invalidate(string server, SqlNotification sqlNotification)
	{
		Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
		lock (sqlDependencyPerAppDomainDispatchers)
		{
			foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher> keyValuePair in this._sqlDependencyPerAppDomainDispatchers)
			{
				SqlDependencyPerAppDomainDispatcher value = keyValuePair.Value;
				try
				{
					value.InvalidateServer(server, sqlNotification);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex);
				}
			}
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00003EA0 File Offset: 0x000020A0
	internal void QueueAppDomainUnloading(string appDomainKey)
	{
		ThreadPool.QueueUserWorkItem(new WaitCallback(this.AppDomainUnloading), appDomainKey);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00003EB8 File Offset: 0x000020B8
	private void AppDomainUnloading(object state)
	{
		string text = (string)state;
		Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
		lock (connectionContainers)
		{
			List<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper> list = new List<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper>();
			foreach (KeyValuePair<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> keyValuePair in this._connectionContainers)
			{
				SqlDependencyProcessDispatcher.SqlConnectionContainer value = keyValuePair.Value;
				if (value.AppDomainUnload(text))
				{
					list.Add(value.HashHelper);
				}
			}
			foreach (SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper sqlConnectionContainerHashHelper in list)
			{
				this._connectionContainers.Remove(sqlConnectionContainerHashHelper);
			}
		}
		Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
		lock (sqlDependencyPerAppDomainDispatchers)
		{
			this._sqlDependencyPerAppDomainDispatchers.Remove(text);
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003FDC File Offset: 0x000021DC
	internal bool StartWithDefault(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string service, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher, out bool errorOccurred, out bool appDomainStart)
	{
		return this.Start(connectionString, out server, out identity, out user, out database, ref service, appDomainKey, dispatcher, out errorOccurred, out appDomainStart, true);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00004004 File Offset: 0x00002204
	internal bool Start(string connectionString, string queue, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher)
	{
		string text;
		DbConnectionPoolIdentity dbConnectionPoolIdentity;
		bool flag;
		return this.Start(connectionString, out text, out dbConnectionPoolIdentity, out text, out text, ref queue, appDomainKey, dispatcher, out flag, out flag, false);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000402C File Offset: 0x0000222C
	private bool Start(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string queueService, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher, out bool errorOccurred, out bool appDomainStart, bool useDefaults)
	{
		server = null;
		identity = null;
		user = null;
		database = null;
		errorOccurred = false;
		appDomainStart = false;
		Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
		lock (sqlDependencyPerAppDomainDispatchers)
		{
			if (!this._sqlDependencyPerAppDomainDispatchers.ContainsKey(appDomainKey))
			{
				this._sqlDependencyPerAppDomainDispatchers[appDomainKey] = dispatcher;
			}
		}
		SqlConnectionStringBuilder sqlConnectionStringBuilder;
		SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper = SqlDependencyProcessDispatcher.GetHashHelper(connectionString, out sqlConnectionStringBuilder, out identity, out user, queueService);
		bool flag2 = false;
		SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = null;
		Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
		lock (connectionContainers)
		{
			if (!this._connectionContainers.ContainsKey(hashHelper))
			{
				sqlConnectionContainer = new SqlDependencyProcessDispatcher.SqlConnectionContainer(hashHelper, appDomainKey, useDefaults);
				this._connectionContainers.Add(hashHelper, sqlConnectionContainer);
				flag2 = true;
				appDomainStart = true;
			}
			else
			{
				sqlConnectionContainer = this._connectionContainers[hashHelper];
				if (sqlConnectionContainer.InErrorState)
				{
					errorOccurred = true;
				}
				else
				{
					sqlConnectionContainer.IncrementStartCount(appDomainKey, out appDomainStart);
				}
			}
		}
		if (useDefaults && !errorOccurred)
		{
			server = sqlConnectionContainer.Server;
			database = sqlConnectionContainer.Database;
			queueService = sqlConnectionContainer.Queue;
		}
		return flag2;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00004158 File Offset: 0x00002358
	internal bool Stop(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string queueService, string appDomainKey, out bool appDomainStop)
	{
		server = null;
		identity = null;
		user = null;
		database = null;
		appDomainStop = false;
		SqlConnectionStringBuilder sqlConnectionStringBuilder;
		SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper = SqlDependencyProcessDispatcher.GetHashHelper(connectionString, out sqlConnectionStringBuilder, out identity, out user, queueService);
		bool flag = false;
		Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
		lock (connectionContainers)
		{
			if (this._connectionContainers.ContainsKey(hashHelper))
			{
				SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = this._connectionContainers[hashHelper];
				server = sqlConnectionContainer.Server;
				database = sqlConnectionContainer.Database;
				queueService = sqlConnectionContainer.Queue;
				if (sqlConnectionContainer.Stop(appDomainKey, out appDomainStop))
				{
					flag = true;
					this._connectionContainers.Remove(hashHelper);
				}
			}
		}
		return flag;
	}

	// Token: 0x0400005C RID: 92
	private static SqlDependencyProcessDispatcher s_staticInstance = new SqlDependencyProcessDispatcher(null);

	// Token: 0x0400005D RID: 93
	private Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> _connectionContainers;

	// Token: 0x0400005E RID: 94
	private Dictionary<string, SqlDependencyPerAppDomainDispatcher> _sqlDependencyPerAppDomainDispatchers;

	// Token: 0x02000012 RID: 18
	private class SqlConnectionContainer
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00004220 File Offset: 0x00002420
		internal SqlConnectionContainer(SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper, string appDomainKey, bool useDefaults)
		{
			bool flag = false;
			try
			{
				this._hashHelper = hashHelper;
				string text = null;
				if (useDefaults)
				{
					text = Guid.NewGuid().ToString();
					this._queue = "SqlQueryNotificationService-" + text;
					this._hashHelper.ConnectionStringBuilder.ApplicationName = this._queue;
				}
				else
				{
					this._queue = this._hashHelper.Queue;
				}
				this._con = new SqlConnection(this._hashHelper.ConnectionStringBuilder.ConnectionString);
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this._con.ConnectionOptions;
				this._con.Open();
				this._cachedServer = this._con.DataSource;
				this._escapedQueueName = SqlConnection.FixupDatabaseTransactionName(this._queue);
				this._appDomainKeyHash = new Dictionary<string, int>();
				this._com = new SqlCommand
				{
					Connection = this._con,
					CommandText = "select is_broker_enabled from sys.databases where database_id=db_id()"
				};
				if (!(bool)this._com.ExecuteScalar())
				{
					throw SQL.SqlDependencyDatabaseBrokerDisabled();
				}
				this._conversationGuidParam = new SqlParameter("@p1", SqlDbType.UniqueIdentifier);
				this._timeoutParam = new SqlParameter("@p2", SqlDbType.Int)
				{
					Value = 0
				};
				this._com.Parameters.Add(this._timeoutParam);
				flag = true;
				this._receiveQuery = "WAITFOR(RECEIVE TOP (1) message_type_name, conversation_handle, cast(message_body AS XML) as message_body from " + this._escapedQueueName + "), TIMEOUT @p2;";
				if (useDefaults)
				{
					this._sprocName = SqlConnection.FixupDatabaseTransactionName("SqlQueryNotificationStoredProcedure-" + text);
					this.CreateQueueAndService(false);
				}
				else
				{
					this._com.CommandText = this._receiveQuery;
					this._endConversationQuery = "END CONVERSATION @p1; ";
					this._concatQuery = this._endConversationQuery + this._receiveQuery;
				}
				bool flag2;
				this.IncrementStartCount(appDomainKey, out flag2);
				this.SynchronouslyQueryServiceBrokerQueue();
				this._timeoutParam.Value = this._defaultWaitforTimeout;
				this.AsynchronouslyQueryServiceBrokerQueue();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex);
				if (flag)
				{
					this.TearDownAndDispose();
				}
				else
				{
					if (this._com != null)
					{
						this._com.Dispose();
						this._com = null;
					}
					if (this._con != null)
					{
						this._con.Dispose();
						this._con = null;
					}
				}
				throw;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000448C File Offset: 0x0000268C
		internal string Database
		{
			get
			{
				if (this._cachedDatabase == null)
				{
					this._cachedDatabase = this._con.Database;
				}
				return this._cachedDatabase;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000044AD File Offset: 0x000026AD
		internal SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper HashHelper
		{
			get
			{
				return this._hashHelper;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000044B5 File Offset: 0x000026B5
		internal bool InErrorState
		{
			get
			{
				return this._errorState;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000044BF File Offset: 0x000026BF
		internal string Queue
		{
			get
			{
				return this._queue;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000044C7 File Offset: 0x000026C7
		internal string Server
		{
			get
			{
				return this._cachedServer;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000044D0 File Offset: 0x000026D0
		internal bool AppDomainUnload(string appDomainKey)
		{
			Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
			lock (appDomainKeyHash)
			{
				if (this._appDomainKeyHash.ContainsKey(appDomainKey))
				{
					int i = this._appDomainKeyHash[appDomainKey];
					bool flag2 = false;
					while (i > 0)
					{
						this.Stop(appDomainKey, out flag2);
						i--;
					}
				}
			}
			return this._stopped;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004544 File Offset: 0x00002744
		private void AsynchronouslyQueryServiceBrokerQueue()
		{
			AsyncCallback asyncCallback = new AsyncCallback(this.AsyncResultCallback);
			this._com.BeginExecuteReader(CommandBehavior.Default, asyncCallback, null);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004570 File Offset: 0x00002770
		private void AsyncResultCallback(IAsyncResult asyncResult)
		{
			try
			{
				using (SqlDataReader sqlDataReader = this._com.EndExecuteReader(asyncResult))
				{
					this.ProcessNotificationResults(sqlDataReader);
				}
				if (!this._stop)
				{
					this.AsynchronouslyQueryServiceBrokerQueue();
				}
				else
				{
					this.TearDownAndDispose();
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					this._errorState = true;
					throw;
				}
				if (!this._stop)
				{
					ADP.TraceExceptionWithoutRethrow(ex);
				}
				if (this._stop)
				{
					this.TearDownAndDispose();
				}
				else
				{
					this._errorState = true;
					this.Restart(null);
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000461C File Offset: 0x0000281C
		private void CreateQueueAndService(bool restart)
		{
			SqlCommand sqlCommand = new SqlCommand
			{
				Connection = this._con
			};
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = this._con.BeginTransaction();
				sqlCommand.Transaction = sqlTransaction;
				string text = SqlServerEscapeHelper.MakeStringLiteral(this._queue);
				sqlCommand.CommandText = string.Concat(new string[]
				{
					"CREATE PROCEDURE ", this._sprocName, " AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM ", this._escapedQueueName, "; IF (SELECT COUNT(*) FROM ", this._escapedQueueName, " WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = ", text, ") > 0)   DROP SERVICE ", this._escapedQueueName,
					"; if (OBJECT_ID(", text, ", 'SQ') IS NOT NULL)   DROP QUEUE ", this._escapedQueueName, "; DROP PROCEDURE ", this._sprocName, "; END COMMIT TRANSACTION; END"
				});
				if (!restart)
				{
					sqlCommand.ExecuteNonQuery();
				}
				else
				{
					try
					{
						sqlCommand.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
						try
						{
							if (sqlTransaction != null)
							{
								sqlTransaction.Rollback();
								sqlTransaction = null;
							}
						}
						catch (Exception ex2)
						{
							if (!ADP.IsCatchableExceptionType(ex2))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex2);
						}
					}
					if (sqlTransaction == null)
					{
						sqlTransaction = this._con.BeginTransaction();
						sqlCommand.Transaction = sqlTransaction;
					}
				}
				sqlCommand.CommandText = string.Concat(new string[]
				{
					"IF OBJECT_ID(", text, ", 'SQ') IS NULL BEGIN CREATE QUEUE ", this._escapedQueueName, " WITH ACTIVATION (PROCEDURE_NAME=", this._sprocName, ", MAX_QUEUE_READERS=1, EXECUTE AS OWNER); END; IF (SELECT COUNT(*) FROM sys.services WHERE NAME=", text, ") = 0 BEGIN CREATE SERVICE ", this._escapedQueueName,
					" ON QUEUE ", this._escapedQueueName, " ([http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]); IF (SELECT COUNT(*) FROM sys.database_principals WHERE name='sql_dependency_subscriber' AND type='R') <> 0 BEGIN GRANT SEND ON SERVICE::", this._escapedQueueName, " TO sql_dependency_subscriber; END;  END; BEGIN DIALOG @dialog_handle FROM SERVICE ", this._escapedQueueName, " TO SERVICE ", text
				});
				SqlParameter sqlParameter = new SqlParameter
				{
					ParameterName = "@dialog_handle",
					DbType = DbType.Guid,
					Direction = ParameterDirection.Output
				};
				sqlCommand.Parameters.Add(sqlParameter);
				sqlCommand.ExecuteNonQuery();
				this._dialogHandle = ((Guid)sqlParameter.Value).ToString();
				this._beginConversationQuery = "BEGIN CONVERSATION TIMER ('" + this._dialogHandle + "') TIMEOUT = 120; " + this._receiveQuery;
				this._com.CommandText = this._beginConversationQuery;
				this._endConversationQuery = "END CONVERSATION @p1; ";
				this._concatQuery = this._endConversationQuery + this._com.CommandText;
				sqlTransaction.Commit();
				sqlTransaction = null;
				this._serviceQueueCreated = true;
			}
			finally
			{
				if (sqlTransaction != null)
				{
					try
					{
						sqlTransaction.Rollback();
						sqlTransaction = null;
					}
					catch (Exception ex3)
					{
						if (!ADP.IsCatchableExceptionType(ex3))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex3);
					}
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004940 File Offset: 0x00002B40
		internal void IncrementStartCount(string appDomainKey, out bool appDomainStart)
		{
			appDomainStart = false;
			Interlocked.Increment(ref this._startCount);
			Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
			lock (appDomainKeyHash)
			{
				if (this._appDomainKeyHash.ContainsKey(appDomainKey))
				{
					this._appDomainKeyHash[appDomainKey] = this._appDomainKeyHash[appDomainKey] + 1;
				}
				else
				{
					this._appDomainKeyHash[appDomainKey] = 1;
					appDomainStart = true;
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000049C4 File Offset: 0x00002BC4
		private void ProcessNotificationResults(SqlDataReader reader)
		{
			Guid guid = Guid.Empty;
			try
			{
				if (!this._stop)
				{
					while (reader.Read())
					{
						string @string = reader.GetString(0);
						guid = reader.GetGuid(1);
						if (string.Compare(@string, "http://schemas.microsoft.com/SQL/Notifications/QueryNotification", StringComparison.OrdinalIgnoreCase) == 0)
						{
							SqlXml sqlXml = reader.GetSqlXml(2);
							if (sqlXml == null)
							{
								continue;
							}
							SqlNotification sqlNotification = SqlDependencyProcessDispatcher.SqlNotificationParser.ProcessMessage(sqlXml);
							if (sqlNotification == null)
							{
								continue;
							}
							string key = sqlNotification.Key;
							int num = key.IndexOf(';');
							if (num < 0)
							{
								continue;
							}
							string text = key.Substring(0, num);
							Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = SqlDependencyProcessDispatcher.s_staticInstance._sqlDependencyPerAppDomainDispatchers;
							SqlDependencyPerAppDomainDispatcher sqlDependencyPerAppDomainDispatcher;
							lock (sqlDependencyPerAppDomainDispatchers)
							{
								sqlDependencyPerAppDomainDispatcher = SqlDependencyProcessDispatcher.s_staticInstance._sqlDependencyPerAppDomainDispatchers[text];
							}
							if (sqlDependencyPerAppDomainDispatcher == null)
							{
								continue;
							}
							try
							{
								sqlDependencyPerAppDomainDispatcher.InvalidateCommandID(sqlNotification);
								continue;
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								ADP.TraceExceptionWithoutRethrow(ex);
								continue;
							}
						}
						guid = Guid.Empty;
					}
				}
			}
			finally
			{
				if (guid == Guid.Empty)
				{
					this._com.CommandText = this._beginConversationQuery ?? this._receiveQuery;
					if (this._com.Parameters.Count > 1)
					{
						this._com.Parameters.Remove(this._conversationGuidParam);
					}
				}
				else
				{
					this._com.CommandText = this._concatQuery;
					this._conversationGuidParam.Value = guid;
					if (this._com.Parameters.Count == 1)
					{
						this._com.Parameters.Add(this._conversationGuidParam);
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B80 File Offset: 0x00002D80
		private void Restart(object unused)
		{
			try
			{
				SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = this;
				lock (sqlConnectionContainer)
				{
					if (!this._stop)
					{
						try
						{
							this._con.Close();
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
						}
					}
				}
				sqlConnectionContainer = this;
				lock (sqlConnectionContainer)
				{
					if (!this._stop)
					{
						this._con.Open();
					}
				}
				sqlConnectionContainer = this;
				lock (sqlConnectionContainer)
				{
					if (!this._stop && this._serviceQueueCreated)
					{
						bool flag2 = false;
						try
						{
							this.CreateQueueAndService(true);
						}
						catch (Exception ex2)
						{
							if (!ADP.IsCatchableExceptionType(ex2))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex2);
							flag2 = true;
						}
						if (flag2)
						{
							SqlDependencyProcessDispatcher.s_staticInstance.Invalidate(this.Server, new SqlNotification(SqlNotificationInfo.Error, SqlNotificationSource.Client, SqlNotificationType.Change, null));
						}
					}
				}
				sqlConnectionContainer = this;
				lock (sqlConnectionContainer)
				{
					if (!this._stop)
					{
						this._timeoutParam.Value = 0;
						this.SynchronouslyQueryServiceBrokerQueue();
						this._timeoutParam.Value = this._defaultWaitforTimeout;
						this.AsynchronouslyQueryServiceBrokerQueue();
						this._errorState = false;
						Timer retryTimer = this._retryTimer;
						if (retryTimer != null)
						{
							this._retryTimer = null;
							retryTimer.Dispose();
						}
					}
				}
				if (this._stop)
				{
					this.TearDownAndDispose();
				}
			}
			catch (Exception ex3)
			{
				if (!ADP.IsCatchableExceptionType(ex3))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex3);
				try
				{
					SqlDependencyProcessDispatcher.s_staticInstance.Invalidate(this.Server, new SqlNotification(SqlNotificationInfo.Error, SqlNotificationSource.Client, SqlNotificationType.Change, null));
				}
				catch (Exception ex4)
				{
					if (!ADP.IsCatchableExceptionType(ex4))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex4);
				}
				try
				{
					this._con.Close();
				}
				catch (Exception ex5)
				{
					if (!ADP.IsCatchableExceptionType(ex5))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex5);
				}
				if (!this._stop)
				{
					this._retryTimer = new Timer(new TimerCallback(this.Restart), null, this._defaultWaitforTimeout, -1);
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004E50 File Offset: 0x00003050
		internal bool Stop(string appDomainKey, out bool appDomainStop)
		{
			appDomainStop = false;
			if (appDomainKey != null)
			{
				Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
				lock (appDomainKeyHash)
				{
					if (this._appDomainKeyHash.ContainsKey(appDomainKey))
					{
						int num = this._appDomainKeyHash[appDomainKey];
						if (num > 0)
						{
							this._appDomainKeyHash[appDomainKey] = num - 1;
						}
						if (1 == num)
						{
							this._appDomainKeyHash.Remove(appDomainKey);
							appDomainStop = true;
						}
					}
				}
			}
			if (Interlocked.Decrement(ref this._startCount) == 0)
			{
				SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = this;
				lock (sqlConnectionContainer)
				{
					try
					{
						this._com.Cancel();
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
					}
					this._stop = true;
				}
				Stopwatch stopwatch = Stopwatch.StartNew();
				for (;;)
				{
					sqlConnectionContainer = this;
					lock (sqlConnectionContainer)
					{
						if (this._stopped)
						{
							break;
						}
						if (this._errorState || stopwatch.Elapsed.Seconds >= 30)
						{
							Timer retryTimer = this._retryTimer;
							this._retryTimer = null;
							if (retryTimer != null)
							{
								retryTimer.Dispose();
							}
							this.TearDownAndDispose();
							break;
						}
					}
					Thread.Sleep(1);
				}
			}
			return this._stopped;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004FC8 File Offset: 0x000031C8
		private void SynchronouslyQueryServiceBrokerQueue()
		{
			using (SqlDataReader sqlDataReader = this._com.ExecuteReader())
			{
				this.ProcessNotificationResults(sqlDataReader);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005004 File Offset: 0x00003204
		private void TearDownAndDispose()
		{
			lock (this)
			{
				try
				{
					if (this._con.State != ConnectionState.Closed && ConnectionState.Broken != this._con.State)
					{
						if (this._com.Parameters.Count > 1)
						{
							try
							{
								this._com.CommandText = this._endConversationQuery;
								this._com.Parameters.Remove(this._timeoutParam);
								this._com.ExecuteNonQuery();
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								ADP.TraceExceptionWithoutRethrow(ex);
							}
						}
						if (this._serviceQueueCreated && !this._errorState)
						{
							this._com.CommandText = string.Concat(new string[] { "BEGIN TRANSACTION; DROP SERVICE ", this._escapedQueueName, "; DROP QUEUE ", this._escapedQueueName, "; DROP PROCEDURE ", this._sprocName, "; COMMIT TRANSACTION;" });
							try
							{
								this._com.ExecuteNonQuery();
							}
							catch (Exception ex2)
							{
								if (!ADP.IsCatchableExceptionType(ex2))
								{
									throw;
								}
								ADP.TraceExceptionWithoutRethrow(ex2);
							}
						}
					}
				}
				finally
				{
					this._stopped = true;
					this._con.Dispose();
				}
			}
		}

		// Token: 0x0400005F RID: 95
		private SqlConnection _con;

		// Token: 0x04000060 RID: 96
		private SqlCommand _com;

		// Token: 0x04000061 RID: 97
		private SqlParameter _conversationGuidParam;

		// Token: 0x04000062 RID: 98
		private SqlParameter _timeoutParam;

		// Token: 0x04000063 RID: 99
		private SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper _hashHelper;

		// Token: 0x04000064 RID: 100
		private string _queue;

		// Token: 0x04000065 RID: 101
		private string _receiveQuery;

		// Token: 0x04000066 RID: 102
		private string _beginConversationQuery;

		// Token: 0x04000067 RID: 103
		private string _endConversationQuery;

		// Token: 0x04000068 RID: 104
		private string _concatQuery;

		// Token: 0x04000069 RID: 105
		private readonly int _defaultWaitforTimeout = 60000;

		// Token: 0x0400006A RID: 106
		private string _escapedQueueName;

		// Token: 0x0400006B RID: 107
		private string _sprocName;

		// Token: 0x0400006C RID: 108
		private string _dialogHandle;

		// Token: 0x0400006D RID: 109
		private string _cachedServer;

		// Token: 0x0400006E RID: 110
		private string _cachedDatabase;

		// Token: 0x0400006F RID: 111
		private volatile bool _errorState;

		// Token: 0x04000070 RID: 112
		private volatile bool _stop;

		// Token: 0x04000071 RID: 113
		private volatile bool _stopped;

		// Token: 0x04000072 RID: 114
		private volatile bool _serviceQueueCreated;

		// Token: 0x04000073 RID: 115
		private int _startCount;

		// Token: 0x04000074 RID: 116
		private Timer _retryTimer;

		// Token: 0x04000075 RID: 117
		private Dictionary<string, int> _appDomainKeyHash;
	}

	// Token: 0x02000013 RID: 19
	private class SqlNotificationParser
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000051A4 File Offset: 0x000033A4
		internal static SqlNotification ProcessMessage(SqlXml xmlMessage)
		{
			SqlNotification sqlNotification;
			using (XmlReader xmlReader = xmlMessage.CreateReader())
			{
				string empty = string.Empty;
				SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes messageAttributes = SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.None;
				SqlNotificationType sqlNotificationType = SqlNotificationType.Unknown;
				SqlNotificationInfo sqlNotificationInfo = SqlNotificationInfo.Unknown;
				SqlNotificationSource sqlNotificationSource = SqlNotificationSource.Unknown;
				string text = string.Empty;
				xmlReader.Read();
				if (XmlNodeType.Element == xmlReader.NodeType && "QueryNotification" == xmlReader.LocalName && 3 <= xmlReader.AttributeCount)
				{
					while (SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.All != messageAttributes && xmlReader.MoveToNextAttribute())
					{
						try
						{
							string localName = xmlReader.LocalName;
							if (!(localName == "type"))
							{
								if (!(localName == "source"))
								{
									if (localName == "info")
									{
										try
										{
											string value = xmlReader.Value;
											if (!(value == "set options"))
											{
												if (!(value == "previous invalid"))
												{
													if (!(value == "query template limit"))
													{
														SqlNotificationInfo sqlNotificationInfo2 = (SqlNotificationInfo)Enum.Parse(typeof(SqlNotificationInfo), value, true);
														if (Enum.IsDefined(typeof(SqlNotificationInfo), sqlNotificationInfo2))
														{
															sqlNotificationInfo = sqlNotificationInfo2;
														}
													}
													else
													{
														sqlNotificationInfo = SqlNotificationInfo.TemplateLimit;
													}
												}
												else
												{
													sqlNotificationInfo = SqlNotificationInfo.PreviousFire;
												}
											}
											else
											{
												sqlNotificationInfo = SqlNotificationInfo.Options;
											}
										}
										catch (Exception ex)
										{
											if (!ADP.IsCatchableExceptionType(ex))
											{
												throw;
											}
											ADP.TraceExceptionWithoutRethrow(ex);
										}
										messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Info;
									}
								}
								else
								{
									try
									{
										SqlNotificationSource sqlNotificationSource2 = (SqlNotificationSource)Enum.Parse(typeof(SqlNotificationSource), xmlReader.Value, true);
										if (Enum.IsDefined(typeof(SqlNotificationSource), sqlNotificationSource2))
										{
											sqlNotificationSource = sqlNotificationSource2;
										}
									}
									catch (Exception ex2)
									{
										if (!ADP.IsCatchableExceptionType(ex2))
										{
											throw;
										}
										ADP.TraceExceptionWithoutRethrow(ex2);
									}
									messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Source;
								}
							}
							else
							{
								try
								{
									SqlNotificationType sqlNotificationType2 = (SqlNotificationType)Enum.Parse(typeof(SqlNotificationType), xmlReader.Value, true);
									if (Enum.IsDefined(typeof(SqlNotificationType), sqlNotificationType2))
									{
										sqlNotificationType = sqlNotificationType2;
									}
								}
								catch (Exception ex3)
								{
									if (!ADP.IsCatchableExceptionType(ex3))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(ex3);
								}
								messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Type;
							}
						}
						catch (ArgumentException ex4)
						{
							ADP.TraceExceptionWithoutRethrow(ex4);
							return null;
						}
					}
					if (SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.All != messageAttributes)
					{
						sqlNotification = null;
					}
					else if (!xmlReader.Read())
					{
						sqlNotification = null;
					}
					else if (XmlNodeType.Element != xmlReader.NodeType || string.Compare(xmlReader.LocalName, "Message", StringComparison.OrdinalIgnoreCase) != 0)
					{
						sqlNotification = null;
					}
					else if (!xmlReader.Read())
					{
						sqlNotification = null;
					}
					else if (xmlReader.NodeType != XmlNodeType.Text)
					{
						sqlNotification = null;
					}
					else
					{
						using (XmlTextReader xmlTextReader = new XmlTextReader(xmlReader.Value, XmlNodeType.Element, null))
						{
							if (!xmlTextReader.Read())
							{
								return null;
							}
							if (xmlTextReader.NodeType != XmlNodeType.Text)
							{
								return null;
							}
							text = xmlTextReader.Value;
							xmlTextReader.Close();
						}
						sqlNotification = new SqlNotification(sqlNotificationInfo, sqlNotificationSource, sqlNotificationType, text);
					}
				}
				else
				{
					sqlNotification = null;
				}
			}
			return sqlNotification;
		}

		// Token: 0x04000076 RID: 118
		private const string RootNode = "QueryNotification";

		// Token: 0x04000077 RID: 119
		private const string MessageNode = "Message";

		// Token: 0x04000078 RID: 120
		private const string InfoAttribute = "info";

		// Token: 0x04000079 RID: 121
		private const string SourceAttribute = "source";

		// Token: 0x0400007A RID: 122
		private const string TypeAttribute = "type";

		// Token: 0x02000014 RID: 20
		[Flags]
		private enum MessageAttributes
		{
			// Token: 0x0400007C RID: 124
			None = 0,
			// Token: 0x0400007D RID: 125
			Type = 1,
			// Token: 0x0400007E RID: 126
			Source = 2,
			// Token: 0x0400007F RID: 127
			Info = 4,
			// Token: 0x04000080 RID: 128
			All = 7
		}
	}

	// Token: 0x02000015 RID: 21
	private class SqlConnectionContainerHashHelper
	{
		// Token: 0x060000BD RID: 189 RVA: 0x000054F4 File Offset: 0x000036F4
		internal SqlConnectionContainerHashHelper(DbConnectionPoolIdentity identity, string connectionString, string queue, SqlConnectionStringBuilder connectionStringBuilder)
		{
			this._identity = identity;
			this._connectionString = connectionString;
			this._queue = queue;
			this._connectionStringBuilder = connectionStringBuilder;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005519 File Offset: 0x00003719
		internal SqlConnectionStringBuilder ConnectionStringBuilder
		{
			get
			{
				return this._connectionStringBuilder;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005521 File Offset: 0x00003721
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005529 File Offset: 0x00003729
		internal string Queue
		{
			get
			{
				return this._queue;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005534 File Offset: 0x00003734
		public override bool Equals(object value)
		{
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper sqlConnectionContainerHashHelper = (SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper)value;
			bool flag;
			if (sqlConnectionContainerHashHelper == null)
			{
				flag = false;
			}
			else if (this == sqlConnectionContainerHashHelper)
			{
				flag = true;
			}
			else if ((this._identity != null && sqlConnectionContainerHashHelper._identity == null) || (this._identity == null && sqlConnectionContainerHashHelper._identity != null))
			{
				flag = false;
			}
			else if (this._identity == null && sqlConnectionContainerHashHelper._identity == null)
			{
				flag = sqlConnectionContainerHashHelper._connectionString == this._connectionString && string.Equals(sqlConnectionContainerHashHelper._queue, this._queue, StringComparison.OrdinalIgnoreCase);
			}
			else
			{
				flag = sqlConnectionContainerHashHelper._identity.Equals(this._identity) && sqlConnectionContainerHashHelper._connectionString == this._connectionString && string.Equals(sqlConnectionContainerHashHelper._queue, this._queue, StringComparison.OrdinalIgnoreCase);
			}
			return flag;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005604 File Offset: 0x00003804
		public override int GetHashCode()
		{
			int num = 0;
			if (this._identity != null)
			{
				num = this._identity.GetHashCode();
			}
			if (this._queue != null)
			{
				num = this._connectionString.GetHashCode() + this._queue.GetHashCode() + num;
			}
			else
			{
				num = this._connectionString.GetHashCode() + num;
			}
			return num;
		}

		// Token: 0x04000081 RID: 129
		private DbConnectionPoolIdentity _identity;

		// Token: 0x04000082 RID: 130
		private string _connectionString;

		// Token: 0x04000083 RID: 131
		private string _queue;

		// Token: 0x04000084 RID: 132
		private SqlConnectionStringBuilder _connectionStringBuilder;
	}
}
