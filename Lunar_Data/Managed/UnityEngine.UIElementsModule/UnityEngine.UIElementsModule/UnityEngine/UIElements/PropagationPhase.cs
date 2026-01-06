using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E1 RID: 481
	public enum PropagationPhase
	{
		// Token: 0x040006C9 RID: 1737
		None,
		// Token: 0x040006CA RID: 1738
		TrickleDown,
		// Token: 0x040006CB RID: 1739
		AtTarget,
		// Token: 0x040006CC RID: 1740
		DefaultActionAtTarget = 5,
		// Token: 0x040006CD RID: 1741
		BubbleUp = 3,
		// Token: 0x040006CE RID: 1742
		DefaultAction
	}
}
