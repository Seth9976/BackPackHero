using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x0200051D RID: 1309
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x040018CA RID: 6346
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x040018CB RID: 6347
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x040018CC RID: 6348
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x040018CD RID: 6349
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x040018CE RID: 6350
		Hybrid = 8
	}
}
