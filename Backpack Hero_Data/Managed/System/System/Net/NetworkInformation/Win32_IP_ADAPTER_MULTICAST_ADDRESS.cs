using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000557 RID: 1367
	internal struct Win32_IP_ADAPTER_MULTICAST_ADDRESS
	{
		// Token: 0x040019F6 RID: 6646
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040019F7 RID: 6647
		public IntPtr Next;

		// Token: 0x040019F8 RID: 6648
		public Win32_SOCKET_ADDRESS Address;
	}
}
