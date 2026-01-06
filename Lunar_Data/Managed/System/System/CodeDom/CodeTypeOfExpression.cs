using System;

namespace System.CodeDom
{
	/// <summary>Represents a typeof expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x02000338 RID: 824
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06001A2B RID: 6699 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the typeof expression. </param>
		// Token: 0x06001A2C RID: 6700 RVA: 0x000614A0 File Offset: 0x0005F6A0
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the typeof expression. </param>
		// Token: 0x06001A2D RID: 6701 RVA: 0x000614AF File Offset: 0x0005F6AF
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The data type of the data type of the typeof expression. </param>
		// Token: 0x06001A2E RID: 6702 RVA: 0x000614C3 File Offset: 0x0005F6C3
		public CodeTypeOfExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the typeof expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the typeof expression. This property will never return null, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x000614D8 File Offset: 0x0005F6D8
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x00061502 File Offset: 0x0005F702
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

		// Token: 0x04000DF6 RID: 3574
		private CodeTypeReference _type;
	}
}
