using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009A RID: 154
	[CallbackIdentity(4011)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsVolume_t
	{
		// Token: 0x0400019D RID: 413
		public const int k_iCallback = 4011;

		// Token: 0x0400019E RID: 414
		public float m_flNewVolume;
	}
}
