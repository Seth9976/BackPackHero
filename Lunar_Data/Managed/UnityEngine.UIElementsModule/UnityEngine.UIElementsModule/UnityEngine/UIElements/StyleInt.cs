using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000284 RID: 644
	public struct StyleInt : IStyleValue<int>, IEquatable<StyleInt>
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0005A9E0 File Offset: 0x00058BE0
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0005AA03 File Offset: 0x00058C03
		public int value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0005AA14 File Offset: 0x00058C14
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0005AA2C File Offset: 0x00058C2C
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

		// Token: 0x060014FC RID: 5372 RVA: 0x0005AA36 File Offset: 0x00058C36
		public StyleInt(int v)
		{
			this = new StyleInt(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0005AA42 File Offset: 0x00058C42
		public StyleInt(StyleKeyword keyword)
		{
			this = new StyleInt(0, keyword);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0005AA4E File Offset: 0x00058C4E
		internal StyleInt(int v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0005AA60 File Offset: 0x00058C60
		public static bool operator ==(StyleInt lhs, StyleInt rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0005AA94 File Offset: 0x00058C94
		public static bool operator !=(StyleInt lhs, StyleInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0005AAB0 File Offset: 0x00058CB0
		public static implicit operator StyleInt(StyleKeyword keyword)
		{
			return new StyleInt(keyword);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0005AAC8 File Offset: 0x00058CC8
		public static implicit operator StyleInt(int v)
		{
			return new StyleInt(v);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		public bool Equals(StyleInt other)
		{
			return other == this;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0005AB00 File Offset: 0x00058D00
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleInt)
			{
				StyleInt styleInt = (StyleInt)obj;
				flag = this.Equals(styleInt);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0005AB2C File Offset: 0x00058D2C
		public override int GetHashCode()
		{
			return (this.m_Value * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0005AB54 File Offset: 0x00058D54
		public override string ToString()
		{
			return this.DebugString<int>();
		}

		// Token: 0x0400090E RID: 2318
		private int m_Value;

		// Token: 0x0400090F RID: 2319
		private StyleKeyword m_Keyword;
	}
}
