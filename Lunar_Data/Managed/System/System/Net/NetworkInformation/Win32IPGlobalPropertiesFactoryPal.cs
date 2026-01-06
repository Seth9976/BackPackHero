using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200053B RID: 1339
	internal static class Win32IPGlobalPropertiesFactoryPal
	{
		// Token: 0x06002AF8 RID: 11000 RVA: 0x0009C37F File Offset: 0x0009A57F
		public static IPGlobalProperties Create()
		{
			return new Win32IPGlobalProperties();
		}
	}
}
