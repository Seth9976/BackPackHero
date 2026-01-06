using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000296 RID: 662
	internal struct InheritedData : IStyleDataGroup<InheritedData>, IEquatable<InheritedData>
	{
		// Token: 0x0600168F RID: 5775 RVA: 0x0005C0E4 File Offset: 0x0005A2E4
		public InheritedData Copy()
		{
			return this;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005C0FC File Offset: 0x0005A2FC
		public void CopyFrom(ref InheritedData other)
		{
			this = other;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005C10C File Offset: 0x0005A30C
		public static bool operator ==(InheritedData lhs, InheritedData rhs)
		{
			return lhs.color == rhs.color && lhs.fontSize == rhs.fontSize && lhs.letterSpacing == rhs.letterSpacing && lhs.textShadow == rhs.textShadow && lhs.unityFont == rhs.unityFont && lhs.unityFontDefinition == rhs.unityFontDefinition && lhs.unityFontStyleAndWeight == rhs.unityFontStyleAndWeight && lhs.unityParagraphSpacing == rhs.unityParagraphSpacing && lhs.unityTextAlign == rhs.unityTextAlign && lhs.unityTextOutlineColor == rhs.unityTextOutlineColor && lhs.unityTextOutlineWidth == rhs.unityTextOutlineWidth && lhs.visibility == rhs.visibility && lhs.whiteSpace == rhs.whiteSpace && lhs.wordSpacing == rhs.wordSpacing;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0005C220 File Offset: 0x0005A420
		public static bool operator !=(InheritedData lhs, InheritedData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005C23C File Offset: 0x0005A43C
		public bool Equals(InheritedData other)
		{
			return other == this;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005C25C File Offset: 0x0005A45C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is InheritedData && this.Equals((InheritedData)obj);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0005C294 File Offset: 0x0005A494
		public override int GetHashCode()
		{
			int num = this.color.GetHashCode();
			num = (num * 397) ^ this.fontSize.GetHashCode();
			num = (num * 397) ^ this.letterSpacing.GetHashCode();
			num = (num * 397) ^ this.textShadow.GetHashCode();
			num = (num * 397) ^ ((this.unityFont == null) ? 0 : this.unityFont.GetHashCode());
			num = (num * 397) ^ this.unityFontDefinition.GetHashCode();
			num = (num * 397) ^ (int)this.unityFontStyleAndWeight;
			num = (num * 397) ^ this.unityParagraphSpacing.GetHashCode();
			num = (num * 397) ^ (int)this.unityTextAlign;
			num = (num * 397) ^ this.unityTextOutlineColor.GetHashCode();
			num = (num * 397) ^ this.unityTextOutlineWidth.GetHashCode();
			num = (num * 397) ^ (int)this.visibility;
			num = (num * 397) ^ (int)this.whiteSpace;
			return (num * 397) ^ this.wordSpacing.GetHashCode();
		}

		// Token: 0x04000930 RID: 2352
		public Color color;

		// Token: 0x04000931 RID: 2353
		public Length fontSize;

		// Token: 0x04000932 RID: 2354
		public Length letterSpacing;

		// Token: 0x04000933 RID: 2355
		public TextShadow textShadow;

		// Token: 0x04000934 RID: 2356
		public Font unityFont;

		// Token: 0x04000935 RID: 2357
		public FontDefinition unityFontDefinition;

		// Token: 0x04000936 RID: 2358
		public FontStyle unityFontStyleAndWeight;

		// Token: 0x04000937 RID: 2359
		public Length unityParagraphSpacing;

		// Token: 0x04000938 RID: 2360
		public TextAnchor unityTextAlign;

		// Token: 0x04000939 RID: 2361
		public Color unityTextOutlineColor;

		// Token: 0x0400093A RID: 2362
		public float unityTextOutlineWidth;

		// Token: 0x0400093B RID: 2363
		public Visibility visibility;

		// Token: 0x0400093C RID: 2364
		public WhiteSpace whiteSpace;

		// Token: 0x0400093D RID: 2365
		public Length wordSpacing;
	}
}
