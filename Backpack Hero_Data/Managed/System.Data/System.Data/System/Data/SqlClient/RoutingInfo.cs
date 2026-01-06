using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020F RID: 527
	internal class RoutingInfo
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0007D590 File Offset: 0x0007B790
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0007D598 File Offset: 0x0007B798
		internal byte Protocol { get; private set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0007D5A1 File Offset: 0x0007B7A1
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0007D5A9 File Offset: 0x0007B7A9
		internal ushort Port { get; private set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0007D5B2 File Offset: 0x0007B7B2
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x0007D5BA File Offset: 0x0007B7BA
		internal string ServerName { get; private set; }

		// Token: 0x060018C9 RID: 6345 RVA: 0x0007D5C3 File Offset: 0x0007B7C3
		internal RoutingInfo(byte protocol, ushort port, string servername)
		{
			this.Protocol = protocol;
			this.Port = port;
			this.ServerName = servername;
		}
	}
}
