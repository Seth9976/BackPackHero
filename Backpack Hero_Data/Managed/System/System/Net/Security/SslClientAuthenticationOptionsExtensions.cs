using System;

namespace System.Net.Security
{
	// Token: 0x0200065E RID: 1630
	internal static class SslClientAuthenticationOptionsExtensions
	{
		// Token: 0x0600340F RID: 13327 RVA: 0x000BD280 File Offset: 0x000BB480
		public static SslClientAuthenticationOptions ShallowClone(this SslClientAuthenticationOptions options)
		{
			return new SslClientAuthenticationOptions
			{
				AllowRenegotiation = options.AllowRenegotiation,
				ApplicationProtocols = options.ApplicationProtocols,
				CertificateRevocationCheckMode = options.CertificateRevocationCheckMode,
				ClientCertificates = options.ClientCertificates,
				EnabledSslProtocols = options.EnabledSslProtocols,
				EncryptionPolicy = options.EncryptionPolicy,
				LocalCertificateSelectionCallback = options.LocalCertificateSelectionCallback,
				RemoteCertificateValidationCallback = options.RemoteCertificateValidationCallback,
				TargetHost = options.TargetHost
			};
		}
	}
}
