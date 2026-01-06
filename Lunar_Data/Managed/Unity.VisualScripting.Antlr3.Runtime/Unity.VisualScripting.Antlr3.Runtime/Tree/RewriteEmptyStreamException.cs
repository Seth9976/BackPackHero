using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class RewriteEmptyStreamException : RewriteCardinalityException
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00004FA9 File Offset: 0x00003FA9
		public RewriteEmptyStreamException(string elementDescription)
			: base(elementDescription)
		{
		}
	}
}
