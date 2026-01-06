using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x0200006A RID: 106
	[Obsolete("Reflection data is now universal between job types. The parameter can be removed.", false)]
	public enum JobType
	{
		// Token: 0x0400018F RID: 399
		Single,
		// Token: 0x04000190 RID: 400
		ParallelFor
	}
}
