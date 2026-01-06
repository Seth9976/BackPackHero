using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000289 RID: 649
	public struct StyleTranslate : IStyleValue<Translate>, IEquatable<StyleTranslate>
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0005B35C File Offset: 0x0005955C
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x0005B387 File Offset: 0x00059587
		public Translate value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Translate);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0005B398 File Offset: 0x00059598
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x0005B3B0 File Offset: 0x000595B0
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

		// Token: 0x06001549 RID: 5449 RVA: 0x0005B3BA File Offset: 0x000595BA
		public StyleTranslate(Translate v)
		{
			this = new StyleTranslate(v, StyleKeyword.Undefined);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005B3C8 File Offset: 0x000595C8
		public StyleTranslate(StyleKeyword keyword)
		{
			this = new StyleTranslate(default(Translate), keyword);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005B3E7 File Offset: 0x000595E7
		internal StyleTranslate(Translate v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0005B3F8 File Offset: 0x000595F8
		public static bool operator ==(StyleTranslate lhs, StyleTranslate rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005B42C File Offset: 0x0005962C
		public static bool operator !=(StyleTranslate lhs, StyleTranslate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0005B448 File Offset: 0x00059648
		public static implicit operator StyleTranslate(StyleKeyword keyword)
		{
			return new StyleTranslate(keyword);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0005B460 File Offset: 0x00059660
		public static implicit operator StyleTranslate(Translate v)
		{
			return new StyleTranslate(v);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0005B478 File Offset: 0x00059678
		public bool Equals(StyleTranslate other)
		{
			return other == this;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0005B498 File Offset: 0x00059698
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleTranslate)
			{
				StyleTranslate styleTranslate = (StyleTranslate)obj;
				flag = this.Equals(styleTranslate);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0005B4C4 File Offset: 0x000596C4
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0005B4F8 File Offset: 0x000596F8
		public override string ToString()
		{
			return this.DebugString<Translate>();
		}

		// Token: 0x04000918 RID: 2328
		private Translate m_Value;

		// Token: 0x04000919 RID: 2329
		private StyleKeyword m_Keyword;
	}
}
