using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020007B0 RID: 1968
	internal sealed class BlockingCollectionDebugView<T>
	{
		// Token: 0x06003E58 RID: 15960 RVA: 0x000DB49F File Offset: 0x000D969F
		public BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._blockingCollection = collection;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000DB4BC File Offset: 0x000D96BC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._blockingCollection.ToArray();
			}
		}

		// Token: 0x0400262E RID: 9774
		private readonly BlockingCollection<T> _blockingCollection;
	}
}
