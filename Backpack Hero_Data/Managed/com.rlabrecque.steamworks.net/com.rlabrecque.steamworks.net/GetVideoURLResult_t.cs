using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F9 RID: 249
	[CallbackIdentity(4611)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetVideoURLResult_t
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0000C007 File Offset: 0x0000A207
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0000C014 File Offset: 0x0000A214
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x040002FD RID: 765
		public const int k_iCallback = 4611;

		// Token: 0x040002FE RID: 766
		public EResult m_eResult;

		// Token: 0x040002FF RID: 767
		public AppId_t m_unVideoAppID;

		// Token: 0x04000300 RID: 768
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;
	}
}
