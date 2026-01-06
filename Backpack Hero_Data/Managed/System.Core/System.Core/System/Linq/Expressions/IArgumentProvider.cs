using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200025D RID: 605
	public interface IArgumentProvider
	{
		// Token: 0x060011B8 RID: 4536
		Expression GetArgument(int index);

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060011B9 RID: 4537
		int ArgumentCount { get; }
	}
}
