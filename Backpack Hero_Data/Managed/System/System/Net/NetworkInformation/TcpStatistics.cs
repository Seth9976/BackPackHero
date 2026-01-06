using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Transmission Control Protocol (TCP) statistical data.</summary>
	// Token: 0x02000518 RID: 1304
	public abstract class TcpStatistics
	{
		/// <summary>Gets the number of accepted Transmission Control Protocol (TCP) connection requests.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connection requests accepted.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002A0F RID: 10767
		public abstract long ConnectionsAccepted { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) connection requests made by clients.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections initiated by clients.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002A10 RID: 10768
		public abstract long ConnectionsInitiated { get; }

		/// <summary>Specifies the total number of Transmission Control Protocol (TCP) connections established.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of connections established.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002A11 RID: 10769
		public abstract long CumulativeConnections { get; }

		/// <summary>Gets the number of current Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of current TCP connections.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002A12 RID: 10770
		public abstract long CurrentConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) errors received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP errors received.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002A13 RID: 10771
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of failed Transmission Control Protocol (TCP) connection attempts.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of failed TCP connection attempts.</returns>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002A14 RID: 10772
		public abstract long FailedConnectionAttempts { get; }

		/// <summary>Gets the maximum number of supported Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections that can be supported.</returns>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002A15 RID: 10773
		public abstract long MaximumConnections { get; }

		/// <summary>Gets the maximum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002A16 RID: 10774
		public abstract long MaximumTransmissionTimeout { get; }

		/// <summary>Gets the minimum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the minimum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002A17 RID: 10775
		public abstract long MinimumTransmissionTimeout { get; }

		/// <summary>Gets the number of RST packets received by Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of reset TCP connections.</returns>
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002A18 RID: 10776
		public abstract long ResetConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments received.</returns>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002A19 RID: 10777
		public abstract long SegmentsReceived { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments re-sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments retransmitted.</returns>
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002A1A RID: 10778
		public abstract long SegmentsResent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002A1B RID: 10779
		public abstract long SegmentsSent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent with the reset flag set.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent with the reset flag set.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002A1C RID: 10780
		public abstract long ResetsSent { get; }
	}
}
