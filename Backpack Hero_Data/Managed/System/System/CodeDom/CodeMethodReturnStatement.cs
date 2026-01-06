using System;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x0200031B RID: 795
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x06001945 RID: 6469 RVA: 0x0005F031 File Offset: 0x0005D231
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value. </param>
		// Token: 0x06001946 RID: 6470 RVA: 0x0006032E File Offset: 0x0005E52E
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or null if the statement is part of a subroutine.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x0006033D File Offset: 0x0005E53D
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x00060345 File Offset: 0x0005E545
		public CodeExpression Expression { get; set; }
	}
}
