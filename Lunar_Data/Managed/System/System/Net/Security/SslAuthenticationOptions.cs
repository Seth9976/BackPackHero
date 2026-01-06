using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000660 RID: 1632
	internal class SslAuthenticationOptions
	{
		// Token: 0x0600341C RID: 13340 RVA: 0x000BD5D8 File Offset: 0x000BB7D8
		internal SslAuthenticationOptions(SslClientAuthenticationOptions sslClientAuthenticationOptions, RemoteCertValidationCallback remoteCallback, LocalCertSelectionCallback localCallback)
		{
			this.AllowRenegotiation = sslClientAuthenticationOptions.AllowRenegotiation;
			this.ApplicationProtocols = sslClientAuthenticationOptions.ApplicationProtocols;
			this.CertValidationDelegate = remoteCallback;
			this.CheckCertName = true;
			this.EnabledSslProtocols = sslClientAuthenticationOptions.EnabledSslProtocols;
			this.EncryptionPolicy = sslClientAuthenticationOptions.EncryptionPolicy;
			this.IsServer = false;
			this.RemoteCertRequired = true;
			this.RemoteCertificateValidationCallback = sslClientAuthenticationOptions.RemoteCertificateValidationCallback;
			this.TargetHost = sslClientAuthenticationOptions.TargetHost;
			this.CertSelectionDelegate = localCallback;
			this.CertificateRevocationCheckMode = sslClientAuthenticationOptions.CertificateRevocationCheckMode;
			this.ClientCertificates = sslClientAuthenticationOptions.ClientCertificates;
			this.LocalCertificateSelectionCallback = sslClientAuthenticationOptions.LocalCertificateSelectionCallback;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000BD67C File Offset: 0x000BB87C
		internal SslAuthenticationOptions(SslServerAuthenticationOptions sslServerAuthenticationOptions)
		{
			this.AllowRenegotiation = sslServerAuthenticationOptions.AllowRenegotiation;
			this.ApplicationProtocols = sslServerAuthenticationOptions.ApplicationProtocols;
			this.CheckCertName = false;
			this.EnabledSslProtocols = sslServerAuthenticationOptions.EnabledSslProtocols;
			this.EncryptionPolicy = sslServerAuthenticationOptions.EncryptionPolicy;
			this.IsServer = true;
			this.RemoteCertRequired = sslServerAuthenticationOptions.ClientCertificateRequired;
			this.RemoteCertificateValidationCallback = sslServerAuthenticationOptions.RemoteCertificateValidationCallback;
			this.TargetHost = string.Empty;
			this.CertificateRevocationCheckMode = sslServerAuthenticationOptions.CertificateRevocationCheckMode;
			this.ServerCertificate = sslServerAuthenticationOptions.ServerCertificate;
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000BD708 File Offset: 0x000BB908
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x000BD710 File Offset: 0x000BB910
		internal bool AllowRenegotiation { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x000BD719 File Offset: 0x000BB919
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x000BD721 File Offset: 0x000BB921
		internal string TargetHost { get; set; }

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000BD72A File Offset: 0x000BB92A
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x000BD732 File Offset: 0x000BB932
		internal X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000BD73B File Offset: 0x000BB93B
		internal List<SslApplicationProtocol> ApplicationProtocols { get; }

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000BD743 File Offset: 0x000BB943
		// (set) Token: 0x06003426 RID: 13350 RVA: 0x000BD74B File Offset: 0x000BB94B
		internal bool IsServer { get; set; }

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000BD754 File Offset: 0x000BB954
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x000BD75C File Offset: 0x000BB95C
		internal RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000BD765 File Offset: 0x000BB965
		// (set) Token: 0x0600342A RID: 13354 RVA: 0x000BD76D File Offset: 0x000BB96D
		internal LocalCertificateSelectionCallback LocalCertificateSelectionCallback { get; set; }

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000BD776 File Offset: 0x000BB976
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x000BD77E File Offset: 0x000BB97E
		internal X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000BD787 File Offset: 0x000BB987
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x000BD78F File Offset: 0x000BB98F
		internal SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000BD798 File Offset: 0x000BB998
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x000BD7A0 File Offset: 0x000BB9A0
		internal X509RevocationMode CertificateRevocationCheckMode { get; set; }

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000BD7A9 File Offset: 0x000BB9A9
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x000BD7B1 File Offset: 0x000BB9B1
		internal EncryptionPolicy EncryptionPolicy { get; set; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000BD7BA File Offset: 0x000BB9BA
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x000BD7C2 File Offset: 0x000BB9C2
		internal bool RemoteCertRequired { get; set; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000BD7CB File Offset: 0x000BB9CB
		// (set) Token: 0x06003436 RID: 13366 RVA: 0x000BD7D3 File Offset: 0x000BB9D3
		internal bool CheckCertName { get; set; }

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000BD7DC File Offset: 0x000BB9DC
		// (set) Token: 0x06003438 RID: 13368 RVA: 0x000BD7E4 File Offset: 0x000BB9E4
		internal RemoteCertValidationCallback CertValidationDelegate { get; set; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000BD7ED File Offset: 0x000BB9ED
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x000BD7F5 File Offset: 0x000BB9F5
		internal LocalCertSelectionCallback CertSelectionDelegate { get; set; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000BD7FE File Offset: 0x000BB9FE
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x000BD806 File Offset: 0x000BBA06
		internal ServerCertSelectionCallback ServerCertSelectionDelegate { get; set; }
	}
}
