using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D8 RID: 728
	internal class X509CertificateImplCollection : IDisposable
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x0005B085 File Offset: 0x00059285
		public X509CertificateImplCollection()
		{
			this.list = new List<X509CertificateImpl>();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0005B098 File Offset: 0x00059298
		private X509CertificateImplCollection(X509CertificateImplCollection other)
		{
			this.list = new List<X509CertificateImpl>();
			foreach (X509CertificateImpl x509CertificateImpl in other.list)
			{
				this.list.Add(x509CertificateImpl.Clone());
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0005B108 File Offset: 0x00059308
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000465 RID: 1125
		public X509CertificateImpl this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005B123 File Offset: 0x00059323
		public void Add(X509CertificateImpl impl, bool takeOwnership)
		{
			if (!takeOwnership)
			{
				impl = impl.Clone();
			}
			this.list.Add(impl);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0005B13C File Offset: 0x0005933C
		public X509CertificateImplCollection Clone()
		{
			return new X509CertificateImplCollection(this);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0005B144 File Offset: 0x00059344
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0005B154 File Offset: 0x00059354
		protected virtual void Dispose(bool disposing)
		{
			foreach (X509CertificateImpl x509CertificateImpl in this.list)
			{
				try
				{
					x509CertificateImpl.Dispose();
				}
				catch
				{
				}
			}
			this.list.Clear();
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0005B1C4 File Offset: 0x000593C4
		~X509CertificateImplCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x04000D00 RID: 3328
		private List<X509CertificateImpl> list;
	}
}
