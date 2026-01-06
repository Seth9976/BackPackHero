using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020A RID: 522
	internal enum PreLoginOptions
	{
		// Token: 0x04001187 RID: 4487
		VERSION,
		// Token: 0x04001188 RID: 4488
		ENCRYPT,
		// Token: 0x04001189 RID: 4489
		INSTANCE,
		// Token: 0x0400118A RID: 4490
		THREADID,
		// Token: 0x0400118B RID: 4491
		MARS,
		// Token: 0x0400118C RID: 4492
		TRACEID,
		// Token: 0x0400118D RID: 4493
		FEDAUTHREQUIRED,
		// Token: 0x0400118E RID: 4494
		NUMOPT,
		// Token: 0x0400118F RID: 4495
		LASTOPT = 255
	}
}
