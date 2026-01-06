using System;

namespace System.CodeDom
{
	/// <summary>Represents a specific location within a specific file.</summary>
	// Token: 0x02000315 RID: 789
	[Serializable]
	public class CodeLinePragma
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class. </summary>
		// Token: 0x0600190E RID: 6414 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeLinePragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		/// <param name="fileName">The file name of the associated file. </param>
		/// <param name="lineNumber">The line number to store a reference to. </param>
		// Token: 0x0600190F RID: 6415 RVA: 0x0005FD6C File Offset: 0x0005DF6C
		public CodeLinePragma(string fileName, int lineNumber)
		{
			this.FileName = fileName;
			this.LineNumber = lineNumber;
		}

		/// <summary>Gets or sets the name of the associated file.</summary>
		/// <returns>The file name of the associated file.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0005FD82 File Offset: 0x0005DF82
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x0005FD93 File Offset: 0x0005DF93
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets the line number of the associated reference.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0005FD9C File Offset: 0x0005DF9C
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x0005FDA4 File Offset: 0x0005DFA4
		public int LineNumber { get; set; }

		// Token: 0x04000D95 RID: 3477
		private string _fileName;
	}
}
