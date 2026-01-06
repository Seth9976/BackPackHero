using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class RewriteEarlyExitException : RewriteCardinalityException
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00008853 File Offset: 0x00007853
		public RewriteEarlyExitException()
			: base(null)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000885C File Offset: 0x0000785C
		public RewriteEarlyExitException(string elementDescription)
			: base(elementDescription)
		{
		}
	}
}
