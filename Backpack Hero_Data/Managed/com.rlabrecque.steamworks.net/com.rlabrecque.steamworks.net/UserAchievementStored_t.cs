using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E7 RID: 231
	[CallbackIdentity(1103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementStored_t
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0000BFC7 File Offset: 0x0000A1C7
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public string m_rgchAchievementName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchAchievementName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchAchievementName_, 128);
			}
		}

		// Token: 0x040002C7 RID: 711
		public const int k_iCallback = 1103;

		// Token: 0x040002C8 RID: 712
		public ulong m_nGameID;

		// Token: 0x040002C9 RID: 713
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGroupAchievement;

		// Token: 0x040002CA RID: 714
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x040002CB RID: 715
		public uint m_nCurProgress;

		// Token: 0x040002CC RID: 716
		public uint m_nMaxProgress;
	}
}
