using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000556 RID: 1366
	internal struct Win32_IP_ADAPTER_DNS_SERVER_ADDRESS
	{
		// Token: 0x040019F3 RID: 6643
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040019F4 RID: 6644
		public IntPtr Next;

		// Token: 0x040019F5 RID: 6645
		public Win32_SOCKET_ADDRESS Address;
	}
}
