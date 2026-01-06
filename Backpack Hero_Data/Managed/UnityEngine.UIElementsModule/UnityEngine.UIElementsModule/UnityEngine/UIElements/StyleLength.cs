using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000285 RID: 645
	public struct StyleLength : IStyleValue<Length>, IEquatable<StyleLength>
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0005AB78 File Offset: 0x00058D78
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x0005ABA3 File Offset: 0x00058DA3
		public Length value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Length);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0005ABB4 File Offset: 0x00058DB4
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x0005ABCC File Offset: 0x00058DCC
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

		// Token: 0x0600150B RID: 5387 RVA: 0x0005ABD6 File Offset: 0x00058DD6
		public StyleLength(float v)
		{
			this = new StyleLength(new Length(v, LengthUnit.Pixel), StyleKeyword.Undefined);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		public StyleLength(Length v)
		{
			this = new StyleLength(v, StyleKeyword.Undefined);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public StyleLength(StyleKeyword keyword)
		{
			this = new StyleLength(default(Length), keyword);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0005AC14 File Offset: 0x00058E14
		internal StyleLength(Length v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
			bool flag = v.IsAuto();
			if (flag)
			{
				this.m_Keyword = StyleKeyword.Auto;
			}
			else
			{
				bool flag2 = v.IsNone();
				if (flag2)
				{
					this.m_Keyword = StyleKeyword.None;
				}
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005AC58 File Offset: 0x00058E58
		public static bool operator ==(StyleLength lhs, StyleLength rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0005AC8C File Offset: 0x00058E8C
		public static bool operator !=(StyleLength lhs, StyleLength rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0005ACA8 File Offset: 0x00058EA8
		public static implicit operator StyleLength(StyleKeyword keyword)
		{
			return new StyleLength(keyword);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0005ACC0 File Offset: 0x00058EC0
		public static implicit operator StyleLength(float v)
		{
			return new StyleLength(v);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0005ACD8 File Offset: 0x00058ED8
		public static implicit operator StyleLength(Length v)
		{
			return new StyleLength(v);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0005ACF0 File Offset: 0x00058EF0
		public bool Equals(StyleLength other)
		{
			return other == this;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0005AD10 File Offset: 0x00058F10
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleLength)
			{
				StyleLength styleLength = (StyleLength)obj;
				flag = this.Equals(styleLength);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0005AD3C File Offset: 0x00058F3C
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Keyword;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0005AD70 File Offset: 0x00058F70
		public override string ToString()
		{
			return this.DebugString<Length>();
		}

		// Token: 0x04000910 RID: 2320
		private Length m_Value;

		// Token: 0x04000911 RID: 2321
		private StyleKeyword m_Keyword;
	}
}
