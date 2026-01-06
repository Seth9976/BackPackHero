using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security.Private
{
	// Token: 0x020000AA RID: 170
	internal static class CallbackHelpers
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000A5FC File Offset: 0x000087FC
		internal static MonoRemoteCertificateValidationCallback PublicToMono(RemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A62C File Offset: 0x0000882C
		internal static MonoRemoteCertificateValidationCallback InternalToMono(RemoteCertValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A65C File Offset: 0x0000885C
		internal static RemoteCertificateValidationCallback InternalToPublic(string hostname, RemoteCertValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(hostname, c, ch, e);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A694 File Offset: 0x00008894
		internal static MonoLocalCertificateSelectionCallback InternalToMono(LocalCertSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A6C4 File Offset: 0x000088C4
		internal static LocalCertificateSelectionCallback MonoToPublic(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A6F4 File Offset: 0x000088F4
		internal static RemoteCertValidationCallback MonoToInternal(MonoRemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, SslPolicyErrors e) => callback(h, c, ch, (MonoSslPolicyErrors)e);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A724 File Offset: 0x00008924
		internal static LocalCertSelectionCallback MonoToInternal(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A754 File Offset: 0x00008954
		internal static ServerCertificateSelectionCallback MonoToPublic(MonoServerCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string h) => callback(s, h);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A784 File Offset: 0x00008984
		internal static MonoServerCertificateSelectionCallback PublicToMono(ServerCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (object s, string h) => callback(s, h);
		}
	}
}
