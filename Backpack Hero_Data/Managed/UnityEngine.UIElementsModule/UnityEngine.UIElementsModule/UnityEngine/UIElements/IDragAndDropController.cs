using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AA RID: 426
	internal interface IDragAndDropController<in TArgs>
	{
		// Token: 0x06000DCE RID: 3534
		bool CanStartDrag(IEnumerable<int> itemIndices);

		// Token: 0x06000DCF RID: 3535
		StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIndices, bool skipText = false);

		// Token: 0x06000DD0 RID: 3536
		DragVisualMode HandleDragAndDrop(TArgs args);

		// Token: 0x06000DD1 RID: 3537
		void OnDrop(TArgs args);
	}
}
