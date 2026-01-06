using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class NoViableAltException : RecognitionException
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00004690 File Offset: 0x00003690
		public NoViableAltException()
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004698 File Offset: 0x00003698
		public NoViableAltException(string grammarDecisionDescription, int decisionNumber, int stateNumber, IIntStream input)
			: base(input)
		{
			this.grammarDecisionDescription = grammarDecisionDescription;
			this.decisionNumber = decisionNumber;
			this.stateNumber = stateNumber;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000046B8 File Offset: 0x000036B8
		public override string ToString()
		{
			if (this.input is ICharStream)
			{
				return string.Concat(new object[]
				{
					"NoViableAltException('",
					(char)this.UnexpectedType,
					"'@[",
					this.grammarDecisionDescription,
					"])"
				});
			}
			return string.Concat(new object[] { "NoViableAltException(", this.UnexpectedType, "@[", this.grammarDecisionDescription, "])" });
		}

		// Token: 0x0400004A RID: 74
		public string grammarDecisionDescription;

		// Token: 0x0400004B RID: 75
		public int decisionNumber;

		// Token: 0x0400004C RID: 76
		public int stateNumber;
	}
}
