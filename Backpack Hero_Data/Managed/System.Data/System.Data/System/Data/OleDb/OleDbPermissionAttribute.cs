using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200010A RID: 266
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OleDbPermissionAttribute : DBDataPermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values representing an action that can be performed by using declarative security. </param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0004EF92 File Offset: 0x0004D192
		public OleDbPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a comma-delimited string that contains a list of supported providers.</summary>
		/// <returns>A comma-delimited list of providers allowed by the security policy.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0004EF9C File Offset: 0x0004D19C
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x0004EFBA File Offset: 0x0004D1BA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Provider property has been deprecated.  Use the Add method.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public string Provider
		{
			get
			{
				string providers = this._providers;
				if (providers == null)
				{
					return ADP.StrEmpty;
				}
				return providers;
			}
			set
			{
				this._providers = value;
			}
		}

		/// <summary>Returns an <see cref="T:System.Data.OleDb.OleDbPermission" /> object that is configured according to the attribute properties.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbPermission" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0004EFC3 File Offset: 0x0004D1C3
		public override IPermission CreatePermission()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000999 RID: 2457
		private string _providers;
	}
}
