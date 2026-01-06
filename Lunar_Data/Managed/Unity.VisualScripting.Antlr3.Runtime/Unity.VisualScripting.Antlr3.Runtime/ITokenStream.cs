using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200000B RID: 11
	public interface ITokenStream : IIntStream
	{
		// Token: 0x06000061 RID: 97
		IToken LT(int k);

		// Token: 0x06000062 RID: 98
		IToken Get(int i);

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99
		ITokenSource TokenSource { get; }

		// Token: 0x06000064 RID: 100
		string ToString(int start, int stop);

		// Token: 0x06000065 RID: 101
		string ToString(IToken start, IToken stop);
	}
}
