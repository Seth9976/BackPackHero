using System;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000079 RID: 121
	public enum ReadStatus
	{
		// Token: 0x040001C6 RID: 454
		Complete,
		// Token: 0x040001C7 RID: 455
		InProgress,
		// Token: 0x040001C8 RID: 456
		Failed,
		// Token: 0x040001C9 RID: 457
		Truncated = 4,
		// Token: 0x040001CA RID: 458
		Canceled
	}
}
