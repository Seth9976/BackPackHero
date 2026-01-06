using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x02000013 RID: 19
	public class GuidCollection<T> : KeyedCollection<Guid, T>, IKeyedCollection<Guid, T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : IIdentifiable
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002E46 File Offset: 0x00001046
		protected override Guid GetKeyForItem(T item)
		{
			return item.guid;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002E55 File Offset: 0x00001055
		protected override void InsertItem(int index, T item)
		{
			Ensure.That("item").IsNotNull<T>(item);
			base.InsertItem(index, item);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002E6F File Offset: 0x0000106F
		protected override void SetItem(int index, T item)
		{
			Ensure.That("item").IsNotNull<T>(item);
			base.SetItem(index, item);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002E89 File Offset: 0x00001089
		public new bool TryGetValue(Guid key, out T value)
		{
			if (base.Dictionary == null)
			{
				value = default(T);
				return false;
			}
			return base.Dictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002EB1 File Offset: 0x000010B1
		T IKeyedCollection<Guid, T>.get_Item(Guid key)
		{
			return base[key];
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002EBA File Offset: 0x000010BA
		bool IKeyedCollection<Guid, T>.Contains(Guid key)
		{
			return base.Contains(key);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002EC3 File Offset: 0x000010C3
		bool IKeyedCollection<Guid, T>.Remove(Guid key)
		{
			return base.Remove(key);
		}
	}
}
