using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies permission to request information from Domain Name Servers.</summary>
	// Token: 0x02000490 RID: 1168
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DnsPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x060024D3 RID: 9427 RVA: 0x0007ABEA File Offset: 0x00078DEA
		public DnsPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.DnsPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.DnsPermission" /> that corresponds to the security declaration.</returns>
		// Token: 0x060024D4 RID: 9428 RVA: 0x00087AAF File Offset: 0x00085CAF
		public override IPermission CreatePermission()
		{
			return new DnsPermission(base.Unrestricted ? PermissionState.Unrestricted : PermissionState.None);
		}
	}
}
