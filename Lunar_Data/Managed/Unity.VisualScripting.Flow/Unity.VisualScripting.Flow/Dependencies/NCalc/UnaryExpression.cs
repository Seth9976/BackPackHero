using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x0200019A RID: 410
	public class UnaryExpression : LogicalExpression
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x0001A1A3 File Offset: 0x000183A3
		public UnaryExpression(UnaryExpressionType type, LogicalExpression expression)
		{
			this.Type = type;
			this.Expression = expression;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001A1B9 File Offset: 0x000183B9
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0001A1C1 File Offset: 0x000183C1
		public LogicalExpression Expression { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0001A1CA File Offset: 0x000183CA
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0001A1D2 File Offset: 0x000183D2
		public UnaryExpressionType Type { get; set; }

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001A1DB File Offset: 0x000183DB
		public override void Accept(LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
