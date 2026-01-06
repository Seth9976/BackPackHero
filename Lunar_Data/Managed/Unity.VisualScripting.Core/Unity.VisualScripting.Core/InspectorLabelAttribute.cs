using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorLabelAttribute : Attribute
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00004D50 File Offset: 0x00002F50
		public InspectorLabelAttribute(string text)
		{
			this.text = text;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004D5F File Offset: 0x00002F5F
		public InspectorLabelAttribute(string text, string tooltip)
		{
			this.text = text;
			this.tooltip = tooltip;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004D75 File Offset: 0x00002F75
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00004D7D File Offset: 0x00002F7D
		public string text { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004D86 File Offset: 0x00002F86
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00004D8E File Offset: 0x00002F8E
		public string tooltip { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00004D97 File Offset: 0x00002F97
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00004D9F File Offset: 0x00002F9F
		public Texture image { get; set; }
	}
}
