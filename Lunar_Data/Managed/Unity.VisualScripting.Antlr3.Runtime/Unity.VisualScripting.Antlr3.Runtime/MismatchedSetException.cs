using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class MismatchedSetException : RecognitionException
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00005772 File Offset: 0x00004772
		public MismatchedSetException()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000577A File Offset: 0x0000477A
		public MismatchedSetException(BitSet expecting, IIntStream input)
			: base(input)
		{
			this.expecting = expecting;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000578C File Offset: 0x0000478C
		public override string ToString()
		{
			return string.Concat(new object[] { "MismatchedSetException(", this.UnexpectedType, "!=", this.expecting, ")" });
		}

		// Token: 0x0400006C RID: 108
		public BitSet expecting;
	}
}
