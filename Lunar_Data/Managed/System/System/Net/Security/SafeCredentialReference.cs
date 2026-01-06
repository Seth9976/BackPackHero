using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000653 RID: 1619
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x060033E8 RID: 13288 RVA: 0x000BCA6C File Offset: 0x000BAC6C
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000BCA8C File Offset: 0x000BAC8C
		private SafeCredentialReference(SafeFreeCredentials target)
		{
			bool flag = false;
			target.DangerousAddRef(ref flag);
			this.Target = target;
			base.SetHandle(new IntPtr(0));
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000BCABC File Offset: 0x000BACBC
		protected override bool ReleaseHandle()
		{
			SafeFreeCredentials target = this.Target;
			if (target != null)
			{
				target.DangerousRelease();
			}
			this.Target = null;
			return true;
		}

		// Token: 0x04001F8E RID: 8078
		internal SafeFreeCredentials Target;
	}
}
