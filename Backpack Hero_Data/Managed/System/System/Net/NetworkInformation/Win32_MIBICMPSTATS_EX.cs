using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000549 RID: 1353
	internal struct Win32_MIBICMPSTATS_EX
	{
		// Token: 0x04001972 RID: 6514
		public uint Msgs;

		// Token: 0x04001973 RID: 6515
		public uint Errors;

		// Token: 0x04001974 RID: 6516
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public uint[] Counts;
	}
}
