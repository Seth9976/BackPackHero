using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000043 RID: 67
	[CallbackIdentity(349)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct OverlayBrowserProtocolNavigation_t
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0000BDAB File Offset: 0x00009FAB
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public string rgchURI
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.rgchURI_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.rgchURI_, 1024);
			}
		}

		// Token: 0x0400005E RID: 94
		public const int k_iCallback = 349;

		// Token: 0x0400005F RID: 95
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] rgchURI_;
	}
}
