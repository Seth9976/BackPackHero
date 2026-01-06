using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AD RID: 429
	internal interface IListDragAndDropArgs
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000DDB RID: 3547
		object target { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000DDC RID: 3548
		int insertAtIndex { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000DDD RID: 3549
		IDragAndDropData dragAndDropData { get; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000DDE RID: 3550
		DragAndDropPosition dragAndDropPosition { get; }
	}
}
