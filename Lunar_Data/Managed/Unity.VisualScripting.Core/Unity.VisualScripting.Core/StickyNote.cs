using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000140 RID: 320
	public class StickyNote : GraphElement<IGraph>
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000262D7 File Offset: 0x000244D7
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x000262DF File Offset: 0x000244DF
		[Serialize]
		public Rect position { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000262E8 File Offset: 0x000244E8
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000262F0 File Offset: 0x000244F0
		[Serialize]
		public string title { get; set; } = "Sticky Note";

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000262F9 File Offset: 0x000244F9
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00026301 File Offset: 0x00024501
		[Serialize]
		[InspectorTextArea(minLines = 1f)]
		public string body { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0002630A File Offset: 0x0002450A
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00026312 File Offset: 0x00024512
		[Serialize]
		[Inspectable]
		public StickyNote.ColorEnum colorTheme { get; set; }

		// Token: 0x060008AC RID: 2220 RVA: 0x0002631C File Offset: 0x0002451C
		public static Color GetStickyColor(StickyNote.ColorEnum enumValue)
		{
			switch (enumValue)
			{
			case StickyNote.ColorEnum.Black:
				return new Color(0.122f, 0.114f, 0.09f);
			case StickyNote.ColorEnum.Dark:
				return new Color(0.184f, 0.145f, 0.024f);
			case StickyNote.ColorEnum.Orange:
				return new Color(0.988f, 0.663f, 0.275f);
			case StickyNote.ColorEnum.Green:
				return new Color(0.376f, 0.886f, 0.655f);
			case StickyNote.ColorEnum.Blue:
				return new Color(0.518f, 0.725f, 0.855f);
			case StickyNote.ColorEnum.Red:
				return new Color(1f, 0.502f, 0.502f);
			case StickyNote.ColorEnum.Purple:
				return new Color(0.98f, 0.769f, 0.949f);
			case StickyNote.ColorEnum.Teal:
				return new Color(0.475f, 0.878f, 0.89f);
			default:
				return new Color(0.969f, 0.91f, 0.624f);
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00026412 File Offset: 0x00024612
		public static Color GetFontColor(StickyNote.ColorEnum enumValue)
		{
			if (enumValue - StickyNote.ColorEnum.Black <= 1)
			{
				return Color.white;
			}
			return Color.black;
		}

		// Token: 0x04000211 RID: 529
		[DoNotSerialize]
		public static readonly Color defaultColor = new Color(0.969f, 0.91f, 0.624f);

		// Token: 0x02000201 RID: 513
		public enum ColorEnum
		{
			// Token: 0x0400097A RID: 2426
			Classic,
			// Token: 0x0400097B RID: 2427
			Black,
			// Token: 0x0400097C RID: 2428
			Dark,
			// Token: 0x0400097D RID: 2429
			Orange,
			// Token: 0x0400097E RID: 2430
			Green,
			// Token: 0x0400097F RID: 2431
			Blue,
			// Token: 0x04000980 RID: 2432
			Red,
			// Token: 0x04000981 RID: 2433
			Purple,
			// Token: 0x04000982 RID: 2434
			Teal
		}
	}
}
