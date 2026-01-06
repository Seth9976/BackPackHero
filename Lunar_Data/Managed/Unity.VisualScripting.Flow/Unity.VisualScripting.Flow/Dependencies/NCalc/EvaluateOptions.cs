using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x0200018D RID: 397
	[Flags]
	public enum EvaluateOptions
	{
		// Token: 0x0400024F RID: 591
		None = 1,
		// Token: 0x04000250 RID: 592
		IgnoreCase = 2,
		// Token: 0x04000251 RID: 593
		NoCache = 4,
		// Token: 0x04000252 RID: 594
		IterateParameters = 8,
		// Token: 0x04000253 RID: 595
		RoundAwayFromZero = 16
	}
}
