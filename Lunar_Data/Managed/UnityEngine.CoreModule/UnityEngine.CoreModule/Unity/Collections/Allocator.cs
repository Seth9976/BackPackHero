using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x0200008C RID: 140
	[UsedByNativeCode]
	public enum Allocator
	{
		// Token: 0x0400020B RID: 523
		Invalid,
		// Token: 0x0400020C RID: 524
		None,
		// Token: 0x0400020D RID: 525
		Temp,
		// Token: 0x0400020E RID: 526
		TempJob,
		// Token: 0x0400020F RID: 527
		Persistent,
		// Token: 0x04000210 RID: 528
		AudioKernel
	}
}
