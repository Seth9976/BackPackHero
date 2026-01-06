using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of methods for creating instances of the <see cref="N:System.Data.SqlClient" /> provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015E RID: 350
	public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x0004F1D2 File Offset: 0x0004D3D2
		private SqlClientFactory()
		{
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001136 RID: 4406 RVA: 0x00054FF8 File Offset: 0x000531F8
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001137 RID: 4407 RVA: 0x00054FFF File Offset: 0x000531FF
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001138 RID: 4408 RVA: 0x00055006 File Offset: 0x00053206
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001139 RID: 4409 RVA: 0x0005500D File Offset: 0x0005320D
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600113A RID: 4410 RVA: 0x00055014 File Offset: 0x00053214
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600113B RID: 4411 RVA: 0x0005501B File Offset: 0x0005321B
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		/// <summary>Returns true if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise false .</summary>
		/// <returns>true if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise false.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />.</summary>
		/// <returns>A new data source enumerator.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600113D RID: 4413 RVA: 0x00055022 File Offset: 0x00053222
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		/// <summary>Returns a new <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>A strongly typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600113E RID: 4414 RVA: 0x00055029 File Offset: 0x00053229
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IServiceProvider.GetService(System.Type)" />.</summary>
		/// <returns>A service object.</returns>
		/// <param name="serviceType">An object that specifies the type of service object to get. </param>
		// Token: 0x06001140 RID: 4416 RVA: 0x0005503D File Offset: 0x0005323D
		object IServiceProvider.GetService(Type serviceType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.SqlClient.SqlClientFactory" />. This can be used to retrieve strongly typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000B71 RID: 2929
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
