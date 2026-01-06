using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000263 RID: 611
	[Serializable]
	public class EventLogPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.MachineName" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		// Token: 0x06001315 RID: 4885 RVA: 0x00050ED6 File Offset: 0x0004F0D6
		public EventLogPermissionEntry(EventLogPermissionAccess permissionAccess, string machineName)
		{
			ResourcePermissionBase.ValidateMachineName(machineName);
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
		}

		/// <summary>Gets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the computer on which to read or write events.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00050EF2 File Offset: 0x0004F0F2
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00050EFA File Offset: 0x0004F0FA
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00050F02 File Offset: 0x0004F102
		internal ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.machineName });
		}

		// Token: 0x04000AD8 RID: 2776
		private EventLogPermissionAccess permissionAccess;

		// Token: 0x04000AD9 RID: 2777
		private string machineName;
	}
}
