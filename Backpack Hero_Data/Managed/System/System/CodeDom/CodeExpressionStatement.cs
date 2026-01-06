using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x0200030F RID: 783
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x060018EB RID: 6379 RVA: 0x0005F031 File Offset: 0x0005D231
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement. </param>
		// Token: 0x060018EC RID: 6380 RVA: 0x0005FBB4 File Offset: 0x0005DDB4
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0005FBC3 File Offset: 0x0005DDC3
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0005FBCB File Offset: 0x0005DDCB
		public CodeExpression Expression { get; set; }
	}
}
