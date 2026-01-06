using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000199 RID: 409
	public class TernaryExpression : LogicalExpression
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0001A14A File Offset: 0x0001834A
		public TernaryExpression(LogicalExpression leftExpression, LogicalExpression middleExpression, LogicalExpression rightExpression)
		{
			this.LeftExpression = leftExpression;
			this.MiddleExpression = middleExpression;
			this.RightExpression = rightExpression;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001A167 File Offset: 0x00018367
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0001A16F File Offset: 0x0001836F
		public LogicalExpression LeftExpression { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001A178 File Offset: 0x00018378
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0001A180 File Offset: 0x00018380
		public LogicalExpression MiddleExpression { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001A189 File Offset: 0x00018389
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0001A191 File Offset: 0x00018391
		public LogicalExpression RightExpression { get; set; }

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001A19A File Offset: 0x0001839A
		public override void Accept(LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
