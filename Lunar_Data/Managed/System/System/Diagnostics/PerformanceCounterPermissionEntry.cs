using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for a <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000279 RID: 633
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The <see cref="P:System.Diagnostics.PerformanceCounterPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The server on which the category of the performance counter resides. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null.-or-<paramref name="machineName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionAccess" /> is not a valid <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> value.-or-<paramref name="machineName" /> is not a valid computer name.</exception>
		// Token: 0x06001428 RID: 5160 RVA: 0x00052DDC File Offset: 0x00050FDC
		public PerformanceCounterPermissionEntry(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if ((permissionAccess | PerformanceCounterPermissionAccess.Administer) != PerformanceCounterPermissionAccess.Administer)
			{
				throw new ArgumentException("permissionAccess");
			}
			ResourcePermissionBase.ValidateMachineName(machineName);
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
			this.categoryName = categoryName;
		}

		/// <summary>Gets the name of the performance counter category (performance object).</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00052E37 File Offset: 0x00051037
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		/// <summary>Gets the name of the server on which the category of the performance counter resides.</summary>
		/// <returns>The name of the server on which the category resides.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00052E3F File Offset: 0x0005103F
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access level of the entry.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00052E47 File Offset: 0x00051047
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00052E4F File Offset: 0x0005104F
		internal ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.machineName, this.categoryName });
		}

		// Token: 0x04000B2D RID: 2861
		private const PerformanceCounterPermissionAccess All = PerformanceCounterPermissionAccess.Administer;

		// Token: 0x04000B2E RID: 2862
		private PerformanceCounterPermissionAccess permissionAccess;

		// Token: 0x04000B2F RID: 2863
		private string machineName;

		// Token: 0x04000B30 RID: 2864
		private string categoryName;
	}
}
