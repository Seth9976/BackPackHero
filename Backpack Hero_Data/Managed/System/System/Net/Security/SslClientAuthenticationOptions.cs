using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000661 RID: 1633
	public class SslClientAuthenticationOptions
	{
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000BD80F File Offset: 0x000BBA0F
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x000BD817 File Offset: 0x000BBA17
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

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000BD820 File Offset: 0x000BBA20
		// (set) Token: 0x06003440 RID: 13376 RVA: 0x000BD828 File Offset: 0x000BBA28
		public LocalCertificateSelectionCallback LocalCertificateSelectionCallback { get; set; }

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000BD831 File Offset: 0x000BBA31
		// (set) Token: 0x06003442 RID: 13378 RVA: 0x000BD839 File Offset: 0x000BBA39
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000BD842 File Offset: 0x000BBA42
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x000BD84A File Offset: 0x000BBA4A
		public List<SslApplicationProtocol> ApplicationProtocols { get; set; }

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000BD853 File Offset: 0x000BBA53
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x000BD85B File Offset: 0x000BBA5B
		public string TargetHost { get; set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000BD864 File Offset: 0x000BBA64
		// (set) Token: 0x06003448 RID: 13384 RVA: 0x000BD86C File Offset: 0x000BBA6C
		public X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000BD875 File Offset: 0x000BBA75
		// (set) Token: 0x0600344A RID: 13386 RVA: 0x000BD87D File Offset: 0x000BBA7D
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

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000BD8AB File Offset: 0x000BBAAB
		// (set) Token: 0x0600344C RID: 13388 RVA: 0x000BD8B3 File Offset: 0x000BBAB3
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

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000BD8E1 File Offset: 0x000BBAE1
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x000BD8E9 File Offset: 0x000BBAE9
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

		// Token: 0x04001FBF RID: 8127
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x04001FC0 RID: 8128
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04001FC1 RID: 8129
		private SslProtocols _enabledSslProtocols;

		// Token: 0x04001FC2 RID: 8130
		private bool _allowRenegotiation = true;
	}
}
