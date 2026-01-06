using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive performance counter permission checks. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000278 RID: 632
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06001420 RID: 5152 RVA: 0x00052D44 File Offset: 0x00050F44
		public PerformanceCounterPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the performance counter category.</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00052D6A File Offset: 0x00050F6A
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x00052D72 File Offset: 0x00050F72
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CategoryName");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the computer name for the performance counter.</summary>
		/// <returns>The server on which the category of the performance counter resides.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.MachineName" /> format is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00052D89 File Offset: 0x00050F89
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00052D91 File Offset: 0x00050F91
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				ResourcePermissionBase.ValidateMachineName(value);
				this.machineName = value;
			}
		}

		/// <summary>Gets or sets the access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00052DA0 File Offset: 0x00050FA0
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00052DA8 File Offset: 0x00050FA8
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		/// <summary>Creates the permission based on the requested access levels that are set through the <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001427 RID: 5159 RVA: 0x00052DB1 File Offset: 0x00050FB1
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.permissionAccess, this.machineName, this.categoryName);
		}

		// Token: 0x04000B2A RID: 2858
		private string categoryName;

		// Token: 0x04000B2B RID: 2859
		private string machineName;

		// Token: 0x04000B2C RID: 2860
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
