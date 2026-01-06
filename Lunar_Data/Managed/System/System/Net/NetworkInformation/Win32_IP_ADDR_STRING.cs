using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000553 RID: 1363
	internal struct Win32_IP_ADDR_STRING
	{
		// Token: 0x040019E8 RID: 6632
		public IntPtr Next;

		// Token: 0x040019E9 RID: 6633
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpAddress;

		// Token: 0x040019EA RID: 6634
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpMask;

		// Token: 0x040019EB RID: 6635
		public uint Context;
	}
}
