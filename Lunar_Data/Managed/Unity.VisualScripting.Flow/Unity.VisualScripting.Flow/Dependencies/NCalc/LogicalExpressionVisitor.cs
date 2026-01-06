using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000194 RID: 404
	public abstract class LogicalExpressionVisitor
	{
		// Token: 0x06000AEC RID: 2796
		public abstract void Visit(TernaryExpression ternary);

		// Token: 0x06000AED RID: 2797
		public abstract void Visit(BinaryExpression binary);

		// Token: 0x06000AEE RID: 2798
		public abstract void Visit(UnaryExpression unary);

		// Token: 0x06000AEF RID: 2799
		public abstract void Visit(ValueExpression value);

		// Token: 0x06000AF0 RID: 2800
		public abstract void Visit(FunctionExpression function);

		// Token: 0x06000AF1 RID: 2801
		public abstract void Visit(IdentifierExpression identifier);
	}
}
