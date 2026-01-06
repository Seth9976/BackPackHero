using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009B RID: 155
	[CallbackIdentity(4012)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x0400019F RID: 415
		public const int k_iCallback = 4012;

		// Token: 0x040001A0 RID: 416
		public int nID;
	}
}
