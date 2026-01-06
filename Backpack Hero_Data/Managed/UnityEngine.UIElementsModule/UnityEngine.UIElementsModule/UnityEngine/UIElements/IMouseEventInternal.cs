using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F0 RID: 496
	internal interface IMouseEventInternal
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F4D RID: 3917
		// (set) Token: 0x06000F4E RID: 3918
		bool triggeredByOS { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F4F RID: 3919
		// (set) Token: 0x06000F50 RID: 3920
		bool recomputeTopElementUnderMouse { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F51 RID: 3921
		// (set) Token: 0x06000F52 RID: 3922
		IPointerEvent sourcePointerEvent { get; set; }
	}
}
