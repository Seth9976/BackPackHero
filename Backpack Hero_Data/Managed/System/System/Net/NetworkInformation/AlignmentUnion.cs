using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200054F RID: 1359
	[StructLayout(LayoutKind.Explicit)]
	internal struct AlignmentUnion
	{
		// Token: 0x04001989 RID: 6537
		[FieldOffset(0)]
		public ulong Alignment;

		// Token: 0x0400198A RID: 6538
		[FieldOffset(0)]
		public int Length;

		// Token: 0x0400198B RID: 6539
		[FieldOffset(4)]
		public int IfIndex;
	}
}
