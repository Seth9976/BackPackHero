using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007DB RID: 2011
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06004018 RID: 16408 RVA: 0x000DFDB2 File Offset: 0x000DDFB2
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x000DFDD0 File Offset: 0x000DDFD0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040026C3 RID: 9923
		private readonly ICollection<T> _collection;
	}
}
