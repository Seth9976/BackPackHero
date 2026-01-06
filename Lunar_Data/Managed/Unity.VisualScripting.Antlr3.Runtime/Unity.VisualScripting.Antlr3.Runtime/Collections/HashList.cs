using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime.Collections
{
	// Token: 0x02000002 RID: 2
	public sealed class HashList : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		public HashList()
			: this(-1)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000105C
		public HashList(int capacity)
		{
			if (capacity < 0)
			{
				this._dictionary = new Hashtable();
				this._insertionOrderList = new List<object>();
			}
			else
			{
				this._dictionary = new Hashtable(capacity);
				this._insertionOrderList = new List<object>(capacity);
			}
			this._version = 0;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000010C0
		public bool IsReadOnly
		{
			get
			{
				return this._dictionary.IsReadOnly;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000010CD
		public IDictionaryEnumerator GetEnumerator()
		{
			return new HashList.HashListEnumerator(this, HashList.HashListEnumerator.EnumerationMode.Entry);
		}

		// Token: 0x17000002 RID: 2
		public object this[object key]
		{
			get
			{
				return this._dictionary[key];
			}
			set
			{
				bool flag = !this._dictionary.Contains(key);
				this._dictionary[key] = value;
				if (flag)
				{
					this._insertionOrderList.Add(key);
				}
				this._version++;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212B File Offset: 0x0000112B
		public void Remove(object key)
		{
			this._dictionary.Remove(key);
			this._insertionOrderList.Remove(key);
			this._version++;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00001154
		public bool Contains(object key)
		{
			return this._dictionary.Contains(key);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002162 File Offset: 0x00001162
		public void Clear()
		{
			this._dictionary.Clear();
			this._insertionOrderList.Clear();
			this._version++;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00001188
		public ICollection Values
		{
			get
			{
				return new HashList.ValueCollection(this);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002190 File Offset: 0x00001190
		public void Add(object key, object value)
		{
			this._dictionary.Add(key, value);
			this._insertionOrderList.Add(key);
			this._version++;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021B9 File Offset: 0x000011B9
		public ICollection Keys
		{
			get
			{
				return new HashList.KeyCollection(this);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021C1 File Offset: 0x000011C1
		public bool IsFixedSize
		{
			get
			{
				return this._dictionary.IsFixedSize;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021CE File Offset: 0x000011CE
		public bool IsSynchronized
		{
			get
			{
				return this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021DB File Offset: 0x000011DB
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000011E8
		public void CopyTo(Array array, int index)
		{
			int count = this._insertionOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				DictionaryEntry dictionaryEntry;
				dictionaryEntry..ctor(this._insertionOrderList[i], this._dictionary[this._insertionOrderList[i]]);
				array.SetValue(dictionaryEntry, index++);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002249 File Offset: 0x00001249
		public object SyncRoot
		{
			get
			{
				return this._dictionary.SyncRoot;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002256 File Offset: 0x00001256
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashList.HashListEnumerator(this, HashList.HashListEnumerator.EnumerationMode.Entry);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002260 File Offset: 0x00001260
		private void CopyKeysTo(Array array, int index)
		{
			int count = this._insertionOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				array.SetValue(this._insertionOrderList[i], index++);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A0 File Offset: 0x000012A0
		private void CopyValuesTo(Array array, int index)
		{
			int count = this._insertionOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				array.SetValue(this._dictionary[this._insertionOrderList[i]], index++);
			}
		}

		// Token: 0x04000001 RID: 1
		private Hashtable _dictionary = new Hashtable();

		// Token: 0x04000002 RID: 2
		private List<object> _insertionOrderList = new List<object>();

		// Token: 0x04000003 RID: 3
		private int _version;

		// Token: 0x02000003 RID: 3
		private sealed class HashListEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000015 RID: 21 RVA: 0x000022E8 File Offset: 0x000012E8
			internal HashListEnumerator()
			{
				this._index = 0;
				this._key = null;
				this._value = null;
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002308 File Offset: 0x00001308
			internal HashListEnumerator(HashList hashList, HashList.HashListEnumerator.EnumerationMode mode)
			{
				this._hashList = hashList;
				this._mode = mode;
				this._version = hashList._version;
				this._orderList = hashList._insertionOrderList;
				this._index = 0;
				this._key = null;
				this._value = null;
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000017 RID: 23 RVA: 0x00002356 File Offset: 0x00001356
			public object Key
			{
				get
				{
					if (this._key == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000018 RID: 24 RVA: 0x00002371 File Offset: 0x00001371
			public object Value
			{
				get
				{
					if (this._key == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000019 RID: 25 RVA: 0x0000238C File Offset: 0x0000138C
			public DictionaryEntry Entry
			{
				get
				{
					if (this._key == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x0600001A RID: 26 RVA: 0x000023B2 File Offset: 0x000013B2
			public void Reset()
			{
				if (this._version != this._hashList._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._key = null;
				this._value = null;
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600001B RID: 27 RVA: 0x000023E8 File Offset: 0x000013E8
			public object Current
			{
				get
				{
					if (this._key == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._mode == HashList.HashListEnumerator.EnumerationMode.Key)
					{
						return this._key;
					}
					if (this._mode == HashList.HashListEnumerator.EnumerationMode.Value)
					{
						return this._value;
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x0600001C RID: 28 RVA: 0x00002440 File Offset: 0x00001440
			public bool MoveNext()
			{
				if (this._version != this._hashList._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._orderList.Count)
				{
					this._key = this._orderList[this._index];
					this._value = this._hashList[this._key];
					this._index++;
					return true;
				}
				this._key = null;
				return false;
			}

			// Token: 0x04000004 RID: 4
			private HashList _hashList;

			// Token: 0x04000005 RID: 5
			private List<object> _orderList;

			// Token: 0x04000006 RID: 6
			private HashList.HashListEnumerator.EnumerationMode _mode;

			// Token: 0x04000007 RID: 7
			private int _index;

			// Token: 0x04000008 RID: 8
			private int _version;

			// Token: 0x04000009 RID: 9
			private object _key;

			// Token: 0x0400000A RID: 10
			private object _value;

			// Token: 0x02000004 RID: 4
			internal enum EnumerationMode
			{
				// Token: 0x0400000C RID: 12
				Key,
				// Token: 0x0400000D RID: 13
				Value,
				// Token: 0x0400000E RID: 14
				Entry
			}
		}

		// Token: 0x02000005 RID: 5
		private sealed class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000024C4 File Offset: 0x000014C4
			internal KeyCollection()
			{
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000024CC File Offset: 0x000014CC
			internal KeyCollection(HashList hashList)
			{
				this._hashList = hashList;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000024DC File Offset: 0x000014DC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				List<object> insertionOrderList = this._hashList._insertionOrderList;
				for (int i = 0; i < insertionOrderList.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(insertionOrderList[i]);
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002548 File Offset: 0x00001548
			public override bool Equals(object o)
			{
				if (o is HashList.KeyCollection)
				{
					HashList.KeyCollection keyCollection = (HashList.KeyCollection)o;
					if (this.Count == 0 && keyCollection.Count == 0)
					{
						return true;
					}
					if (this.Count == keyCollection.Count)
					{
						for (int i = 0; i < this.Count; i++)
						{
							if (this._hashList._insertionOrderList[i] == keyCollection._hashList._insertionOrderList[i] || this._hashList._insertionOrderList[i].Equals(keyCollection._hashList._insertionOrderList[i]))
							{
								return true;
							}
						}
					}
				}
				return false;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x000025E8 File Offset: 0x000015E8
			public override int GetHashCode()
			{
				return this._hashList._insertionOrderList.GetHashCode();
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000022 RID: 34 RVA: 0x000025FA File Offset: 0x000015FA
			public bool IsSynchronized
			{
				get
				{
					return this._hashList.IsSynchronized;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000023 RID: 35 RVA: 0x00002607 File Offset: 0x00001607
			public int Count
			{
				get
				{
					return this._hashList.Count;
				}
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002614 File Offset: 0x00001614
			public void CopyTo(Array array, int index)
			{
				this._hashList.CopyKeysTo(array, index);
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000025 RID: 37 RVA: 0x00002623 File Offset: 0x00001623
			public object SyncRoot
			{
				get
				{
					return this._hashList.SyncRoot;
				}
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002630 File Offset: 0x00001630
			public IEnumerator GetEnumerator()
			{
				return new HashList.HashListEnumerator(this._hashList, HashList.HashListEnumerator.EnumerationMode.Key);
			}

			// Token: 0x0400000F RID: 15
			private HashList _hashList;
		}

		// Token: 0x02000006 RID: 6
		private sealed class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06000027 RID: 39 RVA: 0x0000263E File Offset: 0x0000163E
			internal ValueCollection()
			{
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002646 File Offset: 0x00001646
			internal ValueCollection(HashList hashList)
			{
				this._hashList = hashList;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002658 File Offset: 0x00001658
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				IEnumerator enumerator = new HashList.HashListEnumerator(this._hashList, HashList.HashListEnumerator.EnumerationMode.Value);
				if (enumerator.MoveNext())
				{
					stringBuilder.Append((enumerator.Current == null) ? "null" : enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(", ");
						stringBuilder.Append((enumerator.Current == null) ? "null" : enumerator.Current);
					}
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600002A RID: 42 RVA: 0x000026EC File Offset: 0x000016EC
			public bool IsSynchronized
			{
				get
				{
					return this._hashList.IsSynchronized;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600002B RID: 43 RVA: 0x000026F9 File Offset: 0x000016F9
			public int Count
			{
				get
				{
					return this._hashList.Count;
				}
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002706 File Offset: 0x00001706
			public void CopyTo(Array array, int index)
			{
				this._hashList.CopyValuesTo(array, index);
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002715 File Offset: 0x00001715
			public object SyncRoot
			{
				get
				{
					return this._hashList.SyncRoot;
				}
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002722 File Offset: 0x00001722
			public IEnumerator GetEnumerator()
			{
				return new HashList.HashListEnumerator(this._hashList, HashList.HashListEnumerator.EnumerationMode.Value);
			}

			// Token: 0x04000010 RID: 16
			private HashList _hashList;
		}
	}
}
