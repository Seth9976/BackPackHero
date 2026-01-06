using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000560 RID: 1376
	internal struct Win32_MIB_UDPSTATS
	{
		// Token: 0x04001A1F RID: 6687
		public uint InDatagrams;

		// Token: 0x04001A20 RID: 6688
		public uint NoPorts;

		// Token: 0x04001A21 RID: 6689
		public uint InErrors;

		// Token: 0x04001A22 RID: 6690
		public uint OutDatagrams;

		// Token: 0x04001A23 RID: 6691
		public int NumAddrs;
	}
}
