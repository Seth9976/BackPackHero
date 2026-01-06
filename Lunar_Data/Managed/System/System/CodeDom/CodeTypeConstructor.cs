using System;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x02000332 RID: 818
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x060019EA RID: 6634 RVA: 0x00060EDC File Offset: 0x0005F0DC
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
