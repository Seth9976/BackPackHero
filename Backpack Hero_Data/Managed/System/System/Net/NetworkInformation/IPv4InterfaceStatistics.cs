using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides statistical data for a network interface on the local computer.</summary>
	// Token: 0x020004F7 RID: 1271
	public abstract class IPv4InterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were received on the interface.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002943 RID: 10563
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were transmitted on the interface.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002944 RID: 10564
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded incoming packets.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002945 RID: 10565
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with errors.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002946 RID: 10566
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with an unknown protocol.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002947 RID: 10567
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were received on the interface.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002948 RID: 10568
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002949 RID: 10569
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded outgoing packets.</returns>
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600294A RID: 10570
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets with errors.</returns>
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600294B RID: 10571
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets in the output queue.</returns>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600294C RID: 10572
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were received on the interface.</returns>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600294D RID: 10573
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600294E RID: 10574
		public abstract long UnicastPacketsSent { get; }
	}
}
