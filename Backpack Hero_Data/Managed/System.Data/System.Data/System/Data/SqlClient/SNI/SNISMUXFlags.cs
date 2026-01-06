using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200023C RID: 572
	[Flags]
	internal enum SNISMUXFlags
	{
		// Token: 0x040012D0 RID: 4816
		SMUX_SYN = 1,
		// Token: 0x040012D1 RID: 4817
		SMUX_ACK = 2,
		// Token: 0x040012D2 RID: 4818
		SMUX_FIN = 4,
		// Token: 0x040012D3 RID: 4819
		SMUX_DATA = 8
	}
}
