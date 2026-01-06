using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000037 RID: 55
	[CallbackIdentity(337)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000BD8B File Offset: 0x00009F8B
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0000BD98 File Offset: 0x00009F98
		public string m_rgchConnect
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnect_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnect_, 256);
			}
		}

		// Token: 0x04000035 RID: 53
		public const int k_iCallback = 337;

		// Token: 0x04000036 RID: 54
		public CSteamID m_steamIDFriend;

		// Token: 0x04000037 RID: 55
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnect_;
	}
}
