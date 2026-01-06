using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000188 RID: 392
	public class BinaryExpression : LogicalExpression
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x00012FD2 File Offset: 0x000111D2
		public BinaryExpression(BinaryExpressionType type, LogicalExpression leftExpression, LogicalExpression rightExpression)
		{
			this.Type = type;
			this.LeftExpression = leftExpression;
			this.RightExpression = rightExpression;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00012FEF File Offset: 0x000111EF
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00012FF7 File Offset: 0x000111F7
		public LogicalExpression LeftExpression { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00013000 File Offset: 0x00011200
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00013008 File Offset: 0x00011208
		public LogicalExpression RightExpression { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00013011 File Offset: 0x00011211
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00013019 File Offset: 0x00011219
		public BinaryExpressionType Type { get; set; }

		// Token: 0x06000A7A RID: 2682 RVA: 0x00013022 File Offset: 0x00011222
		public override void Accept(LogicalExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
