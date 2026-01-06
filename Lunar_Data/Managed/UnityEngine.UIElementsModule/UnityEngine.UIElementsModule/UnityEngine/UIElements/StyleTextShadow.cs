using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028A RID: 650
	public struct StyleTextShadow : IStyleValue<TextShadow>, IEquatable<StyleTextShadow>
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0005B51C File Offset: 0x0005971C
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0005B547 File Offset: 0x00059747
		public TextShadow value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(TextShadow);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0005B558 File Offset: 0x00059758
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0005B570 File Offset: 0x00059770
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

		// Token: 0x06001558 RID: 5464 RVA: 0x0005B57A File Offset: 0x0005977A
		public StyleTextShadow(TextShadow v)
		{
			this = new StyleTextShadow(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005B588 File Offset: 0x00059788
		public StyleTextShadow(StyleKeyword keyword)
		{
			this = new StyleTextShadow(default(TextShadow), keyword);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005B5A7 File Offset: 0x000597A7
		internal StyleTextShadow(TextShadow v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0005B5B8 File Offset: 0x000597B8
		public static bool operator ==(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0005B5EC File Offset: 0x000597EC
		public static bool operator !=(StyleTextShadow lhs, StyleTextShadow rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005B608 File Offset: 0x00059808
		public static implicit operator StyleTextShadow(StyleKeyword keyword)
		{
			return new StyleTextShadow(keyword);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0005B620 File Offset: 0x00059820
		public static implicit operator StyleTextShadow(TextShadow v)
		{
			return new StyleTextShadow(v);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0005B638 File Offset: 0x00059838
		public bool Equals(StyleTextShadow other)
		{
			return other == this;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0005B658 File Offset: 0x00059858
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleTextShadow);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleTextShadow styleTextShadow = (StyleTextShadow)obj;
				flag2 = styleTextShadow == this;
			}
			return flag2;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0005B694 File Offset: 0x00059894
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0005B6E4 File Offset: 0x000598E4
		public override string ToString()
		{
			return this.DebugString<TextShadow>();
		}

		// Token: 0x0400091A RID: 2330
		private StyleKeyword m_Keyword;

		// Token: 0x0400091B RID: 2331
		private TextShadow m_Value;
	}
}
