using System;

namespace System.Net.NetworkInformation
{
	/// <summary>The scope level for an IPv6 address.</summary>
	// Token: 0x02000510 RID: 1296
	public enum ScopeLevel
	{
		/// <summary>The scope level is not specified.</summary>
		// Token: 0x04001886 RID: 6278
		None,
		/// <summary>The scope is interface-level.</summary>
		// Token: 0x04001887 RID: 6279
		Interface,
		/// <summary>The scope is link-level.</summary>
		// Token: 0x04001888 RID: 6280
		Link,
		/// <summary>The scope is subnet-level.</summary>
		// Token: 0x04001889 RID: 6281
		Subnet,
		/// <summary>The scope is admin-level.</summary>
		// Token: 0x0400188A RID: 6282
		Admin,
		/// <summary>The scope is site-level.</summary>
		// Token: 0x0400188B RID: 6283
		Site,
		/// <summary>The scope is organization-level.</summary>
		// Token: 0x0400188C RID: 6284
		Organization = 8,
		/// <summary>The scope is global.</summary>
		// Token: 0x0400188D RID: 6285
		Global = 14
	}
}
