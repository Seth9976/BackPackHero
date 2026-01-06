using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides User Datagram Protocol (UDP) statistical data.</summary>
	// Token: 0x02000519 RID: 1305
	public abstract class UdpStatistics
	{
		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were delivered to UDP users.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002A1E RID: 10782
		public abstract long DatagramsReceived { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were sent.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002A1F RID: 10783
		public abstract long DatagramsSent { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of port errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that were discarded because there was no listening application at the destination port.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002A20 RID: 10784
		public abstract long IncomingDatagramsDiscarded { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of errors other than bad port information.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that could not be delivered for reasons other than the lack of an application at the destination port.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002A21 RID: 10785
		public abstract long IncomingDatagramsWithErrors { get; }

		/// <summary>Gets the number of local endpoints that are listening for User Datagram Protocol (UDP) datagrams.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of sockets that are listening for UDP datagrams.</returns>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002A22 RID: 10786
		public abstract int UdpListeners { get; }
	}
}
