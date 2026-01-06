using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200025E RID: 606
	public interface IDynamicExpression : IArgumentProvider
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060011BA RID: 4538
		Type DelegateType { get; }

		// Token: 0x060011BB RID: 4539
		Expression Rewrite(Expression[] args);

		// Token: 0x060011BC RID: 4540
		object CreateCallSite();
	}
}
