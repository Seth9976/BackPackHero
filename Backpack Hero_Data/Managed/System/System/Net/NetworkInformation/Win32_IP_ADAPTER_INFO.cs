using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000551 RID: 1361
	internal struct Win32_IP_ADAPTER_INFO
	{
		// Token: 0x040019B8 RID: 6584
		private const int MAX_ADAPTER_NAME_LENGTH = 256;

		// Token: 0x040019B9 RID: 6585
		private const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;

		// Token: 0x040019BA RID: 6586
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x040019BB RID: 6587
		public IntPtr Next;

		// Token: 0x040019BC RID: 6588
		public int ComboIndex;

		// Token: 0x040019BD RID: 6589
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string AdapterName;

		// Token: 0x040019BE RID: 6590
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string Description;

		// Token: 0x040019BF RID: 6591
		public uint AddressLength;

		// Token: 0x040019C0 RID: 6592
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] Address;

		// Token: 0x040019C1 RID: 6593
		public uint Index;

		// Token: 0x040019C2 RID: 6594
		public uint Type;

		// Token: 0x040019C3 RID: 6595
		public uint DhcpEnabled;

		// Token: 0x040019C4 RID: 6596
		public IntPtr CurrentIpAddress;

		// Token: 0x040019C5 RID: 6597
		public Win32_IP_ADDR_STRING IpAddressList;

		// Token: 0x040019C6 RID: 6598
		public Win32_IP_ADDR_STRING GatewayList;

		// Token: 0x040019C7 RID: 6599
		public Win32_IP_ADDR_STRING DhcpServer;

		// Token: 0x040019C8 RID: 6600
		public bool HaveWins;

		// Token: 0x040019C9 RID: 6601
		public Win32_IP_ADDR_STRING PrimaryWinsServer;

		// Token: 0x040019CA RID: 6602
		public Win32_IP_ADDR_STRING SecondaryWinsServer;

		// Token: 0x040019CB RID: 6603
		public long LeaseObtained;

		// Token: 0x040019CC RID: 6604
		public long LeaseExpires;
	}
}
