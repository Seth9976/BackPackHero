using System;
using System.Collections;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200000D RID: 13
	public abstract class BaseTreeAdaptor : ITreeAdaptor
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002B25 File Offset: 0x00001B25
		public virtual object GetNilNode()
		{
			return this.Create(null);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002B30 File Offset: 0x00001B30
		public virtual object ErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException e)
		{
			return new CommonErrorNode(input, start, stop, e);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002B49 File Offset: 0x00001B49
		public virtual bool IsNil(object tree)
		{
			return ((ITree)tree).IsNil;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002B56 File Offset: 0x00001B56
		public virtual object DupTree(object tree)
		{
			return this.DupTree(tree, null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002B60 File Offset: 0x00001B60
		public virtual object DupTree(object t, object parent)
		{
			if (t == null)
			{
				return null;
			}
			object obj = this.DupNode(t);
			this.SetChildIndex(obj, this.GetChildIndex(t));
			this.SetParent(obj, parent);
			int childCount = this.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.GetChild(t, i);
				object obj2 = this.DupTree(child, t);
				this.AddChild(obj, obj2);
			}
			return obj;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002BC1 File Offset: 0x00001BC1
		public virtual void AddChild(object t, object child)
		{
			if (t != null && child != null)
			{
				((ITree)t).AddChild((ITree)child);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002BDC File Offset: 0x00001BDC
		public virtual object BecomeRoot(object newRoot, object oldRoot)
		{
			ITree tree = (ITree)newRoot;
			ITree tree2 = (ITree)oldRoot;
			if (oldRoot == null)
			{
				return newRoot;
			}
			if (tree.IsNil)
			{
				int childCount = tree.ChildCount;
				if (childCount == 1)
				{
					tree = tree.GetChild(0);
				}
				else if (childCount > 1)
				{
					throw new SystemException("more than one node as root (TODO: make exception hierarchy)");
				}
			}
			tree.AddChild(tree2);
			return tree;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002C30 File Offset: 0x00001C30
		public virtual object RulePostProcessing(object root)
		{
			ITree tree = (ITree)root;
			if (tree != null && tree.IsNil)
			{
				if (tree.ChildCount == 0)
				{
					tree = null;
				}
				else if (tree.ChildCount == 1)
				{
					tree = tree.GetChild(0);
					tree.Parent = null;
					tree.ChildIndex = -1;
				}
			}
			return tree;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002C7B File Offset: 0x00001C7B
		public virtual object BecomeRoot(IToken newRoot, object oldRoot)
		{
			return this.BecomeRoot(this.Create(newRoot), oldRoot);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002C8C File Offset: 0x00001C8C
		public virtual object Create(int tokenType, IToken fromToken)
		{
			fromToken = this.CreateToken(fromToken);
			fromToken.Type = tokenType;
			return (ITree)this.Create(fromToken);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002CB8 File Offset: 0x00001CB8
		public virtual object Create(int tokenType, IToken fromToken, string text)
		{
			fromToken = this.CreateToken(fromToken);
			fromToken.Type = tokenType;
			fromToken.Text = text;
			return (ITree)this.Create(fromToken);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002CEC File Offset: 0x00001CEC
		public virtual object Create(int tokenType, string text)
		{
			IToken token = this.CreateToken(tokenType, text);
			return (ITree)this.Create(token);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002D10 File Offset: 0x00001D10
		public virtual int GetNodeType(object t)
		{
			return ((ITree)t).Type;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002D1D File Offset: 0x00001D1D
		public virtual void SetNodeType(object t, int type)
		{
			throw new NotImplementedException("don't know enough about Tree node");
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002D29 File Offset: 0x00001D29
		public virtual string GetNodeText(object t)
		{
			return ((ITree)t).Text;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002D36 File Offset: 0x00001D36
		public virtual void SetNodeText(object t, string text)
		{
			throw new NotImplementedException("don't know enough about Tree node");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002D42 File Offset: 0x00001D42
		public virtual object GetChild(object t, int i)
		{
			return ((ITree)t).GetChild(i);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002D50 File Offset: 0x00001D50
		public virtual void SetChild(object t, int i, object child)
		{
			((ITree)t).SetChild(i, (ITree)child);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002D64 File Offset: 0x00001D64
		public virtual object DeleteChild(object t, int i)
		{
			return ((ITree)t).DeleteChild(i);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002D72 File Offset: 0x00001D72
		public virtual int GetChildCount(object t)
		{
			return ((ITree)t).ChildCount;
		}

		// Token: 0x06000090 RID: 144
		public abstract object DupNode(object param1);

		// Token: 0x06000091 RID: 145
		public abstract object Create(IToken param1);

		// Token: 0x06000092 RID: 146
		public abstract void SetTokenBoundaries(object param1, IToken param2, IToken param3);

		// Token: 0x06000093 RID: 147
		public abstract int GetTokenStartIndex(object t);

		// Token: 0x06000094 RID: 148
		public abstract int GetTokenStopIndex(object t);

		// Token: 0x06000095 RID: 149
		public abstract IToken GetToken(object treeNode);

		// Token: 0x06000096 RID: 150 RVA: 0x00002D80 File Offset: 0x00001D80
		public int GetUniqueID(object node)
		{
			if (this.treeToUniqueIDMap == null)
			{
				this.treeToUniqueIDMap = new Hashtable();
			}
			object obj = this.treeToUniqueIDMap[node];
			if (obj != null)
			{
				return (int)obj;
			}
			int num = this.uniqueNodeID;
			this.treeToUniqueIDMap[node] = num;
			this.uniqueNodeID++;
			return num;
		}

		// Token: 0x06000097 RID: 151
		public abstract IToken CreateToken(int tokenType, string text);

		// Token: 0x06000098 RID: 152
		public abstract IToken CreateToken(IToken fromToken);

		// Token: 0x06000099 RID: 153
		public abstract object GetParent(object t);

		// Token: 0x0600009A RID: 154
		public abstract void SetParent(object t, object parent);

		// Token: 0x0600009B RID: 155
		public abstract int GetChildIndex(object t);

		// Token: 0x0600009C RID: 156
		public abstract void SetChildIndex(object t, int index);

		// Token: 0x0600009D RID: 157
		public abstract void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);

		// Token: 0x04000019 RID: 25
		protected IDictionary treeToUniqueIDMap;

		// Token: 0x0400001A RID: 26
		protected int uniqueNodeID = 1;
	}
}
