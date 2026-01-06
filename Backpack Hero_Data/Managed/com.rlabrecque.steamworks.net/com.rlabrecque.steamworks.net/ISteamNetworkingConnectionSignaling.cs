using System;

namespace Steamworks
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public struct ISteamNetworkingConnectionSignaling
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0000F3D9 File Offset: 0x0000D5D9
		public bool SendSignal(HSteamNetConnection hConn, ref SteamNetConnectionInfo_t info, IntPtr pMsg, int cbMsg)
		{
			return NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref this, hConn, ref info, pMsg, cbMsg);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000F3E6 File Offset: 0x0000D5E6
		public void Release()
		{
			NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref this);
		}
	}
}
