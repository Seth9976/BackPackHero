using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000658 RID: 1624
	internal static class CertificateHelper
	{
		// Token: 0x060033F8 RID: 13304 RVA: 0x000BCC2C File Offset: 0x000BAE2C
		internal static X509Certificate2 GetEligibleClientCertificate()
		{
			X509Certificate2Collection certificates;
			using (X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				x509Store.Open(OpenFlags.OpenExistingOnly);
				certificates = x509Store.Certificates;
			}
			return CertificateHelper.GetEligibleClientCertificate(certificates);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000BCC74 File Offset: 0x000BAE74
		internal static X509Certificate2 GetEligibleClientCertificate(X509CertificateCollection candidateCerts)
		{
			if (candidateCerts.Count == 0)
			{
				return null;
			}
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			x509Certificate2Collection.AddRange(candidateCerts);
			return CertificateHelper.GetEligibleClientCertificate(x509Certificate2Collection);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000BCC94 File Offset: 0x000BAE94
		internal static X509Certificate2 GetEligibleClientCertificate(X509Certificate2Collection candidateCerts)
		{
			if (candidateCerts.Count == 0)
			{
				return null;
			}
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (X509Certificate2 x509Certificate in candidateCerts)
			{
				if (x509Certificate.HasPrivateKey)
				{
					x509Certificate2Collection.Add(x509Certificate);
				}
			}
			if (x509Certificate2Collection.Count == 0)
			{
				return null;
			}
			x509Certificate2Collection = x509Certificate2Collection.Find(X509FindType.FindByApplicationPolicy, "1.3.6.1.5.5.7.3.2", false);
			x509Certificate2Collection = x509Certificate2Collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
			if (x509Certificate2Collection.Count > 0)
			{
				return x509Certificate2Collection[0];
			}
			return null;
		}

		// Token: 0x04001F90 RID: 8080
		private const string ClientAuthenticationOID = "1.3.6.1.5.5.7.3.2";
	}
}
