using System;

namespace Pathfinding
{
	// Token: 0x0200003C RID: 60
	public enum PathState
	{
		// Token: 0x04000198 RID: 408
		Created,
		// Token: 0x04000199 RID: 409
		PathQueue,
		// Token: 0x0400019A RID: 410
		Processing,
		// Token: 0x0400019B RID: 411
		ReturnQueue,
		// Token: 0x0400019C RID: 412
		Returning,
		// Token: 0x0400019D RID: 413
		Returned
	}
}
