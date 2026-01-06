using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200002C RID: 44
	public abstract class Lexer : BaseRecognizer, ITokenSource
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000592C File Offset: 0x0000492C
		public Lexer()
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005934 File Offset: 0x00004934
		public Lexer(ICharStream input)
		{
			this.input = input;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005943 File Offset: 0x00004943
		public Lexer(ICharStream input, RecognizerSharedState state)
			: base(state)
		{
			this.input = input;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00005953 File Offset: 0x00004953
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000595B File Offset: 0x0000495B
		public virtual ICharStream CharStream
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005971 File Offset: 0x00004971
		public override string SourceName
		{
			get
			{
				return this.input.SourceName;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000597E File Offset: 0x0000497E
		public override IIntStream Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00005986 File Offset: 0x00004986
		public virtual int Line
		{
			get
			{
				return this.input.Line;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005993 File Offset: 0x00004993
		public virtual int CharPositionInLine
		{
			get
			{
				return this.input.CharPositionInLine;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000059A0 File Offset: 0x000049A0
		public virtual int CharIndex
		{
			get
			{
				return this.input.Index();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000059AD File Offset: 0x000049AD
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000059E6 File Offset: 0x000049E6
		public virtual string Text
		{
			get
			{
				if (this.state.text != null)
				{
					return this.state.text;
				}
				return this.input.Substring(this.state.tokenStartCharIndex, this.CharIndex - 1);
			}
			set
			{
				this.state.text = value;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000059F4 File Offset: 0x000049F4
		public override void Reset()
		{
			base.Reset();
			if (this.input != null)
			{
				this.input.Seek(0);
			}
			if (this.state == null)
			{
				return;
			}
			this.state.token = null;
			this.state.type = 0;
			this.state.channel = 0;
			this.state.tokenStartCharIndex = -1;
			this.state.tokenStartCharPositionInLine = -1;
			this.state.tokenStartLine = -1;
			this.state.text = null;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005A78 File Offset: 0x00004A78
		public virtual IToken NextToken()
		{
			for (;;)
			{
				this.state.token = null;
				this.state.channel = 0;
				this.state.tokenStartCharIndex = this.input.Index();
				this.state.tokenStartCharPositionInLine = this.input.CharPositionInLine;
				this.state.tokenStartLine = this.input.Line;
				this.state.text = null;
				if (this.input.LA(1) == -1)
				{
					break;
				}
				IToken token;
				try
				{
					this.mTokens();
					if (this.state.token == null)
					{
						this.Emit();
					}
					else if (this.state.token == Token.SKIP_TOKEN)
					{
						continue;
					}
					token = this.state.token;
				}
				catch (NoViableAltException ex)
				{
					this.ReportError(ex);
					this.Recover(ex);
					continue;
				}
				catch (RecognitionException ex2)
				{
					this.ReportError(ex2);
					continue;
				}
				return token;
			}
			return Token.EOF_TOKEN;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005B80 File Offset: 0x00004B80
		public void Skip()
		{
			this.state.token = Token.SKIP_TOKEN;
		}

		// Token: 0x060001DB RID: 475
		public abstract void mTokens();

		// Token: 0x060001DC RID: 476 RVA: 0x00005B92 File Offset: 0x00004B92
		public virtual void Emit(IToken token)
		{
			this.state.token = token;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005BA0 File Offset: 0x00004BA0
		public virtual IToken Emit()
		{
			IToken token = new CommonToken(this.input, this.state.type, this.state.channel, this.state.tokenStartCharIndex, this.CharIndex - 1);
			token.Line = this.state.tokenStartLine;
			token.Text = this.state.text;
			token.CharPositionInLine = this.state.tokenStartCharPositionInLine;
			this.Emit(token);
			return token;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005C20 File Offset: 0x00004C20
		public virtual void Match(string s)
		{
			int i = 0;
			while (i < s.Length)
			{
				if (this.input.LA(1) != (int)s.get_Chars(i))
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return;
					}
					MismatchedTokenException ex = new MismatchedTokenException((int)s.get_Chars(i), this.input);
					this.Recover(ex);
					throw ex;
				}
				else
				{
					i++;
					this.input.Consume();
					this.state.failed = false;
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005CA1 File Offset: 0x00004CA1
		public virtual void MatchAny()
		{
			this.input.Consume();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005CB0 File Offset: 0x00004CB0
		public virtual void Match(int c)
		{
			if (this.input.LA(1) == c)
			{
				this.input.Consume();
				this.state.failed = false;
				return;
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return;
			}
			MismatchedTokenException ex = new MismatchedTokenException(c, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005D14 File Offset: 0x00004D14
		public virtual void MatchRange(int a, int b)
		{
			if (this.input.LA(1) >= a && this.input.LA(1) <= b)
			{
				this.input.Consume();
				this.state.failed = false;
				return;
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return;
			}
			MismatchedRangeException ex = new MismatchedRangeException(a, b, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005D88 File Offset: 0x00004D88
		public virtual void Recover(RecognitionException re)
		{
			this.input.Consume();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005D95 File Offset: 0x00004D95
		public override void ReportError(RecognitionException e)
		{
			this.DisplayRecognitionError(this.TokenNames, e);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005DA4 File Offset: 0x00004DA4
		public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
		{
			string text;
			if (e is MismatchedTokenException)
			{
				MismatchedTokenException ex = (MismatchedTokenException)e;
				text = "mismatched character " + this.GetCharErrorDisplay(e.Char) + " expecting " + this.GetCharErrorDisplay(ex.Expecting);
			}
			else if (e is NoViableAltException)
			{
				NoViableAltException ex2 = (NoViableAltException)e;
				text = "no viable alternative at character " + this.GetCharErrorDisplay(ex2.Char);
			}
			else if (e is EarlyExitException)
			{
				EarlyExitException ex3 = (EarlyExitException)e;
				text = "required (...)+ loop did not match anything at character " + this.GetCharErrorDisplay(ex3.Char);
			}
			else if (e is MismatchedNotSetException)
			{
				MismatchedSetException ex4 = (MismatchedSetException)e;
				text = string.Concat(new object[]
				{
					"mismatched character ",
					this.GetCharErrorDisplay(ex4.Char),
					" expecting set ",
					ex4.expecting
				});
			}
			else if (e is MismatchedSetException)
			{
				MismatchedSetException ex5 = (MismatchedSetException)e;
				text = string.Concat(new object[]
				{
					"mismatched character ",
					this.GetCharErrorDisplay(ex5.Char),
					" expecting set ",
					ex5.expecting
				});
			}
			else if (e is MismatchedRangeException)
			{
				MismatchedRangeException ex6 = (MismatchedRangeException)e;
				text = string.Concat(new string[]
				{
					"mismatched character ",
					this.GetCharErrorDisplay(ex6.Char),
					" expecting set ",
					this.GetCharErrorDisplay(ex6.A),
					"..",
					this.GetCharErrorDisplay(ex6.B)
				});
			}
			else
			{
				text = base.GetErrorMessage(e, tokenNames);
			}
			return text;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005F64 File Offset: 0x00004F64
		public string GetCharErrorDisplay(int c)
		{
			string text;
			if (c != -1)
			{
				switch (c)
				{
				case 9:
					text = "\\t";
					goto IL_004D;
				case 10:
					text = "\\n";
					goto IL_004D;
				case 13:
					text = "\\r";
					goto IL_004D;
				}
				text = Convert.ToString((char)c);
			}
			else
			{
				text = "<EOF>";
			}
			IL_004D:
			return "'" + text + "'";
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005FD0 File Offset: 0x00004FD0
		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			string text = string.Concat(new object[]
			{
				(char)this.input.LT(1),
				" line=",
				this.Line,
				":",
				this.CharPositionInLine
			});
			base.TraceIn(ruleName, ruleIndex, text);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006038 File Offset: 0x00005038
		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			string text = string.Concat(new object[]
			{
				(char)this.input.LT(1),
				" line=",
				this.Line,
				":",
				this.CharPositionInLine
			});
			base.TraceOut(ruleName, ruleIndex, text);
		}

		// Token: 0x04000070 RID: 112
		private const int TOKEN_dot_EOF = -1;

		// Token: 0x04000071 RID: 113
		protected internal ICharStream input;
	}
}
