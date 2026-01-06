using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000049 RID: 73
	public class ParserRuleReturnScope : RuleReturnScope
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000881F File Offset: 0x0000781F
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00008827 File Offset: 0x00007827
		public override object Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = (IToken)value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00008835 File Offset: 0x00007835
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000883D File Offset: 0x0000783D
		public override object Stop
		{
			get
			{
				return this.stop;
			}
			set
			{
				this.stop = (IToken)value;
			}
		}

		// Token: 0x040000D9 RID: 217
		private IToken start;

		// Token: 0x040000DA RID: 218
		private IToken stop;
	}
}
