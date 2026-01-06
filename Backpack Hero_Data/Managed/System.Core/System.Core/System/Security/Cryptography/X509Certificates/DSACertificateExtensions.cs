using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000062 RID: 98
	public static class DSACertificateExtensions
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00006191 File Offset: 0x00004391
		public static DSA GetDSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PrivateKey as DSA;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000061AC File Offset: 0x000043AC
		public static DSA GetDSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PublicKey.Key as DSA;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000061CC File Offset: 0x000043CC
		[global::System.MonoTODO]
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, DSA privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			throw new NotImplementedException();
		}
	}
}
