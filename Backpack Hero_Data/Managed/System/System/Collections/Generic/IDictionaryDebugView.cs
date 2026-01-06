using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007DC RID: 2012
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x0600401A RID: 16410 RVA: 0x000DFDFC File Offset: 0x000DDFFC
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x000DFE1C File Offset: 0x000DE01C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040026C4 RID: 9924
		private readonly IDictionary<K, V> _dict;
	}
}
