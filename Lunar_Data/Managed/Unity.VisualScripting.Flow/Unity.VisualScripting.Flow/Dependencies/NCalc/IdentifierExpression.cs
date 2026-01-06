using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000192 RID: 402
	public class IdentifierExpression : LogicalExpression
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x000147A8 File Offset: 0x000129A8
		public IdentifierExpression(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000147B7 File Offset: 0x000129B7
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x000147BF File Offset: 0x000129BF
		public string Name { get; set; }

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000147C8 File Offset: 0x000129C8
		public override void Accept(LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
