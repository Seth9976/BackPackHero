using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200015A RID: 346
	public interface IUnitDebugData : IGraphElementDebugData
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060008FE RID: 2302
		// (set) Token: 0x060008FF RID: 2303
		int lastInvokeFrame { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000900 RID: 2304
		// (set) Token: 0x06000901 RID: 2305
		float lastInvokeTime { get; set; }
	}
}
