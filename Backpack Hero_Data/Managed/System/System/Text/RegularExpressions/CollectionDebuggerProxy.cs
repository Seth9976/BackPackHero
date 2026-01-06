using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001EA RID: 490
	internal sealed class CollectionDebuggerProxy<T>
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x000357CA File Offset: 0x000339CA
		public CollectionDebuggerProxy(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000357E8 File Offset: 0x000339E8
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

		// Token: 0x040007D9 RID: 2009
		private readonly ICollection<T> _collection;
	}
}
