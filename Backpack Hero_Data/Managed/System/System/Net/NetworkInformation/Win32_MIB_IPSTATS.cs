using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200053D RID: 1341
	internal struct Win32_MIB_IPSTATS
	{
		// Token: 0x0400192B RID: 6443
		public int Forwarding;

		// Token: 0x0400192C RID: 6444
		public int DefaultTTL;

		// Token: 0x0400192D RID: 6445
		public uint InReceives;

		// Token: 0x0400192E RID: 6446
		public uint InHdrErrors;

		// Token: 0x0400192F RID: 6447
		public uint InAddrErrors;

		// Token: 0x04001930 RID: 6448
		public uint ForwDatagrams;

		// Token: 0x04001931 RID: 6449
		public uint InUnknownProtos;

		// Token: 0x04001932 RID: 6450
		public uint InDiscards;

		// Token: 0x04001933 RID: 6451
		public uint InDelivers;

		// Token: 0x04001934 RID: 6452
		public uint OutRequests;

		// Token: 0x04001935 RID: 6453
		public uint RoutingDiscards;

		// Token: 0x04001936 RID: 6454
		public uint OutDiscards;

		// Token: 0x04001937 RID: 6455
		public uint OutNoRoutes;

		// Token: 0x04001938 RID: 6456
		public uint ReasmTimeout;

		// Token: 0x04001939 RID: 6457
		public uint ReasmReqds;

		// Token: 0x0400193A RID: 6458
		public uint ReasmOks;

		// Token: 0x0400193B RID: 6459
		public uint ReasmFails;

		// Token: 0x0400193C RID: 6460
		public uint FragOks;

		// Token: 0x0400193D RID: 6461
		public uint FragFails;

		// Token: 0x0400193E RID: 6462
		public uint FragCreates;

		// Token: 0x0400193F RID: 6463
		public int NumIf;

		// Token: 0x04001940 RID: 6464
		public int NumAddr;

		// Token: 0x04001941 RID: 6465
		public int NumRoutes;
	}
}
