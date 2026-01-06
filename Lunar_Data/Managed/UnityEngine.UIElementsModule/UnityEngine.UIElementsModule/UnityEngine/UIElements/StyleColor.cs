using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027B RID: 635
	public struct StyleColor : IStyleValue<Color>, IEquatable<StyleColor>
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00057810 File Offset: 0x00055A10
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00057837 File Offset: 0x00055A37
		public Color value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : Color.clear;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00057848 File Offset: 0x00055A48
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00057860 File Offset: 0x00055A60
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

		// Token: 0x06001474 RID: 5236 RVA: 0x0005786A File Offset: 0x00055A6A
		public StyleColor(Color v)
		{
			this = new StyleColor(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00057876 File Offset: 0x00055A76
		public StyleColor(StyleKeyword keyword)
		{
			this = new StyleColor(Color.clear, keyword);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00057886 File Offset: 0x00055A86
		internal StyleColor(Color v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00057898 File Offset: 0x00055A98
		public static bool operator ==(StyleColor lhs, StyleColor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x000578CC File Offset: 0x00055ACC
		public static bool operator !=(StyleColor lhs, StyleColor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000578E8 File Offset: 0x00055AE8
		public static bool operator ==(StyleColor lhs, Color rhs)
		{
			StyleColor styleColor = new StyleColor(rhs);
			return lhs == styleColor;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0005790C File Offset: 0x00055B0C
		public static bool operator !=(StyleColor lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00057928 File Offset: 0x00055B28
		public static implicit operator StyleColor(StyleKeyword keyword)
		{
			return new StyleColor(keyword);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00057940 File Offset: 0x00055B40
		public static implicit operator StyleColor(Color v)
		{
			return new StyleColor(v);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00057958 File Offset: 0x00055B58
		public bool Equals(StyleColor other)
		{
			return other == this;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00057978 File Offset: 0x00055B78
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleColor)
			{
				StyleColor styleColor = (StyleColor)obj;
				flag = this.Equals(styleColor);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000579A4 File Offset: 0x00055BA4
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000579D8 File Offset: 0x00055BD8
		public override string ToString()
		{
			return this.DebugString<Color>();
		}

		// Token: 0x040008F9 RID: 2297
		private Color m_Value;

		// Token: 0x040008FA RID: 2298
		private StyleKeyword m_Keyword;
	}
}
