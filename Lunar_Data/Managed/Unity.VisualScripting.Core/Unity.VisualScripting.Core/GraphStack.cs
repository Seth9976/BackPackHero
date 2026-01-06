using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000067 RID: 103
	public sealed class GraphStack : GraphPointer, IPoolable, IDisposable
	{
		// Token: 0x06000360 RID: 864 RVA: 0x00008EB2 File Offset: 0x000070B2
		private GraphStack()
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00008EBC File Offset: 0x000070BC
		private void InitializeNoAlloc(IGraphRoot root, List<IGraphParentElement> parentElements, bool ensureValid)
		{
			base.Initialize(root);
			Ensure.That("parentElements").IsNotNull<List<IGraphParentElement>>(parentElements);
			foreach (IGraphParentElement graphParentElement in parentElements)
			{
				string text;
				if (!base.TryEnterParentElement(graphParentElement, out text, null, false))
				{
					if (ensureValid)
					{
						throw new GraphPointerException(text, this);
					}
					break;
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00008F3C File Offset: 0x0000713C
		internal static GraphStack New(IGraphRoot root, List<IGraphParentElement> parentElements)
		{
			GraphStack graphStack = GenericPool<GraphStack>.New(() => new GraphStack());
			graphStack.InitializeNoAlloc(root, parentElements, true);
			return graphStack;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00008F6B File Offset: 0x0000716B
		internal static GraphStack New(GraphPointer model)
		{
			GraphStack graphStack = GenericPool<GraphStack>.New(() => new GraphStack());
			graphStack.CopyFrom(model);
			return graphStack;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00008F98 File Offset: 0x00007198
		public GraphStack Clone()
		{
			return GraphStack.New(this);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00008FA0 File Offset: 0x000071A0
		public void Dispose()
		{
			GenericPool<GraphStack>.Free(this);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00008FA8 File Offset: 0x000071A8
		void IPoolable.New()
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00008FAA File Offset: 0x000071AA
		void IPoolable.Free()
		{
			base.root = null;
			this.parentStack.Clear();
			this.parentElementStack.Clear();
			this.graphStack.Clear();
			this.dataStack.Clear();
			this.debugDataStack.Clear();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00008FEA File Offset: 0x000071EA
		public override GraphReference AsReference()
		{
			return this.ToReference();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008FF2 File Offset: 0x000071F2
		public GraphReference ToReference()
		{
			return GraphReference.Intern(this);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00008FFA File Offset: 0x000071FA
		public new void EnterParentElement(IGraphParentElement parentElement)
		{
			base.EnterParentElement(parentElement);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00009004 File Offset: 0x00007204
		public bool TryEnterParentElement(IGraphParentElement parentElement)
		{
			string text;
			return base.TryEnterParentElement(parentElement, out text, null, false);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009024 File Offset: 0x00007224
		public bool TryEnterParentElementUnsafe(IGraphParentElement parentElement)
		{
			string text;
			return base.TryEnterParentElement(parentElement, out text, null, true);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009044 File Offset: 0x00007244
		public new void ExitParentElement()
		{
			base.ExitParentElement();
		}
	}
}
