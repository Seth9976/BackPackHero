using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for Internet Protocol version 6 (ICMPv6) statistical data for the local computer.</summary>
	// Token: 0x020004FE RID: 1278
	public abstract class IcmpV6Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages received.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002977 RID: 10615
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002978 RID: 10616
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages received.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002979 RID: 10617
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages sent.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600297A RID: 10618
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages received.</returns>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600297B RID: 10619
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages sent.</returns>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600297C RID: 10620
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages received.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600297D RID: 10621
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages sent.</returns>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600297E RID: 10622
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages received.</returns>
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600297F RID: 10623
		public abstract long MembershipQueriesReceived { get; }

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages sent.</returns>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002980 RID: 10624
		public abstract long MembershipQueriesSent { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages received.</returns>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002981 RID: 10625
		public abstract long MembershipReductionsReceived { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages sent.</returns>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002982 RID: 10626
		public abstract long MembershipReductionsSent { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages received.</returns>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002983 RID: 10627
		public abstract long MembershipReportsReceived { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages sent.</returns>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002984 RID: 10628
		public abstract long MembershipReportsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages received.</returns>
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002985 RID: 10629
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages sent.</returns>
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002986 RID: 10630
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Neighbor Advertisement messages received.</returns>
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002987 RID: 10631
		public abstract long NeighborAdvertisementsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Advertisement messages sent.</returns>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002988 RID: 10632
		public abstract long NeighborAdvertisementsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages received.</returns>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002989 RID: 10633
		public abstract long NeighborSolicitsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages sent.</returns>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600298A RID: 10634
		public abstract long NeighborSolicitsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages received.</returns>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600298B RID: 10635
		public abstract long PacketTooBigMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages sent.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600298C RID: 10636
		public abstract long PacketTooBigMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages received.</returns>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x0600298D RID: 10637
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages sent.</returns>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600298E RID: 10638
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages received.</returns>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600298F RID: 10639
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages sent.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002990 RID: 10640
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages received.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002991 RID: 10641
		public abstract long RouterAdvertisementsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages sent.</returns>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002992 RID: 10642
		public abstract long RouterAdvertisementsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages received.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002993 RID: 10643
		public abstract long RouterSolicitsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages sent.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002994 RID: 10644
		public abstract long RouterSolicitsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages received.</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002995 RID: 10645
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages sent.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002996 RID: 10646
		public abstract long TimeExceededMessagesSent { get; }
	}
}
