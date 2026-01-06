using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	public struct SteamNetworkingMessage_t
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x0000F801 File Offset: 0x0000DA01
		public void Release()
		{
			throw new NotImplementedException("Please use the static Release function instead which takes an IntPtr.");
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000F80D File Offset: 0x0000DA0D
		public static void Release(IntPtr pointer)
		{
			NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(pointer);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000F815 File Offset: 0x0000DA15
		public static SteamNetworkingMessage_t FromIntPtr(IntPtr pointer)
		{
			return (SteamNetworkingMessage_t)Marshal.PtrToStructure(pointer, typeof(SteamNetworkingMessage_t));
		}

		// Token: 0x04000A8B RID: 2699
		public IntPtr m_pData;

		// Token: 0x04000A8C RID: 2700
		public int m_cbSize;

		// Token: 0x04000A8D RID: 2701
		public HSteamNetConnection m_conn;

		// Token: 0x04000A8E RID: 2702
		public SteamNetworkingIdentity m_identityPeer;

		// Token: 0x04000A8F RID: 2703
		public long m_nConnUserData;

		// Token: 0x04000A90 RID: 2704
		public SteamNetworkingMicroseconds m_usecTimeReceived;

		// Token: 0x04000A91 RID: 2705
		public long m_nMessageNumber;

		// Token: 0x04000A92 RID: 2706
		public IntPtr m_pfnFreeData;

		// Token: 0x04000A93 RID: 2707
		internal IntPtr m_pfnRelease;

		// Token: 0x04000A94 RID: 2708
		public int m_nChannel;

		// Token: 0x04000A95 RID: 2709
		public int m_nFlags;

		// Token: 0x04000A96 RID: 2710
		public long m_nUserData;

		// Token: 0x04000A97 RID: 2711
		public ushort m_idxLane;

		// Token: 0x04000A98 RID: 2712
		public ushort _pad1__;
	}
}
