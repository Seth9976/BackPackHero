using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200054D RID: 1357
	internal class Win32NetworkInterface
	{
		// Token: 0x06002B90 RID: 11152
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetNetworkParams(IntPtr ptr, ref int size);

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x0009D0EC File Offset: 0x0009B2EC
		public static Win32_FIXED_INFO FixedInfo
		{
			get
			{
				if (!Win32NetworkInterface.initialized)
				{
					int num = 0;
					Win32NetworkInterface.GetNetworkParams(IntPtr.Zero, ref num);
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					Win32NetworkInterface.GetNetworkParams(intPtr, ref num);
					Win32NetworkInterface.fixedInfo = Marshal.PtrToStructure<Win32_FIXED_INFO>(intPtr);
					Win32NetworkInterface.initialized = true;
				}
				return Win32NetworkInterface.fixedInfo;
			}
		}

		// Token: 0x0400197B RID: 6523
		private static Win32_FIXED_INFO fixedInfo;

		// Token: 0x0400197C RID: 6524
		private static bool initialized;
	}
}
