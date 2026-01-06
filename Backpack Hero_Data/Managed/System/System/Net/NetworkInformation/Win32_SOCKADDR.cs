using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200055B RID: 1371
	internal struct Win32_SOCKADDR
	{
		// Token: 0x04001A09 RID: 6665
		public ushort AddressFamily;

		// Token: 0x04001A0A RID: 6666
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		public byte[] AddressData;
	}
}
