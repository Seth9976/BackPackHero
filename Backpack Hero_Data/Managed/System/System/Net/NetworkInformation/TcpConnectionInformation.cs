using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the Transmission Control Protocol (TCP) connections on the local computer.</summary>
	// Token: 0x02000516 RID: 1302
	public abstract class TcpConnectionInformation
	{
		/// <summary>Gets the local endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the local computer.</returns>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002A0B RID: 10763
		public abstract IPEndPoint LocalEndPoint { get; }

		/// <summary>Gets the remote endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the remote computer.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002A0C RID: 10764
		public abstract IPEndPoint RemoteEndPoint { get; }

		/// <summary>Gets the state of this Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.TcpState" /> enumeration values.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002A0D RID: 10765
		public abstract TcpState State { get; }
	}
}
