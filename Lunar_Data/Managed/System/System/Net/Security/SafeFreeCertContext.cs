using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000651 RID: 1617
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060033DF RID: 13279 RVA: 0x00013AC8 File Offset: 0x00011CC8
		internal SafeFreeCertContext()
			: base(true)
		{
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x00013AF1 File Offset: 0x00011CF1
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000BC8A4 File Offset: 0x000BAAA4
		protected override bool ReleaseHandle()
		{
			global::Interop.Crypt32.CertFreeCertificateContext(this.handle);
			return true;
		}

		// Token: 0x04001F8C RID: 8076
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
