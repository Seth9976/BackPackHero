using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000282 RID: 642
	public struct StyleFont : IStyleValue<Font>, IEquatable<StyleFont>
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0005A5E8 File Offset: 0x000587E8
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0005A60B File Offset: 0x0005880B
		public Font value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0005A61C File Offset: 0x0005881C
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x0005A634 File Offset: 0x00058834
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

		// Token: 0x060014D7 RID: 5335 RVA: 0x0005A63E File Offset: 0x0005883E
		public StyleFont(Font v)
		{
			this = new StyleFont(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0005A64A File Offset: 0x0005884A
		public StyleFont(StyleKeyword keyword)
		{
			this = new StyleFont(null, keyword);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0005A656 File Offset: 0x00058856
		internal StyleFont(Font v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0005A668 File Offset: 0x00058868
		public static bool operator ==(StyleFont lhs, StyleFont rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0005A69C File Offset: 0x0005889C
		public static bool operator !=(StyleFont lhs, StyleFont rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0005A6B8 File Offset: 0x000588B8
		public static implicit operator StyleFont(StyleKeyword keyword)
		{
			return new StyleFont(keyword);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0005A6D0 File Offset: 0x000588D0
		public static implicit operator StyleFont(Font v)
		{
			return new StyleFont(v);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005A6E8 File Offset: 0x000588E8
		public bool Equals(StyleFont other)
		{
			return other == this;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0005A708 File Offset: 0x00058908
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFont)
			{
				StyleFont styleFont = (StyleFont)obj;
				flag = this.Equals(styleFont);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0005A734 File Offset: 0x00058934
		public override int GetHashCode()
		{
			return (((this.m_Value != null) ? this.m_Value.GetHashCode() : 0) * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005A770 File Offset: 0x00058970
		public override string ToString()
		{
			return this.DebugString<Font>();
		}

		// Token: 0x0400090A RID: 2314
		private Font m_Value;

		// Token: 0x0400090B RID: 2315
		private StyleKeyword m_Keyword;
	}
}
