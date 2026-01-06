using System;

namespace System.Net
{
	/// <summary>Defines transport types for the <see cref="T:System.Net.SocketPermission" /> and <see cref="T:System.Net.Sockets.Socket" /> classes.</summary>
	// Token: 0x02000415 RID: 1045
	public enum TransportType
	{
		/// <summary>UDP transport.</summary>
		// Token: 0x0400130A RID: 4874
		Udp = 1,
		/// <summary>The transport type is connectionless, such as UDP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Udp" />.</summary>
		// Token: 0x0400130B RID: 4875
		Connectionless = 1,
		/// <summary>TCP transport.</summary>
		// Token: 0x0400130C RID: 4876
		Tcp,
		/// <summary>The transport is connection oriented, such as TCP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Tcp" />.</summary>
		// Token: 0x0400130D RID: 4877
		ConnectionOriented = 2,
		/// <summary>All transport types.</summary>
		// Token: 0x0400130E RID: 4878
		All
	}
}
