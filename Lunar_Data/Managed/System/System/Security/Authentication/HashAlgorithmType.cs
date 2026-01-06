using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used for generating message authentication codes (MACs).</summary>
	// Token: 0x0200029E RID: 670
	public enum HashAlgorithmType
	{
		/// <summary>No hashing algorithm is used.</summary>
		// Token: 0x04000BE0 RID: 3040
		None,
		/// <summary>The Message Digest 5 (MD5) hashing algorithm.</summary>
		// Token: 0x04000BE1 RID: 3041
		Md5 = 32771,
		/// <summary>The Secure Hashing Algorithm (SHA1).</summary>
		// Token: 0x04000BE2 RID: 3042
		Sha1,
		// Token: 0x04000BE3 RID: 3043
		Sha256 = 32780,
		// Token: 0x04000BE4 RID: 3044
		Sha384,
		// Token: 0x04000BE5 RID: 3045
		Sha512
	}
}
