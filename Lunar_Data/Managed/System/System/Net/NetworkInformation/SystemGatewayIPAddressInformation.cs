using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000512 RID: 1298
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x060029F4 RID: 10740 RVA: 0x0009A0FC File Offset: 0x000982FC
		internal SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x0009A10B File Offset: 0x0009830B
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0009A114 File Offset: 0x00098314
		internal static GatewayIPAddressInformationCollection ToGatewayIpAddressInformationCollection(IPAddressCollection addresses)
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			foreach (IPAddress ipaddress in addresses)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(ipaddress));
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x04001895 RID: 6293
		private IPAddress address;
	}
}
