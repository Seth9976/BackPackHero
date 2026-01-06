using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A8 RID: 424
	internal interface IDragAndDrop
	{
		// Token: 0x06000DC7 RID: 3527
		void StartDrag(StartDragArgs args);

		// Token: 0x06000DC8 RID: 3528
		void AcceptDrag();

		// Token: 0x06000DC9 RID: 3529
		void SetVisualMode(DragVisualMode visualMode);

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000DCA RID: 3530
		IDragAndDropData data { get; }
	}
}
