using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002D RID: 45
	[CallbackIdentity(1021)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0000BD31 File Offset: 0x00009F31
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0000BD3E File Offset: 0x00009F3E
		public string m_rgchKey
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchKey_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchKey_, 240);
			}
		}

		// Token: 0x0400000F RID: 15
		public const int k_iCallback = 1021;

		// Token: 0x04000010 RID: 16
		public EResult m_eResult;

		// Token: 0x04000011 RID: 17
		public uint m_nAppID;

		// Token: 0x04000012 RID: 18
		public uint m_cchKeyLength;

		// Token: 0x04000013 RID: 19
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)]
		private byte[] m_rgchKey_;
	}
}
