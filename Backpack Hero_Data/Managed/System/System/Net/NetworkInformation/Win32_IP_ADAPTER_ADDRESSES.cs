using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000550 RID: 1360
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct Win32_IP_ADAPTER_ADDRESSES
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x0009D133 File Offset: 0x0009B333
		public bool DdnsEnabled
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x0009D140 File Offset: 0x0009B340
		public bool DhcpEnabled
		{
			get
			{
				return (this.Flags & 4U) > 0U;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x0009D14D File Offset: 0x0009B34D
		public bool IsReceiveOnly
		{
			get
			{
				return (this.Flags & 8U) > 0U;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x0009D15A File Offset: 0x0009B35A
		public bool NoMulticast
		{
			get
			{
				return (this.Flags & 16U) > 0U;
			}
		}

		// Token: 0x0400198C RID: 6540
		public AlignmentUnion Alignment;

		// Token: 0x0400198D RID: 6541
		public IntPtr Next;

		// Token: 0x0400198E RID: 6542
		[MarshalAs(UnmanagedType.LPStr)]
		public string AdapterName;

		// Token: 0x0400198F RID: 6543
		public IntPtr FirstUnicastAddress;

		// Token: 0x04001990 RID: 6544
		public IntPtr FirstAnycastAddress;

		// Token: 0x04001991 RID: 6545
		public IntPtr FirstMulticastAddress;

		// Token: 0x04001992 RID: 6546
		public IntPtr FirstDnsServerAddress;

		// Token: 0x04001993 RID: 6547
		public string DnsSuffix;

		// Token: 0x04001994 RID: 6548
		public string Description;

		// Token: 0x04001995 RID: 6549
		public string FriendlyName;

		// Token: 0x04001996 RID: 6550
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysicalAddress;

		// Token: 0x04001997 RID: 6551
		public uint PhysicalAddressLength;

		// Token: 0x04001998 RID: 6552
		public uint Flags;

		// Token: 0x04001999 RID: 6553
		public uint Mtu;

		// Token: 0x0400199A RID: 6554
		public NetworkInterfaceType IfType;

		// Token: 0x0400199B RID: 6555
		public OperationalStatus OperStatus;

		// Token: 0x0400199C RID: 6556
		public int Ipv6IfIndex;

		// Token: 0x0400199D RID: 6557
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] ZoneIndices;

		// Token: 0x0400199E RID: 6558
		public IntPtr FirstPrefix;

		// Token: 0x0400199F RID: 6559
		public ulong TransmitLinkSpeed;

		// Token: 0x040019A0 RID: 6560
		public ulong ReceiveLinkSpeed;

		// Token: 0x040019A1 RID: 6561
		public IntPtr FirstWinsServerAddress;

		// Token: 0x040019A2 RID: 6562
		public IntPtr FirstGatewayAddress;

		// Token: 0x040019A3 RID: 6563
		public uint Ipv4Metric;

		// Token: 0x040019A4 RID: 6564
		public uint Ipv6Metric;

		// Token: 0x040019A5 RID: 6565
		public ulong Luid;

		// Token: 0x040019A6 RID: 6566
		public Win32_SOCKET_ADDRESS Dhcpv4Server;

		// Token: 0x040019A7 RID: 6567
		public uint CompartmentId;

		// Token: 0x040019A8 RID: 6568
		public ulong NetworkGuid;

		// Token: 0x040019A9 RID: 6569
		public int ConnectionType;

		// Token: 0x040019AA RID: 6570
		public int TunnelType;

		// Token: 0x040019AB RID: 6571
		public Win32_SOCKET_ADDRESS Dhcpv6Server;

		// Token: 0x040019AC RID: 6572
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)]
		public byte[] Dhcpv6ClientDuid;

		// Token: 0x040019AD RID: 6573
		public ulong Dhcpv6ClientDuidLength;

		// Token: 0x040019AE RID: 6574
		public ulong Dhcpv6Iaid;

		// Token: 0x040019AF RID: 6575
		public IntPtr FirstDnsSuffix;

		// Token: 0x040019B0 RID: 6576
		public const int GAA_FLAG_INCLUDE_WINS_INFO = 64;

		// Token: 0x040019B1 RID: 6577
		public const int GAA_FLAG_INCLUDE_GATEWAYS = 128;

		// Token: 0x040019B2 RID: 6578
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x040019B3 RID: 6579
		private const int MAX_DHCPV6_DUID_LENGTH = 130;

		// Token: 0x040019B4 RID: 6580
		private const int IP_ADAPTER_DDNS_ENABLED = 1;

		// Token: 0x040019B5 RID: 6581
		private const int IP_ADAPTER_DHCP_ENABLED = 4;

		// Token: 0x040019B6 RID: 6582
		private const int IP_ADAPTER_RECEIVE_ONLY = 8;

		// Token: 0x040019B7 RID: 6583
		private const int IP_ADAPTER_NO_MULTICAST = 16;
	}
}
