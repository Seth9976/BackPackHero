using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Btls
{
	// Token: 0x02000100 RID: 256
	internal class MonoBtlsX509LookupMonoCollection : MonoBtlsX509LookupMono
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x00010AD7 File Offset: 0x0000ECD7
		internal MonoBtlsX509LookupMonoCollection(X509CertificateCollection collection, MonoBtlsX509TrustKind trust)
		{
			this.collection = collection;
			this.trust = trust;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		private void Initialize()
		{
			if (this.certificates != null)
			{
				return;
			}
			this.hashes = new long[this.collection.Count];
			this.certificates = new MonoBtlsX509[this.collection.Count];
			for (int i = 0; i < this.collection.Count; i++)
			{
				byte[] rawCertData = this.collection[i].GetRawCertData();
				this.certificates[i] = MonoBtlsX509.LoadFromData(rawCertData, MonoBtlsX509Format.DER);
				this.certificates[i].AddExplicitTrust(this.trust);
				this.hashes[i] = this.certificates[i].GetSubjectNameHash();
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00010B94 File Offset: 0x0000ED94
		protected override MonoBtlsX509 OnGetBySubject(MonoBtlsX509Name name)
		{
			this.Initialize();
			long hash = name.GetHash();
			MonoBtlsX509 monoBtlsX = null;
			for (int i = 0; i < this.certificates.Length; i++)
			{
				if (this.hashes[i] == hash)
				{
					monoBtlsX = this.certificates[i];
					base.AddCertificate(monoBtlsX);
				}
			}
			return monoBtlsX;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		protected override void Close()
		{
			try
			{
				if (this.certificates != null)
				{
					for (int i = 0; i < this.certificates.Length; i++)
					{
						if (this.certificates[i] != null)
						{
							this.certificates[i].Dispose();
							this.certificates[i] = null;
						}
					}
					this.certificates = null;
					this.hashes = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x04000427 RID: 1063
		private long[] hashes;

		// Token: 0x04000428 RID: 1064
		private MonoBtlsX509[] certificates;

		// Token: 0x04000429 RID: 1065
		private X509CertificateCollection collection;

		// Token: 0x0400042A RID: 1066
		private MonoBtlsX509TrustKind trust;
	}
}
