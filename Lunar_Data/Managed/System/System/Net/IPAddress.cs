using System;
using System.Buffers.Binary;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	/// <summary>Provides an Internet Protocol (IP) address.</summary>
	// Token: 0x0200038D RID: 909
	[Serializable]
	public class IPAddress
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0006C803 File Offset: 0x0006AA03
		private bool IsIPv4
		{
			get
			{
				return this._numbers == null;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0006C80E File Offset: 0x0006AA0E
		private bool IsIPv6
		{
			get
			{
				return this._numbers != null;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0006C819 File Offset: 0x0006AA19
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0006C821 File Offset: 0x0006AA21
		private uint PrivateAddress
		{
			get
			{
				return this._addressOrScopeId;
			}
			set
			{
				this._toString = null;
				this._hashCode = 0;
				this._addressOrScopeId = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0006C819 File Offset: 0x0006AA19
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0006C821 File Offset: 0x0006AA21
		private uint PrivateScopeId
		{
			get
			{
				return this._addressOrScopeId;
			}
			set
			{
				this._toString = null;
				this._hashCode = 0;
				this._addressOrScopeId = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as an <see cref="T:System.Int64" />.</summary>
		/// <param name="newAddress">The long value of the IP address. For example, the value 0x2414188f in big-endian format would be the IP address "143.24.20.36". </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newAddress" /> &lt; 0 or <paramref name="newAddress" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x06001DCE RID: 7630 RVA: 0x0006C838 File Offset: 0x0006AA38
		public IPAddress(long newAddress)
		{
			if (newAddress < 0L || newAddress > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("newAddress");
			}
			this.PrivateAddress = (uint)newAddress;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array and the specified scope identifier.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <param name="scopeid">The long value of the scope identifier. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeid" /> &lt; 0 or <paramref name="scopeid" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x06001DCF RID: 7631 RVA: 0x0006C85D File Offset: 0x0006AA5D
		public IPAddress(byte[] address, long scopeid)
			: this(new ReadOnlySpan<byte>(address ?? IPAddress.ThrowAddressNullException()), scopeid)
		{
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0006C878 File Offset: 0x0006AA78
		public unsafe IPAddress(ReadOnlySpan<byte> address, long scopeid)
		{
			if (address.Length != 16)
			{
				throw new ArgumentException("An invalid IP address was specified.", "address");
			}
			if (scopeid < 0L || scopeid > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("scopeid");
			}
			this._numbers = new ushort[8];
			for (int i = 0; i < 8; i++)
			{
				this._numbers[i] = (ushort)((int)(*address[i * 2]) * 256 + (int)(*address[i * 2 + 1]));
			}
			this.PrivateScopeId = (uint)scopeid;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0006C904 File Offset: 0x0006AB04
		internal unsafe IPAddress(ushort* numbers, int numbersLength, uint scopeid)
		{
			ushort[] array = new ushort[8];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = numbers[i];
			}
			this._numbers = array;
			this.PrivateScopeId = scopeid;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0006C944 File Offset: 0x0006AB44
		private IPAddress(ushort[] numbers, uint scopeid)
		{
			this._numbers = numbers;
			this.PrivateScopeId = scopeid;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x0006C95A File Offset: 0x0006AB5A
		public IPAddress(byte[] address)
			: this(new ReadOnlySpan<byte>(address ?? IPAddress.ThrowAddressNullException()))
		{
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0006C974 File Offset: 0x0006AB74
		public unsafe IPAddress(ReadOnlySpan<byte> address)
		{
			if (address.Length == 4)
			{
				this.PrivateAddress = (uint)((long)(((int)(*address[3]) << 24) | ((int)(*address[2]) << 16) | ((int)(*address[1]) << 8) | (int)(*address[0])) & (long)((ulong)(-1)));
				return;
			}
			if (address.Length == 16)
			{
				this._numbers = new ushort[8];
				for (int i = 0; i < 8; i++)
				{
					this._numbers[i] = (ushort)((int)(*address[i * 2]) * 256 + (int)(*address[i * 2 + 1]));
				}
				return;
			}
			throw new ArgumentException("An invalid IP address was specified.", "address");
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0006CA27 File Offset: 0x0006AC27
		internal IPAddress(int newAddress)
		{
			this.PrivateAddress = (uint)newAddress;
		}

		/// <summary>Determines whether a string is a valid IP address.</summary>
		/// <returns>true if <paramref name="ipString" /> is a valid IP address; otherwise, false.</returns>
		/// <param name="ipString">The string to validate.</param>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> version of the string.</param>
		// Token: 0x06001DD6 RID: 7638 RVA: 0x0006CA36 File Offset: 0x0006AC36
		public static bool TryParse(string ipString, out IPAddress address)
		{
			if (ipString == null)
			{
				address = null;
				return false;
			}
			address = IPAddressParser.Parse(ipString.AsSpan(), true);
			return address != null;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0006CA53 File Offset: 0x0006AC53
		public static bool TryParse(ReadOnlySpan<char> ipSpan, out IPAddress address)
		{
			address = IPAddressParser.Parse(ipSpan, true);
			return address != null;
		}

		/// <summary>Converts an IP address string to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance.</returns>
		/// <param name="ipString">A string that contains an IP address in dotted-quad notation for IPv4 and in colon-hexadecimal notation for IPv6. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="ipString" /> is not a valid IP address. </exception>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x0006CA63 File Offset: 0x0006AC63
		public static IPAddress Parse(string ipString)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			return IPAddressParser.Parse(ipString.AsSpan(), false);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0006CA7F File Offset: 0x0006AC7F
		public static IPAddress Parse(ReadOnlySpan<char> ipSpan)
		{
			return IPAddressParser.Parse(ipSpan, false);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0006CA88 File Offset: 0x0006AC88
		public bool TryWriteBytes(Span<byte> destination, out int bytesWritten)
		{
			if (this.IsIPv6)
			{
				if (destination.Length < 16)
				{
					bytesWritten = 0;
					return false;
				}
				this.WriteIPv6Bytes(destination);
				bytesWritten = 16;
			}
			else
			{
				if (destination.Length < 4)
				{
					bytesWritten = 0;
					return false;
				}
				this.WriteIPv4Bytes(destination);
				bytesWritten = 4;
			}
			return true;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0006CAD4 File Offset: 0x0006ACD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteIPv6Bytes(Span<byte> destination)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				*destination[num++] = (byte)((this._numbers[i] >> 8) & 255);
				*destination[num++] = (byte)(this._numbers[i] & 255);
			}
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0006CB2C File Offset: 0x0006AD2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteIPv4Bytes(Span<byte> destination)
		{
			uint privateAddress = this.PrivateAddress;
			*destination[0] = (byte)privateAddress;
			*destination[1] = (byte)(privateAddress >> 8);
			*destination[2] = (byte)(privateAddress >> 16);
			*destination[3] = (byte)(privateAddress >> 24);
		}

		/// <summary>Provides a copy of the <see cref="T:System.Net.IPAddress" /> as an array of bytes.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06001DDD RID: 7645 RVA: 0x0006CB74 File Offset: 0x0006AD74
		public byte[] GetAddressBytes()
		{
			if (this.IsIPv6)
			{
				byte[] array = new byte[16];
				this.WriteIPv6Bytes(array);
				return array;
			}
			byte[] array2 = new byte[4];
			this.WriteIPv4Bytes(array2);
			return array2;
		}

		/// <summary>Gets the address family of the IP address.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> for IPv4 or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> for IPv6.</returns>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0006CBB3 File Offset: 0x0006ADB3
		public AddressFamily AddressFamily
		{
			get
			{
				if (!this.IsIPv4)
				{
					return AddressFamily.InterNetworkV6;
				}
				return AddressFamily.InterNetwork;
			}
		}

		/// <summary>Gets or sets the IPv6 address scope identifier.</summary>
		/// <returns>A long integer that specifies the scope of the address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">AddressFamily = InterNetwork. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeId" /> &lt; 0- or -<paramref name="scopeId" /> &gt; 0x00000000FFFFFFFF  </exception>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0006CBC1 File Offset: 0x0006ADC1
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0006CBDD File Offset: 0x0006ADDD
		public long ScopeId
		{
			get
			{
				if (this.IsIPv4)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return (long)((ulong)this.PrivateScopeId);
			}
			set
			{
				if (this.IsIPv4)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.PrivateScopeId = (uint)value;
			}
		}

		/// <summary>Converts an Internet address to its standard notation.</summary>
		/// <returns>A string that contains the IP address in either IPv4 dotted-quad or in IPv6 colon-hexadecimal notation.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> and the address is bad. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001DE1 RID: 7649 RVA: 0x0006CC0F File Offset: 0x0006AE0F
		public override string ToString()
		{
			if (this._toString == null)
			{
				this._toString = (this.IsIPv4 ? IPAddressParser.IPv4AddressToString(this.PrivateAddress) : IPAddressParser.IPv6AddressToString(this._numbers, this.PrivateScopeId));
			}
			return this._toString;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0006CC4B File Offset: 0x0006AE4B
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			if (!this.IsIPv4)
			{
				return IPAddressParser.IPv6AddressToString(this._numbers, this.PrivateScopeId, destination, out charsWritten);
			}
			return IPAddressParser.IPv4AddressToString(this.PrivateAddress, destination, out charsWritten);
		}

		/// <summary>Converts a long value from host byte order to network byte order.</summary>
		/// <returns>A long value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06001DE3 RID: 7651 RVA: 0x0006CC76 File Offset: 0x0006AE76
		public static long HostToNetworkOrder(long host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return BinaryPrimitives.ReverseEndianness(host);
		}

		/// <summary>Converts an integer value from host byte order to network byte order.</summary>
		/// <returns>An integer value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x0006CC87 File Offset: 0x0006AE87
		public static int HostToNetworkOrder(int host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return BinaryPrimitives.ReverseEndianness(host);
		}

		/// <summary>Converts a short value from host byte order to network byte order.</summary>
		/// <returns>A short value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x0006CC98 File Offset: 0x0006AE98
		public static short HostToNetworkOrder(short host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return BinaryPrimitives.ReverseEndianness(host);
		}

		/// <summary>Converts a long value from network byte order to host byte order.</summary>
		/// <returns>A long value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x0006CCA9 File Offset: 0x0006AEA9
		public static long NetworkToHostOrder(long network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts an integer value from network byte order to host byte order.</summary>
		/// <returns>An integer value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0006CCB1 File Offset: 0x0006AEB1
		public static int NetworkToHostOrder(int network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts a short value from network byte order to host byte order.</summary>
		/// <returns>A short value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x0006CCB9 File Offset: 0x0006AEB9
		public static short NetworkToHostOrder(short network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Indicates whether the specified IP address is the loopback address.</summary>
		/// <returns>true if <paramref name="address" /> is the loopback address; otherwise, false.</returns>
		/// <param name="address">An IP address. </param>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x0006CCC4 File Offset: 0x0006AEC4
		public static bool IsLoopback(IPAddress address)
		{
			if (address == null)
			{
				IPAddress.ThrowAddressNullException();
			}
			if (address.IsIPv6)
			{
				return address.Equals(IPAddress.IPv6Loopback);
			}
			return ((ulong)address.PrivateAddress & 255UL) == ((ulong)IPAddress.Loopback.PrivateAddress & 255UL);
		}

		/// <summary>Gets whether the address is an IPv6 multicast global address.</summary>
		/// <returns>true if the IP address is an IPv6 multicast global address; otherwise, false.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x0006CD10 File Offset: 0x0006AF10
		public bool IsIPv6Multicast
		{
			get
			{
				return this.IsIPv6 && (this._numbers[0] & 65280) == 65280;
			}
		}

		/// <summary>Gets whether the address is an IPv6 link local address.</summary>
		/// <returns>true if the IP address is an IPv6 link local address; otherwise, false.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0006CD31 File Offset: 0x0006AF31
		public bool IsIPv6LinkLocal
		{
			get
			{
				return this.IsIPv6 && (this._numbers[0] & 65472) == 65152;
			}
		}

		/// <summary>Gets whether the address is an IPv6 site local address.</summary>
		/// <returns>true if the IP address is an IPv6 site local address; otherwise, false.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0006CD52 File Offset: 0x0006AF52
		public bool IsIPv6SiteLocal
		{
			get
			{
				return this.IsIPv6 && (this._numbers[0] & 65472) == 65216;
			}
		}

		/// <summary>Gets whether the address is an IPv6 Teredo address.</summary>
		/// <returns>true if the IP address is an IPv6 Teredo address; otherwise, false.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0006CD73 File Offset: 0x0006AF73
		public bool IsIPv6Teredo
		{
			get
			{
				return this.IsIPv6 && this._numbers[0] == 8193 && this._numbers[1] == 0;
			}
		}

		/// <summary>Gets whether the IP address is an IPv4-mapped IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the IP address is an IPv4-mapped IPv6 address; otherwise, false.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0006CD9C File Offset: 0x0006AF9C
		public bool IsIPv4MappedToIPv6
		{
			get
			{
				if (this.IsIPv4)
				{
					return false;
				}
				for (int i = 0; i < 5; i++)
				{
					if (this._numbers[i] != 0)
					{
						return false;
					}
				}
				return this._numbers[5] == ushort.MaxValue;
			}
		}

		/// <summary>An Internet Protocol (IP) address.</summary>
		/// <returns>The long value of the IP address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0006CDDA File Offset: 0x0006AFDA
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0006CDF8 File Offset: 0x0006AFF8
		[Obsolete("This property has been deprecated. It is address family dependent. Please use IPAddress.Equals method to perform comparisons. https://go.microsoft.com/fwlink/?linkid=14202")]
		public long Address
		{
			get
			{
				if (this.AddressFamily == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return (long)((ulong)this.PrivateAddress);
			}
			set
			{
				if (this.AddressFamily == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if ((ulong)this.PrivateAddress != (ulong)value)
				{
					if (this is IPAddress.ReadOnlyIPAddress)
					{
						throw new SocketException(SocketError.OperationNotSupported);
					}
					this.PrivateAddress = (uint)value;
				}
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0006CE34 File Offset: 0x0006B034
		internal bool Equals(object comparandObj, bool compareScopeId)
		{
			IPAddress ipaddress = comparandObj as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.AddressFamily != ipaddress.AddressFamily)
			{
				return false;
			}
			if (this.IsIPv6)
			{
				for (int i = 0; i < 8; i++)
				{
					if (ipaddress._numbers[i] != this._numbers[i])
					{
						return false;
					}
				}
				return ipaddress.PrivateScopeId == this.PrivateScopeId || !compareScopeId;
			}
			return ipaddress.PrivateAddress == this.PrivateAddress;
		}

		/// <summary>Compares two IP addresses.</summary>
		/// <returns>true if the two addresses are equal; otherwise, false.</returns>
		/// <param name="comparand">An <see cref="T:System.Net.IPAddress" /> instance to compare to the current instance. </param>
		// Token: 0x06001DF2 RID: 7666 RVA: 0x0006CEA8 File Offset: 0x0006B0A8
		public override bool Equals(object comparand)
		{
			return this.Equals(comparand, true);
		}

		/// <summary>Returns a hash value for an IP address.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001DF3 RID: 7667 RVA: 0x0006CEB4 File Offset: 0x0006B0B4
		public unsafe override int GetHashCode()
		{
			if (this._hashCode != 0)
			{
				return this._hashCode;
			}
			int num;
			if (this.IsIPv6)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
				MemoryMarshal.AsBytes<ushort>(new ReadOnlySpan<ushort>(this._numbers)).CopyTo(span);
				BitConverter.TryWriteBytes(span.Slice(16), this._addressOrScopeId);
				num = Marvin.ComputeHash32(span, Marvin.DefaultSeed);
			}
			else
			{
				num = Marvin.ComputeHash32(MemoryMarshal.AsBytes<uint>(MemoryMarshal.CreateReadOnlySpan<uint>(ref this._addressOrScopeId, 1)), Marvin.DefaultSeed);
			}
			this._hashCode = num;
			return this._hashCode;
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv6 address.</returns>
		// Token: 0x06001DF4 RID: 7668 RVA: 0x0006CF50 File Offset: 0x0006B150
		public IPAddress MapToIPv6()
		{
			if (this.IsIPv6)
			{
				return this;
			}
			uint privateAddress = this.PrivateAddress;
			return new IPAddress(new ushort[]
			{
				0,
				0,
				0,
				0,
				0,
				ushort.MaxValue,
				(ushort)(((privateAddress & 65280U) >> 8) | ((privateAddress & 255U) << 8)),
				(ushort)(((privateAddress & 4278190080U) >> 24) | ((privateAddress & 16711680U) >> 8))
			}, 0U);
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv4 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv4 address.</returns>
		// Token: 0x06001DF5 RID: 7669 RVA: 0x0006CFB4 File Offset: 0x0006B1B4
		public IPAddress MapToIPv4()
		{
			if (this.IsIPv4)
			{
				return this;
			}
			return new IPAddress((long)((ulong)(((uint)(this._numbers[6] & 65280) >> 8) | (uint)((uint)(this._numbers[6] & 255) << 8) | ((((uint)(this._numbers[7] & 65280) >> 8) | (uint)((uint)(this._numbers[7] & 255) << 8)) << 16))));
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0006D017 File Offset: 0x0006B217
		private static byte[] ThrowAddressNullException()
		{
			throw new ArgumentNullException("address");
		}

		/// <summary>Provides an IP address that indicates that the server must listen for client activity on all network interfaces. This field is read-only.</summary>
		// Token: 0x04000FB3 RID: 4019
		public static readonly IPAddress Any = new IPAddress.ReadOnlyIPAddress(0L);

		/// <summary>Provides the IP loopback address. This field is read-only.</summary>
		// Token: 0x04000FB4 RID: 4020
		public static readonly IPAddress Loopback = new IPAddress.ReadOnlyIPAddress(16777343L);

		/// <summary>Provides the IP broadcast address. This field is read-only.</summary>
		// Token: 0x04000FB5 RID: 4021
		public static readonly IPAddress Broadcast = new IPAddress.ReadOnlyIPAddress((long)((ulong)(-1)));

		/// <summary>Provides an IP address that indicates that no network interface should be used. This field is read-only.</summary>
		// Token: 0x04000FB6 RID: 4022
		public static readonly IPAddress None = IPAddress.Broadcast;

		// Token: 0x04000FB7 RID: 4023
		internal const long LoopbackMask = 255L;

		/// <summary>The <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> method uses the <see cref="F:System.Net.IPAddress.IPv6Any" /> field to indicate that a <see cref="T:System.Net.Sockets.Socket" /> must listen for client activity on all network interfaces.</summary>
		// Token: 0x04000FB8 RID: 4024
		public static readonly IPAddress IPv6Any = new IPAddress(new byte[16], 0L);

		/// <summary>Provides the IP loopback address. This property is read-only.</summary>
		// Token: 0x04000FB9 RID: 4025
		public static readonly IPAddress IPv6Loopback = new IPAddress(new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 1
		}, 0L);

		/// <summary>Provides an IP address that indicates that no network interface should be used. This property is read-only.</summary>
		// Token: 0x04000FBA RID: 4026
		public static readonly IPAddress IPv6None = new IPAddress(new byte[16], 0L);

		// Token: 0x04000FBB RID: 4027
		private uint _addressOrScopeId;

		// Token: 0x04000FBC RID: 4028
		private readonly ushort[] _numbers;

		// Token: 0x04000FBD RID: 4029
		private string _toString;

		// Token: 0x04000FBE RID: 4030
		private int _hashCode;

		// Token: 0x04000FBF RID: 4031
		internal const int NumberOfLabels = 8;

		// Token: 0x0200038E RID: 910
		private sealed class ReadOnlyIPAddress : IPAddress
		{
			// Token: 0x06001DF8 RID: 7672 RVA: 0x0006D0A1 File Offset: 0x0006B2A1
			public ReadOnlyIPAddress(long newAddress)
				: base(newAddress)
			{
			}
		}
	}
}
