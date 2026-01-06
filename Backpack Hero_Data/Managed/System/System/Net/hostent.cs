using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200040C RID: 1036
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct hostent
	{
		// Token: 0x040012EE RID: 4846
		public IntPtr h_name;

		// Token: 0x040012EF RID: 4847
		public IntPtr h_aliases;

		// Token: 0x040012F0 RID: 4848
		public short h_addrtype;

		// Token: 0x040012F1 RID: 4849
		public short h_length;

		// Token: 0x040012F2 RID: 4850
		public IntPtr h_addr_list;
	}
}
