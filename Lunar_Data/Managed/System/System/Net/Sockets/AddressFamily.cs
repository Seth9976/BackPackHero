using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020005B2 RID: 1458
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x04001B55 RID: 6997
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x04001B56 RID: 6998
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x04001B57 RID: 6999
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x04001B58 RID: 7000
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x04001B59 RID: 7001
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x04001B5A RID: 7002
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x04001B5B RID: 7003
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x04001B5C RID: 7004
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x04001B5D RID: 7005
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x04001B5E RID: 7006
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x04001B5F RID: 7007
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x04001B60 RID: 7008
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x04001B61 RID: 7009
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x04001B62 RID: 7010
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x04001B63 RID: 7011
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x04001B64 RID: 7012
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x04001B65 RID: 7013
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x04001B66 RID: 7014
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x04001B67 RID: 7015
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x04001B68 RID: 7016
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x04001B69 RID: 7017
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x04001B6A RID: 7018
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x04001B6B RID: 7019
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x04001B6C RID: 7020
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x04001B6D RID: 7021
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x04001B6E RID: 7022
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x04001B6F RID: 7023
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x04001B70 RID: 7024
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x04001B71 RID: 7025
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x04001B72 RID: 7026
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x04001B73 RID: 7027
		Max
	}
}
