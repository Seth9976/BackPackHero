using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x02000308 RID: 776
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class.</summary>
		// Token: 0x060018C2 RID: 6338 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeDirectionExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class using the specified field direction and expression.</summary>
		/// <param name="direction">A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction of the expression. </param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to represent. </param>
		// Token: 0x060018C3 RID: 6339 RVA: 0x0005F9EE File Offset: 0x0005DBEE
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.Expression = expression;
			this.Direction = direction;
		}

		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005FA04 File Offset: 0x0005DC04
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x0005FA0C File Offset: 0x0005DC0C
		public CodeExpression Expression { get; set; }

		/// <summary>Gets or sets the field direction for this direction expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction for this direction expression.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005FA15 File Offset: 0x0005DC15
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0005FA1D File Offset: 0x0005DC1D
		public FieldDirection Direction { get; set; }
	}
}
