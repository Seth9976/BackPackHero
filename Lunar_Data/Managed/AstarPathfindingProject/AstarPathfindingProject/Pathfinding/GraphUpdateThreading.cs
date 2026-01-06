using System;

namespace Pathfinding
{
	// Token: 0x02000037 RID: 55
	[Flags]
	public enum GraphUpdateThreading
	{
		// Token: 0x04000173 RID: 371
		UnityThread = 0,
		// Token: 0x04000174 RID: 372
		UnityInit = 2,
		// Token: 0x04000175 RID: 373
		UnityPost = 4
	}
}
