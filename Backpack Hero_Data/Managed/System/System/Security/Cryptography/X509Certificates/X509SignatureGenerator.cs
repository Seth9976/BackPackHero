using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002EA RID: 746
	public abstract class X509SignatureGenerator
	{
		// Token: 0x060017D6 RID: 6102 RVA: 0x0005E658 File Offset: 0x0005C858
		protected X509SignatureGenerator()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00011EB0 File Offset: 0x000100B0
		public PublicKey PublicKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060017D8 RID: 6104
		protected abstract PublicKey BuildPublicKey();

		// Token: 0x060017D9 RID: 6105 RVA: 0x00011EB0 File Offset: 0x000100B0
		public static X509SignatureGenerator CreateForECDsa(ECDsa key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00011EB0 File Offset: 0x000100B0
		public static X509SignatureGenerator CreateForRSA(RSA key, RSASignaturePadding signaturePadding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DB RID: 6107
		public abstract byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm);

		// Token: 0x060017DC RID: 6108
		public abstract byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm);
	}
}
