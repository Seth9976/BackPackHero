using System;
using System.Globalization;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x02000349 RID: 841
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x06001BA9 RID: 7081 RVA: 0x00065F58 File Offset: 0x00064158
		public CompilerError()
			: this(string.Empty, 0, 0, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error. </param>
		/// <param name="line">The line of the source of the error. </param>
		/// <param name="column">The column of the source of the error. </param>
		/// <param name="errorNumber">The error number of the error. </param>
		/// <param name="errorText">The error message text. </param>
		// Token: 0x06001BAA RID: 7082 RVA: 0x00065F71 File Offset: 0x00064171
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.Line = line;
			this.Column = column;
			this.ErrorNumber = errorNumber;
			this.ErrorText = errorText;
			this.FileName = fileName;
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00065F9E File Offset: 0x0006419E
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x00065FA6 File Offset: 0x000641A6
		public int Line { get; set; }

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00065FAF File Offset: 0x000641AF
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x00065FB7 File Offset: 0x000641B7
		public int Column { get; set; }

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00065FC0 File Offset: 0x000641C0
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x00065FC8 File Offset: 0x000641C8
		public string ErrorNumber { get; set; }

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00065FD1 File Offset: 0x000641D1
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x00065FD9 File Offset: 0x000641D9
		public string ErrorText { get; set; }

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>true if the error is a warning; otherwise, false.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00065FE2 File Offset: 0x000641E2
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x00065FEA File Offset: 0x000641EA
		public bool IsWarning { get; set; }

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00065FF3 File Offset: 0x000641F3
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00065FFB File Offset: 0x000641FB
		public string FileName { get; set; }

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x06001BB7 RID: 7095 RVA: 0x00066004 File Offset: 0x00064204
		public override string ToString()
		{
			if (this.FileName.Length <= 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", this.WarningString, this.ErrorNumber, this.ErrorText);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[] { this.FileName, this.Line, this.Column, this.WarningString, this.ErrorNumber, this.ErrorText });
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00066096 File Offset: 0x00064296
		private string WarningString
		{
			get
			{
				if (!this.IsWarning)
				{
					return "error";
				}
				return "warning";
			}
		}
	}
}
