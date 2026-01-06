using System;

namespace System.CodeDom
{
	/// <summary>Represents a catch exception block of a try/catch statement.</summary>
	// Token: 0x020002FC RID: 764
	[Serializable]
	public class CodeCatchClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class.</summary>
		// Token: 0x06001868 RID: 6248 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeCatchClause()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		// Token: 0x06001869 RID: 6249 RVA: 0x0005F41C File Offset: 0x0005D61C
		public CodeCatchClause(string localName)
		{
			this._localName = localName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception and exception type.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch. </param>
		// Token: 0x0600186A RID: 6250 RVA: 0x0005F42B File Offset: 0x0005D62B
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType)
		{
			this._localName = localName;
			this._catchExceptionType = catchExceptionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception, exception type and statement collection.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch. </param>
		/// <param name="statements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that represent the contents of the catch block. </param>
		// Token: 0x0600186B RID: 6251 RVA: 0x0005F441 File Offset: 0x0005D641
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType, params CodeStatement[] statements)
		{
			this._localName = localName;
			this._catchExceptionType = catchExceptionType;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the variable name of the exception that the catch clause handles.</summary>
		/// <returns>The name for the exception variable that the catch clause handles.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0005F463 File Offset: 0x0005D663
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x0005F474 File Offset: 0x0005D674
		public string LocalName
		{
			get
			{
				return this._localName ?? string.Empty;
			}
			set
			{
				this._localName = value;
			}
		}

		/// <summary>Gets or sets the type of the exception to handle with the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the exception to handle.</returns>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0005F480 File Offset: 0x0005D680
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x0005F4AF File Offset: 0x0005D6AF
		public CodeTypeReference CatchExceptionType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._catchExceptionType) == null)
				{
					codeTypeReference = (this._catchExceptionType = new CodeTypeReference(typeof(Exception)));
				}
				return codeTypeReference;
			}
			set
			{
				this._catchExceptionType = value;
			}
		}

		/// <summary>Gets the statements within the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements within the catch block.</returns>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0005F4B8 File Offset: 0x0005D6B8
		public CodeStatementCollection Statements
		{
			get
			{
				CodeStatementCollection codeStatementCollection;
				if ((codeStatementCollection = this._statements) == null)
				{
					codeStatementCollection = (this._statements = new CodeStatementCollection());
				}
				return codeStatementCollection;
			}
		}

		// Token: 0x04000D6C RID: 3436
		private CodeStatementCollection _statements;

		// Token: 0x04000D6D RID: 3437
		private CodeTypeReference _catchExceptionType;

		// Token: 0x04000D6E RID: 3438
		private string _localName;
	}
}
