using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Unity
{
	// Token: 0x0200007B RID: 123
	internal class X509ChainImplUnityTls : X509ChainImpl
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x0000531C File Offset: 0x0000351C
		internal X509ChainImplUnityTls(UnityTls.unitytls_x509list_ref nativeCertificateChain, bool reverseOrder = false)
		{
			this.elements = null;
			this.nativeCertificateChain = nativeCertificateChain;
			this.reverseOrder = reverseOrder;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005344 File Offset: 0x00003544
		public override bool IsValid
		{
			get
			{
				return this.nativeCertificateChain.handle != UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005360 File Offset: 0x00003560
		public override IntPtr Handle
		{
			get
			{
				return new IntPtr((long)this.nativeCertificateChain.handle);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005372 File Offset: 0x00003572
		internal UnityTls.unitytls_x509list_ref NativeCertificateChain
		{
			get
			{
				return this.nativeCertificateChain;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000537C File Offset: 0x0000357C
		public unsafe override X509ChainElementCollection ChainElements
		{
			get
			{
				base.ThrowIfContextInvalid();
				if (this.elements != null)
				{
					return this.elements;
				}
				this.elements = new X509ChainElementCollection();
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.unitytls_x509_ref unitytls_x509_ref = UnityTls.NativeInterface.unitytls_x509list_get_x509(this.nativeCertificateChain, (IntPtr)0, &unitytls_errorstate);
				int num = 1;
				while (unitytls_x509_ref.handle != UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE)
				{
					IntPtr intPtr = UnityTls.NativeInterface.unitytls_x509_export_der(unitytls_x509_ref, null, (IntPtr)0, &unitytls_errorstate);
					byte[] array = new byte[(int)intPtr];
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					UnityTls.NativeInterface.unitytls_x509_export_der(unitytls_x509_ref, ptr, intPtr, &unitytls_errorstate);
					array2 = null;
					this.elements.Add(new X509Certificate2(array));
					unitytls_x509_ref = UnityTls.NativeInterface.unitytls_x509list_get_x509(this.nativeCertificateChain, (IntPtr)num, &unitytls_errorstate);
					num++;
				}
				if (this.reverseOrder)
				{
					X509ChainElementCollection x509ChainElementCollection = new X509ChainElementCollection();
					for (int i = this.elements.Count - 1; i >= 0; i--)
					{
						x509ChainElementCollection.Add(this.elements[i].Certificate);
					}
					this.elements = x509ChainElementCollection;
				}
				return this.elements;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000054DC File Offset: 0x000036DC
		public override void AddStatus(X509ChainStatusFlags error)
		{
			if (this.chainStatusList == null)
			{
				this.chainStatusList = new List<X509ChainStatus>();
			}
			this.chainStatusList.Add(new X509ChainStatus(error));
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00005502 File Offset: 0x00003702
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000550A File Offset: 0x0000370A
		public override X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
			set
			{
				this.policy = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00005513 File Offset: 0x00003713
		public override X509ChainStatus[] ChainStatus
		{
			get
			{
				List<X509ChainStatus> list = this.chainStatusList;
				return ((list != null) ? list.ToArray() : null) ?? new X509ChainStatus[0];
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00003062 File Offset: 0x00001262
		public override bool Build(X509Certificate2 certificate)
		{
			return false;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005531 File Offset: 0x00003731
		public override void Reset()
		{
			if (this.elements != null)
			{
				this.nativeCertificateChain.handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE;
				this.elements.Clear();
				this.elements = null;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005562 File Offset: 0x00003762
		protected override void Dispose(bool disposing)
		{
			this.Reset();
			base.Dispose(disposing);
		}

		// Token: 0x040001C6 RID: 454
		private X509ChainElementCollection elements;

		// Token: 0x040001C7 RID: 455
		private UnityTls.unitytls_x509list_ref nativeCertificateChain;

		// Token: 0x040001C8 RID: 456
		private X509ChainPolicy policy = new X509ChainPolicy();

		// Token: 0x040001C9 RID: 457
		private List<X509ChainStatus> chainStatusList;

		// Token: 0x040001CA RID: 458
		private bool reverseOrder;
	}
}
