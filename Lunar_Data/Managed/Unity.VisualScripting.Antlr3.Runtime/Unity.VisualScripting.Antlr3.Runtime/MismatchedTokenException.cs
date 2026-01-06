using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class MismatchedTokenException : RecognitionException
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000055DD File Offset: 0x000045DD
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000055E5 File Offset: 0x000045E5
		public int Expecting
		{
			get
			{
				return this.expecting;
			}
			set
			{
				this.expecting = value;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000055EE File Offset: 0x000045EE
		public MismatchedTokenException()
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000055F6 File Offset: 0x000045F6
		public MismatchedTokenException(int expecting, IIntStream input)
			: base(input)
		{
			this.expecting = expecting;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005608 File Offset: 0x00004608
		public override string ToString()
		{
			return string.Concat(new object[] { "MismatchedTokenException(", this.UnexpectedType, "!=", this.expecting, ")" });
		}

		// Token: 0x04000069 RID: 105
		private int expecting;
	}
}
