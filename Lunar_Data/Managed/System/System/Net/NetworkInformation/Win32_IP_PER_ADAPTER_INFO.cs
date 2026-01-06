using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000540 RID: 1344
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_IP_PER_ADAPTER_INFO
	{
		// Token: 0x04001948 RID: 6472
		public uint AutoconfigEnabled;

		// Token: 0x04001949 RID: 6473
		public uint AutoconfigActive;

		// Token: 0x0400194A RID: 6474
		public IntPtr CurrentDnsServer;

		// Token: 0x0400194B RID: 6475
		public Win32_IP_ADDR_STRING DnsServerList;
	}
}
