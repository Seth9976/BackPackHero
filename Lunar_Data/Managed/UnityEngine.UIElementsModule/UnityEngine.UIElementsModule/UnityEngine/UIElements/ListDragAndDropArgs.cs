using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AE RID: 430
	internal struct ListDragAndDropArgs : IListDragAndDropArgs
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00038877 File Offset: 0x00036A77
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0003887F File Offset: 0x00036A7F
		public object target { readonly get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00038888 File Offset: 0x00036A88
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00038890 File Offset: 0x00036A90
		public int insertAtIndex { readonly get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00038899 File Offset: 0x00036A99
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x000388A1 File Offset: 0x00036AA1
		public DragAndDropPosition dragAndDropPosition { readonly get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x000388AA File Offset: 0x00036AAA
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x000388B2 File Offset: 0x00036AB2
		public IDragAndDropData dragAndDropData { readonly get; set; }
	}
}
