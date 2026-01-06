using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000B RID: 11
	public interface IStateTransitionDebugData : IGraphElementDebugData
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49
		int lastBranchFrame { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50
		float lastBranchTime { get; }
	}
}
