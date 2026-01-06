using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000032 RID: 50
	[CallbackIdentity(332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameServerChangeRequested_t
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0000BD51 File Offset: 0x00009F51
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0000BD5E File Offset: 0x00009F5E
		public string m_rgchServer
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchServer_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchServer_, 64);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000BD6E File Offset: 0x00009F6E
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0000BD7B File Offset: 0x00009F7B
		public string m_rgchPassword
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchPassword_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchPassword_, 64);
			}
		}

		// Token: 0x04000023 RID: 35
		public const int k_iCallback = 332;

		// Token: 0x04000024 RID: 36
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchServer_;

		// Token: 0x04000025 RID: 37
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchPassword_;
	}
}
