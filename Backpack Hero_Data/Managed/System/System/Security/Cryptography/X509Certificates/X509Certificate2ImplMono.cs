using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Mono.Security;
using Mono.Security.Authenticode;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D4 RID: 724
	internal class X509Certificate2ImplMono : X509Certificate2ImplUnix
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0005A458 File Offset: 0x00058658
		public override bool IsValid
		{
			get
			{
				return this._cert != null;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00011D8D File Offset: 0x0000FF8D
		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00011D8D File Offset: 0x0000FF8D
		public override IntPtr GetNativeAppleCertificate()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005A463 File Offset: 0x00058663
		public X509Certificate2ImplMono(X509Certificate cert)
		{
			this._cert = cert;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005A472 File Offset: 0x00058672
		private X509Certificate2ImplMono(X509Certificate2ImplMono other)
		{
			this._cert = other._cert;
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005A4A0 File Offset: 0x000586A0
		public X509Certificate2ImplMono(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			switch (X509Certificate2.GetCertContentType(rawData))
			{
			case X509ContentType.Cert:
			case X509ContentType.Pkcs7:
				this._cert = new X509Certificate(rawData);
				return;
			case X509ContentType.Pfx:
				this._cert = this.ImportPkcs12(rawData, password);
				return;
			case X509ContentType.Authenticode:
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(rawData);
				this._cert = authenticodeDeformatter.SigningCertificate;
				if (this._cert != null)
				{
					return;
				}
				break;
			}
			}
			throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0005A523 File Offset: 0x00058723
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509Certificate2ImplMono(this);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0005A531 File Offset: 0x00058731
		private X509Certificate Cert
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this._cert;
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0005A53F File Offset: 0x0005873F
		protected override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.Cert.RawData;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005A552 File Offset: 0x00058752
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			result = false;
			return false;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005A558 File Offset: 0x00058758
		public X509Certificate2ImplMono()
		{
			this._cert = null;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0005A567 File Offset: 0x00058767
		public override bool HasPrivateKey
		{
			get
			{
				return this.PrivateKey != null;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0005A574 File Offset: 0x00058774
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x0005A670 File Offset: 0x00058870
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				try
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = this._cert.RSA as RSACryptoServiceProvider;
					if (rsacryptoServiceProvider != null)
					{
						if (rsacryptoServiceProvider.PublicOnly)
						{
							return null;
						}
						RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider();
						rsacryptoServiceProvider2.ImportParameters(this._cert.RSA.ExportParameters(true));
						return rsacryptoServiceProvider2;
					}
					else
					{
						Mono.Security.Cryptography.RSAManaged rsamanaged = this._cert.RSA as Mono.Security.Cryptography.RSAManaged;
						if (rsamanaged != null)
						{
							if (rsamanaged.PublicOnly)
							{
								return null;
							}
							Mono.Security.Cryptography.RSAManaged rsamanaged2 = new Mono.Security.Cryptography.RSAManaged();
							rsamanaged2.ImportParameters(this._cert.RSA.ExportParameters(true));
							return rsamanaged2;
						}
						else
						{
							DSACryptoServiceProvider dsacryptoServiceProvider = this._cert.DSA as DSACryptoServiceProvider;
							if (dsacryptoServiceProvider != null)
							{
								if (dsacryptoServiceProvider.PublicOnly)
								{
									return null;
								}
								DSACryptoServiceProvider dsacryptoServiceProvider2 = new DSACryptoServiceProvider();
								dsacryptoServiceProvider2.ImportParameters(this._cert.DSA.ExportParameters(true));
								return dsacryptoServiceProvider2;
							}
						}
					}
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (value == null)
				{
					this._cert.RSA = null;
					this._cert.DSA = null;
					return;
				}
				if (value is RSA)
				{
					this._cert.RSA = (RSA)value;
					return;
				}
				if (value is DSA)
				{
					this._cert.DSA = (DSA)value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005A6E5 File Offset: 0x000588E5
		public override RSA GetRSAPrivateKey()
		{
			return this.PrivateKey as RSA;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005A6F2 File Offset: 0x000588F2
		public override DSA GetDSAPrivateKey()
		{
			return this.PrivateKey as DSA;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0005A700 File Offset: 0x00058900
		public override PublicKey PublicKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this._publicKey == null)
				{
					try
					{
						this._publicKey = new PublicKey(this._cert);
					}
					catch (Exception ex)
					{
						throw new CryptographicException(global::Locale.GetText("Unable to decode public key."), ex);
					}
				}
				return this._publicKey;
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005A764 File Offset: 0x00058964
		private X509Certificate ImportPkcs12(byte[] rawData, SafePasswordHandle password)
		{
			if (password == null || password.IsInvalid)
			{
				return this.ImportPkcs12(rawData, null);
			}
			string text = password.Mono_DangerousGetString();
			return this.ImportPkcs12(rawData, text);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005A794 File Offset: 0x00058994
		private X509Certificate ImportPkcs12(byte[] rawData, string password)
		{
			PKCS12 pkcs = null;
			if (string.IsNullOrEmpty(password))
			{
				try
				{
					pkcs = new PKCS12(rawData, null);
					goto IL_002B;
				}
				catch
				{
					pkcs = new PKCS12(rawData, string.Empty);
					goto IL_002B;
				}
			}
			pkcs = new PKCS12(rawData, password);
			IL_002B:
			if (pkcs.Certificates.Count == 0)
			{
				return null;
			}
			if (pkcs.Keys.Count == 0)
			{
				return pkcs.Certificates[0];
			}
			X509Certificate x509Certificate = null;
			AsymmetricAlgorithm asymmetricAlgorithm = pkcs.Keys[0] as AsymmetricAlgorithm;
			string text = asymmetricAlgorithm.ToXmlString(false);
			foreach (X509Certificate x509Certificate2 in pkcs.Certificates)
			{
				if ((x509Certificate2.RSA != null && text == x509Certificate2.RSA.ToXmlString(false)) || (x509Certificate2.DSA != null && text == x509Certificate2.DSA.ToXmlString(false)))
				{
					x509Certificate = x509Certificate2;
					break;
				}
			}
			if (x509Certificate == null)
			{
				x509Certificate = pkcs.Certificates[0];
			}
			else
			{
				x509Certificate.RSA = asymmetricAlgorithm as RSA;
				x509Certificate.DSA = asymmetricAlgorithm as DSA;
			}
			if (pkcs.Certificates.Count > 1)
			{
				this.intermediateCerts = new X509CertificateImplCollection();
				foreach (X509Certificate x509Certificate3 in pkcs.Certificates)
				{
					if (x509Certificate3 != x509Certificate)
					{
						X509Certificate2ImplMono x509Certificate2ImplMono = new X509Certificate2ImplMono(x509Certificate3);
						this.intermediateCerts.Add(x509Certificate2ImplMono, true);
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005A94C File Offset: 0x00058B4C
		public override void Reset()
		{
			this._cert = null;
			this._publicKey = null;
			if (this.intermediateCerts != null)
			{
				this.intermediateCerts.Dispose();
				this.intermediateCerts = null;
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005A976 File Offset: 0x00058B76
		[MonoTODO("by default this depends on the incomplete X509Chain")]
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			if (this._cert == null)
			{
				throw new CryptographicException(X509Certificate2ImplMono.empty_error);
			}
			return X509Chain.Create().Build(thisCertificate);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0005A99C File Offset: 0x00058B9C
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12, Pkcs7 and Unknown")]
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			X509ContentType x509ContentType = X509ContentType.Unknown;
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawData);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
				}
				if (asn.Count == 0)
				{
					return x509ContentType;
				}
				if (asn.Count == 3)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
						{
							x509ContentType = X509ContentType.Cert;
						}
					}
					else if (asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						x509ContentType = X509ContentType.Pfx;
					}
				}
				if (asn[0].Tag == 6 && asn[0].CompareValue(X509Certificate2ImplMono.signedData))
				{
					x509ContentType = X509ContentType.Pkcs7;
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
			}
			return x509ContentType;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005AAA0 File Offset: 0x00058CA0
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12 and Unknown")]
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("fileName");
			}
			return X509Certificate2ImplMono.GetCertContentType(File.ReadAllBytes(fileName));
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0005AACE File Offset: 0x00058CCE
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005AAD6 File Offset: 0x00058CD6
		internal X509Certificate MonoCertificate
		{
			get
			{
				return this._cert;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00007575 File Offset: 0x00005775
		internal override X509Certificate2Impl FallbackImpl
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000CF8 RID: 3320
		private PublicKey _publicKey;

		// Token: 0x04000CF9 RID: 3321
		private X509CertificateImplCollection intermediateCerts;

		// Token: 0x04000CFA RID: 3322
		private X509Certificate _cert;

		// Token: 0x04000CFB RID: 3323
		private static string empty_error = global::Locale.GetText("Certificate instance is empty.");

		// Token: 0x04000CFC RID: 3324
		private static byte[] signedData = new byte[] { 42, 134, 72, 134, 247, 13, 1, 7, 2 };
	}
}
