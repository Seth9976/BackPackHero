using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's unicast address.</summary>
	// Token: 0x0200051A RID: 1306
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002A24 RID: 10788
		public abstract long AddressPreferredLifetime { get; }

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002A25 RID: 10789
		public abstract long AddressValidLifetime { get; }

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002A26 RID: 10790
		public abstract long DhcpLeaseLifetime { get; }

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002A27 RID: 10791
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002A28 RID: 10792
		public abstract PrefixOrigin PrefixOrigin { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002A29 RID: 10793
		public abstract SuffixOrigin SuffixOrigin { get; }

		/// <summary>Gets the IPv4 mask.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IPv4 mask.</returns>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002A2A RID: 10794
		public abstract IPAddress IPv4Mask { get; }

		/// <summary>Gets the length, in bits, of the prefix or network part of the IP address.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.the length, in bits, of the prefix or network part of the IP address.</returns>
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual int PrefixLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
