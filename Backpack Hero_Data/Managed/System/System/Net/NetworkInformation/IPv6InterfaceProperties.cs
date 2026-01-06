using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x020004FC RID: 1276
	public abstract class IPv6InterfaceProperties
	{
		/// <summary>Gets the index of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the index of the network interface for IPv6 address.</returns>
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002958 RID: 10584
		public abstract int Index { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002959 RID: 10585
		public abstract int Mtu { get; }

		/// <summary>Gets the scope ID of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The scope ID of the network interface associated with an IPv6 address.</returns>
		/// <param name="scopeLevel">The scope level.</param>
		// Token: 0x0600295A RID: 10586 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual long GetScopeId(ScopeLevel scopeLevel)
		{
			throw new NotImplementedException();
		}
	}
}
