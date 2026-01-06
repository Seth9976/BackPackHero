using System;

namespace System
{
	/// <summary>Defines the parts of a URI for the <see cref="M:System.Uri.GetLeftPart(System.UriPartial)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000155 RID: 341
	public enum UriPartial
	{
		/// <summary>The scheme segment of the URI.</summary>
		// Token: 0x04000608 RID: 1544
		Scheme,
		/// <summary>The scheme and authority segments of the URI.</summary>
		// Token: 0x04000609 RID: 1545
		Authority,
		/// <summary>The scheme, authority, and path segments of the URI.</summary>
		// Token: 0x0400060A RID: 1546
		Path,
		/// <summary>The scheme, authority, path, and query segments of the URI.</summary>
		// Token: 0x0400060B RID: 1547
		Query
	}
}
