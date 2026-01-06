using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200003E RID: 62
	public class Parser : BaseRecognizer
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00007099 File Offset: 0x00006099
		public Parser(ITokenStream input)
		{
			this.TokenStream = input;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000070A8 File Offset: 0x000060A8
		public Parser(ITokenStream input, RecognizerSharedState state)
			: base(state)
		{
			this.TokenStream = input;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000070B8 File Offset: 0x000060B8
		public override void Reset()
		{
			base.Reset();
			if (this.input != null)
			{
				this.input.Seek(0);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000070D4 File Offset: 0x000060D4
		protected override object GetCurrentInputSymbol(IIntStream input)
		{
			return ((ITokenStream)input).LT(1);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000070E4 File Offset: 0x000060E4
		protected override object GetMissingSymbol(IIntStream input, RecognitionException e, int expectedTokenType, BitSet follow)
		{
			string text;
			if (expectedTokenType == Token.EOF)
			{
				text = "<missing EOF>";
			}
			else
			{
				text = "<missing " + this.TokenNames[expectedTokenType] + ">";
			}
			CommonToken commonToken = new CommonToken(expectedTokenType, text);
			IToken token = ((ITokenStream)input).LT(1);
			if (token.Type == Token.EOF)
			{
				token = ((ITokenStream)input).LT(-1);
			}
			commonToken.line = token.Line;
			commonToken.CharPositionInLine = token.CharPositionInLine;
			commonToken.Channel = 0;
			return commonToken;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000716A File Offset: 0x0000616A
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00007172 File Offset: 0x00006172
		public virtual ITokenStream TokenStream
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = null;
				this.Reset();
				this.input = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00007188 File Offset: 0x00006188
		public override string SourceName
		{
			get
			{
				return this.input.SourceName;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00007195 File Offset: 0x00006195
		public override IIntStream Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000719D File Offset: 0x0000619D
		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			base.TraceIn(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000071B3 File Offset: 0x000061B3
		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			base.TraceOut(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x040000A0 RID: 160
		protected internal ITokenStream input;
	}
}
