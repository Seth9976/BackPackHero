using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000011 RID: 17
	public class DebugDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x1700000E RID: 14
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				this.Debug(string.Format("Set: {0} => {1}", key, value));
				this.dictionary[key] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		object IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BEE File Offset: 0x00000DEE
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public string label { get; set; } = "Dictionary";

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002BFF File Offset: 0x00000DFF
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002C07 File Offset: 0x00000E07
		public bool debug { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C10 File Offset: 0x00000E10
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002C1D File Offset: 0x00000E1D
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.dictionary).SyncRoot;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002C2A File Offset: 0x00000E2A
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.dictionary).IsSynchronized;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002C37 File Offset: 0x00000E37
		ICollection IDictionary.Values
		{
			get
			{
				return ((IDictionary)this.dictionary).Values;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002C44 File Offset: 0x00000E44
		bool IDictionary.IsReadOnly
		{
			get
			{
				return ((IDictionary)this.dictionary).IsReadOnly;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C51 File Offset: 0x00000E51
		bool IDictionary.IsFixedSize
		{
			get
			{
				return ((IDictionary)this.dictionary).IsFixedSize;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002C5E File Offset: 0x00000E5E
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).IsReadOnly;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002C6B File Offset: 0x00000E6B
		public ICollection<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002C78 File Offset: 0x00000E78
		ICollection IDictionary.Keys
		{
			get
			{
				return ((IDictionary)this.dictionary).Keys;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002C85 File Offset: 0x00000E85
		public ICollection<TValue> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002C92 File Offset: 0x00000E92
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.dictionary).CopyTo(array, index);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002CA1 File Offset: 0x00000EA1
		private void Debug(string message)
		{
			if (!this.debug)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.label))
			{
				message = "[" + this.label + "] " + message;
			}
			global::UnityEngine.Debug.Log(message + "\n");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002CE1 File Offset: 0x00000EE1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.dictionary).GetEnumerator();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002CEE File Offset: 0x00000EEE
		void IDictionary.Remove(object key)
		{
			this.Remove((TKey)((object)key));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002CFD File Offset: 0x00000EFD
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D0B File Offset: 0x00000F0B
		void IDictionary.Add(object key, object value)
		{
			this.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D1F File Offset: 0x00000F1F
		public void Clear()
		{
			this.Debug("Clear");
			this.dictionary.Clear();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D37 File Offset: 0x00000F37
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IDictionary)this.dictionary).GetEnumerator();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D44 File Offset: 0x00000F44
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.dictionary.Contains(item);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D52 File Offset: 0x00000F52
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).Add(item);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D60 File Offset: 0x00000F60
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002D6F File Offset: 0x00000F6F
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this.dictionary).Remove(item);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002D7D File Offset: 0x00000F7D
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D8F File Offset: 0x00000F8F
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002D9D File Offset: 0x00000F9D
		public void Add(TKey key, TValue value)
		{
			this.Debug(string.Format("Add: {0} => {1}", key, value));
			this.dictionary.Add(key, value);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public bool Remove(TKey key)
		{
			this.Debug(string.Format("Remove: {0}", key));
			return this.dictionary.Remove(key);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002DEC File Offset: 0x00000FEC
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x04000012 RID: 18
		private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
	}
}
