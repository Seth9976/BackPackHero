using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000545 RID: 1349
	internal struct Win32_MIBICMPSTATS
	{
		// Token: 0x04001952 RID: 6482
		public uint Msgs;

		// Token: 0x04001953 RID: 6483
		public uint Errors;

		// Token: 0x04001954 RID: 6484
		public uint DestUnreachs;

		// Token: 0x04001955 RID: 6485
		public uint TimeExcds;

		// Token: 0x04001956 RID: 6486
		public uint ParmProbs;

		// Token: 0x04001957 RID: 6487
		public uint SrcQuenchs;

		// Token: 0x04001958 RID: 6488
		public uint Redirects;

		// Token: 0x04001959 RID: 6489
		public uint Echos;

		// Token: 0x0400195A RID: 6490
		public uint EchoReps;

		// Token: 0x0400195B RID: 6491
		public uint Timestamps;

		// Token: 0x0400195C RID: 6492
		public uint TimestampReps;

		// Token: 0x0400195D RID: 6493
		public uint AddrMasks;

		// Token: 0x0400195E RID: 6494
		public uint AddrMaskReps;
	}
}
