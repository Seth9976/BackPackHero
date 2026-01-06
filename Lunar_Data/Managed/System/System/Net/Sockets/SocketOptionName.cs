using System;

namespace System.Net.Sockets
{
	/// <summary>Defines configuration option names.</summary>
	// Token: 0x020005C4 RID: 1476
	public enum SocketOptionName
	{
		/// <summary>Record debugging information.</summary>
		// Token: 0x04001C40 RID: 7232
		Debug = 1,
		/// <summary>The socket is listening.</summary>
		// Token: 0x04001C41 RID: 7233
		AcceptConnection,
		/// <summary>Allows the socket to be bound to an address that is already in use.</summary>
		// Token: 0x04001C42 RID: 7234
		ReuseAddress = 4,
		/// <summary>Use keep-alives.</summary>
		// Token: 0x04001C43 RID: 7235
		KeepAlive = 8,
		/// <summary>Do not route; send the packet directly to the interface addresses.</summary>
		// Token: 0x04001C44 RID: 7236
		DontRoute = 16,
		/// <summary>Permit sending broadcast messages on the socket.</summary>
		// Token: 0x04001C45 RID: 7237
		Broadcast = 32,
		/// <summary>Bypass hardware when possible.</summary>
		// Token: 0x04001C46 RID: 7238
		UseLoopback = 64,
		/// <summary>Linger on close if unsent data is present.</summary>
		// Token: 0x04001C47 RID: 7239
		Linger = 128,
		/// <summary>Receives out-of-band data in the normal data stream.</summary>
		// Token: 0x04001C48 RID: 7240
		OutOfBandInline = 256,
		/// <summary>Close the socket gracefully without lingering.</summary>
		// Token: 0x04001C49 RID: 7241
		DontLinger = -129,
		/// <summary>Enables a socket to be bound for exclusive access.</summary>
		// Token: 0x04001C4A RID: 7242
		ExclusiveAddressUse = -5,
		/// <summary>Specifies the total per-socket buffer space reserved for sends. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04001C4B RID: 7243
		SendBuffer = 4097,
		/// <summary>Specifies the total per-socket buffer space reserved for receives. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04001C4C RID: 7244
		ReceiveBuffer,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Send" /> operations.</summary>
		// Token: 0x04001C4D RID: 7245
		SendLowWater,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operations.</summary>
		// Token: 0x04001C4E RID: 7246
		ReceiveLowWater,
		/// <summary>Send a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x04001C4F RID: 7247
		SendTimeout,
		/// <summary>Receive a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x04001C50 RID: 7248
		ReceiveTimeout,
		/// <summary>Get the error status and clear.</summary>
		// Token: 0x04001C51 RID: 7249
		Error,
		/// <summary>Get the socket type.</summary>
		// Token: 0x04001C52 RID: 7250
		Type,
		// Token: 0x04001C53 RID: 7251
		ReuseUnicastPort = 12295,
		/// <summary>Not supported; will throw a <see cref="T:System.Net.Sockets.SocketException" /> if used.</summary>
		// Token: 0x04001C54 RID: 7252
		MaxConnections = 2147483647,
		/// <summary>Specifies the IP options to be inserted into outgoing datagrams.</summary>
		// Token: 0x04001C55 RID: 7253
		IPOptions = 1,
		/// <summary>Indicates that the application provides the IP header for outgoing datagrams.</summary>
		// Token: 0x04001C56 RID: 7254
		HeaderIncluded,
		/// <summary>Change the IP header type of the service field.</summary>
		// Token: 0x04001C57 RID: 7255
		TypeOfService,
		/// <summary>Set the IP header Time-to-Live field.</summary>
		// Token: 0x04001C58 RID: 7256
		IpTimeToLive,
		/// <summary>Set the interface for outgoing multicast packets.</summary>
		// Token: 0x04001C59 RID: 7257
		MulticastInterface = 9,
		/// <summary>An IP multicast Time to Live.</summary>
		// Token: 0x04001C5A RID: 7258
		MulticastTimeToLive,
		/// <summary>An IP multicast loopback.</summary>
		// Token: 0x04001C5B RID: 7259
		MulticastLoopback,
		/// <summary>Add an IP group membership.</summary>
		// Token: 0x04001C5C RID: 7260
		AddMembership,
		/// <summary>Drop an IP group membership.</summary>
		// Token: 0x04001C5D RID: 7261
		DropMembership,
		/// <summary>Do not fragment IP datagrams.</summary>
		// Token: 0x04001C5E RID: 7262
		DontFragment,
		/// <summary>Join a source group.</summary>
		// Token: 0x04001C5F RID: 7263
		AddSourceMembership,
		/// <summary>Drop a source group.</summary>
		// Token: 0x04001C60 RID: 7264
		DropSourceMembership,
		/// <summary>Block data from a source.</summary>
		// Token: 0x04001C61 RID: 7265
		BlockSource,
		/// <summary>Unblock a previously blocked source.</summary>
		// Token: 0x04001C62 RID: 7266
		UnblockSource,
		/// <summary>Return information about received packets.</summary>
		// Token: 0x04001C63 RID: 7267
		PacketInformation,
		/// <summary>Specifies the maximum number of router hops for an Internet Protocol version 6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol version 4.</summary>
		// Token: 0x04001C64 RID: 7268
		HopLimit = 21,
		/// <summary>Enables restriction of a IPv6 socket to a specified scope, such as addresses with the same link local or site local prefix.This socket option enables applications to place access restrictions on IPv6 sockets. Such restrictions enable an application running on a private LAN to simply and robustly harden itself against external attacks. This socket option widens or narrows the scope of a listening socket, enabling unrestricted access from public and private users when appropriate, or restricting access only to the same site, as required. This socket option has defined protection levels specified in the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> enumeration.</summary>
		// Token: 0x04001C65 RID: 7269
		IPProtectionLevel = 23,
		/// <summary>Indicates if a socket created for the AF_INET6 address family is restricted to IPv6 communications only. Sockets created for the AF_INET6 address family may be used for both IPv6 and IPv4 communications. Some applications may want to restrict their use of a socket created for the AF_INET6 address family to IPv6 communications only. When this value is non-zero (the default on Windows), a socket created for the AF_INET6 address family can be used to send and receive IPv6 packets only. When this value is zero, a socket created for the AF_INET6 address family can be used to send and receive packets to and from an IPv6 address or an IPv4 address. Note that the ability to interact with an IPv4 address requires the use of IPv4 mapped addresses. This socket option is supported on Windows Vista or later.</summary>
		// Token: 0x04001C66 RID: 7270
		IPv6Only = 27,
		/// <summary>Disables the Nagle algorithm for send coalescing.</summary>
		// Token: 0x04001C67 RID: 7271
		NoDelay = 1,
		/// <summary>Use urgent data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x04001C68 RID: 7272
		BsdUrgent,
		/// <summary>Use expedited data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x04001C69 RID: 7273
		Expedited = 2,
		/// <summary>Send UDP datagrams with checksum set to zero.</summary>
		// Token: 0x04001C6A RID: 7274
		NoChecksum = 1,
		/// <summary>Set or get the UDP checksum coverage.</summary>
		// Token: 0x04001C6B RID: 7275
		ChecksumCoverage = 20,
		/// <summary>Updates an accepted socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x04001C6C RID: 7276
		UpdateAcceptContext = 28683,
		/// <summary>Updates a connected socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x04001C6D RID: 7277
		UpdateConnectContext = 28688
	}
}
