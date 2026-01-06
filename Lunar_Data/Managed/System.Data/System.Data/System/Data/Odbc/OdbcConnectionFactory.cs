using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.Odbc
{
	// Token: 0x02000288 RID: 648
	internal sealed class OdbcConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06001C24 RID: 7204 RVA: 0x0005C1B4 File Offset: 0x0005A3B4
		private OdbcConnectionFactory()
		{
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0008AABE File Offset: 0x00088CBE
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OdbcFactory.Instance;
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0008AAC5 File Offset: 0x00088CC5
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OdbcConnectionOpen(owningObject as OdbcConnection, options as OdbcConnectionString);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0008AAD9 File Offset: 0x00088CD9
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OdbcConnectionString(connectionString, previous != null);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0008AAE5 File Offset: 0x00088CE5
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OdbcConnectionPoolGroupProviderInfo();
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0008AAEC File Offset: 0x00088CEC
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			OdbcConnection outerConnection = ((OdbcConnectionOpen)internalConnection).OuterConnection;
			string infoStringUnhandled = outerConnection.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.Odbc.OdbcMetaData.xml");
			cacheMetaDataFactory = true;
			string infoStringUnhandled2 = outerConnection.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER);
			return new OdbcMetaDataFactory(manifestResourceStream, infoStringUnhandled2, infoStringUnhandled2, outerConnection);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0008AB38 File Offset: 0x00088D38
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0008AB58 File Offset: 0x00088D58
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0008AB78 File Offset: 0x00088D78
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PermissionDemand();
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0008AB98 File Offset: 0x00088D98
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0008ABB8 File Offset: 0x00088DB8
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0008ABD8 File Offset: 0x00088DD8
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			return odbcConnection != null && odbcConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0008ABFC File Offset: 0x00088DFC
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x0400154D RID: 5453
		public static readonly OdbcConnectionFactory SingletonInstance = new OdbcConnectionFactory();
	}
}
