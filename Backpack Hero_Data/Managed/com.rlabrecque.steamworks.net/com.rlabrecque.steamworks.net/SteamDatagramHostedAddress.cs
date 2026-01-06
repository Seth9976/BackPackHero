using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000191 RID: 401
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramHostedAddress
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public void Clear()
		{
			this.m_cbSize = 0;
			this.m_data = new byte[128];
		}

		// Token: 0x04000A50 RID: 2640
		public int m_cbSize;

		// Token: 0x04000A51 RID: 2641
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] m_data;
	}
}
