using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DE RID: 222
	[Flags]
	internal enum VisualElementFlags
	{
		// Token: 0x040002DE RID: 734
		WorldTransformDirty = 1,
		// Token: 0x040002DF RID: 735
		WorldTransformInverseDirty = 2,
		// Token: 0x040002E0 RID: 736
		WorldClipDirty = 4,
		// Token: 0x040002E1 RID: 737
		BoundingBoxDirty = 8,
		// Token: 0x040002E2 RID: 738
		WorldBoundingBoxDirty = 16,
		// Token: 0x040002E3 RID: 739
		LayoutManual = 32,
		// Token: 0x040002E4 RID: 740
		CompositeRoot = 64,
		// Token: 0x040002E5 RID: 741
		RequireMeasureFunction = 128,
		// Token: 0x040002E6 RID: 742
		EnableViewDataPersistence = 256,
		// Token: 0x040002E7 RID: 743
		DisableClipping = 512,
		// Token: 0x040002E8 RID: 744
		NeedsAttachToPanelEvent = 1024,
		// Token: 0x040002E9 RID: 745
		HierarchyDisplayed = 2048,
		// Token: 0x040002EA RID: 746
		StyleInitialized = 4096,
		// Token: 0x040002EB RID: 747
		Init = 2079
	}
}
