using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000546 RID: 1350
	internal class IcmpV6MessageTypes
	{
		// Token: 0x0400195F RID: 6495
		public const int DestinationUnreachable = 1;

		// Token: 0x04001960 RID: 6496
		public const int PacketTooBig = 2;

		// Token: 0x04001961 RID: 6497
		public const int TimeExceeded = 3;

		// Token: 0x04001962 RID: 6498
		public const int ParameterProblem = 4;

		// Token: 0x04001963 RID: 6499
		public const int EchoRequest = 128;

		// Token: 0x04001964 RID: 6500
		public const int EchoReply = 129;

		// Token: 0x04001965 RID: 6501
		public const int GroupMembershipQuery = 130;

		// Token: 0x04001966 RID: 6502
		public const int GroupMembershipReport = 131;

		// Token: 0x04001967 RID: 6503
		public const int GroupMembershipReduction = 132;

		// Token: 0x04001968 RID: 6504
		public const int RouterSolicitation = 133;

		// Token: 0x04001969 RID: 6505
		public const int RouterAdvertisement = 134;

		// Token: 0x0400196A RID: 6506
		public const int NeighborSolicitation = 135;

		// Token: 0x0400196B RID: 6507
		public const int NeighborAdvertisement = 136;

		// Token: 0x0400196C RID: 6508
		public const int Redirect = 137;

		// Token: 0x0400196D RID: 6509
		public const int RouterRenumbering = 138;
	}
}
