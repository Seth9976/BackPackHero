using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200052A RID: 1322
	internal abstract class NetworkInterfaceFactory
	{
		// Token: 0x06002A85 RID: 10885
		public abstract NetworkInterface[] GetAllNetworkInterfaces();

		// Token: 0x06002A86 RID: 10886
		public abstract int GetLoopbackInterfaceIndex();

		// Token: 0x06002A87 RID: 10887
		public abstract IPAddress GetNetMask(IPAddress address);

		// Token: 0x06002A88 RID: 10888 RVA: 0x0009ADB1 File Offset: 0x00098FB1
		public static NetworkInterfaceFactory Create()
		{
			return NetworkInterfaceFactoryPal.Create();
		}
	}
}
