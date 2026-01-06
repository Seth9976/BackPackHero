using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000027 RID: 39
	public class ConnectionCollectionBase<TConnection, TSource, TDestination, TCollection> : IConnectionCollection<TConnection, TSource, TDestination>, ICollection<TConnection>, IEnumerable<TConnection>, IEnumerable where TConnection : IConnection<TSource, TDestination> where TCollection : ICollection<TConnection>
	{
		// Token: 0x06000174 RID: 372 RVA: 0x0000463C File Offset: 0x0000283C
		public ConnectionCollectionBase(TCollection collection)
		{
			this.collection = collection;
			this.bySource = new Dictionary<TSource, List<TConnection>>();
			this.byDestination = new Dictionary<TDestination, List<TConnection>>();
		}

		// Token: 0x17000047 RID: 71
		public IEnumerable<TConnection> this[TSource source]
		{
			get
			{
				return this.WithSource(source);
			}
		}

		// Token: 0x17000048 RID: 72
		public IEnumerable<TConnection> this[TDestination destination]
		{
			get
			{
				return this.WithDestination(destination);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00004674 File Offset: 0x00002874
		public int Count
		{
			get
			{
				TCollection tcollection = this.collection;
				return tcollection.Count;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004695 File Offset: 0x00002895
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004698 File Offset: 0x00002898
		public IEnumerator<TConnection> GetEnumerator()
		{
			TCollection tcollection = this.collection;
			return tcollection.GetEnumerator();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000046B9 File Offset: 0x000028B9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000046C1 File Offset: 0x000028C1
		public IEnumerable<TConnection> WithSource(TSource source)
		{
			return this.WithSourceNoAlloc(source);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000046CC File Offset: 0x000028CC
		public List<TConnection> WithSourceNoAlloc(TSource source)
		{
			Ensure.That("source").IsNotNull<TSource>(source);
			List<TConnection> list;
			if (this.bySource.TryGetValue(source, out list))
			{
				return list;
			}
			return Empty<TConnection>.list;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004700 File Offset: 0x00002900
		public TConnection SingleOrDefaultWithSource(TSource source)
		{
			Ensure.That("source").IsNotNull<TSource>(source);
			List<TConnection> list;
			if (!this.bySource.TryGetValue(source, out list))
			{
				return default(TConnection);
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (list.Count == 0)
			{
				return default(TConnection);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000475F File Offset: 0x0000295F
		public IEnumerable<TConnection> WithDestination(TDestination destination)
		{
			return this.WithDestinationNoAlloc(destination);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004768 File Offset: 0x00002968
		public List<TConnection> WithDestinationNoAlloc(TDestination destination)
		{
			Ensure.That("destination").IsNotNull<TDestination>(destination);
			List<TConnection> list;
			if (this.byDestination.TryGetValue(destination, out list))
			{
				return list;
			}
			return Empty<TConnection>.list;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000479C File Offset: 0x0000299C
		public TConnection SingleOrDefaultWithDestination(TDestination destination)
		{
			Ensure.That("destination").IsNotNull<TDestination>(destination);
			List<TConnection> list;
			if (!this.byDestination.TryGetValue(destination, out list))
			{
				return default(TConnection);
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (list.Count == 0)
			{
				return default(TConnection);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000047FC File Offset: 0x000029FC
		public void Add(TConnection item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.source == null)
			{
				throw new ArgumentNullException("item.source");
			}
			if (item.destination == null)
			{
				throw new ArgumentNullException("item.destination");
			}
			this.BeforeAdd(item);
			TCollection tcollection = this.collection;
			tcollection.Add(item);
			this.AddToDictionaries(item);
			this.AfterAdd(item);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004884 File Offset: 0x00002A84
		public void Clear()
		{
			TCollection tcollection = this.collection;
			tcollection.Clear();
			this.bySource.Clear();
			this.byDestination.Clear();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000048BC File Offset: 0x00002ABC
		public bool Contains(TConnection item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			TCollection tcollection = this.collection;
			return tcollection.Contains(item);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000048F4 File Offset: 0x00002AF4
		public void CopyTo(TConnection[] array, int arrayIndex)
		{
			TCollection tcollection = this.collection;
			tcollection.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004918 File Offset: 0x00002B18
		public bool Remove(TConnection item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.source == null)
			{
				throw new ArgumentNullException("item.source");
			}
			if (item.destination == null)
			{
				throw new ArgumentNullException("item.destination");
			}
			TCollection tcollection = this.collection;
			if (!tcollection.Contains(item))
			{
				return false;
			}
			this.BeforeRemove(item);
			tcollection = this.collection;
			tcollection.Remove(item);
			this.RemoveFromDictionaries(item);
			this.AfterRemove(item);
			return true;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000049BB File Offset: 0x00002BBB
		protected virtual void BeforeAdd(TConnection item)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000049BD File Offset: 0x00002BBD
		protected virtual void AfterAdd(TConnection item)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000049BF File Offset: 0x00002BBF
		protected virtual void BeforeRemove(TConnection item)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000049C1 File Offset: 0x00002BC1
		protected virtual void AfterRemove(TConnection item)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000049C4 File Offset: 0x00002BC4
		private void AddToDictionaries(TConnection item)
		{
			if (!this.bySource.ContainsKey(item.source))
			{
				this.bySource.Add(item.source, new List<TConnection>());
			}
			this.bySource[item.source].Add(item);
			if (!this.byDestination.ContainsKey(item.destination))
			{
				this.byDestination.Add(item.destination, new List<TConnection>());
			}
			this.byDestination[item.destination].Add(item);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004A7B File Offset: 0x00002C7B
		private void RemoveFromDictionaries(TConnection item)
		{
			this.bySource[item.source].Remove(item);
			this.byDestination[item.destination].Remove(item);
		}

		// Token: 0x04000026 RID: 38
		private readonly Dictionary<TDestination, List<TConnection>> byDestination;

		// Token: 0x04000027 RID: 39
		private readonly Dictionary<TSource, List<TConnection>> bySource;

		// Token: 0x04000028 RID: 40
		protected readonly TCollection collection;
	}
}
