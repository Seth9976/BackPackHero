using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000064 RID: 100
	public static class RSACertificateExtensions
	{
		// Token: 0x06000224 RID: 548 RVA: 0x000061EF File Offset: 0x000043EF
		public static RSA GetRSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey)
			{
				return null;
			}
			return certificate.Impl.GetRSAPrivateKey();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006214 File Offset: 0x00004414
		public static RSA GetRSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PublicKey.Key as RSA;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006234 File Offset: 0x00004434
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, RSA privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			return (X509Certificate2)certificate.Impl.CopyWithPrivateKey(privateKey).CreateCertificate();
		}
	}
}
