using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200054B RID: 1355
	internal sealed class Win32NetworkInterface2 : NetworkInterface
	{
		// Token: 0x06002B7F RID: 11135
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersInfo(IntPtr info, ref int size);

		// Token: 0x06002B80 RID: 11136
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetIfEntry(ref Win32_MIB_IFROW row);

		// Token: 0x06002B81 RID: 11137 RVA: 0x0009CE98 File Offset: 0x0009B098
		private static Win32_IP_ADAPTER_INFO[] GetAdaptersInfo()
		{
			int num = 0;
			Win32NetworkInterface2.GetAdaptersInfo(IntPtr.Zero, ref num);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			int adaptersInfo = Win32NetworkInterface2.GetAdaptersInfo(intPtr, ref num);
			if (adaptersInfo != 0)
			{
				throw new NetworkInformationException(adaptersInfo);
			}
			List<Win32_IP_ADAPTER_INFO> list = new List<Win32_IP_ADAPTER_INFO>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_INFO win32_IP_ADAPTER_INFO = Marshal.PtrToStructure<Win32_IP_ADAPTER_INFO>(intPtr2);
				list.Add(win32_IP_ADAPTER_INFO);
				intPtr2 = win32_IP_ADAPTER_INFO.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0009CF04 File Offset: 0x0009B104
		internal Win32NetworkInterface2(Win32_IP_ADAPTER_ADDRESSES addr)
		{
			this.addr = addr;
			this.mib4 = default(Win32_MIB_IFROW);
			this.mib4.Index = addr.Alignment.IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib4) != 0)
			{
				this.mib4.Index = -1;
			}
			this.mib6 = default(Win32_MIB_IFROW);
			this.mib6.Index = addr.Ipv6IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib6) != 0)
			{
				this.mib6.Index = -1;
			}
			this.ip4stats = new Win32IPv4InterfaceStatistics(this.mib4);
			this.ip_if_props = new Win32IPInterfaceProperties2(addr, this.mib4, this.mib6);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0009CFB8 File Offset: 0x0009B1B8
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.ip_if_props;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0009CFC0 File Offset: 0x0009B1C0
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return this.ip4stats;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0009CFC8 File Offset: 0x0009B1C8
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addr.PhysicalAddressLength];
			Array.Copy(this.addr.PhysicalAddress, 0, array, 0, array.Length);
			return new PhysicalAddress(array);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0009D002 File Offset: 0x0009B202
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			if (networkInterfaceComponent != NetworkInterfaceComponent.IPv4)
			{
				return networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && this.mib6.Index >= 0;
			}
			return this.mib4.Index >= 0;
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x0009D032 File Offset: 0x0009B232
		public override string Description
		{
			get
			{
				return this.addr.Description;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x0009D03F File Offset: 0x0009B23F
		public override string Id
		{
			get
			{
				return this.addr.AdapterName;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x0009D04C File Offset: 0x0009B24C
		public override bool IsReceiveOnly
		{
			get
			{
				return this.addr.IsReceiveOnly;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x0009D059 File Offset: 0x0009B259
		public override string Name
		{
			get
			{
				return this.addr.FriendlyName;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x0009D066 File Offset: 0x0009B266
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.addr.IfType;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x0009D073 File Offset: 0x0009B273
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.addr.OperStatus;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x0009D080 File Offset: 0x0009B280
		public override long Speed
		{
			get
			{
				return (long)((ulong)((this.mib6.Index >= 0) ? this.mib6.Speed : this.mib4.Speed));
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x0009D0A9 File Offset: 0x0009B2A9
		public override bool SupportsMulticast
		{
			get
			{
				return !this.addr.NoMulticast;
			}
		}

		// Token: 0x04001976 RID: 6518
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04001977 RID: 6519
		private Win32_MIB_IFROW mib4;

		// Token: 0x04001978 RID: 6520
		private Win32_MIB_IFROW mib6;

		// Token: 0x04001979 RID: 6521
		private Win32IPv4InterfaceStatistics ip4stats;

		// Token: 0x0400197A RID: 6522
		private IPInterfaceProperties ip_if_props;
	}
}
