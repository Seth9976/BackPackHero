using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as a host name or a string representation of an IP address and a port number.</summary>
	// Token: 0x020003DB RID: 987
	public class DnsEndPoint : EndPoint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address and a port number.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x06002071 RID: 8305 RVA: 0x00076EFF File Offset: 0x000750FF
		public DnsEndPoint(string host, int port)
			: this(host, port, AddressFamily.Unspecified)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address, a port number, and an address family.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.-or- <paramref name="addressFamily" /> is <see cref="F:System.Net.Sockets.AddressFamily.Unknown" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x06002072 RID: 8306 RVA: 0x00076F0C File Offset: 0x0007510C
		public DnsEndPoint(string host, int port, AddressFamily addressFamily)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "host" }));
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (addressFamily != AddressFamily.InterNetwork && addressFamily != AddressFamily.InterNetworkV6 && addressFamily != AddressFamily.Unspecified)
			{
				throw new ArgumentException(SR.GetString("The specified value is not valid."), "addressFamily");
			}
			this.m_Host = host;
			this.m_Port = port;
			this.m_Family = addressFamily;
		}

		/// <summary>Compares two <see cref="T:System.Net.DnsEndPoint" /> objects.</summary>
		/// <returns>true if the two <see cref="T:System.Net.DnsEndPoint" /> instances are equal; otherwise, false.</returns>
		/// <param name="comparand">A <see cref="T:System.Net.DnsEndPoint" /> instance to compare to the current instance.</param>
		// Token: 0x06002073 RID: 8307 RVA: 0x00076FA0 File Offset: 0x000751A0
		public override bool Equals(object comparand)
		{
			DnsEndPoint dnsEndPoint = comparand as DnsEndPoint;
			return dnsEndPoint != null && (this.m_Family == dnsEndPoint.m_Family && this.m_Port == dnsEndPoint.m_Port) && this.m_Host == dnsEndPoint.m_Host;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer hash value for the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x06002074 RID: 8308 RVA: 0x00076FE8 File Offset: 0x000751E8
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
		}

		/// <summary>Returns the host name or string representation of the IP address and port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>A string containing the address family, host name or IP address string, and the port number of the specified <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x06002075 RID: 8309 RVA: 0x00076FFC File Offset: 0x000751FC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.m_Family.ToString(),
				"/",
				this.m_Host,
				":",
				this.m_Port.ToString()
			});
		}

		/// <summary>Gets the host name or string representation of the Internet Protocol (IP) address of the host.</summary>
		/// <returns>A host name or string representation of an IP address.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0007704F File Offset: 0x0007524F
		public string Host
		{
			get
			{
				return this.m_Host;
			}
		}

		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x00077057 File Offset: 0x00075257
		public override AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Gets the port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer value in the range 0 to 0xffff indicating the port number of the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x0007705F File Offset: 0x0007525F
		public int Port
		{
			get
			{
				return this.m_Port;
			}
		}

		// Token: 0x04001143 RID: 4419
		private string m_Host;

		// Token: 0x04001144 RID: 4420
		private int m_Port;

		// Token: 0x04001145 RID: 4421
		private AddressFamily m_Family;
	}
}
