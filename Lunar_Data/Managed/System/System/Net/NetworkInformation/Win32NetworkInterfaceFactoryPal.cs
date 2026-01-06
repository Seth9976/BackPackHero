using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200054C RID: 1356
	internal static class Win32NetworkInterfaceFactoryPal
	{
		// Token: 0x06002B8F RID: 11151 RVA: 0x0009D0BC File Offset: 0x0009B2BC
		public static NetworkInterfaceFactory Create()
		{
			Version version = new Version(5, 1);
			if (Environment.OSVersion.Version >= version)
			{
				return new Win32NetworkInterfaceAPI();
			}
			return null;
		}
	}
}
