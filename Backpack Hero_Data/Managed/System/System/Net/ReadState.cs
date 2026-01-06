using System;

namespace System.Net
{
	// Token: 0x020004CE RID: 1230
	internal enum ReadState
	{
		// Token: 0x0400171B RID: 5915
		None,
		// Token: 0x0400171C RID: 5916
		Status,
		// Token: 0x0400171D RID: 5917
		Headers,
		// Token: 0x0400171E RID: 5918
		Content,
		// Token: 0x0400171F RID: 5919
		Aborted
	}
}
