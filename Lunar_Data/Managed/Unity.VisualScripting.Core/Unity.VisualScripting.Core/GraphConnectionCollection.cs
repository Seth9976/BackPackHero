using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000028 RID: 40
	public class GraphConnectionCollection<TConnection, TSource, TDestination> : ConnectionCollectionBase<TConnection, TSource, TDestination, GraphElementCollection<TConnection>>, IGraphElementCollection<TConnection>, IKeyedCollection<Guid, TConnection>, ICollection<TConnection>, IEnumerable<TConnection>, IEnumerable, INotifyCollectionChanged<TConnection> where TConnection : IConnection<TSource, TDestination>, IGraphElement
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00004ABB File Offset: 0x00002CBB
		public GraphConnectionCollection(IGraph graph)
			: base(new GraphElementCollection<TConnection>(graph))
		{
			this.collection.ProxyCollectionChange = true;
		}

		// Token: 0x1700004B RID: 75
		TConnection IKeyedCollection<Guid, TConnection>.this[Guid key]
		{
			get
			{
				return this.collection[key];
			}
		}

		// Token: 0x1700004C RID: 76
		TConnection IKeyedCollection<Guid, TConnection>.this[int index]
		{
			get
			{
				return this.collection[index];
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004AF1 File Offset: 0x00002CF1
		public bool TryGetValue(Guid key, out TConnection value)
		{
			return this.collection.TryGetValue(key, out value);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004B00 File Offset: 0x00002D00
		public bool Contains(Guid key)
		{
			return this.collection.Contains(key);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004B0E File Offset: 0x00002D0E
		public bool Remove(Guid key)
		{
			return this.Contains(key) && base.Remove(this.collection[key]);
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000192 RID: 402 RVA: 0x00004B2D File Offset: 0x00002D2D
		// (remove) Token: 0x06000193 RID: 403 RVA: 0x00004B3B File Offset: 0x00002D3B
		public event Action<TConnection> ItemAdded
		{
			add
			{
				this.collection.ItemAdded += value;
			}
			remove
			{
				this.collection.ItemAdded -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000194 RID: 404 RVA: 0x00004B49 File Offset: 0x00002D49
		// (remove) Token: 0x06000195 RID: 405 RVA: 0x00004B57 File Offset: 0x00002D57
		public event Action<TConnection> ItemRemoved
		{
			add
			{
				this.collection.ItemRemoved += value;
			}
			remove
			{
				this.collection.ItemRemoved -= value;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000196 RID: 406 RVA: 0x00004B65 File Offset: 0x00002D65
		// (remove) Token: 0x06000197 RID: 407 RVA: 0x00004B73 File Offset: 0x00002D73
		public event Action CollectionChanged
		{
			add
			{
				this.collection.CollectionChanged += value;
			}
			remove
			{
				this.collection.CollectionChanged -= value;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004B81 File Offset: 0x00002D81
		protected override void BeforeAdd(TConnection item)
		{
			this.collection.BeforeAdd(item);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004B8F File Offset: 0x00002D8F
		protected override void AfterAdd(TConnection item)
		{
			this.collection.AfterAdd(item);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004B9D File Offset: 0x00002D9D
		protected override void BeforeRemove(TConnection item)
		{
			this.collection.BeforeRemove(item);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004BAB File Offset: 0x00002DAB
		protected override void AfterRemove(TConnection item)
		{
			this.collection.AfterRemove(item);
		}
	}
}
