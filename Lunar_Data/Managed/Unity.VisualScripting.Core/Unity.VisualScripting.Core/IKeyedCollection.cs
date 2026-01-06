using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000014 RID: 20
	public interface IKeyedCollection<TKey, TItem> : ICollection<TItem>, IEnumerable<TItem>, IEnumerable
	{
		// Token: 0x1700001D RID: 29
		TItem this[TKey key] { get; }

		// Token: 0x1700001E RID: 30
		TItem this[int index] { get; }

		// Token: 0x0600008A RID: 138
		bool TryGetValue(TKey key, out TItem value);

		// Token: 0x0600008B RID: 139
		bool Contains(TKey key);

		// Token: 0x0600008C RID: 140
		bool Remove(TKey key);
	}
}
