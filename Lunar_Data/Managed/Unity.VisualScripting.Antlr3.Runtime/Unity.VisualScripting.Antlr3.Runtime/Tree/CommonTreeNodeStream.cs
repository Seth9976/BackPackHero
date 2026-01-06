using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000046 RID: 70
	public class CommonTreeNodeStream : ITreeNodeStream, IIntStream, IEnumerable
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00007E87 File Offset: 0x00006E87
		public IEnumerator GetEnumerator()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			return new CommonTreeNodeStream.CommonTreeNodeStreamEnumerator(this);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007E9E File Offset: 0x00006E9E
		public CommonTreeNodeStream(object tree)
			: this(new CommonTreeAdaptor(), tree)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007EAC File Offset: 0x00006EAC
		public CommonTreeNodeStream(ITreeAdaptor adaptor, object tree)
			: this(adaptor, tree, 100)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007EB8 File Offset: 0x00006EB8
		public CommonTreeNodeStream(ITreeAdaptor adaptor, object tree, int initialBufferSize)
		{
			this.root = tree;
			this.adaptor = adaptor;
			this.nodes = new List<object>(initialBufferSize);
			this.down = adaptor.Create(2, "DOWN");
			this.up = adaptor.Create(3, "UP");
			this.eof = adaptor.Create(Token.EOF, "EOF");
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007F26 File Offset: 0x00006F26
		protected void FillBuffer()
		{
			this.FillBuffer(this.root);
			this.p = 0;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007F3C File Offset: 0x00006F3C
		public void FillBuffer(object t)
		{
			bool flag = this.adaptor.IsNil(t);
			if (!flag)
			{
				this.nodes.Add(t);
			}
			int childCount = this.adaptor.GetChildCount(t);
			if (!flag && childCount > 0)
			{
				this.AddNavigationNode(2);
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this.FillBuffer(child);
			}
			if (!flag && childCount > 0)
			{
				this.AddNavigationNode(3);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007FB0 File Offset: 0x00006FB0
		protected int GetNodeIndex(object node)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			for (int i = 0; i < this.nodes.Count; i++)
			{
				object obj = this.nodes[i];
				if (obj == node)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007FF8 File Offset: 0x00006FF8
		protected void AddNavigationNode(int ttype)
		{
			object obj;
			if (ttype == 2)
			{
				if (this.HasUniqueNavigationNodes)
				{
					obj = this.adaptor.Create(2, "DOWN");
				}
				else
				{
					obj = this.down;
				}
			}
			else if (this.HasUniqueNavigationNodes)
			{
				obj = this.adaptor.Create(3, "UP");
			}
			else
			{
				obj = this.up;
			}
			this.nodes.Add(obj);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008060 File Offset: 0x00007060
		public object Get(int i)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			return this.nodes[i];
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008080 File Offset: 0x00007080
		public object LT(int k)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (k < 0)
			{
				return this.LB(-k);
			}
			if (this.p + k - 1 >= this.nodes.Count)
			{
				return this.eof;
			}
			return this.nodes[this.p + k - 1];
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000080E1 File Offset: 0x000070E1
		public virtual object CurrentSymbol
		{
			get
			{
				return this.LT(1);
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000080EA File Offset: 0x000070EA
		protected object LB(int k)
		{
			if (k == 0)
			{
				return null;
			}
			if (this.p - k < 0)
			{
				return null;
			}
			return this.nodes[this.p - k];
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00008111 File Offset: 0x00007111
		public virtual object TreeSource
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008119 File Offset: 0x00007119
		public virtual string SourceName
		{
			get
			{
				return this.TokenStream.SourceName;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00008126 File Offset: 0x00007126
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000812E File Offset: 0x0000712E
		public virtual ITokenStream TokenStream
		{
			get
			{
				return this.tokens;
			}
			set
			{
				this.tokens = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008137 File Offset: 0x00007137
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000813F File Offset: 0x0000713F
		public ITreeAdaptor TreeAdaptor
		{
			get
			{
				return this.adaptor;
			}
			set
			{
				this.adaptor = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008148 File Offset: 0x00007148
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008150 File Offset: 0x00007150
		public bool HasUniqueNavigationNodes
		{
			get
			{
				return this.uniqueNavigationNodes;
			}
			set
			{
				this.uniqueNavigationNodes = value;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00008159 File Offset: 0x00007159
		public void Push(int index)
		{
			if (this.calls == null)
			{
				this.calls = new StackList();
			}
			this.calls.Push(this.p);
			this.Seek(index);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000818C File Offset: 0x0000718C
		public int Pop()
		{
			int num = (int)this.calls.Pop();
			this.Seek(num);
			return num;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000081B2 File Offset: 0x000071B2
		public void Reset()
		{
			this.p = -1;
			this.lastMarker = 0;
			if (this.calls != null)
			{
				this.calls.Clear();
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000081D5 File Offset: 0x000071D5
		public void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			if (parent != null)
			{
				this.adaptor.ReplaceChildren(parent, startChildIndex, stopChildIndex, t);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000081EA File Offset: 0x000071EA
		public virtual void Consume()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.p++;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008209 File Offset: 0x00007209
		public virtual int LA(int i)
		{
			return this.adaptor.GetNodeType(this.LT(i));
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000821D File Offset: 0x0000721D
		public virtual int Mark()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.lastMarker = this.Index();
			return this.lastMarker;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008240 File Offset: 0x00007240
		public virtual void Release(int marker)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008242 File Offset: 0x00007242
		public virtual void Rewind(int marker)
		{
			this.Seek(marker);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000824B File Offset: 0x0000724B
		public void Rewind()
		{
			this.Seek(this.lastMarker);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008259 File Offset: 0x00007259
		public virtual void Seek(int index)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.p = index;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008271 File Offset: 0x00007271
		public virtual int Index()
		{
			return this.p;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008279 File Offset: 0x00007279
		[Obsolete("Please use property Count instead.")]
		public virtual int Size()
		{
			return this.Count;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00008281 File Offset: 0x00007281
		public virtual int Count
		{
			get
			{
				if (this.p == -1)
				{
					this.FillBuffer();
				}
				return this.nodes.Count;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000082A0 File Offset: 0x000072A0
		public override string ToString()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.nodes.Count; i++)
			{
				object obj = this.nodes[i];
				stringBuilder.Append(" ");
				stringBuilder.Append(this.adaptor.GetNodeType(obj));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000830C File Offset: 0x0000730C
		public string ToTokenString(int start, int stop)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num < this.nodes.Count && num <= stop)
			{
				object obj = this.nodes[num];
				stringBuilder.Append(" ");
				stringBuilder.Append(this.adaptor.GetToken(obj));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000837C File Offset: 0x0000737C
		public virtual string ToString(object start, object stop)
		{
			Console.Out.WriteLine("ToString");
			if (start == null || stop == null)
			{
				return null;
			}
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (start is CommonTree)
			{
				Console.Out.Write("ToString: " + ((CommonTree)start).Token + ", ");
			}
			else
			{
				Console.Out.WriteLine(start);
			}
			if (stop is CommonTree)
			{
				Console.Out.WriteLine(((CommonTree)stop).Token);
			}
			else
			{
				Console.Out.WriteLine(stop);
			}
			if (this.tokens != null)
			{
				int tokenStartIndex = this.adaptor.GetTokenStartIndex(start);
				int num = this.adaptor.GetTokenStopIndex(stop);
				if (this.adaptor.GetNodeType(stop) == 3)
				{
					num = this.adaptor.GetTokenStopIndex(start);
				}
				else if (this.adaptor.GetNodeType(stop) == Token.EOF)
				{
					num = this.Count - 2;
				}
				return this.tokens.ToString(tokenStartIndex, num);
			}
			int i;
			for (i = 0; i < this.nodes.Count; i++)
			{
				object obj = this.nodes[i];
				if (obj == start)
				{
					break;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			for (object obj = this.nodes[i]; obj != stop; obj = this.nodes[i])
			{
				text = this.adaptor.GetNodeText(obj);
				if (text == null)
				{
					text = " " + this.adaptor.GetNodeType(obj);
				}
				stringBuilder.Append(text);
				i++;
			}
			text = this.adaptor.GetNodeText(stop);
			if (text == null)
			{
				text = " " + this.adaptor.GetNodeType(stop);
			}
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		// Token: 0x040000C9 RID: 201
		public const int DEFAULT_INITIAL_BUFFER_SIZE = 100;

		// Token: 0x040000CA RID: 202
		public const int INITIAL_CALL_STACK_SIZE = 10;

		// Token: 0x040000CB RID: 203
		protected object down;

		// Token: 0x040000CC RID: 204
		protected object up;

		// Token: 0x040000CD RID: 205
		protected object eof;

		// Token: 0x040000CE RID: 206
		protected IList nodes;

		// Token: 0x040000CF RID: 207
		protected internal object root;

		// Token: 0x040000D0 RID: 208
		protected ITokenStream tokens;

		// Token: 0x040000D1 RID: 209
		private ITreeAdaptor adaptor;

		// Token: 0x040000D2 RID: 210
		protected bool uniqueNavigationNodes;

		// Token: 0x040000D3 RID: 211
		protected int p = -1;

		// Token: 0x040000D4 RID: 212
		protected int lastMarker;

		// Token: 0x040000D5 RID: 213
		protected StackList calls;

		// Token: 0x02000048 RID: 72
		protected sealed class CommonTreeNodeStreamEnumerator : IEnumerator
		{
			// Token: 0x060002BC RID: 700 RVA: 0x00008758 File Offset: 0x00007758
			internal CommonTreeNodeStreamEnumerator()
			{
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00008760 File Offset: 0x00007760
			internal CommonTreeNodeStreamEnumerator(CommonTreeNodeStream nodeStream)
			{
				this._nodeStream = nodeStream;
				this.Reset();
			}

			// Token: 0x060002BE RID: 702 RVA: 0x00008775 File Offset: 0x00007775
			public void Reset()
			{
				this._index = 0;
				this._currentItem = null;
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060002BF RID: 703 RVA: 0x00008785 File Offset: 0x00007785
			public object Current
			{
				get
				{
					if (this._currentItem == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentItem;
				}
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x000087A0 File Offset: 0x000077A0
			public bool MoveNext()
			{
				if (this._index >= this._nodeStream.nodes.Count)
				{
					int index = this._index;
					this._index++;
					if (index < this._nodeStream.nodes.Count)
					{
						this._currentItem = this._nodeStream.nodes[index];
					}
					this._currentItem = this._nodeStream.eof;
					return true;
				}
				this._currentItem = null;
				return false;
			}

			// Token: 0x040000D6 RID: 214
			private CommonTreeNodeStream _nodeStream;

			// Token: 0x040000D7 RID: 215
			private int _index;

			// Token: 0x040000D8 RID: 216
			private object _currentItem;
		}
	}
}
