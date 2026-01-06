using System;

namespace System.Collections.Specialized
{
	/// <summary>Represents an indexed collection of key/value pairs.</summary>
	// Token: 0x020007BB RID: 1979
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />. </exception>
		// Token: 0x17000E36 RID: 3638
		object this[int index] { get; set; }

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the entire <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</returns>
		// Token: 0x06003EC6 RID: 16070
		IDictionaryEnumerator GetEnumerator();

		/// <summary>Inserts a key/value pair into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the key/value pair should be inserted.</param>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.  The value can be null.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection is read-only.-or-The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection has a fixed size.</exception>
		// Token: 0x06003EC7 RID: 16071
		void Insert(int index, object key, object value);

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection is read-only.-or- The <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection has a fixed size. </exception>
		// Token: 0x06003EC8 RID: 16072
		void RemoveAt(int index);
	}
}
