using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Controls code access permissions for event logging.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000260 RID: 608
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x06001305 RID: 4869 RVA: 0x00050D5D File Offset: 0x0004EF5D
		public EventLogPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission entries.</summary>
		/// <param name="permissionAccessEntries">An array of  objects that represent permission entries. The <see cref="P:System.Diagnostics.EventLogPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x06001306 RID: 4870 RVA: 0x00050D6B File Offset: 0x0004EF6B
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission state (full access or no access to resources). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001307 RID: 4871 RVA: 0x00050D9F File Offset: 0x0004EF9F
		public EventLogPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified access levels and the name of the computer to use.</summary>
		/// <param name="permissionAccess">One of the enumeration values that specifies an access level. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. </param>
		// Token: 0x06001308 RID: 4872 RVA: 0x00050DAE File Offset: 0x0004EFAE
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.Add(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A collection that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00050DDB File Offset: 0x0004EFDB
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00050DF7 File Offset: 0x0004EFF7
		private void SetUp()
		{
			base.TagNames = new string[] { "Machine" };
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00050E1D File Offset: 0x0004F01D
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00050E25 File Offset: 0x0004F025
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00050E30 File Offset: 0x0004F030
		internal void Add(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.AddPermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00050E50 File Offset: 0x0004F050
		internal void Remove(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.RemovePermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04000ACE RID: 2766
		private EventLogPermissionEntryCollection innerCollection;
	}
}
