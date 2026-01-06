using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x0200037A RID: 890
	internal struct ExpressionMultiplier
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00083C0C File Offset: 0x00081E0C
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x00083C24 File Offset: 0x00081E24
		public ExpressionMultiplierType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.SetType(value);
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00083C30 File Offset: 0x00081E30
		public ExpressionMultiplier(ExpressionMultiplierType type = ExpressionMultiplierType.None)
		{
			this.m_Type = type;
			this.min = (this.max = 1);
			this.SetType(type);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00083C60 File Offset: 0x00081E60
		private void SetType(ExpressionMultiplierType value)
		{
			this.m_Type = value;
			switch (value)
			{
			case ExpressionMultiplierType.ZeroOrMore:
				this.min = 0;
				this.max = 100;
				return;
			case ExpressionMultiplierType.OneOrMore:
			case ExpressionMultiplierType.OneOrMoreComma:
			case ExpressionMultiplierType.GroupAtLeastOne:
				this.min = 1;
				this.max = 100;
				return;
			case ExpressionMultiplierType.ZeroOrOne:
				this.min = 0;
				this.max = 1;
				return;
			}
			this.min = (this.max = 1);
		}

		// Token: 0x04000E36 RID: 3638
		public const int Infinity = 100;

		// Token: 0x04000E37 RID: 3639
		private ExpressionMultiplierType m_Type;

		// Token: 0x04000E38 RID: 3640
		public int min;

		// Token: 0x04000E39 RID: 3641
		public int max;
	}
}
