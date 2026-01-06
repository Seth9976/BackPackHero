using System;

namespace System
{
	// Token: 0x0200015B RID: 347
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x0400063A RID: 1594
		CopyOnly = 0,
		// Token: 0x0400063B RID: 1595
		Escape = 1,
		// Token: 0x0400063C RID: 1596
		Unescape = 2,
		// Token: 0x0400063D RID: 1597
		EscapeUnescape = 3,
		// Token: 0x0400063E RID: 1598
		V1ToStringFlag = 4,
		// Token: 0x0400063F RID: 1599
		UnescapeAll = 8,
		// Token: 0x04000640 RID: 1600
		UnescapeAllOrThrow = 24
	}
}
