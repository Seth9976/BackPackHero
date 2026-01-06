using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of methods for creating instances of the OLEDB provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000116 RID: 278
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbFactory : DbProviderFactory
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0004F1D2 File Offset: 0x0004D3D2
		internal OleDbFactory()
		{
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F6C RID: 3948 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override DbCommand CreateCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F6D RID: 3949 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F6E RID: 3950 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override DbConnection CreateConnection()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000F6F RID: 3951 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F70 RID: 3952 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override DbDataAdapter CreateDataAdapter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F71 RID: 3953 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override DbParameter CreateParameter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance.</summary>
		/// <returns>A strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F72 RID: 3954 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.OleDb.OleDbFactory" />. This can be used to retrieve strongly-typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009C3 RID: 2499
		public static readonly OleDbFactory Instance;
	}
}
