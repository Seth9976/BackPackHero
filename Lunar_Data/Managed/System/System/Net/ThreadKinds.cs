using System;

namespace System.Net
{
	// Token: 0x02000440 RID: 1088
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x0400140B RID: 5131
		Unknown = 0,
		// Token: 0x0400140C RID: 5132
		User = 1,
		// Token: 0x0400140D RID: 5133
		System = 2,
		// Token: 0x0400140E RID: 5134
		Sync = 4,
		// Token: 0x0400140F RID: 5135
		Async = 8,
		// Token: 0x04001410 RID: 5136
		Timer = 16,
		// Token: 0x04001411 RID: 5137
		CompletionPort = 32,
		// Token: 0x04001412 RID: 5138
		Worker = 64,
		// Token: 0x04001413 RID: 5139
		Finalization = 128,
		// Token: 0x04001414 RID: 5140
		Other = 256,
		// Token: 0x04001415 RID: 5141
		OwnerMask = 3,
		// Token: 0x04001416 RID: 5142
		SyncMask = 12,
		// Token: 0x04001417 RID: 5143
		SourceMask = 496,
		// Token: 0x04001418 RID: 5144
		SafeSources = 352,
		// Token: 0x04001419 RID: 5145
		ThreadPool = 96
	}
}
