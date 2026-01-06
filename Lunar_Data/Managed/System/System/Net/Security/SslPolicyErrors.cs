using System;

namespace System.Net.Security
{
	/// <summary>Enumerates Secure Socket Layer (SSL) policy errors.</summary>
	// Token: 0x0200066B RID: 1643
	[Flags]
	public enum SslPolicyErrors
	{
		/// <summary>No SSL policy errors.</summary>
		// Token: 0x04001FE7 RID: 8167
		None = 0,
		/// <summary>Certificate not available.</summary>
		// Token: 0x04001FE8 RID: 8168
		RemoteCertificateNotAvailable = 1,
		/// <summary>Certificate name mismatch.</summary>
		// Token: 0x04001FE9 RID: 8169
		RemoteCertificateNameMismatch = 2,
		/// <summary>
		///   <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> has returned a non empty array.</summary>
		// Token: 0x04001FEA RID: 8170
		RemoteCertificateChainErrors = 4
	}
}
