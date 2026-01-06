using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address host suffix was located.</summary>
	// Token: 0x02000511 RID: 1297
	public enum SuffixOrigin
	{
		/// <summary>The suffix was located using an unspecified source.</summary>
		// Token: 0x0400188F RID: 6287
		Other,
		/// <summary>The suffix was manually configured.</summary>
		// Token: 0x04001890 RID: 6288
		Manual,
		/// <summary>The suffix is a well-known suffix. Well-known suffixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such suffixes are reserved for special purposes.</summary>
		// Token: 0x04001891 RID: 6289
		WellKnown,
		/// <summary>The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x04001892 RID: 6290
		OriginDhcp,
		/// <summary>The suffix is a link-local suffix.</summary>
		// Token: 0x04001893 RID: 6291
		LinkLayerAddress,
		/// <summary>The suffix was randomly assigned.</summary>
		// Token: 0x04001894 RID: 6292
		Random
	}
}
