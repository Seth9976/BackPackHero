using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007DD RID: 2013
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600401C RID: 16412 RVA: 0x000DFE48 File Offset: 0x000DE048
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x000DFE68 File Offset: 0x000DE068
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040026C5 RID: 9925
		private readonly ICollection<TKey> _collection;
	}
}
