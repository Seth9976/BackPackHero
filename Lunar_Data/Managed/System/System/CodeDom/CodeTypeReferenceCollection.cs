using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeReference" /> objects.</summary>
	// Token: 0x020002EE RID: 750
	[Serializable]
	public class CodeTypeReferenceCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> class.</summary>
		// Token: 0x060017F7 RID: 6135 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
		public CodeTypeReferenceCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> with which to initialize the collection. </param>
		// Token: 0x060017F8 RID: 6136 RVA: 0x0005ED25 File Offset: 0x0005CF25
		public CodeTypeReferenceCollection(CodeTypeReferenceCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeTypeReference" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> objects with which to initialize the collection. </param>
		// Token: 0x060017F9 RID: 6137 RVA: 0x0005ED34 File Offset: 0x0005CF34
		public CodeTypeReferenceCollection(CodeTypeReference[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeReference" /> at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x170004B2 RID: 1202
		public CodeTypeReference this[int index]
		{
			get
			{
				return (CodeTypeReference)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to add. </param>
		// Token: 0x060017FC RID: 6140 RVA: 0x00050F9E File Offset: 0x0004F19E
		public int Add(CodeTypeReference value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection using the specified data type name.</summary>
		/// <param name="value">The name of a data type for which to add a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection. </param>
		// Token: 0x060017FD RID: 6141 RVA: 0x0005ED56 File Offset: 0x0005CF56
		public void Add(string value)
		{
			this.Add(new CodeTypeReference(value));
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection using the specified data type.</summary>
		/// <param name="value">The data type for which to add a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection. </param>
		// Token: 0x060017FE RID: 6142 RVA: 0x0005ED65 File Offset: 0x0005CF65
		public void Add(Type value)
		{
			this.Add(new CodeTypeReference(value));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeReference" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeReference" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060017FF RID: 6143 RVA: 0x0005ED74 File Offset: 0x0005CF74
		public void AddRange(CodeTypeReference[] value)
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

		/// <summary>Adds the contents of the specified <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001800 RID: 6144 RVA: 0x0005EDA8 File Offset: 0x0005CFA8
		public void AddRange(CodeTypeReferenceCollection value)
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

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <returns>true if the <see cref="T:System.CodeDom.CodeTypeReference" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to search for in the collection. </param>
		// Token: 0x06001801 RID: 6145 RVA: 0x00051038 File Offset: 0x0004F238
		public bool Contains(CodeTypeReference value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06001802 RID: 6146 RVA: 0x00051046 File Offset: 0x0004F246
		public void CopyTo(CodeTypeReference[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeTypeReference" />, if it exists in the collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.CodeDom.CodeTypeReference" /> in the collection if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to locate in the collection. </param>
		// Token: 0x06001803 RID: 6147 RVA: 0x00051055 File Offset: 0x0004F255
		public int IndexOf(CodeTypeReference value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.CodeDom.CodeTypeReference" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the item should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to insert. </param>
		// Token: 0x06001804 RID: 6148 RVA: 0x00051063 File Offset: 0x0004F263
		public void Insert(int index, CodeTypeReference value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeReference" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06001805 RID: 6149 RVA: 0x000510B5 File Offset: 0x0004F2B5
		public void Remove(CodeTypeReference value)
		{
			base.List.Remove(value);
		}
	}
}
