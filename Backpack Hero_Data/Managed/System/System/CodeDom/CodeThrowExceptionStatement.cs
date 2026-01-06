using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that throws an exception.</summary>
	// Token: 0x02000330 RID: 816
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class.</summary>
		// Token: 0x060019E0 RID: 6624 RVA: 0x0005F031 File Offset: 0x0005D231
		public CodeThrowExceptionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class with the specified exception type instance.</summary>
		/// <param name="toThrow">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the exception to throw. </param>
		// Token: 0x060019E1 RID: 6625 RVA: 0x00060DD4 File Offset: 0x0005EFD4
		public CodeThrowExceptionStatement(CodeExpression toThrow)
		{
			this.ToThrow = toThrow;
		}

		/// <summary>Gets or sets the exception to throw.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing an instance of the exception to throw.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00060DE3 File Offset: 0x0005EFE3
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x00060DEB File Offset: 0x0005EFEB
		public CodeExpression ToThrow { get; set; }
	}
}
