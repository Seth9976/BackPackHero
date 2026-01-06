using System;

namespace System.Net.Security
{
	// Token: 0x02000654 RID: 1620
	internal sealed class SafeFreeCredential_SECURITY : SafeFreeCredentials
	{
		// Token: 0x060033EC RID: 13292 RVA: 0x000BCAE9 File Offset: 0x000BACE9
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.FreeCredentialsHandle(ref this._handle) == 0;
		}
	}
}
