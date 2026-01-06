using System;
using System.IO;
using Mono.Btls;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002E5 RID: 741
	internal static class X509Helper2
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x0005D4D0 File Offset: 0x0005B6D0
		[MonoTODO("Investigate replacement; see comments in source.")]
		internal static X509Certificate GetMonoCertificate(X509Certificate2 certificate)
		{
			X509Certificate2ImplMono x509Certificate2ImplMono = certificate.Impl as X509Certificate2ImplMono;
			if (x509Certificate2ImplMono != null)
			{
				return x509Certificate2ImplMono.MonoCertificate;
			}
			return new X509Certificate(certificate.RawData);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0005D4FE File Offset: 0x0005B6FE
		internal static X509ChainImpl CreateChainImpl(bool useMachineContext)
		{
			return new X509ChainImplMono(useMachineContext);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0005D506 File Offset: 0x0005B706
		public static bool IsValid(X509ChainImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0005D513 File Offset: 0x0005B713
		internal static void ThrowIfContextInvalid(X509ChainImpl impl)
		{
			if (!X509Helper2.IsValid(impl))
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0005D523 File Offset: 0x0005B723
		internal static Exception GetInvalidChainContextException()
		{
			return new CryptographicException(global::Locale.GetText("Chain instance is empty."));
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0005D534 File Offset: 0x0005B734
		[Obsolete("This is only used by Mono.Security's X509Store and will be replaced shortly.")]
		internal static long GetSubjectNameHash(X509Certificate certificate)
		{
			X509Helper.ThrowIfContextInvalid(certificate.Impl);
			long hash;
			using (MonoBtlsX509 nativeInstance = X509Helper2.GetNativeInstance(certificate.Impl))
			{
				using (MonoBtlsX509Name subjectName = nativeInstance.GetSubjectName())
				{
					hash = subjectName.GetHash();
				}
			}
			return hash;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0005D59C File Offset: 0x0005B79C
		[Obsolete("This is only used by Mono.Security's X509Store and will be replaced shortly.")]
		internal static void ExportAsPEM(X509Certificate certificate, Stream stream, bool includeHumanReadableForm)
		{
			X509Helper.ThrowIfContextInvalid(certificate.Impl);
			using (MonoBtlsX509 nativeInstance = X509Helper2.GetNativeInstance(certificate.Impl))
			{
				using (MonoBtlsBio monoBtlsBio = MonoBtlsBio.CreateMonoStream(stream))
				{
					nativeInstance.ExportAsPEM(monoBtlsBio, includeHumanReadableForm);
				}
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0005D604 File Offset: 0x0005B804
		private static MonoBtlsX509 GetNativeInstance(X509CertificateImpl impl)
		{
			X509Helper.ThrowIfContextInvalid(impl);
			X509CertificateImplBtls x509CertificateImplBtls = impl as X509CertificateImplBtls;
			if (x509CertificateImplBtls != null)
			{
				return x509CertificateImplBtls.X509.Copy();
			}
			return MonoBtlsX509.LoadFromData(impl.RawData, MonoBtlsX509Format.DER);
		}
	}
}
