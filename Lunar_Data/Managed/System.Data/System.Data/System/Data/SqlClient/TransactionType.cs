using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001BE RID: 446
	internal enum TransactionType
	{
		// Token: 0x04000E83 RID: 3715
		LocalFromTSQL = 1,
		// Token: 0x04000E84 RID: 3716
		LocalFromAPI,
		// Token: 0x04000E85 RID: 3717
		Delegated,
		// Token: 0x04000E86 RID: 3718
		Distributed,
		// Token: 0x04000E87 RID: 3719
		Context
	}
}
