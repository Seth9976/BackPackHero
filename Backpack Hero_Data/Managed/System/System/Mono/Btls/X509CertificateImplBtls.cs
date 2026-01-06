using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Security.Authenticode;
using Mono.Security.Cryptography;

namespace Mono.Btls
{
	// Token: 0x02000112 RID: 274
	internal class X509CertificateImplBtls : X509Certificate2ImplUnix
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00011BF5 File Offset: 0x0000FDF5
		internal X509CertificateImplBtls()
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00011BFD File Offset: 0x0000FDFD
		internal X509CertificateImplBtls(MonoBtlsX509 x509)
		{
			this.x509 = x509.Copy();
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00011C14 File Offset: 0x0000FE14
		private X509CertificateImplBtls(X509CertificateImplBtls other)
		{
			this.x509 = ((other.x509 != null) ? other.x509.Copy() : null);
			this.nativePrivateKey = ((other.nativePrivateKey != null) ? other.nativePrivateKey.Copy() : null);
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00011C78 File Offset: 0x0000FE78
		internal X509CertificateImplBtls(byte[] data, MonoBtlsX509Format format)
		{
			this.x509 = MonoBtlsX509.LoadFromData(data, format);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00011C90 File Offset: 0x0000FE90
		internal X509CertificateImplBtls(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			if (password == null || password.IsInvalid)
			{
				try
				{
					this.Import(data);
					return;
				}
				catch (Exception ex)
				{
					try
					{
						this.ImportPkcs12(data, null);
					}
					catch
					{
						try
						{
							this.ImportAuthenticode(data);
						}
						catch
						{
							throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
						}
					}
					return;
				}
			}
			try
			{
				this.ImportPkcs12(data, password);
			}
			catch (Exception ex2)
			{
				try
				{
					this.Import(data);
				}
				catch
				{
					try
					{
						this.ImportAuthenticode(data);
					}
					catch
					{
						throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex2);
					}
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00011D64 File Offset: 0x0000FF64
		public override bool IsValid
		{
			get
			{
				return this.x509 != null && this.x509.IsValid;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00011D7B File Offset: 0x0000FF7B
		public override IntPtr Handle
		{
			get
			{
				return this.x509.Handle.DangerousGetHandle();
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00011D8D File Offset: 0x0000FF8D
		public override IntPtr GetNativeAppleCertificate()
		{
			return IntPtr.Zero;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00011D94 File Offset: 0x0000FF94
		internal MonoBtlsX509 X509
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.x509;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00011DA2 File Offset: 0x0000FFA2
		internal MonoBtlsKey NativePrivateKey
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.nativePrivateKey;
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509CertificateImplBtls(this);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			X509CertificateImplBtls x509CertificateImplBtls = other as X509CertificateImplBtls;
			if (x509CertificateImplBtls == null)
			{
				result = false;
				return false;
			}
			result = MonoBtlsX509.Compare(this.X509, x509CertificateImplBtls.X509) == 0;
			return true;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00011DF3 File Offset: 0x0000FFF3
		protected override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.X509.GetRawData(MonoBtlsX509Format.DER);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00011E07 File Offset: 0x00010007
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00011E0F File Offset: 0x0001000F
		protected override void Dispose(bool disposing)
		{
			if (this.x509 != null)
			{
				this.x509.Dispose();
				this.x509 = null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00011E2B File Offset: 0x0001002B
		internal override X509Certificate2Impl FallbackImpl
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00011E32 File Offset: 0x00010032
		public override bool HasPrivateKey
		{
			get
			{
				return this.nativePrivateKey != null;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00011E3D File Offset: 0x0001003D
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00011E5C File Offset: 0x0001005C
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this.nativePrivateKey == null)
				{
					return null;
				}
				return PKCS8.PrivateKeyInfo.DecodeRSA(this.nativePrivateKey.GetBytes(true));
			}
			set
			{
				if (this.nativePrivateKey != null)
				{
					this.nativePrivateKey.Dispose();
				}
				try
				{
					if (value != null)
					{
						this.nativePrivateKey = MonoBtlsKey.CreateFromRSAPrivateKey((RSA)value);
					}
				}
				catch
				{
					this.nativePrivateKey = null;
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00011E3D File Offset: 0x0001003D
		public override RSA GetRSAPrivateKey()
		{
			if (this.nativePrivateKey == null)
			{
				return null;
			}
			return PKCS8.PrivateKeyInfo.DecodeRSA(this.nativePrivateKey.GetBytes(true));
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00011EB0 File Offset: 0x000100B0
		public override DSA GetDSAPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00011EB8 File Offset: 0x000100B8
		public override PublicKey PublicKey
		{
			get
			{
				base.ThrowIfContextInvalid();
				if (this.publicKey == null)
				{
					AsnEncodedData publicKeyAsn = this.X509.GetPublicKeyAsn1();
					AsnEncodedData publicKeyParameters = this.X509.GetPublicKeyParameters();
					this.publicKey = new PublicKey(publicKeyAsn.Oid, publicKeyParameters, publicKeyAsn);
				}
				return this.publicKey;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00011F04 File Offset: 0x00010104
		private void Import(byte[] data)
		{
			if (data != null)
			{
				if (data.Length != 0 && data[0] != 48)
				{
					this.x509 = MonoBtlsX509.LoadFromData(data, MonoBtlsX509Format.PEM);
					return;
				}
				this.x509 = MonoBtlsX509.LoadFromData(data, MonoBtlsX509Format.DER);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00011F30 File Offset: 0x00010130
		private void ImportPkcs12(byte[] data, SafePasswordHandle password)
		{
			using (MonoBtlsPkcs12 monoBtlsPkcs = new MonoBtlsPkcs12())
			{
				if (password == null || password.IsInvalid)
				{
					try
					{
						monoBtlsPkcs.Import(data, null);
						goto IL_0046;
					}
					catch
					{
						using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(string.Empty))
						{
							monoBtlsPkcs.Import(data, safePasswordHandle);
						}
						goto IL_0046;
					}
				}
				monoBtlsPkcs.Import(data, password);
				IL_0046:
				this.x509 = monoBtlsPkcs.GetCertificate(0);
				if (monoBtlsPkcs.HasPrivateKey)
				{
					this.nativePrivateKey = monoBtlsPkcs.GetPrivateKey();
				}
				if (monoBtlsPkcs.Count > 1)
				{
					this.intermediateCerts = new X509CertificateImplCollection();
					for (int i = 0; i < monoBtlsPkcs.Count; i++)
					{
						using (MonoBtlsX509 certificate = monoBtlsPkcs.GetCertificate(i))
						{
							if (MonoBtlsX509.Compare(certificate, this.x509) != 0)
							{
								X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(certificate);
								this.intermediateCerts.Add(x509CertificateImplBtls, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00012044 File Offset: 0x00010244
		private void ImportAuthenticode(byte[] data)
		{
			if (data != null)
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(data);
				this.Import(authenticodeDeformatter.SigningCertificate.RawData);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001206C File Offset: 0x0001026C
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			bool flag;
			using (MonoBtlsX509Chain monoBtlsX509Chain = new MonoBtlsX509Chain())
			{
				monoBtlsX509Chain.AddCertificate(this.x509.Copy());
				if (this.intermediateCerts != null)
				{
					for (int i = 0; i < this.intermediateCerts.Count; i++)
					{
						X509CertificateImplBtls x509CertificateImplBtls = (X509CertificateImplBtls)this.intermediateCerts[i];
						monoBtlsX509Chain.AddCertificate(x509CertificateImplBtls.x509.Copy());
					}
				}
				flag = MonoBtlsProvider.ValidateCertificate(monoBtlsX509Chain, null);
			}
			return flag;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000120F8 File Offset: 0x000102F8
		public override void Reset()
		{
			if (this.x509 != null)
			{
				this.x509.Dispose();
				this.x509 = null;
			}
			if (this.nativePrivateKey != null)
			{
				this.nativePrivateKey.Dispose();
				this.nativePrivateKey = null;
			}
			this.publicKey = null;
			this.intermediateCerts = null;
		}

		// Token: 0x0400046C RID: 1132
		private MonoBtlsX509 x509;

		// Token: 0x0400046D RID: 1133
		private MonoBtlsKey nativePrivateKey;

		// Token: 0x0400046E RID: 1134
		private X509CertificateImplCollection intermediateCerts;

		// Token: 0x0400046F RID: 1135
		private PublicKey publicKey;
	}
}
