using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.SqlClient
{
	// Token: 0x02000180 RID: 384
	internal sealed class SqlConnectionFactory : DbConnectionFactory
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x0005C1B4 File Offset: 0x0005A3B4
		private SqlConnectionFactory()
		{
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0005A804 File Offset: 0x00058A04
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0005C1BC File Offset: 0x0005A3BC
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection, null);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0005C1CC File Offset: 0x0005A3CC
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)options;
			SqlConnectionPoolKey sqlConnectionPoolKey = (SqlConnectionPoolKey)poolKey;
			SessionData sessionData = null;
			SqlConnection sqlConnection = (SqlConnection)owningConnection;
			bool flag = sqlConnection != null && sqlConnection._applyTransientFaultHandling;
			SqlConnectionString sqlConnectionString2 = null;
			if (userOptions != null)
			{
				sqlConnectionString2 = (SqlConnectionString)userOptions;
			}
			else if (sqlConnection != null)
			{
				sqlConnectionString2 = (SqlConnectionString)sqlConnection.UserConnectionOptions;
			}
			if (sqlConnection != null)
			{
				sessionData = sqlConnection._recoverySessionData;
			}
			bool flag2 = false;
			DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
			if (sqlConnectionString.IntegratedSecurity)
			{
				if (pool != null)
				{
					dbConnectionPoolIdentity = pool.Identity;
				}
				else
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
				}
			}
			if (sqlConnectionString.UserInstance)
			{
				flag2 = true;
				string text;
				if (pool == null || (pool != null && pool.Count <= 0))
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = null;
					try
					{
						SqlConnectionString sqlConnectionString3 = new SqlConnectionString(sqlConnectionString, sqlConnectionString.DataSource, true, new bool?(false));
						sqlInternalConnectionTds = new SqlInternalConnectionTds(dbConnectionPoolIdentity, sqlConnectionString3, sqlConnectionPoolKey.Credential, null, "", null, false, null, null, flag, null);
						text = sqlInternalConnectionTds.InstanceName;
						if (!text.StartsWith("\\\\.\\", StringComparison.Ordinal))
						{
							throw SQL.NonLocalSSEInstance();
						}
						if (pool != null)
						{
							((SqlConnectionPoolProviderInfo)pool.ProviderInfo).InstanceName = text;
						}
						goto IL_0125;
					}
					finally
					{
						if (sqlInternalConnectionTds != null)
						{
							sqlInternalConnectionTds.Dispose();
						}
					}
				}
				text = ((SqlConnectionPoolProviderInfo)pool.ProviderInfo).InstanceName;
				IL_0125:
				sqlConnectionString = new SqlConnectionString(sqlConnectionString, text, false, null);
				poolGroupProviderInfo = null;
			}
			return new SqlInternalConnectionTds(dbConnectionPoolIdentity, sqlConnectionString, sqlConnectionPoolKey.Credential, poolGroupProviderInfo, "", null, flag2, sqlConnectionString2, sessionData, flag, sqlConnectionPoolKey.AccessToken);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0005C348 File Offset: 0x0005A548
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new SqlConnectionString(connectionString);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0005C350 File Offset: 0x0005A550
		internal override DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			DbConnectionPoolProviderInfo dbConnectionPoolProviderInfo = null;
			if (((SqlConnectionString)connectionOptions).UserInstance)
			{
				dbConnectionPoolProviderInfo = new SqlConnectionPoolProviderInfo();
			}
			return dbConnectionPoolProviderInfo;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0005C374 File Offset: 0x0005A574
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)connectionOptions;
			DbConnectionPoolGroupOptions dbConnectionPoolGroupOptions = null;
			if (sqlConnectionString.Pooling)
			{
				int num = sqlConnectionString.ConnectTimeout;
				if (0 < num && num < 2147483)
				{
					num *= 1000;
				}
				else if (num >= 2147483)
				{
					num = int.MaxValue;
				}
				dbConnectionPoolGroupOptions = new DbConnectionPoolGroupOptions(sqlConnectionString.IntegratedSecurity, sqlConnectionString.MinPoolSize, sqlConnectionString.MaxPoolSize, num, sqlConnectionString.LoadBalanceTimeout, sqlConnectionString.Enlist);
			}
			return dbConnectionPoolGroupOptions;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0005C3E3 File Offset: 0x0005A5E3
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new SqlConnectionPoolGroupProviderInfo((SqlConnectionString)connectionOptions);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0005C3F0 File Offset: 0x0005A5F0
		internal static SqlConnectionString FindSqlConnectionOptions(SqlConnectionPoolKey key)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)SqlConnectionFactory.SingletonInstance.FindConnectionOptions(key);
			if (sqlConnectionString == null)
			{
				sqlConnectionString = new SqlConnectionString(key.ConnectionString);
			}
			if (sqlConnectionString.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			return sqlConnectionString;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0005C42C File Offset: 0x0005A62C
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0005C44C File Offset: 0x0005A64C
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0005C46C File Offset: 0x0005A66C
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PermissionDemand();
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0005C48C File Offset: 0x0005A68C
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0005C4AC File Offset: 0x0005A6AC
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0005C4CC File Offset: 0x0005A6CC
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			return sqlConnection != null && sqlConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0005C4F0 File Offset: 0x0005A6F0
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0005C50E File Offset: 0x0005A70E
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.SqlClient.SqlMetaData.xml");
			cacheMetaDataFactory = true;
			return new SqlMetaDataFactory(manifestResourceStream, internalConnection.ServerVersion, internalConnection.ServerVersion);
		}

		// Token: 0x04000C34 RID: 3124
		private const string _metaDataXml = "MetaDataXml";

		// Token: 0x04000C35 RID: 3125
		public static readonly SqlConnectionFactory SingletonInstance = new SqlConnectionFactory();
	}
}
