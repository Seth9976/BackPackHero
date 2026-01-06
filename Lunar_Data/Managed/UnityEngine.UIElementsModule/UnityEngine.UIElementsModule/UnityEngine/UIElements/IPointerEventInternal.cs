using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000215 RID: 533
	internal interface IPointerEventInternal
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600100E RID: 4110
		// (set) Token: 0x0600100F RID: 4111
		bool triggeredByOS { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001010 RID: 4112
		// (set) Token: 0x06001011 RID: 4113
		bool recomputeTopElementUnderPointer { get; set; }
	}
}
