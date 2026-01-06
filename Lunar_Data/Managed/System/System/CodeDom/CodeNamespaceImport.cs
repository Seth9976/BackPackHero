using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x0200031E RID: 798
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x06001963 RID: 6499 RVA: 0x0005F5E1 File Offset: 0x0005D7E1
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import. </param>
		// Token: 0x06001964 RID: 6500 RVA: 0x00060658 File Offset: 0x0005E858
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00060667 File Offset: 0x0005E867
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x0006066F File Offset: 0x0005E86F
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00060678 File Offset: 0x0005E878
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x00060689 File Offset: 0x0005E889
		public string Namespace
		{
			get
			{
				return this._nameSpace ?? string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		// Token: 0x04000DC0 RID: 3520
		private string _nameSpace;
	}
}
