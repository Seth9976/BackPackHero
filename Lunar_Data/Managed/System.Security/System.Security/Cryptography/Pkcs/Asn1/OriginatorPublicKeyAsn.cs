using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B0 RID: 176
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class OriginatorPublicKeyAsn
	{
		// Token: 0x04000315 RID: 789
		internal AlgorithmIdentifierAsn Algorithm;

		// Token: 0x04000316 RID: 790
		[BitString]
		internal ReadOnlyMemory<byte> PublicKey;
	}
}
