using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.SqlClient
{
	/// <summary>The <see cref="T:System.Data.SqlClient.SqlDependency" /> object represents a query notification dependency between an application and an instance of SQL Server. An application can create a <see cref="T:System.Data.SqlClient.SqlDependency" /> object and register to receive notifications via the <see cref="T:System.Data.SqlClient.OnChangeEventHandler" /> event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A5 RID: 421
	public sealed class SqlDependency
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class with the default settings.</summary>
		// Token: 0x06001475 RID: 5237 RVA: 0x000650B5 File Offset: 0x000632B5
		public SqlDependency()
			: this(null, null, 0)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class and associates it with the <see cref="T:System.Data.SqlClient.SqlCommand" /> parameter.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to associate with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object. The constructor will set up a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object and bind it to the command. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property, and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency. </exception>
		// Token: 0x06001476 RID: 5238 RVA: 0x000650C0 File Offset: 0x000632C0
		public SqlDependency(SqlCommand command)
			: this(command, null, 0)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class, associates it with the <see cref="T:System.Data.SqlClient.SqlCommand" /> parameter, and specifies notification options and a time-out value.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to associate with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object. The constructor sets up a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object and bind it to the command.</param>
		/// <param name="options">The notification request options to be used by this dependency.  <paramref name="null" /> to use the default service. </param>
		/// <param name="timeout">The time-out for this notification in seconds. The default is 0, indicating that the server's time-out should be used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is NULL. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency.An attempt was made to create a SqlDependency instance from within SQLCLR.</exception>
		// Token: 0x06001477 RID: 5239 RVA: 0x000650CC File Offset: 0x000632CC
		public SqlDependency(SqlCommand command, string options, int timeout)
		{
			if (timeout < 0)
			{
				throw SQL.InvalidSqlDependencyTimeout("timeout");
			}
			this._timeout = timeout;
			if (options != null)
			{
				this._options = options;
			}
			this.AddCommandInternal(command);
			SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddDependencyEntry(this);
		}

		/// <summary>Gets a value that indicates whether one of the result sets associated with the dependency has changed.</summary>
		/// <returns>A Boolean value indicating whether one of the result sets has changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00065165 File Offset: 0x00063365
		public bool HasChanges
		{
			get
			{
				return this._dependencyFired;
			}
		}

		/// <summary>Gets a value that uniquely identifies this instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class.</summary>
		/// <returns>A string representation of a GUID that is generated for each instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0006516D File Offset: 0x0006336D
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00065175 File Offset: 0x00063375
		internal static string AppDomainKey
		{
			get
			{
				return SqlDependency.s_appDomainKey;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0006517C File Offset: 0x0006337C
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00065184 File Offset: 0x00063384
		internal string Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0006518C File Offset: 0x0006338C
		internal static SqlDependencyProcessDispatcher ProcessDispatcher
		{
			get
			{
				return SqlDependency.s_processDispatcher;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00065193 File Offset: 0x00063393
		internal int Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		/// <summary>Occurs when a notification is received for any of the commands associated with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600147F RID: 5247 RVA: 0x0006519C File Offset: 0x0006339C
		// (remove) Token: 0x06001480 RID: 5248 RVA: 0x00065224 File Offset: 0x00063424
		public event OnChangeEventHandler OnChange
		{
			add
			{
				if (value != null)
				{
					SqlNotificationEventArgs sqlNotificationEventArgs = null;
					object eventHandlerLock = this._eventHandlerLock;
					lock (eventHandlerLock)
					{
						if (this._dependencyFired)
						{
							sqlNotificationEventArgs = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
						}
						else
						{
							SqlDependency.EventContextPair eventContextPair = new SqlDependency.EventContextPair(value, this);
							if (this._eventList.Contains(eventContextPair))
							{
								throw SQL.SqlDependencyEventNoDuplicate();
							}
							this._eventList.Add(eventContextPair);
						}
					}
					if (sqlNotificationEventArgs != null)
					{
						value(this, sqlNotificationEventArgs);
					}
				}
			}
			remove
			{
				if (value != null)
				{
					SqlDependency.EventContextPair eventContextPair = new SqlDependency.EventContextPair(value, this);
					object eventHandlerLock = this._eventHandlerLock;
					lock (eventHandlerLock)
					{
						int num = this._eventList.IndexOf(eventContextPair);
						if (0 <= num)
						{
							this._eventList.RemoveAt(num);
						}
					}
				}
			}
		}

		/// <summary>Associates a <see cref="T:System.Data.SqlClient.SqlCommand" /> object with this <see cref="T:System.Data.SqlClient.SqlDependency" /> instance.</summary>
		/// <param name="command">A <see cref="T:System.Data.SqlClient.SqlCommand" /> object containing a statement that is valid for notifications. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property, and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001481 RID: 5249 RVA: 0x00065288 File Offset: 0x00063488
		public void AddCommandDependency(SqlCommand command)
		{
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			this.AddCommandInternal(command);
		}

		/// <summary>Starts the listener for receiving dependency change notifications from the instance of SQL Server specified by the connection string.</summary>
		/// <returns>true if the listener initialized successfully; false if a compatible listener already exists.</returns>
		/// <param name="connectionString">The connection string for the instance of SQL Server from which to obtain change notifications.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="connectionString" /> parameter is the same as a previous call to this method, but the parameters are different.The method was called from within the CLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A subsequent call to the method has been made with an equivalent <paramref name="connectionString" /> parameter with a different user, or a user that does not default to the same schema.Also, any underlying SqlClient exceptions.</exception>
		// Token: 0x06001482 RID: 5250 RVA: 0x0006529F File Offset: 0x0006349F
		public static bool Start(string connectionString)
		{
			return SqlDependency.Start(connectionString, null, true);
		}

		/// <summary>Starts the listener for receiving dependency change notifications from the instance of SQL Server specified by the connection string using the specified SQL Server Service Broker queue.</summary>
		/// <returns>true if the listener initialized successfully; false if a compatible listener already exists.</returns>
		/// <param name="connectionString">The connection string for the instance of SQL Server from which to obtain change notifications.</param>
		/// <param name="queue">An existing SQL Server Service Broker queue to be used. If null, the default queue is used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="connectionString" /> parameter is the same as a previous call to this method, but the parameters are different.The method was called from within the CLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A subsequent call to the method has been made with an equivalent <paramref name="connectionString" /> parameter but a different user, or a user that does not default to the same schema.Also, any underlying SqlClient exceptions.</exception>
		// Token: 0x06001483 RID: 5251 RVA: 0x000652A9 File Offset: 0x000634A9
		public static bool Start(string connectionString, string queue)
		{
			return SqlDependency.Start(connectionString, queue, false);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x000652B4 File Offset: 0x000634B4
		internal static bool Start(string connectionString, string queue, bool useDefaults)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				if (!useDefaults && string.IsNullOrEmpty(queue))
				{
					useDefaults = true;
					queue = null;
				}
				bool flag = false;
				bool flag2 = false;
				object obj = SqlDependency.s_startStopLock;
				lock (obj)
				{
					try
					{
						if (SqlDependency.s_processDispatcher == null)
						{
							SqlDependency.s_processDispatcher = SqlDependencyProcessDispatcher.SingletonProcessDispatcher;
						}
						if (useDefaults)
						{
							string text = null;
							DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
							string text2 = null;
							string text3 = null;
							string text4 = null;
							bool flag4 = false;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								flag2 = SqlDependency.s_processDispatcher.StartWithDefault(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref text4, SqlDependency.s_appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance, out flag, out flag4);
								goto IL_00FF;
							}
							finally
							{
								if (flag4 && !flag)
								{
									SqlDependency.IdentityUserNamePair identityUserNamePair = new SqlDependency.IdentityUserNamePair(dbConnectionPoolIdentity, text2);
									SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(text3, text4);
									if (!SqlDependency.AddToServerUserHash(text, identityUserNamePair, databaseServicePair))
									{
										try
										{
											SqlDependency.Stop(connectionString, queue, useDefaults, true);
										}
										catch (Exception ex)
										{
											if (!ADP.IsCatchableExceptionType(ex))
											{
												throw;
											}
											ADP.TraceExceptionWithoutRethrow(ex);
										}
										throw SQL.SqlDependencyDuplicateStart();
									}
								}
							}
						}
						flag2 = SqlDependency.s_processDispatcher.Start(connectionString, queue, SqlDependency.s_appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance);
						IL_00FF:;
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex2);
						throw;
					}
				}
				return flag2;
			}
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			throw ADP.Argument("connectionString");
		}

		/// <summary>Stops a listener for a connection specified in a previous <see cref="Overload:System.Data.SqlClient.SqlDependency.Start" /> call.</summary>
		/// <returns>true if the listener was completely stopped; false if the <see cref="T:System.AppDomain" /> was unbound from the listener, but there are is at least one other <see cref="T:System.AppDomain" /> using the same listener.</returns>
		/// <param name="connectionString">Connection string for the instance of SQL Server that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String)" /> call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An underlying SqlClient exception occurred.</exception>
		// Token: 0x06001485 RID: 5253 RVA: 0x00065414 File Offset: 0x00063614
		public static bool Stop(string connectionString)
		{
			return SqlDependency.Stop(connectionString, null, true, false);
		}

		/// <summary>Stops a listener for a connection specified in a previous <see cref="Overload:System.Data.SqlClient.SqlDependency.Start" /> call.</summary>
		/// <returns>true if the listener was completely stopped; false if the <see cref="T:System.AppDomain" /> was unbound from the listener, but there is at least one other <see cref="T:System.AppDomain" /> using the same listener.</returns>
		/// <param name="connectionString">Connection string for the instance of SQL Server that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String,System.String)" /> call.</param>
		/// <param name="queue">The SQL Server Service Broker queue that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String,System.String)" /> call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">And underlying SqlClient exception occurred.</exception>
		// Token: 0x06001486 RID: 5254 RVA: 0x0006541F File Offset: 0x0006361F
		public static bool Stop(string connectionString, string queue)
		{
			return SqlDependency.Stop(connectionString, queue, false, false);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0006542C File Offset: 0x0006362C
		internal static bool Stop(string connectionString, string queue, bool useDefaults, bool startFailed)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				if (!useDefaults && string.IsNullOrEmpty(queue))
				{
					useDefaults = true;
					queue = null;
				}
				bool flag = false;
				object obj = SqlDependency.s_startStopLock;
				lock (obj)
				{
					if (SqlDependency.s_processDispatcher != null)
					{
						try
						{
							string text = null;
							DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
							string text2 = null;
							string text3 = null;
							string text4 = null;
							if (useDefaults)
							{
								bool flag3 = false;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									flag = SqlDependency.s_processDispatcher.Stop(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref text4, SqlDependency.s_appDomainKey, out flag3);
									goto IL_00CB;
								}
								finally
								{
									if (flag3 && !startFailed)
									{
										SqlDependency.IdentityUserNamePair identityUserNamePair = new SqlDependency.IdentityUserNamePair(dbConnectionPoolIdentity, text2);
										SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(text3, text4);
										SqlDependency.RemoveFromServerUserHash(text, identityUserNamePair, databaseServicePair);
									}
								}
							}
							bool flag4;
							flag = SqlDependency.s_processDispatcher.Stop(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref queue, SqlDependency.s_appDomainKey, out flag4);
							IL_00CB:;
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
				return flag;
			}
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			throw ADP.Argument("connectionString");
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0006554C File Offset: 0x0006374C
		private static bool AddToServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			bool flag = false;
			Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
			lock (dictionary)
			{
				Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2;
				if (!SqlDependency.s_serverUserHash.ContainsKey(server))
				{
					dictionary2 = new Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>();
					SqlDependency.s_serverUserHash.Add(server, dictionary2);
				}
				else
				{
					dictionary2 = SqlDependency.s_serverUserHash[server];
				}
				List<SqlDependency.DatabaseServicePair> list;
				if (!dictionary2.ContainsKey(identityUser))
				{
					list = new List<SqlDependency.DatabaseServicePair>();
					dictionary2.Add(identityUser, list);
				}
				else
				{
					list = dictionary2[identityUser];
				}
				if (!list.Contains(databaseService))
				{
					list.Add(databaseService);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x000655F0 File Offset: 0x000637F0
		private static void RemoveFromServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
			lock (dictionary)
			{
				if (SqlDependency.s_serverUserHash.ContainsKey(server))
				{
					Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2 = SqlDependency.s_serverUserHash[server];
					if (dictionary2.ContainsKey(identityUser))
					{
						List<SqlDependency.DatabaseServicePair> list = dictionary2[identityUser];
						int num = list.IndexOf(databaseService);
						if (num >= 0)
						{
							list.RemoveAt(num);
							if (list.Count == 0)
							{
								dictionary2.Remove(identityUser);
								if (dictionary2.Count == 0)
								{
									SqlDependency.s_serverUserHash.Remove(server);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00065690 File Offset: 0x00063890
		internal static string GetDefaultComposedOptions(string server, string failoverServer, SqlDependency.IdentityUserNamePair identityUser, string database)
		{
			Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
			string text2;
			lock (dictionary)
			{
				if (!SqlDependency.s_serverUserHash.ContainsKey(server))
				{
					if (SqlDependency.s_serverUserHash.Count == 0)
					{
						throw SQL.SqlDepDefaultOptionsButNoStart();
					}
					if (string.IsNullOrEmpty(failoverServer) || !SqlDependency.s_serverUserHash.ContainsKey(failoverServer))
					{
						throw SQL.SqlDependencyNoMatchingServerStart();
					}
					server = failoverServer;
				}
				Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2 = SqlDependency.s_serverUserHash[server];
				List<SqlDependency.DatabaseServicePair> list = null;
				if (!dictionary2.ContainsKey(identityUser))
				{
					if (dictionary2.Count > 1)
					{
						throw SQL.SqlDependencyNoMatchingServerStart();
					}
					using (Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>.Enumerator enumerator = dictionary2.GetEnumerator())
					{
						if (!enumerator.MoveNext())
						{
							goto IL_00B6;
						}
						KeyValuePair<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> keyValuePair = enumerator.Current;
						list = keyValuePair.Value;
						goto IL_00B6;
					}
				}
				list = dictionary2[identityUser];
				IL_00B6:
				SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(database, null);
				SqlDependency.DatabaseServicePair databaseServicePair2 = null;
				int num = list.IndexOf(databaseServicePair);
				if (num != -1)
				{
					databaseServicePair2 = list[num];
				}
				if (databaseServicePair2 != null)
				{
					database = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Database);
					string text = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Service);
					text2 = "Service=" + text + ";Local Database=" + database;
				}
				else
				{
					if (list.Count != 1)
					{
						throw SQL.SqlDependencyNoMatchingServerDatabaseStart();
					}
					object[] array = list.ToArray();
					databaseServicePair2 = (SqlDependency.DatabaseServicePair)array[0];
					string text3 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Database);
					string text4 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Service);
					text2 = "Service=" + text4 + ";Local Database=" + text3;
				}
			}
			return text2;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00065844 File Offset: 0x00063A44
		internal void AddToServerList(string server)
		{
			List<string> serverList = this._serverList;
			lock (serverList)
			{
				int num = this._serverList.BinarySearch(server, StringComparer.OrdinalIgnoreCase);
				if (0 > num)
				{
					num = ~num;
					this._serverList.Insert(num, server);
				}
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000658A4 File Offset: 0x00063AA4
		internal bool ContainsServer(string server)
		{
			List<string> serverList = this._serverList;
			bool flag2;
			lock (serverList)
			{
				flag2 = this._serverList.Contains(server);
			}
			return flag2;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000658EC File Offset: 0x00063AEC
		internal string ComputeHashAndAddToDispatcher(SqlCommand command)
		{
			string text = this.ComputeCommandHash(command.Connection.ConnectionString, command);
			return SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddCommandEntry(text, this);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00065918 File Offset: 0x00063B18
		internal void Invalidate(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			List<SqlDependency.EventContextPair> list = null;
			object eventHandlerLock = this._eventHandlerLock;
			lock (eventHandlerLock)
			{
				if (this._dependencyFired && SqlNotificationInfo.AlreadyChanged != info && SqlNotificationSource.Client != source)
				{
					if (this.ExpirationTime >= DateTime.UtcNow)
					{
					}
				}
				else
				{
					this._dependencyFired = true;
					list = this._eventList;
					this._eventList = new List<SqlDependency.EventContextPair>();
				}
			}
			if (list != null)
			{
				foreach (SqlDependency.EventContextPair eventContextPair in list)
				{
					eventContextPair.Invoke(new SqlNotificationEventArgs(type, info, source));
				}
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x000659D8 File Offset: 0x00063BD8
		internal void StartTimer(SqlNotificationRequest notificationRequest)
		{
			if (this._expirationTime == DateTime.MaxValue)
			{
				int num = 432000;
				if (this._timeout != 0)
				{
					num = this._timeout;
				}
				if (notificationRequest != null && notificationRequest.Timeout < num && notificationRequest.Timeout != 0)
				{
					num = notificationRequest.Timeout;
				}
				this._expirationTime = DateTime.UtcNow.AddSeconds((double)num);
				SqlDependencyPerAppDomainDispatcher.SingletonInstance.StartTimer(this);
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00065A48 File Offset: 0x00063C48
		private void AddCommandInternal(SqlCommand cmd)
		{
			if (cmd != null)
			{
				SqlConnection connection = cmd.Connection;
				if (cmd.Notification != null)
				{
					if (cmd._sqlDep == null || cmd._sqlDep != this)
					{
						throw SQL.SqlCommandHasExistingSqlNotificationRequest();
					}
				}
				else
				{
					bool flag = false;
					object eventHandlerLock = this._eventHandlerLock;
					lock (eventHandlerLock)
					{
						if (!this._dependencyFired)
						{
							cmd.Notification = new SqlNotificationRequest
							{
								Timeout = this._timeout
							};
							if (this._options != null)
							{
								cmd.Notification.Options = this._options;
							}
							cmd._sqlDep = this;
						}
						else if (this._eventList.Count == 0)
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.Invalidate(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
					}
				}
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00065B14 File Offset: 0x00063D14
		private string ComputeCommandHash(string connectionString, SqlCommand command)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0};{1}", connectionString, command.CommandText);
			for (int i = 0; i < command.Parameters.Count; i++)
			{
				object value = command.Parameters[i].Value;
				if (value == null || value == DBNull.Value)
				{
					stringBuilder.Append("; NULL");
				}
				else
				{
					Type type = value.GetType();
					if (type == typeof(byte[]))
					{
						stringBuilder.Append(";");
						byte[] array = (byte[])value;
						for (int j = 0; j < array.Length; j++)
						{
							stringBuilder.Append(array[j].ToString("x2", CultureInfo.InvariantCulture));
						}
					}
					else if (type == typeof(char[]))
					{
						stringBuilder.Append((char[])value);
					}
					else if (type == typeof(XmlReader))
					{
						stringBuilder.Append(";");
						stringBuilder.Append(Guid.NewGuid().ToString());
					}
					else
					{
						stringBuilder.Append(";");
						stringBuilder.Append(value.ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00065C60 File Offset: 0x00063E60
		internal static string FixupServiceOrDatabaseName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return "\"" + name.Replace("\"", "\"\"") + "\"";
			}
			return name;
		}

		// Token: 0x04000D98 RID: 3480
		private readonly string _id = Guid.NewGuid().ToString() + ";" + SqlDependency.s_appDomainKey;

		// Token: 0x04000D99 RID: 3481
		private string _options;

		// Token: 0x04000D9A RID: 3482
		private int _timeout;

		// Token: 0x04000D9B RID: 3483
		private bool _dependencyFired;

		// Token: 0x04000D9C RID: 3484
		private List<SqlDependency.EventContextPair> _eventList = new List<SqlDependency.EventContextPair>();

		// Token: 0x04000D9D RID: 3485
		private object _eventHandlerLock = new object();

		// Token: 0x04000D9E RID: 3486
		private DateTime _expirationTime = DateTime.MaxValue;

		// Token: 0x04000D9F RID: 3487
		private List<string> _serverList = new List<string>();

		// Token: 0x04000DA0 RID: 3488
		private static object s_startStopLock = new object();

		// Token: 0x04000DA1 RID: 3489
		private static readonly string s_appDomainKey = Guid.NewGuid().ToString();

		// Token: 0x04000DA2 RID: 3490
		private static Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> s_serverUserHash = new Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000DA3 RID: 3491
		private static SqlDependencyProcessDispatcher s_processDispatcher = null;

		// Token: 0x04000DA4 RID: 3492
		private static readonly string s_assemblyName = typeof(SqlDependencyProcessDispatcher).Assembly.FullName;

		// Token: 0x04000DA5 RID: 3493
		private static readonly string s_typeName = typeof(SqlDependencyProcessDispatcher).FullName;

		// Token: 0x020001A6 RID: 422
		internal class IdentityUserNamePair
		{
			// Token: 0x06001494 RID: 5268 RVA: 0x00065CFD File Offset: 0x00063EFD
			internal IdentityUserNamePair(DbConnectionPoolIdentity identity, string userName)
			{
				this._identity = identity;
				this._userName = userName;
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06001495 RID: 5269 RVA: 0x00065D13 File Offset: 0x00063F13
			internal DbConnectionPoolIdentity Identity
			{
				get
				{
					return this._identity;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06001496 RID: 5270 RVA: 0x00065D1B File Offset: 0x00063F1B
			internal string UserName
			{
				get
				{
					return this._userName;
				}
			}

			// Token: 0x06001497 RID: 5271 RVA: 0x00065D24 File Offset: 0x00063F24
			public override bool Equals(object value)
			{
				SqlDependency.IdentityUserNamePair identityUserNamePair = (SqlDependency.IdentityUserNamePair)value;
				bool flag = false;
				if (identityUserNamePair == null)
				{
					flag = false;
				}
				else if (this == identityUserNamePair)
				{
					flag = true;
				}
				else if (this._identity != null)
				{
					if (this._identity.Equals(identityUserNamePair._identity))
					{
						flag = true;
					}
				}
				else if (this._userName == identityUserNamePair._userName)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001498 RID: 5272 RVA: 0x00065D80 File Offset: 0x00063F80
			public override int GetHashCode()
			{
				int num;
				if (this._identity != null)
				{
					num = this._identity.GetHashCode();
				}
				else
				{
					num = this._userName.GetHashCode();
				}
				return num;
			}

			// Token: 0x04000DA6 RID: 3494
			private DbConnectionPoolIdentity _identity;

			// Token: 0x04000DA7 RID: 3495
			private string _userName;
		}

		// Token: 0x020001A7 RID: 423
		private class DatabaseServicePair
		{
			// Token: 0x06001499 RID: 5273 RVA: 0x00065DB2 File Offset: 0x00063FB2
			internal DatabaseServicePair(string database, string service)
			{
				this._database = database;
				this._service = service;
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x0600149A RID: 5274 RVA: 0x00065DC8 File Offset: 0x00063FC8
			internal string Database
			{
				get
				{
					return this._database;
				}
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x0600149B RID: 5275 RVA: 0x00065DD0 File Offset: 0x00063FD0
			internal string Service
			{
				get
				{
					return this._service;
				}
			}

			// Token: 0x0600149C RID: 5276 RVA: 0x00065DD8 File Offset: 0x00063FD8
			public override bool Equals(object value)
			{
				SqlDependency.DatabaseServicePair databaseServicePair = (SqlDependency.DatabaseServicePair)value;
				bool flag = false;
				if (databaseServicePair == null)
				{
					flag = false;
				}
				else if (this == databaseServicePair)
				{
					flag = true;
				}
				else if (this._database == databaseServicePair._database)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x0600149D RID: 5277 RVA: 0x00065E13 File Offset: 0x00064013
			public override int GetHashCode()
			{
				return this._database.GetHashCode();
			}

			// Token: 0x04000DA8 RID: 3496
			private string _database;

			// Token: 0x04000DA9 RID: 3497
			private string _service;
		}

		// Token: 0x020001A8 RID: 424
		internal class EventContextPair
		{
			// Token: 0x0600149E RID: 5278 RVA: 0x00065E20 File Offset: 0x00064020
			internal EventContextPair(OnChangeEventHandler eventHandler, SqlDependency dependency)
			{
				this._eventHandler = eventHandler;
				this._context = ExecutionContext.Capture();
				this._dependency = dependency;
			}

			// Token: 0x0600149F RID: 5279 RVA: 0x00065E44 File Offset: 0x00064044
			public override bool Equals(object value)
			{
				SqlDependency.EventContextPair eventContextPair = (SqlDependency.EventContextPair)value;
				bool flag = false;
				if (eventContextPair == null)
				{
					flag = false;
				}
				else if (this == eventContextPair)
				{
					flag = true;
				}
				else if (this._eventHandler == eventContextPair._eventHandler)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x060014A0 RID: 5280 RVA: 0x00065E7F File Offset: 0x0006407F
			public override int GetHashCode()
			{
				return this._eventHandler.GetHashCode();
			}

			// Token: 0x060014A1 RID: 5281 RVA: 0x00065E8C File Offset: 0x0006408C
			internal void Invoke(SqlNotificationEventArgs args)
			{
				this._args = args;
				ExecutionContext.Run(this._context, SqlDependency.EventContextPair.s_contextCallback, this);
			}

			// Token: 0x060014A2 RID: 5282 RVA: 0x00065EA8 File Offset: 0x000640A8
			private static void InvokeCallback(object eventContextPair)
			{
				SqlDependency.EventContextPair eventContextPair2 = (SqlDependency.EventContextPair)eventContextPair;
				eventContextPair2._eventHandler(eventContextPair2._dependency, eventContextPair2._args);
			}

			// Token: 0x04000DAA RID: 3498
			private OnChangeEventHandler _eventHandler;

			// Token: 0x04000DAB RID: 3499
			private ExecutionContext _context;

			// Token: 0x04000DAC RID: 3500
			private SqlDependency _dependency;

			// Token: 0x04000DAD RID: 3501
			private SqlNotificationEventArgs _args;

			// Token: 0x04000DAE RID: 3502
			private static ContextCallback s_contextCallback = new ContextCallback(SqlDependency.EventContextPair.InvokeCallback);
		}
	}
}
