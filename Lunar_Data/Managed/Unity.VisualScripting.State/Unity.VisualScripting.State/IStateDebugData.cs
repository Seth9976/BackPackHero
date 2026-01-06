using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000009 RID: 9
	public interface IStateDebugData : IGraphElementDebugData
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44
		int lastEnterFrame { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002D RID: 45
		float lastExitTime { get; }
	}
}
