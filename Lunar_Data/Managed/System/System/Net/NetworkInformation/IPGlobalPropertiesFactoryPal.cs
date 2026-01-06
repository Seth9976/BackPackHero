using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200051F RID: 1311
	internal static class IPGlobalPropertiesFactoryPal
	{
		// Token: 0x06002A3D RID: 10813 RVA: 0x0009A2F0 File Offset: 0x000984F0
		public static IPGlobalProperties Create()
		{
			IPGlobalProperties ipglobalProperties = UnixIPGlobalPropertiesFactoryPal.Create();
			if (ipglobalProperties == null)
			{
				ipglobalProperties = Win32IPGlobalPropertiesFactoryPal.Create();
			}
			if (ipglobalProperties == null)
			{
				throw new NotImplementedException();
			}
			return ipglobalProperties;
		}
	}
}
