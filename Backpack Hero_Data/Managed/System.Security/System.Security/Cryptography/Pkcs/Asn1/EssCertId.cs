using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C4 RID: 196
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class EssCertId
	{
		// Token: 0x04000378 RID: 888
		[OctetString]
		public ReadOnlyMemory<byte> Hash;

		// Token: 0x04000379 RID: 889
		[OptionalValue]
		public CadesIssuerSerial? IssuerSerial;
	}
}
