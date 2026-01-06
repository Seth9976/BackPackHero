using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class MismatchedNotSetException : MismatchedSetException
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000586D File Offset: 0x0000486D
		public MismatchedNotSetException()
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005875 File Offset: 0x00004875
		public MismatchedNotSetException(BitSet expecting, IIntStream input)
			: base(expecting, input)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005880 File Offset: 0x00004880
		public override string ToString()
		{
			return string.Concat(new object[] { "MismatchedNotSetException(", this.UnexpectedType, "!=", this.expecting, ")" });
		}
	}
}
