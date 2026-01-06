using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F0 RID: 240
	[CallbackIdentity(1112)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalStatsReceived_t
	{
		// Token: 0x040002EB RID: 747
		public const int k_iCallback = 1112;

		// Token: 0x040002EC RID: 748
		public ulong m_nGameID;

		// Token: 0x040002ED RID: 749
		public EResult m_eResult;
	}
}
