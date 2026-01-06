using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class FailedPredicateException : RecognitionException
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000058C9 File Offset: 0x000048C9
		public FailedPredicateException()
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000058D1 File Offset: 0x000048D1
		public FailedPredicateException(IIntStream input, string ruleName, string predicateText)
			: base(input)
		{
			this.ruleName = ruleName;
			this.predicateText = predicateText;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000058E8 File Offset: 0x000048E8
		public override string ToString()
		{
			return string.Concat(new string[] { "FailedPredicateException(", this.ruleName, ",{", this.predicateText, "}?)" });
		}

		// Token: 0x0400006E RID: 110
		public string ruleName;

		// Token: 0x0400006F RID: 111
		public string predicateText;
	}
}
