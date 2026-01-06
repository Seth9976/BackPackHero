using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000558 RID: 1368
	internal struct Win32_IP_ADAPTER_GATEWAY_ADDRESS
	{
		// Token: 0x040019F9 RID: 6649
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040019FA RID: 6650
		public IntPtr Next;

		// Token: 0x040019FB RID: 6651
		public Win32_SOCKET_ADDRESS Address;
	}
}
