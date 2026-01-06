using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001F3 RID: 499
	internal enum SniContext
	{
		// Token: 0x040010DD RID: 4317
		Undefined,
		// Token: 0x040010DE RID: 4318
		Snix_Connect,
		// Token: 0x040010DF RID: 4319
		Snix_PreLoginBeforeSuccessfulWrite,
		// Token: 0x040010E0 RID: 4320
		Snix_PreLogin,
		// Token: 0x040010E1 RID: 4321
		Snix_LoginSspi,
		// Token: 0x040010E2 RID: 4322
		Snix_ProcessSspi,
		// Token: 0x040010E3 RID: 4323
		Snix_Login,
		// Token: 0x040010E4 RID: 4324
		Snix_EnableMars,
		// Token: 0x040010E5 RID: 4325
		Snix_AutoEnlist,
		// Token: 0x040010E6 RID: 4326
		Snix_GetMarsSession,
		// Token: 0x040010E7 RID: 4327
		Snix_Execute,
		// Token: 0x040010E8 RID: 4328
		Snix_Read,
		// Token: 0x040010E9 RID: 4329
		Snix_Close,
		// Token: 0x040010EA RID: 4330
		Snix_SendRows
	}
}
