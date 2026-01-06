using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000529 RID: 1321
	internal static class SystemNetworkInterface
	{
		// Token: 0x06002A7F RID: 10879 RVA: 0x0009AD54 File Offset: 0x00098F54
		public static NetworkInterface[] GetNetworkInterfaces()
		{
			NetworkInterface[] array;
			try
			{
				array = SystemNetworkInterface.nif.GetAllNetworkInterfaces();
			}
			catch
			{
				array = new NetworkInterface[0];
			}
			return array;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0000390E File Offset: 0x00001B0E
		public static bool InternalGetIsNetworkAvailable()
		{
			return true;
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0009AD8C File Offset: 0x00098F8C
		public static int InternalLoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.nif.GetLoopbackInterfaceIndex();
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x0000822E File Offset: 0x0000642E
		public static int InternalIPv6LoopbackInterfaceIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x0009AD98 File Offset: 0x00098F98
		public static IPAddress GetNetMask(IPAddress address)
		{
			return SystemNetworkInterface.nif.GetNetMask(address);
		}

		// Token: 0x040018F7 RID: 6391
		private static readonly NetworkInterfaceFactory nif = NetworkInterfaceFactory.Create();
	}
}
