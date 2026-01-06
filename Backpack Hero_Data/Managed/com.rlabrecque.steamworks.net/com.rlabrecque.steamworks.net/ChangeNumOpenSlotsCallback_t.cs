using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008B RID: 139
	[CallbackIdentity(5304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ChangeNumOpenSlotsCallback_t
	{
		// Token: 0x0400018A RID: 394
		public const int k_iCallback = 5304;

		// Token: 0x0400018B RID: 395
		public EResult m_eResult;
	}
}
