using System;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x02000211 RID: 529
	internal sealed class SqlLogin
	{
		// Token: 0x040011B8 RID: 4536
		internal int timeout;

		// Token: 0x040011B9 RID: 4537
		internal bool userInstance;

		// Token: 0x040011BA RID: 4538
		internal string hostName = "";

		// Token: 0x040011BB RID: 4539
		internal string userName = "";

		// Token: 0x040011BC RID: 4540
		internal string password = "";

		// Token: 0x040011BD RID: 4541
		internal string applicationName = "";

		// Token: 0x040011BE RID: 4542
		internal string serverName = "";

		// Token: 0x040011BF RID: 4543
		internal string language = "";

		// Token: 0x040011C0 RID: 4544
		internal string database = "";

		// Token: 0x040011C1 RID: 4545
		internal string attachDBFilename = "";

		// Token: 0x040011C2 RID: 4546
		internal bool useReplication;

		// Token: 0x040011C3 RID: 4547
		internal string newPassword = "";

		// Token: 0x040011C4 RID: 4548
		internal bool useSSPI;

		// Token: 0x040011C5 RID: 4549
		internal int packetSize = 8000;

		// Token: 0x040011C6 RID: 4550
		internal bool readOnlyIntent;

		// Token: 0x040011C7 RID: 4551
		internal SqlCredential credential;

		// Token: 0x040011C8 RID: 4552
		internal SecureString newSecurePassword;
	}
}
