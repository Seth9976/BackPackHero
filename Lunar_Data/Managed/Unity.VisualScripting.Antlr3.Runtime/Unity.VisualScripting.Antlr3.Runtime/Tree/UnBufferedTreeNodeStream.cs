using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000043 RID: 67
	public class UnBufferedTreeNodeStream : ITreeNodeStream, IIntStream
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000074BD File Offset: 0x000064BD
		public virtual object TreeSource
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000074C8 File Offset: 0x000064C8
		public virtual void Reset()
		{
			this.currentNode = this.root;
			this.previousNode = null;
			this.currentChildIndex = -1;
			this.absoluteNodeIndex = -1;
			this.head = (this.tail = 0);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007508 File Offset: 0x00006508
		public virtual bool MoveNext()
		{
			if (this.currentNode == null)
			{
				this.AddLookahead(this.eof);
				this.currentEnumerationNode = null;
				return false;
			}
			if (this.currentChildIndex == -1)
			{
				this.currentEnumerationNode = (ITree)this.handleRootNode();
				return true;
			}
			if (this.currentChildIndex < this.adaptor.GetChildCount(this.currentNode))
			{
				this.currentEnumerationNode = (ITree)this.VisitChild(this.currentChildIndex);
				return true;
			}
			this.WalkBackToMostRecentNodeWithUnvisitedChildren();
			if (this.currentNode != null)
			{
				this.currentEnumerationNode = (ITree)this.VisitChild(this.currentChildIndex);
				return true;
			}
			return false;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000075A8 File Offset: 0x000065A8
		public virtual object Current
		{
			get
			{
				return this.currentEnumerationNode;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000075B0 File Offset: 0x000065B0
		public UnBufferedTreeNodeStream(object tree)
			: this(new CommonTreeAdaptor(), tree)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000075C0 File Offset: 0x000065C0
		public UnBufferedTreeNodeStream(ITreeAdaptor adaptor, object tree)
		{
			this.root = tree;
			this.adaptor = adaptor;
			this.Reset();
			this.down = adaptor.Create(2, "DOWN");
			this.up = adaptor.Create(3, "UP");
			this.eof = adaptor.Create(Token.EOF, "EOF");
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007643 File Offset: 0x00006643
		public virtual object Get(int i)
		{
			throw new NotSupportedException("stream is unbuffered");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007650 File Offset: 0x00006650
		public virtual object LT(int k)
		{
			if (k == -1)
			{
				return this.previousNode;
			}
			if (k < 0)
			{
				throw new ArgumentNullException("tree node streams cannot look backwards more than 1 node", "k");
			}
			if (k == 0)
			{
				return Tree.INVALID_NODE;
			}
			this.fill(k);
			return this.lookahead[(this.head + k - 1) % this.lookahead.Length];
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000076A8 File Offset: 0x000066A8
		protected internal virtual void fill(int k)
		{
			int lookaheadSize = this.LookaheadSize;
			for (int i = 1; i <= k - lookaheadSize; i++)
			{
				this.MoveNext();
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000076D4 File Offset: 0x000066D4
		protected internal virtual void AddLookahead(object node)
		{
			this.lookahead[this.tail] = node;
			this.tail = (this.tail + 1) % this.lookahead.Length;
			if (this.tail == this.head)
			{
				object[] array = new object[2 * this.lookahead.Length];
				int num = this.lookahead.Length - this.head;
				Array.Copy(this.lookahead, this.head, array, 0, num);
				Array.Copy(this.lookahead, 0, array, num, this.tail);
				this.lookahead = array;
				this.head = 0;
				this.tail += num;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007778 File Offset: 0x00006778
		public virtual void Consume()
		{
			this.fill(1);
			this.absoluteNodeIndex++;
			this.previousNode = this.lookahead[this.head];
			this.head = (this.head + 1) % this.lookahead.Length;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000077C4 File Offset: 0x000067C4
		public virtual int LA(int i)
		{
			object obj = (ITree)this.LT(i);
			if (obj == null)
			{
				return 0;
			}
			return this.adaptor.GetNodeType(obj);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000077F0 File Offset: 0x000067F0
		public virtual int Mark()
		{
			if (this.markers == null)
			{
				this.markers = new List<object>();
				this.markers.Add(null);
			}
			this.markDepth++;
			UnBufferedTreeNodeStream.TreeWalkState treeWalkState;
			if (this.markDepth >= this.markers.Count)
			{
				treeWalkState = new UnBufferedTreeNodeStream.TreeWalkState();
				this.markers.Add(treeWalkState);
			}
			else
			{
				treeWalkState = (UnBufferedTreeNodeStream.TreeWalkState)this.markers[this.markDepth];
			}
			treeWalkState.absoluteNodeIndex = this.absoluteNodeIndex;
			treeWalkState.currentChildIndex = this.currentChildIndex;
			treeWalkState.currentNode = this.currentNode;
			treeWalkState.previousNode = this.previousNode;
			treeWalkState.nodeStackSize = this.nodeStack.Count;
			treeWalkState.indexStackSize = this.indexStack.Count;
			int lookaheadSize = this.LookaheadSize;
			int num = 0;
			treeWalkState.lookahead = new object[lookaheadSize];
			int i = 1;
			while (i <= lookaheadSize)
			{
				treeWalkState.lookahead[num] = this.LT(i);
				i++;
				num++;
			}
			this.lastMarker = this.markDepth;
			return this.markDepth;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007904 File Offset: 0x00006904
		public virtual void Release(int marker)
		{
			this.markDepth = marker;
			this.markDepth--;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000791C File Offset: 0x0000691C
		public virtual void Rewind(int marker)
		{
			if (this.markers == null)
			{
				return;
			}
			UnBufferedTreeNodeStream.TreeWalkState treeWalkState = (UnBufferedTreeNodeStream.TreeWalkState)this.markers[marker];
			this.absoluteNodeIndex = treeWalkState.absoluteNodeIndex;
			this.currentChildIndex = treeWalkState.currentChildIndex;
			this.currentNode = treeWalkState.currentNode;
			this.previousNode = treeWalkState.previousNode;
			this.nodeStack.Capacity = treeWalkState.nodeStackSize;
			this.indexStack.Capacity = treeWalkState.indexStackSize;
			this.head = (this.tail = 0);
			while (this.tail < treeWalkState.lookahead.Length)
			{
				this.lookahead[this.tail] = treeWalkState.lookahead[this.tail];
				this.tail++;
			}
			this.Release(marker);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000079E7 File Offset: 0x000069E7
		public void Rewind()
		{
			this.Rewind(this.lastMarker);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000079F5 File Offset: 0x000069F5
		public virtual void Seek(int index)
		{
			if (index < this.Index())
			{
				throw new ArgumentOutOfRangeException("can't seek backwards in node stream", "index");
			}
			while (this.Index() < index)
			{
				this.Consume();
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007A1F File Offset: 0x00006A1F
		public virtual int Index()
		{
			return this.absoluteNodeIndex + 1;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007A29 File Offset: 0x00006A29
		[Obsolete("Please use property Count instead.")]
		public virtual int Size()
		{
			return this.Count;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007A34 File Offset: 0x00006A34
		public virtual int Count
		{
			get
			{
				CommonTreeNodeStream commonTreeNodeStream = new CommonTreeNodeStream(this.root);
				return commonTreeNodeStream.Count;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007A54 File Offset: 0x00006A54
		protected internal virtual object handleRootNode()
		{
			object obj = this.currentNode;
			this.currentChildIndex = 0;
			if (this.adaptor.IsNil(obj))
			{
				obj = this.VisitChild(this.currentChildIndex);
			}
			else
			{
				this.AddLookahead(obj);
				if (this.adaptor.GetChildCount(this.currentNode) == 0)
				{
					this.currentNode = null;
				}
			}
			return obj;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007AB0 File Offset: 0x00006AB0
		protected internal virtual object VisitChild(int child)
		{
			this.nodeStack.Push(this.currentNode);
			this.indexStack.Push(child);
			if (child == 0 && !this.adaptor.IsNil(this.currentNode))
			{
				this.AddNavigationNode(2);
			}
			this.currentNode = this.adaptor.GetChild(this.currentNode, child);
			this.currentChildIndex = 0;
			object obj = this.currentNode;
			this.AddLookahead(obj);
			this.WalkBackToMostRecentNodeWithUnvisitedChildren();
			return obj;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007B34 File Offset: 0x00006B34
		protected internal virtual void AddNavigationNode(int ttype)
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
			this.AddLookahead(obj);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007B98 File Offset: 0x00006B98
		protected internal virtual void WalkBackToMostRecentNodeWithUnvisitedChildren()
		{
			while (this.currentNode != null && this.currentChildIndex >= this.adaptor.GetChildCount(this.currentNode))
			{
				this.currentNode = this.nodeStack.Pop();
				if (this.currentNode == null)
				{
					return;
				}
				this.currentChildIndex = (int)this.indexStack.Pop();
				this.currentChildIndex++;
				if (this.currentChildIndex >= this.adaptor.GetChildCount(this.currentNode))
				{
					if (!this.adaptor.IsNil(this.currentNode))
					{
						this.AddNavigationNode(3);
					}
					if (this.currentNode == this.root)
					{
						this.currentNode = null;
					}
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00007C54 File Offset: 0x00006C54
		public ITreeAdaptor TreeAdaptor
		{
			get
			{
				return this.adaptor;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00007C5C File Offset: 0x00006C5C
		public string SourceName
		{
			get
			{
				return this.TokenStream.SourceName;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00007C69 File Offset: 0x00006C69
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00007C71 File Offset: 0x00006C71
		public ITokenStream TokenStream
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00007C7A File Offset: 0x00006C7A
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00007C82 File Offset: 0x00006C82
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

		// Token: 0x0600028A RID: 650 RVA: 0x00007C8B File Offset: 0x00006C8B
		public void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			throw new NotSupportedException("can't do stream rewrites yet");
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007C97 File Offset: 0x00006C97
		public override string ToString()
		{
			return this.ToString(this.root, null);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00007CA6 File Offset: 0x00006CA6
		protected int LookaheadSize
		{
			get
			{
				if (this.tail >= this.head)
				{
					return this.tail - this.head;
				}
				return this.lookahead.Length - this.head + this.tail;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007CDC File Offset: 0x00006CDC
		public virtual string ToString(object start, object stop)
		{
			if (start == null)
			{
				return null;
			}
			if (this.tokens != null)
			{
				int tokenStartIndex = this.adaptor.GetTokenStartIndex(start);
				int num = this.adaptor.GetTokenStopIndex(stop);
				if (stop != null && this.adaptor.GetNodeType(stop) == 3)
				{
					num = this.adaptor.GetTokenStopIndex(start);
				}
				else
				{
					num = this.Count - 1;
				}
				return this.tokens.ToString(tokenStartIndex, num);
			}
			StringBuilder stringBuilder = new StringBuilder();
			this.ToStringWork(start, stop, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007D60 File Offset: 0x00006D60
		protected internal virtual void ToStringWork(object p, object stop, StringBuilder buf)
		{
			if (!this.adaptor.IsNil(p))
			{
				string text = this.adaptor.GetNodeText(p);
				if (text == null)
				{
					text = " " + this.adaptor.GetNodeType(p);
				}
				buf.Append(text);
			}
			if (p == stop)
			{
				return;
			}
			int childCount = this.adaptor.GetChildCount(p);
			if (childCount > 0 && !this.adaptor.IsNil(p))
			{
				buf.Append(" ");
				buf.Append(2);
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(p, i);
				this.ToStringWork(child, stop, buf);
			}
			if (childCount > 0 && !this.adaptor.IsNil(p))
			{
				buf.Append(" ");
				buf.Append(3);
			}
		}

		// Token: 0x040000AD RID: 173
		public const int INITIAL_LOOKAHEAD_BUFFER_SIZE = 5;

		// Token: 0x040000AE RID: 174
		private ITree currentEnumerationNode;

		// Token: 0x040000AF RID: 175
		protected bool uniqueNavigationNodes;

		// Token: 0x040000B0 RID: 176
		protected internal object root;

		// Token: 0x040000B1 RID: 177
		protected ITokenStream tokens;

		// Token: 0x040000B2 RID: 178
		private ITreeAdaptor adaptor;

		// Token: 0x040000B3 RID: 179
		protected internal StackList nodeStack = new StackList();

		// Token: 0x040000B4 RID: 180
		protected internal StackList indexStack = new StackList();

		// Token: 0x040000B5 RID: 181
		protected internal object currentNode;

		// Token: 0x040000B6 RID: 182
		protected internal object previousNode;

		// Token: 0x040000B7 RID: 183
		protected internal int currentChildIndex;

		// Token: 0x040000B8 RID: 184
		protected int absoluteNodeIndex;

		// Token: 0x040000B9 RID: 185
		protected internal object[] lookahead = new object[5];

		// Token: 0x040000BA RID: 186
		protected internal int head;

		// Token: 0x040000BB RID: 187
		protected internal int tail;

		// Token: 0x040000BC RID: 188
		protected IList markers;

		// Token: 0x040000BD RID: 189
		protected int markDepth;

		// Token: 0x040000BE RID: 190
		protected int lastMarker;

		// Token: 0x040000BF RID: 191
		protected object down;

		// Token: 0x040000C0 RID: 192
		protected object up;

		// Token: 0x040000C1 RID: 193
		protected object eof;

		// Token: 0x02000044 RID: 68
		protected class TreeWalkState
		{
			// Token: 0x040000C2 RID: 194
			protected internal int currentChildIndex;

			// Token: 0x040000C3 RID: 195
			protected internal int absoluteNodeIndex;

			// Token: 0x040000C4 RID: 196
			protected internal object currentNode;

			// Token: 0x040000C5 RID: 197
			protected internal object previousNode;

			// Token: 0x040000C6 RID: 198
			protected internal int nodeStackSize;

			// Token: 0x040000C7 RID: 199
			protected internal int indexStackSize;

			// Token: 0x040000C8 RID: 200
			protected internal object[] lookahead;
		}
	}
}
