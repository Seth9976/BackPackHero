using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.Clear" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000078 RID: 120
	public sealed class DataTableClearEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableClearEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</param>
		// Token: 0x06000844 RID: 2116 RVA: 0x000258FF File Offset: 0x00023AFF
		public DataTableClearEventArgs(DataTable dataTable)
		{
			this.Table = dataTable;
		}

		/// <summary>Gets the table whose rows are being cleared.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0002590E File Offset: 0x00023B0E
		public DataTable Table { get; }

		/// <summary>Gets the table name whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the table name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00025916 File Offset: 0x00023B16
		public string TableName
		{
			get
			{
				return this.Table.TableName;
			}
		}

		/// <summary>Gets the namespace of the table whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the namespace name.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00025923 File Offset: 0x00023B23
		public string TableNamespace
		{
			get
			{
				return this.Table.Namespace;
			}
		}
	}
}
