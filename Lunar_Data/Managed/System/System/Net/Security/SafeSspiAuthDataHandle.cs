using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200064E RID: 1614
	internal sealed class SafeSspiAuthDataHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060033D5 RID: 13269 RVA: 0x00013AC8 File Offset: 0x00011CC8
		public SafeSspiAuthDataHandle()
			: base(true)
		{
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000BC78B File Offset: 0x000BA98B
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.SspiFreeAuthIdentity(this.handle) == global::Interop.SECURITY_STATUS.OK;
		}
	}
}
