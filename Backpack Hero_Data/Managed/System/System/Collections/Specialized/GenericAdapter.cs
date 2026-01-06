using System;
using System.Collections.Generic;

namespace System.Collections.Specialized
{
	// Token: 0x020007D3 RID: 2003
	internal class GenericAdapter : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000DF55D File Offset: 0x000DD75D
		internal GenericAdapter(StringDictionary stringDictionary)
		{
			this.m_stringDictionary = stringDictionary;
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000DF56C File Offset: 0x000DD76C
		public void Add(string key, string value)
		{
			this[key] = value;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000DF576 File Offset: 0x000DD776
		public bool ContainsKey(string key)
		{
			return this.m_stringDictionary.ContainsKey(key);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000DF584 File Offset: 0x000DD784
		public void Clear()
		{
			this.m_stringDictionary.Clear();
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x000DF591 File Offset: 0x000DD791
		public int Count
		{
			get
			{
				return this.m_stringDictionary.Count;
			}
		}

		// Token: 0x17000E87 RID: 3719
		public string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					throw new KeyNotFoundException();
				}
				return this.m_stringDictionary[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.m_stringDictionary[key] = value;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000DF5EB File Offset: 0x000DD7EB
		public ICollection<string> Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, GenericAdapter.KeyOrValue.Key);
				}
				return this._keys;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x000DF60D File Offset: 0x000DD80D
		public ICollection<string> Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, GenericAdapter.KeyOrValue.Value);
				}
				return this._values;
			}
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000DF62F File Offset: 0x000DD82F
		public bool Remove(string key)
		{
			if (!this.m_stringDictionary.ContainsKey(key))
			{
				return false;
			}
			this.m_stringDictionary.Remove(key);
			return true;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000DF64E File Offset: 0x000DD84E
		public bool TryGetValue(string key, out string value)
		{
			if (!this.m_stringDictionary.ContainsKey(key))
			{
				value = null;
				return false;
			}
			value = this.m_stringDictionary[key];
			return true;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000DF672 File Offset: 0x000DD872
		void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
		{
			this.m_stringDictionary.Add(item.Key, item.Value);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000DF690 File Offset: 0x000DD890
		bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
		{
			string text;
			return this.TryGetValue(item.Key, out text) && text.Equals(item.Value);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000DF6C0 File Offset: 0x000DD8C0
		void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("Array cannot be null."));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", SR.GetString("Non-negative number required."));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(SR.GetString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
			}
			int num = arrayIndex;
			foreach (object obj in this.m_stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num++] = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<KeyValuePair<string, string>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000DF78C File Offset: 0x000DD98C
		bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
		{
			if (!((ICollection<KeyValuePair<string, string>>)this).Contains(item))
			{
				return false;
			}
			this.m_stringDictionary.Remove(item.Key);
			return true;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000DF7AC File Offset: 0x000DD9AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000DF7B4 File Offset: 0x000DD9B4
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			foreach (object obj in this.m_stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				yield return new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040026A8 RID: 9896
		private StringDictionary m_stringDictionary;

		// Token: 0x040026A9 RID: 9897
		private GenericAdapter.ICollectionToGenericCollectionAdapter _values;

		// Token: 0x040026AA RID: 9898
		private GenericAdapter.ICollectionToGenericCollectionAdapter _keys;

		// Token: 0x020007D4 RID: 2004
		internal enum KeyOrValue
		{
			// Token: 0x040026AC RID: 9900
			Key,
			// Token: 0x040026AD RID: 9901
			Value
		}

		// Token: 0x020007D5 RID: 2005
		private class ICollectionToGenericCollectionAdapter : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06003FE9 RID: 16361 RVA: 0x000DF7C3 File Offset: 0x000DD9C3
			public ICollectionToGenericCollectionAdapter(StringDictionary source, GenericAdapter.KeyOrValue keyOrValue)
			{
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				this._internal = source;
				this._keyOrValue = keyOrValue;
			}

			// Token: 0x06003FEA RID: 16362 RVA: 0x000DF7E7 File Offset: 0x000DD9E7
			public void Add(string item)
			{
				this.ThrowNotSupportedException();
			}

			// Token: 0x06003FEB RID: 16363 RVA: 0x000DF7E7 File Offset: 0x000DD9E7
			public void Clear()
			{
				this.ThrowNotSupportedException();
			}

			// Token: 0x06003FEC RID: 16364 RVA: 0x000DF7EF File Offset: 0x000DD9EF
			public void ThrowNotSupportedException()
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					throw new NotSupportedException(SR.GetString("Mutating a key collection derived from a dictionary is not allowed."));
				}
				throw new NotSupportedException(SR.GetString("Mutating a value collection derived from a dictionary is not allowed."));
			}

			// Token: 0x06003FED RID: 16365 RVA: 0x000DF818 File Offset: 0x000DDA18
			public bool Contains(string item)
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					return this._internal.ContainsKey(item);
				}
				return this._internal.ContainsValue(item);
			}

			// Token: 0x06003FEE RID: 16366 RVA: 0x000DF83B File Offset: 0x000DDA3B
			public void CopyTo(string[] array, int arrayIndex)
			{
				this.GetUnderlyingCollection().CopyTo(array, arrayIndex);
			}

			// Token: 0x17000E8B RID: 3723
			// (get) Token: 0x06003FEF RID: 16367 RVA: 0x000DF84A File Offset: 0x000DDA4A
			public int Count
			{
				get
				{
					return this._internal.Count;
				}
			}

			// Token: 0x17000E8C RID: 3724
			// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003FF1 RID: 16369 RVA: 0x000DF857 File Offset: 0x000DDA57
			public bool Remove(string item)
			{
				this.ThrowNotSupportedException();
				return false;
			}

			// Token: 0x06003FF2 RID: 16370 RVA: 0x000DF860 File Offset: 0x000DDA60
			private ICollection GetUnderlyingCollection()
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					return this._internal.Keys;
				}
				return this._internal.Values;
			}

			// Token: 0x06003FF3 RID: 16371 RVA: 0x000DF881 File Offset: 0x000DDA81
			public IEnumerator<string> GetEnumerator()
			{
				ICollection underlyingCollection = this.GetUnderlyingCollection();
				foreach (object obj in underlyingCollection)
				{
					string text = (string)obj;
					yield return text;
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06003FF4 RID: 16372 RVA: 0x000DF890 File Offset: 0x000DDA90
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetUnderlyingCollection().GetEnumerator();
			}

			// Token: 0x040026AE RID: 9902
			private StringDictionary _internal;

			// Token: 0x040026AF RID: 9903
			private GenericAdapter.KeyOrValue _keyOrValue;
		}
	}
}
