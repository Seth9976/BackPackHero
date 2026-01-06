using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000079 RID: 121
	[SerializationVersion("A", new Type[] { })]
	public sealed class GraphGroup : GraphElement<IGraph>
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00009258 File Offset: 0x00007458
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00009260 File Offset: 0x00007460
		[Serialize]
		public Rect position { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00009269 File Offset: 0x00007469
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00009271 File Offset: 0x00007471
		[Serialize]
		public string label { get; set; } = "Group";

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000927A File Offset: 0x0000747A
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00009282 File Offset: 0x00007482
		[Serialize]
		[InspectorTextArea(minLines = 1f, maxLines = 10f)]
		public string comment { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000928B File Offset: 0x0000748B
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00009293 File Offset: 0x00007493
		[Serialize]
		[Inspectable]
		public Color color { get; set; } = GraphGroup.defaultColor;

		// Token: 0x040000E9 RID: 233
		[DoNotSerialize]
		public static readonly Color defaultColor = new Color(0f, 0f, 0f);
	}
}
