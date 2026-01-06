using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface address.</summary>
	// Token: 0x020004F1 RID: 1265
	public abstract class IPAddressInformation
	{
		/// <summary>Gets the Internet Protocol (IP) address.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance that contains the IP address of an interface.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060028EB RID: 10475
		public abstract IPAddress Address { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is valid to appear in a Domain Name System (DNS) server database.</summary>
		/// <returns>true if the address can appear in a DNS database; otherwise, false.</returns>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060028EC RID: 10476
		public abstract bool IsDnsEligible { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is transient (a cluster address).</summary>
		/// <returns>true if the address is transient; otherwise, false.</returns>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060028ED RID: 10477
		public abstract bool IsTransient { get; }
	}
}
