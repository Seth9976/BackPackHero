using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
	// Token: 0x020006E6 RID: 1766
	public class ListSortDescriptionCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class. </summary>
		// Token: 0x06003805 RID: 14341 RVA: 0x000C4224 File Offset: 0x000C2424
		public ListSortDescriptionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class with the specified array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
		/// <param name="sorts">The array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects to be contained in the collection.</param>
		// Token: 0x06003806 RID: 14342 RVA: 0x000C4238 File Offset: 0x000C2438
		public ListSortDescriptionCollection(ListSortDescription[] sorts)
		{
			if (sorts != null)
			{
				for (int i = 0; i < sorts.Length; i++)
				{
					this._sorts.Add(sorts[i]);
				}
			}
		}

		/// <summary>Gets or sets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get or set in the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">An item is set in the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />, which is read-only.</exception>
		// Token: 0x17000CEC RID: 3308
		public ListSortDescription this[int index]
		{
			get
			{
				return (ListSortDescription)this._sorts[index];
			}
			set
			{
				throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get in the collection </param>
		// Token: 0x17000CEF RID: 3311
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600380D RID: 14349 RVA: 0x000C4289 File Offset: 0x000C2489
		int IList.Add(object value)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600380E RID: 14350 RVA: 0x000C4289 File Offset: 0x000C2489
		void IList.Clear()
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Determines if the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x0600380F RID: 14351 RVA: 0x000C429E File Offset: 0x000C249E
		public bool Contains(object value)
		{
			return ((IList)this._sorts).Contains(value);
		}

		/// <summary>Returns the index of the specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06003810 RID: 14352 RVA: 0x000C42AC File Offset: 0x000C24AC
		public int IndexOf(object value)
		{
			return ((IList)this._sorts).IndexOf(value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to get or set in the collection</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003811 RID: 14353 RVA: 0x000C4289 File Offset: 0x000C2489
		void IList.Insert(int index, object value)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Removes the first occurrence of an item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003812 RID: 14354 RVA: 0x000C4289 File Offset: 0x000C2489
		void IList.Remove(object value)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" />  to remove from the collection</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003813 RID: 14355 RVA: 0x000C4289 File Offset: 0x000C2489
		void IList.RemoveAt(int index)
		{
			throw new InvalidOperationException("Once a ListSortDescriptionCollection has been created it can't be modified.");
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000C42BA File Offset: 0x000C24BA
		public int Count
		{
			get
			{
				return this._sorts.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is thread safe.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x0000390E File Offset: 0x00001B0E
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the current instance that can be used to synchronize access to the collection.</summary>
		/// <returns>The current instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</returns>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x00007575 File Offset: 0x00005775
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the contents of the collection to the specified array, starting at the specified destination array index.</summary>
		/// <param name="array">The destination array for the items copied from the collection.</param>
		/// <param name="index">The index of the destination array at which copying begins.</param>
		// Token: 0x06003817 RID: 14359 RVA: 0x000C42C7 File Offset: 0x000C24C7
		public void CopyTo(Array array, int index)
		{
			this._sorts.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003818 RID: 14360 RVA: 0x000C42D6 File Offset: 0x000C24D6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._sorts.GetEnumerator();
		}

		// Token: 0x040020D8 RID: 8408
		private ArrayList _sorts = new ArrayList();
	}
}
