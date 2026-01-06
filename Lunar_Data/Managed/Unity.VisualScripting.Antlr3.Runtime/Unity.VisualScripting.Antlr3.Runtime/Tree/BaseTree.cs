using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public abstract class BaseTree : ITree
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00005014 File Offset: 0x00004014
		public BaseTree()
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000501C File Offset: 0x0000401C
		public BaseTree(ITree node)
		{
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005024 File Offset: 0x00004024
		public virtual int ChildCount
		{
			get
			{
				if (this.children == null)
				{
					return 0;
				}
				return this.children.Count;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000503B File Offset: 0x0000403B
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000503E File Offset: 0x0000403E
		public virtual int Line
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005041 File Offset: 0x00004041
		public virtual int CharPositionInLine
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005044 File Offset: 0x00004044
		public virtual ITree GetChild(int i)
		{
			if (this.children == null || i >= this.children.Count)
			{
				return null;
			}
			return (ITree)this.children[i];
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000506F File Offset: 0x0000406F
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005078 File Offset: 0x00004078
		public virtual void AddChild(ITree t)
		{
			if (t == null)
			{
				return;
			}
			BaseTree baseTree = (BaseTree)t;
			if (baseTree.IsNil)
			{
				if (this.children != null && this.children == baseTree.children)
				{
					throw new InvalidOperationException("attempt to add child list to itself");
				}
				if (baseTree.children != null)
				{
					if (this.children != null)
					{
						int count = baseTree.children.Count;
						for (int i = 0; i < count; i++)
						{
							ITree tree = (ITree)baseTree.Children[i];
							this.children.Add(tree);
							tree.Parent = this;
							tree.ChildIndex = this.children.Count - 1;
						}
						return;
					}
					this.children = baseTree.children;
					this.FreshenParentAndChildIndexes();
					return;
				}
			}
			else
			{
				if (this.children == null)
				{
					this.children = this.CreateChildrenList();
				}
				this.children.Add(t);
				baseTree.Parent = this;
				baseTree.ChildIndex = this.children.Count - 1;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005170 File Offset: 0x00004170
		public void AddChildren(IList kids)
		{
			for (int i = 0; i < kids.Count; i++)
			{
				ITree tree = (ITree)kids[i];
				this.AddChild(tree);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000051A4 File Offset: 0x000041A4
		public virtual void SetChild(int i, ITree t)
		{
			if (t == null)
			{
				return;
			}
			if (t.IsNil)
			{
				throw new ArgumentException("Can't set single child to a list");
			}
			if (this.children == null)
			{
				this.children = this.CreateChildrenList();
			}
			this.children[i] = t;
			t.Parent = this;
			t.ChildIndex = i;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000051F8 File Offset: 0x000041F8
		public virtual object DeleteChild(int i)
		{
			if (this.children == null)
			{
				return null;
			}
			ITree tree = (ITree)this.children[i];
			this.children.RemoveAt(i);
			this.FreshenParentAndChildIndexes(i);
			return tree;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005238 File Offset: 0x00004238
		public virtual void ReplaceChildren(int startChildIndex, int stopChildIndex, object t)
		{
			if (this.children == null)
			{
				throw new ArgumentException("indexes invalid; no children in list");
			}
			int num = stopChildIndex - startChildIndex + 1;
			BaseTree baseTree = (BaseTree)t;
			IList list;
			if (baseTree.IsNil)
			{
				list = baseTree.Children;
			}
			else
			{
				list = new List<object>(1);
				list.Add(baseTree);
			}
			int count = list.Count;
			int count2 = list.Count;
			int num2 = num - count;
			if (num2 == 0)
			{
				int num3 = 0;
				for (int i = startChildIndex; i <= stopChildIndex; i++)
				{
					BaseTree baseTree2 = (BaseTree)list[num3];
					this.children[i] = baseTree2;
					baseTree2.Parent = this;
					baseTree2.ChildIndex = i;
					num3++;
				}
				return;
			}
			if (num2 > 0)
			{
				for (int j = 0; j < count2; j++)
				{
					this.children[startChildIndex + j] = list[j];
				}
				int num4 = startChildIndex + count2;
				for (int k = num4; k <= stopChildIndex; k++)
				{
					this.children.RemoveAt(num4);
				}
				this.FreshenParentAndChildIndexes(startChildIndex);
				return;
			}
			int l;
			for (l = 0; l < num; l++)
			{
				this.children[startChildIndex + l] = list[l];
			}
			while (l < count)
			{
				this.children.Insert(startChildIndex + l, list[l]);
				l++;
			}
			this.FreshenParentAndChildIndexes(startChildIndex);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005391 File Offset: 0x00004391
		protected internal virtual IList CreateChildrenList()
		{
			return new List<object>();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005398 File Offset: 0x00004398
		public virtual void FreshenParentAndChildIndexes()
		{
			this.FreshenParentAndChildIndexes(0);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000053A4 File Offset: 0x000043A4
		public virtual void FreshenParentAndChildIndexes(int offset)
		{
			int childCount = this.ChildCount;
			for (int i = offset; i < childCount; i++)
			{
				ITree child = this.GetChild(i);
				child.ChildIndex = i;
				child.Parent = this;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000053DA File Offset: 0x000043DA
		public virtual void SanityCheckParentAndChildIndexes()
		{
			this.SanityCheckParentAndChildIndexes(null, -1);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000053E4 File Offset: 0x000043E4
		public virtual void SanityCheckParentAndChildIndexes(ITree parent, int i)
		{
			if (parent != this.Parent)
			{
				throw new ArgumentException(string.Concat(new object[] { "parents don't match; expected ", parent, " found ", this.Parent }));
			}
			if (i != this.ChildIndex)
			{
				throw new NotSupportedException(string.Concat(new object[] { "child indexes don't match; expected ", i, " found ", this.ChildIndex }));
			}
			int childCount = this.ChildCount;
			for (int j = 0; j < childCount; j++)
			{
				CommonTree commonTree = (CommonTree)this.GetChild(j);
				commonTree.SanityCheckParentAndChildIndexes(this, j);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000549B File Offset: 0x0000449B
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000549E File Offset: 0x0000449E
		public virtual int ChildIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000054A0 File Offset: 0x000044A0
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000054A3 File Offset: 0x000044A3
		public virtual ITree Parent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000054A5 File Offset: 0x000044A5
		public bool HasAncestor(int ttype)
		{
			return this.GetAncestor(ttype) != null;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000054B4 File Offset: 0x000044B4
		public ITree GetAncestor(int ttype)
		{
			for (ITree tree = ((ITree)this).Parent; tree != null; tree = tree.Parent)
			{
				if (tree.Type == ttype)
				{
					return tree;
				}
			}
			return null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000054E4 File Offset: 0x000044E4
		public IList GetAncestors()
		{
			if (this.Parent == null)
			{
				return null;
			}
			IList list = new List<object>();
			for (ITree tree = ((ITree)this).Parent; tree != null; tree = tree.Parent)
			{
				list.Insert(0, tree);
			}
			return list;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005520 File Offset: 0x00004520
		public virtual string ToStringTree()
		{
			if (this.children == null || this.children.Count == 0)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsNil)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(this.ToString());
				stringBuilder.Append(' ');
			}
			int num = 0;
			while (this.children != null && num < this.children.Count)
			{
				ITree tree = (ITree)this.children[num];
				if (num > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(tree.ToStringTree());
				num++;
			}
			if (!this.IsNil)
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000197 RID: 407
		public abstract override string ToString();

		// Token: 0x06000198 RID: 408
		public abstract ITree DupNode();

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000199 RID: 409
		public abstract int Type { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019A RID: 410
		// (set) Token: 0x0600019B RID: 411
		public abstract int TokenStartIndex { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019C RID: 412
		// (set) Token: 0x0600019D RID: 413
		public abstract int TokenStopIndex { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019E RID: 414
		public abstract string Text { get; }

		// Token: 0x04000068 RID: 104
		protected IList children;
	}
}
