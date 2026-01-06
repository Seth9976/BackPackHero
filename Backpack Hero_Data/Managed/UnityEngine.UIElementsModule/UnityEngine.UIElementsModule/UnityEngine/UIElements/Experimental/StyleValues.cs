using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000380 RID: 896
	public struct StyleValues
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0008545C File Offset: 0x0008365C
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x00085486 File Offset: 0x00083686
		public float top
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Top).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Top, value);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00085498 File Offset: 0x00083698
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x000854C2 File Offset: 0x000836C2
		public float left
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Left).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Left, value);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000854D4 File Offset: 0x000836D4
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x000854FE File Offset: 0x000836FE
		public float width
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Width).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Width, value);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00085510 File Offset: 0x00083710
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0008553A File Offset: 0x0008373A
		public float height
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Height).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Height, value);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0008554C File Offset: 0x0008374C
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x00085576 File Offset: 0x00083776
		public float right
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Right).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Right, value);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00085588 File Offset: 0x00083788
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x000855B2 File Offset: 0x000837B2
		public float bottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Bottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Bottom, value);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x000855C4 File Offset: 0x000837C4
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x000855EE File Offset: 0x000837EE
		public Color color
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.Color).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Color, value);
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00085600 File Offset: 0x00083800
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x0008562A File Offset: 0x0008382A
		public Color backgroundColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BackgroundColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BackgroundColor, value);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0008563C File Offset: 0x0008383C
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x00085666 File Offset: 0x00083866
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.UnityBackgroundImageTintColor, value);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00085678 File Offset: 0x00083878
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x000856A2 File Offset: 0x000838A2
		public Color borderColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BorderColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderColor, value);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000856B4 File Offset: 0x000838B4
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x000856DE File Offset: 0x000838DE
		public float marginLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginLeft, value);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x000856F0 File Offset: 0x000838F0
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0008571A File Offset: 0x0008391A
		public float marginTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginTop, value);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0008572C File Offset: 0x0008392C
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x00085756 File Offset: 0x00083956
		public float marginRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginRight, value);
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00085768 File Offset: 0x00083968
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x00085792 File Offset: 0x00083992
		public float marginBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginBottom, value);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000857A4 File Offset: 0x000839A4
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x000857CE File Offset: 0x000839CE
		public float paddingLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingLeft, value);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x000857E0 File Offset: 0x000839E0
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x0008580A File Offset: 0x00083A0A
		public float paddingTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingTop, value);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0008581C File Offset: 0x00083A1C
		// (set) Token: 0x06001C9D RID: 7325 RVA: 0x00085846 File Offset: 0x00083A46
		public float paddingRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingRight, value);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00085858 File Offset: 0x00083A58
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x00085882 File Offset: 0x00083A82
		public float paddingBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingBottom, value);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00085894 File Offset: 0x00083A94
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x000858BE File Offset: 0x00083ABE
		public float borderLeftWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderLeftWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderLeftWidth, value);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x000858D0 File Offset: 0x00083AD0
		// (set) Token: 0x06001CA3 RID: 7331 RVA: 0x000858FA File Offset: 0x00083AFA
		public float borderRightWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderRightWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderRightWidth, value);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0008590C File Offset: 0x00083B0C
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x00085936 File Offset: 0x00083B36
		public float borderTopWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopWidth, value);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x00085948 File Offset: 0x00083B48
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x00085972 File Offset: 0x00083B72
		public float borderBottomWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomWidth, value);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00085984 File Offset: 0x00083B84
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x000859AE File Offset: 0x00083BAE
		public float borderTopLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopLeftRadius, value);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x000859C0 File Offset: 0x00083BC0
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x000859EA File Offset: 0x00083BEA
		public float borderTopRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopRightRadius, value);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000859FC File Offset: 0x00083BFC
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x00085A26 File Offset: 0x00083C26
		public float borderBottomLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomLeftRadius, value);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00085A38 File Offset: 0x00083C38
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x00085A62 File Offset: 0x00083C62
		public float borderBottomRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomRightRadius, value);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00085A74 File Offset: 0x00083C74
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x00085A9E File Offset: 0x00083C9E
		public float opacity
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Opacity).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Opacity, value);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00085AB0 File Offset: 0x00083CB0
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x00085ADA File Offset: 0x00083CDA
		public float flexGrow
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexGrow).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x00085AEC File Offset: 0x00083CEC
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x00085ADA File Offset: 0x00083CDA
		public float flexShrink
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexShrink).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00085B18 File Offset: 0x00083D18
		internal void SetValue(StylePropertyId id, float value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.number = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00085B4C File Offset: 0x00083D4C
		internal void SetValue(StylePropertyId id, Color value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.color = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00085B80 File Offset: 0x00083D80
		internal StyleValueCollection Values()
		{
			bool flag = this.m_StyleValues == null;
			if (flag)
			{
				this.m_StyleValues = new StyleValueCollection();
			}
			return this.m_StyleValues;
		}

		// Token: 0x04000E5A RID: 3674
		internal StyleValueCollection m_StyleValues;
	}
}
