using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000184 RID: 388
	internal sealed class SqlConnectionString : DbConnectionOptions
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x0005C7B0 File Offset: 0x0005A9B0
		internal SqlConnectionString(string connectionString)
			: base(connectionString, SqlConnectionString.GetParseSynonyms())
		{
			this.ThrowUnsupportedIfKeywordSet("asynchronous processing");
			this.ThrowUnsupportedIfKeywordSet("connection reset");
			this.ThrowUnsupportedIfKeywordSet("context connection");
			if (base.ContainsKey("network library"))
			{
				throw SQL.NetworkLibraryKeywordNotSupported();
			}
			this._integratedSecurity = base.ConvertValueToIntegratedSecurity();
			this._encrypt = base.ConvertValueToBoolean("encrypt", false);
			this._enlist = base.ConvertValueToBoolean("enlist", true);
			this._mars = base.ConvertValueToBoolean("multipleactiveresultsets", false);
			this._persistSecurityInfo = base.ConvertValueToBoolean("persist security info", false);
			this._pooling = base.ConvertValueToBoolean("pooling", true);
			this._replication = base.ConvertValueToBoolean("replication", false);
			this._userInstance = base.ConvertValueToBoolean("user instance", false);
			this._multiSubnetFailover = base.ConvertValueToBoolean("multisubnetfailover", false);
			this._connectTimeout = base.ConvertValueToInt32("connect timeout", 15);
			this._loadBalanceTimeout = base.ConvertValueToInt32("load balance timeout", 0);
			this._maxPoolSize = base.ConvertValueToInt32("max pool size", 100);
			this._minPoolSize = base.ConvertValueToInt32("min pool size", 0);
			this._packetSize = base.ConvertValueToInt32("packet size", 8000);
			this._connectRetryCount = base.ConvertValueToInt32("connectretrycount", 1);
			this._connectRetryInterval = base.ConvertValueToInt32("connectretryinterval", 10);
			this._applicationIntent = this.ConvertValueToApplicationIntent();
			this._applicationName = base.ConvertValueToString("application name", "Core .Net SqlClient Data Provider");
			this._attachDBFileName = base.ConvertValueToString("attachdbfilename", "");
			this._currentLanguage = base.ConvertValueToString("current language", "");
			this._dataSource = base.ConvertValueToString("data source", "");
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = base.ConvertValueToString("failover partner", "");
			this._initialCatalog = base.ConvertValueToString("initial catalog", "");
			this._password = base.ConvertValueToString("password", "");
			this._trustServerCertificate = base.ConvertValueToBoolean("trustservercertificate", false);
			string text = base.ConvertValueToString("type system version", null);
			string text2 = base.ConvertValueToString("transaction binding", null);
			this._userID = base.ConvertValueToString("user id", "");
			this._workstationId = base.ConvertValueToString("workstation id", null);
			if (this._loadBalanceTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("load balance timeout");
			}
			if (this._connectTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("connect timeout");
			}
			if (this._maxPoolSize < 1)
			{
				throw ADP.InvalidConnectionOptionValue("max pool size");
			}
			if (this._minPoolSize < 0)
			{
				throw ADP.InvalidConnectionOptionValue("min pool size");
			}
			if (this._maxPoolSize < this._minPoolSize)
			{
				throw ADP.InvalidMinMaxPoolSizeValues();
			}
			if (this._packetSize < 512 || 32768 < this._packetSize)
			{
				throw SQL.InvalidPacketSizeValue();
			}
			this.ValidateValueLength(this._applicationName, 128, "application name");
			this.ValidateValueLength(this._currentLanguage, 128, "current language");
			this.ValidateValueLength(this._dataSource, 128, "data source");
			this.ValidateValueLength(this._failoverPartner, 128, "failover partner");
			this.ValidateValueLength(this._initialCatalog, 128, "initial catalog");
			this.ValidateValueLength(this._password, 128, "password");
			this.ValidateValueLength(this._userID, 128, "user id");
			if (this._workstationId != null)
			{
				this.ValidateValueLength(this._workstationId, 128, "workstation id");
			}
			if (!string.Equals("", this._failoverPartner, StringComparison.OrdinalIgnoreCase))
			{
				if (this._multiSubnetFailover)
				{
					throw SQL.MultiSubnetFailoverWithFailoverPartner(false, null);
				}
				if (string.Equals("", this._initialCatalog, StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.MissingConnectionOptionValue("failover partner", "initial catalog");
				}
			}
			if (0 <= this._attachDBFileName.IndexOf('|'))
			{
				throw ADP.InvalidConnectionOptionValue("attachdbfilename");
			}
			this.ValidateValueLength(this._attachDBFileName, 260, "attachdbfilename");
			this._typeSystemAssemblyVersion = SqlConnectionString.constTypeSystemAsmVersion10;
			if (this._userInstance && !string.IsNullOrEmpty(this._failoverPartner))
			{
				throw SQL.UserInstanceFailoverNotCompatible();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "Latest";
			}
			if (text.Equals("Latest", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else if (text.Equals("SQL Server 2000", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2000;
			}
			else if (text.Equals("SQL Server 2005", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2005;
			}
			else if (text.Equals("SQL Server 2008", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else
			{
				if (!text.Equals("SQL Server 2012", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("type system version");
				}
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2012;
				this._typeSystemAssemblyVersion = SqlConnectionString.constTypeSystemAsmVersion11;
			}
			if (string.IsNullOrEmpty(text2))
			{
				text2 = "Implicit Unbind";
			}
			if (text2.Equals("Implicit Unbind", StringComparison.OrdinalIgnoreCase))
			{
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ImplicitUnbind;
			}
			else
			{
				if (!text2.Equals("Explicit Unbind", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("transaction binding");
				}
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
			}
			if (this._applicationIntent == ApplicationIntent.ReadOnly && !string.IsNullOrEmpty(this._failoverPartner))
			{
				throw SQL.ROR_FailoverNotSupportedConnString();
			}
			if (this._connectRetryCount < 0 || this._connectRetryCount > 255)
			{
				throw ADP.InvalidConnectRetryCountValue();
			}
			if (this._connectRetryInterval < 1 || this._connectRetryInterval > 60)
			{
				throw ADP.InvalidConnectRetryIntervalValue();
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0005CD48 File Offset: 0x0005AF48
		internal SqlConnectionString(SqlConnectionString connectionOptions, string dataSource, bool userInstance, bool? setEnlistValue)
			: base(connectionOptions)
		{
			this._integratedSecurity = connectionOptions._integratedSecurity;
			this._encrypt = connectionOptions._encrypt;
			if (setEnlistValue != null)
			{
				this._enlist = setEnlistValue.Value;
			}
			else
			{
				this._enlist = connectionOptions._enlist;
			}
			this._mars = connectionOptions._mars;
			this._persistSecurityInfo = connectionOptions._persistSecurityInfo;
			this._pooling = connectionOptions._pooling;
			this._replication = connectionOptions._replication;
			this._userInstance = userInstance;
			this._connectTimeout = connectionOptions._connectTimeout;
			this._loadBalanceTimeout = connectionOptions._loadBalanceTimeout;
			this._maxPoolSize = connectionOptions._maxPoolSize;
			this._minPoolSize = connectionOptions._minPoolSize;
			this._multiSubnetFailover = connectionOptions._multiSubnetFailover;
			this._packetSize = connectionOptions._packetSize;
			this._applicationName = connectionOptions._applicationName;
			this._attachDBFileName = connectionOptions._attachDBFileName;
			this._currentLanguage = connectionOptions._currentLanguage;
			this._dataSource = dataSource;
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = connectionOptions._failoverPartner;
			this._initialCatalog = connectionOptions._initialCatalog;
			this._password = connectionOptions._password;
			this._userID = connectionOptions._userID;
			this._workstationId = connectionOptions._workstationId;
			this._typeSystemVersion = connectionOptions._typeSystemVersion;
			this._transactionBinding = connectionOptions._transactionBinding;
			this._applicationIntent = connectionOptions._applicationIntent;
			this._connectRetryCount = connectionOptions._connectRetryCount;
			this._connectRetryInterval = connectionOptions._connectRetryInterval;
			this.ValidateValueLength(this._dataSource, 128, "data source");
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0005CEE1 File Offset: 0x0005B0E1
		internal bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal bool Asynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal bool ConnectionReset
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0005CEE9 File Offset: 0x0005B0E9
		internal bool Encrypt
		{
			get
			{
				return this._encrypt;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0005CEF1 File Offset: 0x0005B0F1
		internal bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0005CEF9 File Offset: 0x0005B0F9
		internal bool Enlist
		{
			get
			{
				return this._enlist;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0005CF01 File Offset: 0x0005B101
		internal bool MARS
		{
			get
			{
				return this._mars;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0005CF09 File Offset: 0x0005B109
		internal bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0005CF11 File Offset: 0x0005B111
		internal bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0005CF19 File Offset: 0x0005B119
		internal bool Pooling
		{
			get
			{
				return this._pooling;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0005CF21 File Offset: 0x0005B121
		internal bool Replication
		{
			get
			{
				return this._replication;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0005CF29 File Offset: 0x0005B129
		internal bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0005CF31 File Offset: 0x0005B131
		internal int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0005CF39 File Offset: 0x0005B139
		internal int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0005CF41 File Offset: 0x0005B141
		internal int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0005CF49 File Offset: 0x0005B149
		internal int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0005CF51 File Offset: 0x0005B151
		internal int PacketSize
		{
			get
			{
				return this._packetSize;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0005CF59 File Offset: 0x0005B159
		internal int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0005CF61 File Offset: 0x0005B161
		internal int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0005CF69 File Offset: 0x0005B169
		internal ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0005CF71 File Offset: 0x0005B171
		internal string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0005CF79 File Offset: 0x0005B179
		internal string AttachDBFilename
		{
			get
			{
				return this._attachDBFileName;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0005CF81 File Offset: 0x0005B181
		internal string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0005CF89 File Offset: 0x0005B189
		internal string DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0005CF91 File Offset: 0x0005B191
		internal string LocalDBInstance
		{
			get
			{
				return this._localDBInstance;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0005CF99 File Offset: 0x0005B199
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0005CFA1 File Offset: 0x0005B1A1
		internal string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0005CFA9 File Offset: 0x0005B1A9
		internal string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0005CFB1 File Offset: 0x0005B1B1
		internal string UserID
		{
			get
			{
				return this._userID;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0005CFB9 File Offset: 0x0005B1B9
		internal string WorkstationId
		{
			get
			{
				return this._workstationId;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0005CFC1 File Offset: 0x0005B1C1
		internal SqlConnectionString.TypeSystem TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0005CFC9 File Offset: 0x0005B1C9
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return this._typeSystemAssemblyVersion;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0005CFD1 File Offset: 0x0005B1D1
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0005CFDC File Offset: 0x0005B1DC
		internal static Dictionary<string, string> GetParseSynonyms()
		{
			Dictionary<string, string> dictionary = SqlConnectionString.s_sqlClientSynonyms;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, string>(54)
				{
					{ "applicationintent", "applicationintent" },
					{ "application name", "application name" },
					{ "asynchronous processing", "asynchronous processing" },
					{ "attachdbfilename", "attachdbfilename" },
					{ "connect timeout", "connect timeout" },
					{ "connection reset", "connection reset" },
					{ "context connection", "context connection" },
					{ "current language", "current language" },
					{ "data source", "data source" },
					{ "encrypt", "encrypt" },
					{ "enlist", "enlist" },
					{ "failover partner", "failover partner" },
					{ "initial catalog", "initial catalog" },
					{ "integrated security", "integrated security" },
					{ "load balance timeout", "load balance timeout" },
					{ "multipleactiveresultsets", "multipleactiveresultsets" },
					{ "max pool size", "max pool size" },
					{ "min pool size", "min pool size" },
					{ "multisubnetfailover", "multisubnetfailover" },
					{ "network library", "network library" },
					{ "packet size", "packet size" },
					{ "password", "password" },
					{ "persist security info", "persist security info" },
					{ "pooling", "pooling" },
					{ "replication", "replication" },
					{ "trustservercertificate", "trustservercertificate" },
					{ "transaction binding", "transaction binding" },
					{ "type system version", "type system version" },
					{ "user id", "user id" },
					{ "user instance", "user instance" },
					{ "workstation id", "workstation id" },
					{ "connectretrycount", "connectretrycount" },
					{ "connectretryinterval", "connectretryinterval" },
					{ "app", "application name" },
					{ "async", "asynchronous processing" },
					{ "extended properties", "attachdbfilename" },
					{ "initial file name", "attachdbfilename" },
					{ "connection timeout", "connect timeout" },
					{ "timeout", "connect timeout" },
					{ "language", "current language" },
					{ "addr", "data source" },
					{ "address", "data source" },
					{ "network address", "data source" },
					{ "server", "data source" },
					{ "database", "initial catalog" },
					{ "trusted_connection", "integrated security" },
					{ "connection lifetime", "load balance timeout" },
					{ "net", "network library" },
					{ "network", "network library" },
					{ "pwd", "password" },
					{ "persistsecurityinfo", "persist security info" },
					{ "uid", "user id" },
					{ "user", "user id" },
					{ "wsid", "workstation id" }
				};
				SqlConnectionString.s_sqlClientSynonyms = dictionary;
			}
			return dictionary;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0005D364 File Offset: 0x0005B564
		internal string ObtainWorkstationId()
		{
			string text = this.WorkstationId;
			if (text == null)
			{
				text = ADP.MachineName();
				this.ValidateValueLength(text, 128, "workstation id");
			}
			return text;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0005D393 File Offset: 0x0005B593
		private void ValidateValueLength(string value, int limit, string key)
		{
			if (limit < value.Length)
			{
				throw ADP.InvalidConnectionOptionValueLength(key, limit);
			}
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0005D3A8 File Offset: 0x0005B5A8
		internal ApplicationIntent ConvertValueToApplicationIntent()
		{
			string text;
			if (!base.TryGetParsetableValue("applicationintent", out text))
			{
				return ApplicationIntent.ReadWrite;
			}
			ApplicationIntent applicationIntent;
			try
			{
				applicationIntent = DbConnectionStringBuilderUtil.ConvertToApplicationIntent("applicationintent", text);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue("applicationintent", ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue("applicationintent", ex2);
			}
			return applicationIntent;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0005D40C File Offset: 0x0005B60C
		internal void ThrowUnsupportedIfKeywordSet(string keyword)
		{
			if (base.ContainsKey(keyword))
			{
				throw SQL.UnsupportedKeyword(keyword);
			}
		}

		// Token: 0x04000C3D RID: 3133
		internal const int SynonymCount = 18;

		// Token: 0x04000C3E RID: 3134
		internal const int DeprecatedSynonymCount = 3;

		// Token: 0x04000C3F RID: 3135
		private static Dictionary<string, string> s_sqlClientSynonyms;

		// Token: 0x04000C40 RID: 3136
		private readonly bool _integratedSecurity;

		// Token: 0x04000C41 RID: 3137
		private readonly bool _encrypt;

		// Token: 0x04000C42 RID: 3138
		private readonly bool _trustServerCertificate;

		// Token: 0x04000C43 RID: 3139
		private readonly bool _enlist;

		// Token: 0x04000C44 RID: 3140
		private readonly bool _mars;

		// Token: 0x04000C45 RID: 3141
		private readonly bool _persistSecurityInfo;

		// Token: 0x04000C46 RID: 3142
		private readonly bool _pooling;

		// Token: 0x04000C47 RID: 3143
		private readonly bool _replication;

		// Token: 0x04000C48 RID: 3144
		private readonly bool _userInstance;

		// Token: 0x04000C49 RID: 3145
		private readonly bool _multiSubnetFailover;

		// Token: 0x04000C4A RID: 3146
		private readonly int _connectTimeout;

		// Token: 0x04000C4B RID: 3147
		private readonly int _loadBalanceTimeout;

		// Token: 0x04000C4C RID: 3148
		private readonly int _maxPoolSize;

		// Token: 0x04000C4D RID: 3149
		private readonly int _minPoolSize;

		// Token: 0x04000C4E RID: 3150
		private readonly int _packetSize;

		// Token: 0x04000C4F RID: 3151
		private readonly int _connectRetryCount;

		// Token: 0x04000C50 RID: 3152
		private readonly int _connectRetryInterval;

		// Token: 0x04000C51 RID: 3153
		private readonly ApplicationIntent _applicationIntent;

		// Token: 0x04000C52 RID: 3154
		private readonly string _applicationName;

		// Token: 0x04000C53 RID: 3155
		private readonly string _attachDBFileName;

		// Token: 0x04000C54 RID: 3156
		private readonly string _currentLanguage;

		// Token: 0x04000C55 RID: 3157
		private readonly string _dataSource;

		// Token: 0x04000C56 RID: 3158
		private readonly string _localDBInstance;

		// Token: 0x04000C57 RID: 3159
		private readonly string _failoverPartner;

		// Token: 0x04000C58 RID: 3160
		private readonly string _initialCatalog;

		// Token: 0x04000C59 RID: 3161
		private readonly string _password;

		// Token: 0x04000C5A RID: 3162
		private readonly string _userID;

		// Token: 0x04000C5B RID: 3163
		private readonly string _workstationId;

		// Token: 0x04000C5C RID: 3164
		private readonly SqlConnectionString.TransactionBindingEnum _transactionBinding;

		// Token: 0x04000C5D RID: 3165
		private readonly SqlConnectionString.TypeSystem _typeSystemVersion;

		// Token: 0x04000C5E RID: 3166
		private readonly Version _typeSystemAssemblyVersion;

		// Token: 0x04000C5F RID: 3167
		private static readonly Version constTypeSystemAsmVersion10 = new Version("10.0.0.0");

		// Token: 0x04000C60 RID: 3168
		private static readonly Version constTypeSystemAsmVersion11 = new Version("11.0.0.0");

		// Token: 0x02000185 RID: 389
		internal static class DEFAULT
		{
			// Token: 0x04000C61 RID: 3169
			internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

			// Token: 0x04000C62 RID: 3170
			internal const string Application_Name = "Core .Net SqlClient Data Provider";

			// Token: 0x04000C63 RID: 3171
			internal const string AttachDBFilename = "";

			// Token: 0x04000C64 RID: 3172
			internal const int Connect_Timeout = 15;

			// Token: 0x04000C65 RID: 3173
			internal const string Current_Language = "";

			// Token: 0x04000C66 RID: 3174
			internal const string Data_Source = "";

			// Token: 0x04000C67 RID: 3175
			internal const bool Encrypt = false;

			// Token: 0x04000C68 RID: 3176
			internal const bool Enlist = true;

			// Token: 0x04000C69 RID: 3177
			internal const string FailoverPartner = "";

			// Token: 0x04000C6A RID: 3178
			internal const string Initial_Catalog = "";

			// Token: 0x04000C6B RID: 3179
			internal const bool Integrated_Security = false;

			// Token: 0x04000C6C RID: 3180
			internal const int Load_Balance_Timeout = 0;

			// Token: 0x04000C6D RID: 3181
			internal const bool MARS = false;

			// Token: 0x04000C6E RID: 3182
			internal const int Max_Pool_Size = 100;

			// Token: 0x04000C6F RID: 3183
			internal const int Min_Pool_Size = 0;

			// Token: 0x04000C70 RID: 3184
			internal const bool MultiSubnetFailover = false;

			// Token: 0x04000C71 RID: 3185
			internal const int Packet_Size = 8000;

			// Token: 0x04000C72 RID: 3186
			internal const string Password = "";

			// Token: 0x04000C73 RID: 3187
			internal const bool Persist_Security_Info = false;

			// Token: 0x04000C74 RID: 3188
			internal const bool Pooling = true;

			// Token: 0x04000C75 RID: 3189
			internal const bool TrustServerCertificate = false;

			// Token: 0x04000C76 RID: 3190
			internal const string Type_System_Version = "";

			// Token: 0x04000C77 RID: 3191
			internal const string User_ID = "";

			// Token: 0x04000C78 RID: 3192
			internal const bool User_Instance = false;

			// Token: 0x04000C79 RID: 3193
			internal const bool Replication = false;

			// Token: 0x04000C7A RID: 3194
			internal const int Connect_Retry_Count = 1;

			// Token: 0x04000C7B RID: 3195
			internal const int Connect_Retry_Interval = 10;
		}

		// Token: 0x02000186 RID: 390
		internal static class KEY
		{
			// Token: 0x04000C7C RID: 3196
			internal const string ApplicationIntent = "applicationintent";

			// Token: 0x04000C7D RID: 3197
			internal const string Application_Name = "application name";

			// Token: 0x04000C7E RID: 3198
			internal const string AsynchronousProcessing = "asynchronous processing";

			// Token: 0x04000C7F RID: 3199
			internal const string AttachDBFilename = "attachdbfilename";

			// Token: 0x04000C80 RID: 3200
			internal const string Connect_Timeout = "connect timeout";

			// Token: 0x04000C81 RID: 3201
			internal const string Connection_Reset = "connection reset";

			// Token: 0x04000C82 RID: 3202
			internal const string Context_Connection = "context connection";

			// Token: 0x04000C83 RID: 3203
			internal const string Current_Language = "current language";

			// Token: 0x04000C84 RID: 3204
			internal const string Data_Source = "data source";

			// Token: 0x04000C85 RID: 3205
			internal const string Encrypt = "encrypt";

			// Token: 0x04000C86 RID: 3206
			internal const string Enlist = "enlist";

			// Token: 0x04000C87 RID: 3207
			internal const string FailoverPartner = "failover partner";

			// Token: 0x04000C88 RID: 3208
			internal const string Initial_Catalog = "initial catalog";

			// Token: 0x04000C89 RID: 3209
			internal const string Integrated_Security = "integrated security";

			// Token: 0x04000C8A RID: 3210
			internal const string Load_Balance_Timeout = "load balance timeout";

			// Token: 0x04000C8B RID: 3211
			internal const string MARS = "multipleactiveresultsets";

			// Token: 0x04000C8C RID: 3212
			internal const string Max_Pool_Size = "max pool size";

			// Token: 0x04000C8D RID: 3213
			internal const string Min_Pool_Size = "min pool size";

			// Token: 0x04000C8E RID: 3214
			internal const string MultiSubnetFailover = "multisubnetfailover";

			// Token: 0x04000C8F RID: 3215
			internal const string Network_Library = "network library";

			// Token: 0x04000C90 RID: 3216
			internal const string Packet_Size = "packet size";

			// Token: 0x04000C91 RID: 3217
			internal const string Password = "password";

			// Token: 0x04000C92 RID: 3218
			internal const string Persist_Security_Info = "persist security info";

			// Token: 0x04000C93 RID: 3219
			internal const string Pooling = "pooling";

			// Token: 0x04000C94 RID: 3220
			internal const string TransactionBinding = "transaction binding";

			// Token: 0x04000C95 RID: 3221
			internal const string TrustServerCertificate = "trustservercertificate";

			// Token: 0x04000C96 RID: 3222
			internal const string Type_System_Version = "type system version";

			// Token: 0x04000C97 RID: 3223
			internal const string User_ID = "user id";

			// Token: 0x04000C98 RID: 3224
			internal const string User_Instance = "user instance";

			// Token: 0x04000C99 RID: 3225
			internal const string Workstation_Id = "workstation id";

			// Token: 0x04000C9A RID: 3226
			internal const string Replication = "replication";

			// Token: 0x04000C9B RID: 3227
			internal const string Connect_Retry_Count = "connectretrycount";

			// Token: 0x04000C9C RID: 3228
			internal const string Connect_Retry_Interval = "connectretryinterval";
		}

		// Token: 0x02000187 RID: 391
		private static class SYNONYM
		{
			// Token: 0x04000C9D RID: 3229
			internal const string APP = "app";

			// Token: 0x04000C9E RID: 3230
			internal const string Async = "async";

			// Token: 0x04000C9F RID: 3231
			internal const string EXTENDED_PROPERTIES = "extended properties";

			// Token: 0x04000CA0 RID: 3232
			internal const string INITIAL_FILE_NAME = "initial file name";

			// Token: 0x04000CA1 RID: 3233
			internal const string CONNECTION_TIMEOUT = "connection timeout";

			// Token: 0x04000CA2 RID: 3234
			internal const string TIMEOUT = "timeout";

			// Token: 0x04000CA3 RID: 3235
			internal const string LANGUAGE = "language";

			// Token: 0x04000CA4 RID: 3236
			internal const string ADDR = "addr";

			// Token: 0x04000CA5 RID: 3237
			internal const string ADDRESS = "address";

			// Token: 0x04000CA6 RID: 3238
			internal const string SERVER = "server";

			// Token: 0x04000CA7 RID: 3239
			internal const string NETWORK_ADDRESS = "network address";

			// Token: 0x04000CA8 RID: 3240
			internal const string DATABASE = "database";

			// Token: 0x04000CA9 RID: 3241
			internal const string TRUSTED_CONNECTION = "trusted_connection";

			// Token: 0x04000CAA RID: 3242
			internal const string Connection_Lifetime = "connection lifetime";

			// Token: 0x04000CAB RID: 3243
			internal const string NET = "net";

			// Token: 0x04000CAC RID: 3244
			internal const string NETWORK = "network";

			// Token: 0x04000CAD RID: 3245
			internal const string Pwd = "pwd";

			// Token: 0x04000CAE RID: 3246
			internal const string PERSISTSECURITYINFO = "persistsecurityinfo";

			// Token: 0x04000CAF RID: 3247
			internal const string UID = "uid";

			// Token: 0x04000CB0 RID: 3248
			internal const string User = "user";

			// Token: 0x04000CB1 RID: 3249
			internal const string WSID = "wsid";
		}

		// Token: 0x02000188 RID: 392
		internal enum TypeSystem
		{
			// Token: 0x04000CB3 RID: 3251
			Latest = 2008,
			// Token: 0x04000CB4 RID: 3252
			SQLServer2000 = 2000,
			// Token: 0x04000CB5 RID: 3253
			SQLServer2005 = 2005,
			// Token: 0x04000CB6 RID: 3254
			SQLServer2008 = 2008,
			// Token: 0x04000CB7 RID: 3255
			SQLServer2012 = 2012
		}

		// Token: 0x02000189 RID: 393
		internal static class TYPESYSTEMVERSION
		{
			// Token: 0x04000CB8 RID: 3256
			internal const string Latest = "Latest";

			// Token: 0x04000CB9 RID: 3257
			internal const string SQL_Server_2000 = "SQL Server 2000";

			// Token: 0x04000CBA RID: 3258
			internal const string SQL_Server_2005 = "SQL Server 2005";

			// Token: 0x04000CBB RID: 3259
			internal const string SQL_Server_2008 = "SQL Server 2008";

			// Token: 0x04000CBC RID: 3260
			internal const string SQL_Server_2012 = "SQL Server 2012";
		}

		// Token: 0x0200018A RID: 394
		internal enum TransactionBindingEnum
		{
			// Token: 0x04000CBE RID: 3262
			ImplicitUnbind,
			// Token: 0x04000CBF RID: 3263
			ExplicitUnbind
		}

		// Token: 0x0200018B RID: 395
		internal static class TRANSACTIONBINDING
		{
			// Token: 0x04000CC0 RID: 3264
			internal const string ImplicitUnbind = "Implicit Unbind";

			// Token: 0x04000CC1 RID: 3265
			internal const string ExplicitUnbind = "Explicit Unbind";
		}
	}
}
