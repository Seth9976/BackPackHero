using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the type of protocol that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020005B9 RID: 1465
	public enum ProtocolFamily
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x04001BA6 RID: 7078
		Unknown = -1,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04001BA7 RID: 7079
		Unspecified,
		/// <summary>Unix local to host protocol.</summary>
		// Token: 0x04001BA8 RID: 7080
		Unix,
		/// <summary>IP version 4 protocol.</summary>
		// Token: 0x04001BA9 RID: 7081
		InterNetwork,
		/// <summary>ARPANET IMP protocol.</summary>
		// Token: 0x04001BAA RID: 7082
		ImpLink,
		/// <summary>PUP protocol.</summary>
		// Token: 0x04001BAB RID: 7083
		Pup,
		/// <summary>MIT CHAOS protocol.</summary>
		// Token: 0x04001BAC RID: 7084
		Chaos,
		/// <summary>Xerox NS protocol.</summary>
		// Token: 0x04001BAD RID: 7085
		NS,
		/// <summary>IPX or SPX protocol.</summary>
		// Token: 0x04001BAE RID: 7086
		Ipx = 6,
		/// <summary>ISO protocol.</summary>
		// Token: 0x04001BAF RID: 7087
		Iso,
		/// <summary>OSI protocol.</summary>
		// Token: 0x04001BB0 RID: 7088
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) protocol.</summary>
		// Token: 0x04001BB1 RID: 7089
		Ecma,
		/// <summary>DataKit protocol.</summary>
		// Token: 0x04001BB2 RID: 7090
		DataKit,
		/// <summary>CCITT protocol, such as X.25.</summary>
		// Token: 0x04001BB3 RID: 7091
		Ccitt,
		/// <summary>IBM SNA protocol.</summary>
		// Token: 0x04001BB4 RID: 7092
		Sna,
		/// <summary>DECNet protocol.</summary>
		// Token: 0x04001BB5 RID: 7093
		DecNet,
		/// <summary>Direct data link protocol.</summary>
		// Token: 0x04001BB6 RID: 7094
		DataLink,
		/// <summary>LAT protocol.</summary>
		// Token: 0x04001BB7 RID: 7095
		Lat,
		/// <summary>NSC HyperChannel protocol.</summary>
		// Token: 0x04001BB8 RID: 7096
		HyperChannel,
		/// <summary>AppleTalk protocol.</summary>
		// Token: 0x04001BB9 RID: 7097
		AppleTalk,
		/// <summary>NetBIOS protocol.</summary>
		// Token: 0x04001BBA RID: 7098
		NetBios,
		/// <summary>VoiceView protocol.</summary>
		// Token: 0x04001BBB RID: 7099
		VoiceView,
		/// <summary>FireFox protocol.</summary>
		// Token: 0x04001BBC RID: 7100
		FireFox,
		/// <summary>Banyan protocol.</summary>
		// Token: 0x04001BBD RID: 7101
		Banyan = 21,
		/// <summary>Native ATM services protocol.</summary>
		// Token: 0x04001BBE RID: 7102
		Atm,
		/// <summary>IP version 6 protocol.</summary>
		// Token: 0x04001BBF RID: 7103
		InterNetworkV6,
		/// <summary>Microsoft Cluster products protocol.</summary>
		// Token: 0x04001BC0 RID: 7104
		Cluster,
		/// <summary>IEEE 1284.4 workgroup protocol.</summary>
		// Token: 0x04001BC1 RID: 7105
		Ieee12844,
		/// <summary>IrDA protocol.</summary>
		// Token: 0x04001BC2 RID: 7106
		Irda,
		/// <summary>Network Designers OSI gateway enabled protocol.</summary>
		// Token: 0x04001BC3 RID: 7107
		NetworkDesigners = 28,
		/// <summary>MAX protocol.</summary>
		// Token: 0x04001BC4 RID: 7108
		Max
	}
}
