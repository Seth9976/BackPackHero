using System;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x02000025 RID: 37
	public class WatchedList<T> : Collection<T>, INotifyCollectionChanged<T>
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000169 RID: 361 RVA: 0x00004450 File Offset: 0x00002650
		// (remove) Token: 0x0600016A RID: 362 RVA: 0x00004488 File Offset: 0x00002688
		public event Action<T> ItemAdded;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600016B RID: 363 RVA: 0x000044C0 File Offset: 0x000026C0
		// (remove) Token: 0x0600016C RID: 364 RVA: 0x000044F8 File Offset: 0x000026F8
		public event Action<T> ItemRemoved;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600016D RID: 365 RVA: 0x00004530 File Offset: 0x00002730
		// (remove) Token: 0x0600016E RID: 366 RVA: 0x00004568 File Offset: 0x00002768
		public event Action CollectionChanged;

		// Token: 0x0600016F RID: 367 RVA: 0x0000459D File Offset: 0x0000279D
		protected override void InsertItem(int index, T item)
		{
			base.InsertItem(index, item);
			Action<T> itemAdded = this.ItemAdded;
			if (itemAdded != null)
			{
				itemAdded(item);
			}
			Action collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000045CC File Offset: 0x000027CC
		protected override void RemoveItem(int index)
		{
			if (index < base.Count)
			{
				T t = base[index];
				base.RemoveItem(index);
				Action<T> itemRemoved = this.ItemRemoved;
				if (itemRemoved != null)
				{
					itemRemoved(t);
				}
				Action collectionChanged = this.CollectionChanged;
				if (collectionChanged == null)
				{
					return;
				}
				collectionChanged();
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004613 File Offset: 0x00002813
		protected override void ClearItems()
		{
			while (base.Count > 0)
			{
				this.RemoveItem(0);
			}
		}
	}
}
