using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A2 RID: 162
	internal abstract class MonoSslAuthenticationOptions : IMonoAuthenticationOptions
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E3 RID: 739
		public abstract bool ServerMode { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		public abstract bool AllowRenegotiation { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		public abstract RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E8 RID: 744
		// (set) Token: 0x060002E9 RID: 745
		public abstract SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002EA RID: 746
		// (set) Token: 0x060002EB RID: 747
		public abstract EncryptionPolicy EncryptionPolicy { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002EC RID: 748
		// (set) Token: 0x060002ED RID: 749
		public abstract X509RevocationMode CertificateRevocationCheckMode { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EE RID: 750
		// (set) Token: 0x060002EF RID: 751
		public abstract string TargetHost { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F0 RID: 752
		// (set) Token: 0x060002F1 RID: 753
		public abstract X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F2 RID: 754
		// (set) Token: 0x060002F3 RID: 755
		public abstract X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F4 RID: 756
		// (set) Token: 0x060002F5 RID: 757
		public abstract bool ClientCertificateRequired { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00009147 File Offset: 0x00007347
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000914F File Offset: 0x0000734F
		internal ServerCertSelectionCallback ServerCertSelectionDelegate { get; set; }
	}
}
