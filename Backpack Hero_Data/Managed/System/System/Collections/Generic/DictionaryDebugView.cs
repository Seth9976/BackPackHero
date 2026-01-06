using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000805 RID: 2053
	internal sealed class DictionaryDebugView<K, V>
	{
		// Token: 0x060041DA RID: 16858 RVA: 0x000E54A0 File Offset: 0x000E36A0
		public DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x000E54C0 File Offset: 0x000E36C0
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

		// Token: 0x0400274D RID: 10061
		private readonly IDictionary<K, V> _dict;
	}
}
