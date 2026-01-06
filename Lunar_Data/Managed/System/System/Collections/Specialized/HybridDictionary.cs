using System;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary by using a <see cref="T:System.Collections.Specialized.ListDictionary" /> while the collection is small, and then switching to a <see cref="T:System.Collections.Hashtable" /> when the collection gets large.</summary>
	// Token: 0x020007BA RID: 1978
	[Serializable]
	public class HybridDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty case-sensitive <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x06003EAE RID: 16046 RVA: 0x0000219B File Offset: 0x0000039B
		public HybridDictionary()
		{
		}

		/// <summary>Creates a case-sensitive <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified initial size.</summary>
		/// <param name="initialSize">The approximate number of entries that the <see cref="T:System.Collections.Specialized.HybridDictionary" /> can initially contain. </param>
		// Token: 0x06003EAF RID: 16047 RVA: 0x000DC763 File Offset: 0x000DA963
		public HybridDictionary(int initialSize)
			: this(initialSize, false)
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified case sensitivity.</summary>
		/// <param name="caseInsensitive">A Boolean that denotes whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is case-insensitive. </param>
		// Token: 0x06003EB0 RID: 16048 RVA: 0x000DC76D File Offset: 0x000DA96D
		public HybridDictionary(bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified initial size and case sensitivity.</summary>
		/// <param name="initialSize">The approximate number of entries that the <see cref="T:System.Collections.Specialized.HybridDictionary" /> can initially contain. </param>
		/// <param name="caseInsensitive">A Boolean that denotes whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is case-insensitive. </param>
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000DC77C File Offset: 0x000DA97C
		public HybridDictionary(int initialSize, bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
			if (initialSize >= 6)
			{
				if (caseInsensitive)
				{
					this.hashtable = new Hashtable(initialSize, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this.hashtable = new Hashtable(initialSize);
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x17000E2D RID: 3629
		public object this[object key]
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable[key];
				}
				if (listDictionary != null)
				{
					return listDictionary[key];
				}
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return null;
			}
			set
			{
				if (this.hashtable != null)
				{
					this.hashtable[key] = value;
					return;
				}
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
					this.list[key] = value;
					return;
				}
				if (this.list.Count >= 8)
				{
					this.ChangeOver();
					this.hashtable[key] = value;
					return;
				}
				this.list[key] = value;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000DC877 File Offset: 0x000DAA77
		private ListDictionary List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				}
				return this.list;
			}
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000DC8A4 File Offset: 0x000DAAA4
		private void ChangeOver()
		{
			IDictionaryEnumerator enumerator = this.list.GetEnumerator();
			Hashtable hashtable;
			if (this.caseInsensitive)
			{
				hashtable = new Hashtable(13, StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashtable = new Hashtable(13);
			}
			while (enumerator.MoveNext())
			{
				hashtable.Add(enumerator.Key, enumerator.Value);
			}
			this.hashtable = hashtable;
			this.list = null;
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000DC908 File Offset: 0x000DAB08
		public int Count
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				if (listDictionary != null)
				{
					return listDictionary.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x000DC93B File Offset: 0x000DAB3B
		public ICollection Keys
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Keys;
				}
				return this.List.Keys;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x00007575 File Offset: 0x00005775
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000DC95C File Offset: 0x000DAB5C
		public ICollection Values
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Values;
				}
				return this.List.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.HybridDictionary" />. </exception>
		// Token: 0x06003EBD RID: 16061 RVA: 0x000DC980 File Offset: 0x000DAB80
		public void Add(object key, object value)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Add(key, value);
				return;
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				this.list.Add(key, value);
				return;
			}
			if (this.list.Count + 1 >= 9)
			{
				this.ChangeOver();
				this.hashtable.Add(key, value);
				return;
			}
			this.list.Add(key, value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x06003EBE RID: 16062 RVA: 0x000DCA06 File Offset: 0x000DAC06
		public void Clear()
		{
			if (this.hashtable != null)
			{
				Hashtable hashtable = this.hashtable;
				this.hashtable = null;
				hashtable.Clear();
			}
			if (this.list != null)
			{
				ListDictionary listDictionary = this.list;
				this.list = null;
				listDictionary.Clear();
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.HybridDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06003EBF RID: 16063 RVA: 0x000DCA3C File Offset: 0x000DAC3C
		public bool Contains(object key)
		{
			ListDictionary listDictionary = this.list;
			if (this.hashtable != null)
			{
				return this.hashtable.Contains(key);
			}
			if (listDictionary != null)
			{
				return listDictionary.Contains(key);
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.HybridDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.HybridDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06003EC0 RID: 16064 RVA: 0x000DCA7F File Offset: 0x000DAC7F
		public void CopyTo(Array array, int index)
		{
			if (this.hashtable != null)
			{
				this.hashtable.CopyTo(array, index);
				return;
			}
			this.List.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000DCAA4 File Offset: 0x000DACA4
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x06003EC2 RID: 16066 RVA: 0x000DCAF4 File Offset: 0x000DACF4
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06003EC3 RID: 16067 RVA: 0x000DCB43 File Offset: 0x000DAD43
		public void Remove(object key)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Remove(key);
				return;
			}
			if (this.list != null)
			{
				this.list.Remove(key);
				return;
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
		}

		// Token: 0x0400264F RID: 9807
		private const int CutoverPoint = 9;

		// Token: 0x04002650 RID: 9808
		private const int InitialHashtableSize = 13;

		// Token: 0x04002651 RID: 9809
		private const int FixedSizeCutoverPoint = 6;

		// Token: 0x04002652 RID: 9810
		private ListDictionary list;

		// Token: 0x04002653 RID: 9811
		private Hashtable hashtable;

		// Token: 0x04002654 RID: 9812
		private readonly bool caseInsensitive;
	}
}
