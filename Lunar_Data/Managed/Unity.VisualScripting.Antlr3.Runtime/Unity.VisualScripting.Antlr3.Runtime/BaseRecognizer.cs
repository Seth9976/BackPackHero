using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000016 RID: 22
	public abstract class BaseRecognizer
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00003A4C File Offset: 0x00002A4C
		public BaseRecognizer()
		{
			this.state = new RecognizerSharedState();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003A5F File Offset: 0x00002A5F
		public BaseRecognizer(RecognizerSharedState state)
		{
			if (state == null)
			{
				state = new RecognizerSharedState();
			}
			this.state = state;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003A78 File Offset: 0x00002A78
		public virtual void BeginBacktrack(int level)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003A7A File Offset: 0x00002A7A
		public virtual void EndBacktrack(int level, bool successful)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000118 RID: 280
		public abstract IIntStream Input { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003A7C File Offset: 0x00002A7C
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00003A89 File Offset: 0x00002A89
		public int BacktrackingLevel
		{
			get
			{
				return this.state.backtracking;
			}
			set
			{
				this.state.backtracking = value;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003A97 File Offset: 0x00002A97
		public bool Failed()
		{
			return this.state.failed;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003AA4 File Offset: 0x00002AA4
		public virtual void Reset()
		{
			if (this.state == null)
			{
				return;
			}
			this.state.followingStackPointer = -1;
			this.state.errorRecovery = false;
			this.state.lastErrorIndex = -1;
			this.state.failed = false;
			this.state.syntaxErrors = 0;
			this.state.backtracking = 0;
			int num = 0;
			while (this.state.ruleMemo != null && num < this.state.ruleMemo.Length)
			{
				this.state.ruleMemo[num] = null;
				num++;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003B38 File Offset: 0x00002B38
		public virtual object Match(IIntStream input, int ttype, BitSet follow)
		{
			object currentInputSymbol = this.GetCurrentInputSymbol(input);
			if (input.LA(1) == ttype)
			{
				input.Consume();
				this.state.errorRecovery = false;
				this.state.failed = false;
				return currentInputSymbol;
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return currentInputSymbol;
			}
			return this.RecoverFromMismatchedToken(input, ttype, follow);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003B9E File Offset: 0x00002B9E
		public virtual void MatchAny(IIntStream input)
		{
			this.state.errorRecovery = false;
			this.state.failed = false;
			input.Consume();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003BBE File Offset: 0x00002BBE
		public bool MismatchIsUnwantedToken(IIntStream input, int ttype)
		{
			return input.LA(2) == ttype;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003BCC File Offset: 0x00002BCC
		public bool MismatchIsMissingToken(IIntStream input, BitSet follow)
		{
			if (follow == null)
			{
				return false;
			}
			if (follow.Member(1))
			{
				BitSet bitSet = this.ComputeContextSensitiveRuleFOLLOW();
				follow = follow.Or(bitSet);
				if (this.state.followingStackPointer >= 0)
				{
					follow.Remove(1);
				}
			}
			return follow.Member(input.LA(1)) || follow.Member(1);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003C27 File Offset: 0x00002C27
		public virtual void ReportError(RecognitionException e)
		{
			if (this.state.errorRecovery)
			{
				return;
			}
			this.state.syntaxErrors++;
			this.state.errorRecovery = true;
			this.DisplayRecognitionError(this.TokenNames, e);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003C64 File Offset: 0x00002C64
		public virtual void DisplayRecognitionError(string[] tokenNames, RecognitionException e)
		{
			string errorHeader = this.GetErrorHeader(e);
			string errorMessage = this.GetErrorMessage(e, tokenNames);
			this.EmitErrorMessage(errorHeader + " " + errorMessage);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003C94 File Offset: 0x00002C94
		public virtual string GetErrorMessage(RecognitionException e, string[] tokenNames)
		{
			string text = e.Message;
			if (e is UnwantedTokenException)
			{
				UnwantedTokenException ex = (UnwantedTokenException)e;
				string text2;
				if (ex.Expecting == Token.EOF)
				{
					text2 = "EOF";
				}
				else
				{
					text2 = tokenNames[ex.Expecting];
				}
				text = "extraneous input " + this.GetTokenErrorDisplay(ex.UnexpectedToken) + " expecting " + text2;
			}
			else if (e is MissingTokenException)
			{
				MissingTokenException ex2 = (MissingTokenException)e;
				string text3;
				if (ex2.Expecting == Token.EOF)
				{
					text3 = "EOF";
				}
				else
				{
					text3 = tokenNames[ex2.Expecting];
				}
				text = "missing " + text3 + " at " + this.GetTokenErrorDisplay(e.Token);
			}
			else if (e is MismatchedTokenException)
			{
				MismatchedTokenException ex3 = (MismatchedTokenException)e;
				string text4;
				if (ex3.Expecting == Token.EOF)
				{
					text4 = "EOF";
				}
				else
				{
					text4 = tokenNames[ex3.Expecting];
				}
				text = "mismatched input " + this.GetTokenErrorDisplay(e.Token) + " expecting " + text4;
			}
			else if (e is MismatchedTreeNodeException)
			{
				MismatchedTreeNodeException ex4 = (MismatchedTreeNodeException)e;
				string text5;
				if (ex4.expecting == Token.EOF)
				{
					text5 = "EOF";
				}
				else
				{
					text5 = tokenNames[ex4.expecting];
				}
				text = string.Concat(new object[]
				{
					"mismatched tree node: ",
					(ex4.Node != null && ex4.Node.ToString() != null) ? ex4.Node : string.Empty,
					" expecting ",
					text5
				});
			}
			else if (e is NoViableAltException)
			{
				text = "no viable alternative at input " + this.GetTokenErrorDisplay(e.Token);
			}
			else if (e is EarlyExitException)
			{
				text = "required (...)+ loop did not match anything at input " + this.GetTokenErrorDisplay(e.Token);
			}
			else if (e is MismatchedSetException)
			{
				MismatchedSetException ex5 = (MismatchedSetException)e;
				text = string.Concat(new object[]
				{
					"mismatched input ",
					this.GetTokenErrorDisplay(e.Token),
					" expecting set ",
					ex5.expecting
				});
			}
			else if (e is MismatchedNotSetException)
			{
				MismatchedNotSetException ex6 = (MismatchedNotSetException)e;
				text = string.Concat(new object[]
				{
					"mismatched input ",
					this.GetTokenErrorDisplay(e.Token),
					" expecting set ",
					ex6.expecting
				});
			}
			else if (e is FailedPredicateException)
			{
				FailedPredicateException ex7 = (FailedPredicateException)e;
				text = string.Concat(new string[] { "rule ", ex7.ruleName, " failed predicate: {", ex7.predicateText, "}?" });
			}
			return text;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003F7F File Offset: 0x00002F7F
		public int NumberOfSyntaxErrors
		{
			get
			{
				return this.state.syntaxErrors;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003F8C File Offset: 0x00002F8C
		public virtual string GetErrorHeader(RecognitionException e)
		{
			return string.Concat(new object[] { "line ", e.Line, ":", e.CharPositionInLine });
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003FD4 File Offset: 0x00002FD4
		public virtual string GetTokenErrorDisplay(IToken t)
		{
			string text = t.Text;
			if (text == null)
			{
				if (t.Type == Token.EOF)
				{
					text = "<EOF>";
				}
				else
				{
					text = "<" + t.Type + ">";
				}
			}
			text = text.Replace("\n", "\\\\n");
			text = text.Replace("\r", "\\\\r");
			text = text.Replace("\t", "\\\\t");
			return "'" + text + "'";
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000405E File Offset: 0x0000305E
		public virtual void EmitErrorMessage(string msg)
		{
			Console.Error.WriteLine(msg);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000406C File Offset: 0x0000306C
		public virtual void Recover(IIntStream input, RecognitionException re)
		{
			if (this.state.lastErrorIndex == input.Index())
			{
				input.Consume();
			}
			this.state.lastErrorIndex = input.Index();
			BitSet bitSet = this.ComputeErrorRecoverySet();
			this.BeginResync();
			this.ConsumeUntil(input, bitSet);
			this.EndResync();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000040BE File Offset: 0x000030BE
		public virtual void BeginResync()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000040C0 File Offset: 0x000030C0
		public virtual void EndResync()
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000040C4 File Offset: 0x000030C4
		protected internal virtual object RecoverFromMismatchedToken(IIntStream input, int ttype, BitSet follow)
		{
			RecognitionException ex = null;
			if (this.MismatchIsUnwantedToken(input, ttype))
			{
				ex = new UnwantedTokenException(ttype, input);
				this.BeginResync();
				input.Consume();
				this.EndResync();
				this.ReportError(ex);
				object currentInputSymbol = this.GetCurrentInputSymbol(input);
				input.Consume();
				return currentInputSymbol;
			}
			if (this.MismatchIsMissingToken(input, follow))
			{
				object missingSymbol = this.GetMissingSymbol(input, ex, ttype, follow);
				ex = new MissingTokenException(ttype, input, missingSymbol);
				this.ReportError(ex);
				return missingSymbol;
			}
			ex = new MismatchedTokenException(ttype, input);
			throw ex;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000413E File Offset: 0x0000313E
		public virtual object RecoverFromMismatchedSet(IIntStream input, RecognitionException e, BitSet follow)
		{
			if (this.MismatchIsMissingToken(input, follow))
			{
				this.ReportError(e);
				return this.GetMissingSymbol(input, e, 0, follow);
			}
			throw e;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004160 File Offset: 0x00003160
		public virtual void ConsumeUntil(IIntStream input, int tokenType)
		{
			int num = input.LA(1);
			while (num != Token.EOF && num != tokenType)
			{
				input.Consume();
				num = input.LA(1);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004194 File Offset: 0x00003194
		public virtual void ConsumeUntil(IIntStream input, BitSet set)
		{
			int num = input.LA(1);
			while (num != Token.EOF && !set.Member(num))
			{
				input.Consume();
				num = input.LA(1);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000041CC File Offset: 0x000031CC
		public virtual IList GetRuleInvocationStack()
		{
			string fullName = base.GetType().FullName;
			return BaseRecognizer.GetRuleInvocationStack(new Exception(), fullName);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000041F0 File Offset: 0x000031F0
		public static IList GetRuleInvocationStack(Exception e, string recognizerClassName)
		{
			IList list = new List<object>();
			StackTrace stackTrace = new StackTrace(e);
			for (int i = stackTrace.FrameCount - 1; i >= 0; i--)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				if (!frame.GetMethod().DeclaringType.FullName.StartsWith("Unity.VisualScripting.Antlr3.Runtime.") && !frame.GetMethod().Name.Equals(BaseRecognizer.NEXT_TOKEN_RULE_NAME) && frame.GetMethod().DeclaringType.FullName.Equals(recognizerClassName))
				{
					list.Add(frame.GetMethod().Name);
				}
			}
			return list;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004285 File Offset: 0x00003285
		public virtual string GrammarFileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000132 RID: 306
		public abstract string SourceName { get; }

		// Token: 0x06000133 RID: 307 RVA: 0x00004288 File Offset: 0x00003288
		public virtual IList ToStrings(IList tokens)
		{
			if (tokens == null)
			{
				return null;
			}
			IList list = new List<object>(tokens.Count);
			for (int i = 0; i < tokens.Count; i++)
			{
				list.Add(((IToken)tokens[i]).Text);
			}
			return list;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000042D0 File Offset: 0x000032D0
		public virtual int GetRuleMemoization(int ruleIndex, int ruleStartIndex)
		{
			if (this.state.ruleMemo[ruleIndex] == null)
			{
				this.state.ruleMemo[ruleIndex] = new Hashtable();
			}
			object obj = this.state.ruleMemo[ruleIndex][ruleStartIndex];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004324 File Offset: 0x00003324
		public virtual bool AlreadyParsedRule(IIntStream input, int ruleIndex)
		{
			int ruleMemoization = this.GetRuleMemoization(ruleIndex, input.Index());
			if (ruleMemoization == -1)
			{
				return false;
			}
			if (ruleMemoization == -2)
			{
				this.state.failed = true;
			}
			else
			{
				input.Seek(ruleMemoization + 1);
			}
			return true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004364 File Offset: 0x00003364
		public virtual void Memoize(IIntStream input, int ruleIndex, int ruleStartIndex)
		{
			int num = (this.state.failed ? (-2) : (input.Index() - 1));
			if (this.state.ruleMemo[ruleIndex] != null)
			{
				this.state.ruleMemo[ruleIndex][ruleStartIndex] = num;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000043B8 File Offset: 0x000033B8
		public int GetRuleMemoizationCacheSize()
		{
			int num = 0;
			int num2 = 0;
			while (this.state.ruleMemo != null && num2 < this.state.ruleMemo.Length)
			{
				IDictionary dictionary = this.state.ruleMemo[num2];
				if (dictionary != null)
				{
					num += dictionary.Count;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004408 File Offset: 0x00003408
		public virtual void TraceIn(string ruleName, int ruleIndex, object inputSymbol)
		{
			Console.Out.Write(string.Concat(new object[] { "enter ", ruleName, " ", inputSymbol }));
			if (this.state.backtracking > 0)
			{
				Console.Out.Write(" backtracking=" + this.state.backtracking);
			}
			Console.Out.WriteLine();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004480 File Offset: 0x00003480
		public virtual void TraceOut(string ruleName, int ruleIndex, object inputSymbol)
		{
			Console.Out.Write(string.Concat(new object[] { "exit ", ruleName, " ", inputSymbol }));
			if (this.state.backtracking > 0)
			{
				Console.Out.Write(" backtracking=" + this.state.backtracking);
				if (this.state.failed)
				{
					Console.Out.WriteLine(" failed" + this.state.failed);
				}
				else
				{
					Console.Out.WriteLine(" succeeded" + this.state.failed);
				}
			}
			Console.Out.WriteLine();
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000454F File Offset: 0x0000354F
		public virtual string[] TokenNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004552 File Offset: 0x00003552
		protected internal virtual BitSet ComputeErrorRecoverySet()
		{
			return this.CombineFollows(false);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000455B File Offset: 0x0000355B
		protected internal virtual BitSet ComputeContextSensitiveRuleFOLLOW()
		{
			return this.CombineFollows(true);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004564 File Offset: 0x00003564
		protected internal virtual BitSet CombineFollows(bool exact)
		{
			int followingStackPointer = this.state.followingStackPointer;
			BitSet bitSet = new BitSet();
			for (int i = followingStackPointer; i >= 0; i--)
			{
				BitSet bitSet2 = this.state.following[i];
				bitSet.OrInPlace(bitSet2);
				if (exact)
				{
					if (!bitSet2.Member(1))
					{
						break;
					}
					if (i > 0)
					{
						bitSet.Remove(1);
					}
				}
			}
			return bitSet;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000045BC File Offset: 0x000035BC
		protected virtual object GetCurrentInputSymbol(IIntStream input)
		{
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000045BF File Offset: 0x000035BF
		protected virtual object GetMissingSymbol(IIntStream input, RecognitionException e, int expectedTokenType, BitSet follow)
		{
			return null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000045C4 File Offset: 0x000035C4
		protected void PushFollow(BitSet fset)
		{
			if (this.state.followingStackPointer + 1 >= this.state.following.Length)
			{
				BitSet[] array = new BitSet[this.state.following.Length * 2];
				Array.Copy(this.state.following, 0, array, 0, this.state.following.Length);
				this.state.following = array;
			}
			this.state.following[++this.state.followingStackPointer] = fset;
		}

		// Token: 0x04000038 RID: 56
		public const int MEMO_RULE_FAILED = -2;

		// Token: 0x04000039 RID: 57
		public const int MEMO_RULE_UNKNOWN = -1;

		// Token: 0x0400003A RID: 58
		public const int INITIAL_FOLLOW_STACK_SIZE = 100;

		// Token: 0x0400003B RID: 59
		public const int DEFAULT_TOKEN_CHANNEL = 0;

		// Token: 0x0400003C RID: 60
		public const int HIDDEN = 99;

		// Token: 0x0400003D RID: 61
		public static readonly string NEXT_TOKEN_RULE_NAME = "nextToken";

		// Token: 0x0400003E RID: 62
		protected internal RecognizerSharedState state;
	}
}
