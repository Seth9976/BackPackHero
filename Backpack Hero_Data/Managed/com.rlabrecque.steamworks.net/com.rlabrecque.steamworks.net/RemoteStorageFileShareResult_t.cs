using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A9 RID: 169
	[CallbackIdentity(1307)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileShareResult_t
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0000BE87 File Offset: 0x0000A087
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0000BE94 File Offset: 0x0000A094
		public string m_rgchFilename
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchFilename_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchFilename_, 260);
			}
		}

		// Token: 0x040001C5 RID: 453
		public const int k_iCallback = 1307;

		// Token: 0x040001C6 RID: 454
		public EResult m_eResult;

		// Token: 0x040001C7 RID: 455
		public UGCHandle_t m_hFile;

		// Token: 0x040001C8 RID: 456
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_rgchFilename_;
	}
}
