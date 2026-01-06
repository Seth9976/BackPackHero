using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000055 RID: 85
	internal class RepaintData
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008B54 File Offset: 0x00006D54
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00008B5C File Offset: 0x00006D5C
		public Matrix4x4 currentOffset { get; set; } = Matrix4x4.identity;

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008B65 File Offset: 0x00006D65
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00008B6D File Offset: 0x00006D6D
		public Vector2 mousePosition { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008B76 File Offset: 0x00006D76
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00008B7E File Offset: 0x00006D7E
		public Rect currentWorldClip { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008B87 File Offset: 0x00006D87
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00008B8F File Offset: 0x00006D8F
		public Event repaintEvent { get; set; }
	}
}
