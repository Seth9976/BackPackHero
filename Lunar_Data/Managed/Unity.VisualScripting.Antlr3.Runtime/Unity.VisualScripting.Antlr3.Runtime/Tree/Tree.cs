using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200001B RID: 27
	public sealed class Tree
	{
		// Token: 0x04000060 RID: 96
		public static readonly ITree INVALID_NODE = new CommonTree(Token.INVALID_TOKEN);
	}
}
