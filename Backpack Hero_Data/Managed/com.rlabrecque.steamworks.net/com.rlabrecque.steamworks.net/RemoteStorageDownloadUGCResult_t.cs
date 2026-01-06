using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B1 RID: 177
	[CallbackIdentity(1317)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x040001E5 RID: 485
		public const int k_iCallback = 1317;

		// Token: 0x040001E6 RID: 486
		public EResult m_eResult;

		// Token: 0x040001E7 RID: 487
		public UGCHandle_t m_hFile;

		// Token: 0x040001E8 RID: 488
		public AppId_t m_nAppID;

		// Token: 0x040001E9 RID: 489
		public int m_nSizeInBytes;

		// Token: 0x040001EA RID: 490
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x040001EB RID: 491
		public ulong m_ulSteamIDOwner;
	}
}
