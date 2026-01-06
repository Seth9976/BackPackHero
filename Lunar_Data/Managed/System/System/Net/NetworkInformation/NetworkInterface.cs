using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides configuration and statistical information for a network interface.</summary>
	// Token: 0x02000509 RID: 1289
	public abstract class NetworkInterface
	{
		/// <summary>Returns objects that describe the network interfaces on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array that contains objects that describe the available network interfaces, or an empty array if no interfaces are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Windows system function call failed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Net.NetworkInformation.NetworkInformationPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Access="Read" />
		/// </PermissionSet>
		// Token: 0x060029CB RID: 10699 RVA: 0x00099BD7 File Offset: 0x00097DD7
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			return SystemNetworkInterface.GetNetworkInterfaces();
		}

		/// <summary>Indicates whether any network connection is available.</summary>
		/// <returns>true if a network connection is available; otherwise, false.</returns>
		// Token: 0x060029CC RID: 10700 RVA: 0x00099BDE File Offset: 0x00097DDE
		public static bool GetIsNetworkAvailable()
		{
			return SystemNetworkInterface.InternalGetIsNetworkAvailable();
		}

		/// <summary>Gets the index of the IPv4 loopback interface.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the index for the IPv4 loopback interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">This property is not valid on computers running only Ipv6.</exception>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x00099BE5 File Offset: 0x00097DE5
		public static int LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.InternalLoopbackInterfaceIndex;
			}
		}

		/// <summary>Gets the index of the IPv6 loopback interface.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The index for the IPv6 loopback interface.</returns>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x00099BEC File Offset: 0x00097DEC
		public static int IPv6LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.InternalIPv6LoopbackInterfaceIndex;
			}
		}

		/// <summary>Gets the identifier of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the identifier.</returns>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual string Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the adapter name.</returns>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the description of the interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes this interface.</returns>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns an object that describes the configuration of this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> object that describes this network interface.</returns>
		// Token: 0x060029D2 RID: 10706 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual IPInterfaceProperties GetIPProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the IPv4 statistics for this <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceStatistics" /> object.</returns>
		// Token: 0x060029D3 RID: 10707 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual IPv4InterfaceStatistics GetIPv4Statistics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the IP statistics for this <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.NetworkInformation.IPInterfaceStatistics" />.The IP statistics.</returns>
		// Token: 0x060029D4 RID: 10708 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual IPInterfaceStatistics GetIPStatistics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the current operational state of the network connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.OperationalStatus" /> values.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual OperationalStatus OperationalStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the speed of the network interface.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that specifies the speed in bits per second.</returns>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual long Speed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is set to only receive data packets.</summary>
		/// <returns>true if the interface only receives network traffic; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual bool IsReceiveOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is enabled to receive multicast packets.</summary>
		/// <returns>true if the interface receives multicast packets; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual bool SupportsMulticast
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the Media Access Control (MAC) or physical address for this adapter.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> object that contains the physical address.</returns>
		// Token: 0x060029D9 RID: 10713 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual PhysicalAddress GetPhysicalAddress()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the interface type.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.NetworkInterfaceType" /> value that specifies the network interface type.</returns>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface supports the specified protocol.</summary>
		/// <returns>true if the specified protocol is supported; otherwise, false.</returns>
		/// <param name="networkInterfaceComponent">A <see cref="T:System.Net.NetworkInformation.NetworkInterfaceComponent" /> value.</param>
		// Token: 0x060029DB RID: 10715 RVA: 0x0000822E File Offset: 0x0000642E
		public virtual bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			throw new NotImplementedException();
		}
	}
}
