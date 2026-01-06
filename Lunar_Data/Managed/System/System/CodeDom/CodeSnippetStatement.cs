using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement using a literal code fragment.</summary>
	// Token: 0x0200032B RID: 811
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class.</summary>
		// Token: 0x060019C4 RID: 6596 RVA: 0x0005F031 File Offset: 0x0005D231
		public CodeSnippetStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class using the specified code fragment.</summary>
		/// <param name="value">The literal code fragment of the statement to represent. </param>
		// Token: 0x060019C5 RID: 6597 RVA: 0x00060C72 File Offset: 0x0005EE72
		public CodeSnippetStatement(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment statement.</summary>
		/// <returns>The literal code fragment statement.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00060C81 File Offset: 0x0005EE81
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x00060C92 File Offset: 0x0005EE92
		public string Value
		{
			get
			{
				return this._value ?? string.Empty;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000DD8 RID: 3544
		private string _value;
	}
}
