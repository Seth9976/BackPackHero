using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000555 RID: 1365
	internal struct Win32_IP_ADAPTER_ANYCAST_ADDRESS
	{
		// Token: 0x040019F0 RID: 6640
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040019F1 RID: 6641
		public IntPtr Next;

		// Token: 0x040019F2 RID: 6642
		public Win32_SOCKET_ADDRESS Address;
	}
}
