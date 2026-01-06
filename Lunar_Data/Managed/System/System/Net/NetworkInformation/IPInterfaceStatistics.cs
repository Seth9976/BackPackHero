using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data for an network interface on the local computer.</summary>
	// Token: 0x020004F6 RID: 1270
	public abstract class IPInterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of bytes that were received on the interface.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002936 RID: 10550
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of bytes that were sent on the interface.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002937 RID: 10551
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming packets that were discarded.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002938 RID: 10552
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming packets with errors.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002939 RID: 10553
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming packets with an unknown protocol that were received on the interface.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600293A RID: 10554
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of incoming non-unicast packets received on the interface.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600293B RID: 10555
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600293C RID: 10556
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of outgoing packets that were discarded.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600293D RID: 10557
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of outgoing packets with errors.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600293E RID: 10558
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of packets in the output queue.</returns>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600293F RID: 10559
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of unicast packets that were received on the interface.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002940 RID: 10560
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002941 RID: 10561
		public abstract long UnicastPacketsSent { get; }
	}
}
