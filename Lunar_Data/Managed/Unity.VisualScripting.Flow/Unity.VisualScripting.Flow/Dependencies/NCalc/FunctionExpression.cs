using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000191 RID: 401
	public class FunctionExpression : LogicalExpression
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00014767 File Offset: 0x00012967
		public FunctionExpression(IdentifierExpression identifier, LogicalExpression[] expressions)
		{
			this.Identifier = identifier;
			this.Expressions = expressions;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001477D File Offset: 0x0001297D
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00014785 File Offset: 0x00012985
		public IdentifierExpression Identifier { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001478E File Offset: 0x0001298E
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00014796 File Offset: 0x00012996
		public LogicalExpression[] Expressions { get; set; }

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001479F File Offset: 0x0001299F
		public override void Accept(LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
