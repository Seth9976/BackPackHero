using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class RecognitionException : Exception
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000281C File Offset: 0x0000181C
		public RecognitionException()
			: this(null, null, null)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002827 File Offset: 0x00001827
		public RecognitionException(string message)
			: this(message, null, null)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002832 File Offset: 0x00001832
		public RecognitionException(string message, Exception inner)
			: this(message, inner, null)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000283D File Offset: 0x0000183D
		public RecognitionException(IIntStream input)
			: this(null, null, input)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002848 File Offset: 0x00001848
		public RecognitionException(string message, IIntStream input)
			: this(message, null, input)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002854 File Offset: 0x00001854
		public RecognitionException(string message, Exception inner, IIntStream input)
			: base(message, inner)
		{
			this.input = input;
			this.index = input.Index();
			if (input is ITokenStream)
			{
				this.token = ((ITokenStream)input).LT(1);
				this.line = this.token.Line;
				this.charPositionInLine = this.token.CharPositionInLine;
			}
			if (input is ITreeNodeStream)
			{
				this.ExtractInformationFromTreeNodeStream(input);
				return;
			}
			if (input is ICharStream)
			{
				this.c = input.LA(1);
				this.line = ((ICharStream)input).Line;
				this.charPositionInLine = ((ICharStream)input).CharPositionInLine;
				return;
			}
			this.c = input.LA(1);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000290D File Offset: 0x0000190D
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002915 File Offset: 0x00001915
		public IIntStream Input
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000291E File Offset: 0x0000191E
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002926 File Offset: 0x00001926
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000292F File Offset: 0x0000192F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002937 File Offset: 0x00001937
		public IToken Token
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002940 File Offset: 0x00001940
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002948 File Offset: 0x00001948
		public object Node
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002951 File Offset: 0x00001951
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002959 File Offset: 0x00001959
		public int Char
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002962 File Offset: 0x00001962
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000296A File Offset: 0x0000196A
		public int CharPositionInLine
		{
			get
			{
				return this.charPositionInLine;
			}
			set
			{
				this.charPositionInLine = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002973 File Offset: 0x00001973
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000297B File Offset: 0x0000197B
		public int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002984 File Offset: 0x00001984
		public virtual int UnexpectedType
		{
			get
			{
				if (this.input is ITokenStream)
				{
					return this.token.Type;
				}
				if (this.input is ITreeNodeStream)
				{
					ITreeNodeStream treeNodeStream = (ITreeNodeStream)this.input;
					ITreeAdaptor treeAdaptor = treeNodeStream.TreeAdaptor;
					return treeAdaptor.GetNodeType(this.node);
				}
				return this.c;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000029E0 File Offset: 0x000019E0
		protected void ExtractInformationFromTreeNodeStream(IIntStream input)
		{
			ITreeNodeStream treeNodeStream = (ITreeNodeStream)input;
			this.node = treeNodeStream.LT(1);
			ITreeAdaptor treeAdaptor = treeNodeStream.TreeAdaptor;
			IToken token = treeAdaptor.GetToken(this.node);
			if (token == null)
			{
				if (this.node is ITree)
				{
					this.line = ((ITree)this.node).Line;
					this.charPositionInLine = ((ITree)this.node).CharPositionInLine;
					if (this.node is CommonTree)
					{
						this.token = ((CommonTree)this.node).Token;
						return;
					}
				}
				else
				{
					int nodeType = treeAdaptor.GetNodeType(this.node);
					string nodeText = treeAdaptor.GetNodeText(this.node);
					this.token = new CommonToken(nodeType, nodeText);
				}
				return;
			}
			this.token = token;
			if (token.Line <= 0)
			{
				int num = -1;
				for (object obj = treeNodeStream.LT(num); obj != null; obj = treeNodeStream.LT(num))
				{
					IToken token2 = treeAdaptor.GetToken(obj);
					if (token2 != null && token2.Line > 0)
					{
						this.line = token2.Line;
						this.charPositionInLine = token2.CharPositionInLine;
						this.approximateLineInfo = true;
						return;
					}
					num--;
				}
				return;
			}
			this.line = token.Line;
			this.charPositionInLine = token.CharPositionInLine;
		}

		// Token: 0x04000011 RID: 17
		[NonSerialized]
		protected IIntStream input;

		// Token: 0x04000012 RID: 18
		protected int index;

		// Token: 0x04000013 RID: 19
		protected IToken token;

		// Token: 0x04000014 RID: 20
		protected object node;

		// Token: 0x04000015 RID: 21
		protected int c;

		// Token: 0x04000016 RID: 22
		protected int line;

		// Token: 0x04000017 RID: 23
		protected int charPositionInLine;

		// Token: 0x04000018 RID: 24
		public bool approximateLineInfo;
	}
}
