using System;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an if statement.</summary>
	// Token: 0x02000303 RID: 771
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x060018A6 RID: 6310 RVA: 0x0005F7DD File Offset: 0x0005D9DD
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		// Token: 0x060018A7 RID: 6311 RVA: 0x0005F7FB File Offset: 0x0005D9FB
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the condition to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		/// <param name="falseStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is false. </param>
		// Token: 0x060018A8 RID: 6312 RVA: 0x0005F82C File Offset: 0x0005DA2C
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		/// <summary>Gets or sets the expression to evaluate true or false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate true or false.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0005F869 File Offset: 0x0005DA69
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0005F871 File Offset: 0x0005DA71
		public CodeExpression Condition { get; set; }

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to true.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to true.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0005F87A File Offset: 0x0005DA7A
		public CodeStatementCollection TrueStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to false.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005F882 File Offset: 0x0005DA82
		public CodeStatementCollection FalseStatements { get; } = new CodeStatementCollection();
	}
}
