using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4).</summary>
	// Token: 0x020004FB RID: 1275
	public abstract class IPv4InterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether an interface uses Windows Internet Name Service (WINS).</summary>
		/// <returns>true if the interface uses WINS; otherwise, false.</returns>
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002950 RID: 10576
		public abstract bool UsesWins { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface is configured to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP address.</summary>
		/// <returns>true if the interface is configured to obtain an IP address from a DHCP server; otherwise, false.</returns>
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002951 RID: 10577
		public abstract bool IsDhcpEnabled { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has an automatic private IP addressing (APIPA) address.</summary>
		/// <returns>true if the interface uses an APIPA address; otherwise, false.</returns>
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002952 RID: 10578
		public abstract bool IsAutomaticPrivateAddressingActive { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has automatic private IP addressing (APIPA) enabled.</summary>
		/// <returns>true if the interface uses APIPA; otherwise, false.</returns>
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002953 RID: 10579
		public abstract bool IsAutomaticPrivateAddressingEnabled { get; }

		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 4 (IPv4) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the index of the IPv4 interface.</returns>
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002954 RID: 10580
		public abstract int Index { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface can forward (route) packets.</summary>
		/// <returns>true if this interface routes packets; otherwise false.</returns>
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002955 RID: 10581
		public abstract bool IsForwardingEnabled { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002956 RID: 10582
		public abstract int Mtu { get; }
	}
}
