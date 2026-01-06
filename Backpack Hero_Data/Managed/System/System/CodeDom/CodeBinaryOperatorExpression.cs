using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x020002F9 RID: 761
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06001858 RID: 6232 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator. </param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator. </param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator. </param>
		// Token: 0x06001859 RID: 6233 RVA: 0x0005F33C File Offset: 0x0005D53C
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005F359 File Offset: 0x0005D559
		// (set) Token: 0x0600185B RID: 6235 RVA: 0x0005F361 File Offset: 0x0005D561
		public CodeExpression Right { get; set; }

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0005F36A File Offset: 0x0005D56A
		// (set) Token: 0x0600185D RID: 6237 RVA: 0x0005F372 File Offset: 0x0005D572
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0005F37B File Offset: 0x0005D57B
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x0005F383 File Offset: 0x0005D583
		public CodeBinaryOperatorType Operator { get; set; }
	}
}
