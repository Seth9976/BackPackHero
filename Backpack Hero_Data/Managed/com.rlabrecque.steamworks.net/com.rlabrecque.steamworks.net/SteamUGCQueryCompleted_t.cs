using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C4 RID: 196
	[CallbackIdentity(3401)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCQueryCompleted_t
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0000BF67 File Offset: 0x0000A167
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0000BF74 File Offset: 0x0000A174
		public string m_rgchNextCursor
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchNextCursor_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchNextCursor_, 256);
			}
		}

		// Token: 0x04000244 RID: 580
		public const int k_iCallback = 3401;

		// Token: 0x04000245 RID: 581
		public UGCQueryHandle_t m_handle;

		// Token: 0x04000246 RID: 582
		public EResult m_eResult;

		// Token: 0x04000247 RID: 583
		public uint m_unNumResultsReturned;

		// Token: 0x04000248 RID: 584
		public uint m_unTotalMatchingResults;

		// Token: 0x04000249 RID: 585
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;

		// Token: 0x0400024A RID: 586
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchNextCursor_;
	}
}
