using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.OleDb.OleDbConnection" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010F RID: 271
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbConnectionStringBuilder : DbConnectionStringBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> class.</summary>
		// Token: 0x06000EFB RID: 3835 RVA: 0x0004F071 File Offset: 0x0004D271
		public OleDbConnectionStringBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into key/value pairs.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		// Token: 0x06000EFC RID: 3836 RVA: 0x0004F071 File Offset: 0x0004D271
		public OleDbConnectionStringBuilder(string connectionString)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets the name of the data source to connect to.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.DataSource" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x000094D4 File Offset: 0x000076D4
		public string DataSource
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the Universal Data Link (UDL) file for connecting to the data source.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.FileName" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x000094D4 File Offset: 0x000076D4
		public string FileName
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x000094D4 File Offset: 0x000076D4
		public object Item
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x000094D4 File Offset: 0x000076D4
		public override ICollection Keys
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value to be passed for the OLE DB Services key within the connection string.</summary>
		/// <returns>Returns the value corresponding to the OLE DB Services key within the connection string. By default, the value is -13.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x000094D4 File Offset: 0x000076D4
		public int OleDbServices
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether security-sensitive information, such as the password, is returned as part of the connection if the connection is open or has ever been in an open state.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.PersistSecurityInfo" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x000094D4 File Offset: 0x000076D4
		public bool PersistSecurityInfo
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a string that contains the name of the data provider associated with the internal connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.OleDb.OleDbConnectionStringBuilder.Provider" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x000094D4 File Offset: 0x000076D4
		public string Provider
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F0B RID: 3851 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override void Clear()
		{
			throw ADP.OleDb();
		}

		/// <summary>Determines whether the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> contains an element that has the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0C RID: 3852 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override bool ContainsKey(string keyword)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			throw ADP.OleDb();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed, false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.OleDb.OleDbConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0E RID: 3854 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override bool Remove(string keyword)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public bool TryGetValue(string keyword, object value)
		{
			throw ADP.OleDb();
		}
	}
}
