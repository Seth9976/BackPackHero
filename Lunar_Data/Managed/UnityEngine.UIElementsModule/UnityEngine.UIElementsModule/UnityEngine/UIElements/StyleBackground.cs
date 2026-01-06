using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027A RID: 634
	public struct StyleBackground : IStyleValue<Background>, IEquatable<StyleBackground>
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x000575E0 File Offset: 0x000557E0
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0005760B File Offset: 0x0005580B
		public Background value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Background);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0005761C File Offset: 0x0005581C
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00057634 File Offset: 0x00055834
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0005763E File Offset: 0x0005583E
		public StyleBackground(Background v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0005764A File Offset: 0x0005584A
		public StyleBackground(Texture2D v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00057656 File Offset: 0x00055856
		public StyleBackground(Sprite v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00057662 File Offset: 0x00055862
		public StyleBackground(VectorImage v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00057670 File Offset: 0x00055870
		public StyleBackground(StyleKeyword keyword)
		{
			this = new StyleBackground(default(Background), keyword);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0005768F File Offset: 0x0005588F
		internal StyleBackground(Texture2D v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromTexture2D(v), keyword);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000576A0 File Offset: 0x000558A0
		internal StyleBackground(Sprite v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromSprite(v), keyword);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000576B1 File Offset: 0x000558B1
		internal StyleBackground(VectorImage v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromVectorImage(v), keyword);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000576C2 File Offset: 0x000558C2
		internal StyleBackground(Background v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000576D4 File Offset: 0x000558D4
		public static bool operator ==(StyleBackground lhs, StyleBackground rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00057708 File Offset: 0x00055908
		public static bool operator !=(StyleBackground lhs, StyleBackground rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00057724 File Offset: 0x00055924
		public static implicit operator StyleBackground(StyleKeyword keyword)
		{
			return new StyleBackground(keyword);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0005773C File Offset: 0x0005593C
		public static implicit operator StyleBackground(Background v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00057754 File Offset: 0x00055954
		public static implicit operator StyleBackground(Texture2D v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0005776C File Offset: 0x0005596C
		public bool Equals(StyleBackground other)
		{
			return other == this;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005778C File Offset: 0x0005598C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleBackground)
			{
				StyleBackground styleBackground = (StyleBackground)obj;
				flag = this.Equals(styleBackground);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000577B8 File Offset: 0x000559B8
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x000577EC File Offset: 0x000559EC
		public override string ToString()
		{
			return this.DebugString<Background>();
		}

		// Token: 0x040008F7 RID: 2295
		private Background m_Value;

		// Token: 0x040008F8 RID: 2296
		private StyleKeyword m_Keyword;
	}
}
