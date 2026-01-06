using System;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Represents a set of methods for creating instances of a provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000349 RID: 841
	public abstract class DbProviderFactory
	{
		/// <summary>Returns a new instance of the provider's class that implements the provider's version of the <see cref="T:System.Security.CodeAccessPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.CodeAccessPermission" /> object for the specified <see cref="T:System.Security.Permissions.PermissionState" />.</returns>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028DE RID: 10462 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual CodeAccessPermission CreatePermission(PermissionState state)
		{
			return null;
		}

		/// <summary>Specifies whether the specific <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>true if the instance of the <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class; otherwise false.</returns>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public virtual bool CanCreateDataSourceEnumerator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000B2558 File Offset: 0x000B0758
		public virtual bool CanCreateDataAdapter
		{
			get
			{
				if (this._canCreateDataAdapter == null)
				{
					using (DbDataAdapter dbDataAdapter = this.CreateDataAdapter())
					{
						this._canCreateDataAdapter = new bool?(dbDataAdapter != null);
					}
				}
				return this._canCreateDataAdapter.Value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000B25B0 File Offset: 0x000B07B0
		public virtual bool CanCreateCommandBuilder
		{
			get
			{
				if (this._canCreateCommandBuilder == null)
				{
					using (DbCommandBuilder dbCommandBuilder = this.CreateCommandBuilder())
					{
						this._canCreateCommandBuilder = new bool?(dbCommandBuilder != null);
					}
				}
				return this._canCreateCommandBuilder.Value;
			}
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommand" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E3 RID: 10467 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbCommand CreateCommand()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E4 RID: 10468 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbCommandBuilder CreateCommandBuilder()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnection" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E5 RID: 10469 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbConnection CreateConnection()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E6 RID: 10470 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataAdapter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E7 RID: 10471 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbDataAdapter CreateDataAdapter()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbParameter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E8 RID: 10472 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbParameter CreateParameter()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataSourceEnumerator" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028E9 RID: 10473 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return null;
		}

		// Token: 0x0400196B RID: 6507
		private bool? _canCreateDataAdapter;

		// Token: 0x0400196C RID: 6508
		private bool? _canCreateCommandBuilder;
	}
}
