using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000281 RID: 641
	public struct StyleFloat : IStyleValue<float>, IEquatable<StyleFloat>
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0005A444 File Offset: 0x00058644
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0005A46B File Offset: 0x0005866B
		public float value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0f;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0005A47C File Offset: 0x0005867C
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0005A494 File Offset: 0x00058694
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

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005A49E File Offset: 0x0005869E
		public StyleFloat(float v)
		{
			this = new StyleFloat(v, StyleKeyword.Undefined);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005A4AA File Offset: 0x000586AA
		public StyleFloat(StyleKeyword keyword)
		{
			this = new StyleFloat(0f, keyword);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005A4BA File Offset: 0x000586BA
		internal StyleFloat(float v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0005A4CC File Offset: 0x000586CC
		public static bool operator ==(StyleFloat lhs, StyleFloat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005A500 File Offset: 0x00058700
		public static bool operator !=(StyleFloat lhs, StyleFloat rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005A51C File Offset: 0x0005871C
		public static implicit operator StyleFloat(StyleKeyword keyword)
		{
			return new StyleFloat(keyword);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005A534 File Offset: 0x00058734
		public static implicit operator StyleFloat(float v)
		{
			return new StyleFloat(v);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0005A54C File Offset: 0x0005874C
		public bool Equals(StyleFloat other)
		{
			return other == this;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0005A56C File Offset: 0x0005876C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFloat)
			{
				StyleFloat styleFloat = (StyleFloat)obj;
				flag = this.Equals(styleFloat);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0005A598 File Offset: 0x00058798
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0005A5C4 File Offset: 0x000587C4
		public override string ToString()
		{
			return this.DebugString<float>();
		}

		// Token: 0x04000908 RID: 2312
		private float m_Value;

		// Token: 0x04000909 RID: 2313
		private StyleKeyword m_Keyword;
	}
}
