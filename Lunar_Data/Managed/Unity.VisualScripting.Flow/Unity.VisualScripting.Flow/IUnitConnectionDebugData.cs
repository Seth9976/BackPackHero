using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000005 RID: 5
	public interface IUnitConnectionDebugData : IGraphElementDebugData
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32
		// (set) Token: 0x06000021 RID: 33
		int lastInvokeFrame { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34
		// (set) Token: 0x06000023 RID: 35
		float lastInvokeTime { get; set; }
	}
}
