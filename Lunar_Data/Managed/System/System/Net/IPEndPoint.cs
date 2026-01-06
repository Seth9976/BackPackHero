using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as an IP address and a port number.</summary>
	// Token: 0x02000390 RID: 912
	[Serializable]
	public class IPEndPoint : EndPoint
	{
		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0006D54A File Offset: 0x0006B74A
		public override AddressFamily AddressFamily
		{
			get
			{
				return this._address.AddressFamily;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">The IP address of the Internet host. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06001E0A RID: 7690 RVA: 0x0006D557 File Offset: 0x0006B757
		public IPEndPoint(long address, int port)
		{
			if (!TcpValidationHelpers.ValidatePortNumber(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this._port = port;
			this._address = new IPAddress(address);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06001E0B RID: 7691 RVA: 0x0006D585 File Offset: 0x0006B785
		public IPEndPoint(IPAddress address, int port)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!TcpValidationHelpers.ValidatePortNumber(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this._port = port;
			this._address = address;
		}

		/// <summary>Gets or sets the IP address of the endpoint.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance containing the IP address of the endpoint.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0006D5BC File Offset: 0x0006B7BC
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x0006D5C4 File Offset: 0x0006B7C4
		public IPAddress Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		/// <summary>Gets or sets the port number of the endpoint.</summary>
		/// <returns>An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort" /> to <see cref="F:System.Net.IPEndPoint.MaxPort" /> indicating the port number of the endpoint.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort" /> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0006D5CD File Offset: 0x0006B7CD
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0006D5D5 File Offset: 0x0006B7D5
		public int Port
		{
			get
			{
				return this._port;
			}
			set
			{
				if (!TcpValidationHelpers.ValidatePortNumber(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._port = value;
			}
		}

		/// <summary>Returns the IP address and port number of the specified endpoint.</summary>
		/// <returns>A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E10 RID: 7696 RVA: 0x0006D5F4 File Offset: 0x0006B7F4
		public override string ToString()
		{
			return string.Format((this._address.AddressFamily == AddressFamily.InterNetworkV6) ? "[{0}]:{1}" : "{0}:{1}", this._address.ToString(), this.Port.ToString(NumberFormatInfo.InvariantInfo));
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance containing the socket address for the endpoint.</returns>
		// Token: 0x06001E11 RID: 7697 RVA: 0x0006D63F File Offset: 0x0006B83F
		public override SocketAddress Serialize()
		{
			return new SocketAddress(this.Address, this.Port);
		}

		/// <summary>Creates an endpoint from a socket address.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> instance using the specified socket address.</returns>
		/// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress" /> to use for the endpoint. </param>
		/// <exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress" /> is not equal to the AddressFamily of the current instance.-or- <paramref name="socketAddress" />.Size &lt; 8. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001E12 RID: 7698 RVA: 0x0006D654 File Offset: 0x0006B854
		public override EndPoint Create(SocketAddress socketAddress)
		{
			if (socketAddress.Family != this.AddressFamily)
			{
				throw new ArgumentException(SR.Format("The AddressFamily {0} is not valid for the {1} end point, use {2} instead.", socketAddress.Family.ToString(), base.GetType().FullName, this.AddressFamily.ToString()), "socketAddress");
			}
			if (socketAddress.Size < 8)
			{
				throw new ArgumentException(SR.Format("The supplied {0} is an invalid size for the {1} end point.", socketAddress.GetType().FullName, base.GetType().FullName), "socketAddress");
			}
			return socketAddress.GetIPEndPoint();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="comparand">The specified <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Net.IPEndPoint" /> instance.</param>
		// Token: 0x06001E13 RID: 7699 RVA: 0x0006D6F4 File Offset: 0x0006B8F4
		public override bool Equals(object comparand)
		{
			IPEndPoint ipendPoint = comparand as IPEndPoint;
			return ipendPoint != null && ipendPoint._address.Equals(this._address) && ipendPoint._port == this._port;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E14 RID: 7700 RVA: 0x0006D72E File Offset: 0x0006B92E
		public override int GetHashCode()
		{
			return this._address.GetHashCode() ^ this._port;
		}

		/// <summary>Specifies the minimum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. This field is read-only.</summary>
		// Token: 0x04000FC1 RID: 4033
		public const int MinPort = 0;

		/// <summary>Specifies the maximum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. The MaxPort value is set to 0x0000FFFF. This field is read-only.</summary>
		// Token: 0x04000FC2 RID: 4034
		public const int MaxPort = 65535;

		// Token: 0x04000FC3 RID: 4035
		private IPAddress _address;

		// Token: 0x04000FC4 RID: 4036
		private int _port;

		// Token: 0x04000FC5 RID: 4037
		internal const int AnyPort = 0;

		// Token: 0x04000FC6 RID: 4038
		internal static IPEndPoint Any = new IPEndPoint(IPAddress.Any, 0);

		// Token: 0x04000FC7 RID: 4039
		internal static IPEndPoint IPv6Any = new IPEndPoint(IPAddress.IPv6Any, 0);
	}
}
