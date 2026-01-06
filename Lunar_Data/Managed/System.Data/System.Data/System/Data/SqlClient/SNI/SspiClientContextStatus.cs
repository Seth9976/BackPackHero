using System;
using System.Net;
using System.Net.Security;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000254 RID: 596
	internal class SspiClientContextStatus
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000873E6 File Offset: 0x000855E6
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x000873EE File Offset: 0x000855EE
		public SafeFreeCredentials CredentialsHandle { get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000873F7 File Offset: 0x000855F7
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x000873FF File Offset: 0x000855FF
		public SafeDeleteContext SecurityContext { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x00087408 File Offset: 0x00085608
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x00087410 File Offset: 0x00085610
		public ContextFlagsPal ContextFlags { get; set; }
	}
}
