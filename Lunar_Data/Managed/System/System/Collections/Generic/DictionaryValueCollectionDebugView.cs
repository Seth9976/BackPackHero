using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007DE RID: 2014
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600401E RID: 16414 RVA: 0x000DFE94 File Offset: 0x000DE094
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x000DFEB4 File Offset: 0x000DE0B4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040026C6 RID: 9926
		private readonly ICollection<TValue> _collection;
	}
}
