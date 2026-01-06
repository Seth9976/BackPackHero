using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000022 RID: 34
	public interface ICharStream : IIntStream
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A7 RID: 423
		// (set) Token: 0x060001A8 RID: 424
		int Line { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A9 RID: 425
		// (set) Token: 0x060001AA RID: 426
		int CharPositionInLine { get; set; }

		// Token: 0x060001AB RID: 427
		int LT(int i);

		// Token: 0x060001AC RID: 428
		string Substring(int start, int stop);
	}
}
