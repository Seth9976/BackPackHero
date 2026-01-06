using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000662 RID: 1634
	public class SslServerAuthenticationOptions
	{
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000BD901 File Offset: 0x000BBB01
		// (set) Token: 0x06003451 RID: 13393 RVA: 0x000BD909 File Offset: 0x000BBB09
		public bool AllowRenegotiation
		{
			get
			{
				return this._allowRenegotiation;
			}
			set
			{
				this._allowRenegotiation = value;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000BD912 File Offset: 0x000BBB12
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x000BD91A File Offset: 0x000BBB1A
		public bool ClientCertificateRequired { get; set; }

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000BD923 File Offset: 0x000BBB23
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x000BD92B File Offset: 0x000BBB2B
		public List<SslApplicationProtocol> ApplicationProtocols { get; set; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000BD934 File Offset: 0x000BBB34
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x000BD93C File Offset: 0x000BBB3C
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000BD945 File Offset: 0x000BBB45
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x000BD94D File Offset: 0x000BBB4D
		public ServerCertificateSelectionCallback ServerCertificateSelectionCallback { get; set; }

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000BD956 File Offset: 0x000BBB56
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000BD95E File Offset: 0x000BBB5E
		public X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000BD967 File Offset: 0x000BBB67
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x000BD96F File Offset: 0x000BBB6F
		public SslProtocols EnabledSslProtocols
		{
			get
			{
				return this._enabledSslProtocols;
			}
			set
			{
				this._enabledSslProtocols = value;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000BD978 File Offset: 0x000BBB78
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x000BD980 File Offset: 0x000BBB80
		public X509RevocationMode CertificateRevocationCheckMode
		{
			get
			{
				return this._checkCertificateRevocation;
			}
			set
			{
				if (value != X509RevocationMode.NoCheck && value != X509RevocationMode.Offline && value != X509RevocationMode.Online)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "X509RevocationMode"), "value");
				}
				this._checkCertificateRevocation = value;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000BD9AE File Offset: 0x000BBBAE
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x000BD9B6 File Offset: 0x000BBBB6
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this._encryptionPolicy;
			}
			set
			{
				if (value != EncryptionPolicy.RequireEncryption && value != EncryptionPolicy.AllowNoEncryption && value != EncryptionPolicy.NoEncryption)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "EncryptionPolicy"), "value");
				}
				this._encryptionPolicy = value;
			}
		}

		// Token: 0x04001FC8 RID: 8136
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04001FC9 RID: 8137
		private SslProtocols _enabledSslProtocols;

		// Token: 0x04001FCA RID: 8138
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x04001FCB RID: 8139
		private bool _allowRenegotiation = true;
	}
}
