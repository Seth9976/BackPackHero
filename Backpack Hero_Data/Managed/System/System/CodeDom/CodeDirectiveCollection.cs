using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeDirective" /> objects.</summary>
	// Token: 0x0200030A RID: 778
	[Serializable]
	public class CodeDirectiveCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> class. </summary>
		// Token: 0x060018C9 RID: 6345 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
		public CodeDirectiveCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> class with the elements in the specified code directive collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018CA RID: 6346 RVA: 0x0005FA26 File Offset: 0x0005DC26
		public CodeDirectiveCollection(CodeDirectiveCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> class with the code directive objects in the specified array. </summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeDirective" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018CB RID: 6347 RVA: 0x0005FA35 File Offset: 0x0005DC35
		public CodeDirectiveCollection(CodeDirective[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeDirective" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeDirective" /> at the index position.</returns>
		/// <param name="index">The index position to access.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of index positions for the collection. </exception>
		// Token: 0x170004E7 RID: 1255
		public CodeDirective this[int index]
		{
			get
			{
				return (CodeDirective)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeDirective" /> object to the collection.</summary>
		/// <returns>The index position at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to add.</param>
		// Token: 0x060018CE RID: 6350 RVA: 0x00050F9E File Offset: 0x0004F19E
		public int Add(CodeDirective value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds an array of <see cref="T:System.CodeDom.CodeDirective" /> objects to the end of the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeDirective" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018CF RID: 6351 RVA: 0x0005FA58 File Offset: 0x0005DC58
		public void AddRange(CodeDirective[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of the specified <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing the <see cref="T:System.CodeDom.CodeDirective" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018D0 RID: 6352 RVA: 0x0005FA8C File Offset: 0x0005DC8C
		public void AddRange(CodeDirectiveCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeDirective" /> object.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to search for in the collection.</param>
		// Token: 0x060018D1 RID: 6353 RVA: 0x00051038 File Offset: 0x0004F238
		public bool Contains(CodeDirective value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the contents of the collection to a one-dimensional array beginning at the specified index.</summary>
		/// <param name="array">An array of type <see cref="T:System.CodeDom.CodeDirective" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index in the array at which to begin inserting collection objects. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> is greater than the available space between the index of the target array specified by <paramref name="index" /> and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the target array's minimum index. </exception>
		// Token: 0x060018D2 RID: 6354 RVA: 0x00051046 File Offset: 0x0004F246
		public void CopyTo(CodeDirective[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeDirective" /> object, if it exists in the collection.</summary>
		/// <returns>The index position in the collection of the specified object, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to locate in the collection.</param>
		// Token: 0x060018D3 RID: 6355 RVA: 0x00051055 File Offset: 0x0004F255
		public int IndexOf(CodeDirective value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeDirective" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index position where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x060018D4 RID: 6356 RVA: 0x00051063 File Offset: 0x0004F263
		public void Insert(int index, CodeDirective value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeDirective" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x060018D5 RID: 6357 RVA: 0x000510B5 File Offset: 0x0004F2B5
		public void Remove(CodeDirective value)
		{
			base.List.Remove(value);
		}
	}
}
