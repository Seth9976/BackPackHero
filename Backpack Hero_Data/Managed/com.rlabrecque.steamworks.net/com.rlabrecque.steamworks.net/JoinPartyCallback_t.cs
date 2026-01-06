using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000088 RID: 136
	[CallbackIdentity(5301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct JoinPartyCallback_t
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0000BE27 File Offset: 0x0000A027
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0000BE34 File Offset: 0x0000A034
		public string m_rgchConnectString
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnectString_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnectString_, 256);
			}
		}

		// Token: 0x0400017F RID: 383
		public const int k_iCallback = 5301;

		// Token: 0x04000180 RID: 384
		public EResult m_eResult;

		// Token: 0x04000181 RID: 385
		public PartyBeaconID_t m_ulBeaconID;

		// Token: 0x04000182 RID: 386
		public CSteamID m_SteamIDBeaconOwner;

		// Token: 0x04000183 RID: 387
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnectString_;
	}
}
