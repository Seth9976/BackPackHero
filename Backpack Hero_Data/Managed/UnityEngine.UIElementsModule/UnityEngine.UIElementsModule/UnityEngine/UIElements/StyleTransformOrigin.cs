using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028B RID: 651
	public struct StyleTransformOrigin : IStyleValue<TransformOrigin>, IEquatable<StyleTransformOrigin>
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0005B708 File Offset: 0x00059908
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x0005B733 File Offset: 0x00059933
		public TransformOrigin value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(TransformOrigin);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0005B744 File Offset: 0x00059944
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x0005B75C File Offset: 0x0005995C
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

		// Token: 0x06001567 RID: 5479 RVA: 0x0005B766 File Offset: 0x00059966
		public StyleTransformOrigin(TransformOrigin v)
		{
			this = new StyleTransformOrigin(v, StyleKeyword.Undefined);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0005B774 File Offset: 0x00059974
		public StyleTransformOrigin(StyleKeyword keyword)
		{
			this = new StyleTransformOrigin(default(TransformOrigin), keyword);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0005B793 File Offset: 0x00059993
		internal StyleTransformOrigin(TransformOrigin v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0005B7A4 File Offset: 0x000599A4
		public static bool operator ==(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0005B7D8 File Offset: 0x000599D8
		public static bool operator !=(StyleTransformOrigin lhs, StyleTransformOrigin rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0005B7F4 File Offset: 0x000599F4
		public static implicit operator StyleTransformOrigin(StyleKeyword keyword)
		{
			return new StyleTransformOrigin(keyword);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005B80C File Offset: 0x00059A0C
		public static implicit operator StyleTransformOrigin(TransformOrigin v)
		{
			return new StyleTransformOrigin(v);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005B824 File Offset: 0x00059A24
		public bool Equals(StyleTransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005B844 File Offset: 0x00059A44
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleTransformOrigin)
			{
				StyleTransformOrigin styleTransformOrigin = (StyleTransformOrigin)obj;
				flag = this.Equals(styleTransformOrigin);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0005B870 File Offset: 0x00059A70
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0005B8A4 File Offset: 0x00059AA4
		public override string ToString()
		{
			return this.DebugString<TransformOrigin>();
		}

		// Token: 0x0400091C RID: 2332
		private TransformOrigin m_Value;

		// Token: 0x0400091D RID: 2333
		private StyleKeyword m_Keyword;
	}
}
