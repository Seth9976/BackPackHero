using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000559 RID: 1369
	internal struct Win32_IP_ADAPTER_WINS_SERVER_ADDRESS
	{
		// Token: 0x040019FC RID: 6652
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x040019FD RID: 6653
		public IntPtr Next;

		// Token: 0x040019FE RID: 6654
		public Win32_SOCKET_ADDRESS Address;
	}
}
