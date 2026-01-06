using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000288 RID: 648
	public struct StyleScale : IStyleValue<Scale>, IEquatable<StyleScale>
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0005B19C File Offset: 0x0005939C
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0005B1C7 File Offset: 0x000593C7
		public Scale value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Scale);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0005B1D8 File Offset: 0x000593D8
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0005B1F0 File Offset: 0x000593F0
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

		// Token: 0x0600153A RID: 5434 RVA: 0x0005B1FA File Offset: 0x000593FA
		public StyleScale(Scale v)
		{
			this = new StyleScale(v, StyleKeyword.Undefined);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0005B208 File Offset: 0x00059408
		public StyleScale(StyleKeyword keyword)
		{
			this = new StyleScale(default(Scale), keyword);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005B227 File Offset: 0x00059427
		internal StyleScale(Scale v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0005B238 File Offset: 0x00059438
		public static bool operator ==(StyleScale lhs, StyleScale rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0005B26C File Offset: 0x0005946C
		public static bool operator !=(StyleScale lhs, StyleScale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0005B288 File Offset: 0x00059488
		public static implicit operator StyleScale(StyleKeyword keyword)
		{
			return new StyleScale(keyword);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005B2A0 File Offset: 0x000594A0
		public static implicit operator StyleScale(Scale v)
		{
			return new StyleScale(v);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0005B2B8 File Offset: 0x000594B8
		public bool Equals(StyleScale other)
		{
			return other == this;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005B2D8 File Offset: 0x000594D8
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleScale)
			{
				StyleScale styleScale = (StyleScale)obj;
				flag = this.Equals(styleScale);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005B304 File Offset: 0x00059504
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005B338 File Offset: 0x00059538
		public override string ToString()
		{
			return this.DebugString<Scale>();
		}

		// Token: 0x04000916 RID: 2326
		private Scale m_Value;

		// Token: 0x04000917 RID: 2327
		private StyleKeyword m_Keyword;
	}
}
