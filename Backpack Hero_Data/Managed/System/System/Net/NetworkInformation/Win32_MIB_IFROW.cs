using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000552 RID: 1362
	internal struct Win32_MIB_IFROW
	{
		// Token: 0x040019CD RID: 6605
		private const int MAX_INTERFACE_NAME_LEN = 256;

		// Token: 0x040019CE RID: 6606
		private const int MAXLEN_PHYSADDR = 8;

		// Token: 0x040019CF RID: 6607
		private const int MAXLEN_IFDESCR = 256;

		// Token: 0x040019D0 RID: 6608
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] Name;

		// Token: 0x040019D1 RID: 6609
		public int Index;

		// Token: 0x040019D2 RID: 6610
		public NetworkInterfaceType Type;

		// Token: 0x040019D3 RID: 6611
		public int Mtu;

		// Token: 0x040019D4 RID: 6612
		public uint Speed;

		// Token: 0x040019D5 RID: 6613
		public int PhysAddrLen;

		// Token: 0x040019D6 RID: 6614
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysAddr;

		// Token: 0x040019D7 RID: 6615
		public uint AdminStatus;

		// Token: 0x040019D8 RID: 6616
		public uint OperStatus;

		// Token: 0x040019D9 RID: 6617
		public uint LastChange;

		// Token: 0x040019DA RID: 6618
		public int InOctets;

		// Token: 0x040019DB RID: 6619
		public int InUcastPkts;

		// Token: 0x040019DC RID: 6620
		public int InNUcastPkts;

		// Token: 0x040019DD RID: 6621
		public int InDiscards;

		// Token: 0x040019DE RID: 6622
		public int InErrors;

		// Token: 0x040019DF RID: 6623
		public int InUnknownProtos;

		// Token: 0x040019E0 RID: 6624
		public int OutOctets;

		// Token: 0x040019E1 RID: 6625
		public int OutUcastPkts;

		// Token: 0x040019E2 RID: 6626
		public int OutNUcastPkts;

		// Token: 0x040019E3 RID: 6627
		public int OutDiscards;

		// Token: 0x040019E4 RID: 6628
		public int OutErrors;

		// Token: 0x040019E5 RID: 6629
		public int OutQLen;

		// Token: 0x040019E6 RID: 6630
		public int DescrLen;

		// Token: 0x040019E7 RID: 6631
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] Descr;
	}
}
