using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000210 RID: 528
	internal sealed class SqlEnvChange
	{
		// Token: 0x040011AB RID: 4523
		internal byte type;

		// Token: 0x040011AC RID: 4524
		internal byte oldLength;

		// Token: 0x040011AD RID: 4525
		internal int newLength;

		// Token: 0x040011AE RID: 4526
		internal int length;

		// Token: 0x040011AF RID: 4527
		internal string newValue;

		// Token: 0x040011B0 RID: 4528
		internal string oldValue;

		// Token: 0x040011B1 RID: 4529
		internal byte[] newBinValue;

		// Token: 0x040011B2 RID: 4530
		internal byte[] oldBinValue;

		// Token: 0x040011B3 RID: 4531
		internal long newLongValue;

		// Token: 0x040011B4 RID: 4532
		internal long oldLongValue;

		// Token: 0x040011B5 RID: 4533
		internal SqlCollation newCollation;

		// Token: 0x040011B6 RID: 4534
		internal SqlCollation oldCollation;

		// Token: 0x040011B7 RID: 4535
		internal RoutingInfo newRoutingInfo;
	}
}
