using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027C RID: 636
	public struct StyleCursor : IStyleValue<Cursor>, IEquatable<StyleCursor>
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x000579FC File Offset: 0x00055BFC
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x00057A27 File Offset: 0x00055C27
		public Cursor value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Cursor);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00057A38 File Offset: 0x00055C38
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00057A50 File Offset: 0x00055C50
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

		// Token: 0x06001485 RID: 5253 RVA: 0x00057A5A File Offset: 0x00055C5A
		public StyleCursor(Cursor v)
		{
			this = new StyleCursor(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00057A68 File Offset: 0x00055C68
		public StyleCursor(StyleKeyword keyword)
		{
			this = new StyleCursor(default(Cursor), keyword);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00057A87 File Offset: 0x00055C87
		internal StyleCursor(Cursor v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00057A98 File Offset: 0x00055C98
		public static bool operator ==(StyleCursor lhs, StyleCursor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00057ACC File Offset: 0x00055CCC
		public static bool operator !=(StyleCursor lhs, StyleCursor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00057AE8 File Offset: 0x00055CE8
		public static implicit operator StyleCursor(StyleKeyword keyword)
		{
			return new StyleCursor(keyword);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00057B00 File Offset: 0x00055D00
		public static implicit operator StyleCursor(Cursor v)
		{
			return new StyleCursor(v);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00057B18 File Offset: 0x00055D18
		public bool Equals(StyleCursor other)
		{
			return other == this;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00057B38 File Offset: 0x00055D38
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleCursor)
			{
				StyleCursor styleCursor = (StyleCursor)obj;
				flag = this.Equals(styleCursor);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00057B64 File Offset: 0x00055D64
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00057B98 File Offset: 0x00055D98
		public override string ToString()
		{
			return this.DebugString<Cursor>();
		}

		// Token: 0x040008FB RID: 2299
		private Cursor m_Value;

		// Token: 0x040008FC RID: 2300
		private StyleKeyword m_Keyword;
	}
}
