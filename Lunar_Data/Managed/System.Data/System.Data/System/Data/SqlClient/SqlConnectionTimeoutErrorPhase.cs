using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200018F RID: 399
	internal enum SqlConnectionTimeoutErrorPhase
	{
		// Token: 0x04000D07 RID: 3335
		Undefined,
		// Token: 0x04000D08 RID: 3336
		PreLoginBegin,
		// Token: 0x04000D09 RID: 3337
		InitializeConnection,
		// Token: 0x04000D0A RID: 3338
		SendPreLoginHandshake,
		// Token: 0x04000D0B RID: 3339
		ConsumePreLoginHandshake,
		// Token: 0x04000D0C RID: 3340
		LoginBegin,
		// Token: 0x04000D0D RID: 3341
		ProcessConnectionAuth,
		// Token: 0x04000D0E RID: 3342
		PostLogin,
		// Token: 0x04000D0F RID: 3343
		Complete,
		// Token: 0x04000D10 RID: 3344
		Count
	}
}
