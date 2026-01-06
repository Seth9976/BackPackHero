using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200002A RID: 42
	internal enum ConsentStatus
	{
		// Token: 0x040000B8 RID: 184
		Unknown,
		// Token: 0x040000B9 RID: 185
		Forgetting,
		// Token: 0x040000BA RID: 186
		OptedOut,
		// Token: 0x040000BB RID: 187
		NotRequired,
		// Token: 0x040000BC RID: 188
		RequiredButUnchecked,
		// Token: 0x040000BD RID: 189
		ConsentGiven,
		// Token: 0x040000BE RID: 190
		ConsentDenied
	}
}
