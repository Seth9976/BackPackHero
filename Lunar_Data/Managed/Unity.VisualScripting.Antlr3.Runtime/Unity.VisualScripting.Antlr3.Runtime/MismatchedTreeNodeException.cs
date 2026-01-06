using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class MismatchedTreeNodeException : RecognitionException
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x0000570A File Offset: 0x0000470A
		public MismatchedTreeNodeException()
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005712 File Offset: 0x00004712
		public MismatchedTreeNodeException(int expecting, ITreeNodeStream input)
			: base(input)
		{
			this.expecting = expecting;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005724 File Offset: 0x00004724
		public override string ToString()
		{
			return string.Concat(new object[] { "MismatchedTreeNodeException(", this.UnexpectedType, "!=", this.expecting, ")" });
		}

		// Token: 0x0400006B RID: 107
		public int expecting;
	}
}
