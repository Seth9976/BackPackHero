using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EC RID: 236
	[Flags]
	public enum InputUpdateType
	{
		// Token: 0x040005A2 RID: 1442
		None = 0,
		// Token: 0x040005A3 RID: 1443
		Dynamic = 1,
		// Token: 0x040005A4 RID: 1444
		Fixed = 2,
		// Token: 0x040005A5 RID: 1445
		BeforeRender = 4,
		// Token: 0x040005A6 RID: 1446
		Editor = 8,
		// Token: 0x040005A7 RID: 1447
		Manual = 16,
		// Token: 0x040005A8 RID: 1448
		Default = 11
	}
}
