using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F5 RID: 245
	[CallbackIdentity(705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CheckFileSignature_t
	{
		// Token: 0x040002F6 RID: 758
		public const int k_iCallback = 705;

		// Token: 0x040002F7 RID: 759
		public ECheckFileSignature m_eCheckFileSignature;
	}
}
