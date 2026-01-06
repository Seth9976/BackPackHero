using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007D RID: 125
	internal struct CursorPositionStylePainterParameters
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000B810 File Offset: 0x00009A10
		public unsafe static CursorPositionStylePainterParameters GetDefault(VisualElement ve, string text)
		{
			ComputedStyle computedStyle = *ve.computedStyle;
			return new CursorPositionStylePainterParameters
			{
				rect = ve.contentRect,
				text = text,
				font = TextUtilities.GetFont(ve),
				fontSize = (int)computedStyle.fontSize.value,
				fontStyle = computedStyle.unityFontStyleAndWeight,
				anchor = computedStyle.unityTextAlign,
				wordWrapWidth = ((computedStyle.whiteSpace == WhiteSpace.Normal) ? ve.contentRect.width : 0f),
				richText = false,
				cursorIndex = 0
			};
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		internal TextNativeSettings GetTextNativeSettings(float scaling)
		{
			return new TextNativeSettings
			{
				text = this.text,
				font = this.font,
				size = this.fontSize,
				scaling = scaling,
				style = this.fontStyle,
				color = Color.white,
				anchor = this.anchor,
				wordWrap = true,
				wordWrapWidth = this.wordWrapWidth,
				richText = this.richText
			};
		}

		// Token: 0x0400019B RID: 411
		public Rect rect;

		// Token: 0x0400019C RID: 412
		public string text;

		// Token: 0x0400019D RID: 413
		public Font font;

		// Token: 0x0400019E RID: 414
		public int fontSize;

		// Token: 0x0400019F RID: 415
		public FontStyle fontStyle;

		// Token: 0x040001A0 RID: 416
		public TextAnchor anchor;

		// Token: 0x040001A1 RID: 417
		public float wordWrapWidth;

		// Token: 0x040001A2 RID: 418
		public bool richText;

		// Token: 0x040001A3 RID: 419
		public int cursorIndex;
	}
}
