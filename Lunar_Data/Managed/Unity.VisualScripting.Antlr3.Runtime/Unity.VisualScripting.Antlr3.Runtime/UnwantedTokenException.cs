using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000028 RID: 40
	public class UnwantedTokenException : MismatchedTokenException
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x000057ED File Offset: 0x000047ED
		public UnwantedTokenException()
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000057F5 File Offset: 0x000047F5
		public UnwantedTokenException(int expecting, IIntStream input)
			: base(expecting, input)
		{
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000057FF File Offset: 0x000047FF
		public IToken UnexpectedToken
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005808 File Offset: 0x00004808
		public override string ToString()
		{
			string text = ", expected " + base.Expecting;
			if (base.Expecting == 0)
			{
				text = "";
			}
			if (this.token == null)
			{
				return "UnwantedTokenException(found=" + text + ")";
			}
			return "UnwantedTokenException(found=" + this.token.Text + text + ")";
		}
	}
}
