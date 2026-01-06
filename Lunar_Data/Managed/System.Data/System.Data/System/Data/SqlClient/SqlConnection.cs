using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents an open connection to a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000177 RID: 375
	public sealed class SqlConnection : DbConnection, ICloneable, IDbConnection, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection" /> class when given a string that contains the connection string.</summary>
		/// <param name="connectionString">The connection used to open the SQL Server database.</param>
		// Token: 0x06001239 RID: 4665 RVA: 0x0005A211 File Offset: 0x00058411
		public SqlConnection(string connectionString)
			: this()
		{
			this.ConnectionString = connectionString;
			this.CacheConnectionStringProperties();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection" /> class given a connection string, that does not use Integrated Security = true and a <see cref="T:System.Data.SqlClient.SqlCredential" /> object that contains the user ID and password.</summary>
		/// <param name="connectionString">A connection string that does not use any of the following connection string keywords: Integrated Security = true, UserId, or Password; or that does not use ContextConnection = true.</param>
		/// <param name="credential">A <see cref="T:System.Data.SqlClient.SqlCredential" /> object. If <paramref name="credential" /> is null, <see cref="M:System.Data.SqlClient.SqlConnection.#ctor(System.String,System.Data.SqlClient.SqlCredential)" /> is functionally equivalent to <see cref="M:System.Data.SqlClient.SqlConnection.#ctor(System.String)" />.</param>
		// Token: 0x0600123A RID: 4666 RVA: 0x0005A228 File Offset: 0x00058428
		public SqlConnection(string connectionString, SqlCredential credential)
			: this()
		{
			this.ConnectionString = connectionString;
			if (credential != null)
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (this.UsesClearUserIdOrPassword(sqlConnectionString))
				{
					throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
				}
				if (this.UsesIntegratedSecurity(sqlConnectionString))
				{
					throw ADP.InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity();
				}
				this.Credential = credential;
			}
			this.CacheConnectionStringProperties();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0005A27C File Offset: 0x0005847C
		private SqlConnection(SqlConnection connection)
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			base..ctor();
			GC.SuppressFinalize(this);
			this.CopyFrom(connection);
			this._connectionString = connection._connectionString;
			if (connection._credential != null)
			{
				SecureString secureString = connection._credential.Password.Copy();
				secureString.MakeReadOnly();
				this._credential = new SqlCredential(connection._credential.UserId, secureString);
			}
			this._accessToken = connection._accessToken;
			this.CacheConnectionStringProperties();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0005A308 File Offset: 0x00058508
		private void CacheConnectionStringProperties()
		{
			SqlConnectionString sqlConnectionString = this.ConnectionOptions as SqlConnectionString;
			if (sqlConnectionString != null)
			{
				this._connectRetryCount = sqlConnectionString.ConnectRetryCount;
			}
		}

		/// <summary>When set to true, enables statistics gathering for the current connection.</summary>
		/// <returns>Returns true if statistics gathering is enabled; otherwise false. false is the default.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0005A330 File Offset: 0x00058530
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x0005A338 File Offset: 0x00058538
		public bool StatisticsEnabled
		{
			get
			{
				return this._collectstats;
			}
			set
			{
				if (value)
				{
					if (ConnectionState.Open == this.State)
					{
						if (this._statistics == null)
						{
							this._statistics = new SqlStatistics();
							ADP.TimerCurrent(out this._statistics._openTimestamp);
						}
						this.Parser.Statistics = this._statistics;
					}
				}
				else if (this._statistics != null && ConnectionState.Open == this.State)
				{
					this.Parser.Statistics = null;
					ADP.TimerCurrent(out this._statistics._closeTimestamp);
				}
				this._collectstats = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0005A3BB File Offset: 0x000585BB
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x0005A3C3 File Offset: 0x000585C3
		internal bool AsyncCommandInProgress
		{
			get
			{
				return this._AsyncCommandInProgress;
			}
			set
			{
				this._AsyncCommandInProgress = value;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0005A3CC File Offset: 0x000585CC
		private bool UsesIntegratedSecurity(SqlConnectionString opt)
		{
			return opt != null && opt.IntegratedSecurity;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0005A3DC File Offset: 0x000585DC
		private bool UsesClearUserIdOrPassword(SqlConnectionString opt)
		{
			bool flag = false;
			if (opt != null)
			{
				flag = !string.IsNullOrEmpty(opt.UserID) || !string.IsNullOrEmpty(opt.Password);
			}
			return flag;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0005A40E File Offset: 0x0005860E
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TransactionBinding;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0005A420 File Offset: 0x00058620
		internal SqlConnectionString.TypeSystem TypeSystem
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemVersion;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0005A432 File Offset: 0x00058632
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).TypeSystemAssemblyVersion;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0005A444 File Offset: 0x00058644
		internal int ConnectRetryInterval
		{
			get
			{
				return ((SqlConnectionString)this.ConnectionOptions).ConnectRetryInterval;
			}
		}

		/// <summary>Gets or sets the string used to open a SQL Server database.</summary>
		/// <returns>The connection string that includes the source database name, and other parameters needed to establish the initial connection. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied, or a required connection string argument has not been supplied. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0005A456 File Offset: 0x00058656
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0005A460 File Offset: 0x00058660
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				if (this._credential != null || this._accessToken != null)
				{
					SqlConnectionString sqlConnectionString = new SqlConnectionString(value);
					if (this._credential != null)
					{
						this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(sqlConnectionString);
					}
					else
					{
						this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(sqlConnectionString);
					}
				}
				this.ConnectionString_Set(new SqlConnectionPoolKey(value, this._credential, this._accessToken));
				this._connectionString = value;
				this.CacheConnectionStringProperties();
			}
		}

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0005A4C4 File Offset: 0x000586C4
		public override int ConnectionTimeout
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				if (sqlConnectionString == null)
				{
					return 15;
				}
				return sqlConnectionString.ConnectTimeout;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0005A4EC File Offset: 0x000586EC
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0005A52C File Offset: 0x0005872C
		public string AccessToken
		{
			get
			{
				string accessToken = this._accessToken;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (!this.InnerConnection.ShouldHidePassword || sqlConnectionString == null || sqlConnectionString.PersistSecurityInfo)
				{
					return this._accessToken;
				}
				return null;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("AccessToken", this.InnerConnection.State);
				}
				if (value != null)
				{
					this.CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken((SqlConnectionString)this.ConnectionOptions);
				}
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, value));
				this._accessToken = value;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0005A590 File Offset: 0x00058790
		public override string Database
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string text;
				if (sqlInternalConnection != null)
				{
					text = sqlInternalConnection.CurrentDatabase;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					text = ((sqlConnectionString != null) ? sqlConnectionString.InitialCatalog : "");
				}
				return text;
			}
		}

		/// <summary>Gets the name of the instance of SQL Server to which to connect.</summary>
		/// <returns>The name of the instance of SQL Server to which to connect. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0005A5D4 File Offset: 0x000587D4
		public override string DataSource
		{
			get
			{
				SqlInternalConnection sqlInternalConnection = this.InnerConnection as SqlInternalConnection;
				string text;
				if (sqlInternalConnection != null)
				{
					text = sqlInternalConnection.CurrentDataSource;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					text = ((sqlConnectionString != null) ? sqlConnectionString.DataSource : "");
				}
				return text;
			}
		}

		/// <summary>Gets the size (in bytes) of network packets used to communicate with an instance of SQL Server.</summary>
		/// <returns>The size (in bytes) of network packets. The default value is 8000.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0005A618 File Offset: 0x00058818
		public int PacketSize
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				int num;
				if (sqlInternalConnectionTds != null)
				{
					num = sqlInternalConnectionTds.PacketSize;
				}
				else
				{
					SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
					num = ((sqlConnectionString != null) ? sqlConnectionString.PacketSize : 8000);
				}
				return num;
			}
		}

		/// <summary>The connection ID of the most recent connection attempt, regardless of whether the attempt succeeded or failed.</summary>
		/// <returns>The connection ID of the most recent connection attempt.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0005A65C File Offset: 0x0005885C
		public Guid ClientConnectionId
		{
			get
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					return sqlInternalConnectionTds.ClientConnectionId;
				}
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return this._originalConnectionId;
				}
				return Guid.Empty;
			}
		}

		/// <summary>Gets a string that contains the version of the instance of SQL Server to which the client is connected.</summary>
		/// <returns>The version of the instance of SQL Server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed. <see cref="P:System.Data.SqlClient.SqlConnection.ServerVersion" /> was called while the returned Task was not completed and the connection was not opened after a call to <see cref="M:System.Data.SqlClient.SqlConnection.OpenAsync(System.Threading.CancellationToken)" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0005A69D File Offset: 0x0005889D
		public override string ServerVersion
		{
			get
			{
				return this.GetOpenTdsConnection().ServerVersion;
			}
		}

		/// <summary>Indicates the state of the <see cref="T:System.Data.SqlClient.SqlConnection" /> during the most recent network operation performed on the connection.</summary>
		/// <returns>An <see cref="T:System.Data.ConnectionState" /> enumeration.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0005A6AC File Offset: 0x000588AC
		public override ConnectionState State
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return ConnectionState.Open;
				}
				return this.InnerConnection.State;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0005A6D8 File Offset: 0x000588D8
		internal SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		/// <summary>Gets a string that identifies the database client.</summary>
		/// <returns>A string that identifies the database client. If not specified, the name of the client computer. If neither is specified, the value is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0005A6E0 File Offset: 0x000588E0
		public string WorkstationId
		{
			get
			{
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
				return ((sqlConnectionString != null) ? sqlConnectionString.WorkstationId : null) ?? Environment.MachineName;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCredential" /> object for this connection.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCredential" /> object for this connection.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0005A704 File Offset: 0x00058904
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x0005A740 File Offset: 0x00058940
		public SqlCredential Credential
		{
			get
			{
				SqlCredential sqlCredential = this._credential;
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this.UserConnectionOptions;
				if (this.InnerConnection.ShouldHidePassword && sqlConnectionString != null && !sqlConnectionString.PersistSecurityInfo)
				{
					sqlCredential = null;
				}
				return sqlCredential;
			}
			set
			{
				if (!this.InnerConnection.AllowSetConnectionString)
				{
					throw ADP.OpenConnectionPropertySet("Credential", this.InnerConnection.State);
				}
				if (value != null)
				{
					this.CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential((SqlConnectionString)this.ConnectionOptions);
					if (this._accessToken != null)
					{
						throw ADP.InvalidMixedUsageOfCredentialAndAccessToken();
					}
				}
				this._credential = value;
				this.ConnectionString_Set(new SqlConnectionPoolKey(this._connectionString, this._credential, this._accessToken));
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0005A7B6 File Offset: 0x000589B6
		private void CheckAndThrowOnInvalidCombinationOfConnectionStringAndSqlCredential(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureAndClearCredential();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity();
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0005A7D6 File Offset: 0x000589D6
		private void CheckAndThrowOnInvalidCombinationOfConnectionOptionAndAccessToken(SqlConnectionString connectionOptions)
		{
			if (this.UsesClearUserIdOrPassword(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndUserIDPassword();
			}
			if (this.UsesIntegratedSecurity(connectionOptions))
			{
				throw ADP.InvalidMixedUsageOfAccessTokenAndIntegratedSecurity();
			}
			if (this._credential != null)
			{
				throw ADP.InvalidMixedUsageOfCredentialAndAccessToken();
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0005A804 File Offset: 0x00058A04
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		/// <summary>Occurs when SQL Server returns a warning or informational message.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001259 RID: 4697 RVA: 0x0005A80C File Offset: 0x00058A0C
		// (remove) Token: 0x0600125A RID: 4698 RVA: 0x0005A844 File Offset: 0x00058A44
		public event SqlInfoMessageEventHandler InfoMessage;

		/// <summary>Gets or sets the <see cref="P:System.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors" /> property.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors" /> property has been set; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0005A879 File Offset: 0x00058A79
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x0005A881 File Offset: 0x00058A81
		public bool FireInfoMessageEventOnUserErrors
		{
			get
			{
				return this._fireInfoMessageEventOnUserErrors;
			}
			set
			{
				this._fireInfoMessageEventOnUserErrors = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0005A88A File Offset: 0x00058A8A
		internal int ReconnectCount
		{
			get
			{
				return this._reconnectCount;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0005A892 File Offset: 0x00058A92
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x0005A89A File Offset: 0x00058A9A
		internal bool ForceNewConnection { get; set; }

		// Token: 0x06001260 RID: 4704 RVA: 0x0005A8A3 File Offset: 0x00058AA3
		protected override void OnStateChange(StateChangeEventArgs stateChange)
		{
			if (!this._suppressStateChangeForReconnection)
			{
				base.OnStateChange(stateChange);
			}
		}

		/// <summary>Starts a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
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
		// Token: 0x06001261 RID: 4705 RVA: 0x0005A8B4 File Offset: 0x00058AB4
		public new SqlTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, null);
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="iso">The isolation level under which the transaction should run. </param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
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
		// Token: 0x06001262 RID: 4706 RVA: 0x0005A8BE File Offset: 0x00058ABE
		public new SqlTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginTransaction(iso, null);
		}

		/// <summary>Starts a database transaction with the specified transaction name.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="transactionName">The name of the transaction. </param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001263 RID: 4707 RVA: 0x0005A8C8 File Offset: 0x00058AC8
		public SqlTransaction BeginTransaction(string transactionName)
		{
			return this.BeginTransaction(IsolationLevel.Unspecified, transactionName);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0005A8D2 File Offset: 0x00058AD2
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			DbTransaction dbTransaction = this.BeginTransaction(isolationLevel);
			GC.KeepAlive(this);
			return dbTransaction;
		}

		/// <summary>Starts a database transaction with the specified isolation level and transaction name.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="iso">The isolation level under which the transaction should run. </param>
		/// <param name="transactionName">The name of the transaction. </param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Parallel transactions are not allowed when using Multiple Active Result Sets (MARS).</exception>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001265 RID: 4709 RVA: 0x0005A8E4 File Offset: 0x00058AE4
		public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName)
		{
			this.WaitForPendingReconnection();
			SqlStatistics sqlStatistics = null;
			SqlTransaction sqlTransaction2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				bool flag = true;
				SqlTransaction sqlTransaction;
				do
				{
					sqlTransaction = this.GetOpenTdsConnection().BeginSqlTransaction(iso, transactionName, flag);
					flag = false;
				}
				while (sqlTransaction.InternalTransaction.ConnectionHasBeenRestored);
				GC.KeepAlive(this);
				sqlTransaction2 = sqlTransaction;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlTransaction2;
		}

		/// <summary>Changes the current database for an open <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <param name="database">The name of the database to use instead of the current database. </param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open. </exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Cannot change the database. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001266 RID: 4710 RVA: 0x0005A948 File Offset: 0x00058B48
		public override void ChangeDatabase(string database)
		{
			SqlStatistics sqlStatistics = null;
			this.RepairInnerConnection();
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.InnerConnection.ChangeDatabase(database);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Empties the connection pool.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001267 RID: 4711 RVA: 0x0005A990 File Offset: 0x00058B90
		public static void ClearAllPools()
		{
			SqlConnectionFactory.SingletonInstance.ClearAllPools();
		}

		/// <summary>Empties the connection pool associated with the specified connection.</summary>
		/// <param name="connection">The <see cref="T:System.Data.SqlClient.SqlConnection" /> to be cleared from the pool.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001268 RID: 4712 RVA: 0x0005A99C File Offset: 0x00058B9C
		public static void ClearPool(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionOptions userConnectionOptions = connection.UserConnectionOptions;
			if (userConnectionOptions != null)
			{
				SqlConnectionFactory.SingletonInstance.ClearPool(connection);
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0005A9C9 File Offset: 0x00058BC9
		private void CloseInnerConnection()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
		}

		/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
		/// <exception cref="T:System.Data.SqlClient.SqlException">The connection-level error that occurred while opening the connection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600126A RID: 4714 RVA: 0x0005A9E0 File Offset: 0x00058BE0
		public override void Close()
		{
			ConnectionState state = this.State;
			Guid guid = default(Guid);
			Guid guid2 = default(Guid);
			if (state != ConnectionState.Closed)
			{
				guid = SqlConnection.s_diagnosticListener.WriteConnectionCloseBefore(this, "Close");
				guid2 = this.ClientConnectionId;
			}
			SqlStatistics sqlStatistics = null;
			Exception ex = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					CancellationTokenSource reconnectionCancellationSource = this._reconnectionCancellationSource;
					if (reconnectionCancellationSource != null)
					{
						reconnectionCancellationSource.Cancel();
					}
					AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
					if (this.State != ConnectionState.Open)
					{
						this.OnStateChange(DbConnectionInternal.StateChangeClosed);
					}
				}
				this.CancelOpenAndWait();
				this.CloseInnerConnection();
				GC.SuppressFinalize(this);
				if (this.Statistics != null)
				{
					ADP.TimerCurrent(out this._statistics._closeTimestamp);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				if (state != ConnectionState.Closed)
				{
					if (ex != null)
					{
						SqlConnection.s_diagnosticListener.WriteConnectionCloseError(guid, guid2, this, ex, "Close");
					}
					else
					{
						SqlConnection.s_diagnosticListener.WriteConnectionCloseAfter(guid, guid2, this, "Close");
					}
				}
			}
		}

		/// <summary>Creates and returns a <see cref="T:System.Data.SqlClient.SqlCommand" /> object associated with the <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600126B RID: 4715 RVA: 0x0005AAF8 File Offset: 0x00058CF8
		public new SqlCommand CreateCommand()
		{
			return new SqlCommand(null, this);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0005AB04 File Offset: 0x00058D04
		private void DisposeMe(bool disposing)
		{
			this._credential = null;
			this._accessToken = null;
			if (!disposing)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null && !sqlInternalConnectionTds.ConnectionOptions.Pooling)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser != null && parser._physicalStateObj != null)
					{
						parser._physicalStateObj.DecrementPendingCallbacks(false);
					}
				}
			}
		}

		/// <summary>Opens a database connection with the property settings specified by the <see cref="P:System.Data.SqlClient.SqlConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">Cannot open a connection without specifying a data source or server.orThe connection is already open.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A connection-level error occurred while opening the connection. If the <see cref="P:System.Data.SqlClient.SqlException.Number" /> property contains the value 18487 or 18488, this indicates that the specified password has expired or must be reset. See the <see cref="M:System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String)" /> method for more information.The &lt;system.data.localdb&gt; tag in the app.config file has invalid or unknown elements.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There are two entries with the same name in the &lt;localdbinstances&gt; section.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600126D RID: 4717 RVA: 0x0005AB60 File Offset: 0x00058D60
		public override void Open()
		{
			Guid guid = SqlConnection.s_diagnosticListener.WriteConnectionOpenBefore(this, "Open");
			this.PrepareStatisticsForNewConnection();
			SqlStatistics sqlStatistics = null;
			Exception ex = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (!this.TryOpen(null))
				{
					throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				if (ex != null)
				{
					SqlConnection.s_diagnosticListener.WriteConnectionOpenError(guid, this, ex, "Open");
				}
				else
				{
					SqlConnection.s_diagnosticListener.WriteConnectionOpenAfter(guid, this, "Open");
				}
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0005ABF8 File Offset: 0x00058DF8
		internal void RegisterWaitingForReconnect(Task waitingTask)
		{
			if (((SqlConnectionString)this.ConnectionOptions).MARS)
			{
				return;
			}
			Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, waitingTask, null);
			if (this._asyncWaitingForReconnection != waitingTask)
			{
				throw SQL.MARSUnspportedOnConnection();
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0005AC2C File Offset: 0x00058E2C
		private async Task ReconnectAsync(int timeout)
		{
			try
			{
				long commandTimeoutExpiration = 0L;
				if (timeout > 0)
				{
					commandTimeoutExpiration = ADP.TimerCurrent() + ADP.TimerFromSeconds(timeout);
				}
				CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
				this._reconnectionCancellationSource = cancellationTokenSource;
				CancellationToken ctoken = cancellationTokenSource.Token;
				int retryCount = this._connectRetryCount;
				for (int attempt = 0; attempt < retryCount; attempt++)
				{
					if (ctoken.IsCancellationRequested)
					{
						return;
					}
					try
					{
						try
						{
							this.ForceNewConnection = true;
							await this.OpenAsync(ctoken).ConfigureAwait(false);
							this._reconnectCount++;
						}
						finally
						{
							this.ForceNewConnection = false;
						}
						return;
					}
					catch (SqlException ex)
					{
						if (attempt == retryCount - 1)
						{
							throw SQL.CR_AllAttemptsFailed(ex, this._originalConnectionId);
						}
						if (timeout > 0 && ADP.TimerRemaining(commandTimeoutExpiration) < ADP.TimerFromSeconds(this.ConnectRetryInterval))
						{
							throw SQL.CR_NextAttemptWillExceedQueryTimeout(ex, this._originalConnectionId);
						}
					}
					await Task.Delay(1000 * this.ConnectRetryInterval, ctoken).ConfigureAwait(false);
				}
				ctoken = default(CancellationToken);
			}
			finally
			{
				this._recoverySessionData = null;
				this._suppressStateChangeForReconnection = false;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0005AC78 File Offset: 0x00058E78
		internal Task ValidateAndReconnect(Action beforeDisconnect, int timeout)
		{
			Task task = this._currentReconnectionTask;
			while (task != null && task.IsCompleted)
			{
				Interlocked.CompareExchange<Task>(ref this._currentReconnectionTask, null, task);
				task = this._currentReconnectionTask;
			}
			if (task == null)
			{
				if (this._connectRetryCount > 0)
				{
					SqlInternalConnectionTds openTdsConnection = this.GetOpenTdsConnection();
					if (openTdsConnection._sessionRecoveryAcknowledged && !openTdsConnection.Parser._physicalStateObj.ValidateSNIConnection())
					{
						if (openTdsConnection.Parser._sessionPool != null && openTdsConnection.Parser._sessionPool.ActiveSessionsCount > 0)
						{
							if (beforeDisconnect != null)
							{
								beforeDisconnect();
							}
							this.OnError(SQL.CR_UnrecoverableClient(this.ClientConnectionId), true, null);
						}
						SessionData currentSessionData = openTdsConnection.CurrentSessionData;
						if (currentSessionData._unrecoverableStatesCount == 0)
						{
							bool flag = false;
							object reconnectLock = this._reconnectLock;
							lock (reconnectLock)
							{
								openTdsConnection.CheckEnlistedTransactionBinding();
								task = this._currentReconnectionTask;
								if (task == null)
								{
									if (currentSessionData._unrecoverableStatesCount == 0)
									{
										this._originalConnectionId = this.ClientConnectionId;
										this._recoverySessionData = currentSessionData;
										if (beforeDisconnect != null)
										{
											beforeDisconnect();
										}
										try
										{
											this._suppressStateChangeForReconnection = true;
											openTdsConnection.DoomThisConnection();
										}
										catch (SqlException)
										{
										}
										task = Task.Run(() => this.ReconnectAsync(timeout));
										this._currentReconnectionTask = task;
									}
								}
								else
								{
									flag = true;
								}
							}
							if (flag && beforeDisconnect != null)
							{
								beforeDisconnect();
							}
						}
						else
						{
							if (beforeDisconnect != null)
							{
								beforeDisconnect();
							}
							this.OnError(SQL.CR_UnrecoverableServer(this.ClientConnectionId), true, null);
						}
					}
				}
			}
			else if (beforeDisconnect != null)
			{
				beforeDisconnect();
			}
			return task;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0005AE28 File Offset: 0x00059028
		private void WaitForPendingReconnection()
		{
			Task currentReconnectionTask = this._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				AsyncHelper.WaitForCompletion(currentReconnectionTask, 0, null, false);
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0005AE50 File Offset: 0x00059050
		private void CancelOpenAndWait()
		{
			Tuple<TaskCompletionSource<DbConnectionInternal>, Task> currentCompletion = this._currentCompletion;
			if (currentCompletion != null)
			{
				currentCompletion.Item1.TrySetCanceled();
				((IAsyncResult)currentCompletion.Item2).AsyncWaitHandle.WaitOne();
			}
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlConnection.Open" />, which opens a database connection with the property settings specified by the <see cref="P:System.Data.SqlClient.SqlConnection.ConnectionString" />. The cancellation token can be used to request that the operation be abandoned before the connection timeout elapses.  Exceptions will be propagated via the returned Task. If the connection timeout time elapses without successfully connecting, the returned Task will be marked as faulted with an Exception. The implementation returns a Task without blocking the calling thread for both pooled and non-pooled connections.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlConnection.OpenAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.Context Connection=true is specified in the connection string.A connection was not available from the connection pool before the connection time out elapsed.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x06001273 RID: 4723 RVA: 0x0005AE84 File Offset: 0x00059084
		public override Task OpenAsync(CancellationToken cancellationToken)
		{
			Guid operationId = SqlConnection.s_diagnosticListener.WriteConnectionOpenBefore(this, "OpenAsync");
			this.PrepareStatisticsForNewConnection();
			SqlStatistics sqlStatistics = null;
			Task task;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<DbConnectionInternal> taskCompletionSource = new TaskCompletionSource<DbConnectionInternal>(ADP.GetCurrentTransaction());
				TaskCompletionSource<object> taskCompletionSource2 = new TaskCompletionSource<object>();
				if (SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter") || SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteConnectionOpenError"))
				{
					taskCompletionSource2.Task.ContinueWith(delegate(Task<object> t)
					{
						if (t.Exception != null)
						{
							SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, t.Exception, "OpenAsync");
							return;
						}
						SqlConnection.s_diagnosticListener.WriteConnectionOpenAfter(operationId, this, "OpenAsync");
					}, TaskScheduler.Default);
				}
				if (cancellationToken.IsCancellationRequested)
				{
					taskCompletionSource2.SetCanceled();
					task = taskCompletionSource2.Task;
				}
				else
				{
					bool flag;
					try
					{
						flag = this.TryOpen(taskCompletionSource);
					}
					catch (Exception ex)
					{
						SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, ex, "OpenAsync");
						taskCompletionSource2.SetException(ex);
						return taskCompletionSource2.Task;
					}
					if (flag)
					{
						taskCompletionSource2.SetResult(null);
						task = taskCompletionSource2.Task;
					}
					else
					{
						CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
						if (cancellationToken.CanBeCanceled)
						{
							cancellationTokenRegistration = cancellationToken.Register(delegate(object s)
							{
								((TaskCompletionSource<DbConnectionInternal>)s).TrySetCanceled();
							}, taskCompletionSource);
						}
						SqlConnection.OpenAsyncRetry openAsyncRetry = new SqlConnection.OpenAsyncRetry(this, taskCompletionSource, taskCompletionSource2, cancellationTokenRegistration);
						this._currentCompletion = new Tuple<TaskCompletionSource<DbConnectionInternal>, Task>(taskCompletionSource, taskCompletionSource2.Task);
						taskCompletionSource.Task.ContinueWith(new Action<Task<DbConnectionInternal>>(openAsyncRetry.Retry), TaskScheduler.Default);
						task = taskCompletionSource2.Task;
					}
				}
			}
			catch (Exception ex2)
			{
				SqlConnection.s_diagnosticListener.WriteConnectionOpenError(operationId, this, ex2, "OpenAsync");
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.SqlClient.SqlConnection" />. For more information about scheme, see SQL Server Schema Collections.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06001274 RID: 4724 RVA: 0x0005B07C File Offset: 0x0005927C
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.SqlClient.SqlConnection" /> using the specified string for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information. </returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06001275 RID: 4725 RVA: 0x0005B08A File Offset: 0x0005928A
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.SqlClient.SqlConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information. </returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">A set of restriction values for the requested schema.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06001276 RID: 4726 RVA: 0x0005B094 File Offset: 0x00059294
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0005B0B0 File Offset: 0x000592B0
		private void PrepareStatisticsForNewConnection()
		{
			if (this.StatisticsEnabled || SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteCommandAfter") || SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter"))
			{
				if (this._statistics == null)
				{
					this._statistics = new SqlStatistics();
					return;
				}
				this._statistics.ContinueOnNewConnection();
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0005B108 File Offset: 0x00059308
		private bool TryOpen(TaskCompletionSource<DbConnectionInternal> retry)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)this.ConnectionOptions;
			this._applyTransientFaultHandling = retry == null && sqlConnectionString != null && sqlConnectionString.ConnectRetryCount > 0;
			if (this.ForceNewConnection)
			{
				if (!this.InnerConnection.TryReplaceConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
				{
					return false;
				}
			}
			else if (!this.InnerConnection.TryOpenConnection(this, this.ConnectionFactory, retry, this.UserConnectionOptions))
			{
				return false;
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = (SqlInternalConnectionTds)this.InnerConnection;
			if (!sqlInternalConnectionTds.ConnectionOptions.Pooling)
			{
				GC.ReRegisterForFinalize(this);
			}
			SqlStatistics statistics = this._statistics;
			if (this.StatisticsEnabled || (SqlConnection.s_diagnosticListener.IsEnabled("System.Data.SqlClient.WriteCommandAfter") && statistics != null))
			{
				ADP.TimerCurrent(out this._statistics._openTimestamp);
				sqlInternalConnectionTds.Parser.Statistics = this._statistics;
			}
			else
			{
				sqlInternalConnectionTds.Parser.Statistics = null;
				this._statistics = null;
			}
			return true;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0005B1F4 File Offset: 0x000593F4
		internal bool HasLocalTransaction
		{
			get
			{
				return this.GetOpenTdsConnection().HasLocalTransaction;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0005B204 File Offset: 0x00059404
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				return (currentReconnectionTask == null || currentReconnectionTask.IsCompleted) && this.GetOpenTdsConnection().HasLocalTransactionFromAPI;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0005B230 File Offset: 0x00059430
		internal bool IsKatmaiOrNewer
		{
			get
			{
				return this._currentReconnectionTask != null || this.GetOpenTdsConnection().IsKatmaiOrNewer;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0005B247 File Offset: 0x00059447
		internal TdsParser Parser
		{
			get
			{
				return this.GetOpenTdsConnection().Parser;
			}
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0005B254 File Offset: 0x00059454
		internal void ValidateConnectionForExecute(string method, SqlCommand command)
		{
			Task asyncWaitingForReconnection = this._asyncWaitingForReconnection;
			if (asyncWaitingForReconnection != null)
			{
				if (!asyncWaitingForReconnection.IsCompleted)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				Interlocked.CompareExchange<Task>(ref this._asyncWaitingForReconnection, null, asyncWaitingForReconnection);
			}
			if (this._currentReconnectionTask != null)
			{
				Task currentReconnectionTask = this._currentReconnectionTask;
				if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
				{
					return;
				}
			}
			this.GetOpenTdsConnection(method).ValidateConnectionForExecute(command);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0005B2AF File Offset: 0x000594AF
		internal static string FixupDatabaseTransactionName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return SqlServerEscapeHelper.EscapeIdentifier(name);
			}
			return name;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0005B2C4 File Offset: 0x000594C4
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
		{
			if (breakConnection && ConnectionState.Open == this.State)
			{
				if (wrapCloseInAction != null)
				{
					int capturedCloseCount = this._closeCount;
					Action action = delegate
					{
						if (capturedCloseCount == this._closeCount)
						{
							this.Close();
						}
					};
					wrapCloseInAction(action);
				}
				else
				{
					this.Close();
				}
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
			this.OnInfoMessage(new SqlInfoMessageEventArgs(exception));
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0005B330 File Offset: 0x00059530
		internal SqlInternalConnectionTds GetOpenTdsConnection()
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.ClosedConnectionError();
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0005B354 File Offset: 0x00059554
		internal SqlInternalConnectionTds GetOpenTdsConnection(string method)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds == null)
			{
				throw ADP.OpenConnectionRequired(method, this.InnerConnection.State);
			}
			return sqlInternalConnectionTds;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0005B384 File Offset: 0x00059584
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent)
		{
			bool flag;
			this.OnInfoMessage(imevent, out flag);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0005B39C File Offset: 0x0005959C
		internal void OnInfoMessage(SqlInfoMessageEventArgs imevent, out bool notified)
		{
			SqlInfoMessageEventHandler infoMessage = this.InfoMessage;
			if (infoMessage != null)
			{
				notified = true;
				try
				{
					infoMessage(this, imevent);
					return;
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					return;
				}
			}
			notified = false;
		}

		/// <summary>Changes the SQL Server password for the user indicated in the connection string to the supplied new password.</summary>
		/// <param name="connectionString">The connection string that contains enough information to connect to the server that you want. The connection string must contain the user ID and the current password.</param>
		/// <param name="newPassword">The new password to set. This password must comply with any password security policy set on the server, including minimum length, requirements for specific characters, and so on.</param>
		/// <exception cref="T:System.ArgumentException">The connection string includes the option to use integrated security. Or The <paramref name="newPassword" /> exceeds 128 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either the <paramref name="connectionString" /> or the <paramref name="newPassword" /> parameter is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001284 RID: 4740 RVA: 0x0005B3E0 File Offset: 0x000595E0
		public static void ChangePassword(string connectionString, string newPassword)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw SQL.ChangePasswordArgumentMissing("newPassword");
			}
			if (string.IsNullOrEmpty(newPassword))
			{
				throw SQL.ChangePasswordArgumentMissing("newPassword");
			}
			if (128 < newPassword.Length)
			{
				throw ADP.InvalidArgumentLength("newPassword", 128);
			}
			SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(new SqlConnectionPoolKey(connectionString, null, null));
			if (sqlConnectionString.IntegratedSecurity)
			{
				throw SQL.ChangePasswordConflictsWithSSPI();
			}
			if (!string.IsNullOrEmpty(sqlConnectionString.AttachDBFilename))
			{
				throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
			}
			SqlConnection.ChangePassword(connectionString, sqlConnectionString, null, newPassword, null);
		}

		/// <summary>Changes the SQL Server password for the user indicated in the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <param name="connectionString">The connection string that contains enough information to connect to a server. The connection string should not use any of the following connection string keywords: Integrated Security = true, UserId, or Password; or ContextConnection = true.</param>
		/// <param name="credential">A <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</param>
		/// <param name="newSecurePassword">The new password. <paramref name="newSecurePassword" /> must be read only. The password must also comply with any password security policy set on the server (for example, minimum length and requirements for specific characters).</param>
		/// <exception cref="T:System.ArgumentException">The connection string contains any combination of UserId, Password, or Integrated Security=true.The connection string contains Context Connection=true.<paramref name="newSecurePassword" /> is greater than 128 characters.<paramref name="newSecurePassword" /> is not read only.<paramref name="newSecurePassword" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters (<paramref name="connectionString" />, <paramref name="credential" />, or <paramref name="newSecurePassword" />) is null.</exception>
		// Token: 0x06001285 RID: 4741 RVA: 0x0005B470 File Offset: 0x00059670
		public static void ChangePassword(string connectionString, SqlCredential credential, SecureString newSecurePassword)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw SQL.ChangePasswordArgumentMissing("connectionString");
			}
			if (credential == null)
			{
				throw SQL.ChangePasswordArgumentMissing("credential");
			}
			if (newSecurePassword == null || newSecurePassword.Length == 0)
			{
				throw SQL.ChangePasswordArgumentMissing("newSecurePassword");
			}
			if (!newSecurePassword.IsReadOnly())
			{
				throw ADP.MustBeReadOnly("newSecurePassword");
			}
			if (128 < newSecurePassword.Length)
			{
				throw ADP.InvalidArgumentLength("newSecurePassword", 128);
			}
			SqlConnectionString sqlConnectionString = SqlConnectionFactory.FindSqlConnectionOptions(new SqlConnectionPoolKey(connectionString, null, null));
			if (!string.IsNullOrEmpty(sqlConnectionString.UserID) || !string.IsNullOrEmpty(sqlConnectionString.Password))
			{
				throw ADP.InvalidMixedArgumentOfSecureAndClearCredential();
			}
			if (sqlConnectionString.IntegratedSecurity)
			{
				throw SQL.ChangePasswordConflictsWithSSPI();
			}
			if (!string.IsNullOrEmpty(sqlConnectionString.AttachDBFilename))
			{
				throw SQL.ChangePasswordUseOfUnallowedKey("attachdbfilename");
			}
			SqlConnection.ChangePassword(connectionString, sqlConnectionString, credential, null, newSecurePassword);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0005B544 File Offset: 0x00059744
		private static void ChangePassword(string connectionString, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword)
		{
			SqlInternalConnectionTds sqlInternalConnectionTds = null;
			try
			{
				sqlInternalConnectionTds = new SqlInternalConnectionTds(null, connectionOptions, credential, null, newPassword, newSecurePassword, false, null, null, false, null);
			}
			finally
			{
				if (sqlInternalConnectionTds != null)
				{
					sqlInternalConnectionTds.Dispose();
				}
			}
			SqlConnectionPoolKey sqlConnectionPoolKey = new SqlConnectionPoolKey(connectionString, null, null);
			SqlConnectionFactory.SingletonInstance.ClearPool(sqlConnectionPoolKey);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0005B598 File Offset: 0x00059798
		internal void RegisterForConnectionCloseNotification<T>(ref Task<T> outerTask, object value, int tag)
		{
			outerTask = outerTask.ContinueWith<Task<T>>(delegate(Task<T> task)
			{
				this.RemoveWeakReference(value);
				return task;
			}, TaskScheduler.Default).Unwrap<T>();
		}

		/// <summary>If statistics gathering is enabled, all values are reset to zero.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001288 RID: 4744 RVA: 0x0005B5D8 File Offset: 0x000597D8
		public void ResetStatistics()
		{
			if (this.Statistics != null)
			{
				this.Statistics.Reset();
				if (ConnectionState.Open == this.State)
				{
					ADP.TimerCurrent(out this._statistics._openTimestamp);
				}
			}
		}

		/// <summary>Returns a name value pair collection of statistics at the point in time the method is called.</summary>
		/// <returns>Returns a reference of type <see cref="T:System.Collections.IDictionary" /> of <see cref="T:System.Collections.DictionaryEntry" /> items.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001289 RID: 4745 RVA: 0x0005B606 File Offset: 0x00059806
		public IDictionary RetrieveStatistics()
		{
			if (this.Statistics != null)
			{
				this.UpdateStatistics();
				return this.Statistics.GetDictionary();
			}
			return new SqlStatistics().GetDictionary();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0005B62C File Offset: 0x0005982C
		private void UpdateStatistics()
		{
			if (ConnectionState.Open == this.State)
			{
				ADP.TimerCurrent(out this._statistics._closeTimestamp);
			}
			this.Statistics.UpdateStatistics();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x0600128B RID: 4747 RVA: 0x0005B652 File Offset: 0x00059852
		object ICloneable.Clone()
		{
			return new SqlConnection(this);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0005B65C File Offset: 0x0005985C
		private void CopyFrom(SqlConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0005B6B0 File Offset: 0x000598B0
		private Assembly ResolveTypeAssembly(AssemblyName asmRef, bool throwOnError)
		{
			if (string.Compare(asmRef.Name, "Microsoft.SqlServer.Types", StringComparison.OrdinalIgnoreCase) == 0)
			{
				asmRef.Version = this.TypeSystemAssemblyVersion;
			}
			Assembly assembly;
			try
			{
				assembly = Assembly.Load(asmRef);
			}
			catch (Exception ex)
			{
				if (throwOnError || !ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0005B70C File Offset: 0x0005990C
		internal void CheckGetExtendedUDTInfo(SqlMetaDataPriv metaData, bool fThrow)
		{
			if (metaData.udtType == null)
			{
				metaData.udtType = Type.GetType(metaData.udtAssemblyQualifiedName, (AssemblyName asmRef) => this.ResolveTypeAssembly(asmRef, fThrow), null, fThrow);
				if (fThrow && metaData.udtType == null)
				{
					throw SQL.UDTUnexpectedResult(metaData.udtAssemblyQualifiedName);
				}
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0005B784 File Offset: 0x00059984
		internal object GetUdtValue(object value, SqlMetaDataPriv metaData, bool returnDBNull)
		{
			if (returnDBNull && ADP.IsNull(value))
			{
				return DBNull.Value;
			}
			if (ADP.IsNull(value))
			{
				return metaData.udtType.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			return SerializationHelperSql9.Deserialize(new MemoryStream((byte[])value), metaData.udtType);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0005B7E4 File Offset: 0x000599E4
		internal byte[] GetBytes(object o)
		{
			Format format = Format.Native;
			int num;
			return this.GetBytes(o, out format, out num);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0005B800 File Offset: 0x00059A00
		internal byte[] GetBytes(object o, out Format format, out int maxSize)
		{
			SqlUdtInfo infoFromType = this.GetInfoFromType(o.GetType());
			maxSize = infoFromType.MaxByteSize;
			format = infoFromType.SerializationFormat;
			if (maxSize < -1 || maxSize >= 65535)
			{
				Type type = o.GetType();
				throw new InvalidOperationException(((type != null) ? type.ToString() : null) + ": invalid Size");
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream((maxSize < 0) ? 0 : maxSize))
			{
				SerializationHelperSql9.Serialize(memoryStream, o);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0005B898 File Offset: 0x00059A98
		private SqlUdtInfo GetInfoFromType(Type t)
		{
			Type type = t;
			SqlUdtInfo sqlUdtInfo;
			for (;;)
			{
				sqlUdtInfo = SqlUdtInfo.TryGetFromType(t);
				if (sqlUdtInfo != null)
				{
					break;
				}
				t = t.BaseType;
				if (!(t != null))
				{
					goto Block_2;
				}
			}
			return sqlUdtInfo;
			Block_2:
			throw SQL.UDTInvalidSqlType(type.AssemblyQualifiedName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnection" /> class.</summary>
		// Token: 0x06001293 RID: 4755 RVA: 0x0005B8CF File Offset: 0x00059ACF
		public SqlConnection()
		{
			this._reconnectLock = new object();
			this._originalConnectionId = Guid.Empty;
			base..ctor();
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0005B8FE File Offset: 0x00059AFE
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0005B906 File Offset: 0x00059B06
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return SqlConnection.s_connectionFactory;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0005B910 File Offset: 0x00059B10
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0005B930 File Offset: 0x00059B30
		private string ConnectionString_Get()
		{
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0005B960 File Offset: 0x00059B60
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref dbConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = dbConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0005B9CD File Offset: 0x00059BCD
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0005B9D5 File Offset: 0x00059BD5
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x0005B9DD File Offset: 0x00059BDD
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0005B9E6 File Offset: 0x00059BE6
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0005B9F0 File Offset: 0x00059BF0
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0005BA25 File Offset: 0x00059C25
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0005BA34 File Offset: 0x00059C34
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = this.ConnectionFactory.ProviderFactory.CreateCommand();
			dbCommand.Connection = this;
			return dbCommand;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0005BA4D File Offset: 0x00059C4D
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0005BA74 File Offset: 0x00059C74
		private void RepairInnerConnection()
		{
			this.WaitForPendingReconnection();
			if (this._connectRetryCount == 0)
			{
				return;
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds != null)
			{
				sqlInternalConnectionTds.ValidateConnectionForExecute(null);
				sqlInternalConnectionTds.GetSessionAndReconnectIfNeeded(this, 0);
			}
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x060012A2 RID: 4770 RVA: 0x0005BAB0 File Offset: 0x00059CB0
		public override void EnlistTransaction(Transaction transaction)
		{
			Transaction enlistedTransaction = this.InnerConnection.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (enlistedTransaction.Equals(transaction))
				{
					return;
				}
				if (enlistedTransaction.TransactionInformation.Status == global::System.Transactions.TransactionStatus.Active)
				{
					throw ADP.TransactionPresent();
				}
			}
			this.RepairInnerConnection();
			this.InnerConnection.EnlistTransaction(transaction);
			GC.KeepAlive(this);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0005BB07 File Offset: 0x00059D07
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0005BB18 File Offset: 0x00059D18
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = ((poolGroup != null) ? poolGroup.ConnectionOptions : null);
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0005BB51 File Offset: 0x00059D51
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0005BB60 File Offset: 0x00059D60
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0005BBD7 File Offset: 0x00059DD7
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0005BBE9 File Offset: 0x00059DE9
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00058EFE File Offset: 0x000570FE
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x00058EFE File Offset: 0x000570FE
		[MonoTODO]
		public SqlCredential Credentials
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x060012AB RID: 4779 RVA: 0x00058EFE File Offset: 0x000570FE
		[MonoTODO]
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0005BC10 File Offset: 0x00059E10
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x0000E24C File Offset: 0x0000C44C
		public static TimeSpan ColumnEncryptionKeyCacheTtl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0005BC2C File Offset: 0x00059E2C
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0000E24C File Offset: 0x0000C44C
		public static bool ColumnEncryptionQueryMetadataCacheEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0005BC47 File Offset: 0x00059E47
		public static IDictionary<string, IList<string>> ColumnEncryptionTrustedMasterKeyPaths
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0000E24C File Offset: 0x0000C44C
		public static void RegisterColumnEncryptionKeyStoreProviders(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C01 RID: 3073
		private bool _AsyncCommandInProgress;

		// Token: 0x04000C02 RID: 3074
		internal SqlStatistics _statistics;

		// Token: 0x04000C03 RID: 3075
		private bool _collectstats;

		// Token: 0x04000C04 RID: 3076
		private bool _fireInfoMessageEventOnUserErrors;

		// Token: 0x04000C05 RID: 3077
		private Tuple<TaskCompletionSource<DbConnectionInternal>, Task> _currentCompletion;

		// Token: 0x04000C06 RID: 3078
		private SqlCredential _credential;

		// Token: 0x04000C07 RID: 3079
		private string _connectionString;

		// Token: 0x04000C08 RID: 3080
		private int _connectRetryCount;

		// Token: 0x04000C09 RID: 3081
		private string _accessToken;

		// Token: 0x04000C0A RID: 3082
		private object _reconnectLock;

		// Token: 0x04000C0B RID: 3083
		internal Task _currentReconnectionTask;

		// Token: 0x04000C0C RID: 3084
		private Task _asyncWaitingForReconnection;

		// Token: 0x04000C0D RID: 3085
		private Guid _originalConnectionId;

		// Token: 0x04000C0E RID: 3086
		private CancellationTokenSource _reconnectionCancellationSource;

		// Token: 0x04000C0F RID: 3087
		internal SessionData _recoverySessionData;

		// Token: 0x04000C10 RID: 3088
		internal new bool _suppressStateChangeForReconnection;

		// Token: 0x04000C11 RID: 3089
		private int _reconnectCount;

		// Token: 0x04000C12 RID: 3090
		private static readonly DiagnosticListener s_diagnosticListener = new DiagnosticListener("SqlClientDiagnosticListener");

		// Token: 0x04000C13 RID: 3091
		internal bool _applyTransientFaultHandling;

		// Token: 0x04000C16 RID: 3094
		private static readonly DbConnectionFactory s_connectionFactory = SqlConnectionFactory.SingletonInstance;

		// Token: 0x04000C17 RID: 3095
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x04000C18 RID: 3096
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x04000C19 RID: 3097
		private DbConnectionInternal _innerConnection;

		// Token: 0x04000C1A RID: 3098
		private int _closeCount;

		// Token: 0x02000178 RID: 376
		private class OpenAsyncRetry
		{
			// Token: 0x060012B3 RID: 4787 RVA: 0x0005BC4F File Offset: 0x00059E4F
			public OpenAsyncRetry(SqlConnection parent, TaskCompletionSource<DbConnectionInternal> retry, TaskCompletionSource<object> result, CancellationTokenRegistration registration)
			{
				this._parent = parent;
				this._retry = retry;
				this._result = result;
				this._registration = registration;
			}

			// Token: 0x060012B4 RID: 4788 RVA: 0x0005BC74 File Offset: 0x00059E74
			internal void Retry(Task<DbConnectionInternal> retryTask)
			{
				this._registration.Dispose();
				try
				{
					SqlStatistics sqlStatistics = null;
					try
					{
						sqlStatistics = SqlStatistics.StartTimer(this._parent.Statistics);
						if (retryTask.IsFaulted)
						{
							Exception innerException = retryTask.Exception.InnerException;
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetException(retryTask.Exception.InnerException);
						}
						else if (retryTask.IsCanceled)
						{
							this._parent.CloseInnerConnection();
							this._parent._currentCompletion = null;
							this._result.SetCanceled();
						}
						else
						{
							DbConnectionInternal innerConnection = this._parent.InnerConnection;
							bool flag2;
							lock (innerConnection)
							{
								flag2 = this._parent.TryOpen(this._retry);
							}
							if (flag2)
							{
								this._parent._currentCompletion = null;
								this._result.SetResult(null);
							}
							else
							{
								this._parent.CloseInnerConnection();
								this._parent._currentCompletion = null;
								this._result.SetException(ADP.ExceptionWithStackTrace(ADP.InternalError(ADP.InternalErrorCode.CompletedConnectReturnedPending)));
							}
						}
					}
					finally
					{
						SqlStatistics.StopTimer(sqlStatistics);
					}
				}
				catch (Exception ex)
				{
					this._parent.CloseInnerConnection();
					this._parent._currentCompletion = null;
					this._result.SetException(ex);
				}
			}

			// Token: 0x04000C1B RID: 3099
			private SqlConnection _parent;

			// Token: 0x04000C1C RID: 3100
			private TaskCompletionSource<DbConnectionInternal> _retry;

			// Token: 0x04000C1D RID: 3101
			private TaskCompletionSource<object> _result;

			// Token: 0x04000C1E RID: 3102
			private CancellationTokenRegistration _registration;
		}
	}
}
