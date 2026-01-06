using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001BD RID: 445
	internal enum TransactionState
	{
		// Token: 0x04000E7D RID: 3709
		Pending,
		// Token: 0x04000E7E RID: 3710
		Active,
		// Token: 0x04000E7F RID: 3711
		Aborted,
		// Token: 0x04000E80 RID: 3712
		Committed,
		// Token: 0x04000E81 RID: 3713
		Unknown
	}
}
