using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000375 RID: 885
	internal class Expression
	{
		// Token: 0x06001C3E RID: 7230 RVA: 0x00083BD9 File Offset: 0x00081DD9
		public Expression(ExpressionType type)
		{
			this.type = type;
			this.combinator = ExpressionCombinator.None;
			this.multiplier = new ExpressionMultiplier(ExpressionMultiplierType.None);
			this.subExpressions = null;
			this.keyword = null;
		}

		// Token: 0x04000E10 RID: 3600
		public ExpressionType type;

		// Token: 0x04000E11 RID: 3601
		public ExpressionMultiplier multiplier;

		// Token: 0x04000E12 RID: 3602
		public DataType dataType;

		// Token: 0x04000E13 RID: 3603
		public ExpressionCombinator combinator;

		// Token: 0x04000E14 RID: 3604
		public Expression[] subExpressions;

		// Token: 0x04000E15 RID: 3605
		public string keyword;
	}
}
