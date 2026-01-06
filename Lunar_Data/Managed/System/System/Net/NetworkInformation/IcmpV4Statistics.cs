using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for IPv4 (ICMPv4) statistical data for the local computer.</summary>
	// Token: 0x020004FD RID: 1277
	public abstract class IcmpV4Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were received.</returns>
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600295C RID: 10588
		public abstract long AddressMaskRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were sent.</returns>
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600295D RID: 10589
		public abstract long AddressMaskRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were received.</returns>
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600295E RID: 10590
		public abstract long AddressMaskRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were sent.</returns>
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600295F RID: 10591
		public abstract long AddressMaskRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages that were received.</returns>
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002960 RID: 10592
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002961 RID: 10593
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were received.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002962 RID: 10594
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were sent.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002963 RID: 10595
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were received.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002964 RID: 10596
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were sent.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002965 RID: 10597
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages that were received.</returns>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002966 RID: 10598
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP error messages that were sent.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002967 RID: 10599
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were received.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002968 RID: 10600
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were sent.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002969 RID: 10601
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were received.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x0600296A RID: 10602
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were sent.</returns>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600296B RID: 10603
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were received.</returns>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600296C RID: 10604
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were sent.</returns>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600296D RID: 10605
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were received.</returns>
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600296E RID: 10606
		public abstract long SourceQuenchesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were sent.</returns>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600296F RID: 10607
		public abstract long SourceQuenchesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were received.</returns>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002970 RID: 10608
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were sent.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002971 RID: 10609
		public abstract long TimeExceededMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were received.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002972 RID: 10610
		public abstract long TimestampRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were sent.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002973 RID: 10611
		public abstract long TimestampRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were received.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002974 RID: 10612
		public abstract long TimestampRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were sent.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002975 RID: 10613
		public abstract long TimestampRequestsSent { get; }
	}
}
