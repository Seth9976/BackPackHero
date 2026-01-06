using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a default value.</summary>
	// Token: 0x02000305 RID: 773
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class. </summary>
		// Token: 0x060018B0 RID: 6320 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeDefaultValueExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class using the specified code type reference.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that specifies the reference to a value type.</param>
		// Token: 0x060018B1 RID: 6321 RVA: 0x0005F8C3 File Offset: 0x0005DAC3
		public CodeDefaultValueExpression(CodeTypeReference type)
		{
			this._type = type;
		}

		/// <summary>Gets or sets the data type reference for a default value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> object representing a data type that has a default value.</returns>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005F8D4 File Offset: 0x0005DAD4
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0005F8FE File Offset: 0x0005DAFE
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x04000D7F RID: 3455
		private CodeTypeReference _type;
	}
}
