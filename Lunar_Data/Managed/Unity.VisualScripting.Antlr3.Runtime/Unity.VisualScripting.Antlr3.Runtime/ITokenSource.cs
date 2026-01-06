using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200002B RID: 43
	public interface ITokenSource
	{
		// Token: 0x060001CA RID: 458
		IToken NextToken();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CB RID: 459
		string SourceName { get; }
	}
}
