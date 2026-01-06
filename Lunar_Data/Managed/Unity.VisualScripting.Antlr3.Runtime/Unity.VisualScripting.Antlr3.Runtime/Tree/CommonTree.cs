using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class CommonTree : BaseTree
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003174 File Offset: 0x00002174
		public CommonTree()
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003194 File Offset: 0x00002194
		public CommonTree(CommonTree node)
			: base(node)
		{
			this.token = node.token;
			this.startIndex = node.startIndex;
			this.stopIndex = node.stopIndex;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000031E1 File Offset: 0x000021E1
		public CommonTree(IToken t)
		{
			this.token = t;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003205 File Offset: 0x00002205
		public virtual IToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000320D File Offset: 0x0000220D
		public override bool IsNil
		{
			get
			{
				return this.token == null;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003218 File Offset: 0x00002218
		public override int Type
		{
			get
			{
				if (this.token == null)
				{
					return 0;
				}
				return this.token.Type;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000322F File Offset: 0x0000222F
		public override string Text
		{
			get
			{
				if (this.token == null)
				{
					return null;
				}
				return this.token.Text;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003246 File Offset: 0x00002246
		public override int Line
		{
			get
			{
				if (this.token != null && this.token.Line != 0)
				{
					return this.token.Line;
				}
				if (this.ChildCount > 0)
				{
					return this.GetChild(0).Line;
				}
				return 0;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003280 File Offset: 0x00002280
		public override int CharPositionInLine
		{
			get
			{
				if (this.token != null && this.token.CharPositionInLine != -1)
				{
					return this.token.CharPositionInLine;
				}
				if (this.ChildCount > 0)
				{
					return this.GetChild(0).CharPositionInLine;
				}
				return 0;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000032BB File Offset: 0x000022BB
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000032E0 File Offset: 0x000022E0
		public override int TokenStartIndex
		{
			get
			{
				if (this.startIndex == -1 && this.token != null)
				{
					return this.token.TokenIndex;
				}
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000032E9 File Offset: 0x000022E9
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000330E File Offset: 0x0000230E
		public override int TokenStopIndex
		{
			get
			{
				if (this.stopIndex == -1 && this.token != null)
				{
					return this.token.TokenIndex;
				}
				return this.stopIndex;
			}
			set
			{
				this.stopIndex = value;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003318 File Offset: 0x00002318
		public void SetUnknownTokenBoundaries()
		{
			if (this.children == null)
			{
				if (this.startIndex < 0 || this.stopIndex < 0)
				{
					this.startIndex = (this.stopIndex = this.token.TokenIndex);
				}
				return;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				((CommonTree)this.children[i]).SetUnknownTokenBoundaries();
			}
			if (this.startIndex >= 0 && this.stopIndex >= 0)
			{
				return;
			}
			if (this.children.Count > 0)
			{
				CommonTree commonTree = (CommonTree)this.children[0];
				CommonTree commonTree2 = (CommonTree)this.children[this.children.Count - 1];
				this.startIndex = commonTree.TokenStartIndex;
				this.stopIndex = commonTree2.TokenStopIndex;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000033EF File Offset: 0x000023EF
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000033F7 File Offset: 0x000023F7
		public override int ChildIndex
		{
			get
			{
				return this.childIndex;
			}
			set
			{
				this.childIndex = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003400 File Offset: 0x00002400
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003408 File Offset: 0x00002408
		public override ITree Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = (CommonTree)value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003416 File Offset: 0x00002416
		public override ITree DupNode()
		{
			return new CommonTree(this);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000341E File Offset: 0x0000241E
		public override string ToString()
		{
			if (this.IsNil)
			{
				return "nil";
			}
			if (this.Type == 0)
			{
				return "<errornode>";
			}
			if (this.token == null)
			{
				return null;
			}
			return this.token.Text;
		}

		// Token: 0x0400001F RID: 31
		public int startIndex = -1;

		// Token: 0x04000020 RID: 32
		public int stopIndex = -1;

		// Token: 0x04000021 RID: 33
		protected IToken token;

		// Token: 0x04000022 RID: 34
		public CommonTree parent;

		// Token: 0x04000023 RID: 35
		public int childIndex = -1;
	}
}
