using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted by key based on the associated <see cref="T:System.Collections.Generic.IComparer`1" /> implementation. </summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	// Token: 0x020007F0 RID: 2032
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		// Token: 0x060040C6 RID: 16582 RVA: 0x000E179B File Offset: 0x000DF99B
		public SortedList()
		{
			this.keys = Array.Empty<TKey>();
			this.values = Array.Empty<TValue>();
			this._size = 0;
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060040C7 RID: 16583 RVA: 0x000E17CC File Offset: 0x000DF9CC
		public SortedList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this.keys = new TKey[capacity];
			this.values = new TValue[capacity];
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x060040C8 RID: 16584 RVA: 0x000E181C File Offset: 0x000DFA1C
		public SortedList(IComparer<TKey> comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060040C9 RID: 16585 RVA: 0x000E182E File Offset: 0x000DFA2E
		public SortedList(int capacity, IComparer<TKey> comparer)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060040CA RID: 16586 RVA: 0x000E183E File Offset: 0x000DFA3E
		public SortedList(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060040CB RID: 16587 RVA: 0x000E1848 File Offset: 0x000DFA48
		public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			int count = dictionary.Count;
			if (count != 0)
			{
				TKey[] array = this.keys;
				dictionary.Keys.CopyTo(array, 0);
				dictionary.Values.CopyTo(this.values, 0);
				if (count > 1)
				{
					comparer = this.Comparer;
					Array.Sort<TKey, TValue>(array, this.values, comparer);
					for (int num = 1; num != array.Length; num++)
					{
						if (comparer.Compare(array[num - 1], array[num]) == 0)
						{
							throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", array[num]));
						}
					}
				}
			}
			this._size = count;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x060040CC RID: 16588 RVA: 0x000E190C File Offset: 0x000DFB0C
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key), "key");
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000E196F File Offset: 0x000DFB6F
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000E1988 File Offset: 0x000DFB88
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x000E19CC File Offset: 0x000DFBCC
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value))
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.SortedList`2.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x000E1A14 File Offset: 0x000DFC14
		// (set) Token: 0x060040D1 RID: 16593 RVA: 0x000E1A20 File Offset: 0x000DFC20
		public int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value != this.keys.Length)
				{
					if (value < this._size)
					{
						throw new ArgumentOutOfRangeException("value", value, "capacity was less than the current size.");
					}
					if (value > 0)
					{
						TKey[] array = new TKey[value];
						TValue[] array2 = new TValue[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = Array.Empty<TKey>();
					this.values = Array.Empty<TValue>();
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> for the sorted list. </summary>
		/// <returns>The <see cref="T:System.IComparable`1" /> for the current <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x000E1AC2 File Offset: 0x000DFCC2
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x060040D3 RID: 16595 RVA: 0x000E1ACC File Offset: 0x000DFCCC
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null && default(TValue) != null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
			if (!(value is TValue) && value != null)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
			}
			this.Add((TKey)((object)key), (TValue)((object)value));
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000E1B6A File Offset: 0x000DFD6A
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x000E1B72 File Offset: 0x000DFD72
		public IList<TKey> Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000E1B72 File Offset: 0x000DFD72
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x000E1B72 File Offset: 0x000DFD72
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000E1B72 File Offset: 0x000DFD72
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x000E1B7A File Offset: 0x000DFD7A
		public IList<TValue> Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x000E1B7A File Offset: 0x000DFD7A
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x000E1B7A File Offset: 0x000DFD7A
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x000E1B7A File Offset: 0x000DFD7A
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x000E1B82 File Offset: 0x000DFD82
		private SortedList<TKey, TValue>.KeyList GetKeyListHelper()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList<TKey, TValue>.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x000E1B9E File Offset: 0x000DFD9E
		private SortedList<TKey, TValue>.ValueList GetValueListHelper()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList<TKey, TValue>.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x00003062 File Offset: 0x00001262
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x00003062 File Offset: 0x00001262
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns the current instance.</returns>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000E1BBA File Offset: 0x000DFDBA
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		// Token: 0x060040E4 RID: 16612 RVA: 0x000E1BDC File Offset: 0x000DFDDC
		public void Clear()
		{
			this.version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				Array.Clear(this.keys, 0, this._size);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				Array.Clear(this.values, 0, this._size);
			}
			this._size = 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060040E5 RID: 16613 RVA: 0x000E1C30 File Offset: 0x000DFE30
		bool IDictionary.Contains(object key)
		{
			return SortedList<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060040E6 RID: 16614 RVA: 0x000E1C48 File Offset: 0x000DFE48
		public bool ContainsKey(TKey key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />. The value can be null for reference types.</param>
		// Token: 0x060040E7 RID: 16615 RVA: 0x000E1C57 File Offset: 0x000DFE57
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000E1C68 File Offset: 0x000DFE68
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				array[arrayIndex + i] = keyValuePair;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060040E9 RID: 16617 RVA: 0x000E1CF8 File Offset: 0x000DFEF8
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + index] = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				}
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[j + index] = new KeyValuePair<TKey, TValue>(this.keys[j], this.values[j]);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000E1E3C File Offset: 0x000E003C
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 4 : (this.keys.Length * 2));
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000E1E7B File Offset: 0x000E007B
		private TValue GetByIndex(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> of type <see cref="T:System.Collections.Generic.KeyValuePair`2" /> for the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x060040EC RID: 16620 RVA: 0x000E1EAC File Offset: 0x000E00AC
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000E1EAC File Offset: 0x000E00AC
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x060040EE RID: 16622 RVA: 0x000E1EBA File Offset: 0x000E00BA
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060040EF RID: 16623 RVA: 0x000E1EAC File Offset: 0x000E00AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000E1EC8 File Offset: 0x000E00C8
		private TKey GetKey(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" /> and a set operation creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000ED7 RID: 3799
		public TValue this[TKey key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x17000ED8 RID: 3800
		object IDictionary.this[object key]
		{
			get
			{
				if (SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.IndexOfKey((TKey)((object)key));
					if (num >= 0)
					{
						return this.values[num];
					}
				}
				return null;
			}
			set
			{
				if (!SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					throw new ArgumentNullException("key");
				}
				if (value == null && default(TValue) != null)
				{
					throw new ArgumentNullException("value");
				}
				TKey tkey = (TKey)((object)key);
				try
				{
					this[tkey] = (TValue)((object)value);
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
		}

		/// <summary>Searches for the specified key and returns the zero-based index within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of <paramref name="key" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060040F5 RID: 16629 RVA: 0x000E2068 File Offset: 0x000E0268
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Searches for the specified value and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.  The value can be null for reference types.</param>
		// Token: 0x060040F6 RID: 16630 RVA: 0x000E20A9 File Offset: 0x000E02A9
		public int IndexOfValue(TValue value)
		{
			return Array.IndexOf<TValue>(this.values, value, 0, this._size);
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000E20C0 File Offset: 0x000E02C0
		private void Insert(int index, TKey key, TValue value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060040F8 RID: 16632 RVA: 0x000E2164 File Offset: 0x000E0364
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				value = this.values[num];
				return true;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		// Token: 0x060040F9 RID: 16633 RVA: 0x000E219C File Offset: 0x000E039C
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				this.keys[this._size] = default(TKey);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				this.values[this._size] = default(TValue);
			}
			this.version++;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060040FA RID: 16634 RVA: 0x000E2270 File Offset: 0x000E0470
		public bool Remove(TKey key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
			return num >= 0;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060040FB RID: 16635 RVA: 0x000E2297 File Offset: 0x000E0497
		void IDictionary.Remove(object key)
		{
			if (SortedList<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.SortedList`2" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x060040FC RID: 16636 RVA: 0x000E22B0 File Offset: 0x000E04B0
		public void TrimExcess()
		{
			int num = (int)((double)this.keys.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000E0EDC File Offset: 0x000DF0DC
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		// Token: 0x040026F4 RID: 9972
		private TKey[] keys;

		// Token: 0x040026F5 RID: 9973
		private TValue[] values;

		// Token: 0x040026F6 RID: 9974
		private int _size;

		// Token: 0x040026F7 RID: 9975
		private int version;

		// Token: 0x040026F8 RID: 9976
		private IComparer<TKey> comparer;

		// Token: 0x040026F9 RID: 9977
		private SortedList<TKey, TValue>.KeyList keyList;

		// Token: 0x040026FA RID: 9978
		private SortedList<TKey, TValue>.ValueList valueList;

		// Token: 0x040026FB RID: 9979
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040026FC RID: 9980
		private const int DefaultCapacity = 4;

		// Token: 0x040026FD RID: 9981
		private const int MaxArrayLength = 2146435071;

		// Token: 0x020007F1 RID: 2033
		[Serializable]
		private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x060040FE RID: 16638 RVA: 0x000E22E7 File Offset: 0x000E04E7
			internal Enumerator(SortedList<TKey, TValue> sortedList, int getEnumeratorRetType)
			{
				this._sortedList = sortedList;
				this._index = 0;
				this._version = this._sortedList.version;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x060040FF RID: 16639 RVA: 0x000E2327 File Offset: 0x000E0527
			public void Dispose()
			{
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x17000ED9 RID: 3801
			// (get) Token: 0x06004100 RID: 16640 RVA: 0x000E2348 File Offset: 0x000E0548
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x06004101 RID: 16641 RVA: 0x000E2380 File Offset: 0x000E0580
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._key = default(TKey);
				this._value = default(TValue);
				return false;
			}

			// Token: 0x17000EDA RID: 3802
			// (get) Token: 0x06004102 RID: 16642 RVA: 0x000E2434 File Offset: 0x000E0634
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x17000EDB RID: 3803
			// (get) Token: 0x06004103 RID: 16643 RVA: 0x000E2484 File Offset: 0x000E0684
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000EDC RID: 3804
			// (get) Token: 0x06004104 RID: 16644 RVA: 0x000E2498 File Offset: 0x000E0698
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getEnumeratorRetType == 2)
					{
						return new DictionaryEntry(this._key, this._value);
					}
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000EDD RID: 3805
			// (get) Token: 0x06004105 RID: 16645 RVA: 0x000E250D File Offset: 0x000E070D
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x06004106 RID: 16646 RVA: 0x000E2542 File Offset: 0x000E0742
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x040026FE RID: 9982
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x040026FF RID: 9983
			private TKey _key;

			// Token: 0x04002700 RID: 9984
			private TValue _value;

			// Token: 0x04002701 RID: 9985
			private int _index;

			// Token: 0x04002702 RID: 9986
			private int _version;

			// Token: 0x04002703 RID: 9987
			private int _getEnumeratorRetType;

			// Token: 0x04002704 RID: 9988
			internal const int KeyValuePair = 1;

			// Token: 0x04002705 RID: 9989
			internal const int DictEntry = 2;
		}

		// Token: 0x020007F2 RID: 2034
		[Serializable]
		private sealed class SortedListKeyEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x06004107 RID: 16647 RVA: 0x000E2581 File Offset: 0x000E0781
			internal SortedListKeyEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x06004108 RID: 16648 RVA: 0x000E259C File Offset: 0x000E079C
			public void Dispose()
			{
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x06004109 RID: 16649 RVA: 0x000E25B4 File Offset: 0x000E07B4
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentKey = this._sortedList.keys[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentKey = default(TKey);
				return false;
			}

			// Token: 0x17000EDE RID: 3806
			// (get) Token: 0x0600410A RID: 16650 RVA: 0x000E263E File Offset: 0x000E083E
			public TKey Current
			{
				get
				{
					return this._currentKey;
				}
			}

			// Token: 0x17000EDF RID: 3807
			// (get) Token: 0x0600410B RID: 16651 RVA: 0x000E2646 File Offset: 0x000E0846
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x0600410C RID: 16652 RVA: 0x000E267B File Offset: 0x000E087B
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x04002706 RID: 9990
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04002707 RID: 9991
			private int _index;

			// Token: 0x04002708 RID: 9992
			private int _version;

			// Token: 0x04002709 RID: 9993
			private TKey _currentKey;
		}

		// Token: 0x020007F3 RID: 2035
		[Serializable]
		private sealed class SortedListValueEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x0600410D RID: 16653 RVA: 0x000E26AE File Offset: 0x000E08AE
			internal SortedListValueEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x0600410E RID: 16654 RVA: 0x000E26C9 File Offset: 0x000E08C9
			public void Dispose()
			{
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x0600410F RID: 16655 RVA: 0x000E26E0 File Offset: 0x000E08E0
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentValue = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentValue = default(TValue);
				return false;
			}

			// Token: 0x17000EE0 RID: 3808
			// (get) Token: 0x06004110 RID: 16656 RVA: 0x000E276A File Offset: 0x000E096A
			public TValue Current
			{
				get
				{
					return this._currentValue;
				}
			}

			// Token: 0x17000EE1 RID: 3809
			// (get) Token: 0x06004111 RID: 16657 RVA: 0x000E2772 File Offset: 0x000E0972
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x06004112 RID: 16658 RVA: 0x000E27A7 File Offset: 0x000E09A7
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x0400270A RID: 9994
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x0400270B RID: 9995
			private int _index;

			// Token: 0x0400270C RID: 9996
			private int _version;

			// Token: 0x0400270D RID: 9997
			private TValue _currentValue;
		}

		// Token: 0x020007F4 RID: 2036
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class KeyList : IList<TKey>, ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
		{
			// Token: 0x06004113 RID: 16659 RVA: 0x000E27DA File Offset: 0x000E09DA
			internal KeyList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000EE2 RID: 3810
			// (get) Token: 0x06004114 RID: 16660 RVA: 0x000E27E9 File Offset: 0x000E09E9
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000EE3 RID: 3811
			// (get) Token: 0x06004115 RID: 16661 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06004116 RID: 16662 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06004117 RID: 16663 RVA: 0x000E27F6 File Offset: 0x000E09F6
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06004118 RID: 16664 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void Add(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06004119 RID: 16665 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0600411A RID: 16666 RVA: 0x000E280F File Offset: 0x000E0A0F
			public bool Contains(TKey key)
			{
				return this._dict.ContainsKey(key);
			}

			// Token: 0x0600411B RID: 16667 RVA: 0x000E281D File Offset: 0x000E0A1D
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x0600411C RID: 16668 RVA: 0x000E2840 File Offset: 0x000E0A40
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x0600411D RID: 16669 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void Insert(int index, TKey value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17000EE6 RID: 3814
			public TKey this[int index]
			{
				get
				{
					return this._dict.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x06004120 RID: 16672 RVA: 0x000E28BA File Offset: 0x000E0ABA
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x06004121 RID: 16673 RVA: 0x000E28BA File Offset: 0x000E0ABA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x06004122 RID: 16674 RVA: 0x000E28C8 File Offset: 0x000E0AC8
			public int IndexOf(TKey key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06004123 RID: 16675 RVA: 0x000E2803 File Offset: 0x000E0A03
			public bool Remove(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06004124 RID: 16676 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0400270E RID: 9998
			private SortedList<TKey, TValue> _dict;
		}

		// Token: 0x020007F5 RID: 2037
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[Serializable]
		private sealed class ValueList : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
		{
			// Token: 0x06004125 RID: 16677 RVA: 0x000E2918 File Offset: 0x000E0B18
			internal ValueList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000EE7 RID: 3815
			// (get) Token: 0x06004126 RID: 16678 RVA: 0x000E2927 File Offset: 0x000E0B27
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000EE8 RID: 3816
			// (get) Token: 0x06004127 RID: 16679 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000EE9 RID: 3817
			// (get) Token: 0x06004128 RID: 16680 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000EEA RID: 3818
			// (get) Token: 0x06004129 RID: 16681 RVA: 0x000E2934 File Offset: 0x000E0B34
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x0600412A RID: 16682 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void Add(TValue key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0600412B RID: 16683 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0600412C RID: 16684 RVA: 0x000E2941 File Offset: 0x000E0B41
			public bool Contains(TValue value)
			{
				return this._dict.ContainsValue(value);
			}

			// Token: 0x0600412D RID: 16685 RVA: 0x000E294F File Offset: 0x000E0B4F
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x0600412E RID: 16686 RVA: 0x000E2970 File Offset: 0x000E0B70
			void ICollection.CopyTo(Array array, int index)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.values, 0, array, index, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x0600412F RID: 16687 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void Insert(int index, TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17000EEB RID: 3819
			public TValue this[int index]
			{
				get
				{
					return this._dict.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x06004132 RID: 16690 RVA: 0x000E29EA File Offset: 0x000E0BEA
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06004133 RID: 16691 RVA: 0x000E29EA File Offset: 0x000E0BEA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06004134 RID: 16692 RVA: 0x000E29F7 File Offset: 0x000E0BF7
			public int IndexOf(TValue value)
			{
				return Array.IndexOf<TValue>(this._dict.values, value, 0, this._dict.Count);
			}

			// Token: 0x06004135 RID: 16693 RVA: 0x000E2803 File Offset: 0x000E0A03
			public bool Remove(TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06004136 RID: 16694 RVA: 0x000E2803 File Offset: 0x000E0A03
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0400270F RID: 9999
			private SortedList<TKey, TValue> _dict;
		}
	}
}
