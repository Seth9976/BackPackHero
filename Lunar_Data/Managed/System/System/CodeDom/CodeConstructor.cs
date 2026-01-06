using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x02000304 RID: 772
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x060018AD RID: 6317 RVA: 0x0005F88A File Offset: 0x0005DA8A
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005F8B3 File Offset: 0x0005DAB3
		public CodeExpressionCollection BaseConstructorArgs { get; } = new CodeExpressionCollection();

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0005F8BB File Offset: 0x0005DABB
		public CodeExpressionCollection ChainedConstructorArgs { get; } = new CodeExpressionCollection();
	}
}
