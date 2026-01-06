using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200053E RID: 1342
	internal class Win32IPInterfaceProperties2 : IPInterfaceProperties
	{
		// Token: 0x06002B10 RID: 11024 RVA: 0x0009C4C7 File Offset: 0x0009A6C7
		public Win32IPInterfaceProperties2(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib4, Win32_MIB_IFROW mib6)
		{
			this.addr = addr;
			this.mib4 = mib4;
			this.mib6 = mib6;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0009C4E4 File Offset: 0x0009A6E4
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			return new Win32IPv4InterfaceProperties(this.addr, this.mib4);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x0009C4F7 File Offset: 0x0009A6F7
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			return new Win32IPv6InterfaceProperties(this.mib6);
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x0009C504 File Offset: 0x0009A704
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return Win32IPInterfaceProperties2.Win32FromAnycast(this.addr.FirstAnycastAddress);
			}
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0009C518 File Offset: 0x0009A718
		private static IPAddressInformationCollection Win32FromAnycast(IntPtr ptr)
		{
			IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsTransient));
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			return ipaddressInformationCollection;
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x0009C58C File Offset: 0x0009A78C
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				IPAddressCollection ipaddressCollection;
				try
				{
					ipaddressCollection = Win32IPAddressCollection.FromSocketAddress(this.addr.Dhcpv4Server);
				}
				catch (IndexOutOfRangeException)
				{
					ipaddressCollection = Win32IPAddressCollection.Empty;
				}
				return ipaddressCollection;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0009C5C8 File Offset: 0x0009A7C8
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return Win32IPAddressCollection.FromDnsServer(this.addr.FirstDnsServerAddress);
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x0009C5DA File Offset: 0x0009A7DA
		public override string DnsSuffix
		{
			get
			{
				return this.addr.DnsSuffix;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0009C5E8 File Offset: 0x0009A7E8
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
				try
				{
					IntPtr intPtr = this.addr.FirstGatewayAddress;
					while (intPtr != IntPtr.Zero)
					{
						Win32_IP_ADAPTER_GATEWAY_ADDRESS win32_IP_ADAPTER_GATEWAY_ADDRESS = (Win32_IP_ADAPTER_GATEWAY_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_GATEWAY_ADDRESS));
						gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(win32_IP_ADAPTER_GATEWAY_ADDRESS.Address.GetIPAddress()));
						intPtr = win32_IP_ADAPTER_GATEWAY_ADDRESS.Next;
					}
				}
				catch (IndexOutOfRangeException)
				{
				}
				return gatewayIPAddressInformationCollection;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0009C660 File Offset: 0x0009A860
		public override bool IsDnsEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableDns > 0U;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x0009C670 File Offset: 0x0009A870
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.addr.DdnsEnabled;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0009C68B File Offset: 0x0009A88B
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return Win32IPInterfaceProperties2.Win32FromMulticast(this.addr.FirstMulticastAddress);
			}
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0009C6A0 File Offset: 0x0009A8A0
		private static MulticastIPAddressInformationCollection Win32FromMulticast(IntPtr ptr)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_MULTICAST_ADDRESS win32_IP_ADAPTER_MULTICAST_ADDRESS = (Win32_IP_ADAPTER_MULTICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_MULTICAST_ADDRESS));
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(new SystemIPAddressInformation(win32_IP_ADAPTER_MULTICAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsTransient)));
				intPtr = win32_IP_ADAPTER_MULTICAST_ADDRESS.Next;
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x0009C718 File Offset: 0x0009A918
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				UnicastIPAddressInformationCollection unicastIPAddressInformationCollection;
				try
				{
					unicastIPAddressInformationCollection = Win32IPInterfaceProperties2.Win32FromUnicast(this.addr.FirstUnicastAddress);
				}
				catch (IndexOutOfRangeException)
				{
					unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
				}
				return unicastIPAddressInformationCollection;
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0009C754 File Offset: 0x0009A954
		private static UnicastIPAddressInformationCollection Win32FromUnicast(IntPtr ptr)
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_UNICAST_ADDRESS win32_IP_ADAPTER_UNICAST_ADDRESS = (Win32_IP_ADAPTER_UNICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_UNICAST_ADDRESS));
				unicastIPAddressInformationCollection.InternalAdd(new Win32UnicastIPAddressInformation(win32_IP_ADAPTER_UNICAST_ADDRESS));
				intPtr = win32_IP_ADAPTER_UNICAST_ADDRESS.Next;
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0009C7A4 File Offset: 0x0009A9A4
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				IPAddressCollection ipaddressCollection;
				try
				{
					ipaddressCollection = Win32IPAddressCollection.FromWinsServer(this.addr.FirstWinsServerAddress);
				}
				catch (IndexOutOfRangeException)
				{
					ipaddressCollection = Win32IPAddressCollection.Empty;
				}
				return ipaddressCollection;
			}
		}

		// Token: 0x04001942 RID: 6466
		private readonly Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04001943 RID: 6467
		private readonly Win32_MIB_IFROW mib4;

		// Token: 0x04001944 RID: 6468
		private readonly Win32_MIB_IFROW mib6;
	}
}
