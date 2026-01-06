using System;
using System.Text.RegularExpressions;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000058 RID: 88
	public class TreeParser : BaseRecognizer
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00009CBF File Offset: 0x00008CBF
		public TreeParser(ITreeNodeStream input)
		{
			this.TreeNodeStream = input;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009CCE File Offset: 0x00008CCE
		public TreeParser(ITreeNodeStream input, RecognizerSharedState state)
			: base(state)
		{
			this.TreeNodeStream = input;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009CDE File Offset: 0x00008CDE
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00009CE6 File Offset: 0x00008CE6
		public virtual ITreeNodeStream TreeNodeStream
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00009CEF File Offset: 0x00008CEF
		public override string SourceName
		{
			get
			{
				return this.input.SourceName;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009CFC File Offset: 0x00008CFC
		protected override object GetCurrentInputSymbol(IIntStream input)
		{
			return ((ITreeNodeStream)input).LT(1);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009D0C File Offset: 0x00008D0C
		protected override object GetMissingSymbol(IIntStream input, RecognitionException e, int expectedTokenType, BitSet follow)
		{
			string text = "<missing " + this.TokenNames[expectedTokenType] + ">";
			return new CommonTree(new CommonToken(expectedTokenType, text));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009D3D File Offset: 0x00008D3D
		public override void Reset()
		{
			base.Reset();
			if (this.input != null)
			{
				this.input.Seek(0);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00009D5C File Offset: 0x00008D5C
		public override void MatchAny(IIntStream ignore)
		{
			this.state.errorRecovery = false;
			this.state.failed = false;
			object obj = this.input.LT(1);
			if (this.input.TreeAdaptor.GetChildCount(obj) == 0)
			{
				this.input.Consume();
				return;
			}
			int num = 0;
			int num2 = this.input.TreeAdaptor.GetNodeType(obj);
			while (num2 != Token.EOF && (num2 != 3 || num != 0))
			{
				this.input.Consume();
				obj = this.input.LT(1);
				num2 = this.input.TreeAdaptor.GetNodeType(obj);
				if (num2 == 2)
				{
					num++;
				}
				else if (num2 == 3)
				{
					num--;
				}
			}
			this.input.Consume();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00009E19 File Offset: 0x00008E19
		public override IIntStream Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00009E21 File Offset: 0x00008E21
		protected internal override object RecoverFromMismatchedToken(IIntStream input, int ttype, BitSet follow)
		{
			throw new MismatchedTreeNodeException(ttype, (ITreeNodeStream)input);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009E30 File Offset: 0x00008E30
		public override string GetErrorHeader(RecognitionException e)
		{
			return string.Concat(new object[]
			{
				this.GrammarFileName,
				": node from ",
				e.approximateLineInfo ? "after " : "",
				"line ",
				e.Line,
				":",
				e.CharPositionInLine
			});
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009EA0 File Offset: 0x00008EA0
		public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
		{
			if (this != null)
			{
				ITreeAdaptor treeAdaptor = ((ITreeNodeStream)e.Input).TreeAdaptor;
				e.Token = treeAdaptor.GetToken(e.Node);
				if (e.Token == null)
				{
					e.Token = new CommonToken(treeAdaptor.GetNodeType(e.Node), treeAdaptor.GetNodeText(e.Node));
				}
			}
			return base.GetErrorMessage(e, tokenNames);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00009F06 File Offset: 0x00008F06
		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			base.TraceIn(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009F1C File Offset: 0x00008F1C
		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			base.TraceOut(ruleName, ruleIndex, this.input.LT(1));
		}

		// Token: 0x040000EF RID: 239
		public const int DOWN = 2;

		// Token: 0x040000F0 RID: 240
		public const int UP = 3;

		// Token: 0x040000F1 RID: 241
		private static readonly string dotdot = ".*[^.]\\.\\.[^.].*";

		// Token: 0x040000F2 RID: 242
		private static readonly string doubleEtc = ".*\\.\\.\\.\\s+\\.\\.\\..*";

		// Token: 0x040000F3 RID: 243
		private static readonly string spaces = "\\s+";

		// Token: 0x040000F4 RID: 244
		private static readonly Regex dotdotPattern = new Regex(TreeParser.dotdot, 8);

		// Token: 0x040000F5 RID: 245
		private static readonly Regex doubleEtcPattern = new Regex(TreeParser.doubleEtc, 8);

		// Token: 0x040000F6 RID: 246
		private static readonly Regex spacesPattern = new Regex(TreeParser.spaces, 8);

		// Token: 0x040000F7 RID: 247
		protected internal ITreeNodeStream input;
	}
}
