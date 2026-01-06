using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200001F RID: 31
	public class NonNullableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003A08 File Offset: 0x00001C08
		public NonNullableDictionary()
		{
			this.dictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003A1B File Offset: 0x00001C1B
		public NonNullableDictionary(int capacity)
		{
			this.dictionary = new Dictionary<TKey, TValue>(capacity);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003A2F File Offset: 0x00001C2F
		public NonNullableDictionary(IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, TValue>(comparer);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003A43 File Offset: 0x00001C43
		public NonNullableDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = new Dictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003A57 File Offset: 0x00001C57
		public NonNullableDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003A6C File Offset: 0x00001C6C
		public NonNullableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
		}

		// Token: 0x17000029 RID: 41
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				this.dictionary[key] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		object IDictionary.this[object key]
		{
			get
			{
				return ((IDictionary)this.dictionary)[key];
			}
			set
			{
				((IDictionary)this.dictionary)[key] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003ABB File Offset: 0x00001CBB
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.dictionary).IsSynchronized;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003AD5 File Offset: 0x00001CD5
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.dictionary).SyncRoot;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003AE2 File Offset: 0x00001CE2
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public ICollection<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003AF2 File Offset: 0x00001CF2
		ICollection IDictionary.Values
		{
			get
			{
				return ((IDictionary)this.dictionary).Values;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003AFF File Offset: 0x00001CFF
		ICollection IDictionary.Keys
		{
			get
			{
				return ((IDictionary)this.dictionary).Keys;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003B0C File Offset: 0x00001D0C
		public ICollection<TValue> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003B19 File Offset: 0x00001D19
		public bool IsFixedSize
		{
			get
			{
				return ((IDictionary)this.dictionary).IsFixedSize;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003B26 File Offset: 0x00001D26
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.dictionary).CopyTo(array, index);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003B35 File Offset: 0x00001D35
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).Add(item);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003B43 File Offset: 0x00001D43
		public void Add(TKey key, TValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.dictionary.Add(key, value);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003B65 File Offset: 0x00001D65
		public void Add(object key, object value)
		{
			((IDictionary)this.dictionary).Add(key, value);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003B74 File Offset: 0x00001D74
		public void Clear()
		{
			this.dictionary.Clear();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003B81 File Offset: 0x00001D81
		public bool Contains(object key)
		{
			return ((IDictionary)this.dictionary).Contains(key);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003B8F File Offset: 0x00001D8F
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IDictionary)this.dictionary).GetEnumerator();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003B9C File Offset: 0x00001D9C
		public void Remove(object key)
		{
			((IDictionary)this.dictionary).Remove(key);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003BAA File Offset: 0x00001DAA
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).Contains(item);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003BC6 File Offset: 0x00001DC6
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003BD5 File Offset: 0x00001DD5
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003BE7 File Offset: 0x00001DE7
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).Remove(item);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public bool Remove(TKey key)
		{
			return this.dictionary.Remove(key);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003C03 File Offset: 0x00001E03
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003C12 File Offset: 0x00001E12
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x0400001D RID: 29
		private readonly Dictionary<TKey, TValue> dictionary;
	}
}
