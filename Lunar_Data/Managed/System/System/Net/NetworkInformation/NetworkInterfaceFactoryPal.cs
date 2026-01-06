using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200052B RID: 1323
	internal static class NetworkInterfaceFactoryPal
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x0009ADB8 File Offset: 0x00098FB8
		public static NetworkInterfaceFactory Create()
		{
			NetworkInterfaceFactory networkInterfaceFactory = UnixNetworkInterfaceFactoryPal.Create();
			if (networkInterfaceFactory == null)
			{
				networkInterfaceFactory = Win32NetworkInterfaceFactoryPal.Create();
			}
			if (networkInterfaceFactory == null)
			{
				throw new NotImplementedException();
			}
			return networkInterfaceFactory;
		}
	}
}
