using System;

namespace System.Net
{
	// Token: 0x020003FD RID: 1021
	internal enum CertificateEncoding
	{
		// Token: 0x04001257 RID: 4695
		Zero,
		// Token: 0x04001258 RID: 4696
		X509AsnEncoding,
		// Token: 0x04001259 RID: 4697
		X509NdrEncoding,
		// Token: 0x0400125A RID: 4698
		Pkcs7AsnEncoding = 65536,
		// Token: 0x0400125B RID: 4699
		Pkcs7NdrEncoding = 131072,
		// Token: 0x0400125C RID: 4700
		AnyAsnEncoding = 65537
	}
}
