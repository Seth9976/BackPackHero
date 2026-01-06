using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200005F RID: 95
	public sealed class GraphElementCollection<TElement> : GuidCollection<TElement>, IGraphElementCollection<TElement>, IKeyedCollection<Guid, TElement>, ICollection<TElement>, IEnumerable<TElement>, IEnumerable, INotifyCollectionChanged<TElement>, IProxyableNotifyCollectionChanged<TElement> where TElement : IGraphElement
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00006FB1 File Offset: 0x000051B1
		public GraphElementCollection(IGraph graph)
		{
			Ensure.That("graph").IsNotNull<IGraph>(graph);
			this.graph = graph;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00006FD0 File Offset: 0x000051D0
		public IGraph graph { get; }

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002D0 RID: 720 RVA: 0x00006FD8 File Offset: 0x000051D8
		// (remove) Token: 0x060002D1 RID: 721 RVA: 0x00007010 File Offset: 0x00005210
		public event Action<TElement> ItemAdded;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002D2 RID: 722 RVA: 0x00007048 File Offset: 0x00005248
		// (remove) Token: 0x060002D3 RID: 723 RVA: 0x00007080 File Offset: 0x00005280
		public event Action<TElement> ItemRemoved;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002D4 RID: 724 RVA: 0x000070B8 File Offset: 0x000052B8
		// (remove) Token: 0x060002D5 RID: 725 RVA: 0x000070F0 File Offset: 0x000052F0
		public event Action CollectionChanged;

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00007125 File Offset: 0x00005325
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000712D File Offset: 0x0000532D
		public bool ProxyCollectionChange { get; set; }

		// Token: 0x060002D8 RID: 728 RVA: 0x00007138 File Offset: 0x00005338
		public void BeforeAdd(TElement element)
		{
			if (element.graph == null)
			{
				element.graph = this.graph;
				element.BeforeAdd();
				return;
			}
			if (element.graph == this.graph)
			{
				throw new InvalidOperationException("Graph elements cannot be added multiple time into the same graph.");
			}
			throw new InvalidOperationException("Graph elements cannot be shared across graphs.");
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000719F File Offset: 0x0000539F
		public void AfterAdd(TElement element)
		{
			element.AfterAdd();
			Action<TElement> itemAdded = this.ItemAdded;
			if (itemAdded != null)
			{
				itemAdded(element);
			}
			Action collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000071D0 File Offset: 0x000053D0
		public void BeforeRemove(TElement element)
		{
			element.BeforeRemove();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000071DF File Offset: 0x000053DF
		public void AfterRemove(TElement element)
		{
			element.graph = null;
			element.AfterRemove();
			Action<TElement> itemRemoved = this.ItemRemoved;
			if (itemRemoved != null)
			{
				itemRemoved(element);
			}
			Action collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000721E File Offset: 0x0000541E
		protected override void InsertItem(int index, TElement element)
		{
			Ensure.That("element").IsNotNull<TElement>(element);
			if (!this.ProxyCollectionChange)
			{
				this.BeforeAdd(element);
			}
			base.InsertItem(index, element);
			if (!this.ProxyCollectionChange)
			{
				this.AfterAdd(element);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00007258 File Offset: 0x00005458
		protected override void RemoveItem(int index)
		{
			TElement telement = base[index];
			if (!base.Contains(telement))
			{
				throw new ArgumentOutOfRangeException("element");
			}
			if (!this.ProxyCollectionChange)
			{
				this.BeforeRemove(telement);
			}
			base.RemoveItem(index);
			if (!this.ProxyCollectionChange)
			{
				this.AfterRemove(telement);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000072A8 File Offset: 0x000054A8
		protected override void ClearItems()
		{
			List<TElement> list = ListPool<TElement>.New();
			foreach (TElement telement in this)
			{
				list.Add(telement);
			}
			list.Sort((TElement a, TElement b) => b.dependencyOrder.CompareTo(a.dependencyOrder));
			foreach (TElement telement2 in list)
			{
				base.Remove(telement2);
			}
			ListPool<TElement>.Free(list);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00007368 File Offset: 0x00005568
		protected override void SetItem(int index, TElement item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000736F File Offset: 0x0000556F
		public new NoAllocEnumerator<TElement> GetEnumerator()
		{
			return new NoAllocEnumerator<TElement>(this);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00007377 File Offset: 0x00005577
		TElement IKeyedCollection<Guid, TElement>.get_Item(Guid key)
		{
			return base[key];
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00007380 File Offset: 0x00005580
		bool IKeyedCollection<Guid, TElement>.Contains(Guid key)
		{
			return base.Contains(key);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007389 File Offset: 0x00005589
		bool IKeyedCollection<Guid, TElement>.Remove(Guid key)
		{
			return base.Remove(key);
		}
	}
}
