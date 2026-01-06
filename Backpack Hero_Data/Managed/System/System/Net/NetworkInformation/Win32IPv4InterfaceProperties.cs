using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200053F RID: 1343
	internal sealed class Win32IPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x06002B20 RID: 11040
		[DllImport("iphlpapi.dll")]
		private static extern int GetPerAdapterInfo(int IfIndex, Win32_IP_PER_ADAPTER_INFO pPerAdapterInfo, ref int pOutBufLen);

		// Token: 0x06002B21 RID: 11041 RVA: 0x0009C7E0 File Offset: 0x0009A9E0
		public Win32IPv4InterfaceProperties(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib)
		{
			this.addr = addr;
			this.mib = mib;
			int num = 0;
			Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, null, ref num);
			this.painfo = new Win32_IP_PER_ADAPTER_INFO();
			int perAdapterInfo = Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, this.painfo, ref num);
			if (perAdapterInfo != 0)
			{
				throw new NetworkInformationException(perAdapterInfo);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x0009C83B File Offset: 0x0009AA3B
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0009C848 File Offset: 0x0009AA48
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.painfo.AutoconfigActive > 0U;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0009C858 File Offset: 0x0009AA58
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.painfo.AutoconfigEnabled > 0U;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x0009C868 File Offset: 0x0009AA68
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.addr.DhcpEnabled;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x0009C875 File Offset: 0x0009AA75
		public override bool IsForwardingEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableRouting > 0U;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x0009C884 File Offset: 0x0009AA84
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x0009C891 File Offset: 0x0009AA91
		public override bool UsesWins
		{
			get
			{
				return this.addr.FirstWinsServerAddress != IntPtr.Zero;
			}
		}

		// Token: 0x04001945 RID: 6469
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04001946 RID: 6470
		private Win32_IP_PER_ADAPTER_INFO painfo;

		// Token: 0x04001947 RID: 6471
		private Win32_MIB_IFROW mib;
	}
}
