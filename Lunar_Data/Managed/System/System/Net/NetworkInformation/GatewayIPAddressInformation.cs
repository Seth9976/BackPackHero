using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the IP address of the network gateway. This class cannot be instantiated.</summary>
	// Token: 0x020004EE RID: 1262
	public abstract class GatewayIPAddressInformation
	{
		/// <summary>Get the IP address of the gateway.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IP address of the gateway.</returns>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060028D1 RID: 10449
		public abstract IPAddress Address { get; }
	}
}
