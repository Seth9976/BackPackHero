using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E2 RID: 226
	[CallbackIdentity(165)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StoreAuthURLResponse_t
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0000BFA7 File Offset: 0x0000A1A7
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 512);
			}
		}

		// Token: 0x040002AF RID: 687
		public const int k_iCallback = 165;

		// Token: 0x040002B0 RID: 688
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		private byte[] m_szURL_;
	}
}
