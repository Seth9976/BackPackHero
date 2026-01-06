using System;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x020002FF RID: 767
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x06001886 RID: 6278 RVA: 0x0005F5E1 File Offset: 0x0005D7E1
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class with the specified text as contents.</summary>
		/// <param name="text">The contents of the comment. </param>
		// Token: 0x06001887 RID: 6279 RVA: 0x0005F5E9 File Offset: 0x0005D7E9
		public CodeComment(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x06001888 RID: 6280 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.DocComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>true if the comment is a documentation comment; otherwise, false.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0005F60E File Offset: 0x0005D80E
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0005F616 File Offset: 0x0005D816
		public bool DocComment { get; set; }

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005F61F File Offset: 0x0005D81F
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0005F630 File Offset: 0x0005D830
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

		// Token: 0x04000D72 RID: 3442
		private string _text;
	}
}
