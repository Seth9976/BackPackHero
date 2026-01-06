using System;

namespace System.Net.Security
{
	// Token: 0x02000655 RID: 1621
	internal sealed class SafeDeleteContext_SECURITY : SafeDeleteContext
	{
		// Token: 0x060033ED RID: 13293 RVA: 0x000BCAF9 File Offset: 0x000BACF9
		internal SafeDeleteContext_SECURITY()
		{
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000BCB01 File Offset: 0x000BAD01
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return global::Interop.SspiCli.DeleteSecurityContext(ref this._handle) == 0;
		}
	}
}
