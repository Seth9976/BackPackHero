using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000804 RID: 2052
	internal sealed class CollectionDebugView<T>
	{
		// Token: 0x060041D8 RID: 16856 RVA: 0x000E5454 File Offset: 0x000E3654
		public CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x000E5474 File Offset: 0x000E3674
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

		// Token: 0x0400274C RID: 10060
		private readonly ICollection<T> _collection;
	}
}
