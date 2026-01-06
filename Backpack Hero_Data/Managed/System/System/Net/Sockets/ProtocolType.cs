using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x020005BA RID: 1466
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x04001BC6 RID: 7110
		IP,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x04001BC7 RID: 7111
		IPv6HopByHopOptions = 0,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x04001BC8 RID: 7112
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x04001BC9 RID: 7113
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x04001BCA RID: 7114
		Ggp,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x04001BCB RID: 7115
		IPv4,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x04001BCC RID: 7116
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x04001BCD RID: 7117
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x04001BCE RID: 7118
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x04001BCF RID: 7119
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6). </summary>
		// Token: 0x04001BD0 RID: 7120
		IPv6 = 41,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x04001BD1 RID: 7121
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x04001BD2 RID: 7122
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x04001BD3 RID: 7123
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at http://www.ietf.org.</summary>
		// Token: 0x04001BD4 RID: 7124
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x04001BD5 RID: 7125
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x04001BD6 RID: 7126
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x04001BD7 RID: 7127
		IPv6DestinationOptions,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x04001BD8 RID: 7128
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x04001BD9 RID: 7129
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04001BDA RID: 7130
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x04001BDB RID: 7131
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x04001BDC RID: 7132
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x04001BDD RID: 7133
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x04001BDE RID: 7134
		Unknown = -1
	}
}
