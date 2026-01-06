using System;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x02000323 RID: 803
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x060019A2 RID: 6562 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent. </param>
		// Token: 0x060019A3 RID: 6563 RVA: 0x00060AF4 File Offset: 0x0005ECF4
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00060B03 File Offset: 0x0005ED03
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x00060B0B File Offset: 0x0005ED0B
		public object Value { get; set; }
	}
}
