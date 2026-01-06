using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class MismatchedRangeException : RecognitionException
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000609D File Offset: 0x0000509D
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000060A5 File Offset: 0x000050A5
		public int A
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000060AE File Offset: 0x000050AE
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000060B6 File Offset: 0x000050B6
		public int B
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000060BF File Offset: 0x000050BF
		public MismatchedRangeException()
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000060C7 File Offset: 0x000050C7
		public MismatchedRangeException(int a, int b, IIntStream input)
			: base(input)
		{
			this.a = a;
			this.b = b;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000060E0 File Offset: 0x000050E0
		public override string ToString()
		{
			return string.Concat(new object[] { "MismatchedNotSetException(", this.UnexpectedType, " not in [", this.a, ",", this.b, "])" });
		}

		// Token: 0x04000072 RID: 114
		private int a;

		// Token: 0x04000073 RID: 115
		private int b;
	}
}
