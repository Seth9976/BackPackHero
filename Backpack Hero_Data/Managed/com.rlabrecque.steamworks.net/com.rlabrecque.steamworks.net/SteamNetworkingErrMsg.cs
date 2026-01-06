using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public struct SteamNetworkingErrMsg
	{
		// Token: 0x04000A81 RID: 2689
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}
