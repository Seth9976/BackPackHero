using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200001B RID: 27
	public class MergedKeyedCollection<TKey, TItem> : IMergedCollection<TItem>, ICollection<TItem>, IEnumerable<TItem>, IEnumerable
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003194 File Offset: 0x00001394
		public MergedKeyedCollection()
		{
			this.collections = new Dictionary<Type, IKeyedCollection<TKey, TItem>>();
			this.collectionsLookup = new Dictionary<Type, IKeyedCollection<TKey, TItem>>();
		}

		// Token: 0x17000022 RID: 34
		public TItem this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				foreach (KeyValuePair<Type, IKeyedCollection<TKey, TItem>> keyValuePair in this.collections)
				{
					if (keyValuePair.Value.Contains(key))
					{
						return keyValuePair.Value[key];
					}
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000323C File Offset: 0x0000143C
		public int Count
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<Type, IKeyedCollection<TKey, TItem>> keyValuePair in this.collections)
				{
					num += keyValuePair.Value.Count;
				}
				return num;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000329C File Offset: 0x0000149C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000329F File Offset: 0x0000149F
		public bool Includes<TSubItem>() where TSubItem : TItem
		{
			return this.Includes(typeof(TSubItem));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000032B1 File Offset: 0x000014B1
		public bool Includes(Type elementType)
		{
			return this.GetCollectionForType(elementType, false) != null;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000032BE File Offset: 0x000014BE
		public IKeyedCollection<TKey, TSubItem> ForType<TSubItem>() where TSubItem : TItem
		{
			return ((VariantKeyedCollection<TItem, TSubItem, TKey>)this.GetCollectionForType(typeof(TSubItem), true)).implementation;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000032DC File Offset: 0x000014DC
		public virtual void Include<TSubItem>(IKeyedCollection<TKey, TSubItem> collection) where TSubItem : TItem
		{
			Type typeFromHandle = typeof(TSubItem);
			VariantKeyedCollection<TItem, TSubItem, TKey> variantKeyedCollection = new VariantKeyedCollection<TItem, TSubItem, TKey>(collection);
			this.collections.Add(typeFromHandle, variantKeyedCollection);
			this.collectionsLookup.Add(typeFromHandle, variantKeyedCollection);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003315 File Offset: 0x00001515
		protected IKeyedCollection<TKey, TItem> GetCollectionForItem(TItem item)
		{
			Ensure.That("item").IsNotNull<TItem>(item);
			return this.GetCollectionForType(item.GetType(), true);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000333C File Offset: 0x0000153C
		protected IKeyedCollection<TKey, TItem> GetCollectionForType(Type type, bool throwOnFail = true)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			IKeyedCollection<TKey, TItem> value;
			if (this.collectionsLookup.TryGetValue(type, out value))
			{
				return value;
			}
			foreach (KeyValuePair<Type, IKeyedCollection<TKey, TItem>> keyValuePair in this.collections)
			{
				if (keyValuePair.Key.IsAssignableFrom(type))
				{
					value = keyValuePair.Value;
					this.collectionsLookup.Add(type, value);
					return value;
				}
			}
			if (throwOnFail)
			{
				throw new InvalidOperationException(string.Format("No sub-collection available for type '{0}'.", type));
			}
			return null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000033EC File Offset: 0x000015EC
		protected IKeyedCollection<TKey, TItem> GetCollectionForKey(TKey key, bool throwOnFail = true)
		{
			foreach (KeyValuePair<Type, IKeyedCollection<TKey, TItem>> keyValuePair in this.collections)
			{
				if (keyValuePair.Value.Contains(key))
				{
					return keyValuePair.Value;
				}
			}
			if (throwOnFail)
			{
				throw new InvalidOperationException(string.Format("No sub-collection available for key '{0}'.", key));
			}
			return null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003470 File Offset: 0x00001670
		public bool TryGetValue(TKey key, out TItem value)
		{
			IKeyedCollection<TKey, TItem> collectionForKey = this.GetCollectionForKey(key, false);
			value = default(TItem);
			return collectionForKey != null && collectionForKey.TryGetValue(key, out value);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000349A File Offset: 0x0000169A
		public virtual void Add(TItem item)
		{
			this.GetCollectionForItem(item).Add(item);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000034AC File Offset: 0x000016AC
		public void Clear()
		{
			foreach (IKeyedCollection<TKey, TItem> keyedCollection in this.collections.Values)
			{
				keyedCollection.Clear();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003504 File Offset: 0x00001704
		public bool Contains(TItem item)
		{
			return this.GetCollectionForItem(item).Contains(item);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003513 File Offset: 0x00001713
		public bool Remove(TItem item)
		{
			return this.GetCollectionForItem(item).Remove(item);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003524 File Offset: 0x00001724
		public void CopyTo(TItem[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException();
			}
			int num = 0;
			foreach (IKeyedCollection<TKey, TItem> keyedCollection in this.collections.Values)
			{
				keyedCollection.CopyTo(array, arrayIndex + num);
				num += keyedCollection.Count;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000035BC File Offset: 0x000017BC
		public bool Contains(TKey key)
		{
			return this.GetCollectionForKey(key, false) != null;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000035C9 File Offset: 0x000017C9
		public bool Remove(TKey key)
		{
			return this.GetCollectionForKey(key, true).Remove(key);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000035D9 File Offset: 0x000017D9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000035E6 File Offset: 0x000017E6
		IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000035F3 File Offset: 0x000017F3
		public MergedKeyedCollection<TKey, TItem>.Enumerator GetEnumerator()
		{
			return new MergedKeyedCollection<TKey, TItem>.Enumerator(this);
		}

		// Token: 0x04000016 RID: 22
		protected readonly Dictionary<Type, IKeyedCollection<TKey, TItem>> collections;

		// Token: 0x04000017 RID: 23
		protected readonly Dictionary<Type, IKeyedCollection<TKey, TItem>> collectionsLookup;

		// Token: 0x020001B8 RID: 440
		public struct Enumerator : IEnumerator<TItem>, IEnumerator, IDisposable
		{
			// Token: 0x06000BBC RID: 3004 RVA: 0x00031C13 File Offset: 0x0002FE13
			public Enumerator(MergedKeyedCollection<TKey, TItem> merged)
			{
				this = default(MergedKeyedCollection<TKey, TItem>.Enumerator);
				this.collectionsEnumerator = merged.collections.GetEnumerator();
			}

			// Token: 0x06000BBD RID: 3005 RVA: 0x00031C2D File Offset: 0x0002FE2D
			public void Dispose()
			{
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x00031C30 File Offset: 0x0002FE30
			public bool MoveNext()
			{
				if (this.currentCollection == null)
				{
					if (!this.collectionsEnumerator.MoveNext())
					{
						this.currentItem = default(TItem);
						this.exceeded = true;
						return false;
					}
					KeyValuePair<Type, IKeyedCollection<TKey, TItem>> keyValuePair = this.collectionsEnumerator.Current;
					this.currentCollection = keyValuePair.Value;
					if (this.currentCollection == null)
					{
						throw new InvalidOperationException("Merged sub collection is null.");
					}
				}
				if (this.indexInCurrentCollection < this.currentCollection.Count)
				{
					this.currentItem = this.currentCollection[this.indexInCurrentCollection];
					this.indexInCurrentCollection++;
					return true;
				}
				while (this.collectionsEnumerator.MoveNext())
				{
					KeyValuePair<Type, IKeyedCollection<TKey, TItem>> keyValuePair = this.collectionsEnumerator.Current;
					this.currentCollection = keyValuePair.Value;
					this.indexInCurrentCollection = 0;
					if (this.currentCollection == null)
					{
						throw new InvalidOperationException("Merged sub collection is null.");
					}
					if (this.indexInCurrentCollection < this.currentCollection.Count)
					{
						this.currentItem = this.currentCollection[this.indexInCurrentCollection];
						this.indexInCurrentCollection++;
						return true;
					}
				}
				this.currentItem = default(TItem);
				this.exceeded = true;
				return false;
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00031D5E File Offset: 0x0002FF5E
			public TItem Current
			{
				get
				{
					return this.currentItem;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00031D66 File Offset: 0x0002FF66
			object IEnumerator.Current
			{
				get
				{
					if (this.exceeded)
					{
						throw new InvalidOperationException();
					}
					return this.Current;
				}
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x00031D81 File Offset: 0x0002FF81
			void IEnumerator.Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x040002E1 RID: 737
			private Dictionary<Type, IKeyedCollection<TKey, TItem>>.Enumerator collectionsEnumerator;

			// Token: 0x040002E2 RID: 738
			private TItem currentItem;

			// Token: 0x040002E3 RID: 739
			private IKeyedCollection<TKey, TItem> currentCollection;

			// Token: 0x040002E4 RID: 740
			private int indexInCurrentCollection;

			// Token: 0x040002E5 RID: 741
			private bool exceeded;
		}
	}
}
