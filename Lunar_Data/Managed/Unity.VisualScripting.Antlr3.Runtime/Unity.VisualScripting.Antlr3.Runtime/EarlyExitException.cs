using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class EarlyExitException : RecognitionException
	{
		// Token: 0x060001BE RID: 446 RVA: 0x000057D5 File Offset: 0x000047D5
		public EarlyExitException()
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000057DD File Offset: 0x000047DD
		public EarlyExitException(int decisionNumber, IIntStream input)
			: base(input)
		{
			this.decisionNumber = decisionNumber;
		}

		// Token: 0x0400006D RID: 109
		public int decisionNumber;
	}
}
