using System;

namespace System.Net
{
	// Token: 0x02000432 RID: 1074
	internal enum DataParseStatus
	{
		// Token: 0x040013AE RID: 5038
		NeedMoreData,
		// Token: 0x040013AF RID: 5039
		ContinueParsing,
		// Token: 0x040013B0 RID: 5040
		Done,
		// Token: 0x040013B1 RID: 5041
		Invalid,
		// Token: 0x040013B2 RID: 5042
		DataTooBig
	}
}
