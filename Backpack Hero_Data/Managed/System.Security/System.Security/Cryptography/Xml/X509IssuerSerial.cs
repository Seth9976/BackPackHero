using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;X509IssuerSerial&gt; element of an XML digital signature.</summary>
	// Token: 0x02000019 RID: 25
	public struct X509IssuerSerial
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000030C4 File Offset: 0x000012C4
		internal X509IssuerSerial(string issuerName, string serialNumber)
		{
			this = default(X509IssuerSerial);
			this.IssuerName = issuerName;
			this.SerialNumber = serialNumber;
		}

		/// <summary>Gets or sets an X.509 certificate issuer's distinguished name.</summary>
		/// <returns>An X.509 certificate issuer's distinguished name.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030DB File Offset: 0x000012DB
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000030E3 File Offset: 0x000012E3
		public string IssuerName { readonly get; set; }

		/// <summary>Gets or sets an X.509 certificate issuer's serial number.</summary>
		/// <returns>An X.509 certificate issuer's serial number.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000030EC File Offset: 0x000012EC
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000030F4 File Offset: 0x000012F4
		public string SerialNumber { readonly get; set; }
	}
}
