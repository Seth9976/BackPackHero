using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x02000329 RID: 809
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class. </summary>
		// Token: 0x060019BA RID: 6586 RVA: 0x00060C07 File Offset: 0x0005EE07
		public CodeSnippetCompileUnit()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		/// <param name="value">The literal code fragment to represent. </param>
		// Token: 0x060019BB RID: 6587 RVA: 0x00060C0F File Offset: 0x0005EE0F
		public CodeSnippetCompileUnit(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment to represent.</summary>
		/// <returns>The literal code fragment.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00060C1E File Offset: 0x0005EE1E
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x00060C2F File Offset: 0x0005EE2F
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

		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00060C38 File Offset: 0x0005EE38
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x00060C40 File Offset: 0x0005EE40
		public CodeLinePragma LinePragma { get; set; }

		// Token: 0x04000DD5 RID: 3541
		private string _value;
	}
}
