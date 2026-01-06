using System;

namespace System.CodeDom
{
	/// <summary>Represents a member of a type using a literal code fragment.</summary>
	// Token: 0x0200032C RID: 812
	[Serializable]
	public class CodeSnippetTypeMember : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class.</summary>
		// Token: 0x060019C8 RID: 6600 RVA: 0x0005FDAD File Offset: 0x0005DFAD
		public CodeSnippetTypeMember()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class using the specified text.</summary>
		/// <param name="text">The literal code fragment for the type member. </param>
		// Token: 0x060019C9 RID: 6601 RVA: 0x00060C9B File Offset: 0x0005EE9B
		public CodeSnippetTypeMember(string text)
		{
			this.Text = text;
		}

		/// <summary>Gets or sets the literal code fragment for the type member.</summary>
		/// <returns>The literal code fragment for the type member.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00060CAA File Offset: 0x0005EEAA
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x00060CBB File Offset: 0x0005EEBB
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x04000DD9 RID: 3545
		private string _text;
	}
}
