using System;

namespace System.Net
{
	// Token: 0x0200043B RID: 1083
	[Flags]
	internal enum CloseExState
	{
		// Token: 0x040013FA RID: 5114
		Normal = 0,
		// Token: 0x040013FB RID: 5115
		Abort = 1,
		// Token: 0x040013FC RID: 5116
		Silent = 2
	}
}
