using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
	// Token: 0x02000761 RID: 1889
	public class DesignerVerbCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class.</summary>
		// Token: 0x06003C48 RID: 15432 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
		public DesignerVerbCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class using the specified array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array that indicates the verbs to contain within the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003C49 RID: 15433 RVA: 0x000D7FCD File Offset: 0x000D61CD
		public DesignerVerbCollection(DesignerVerb[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at each valid index in the collection.</returns>
		/// <param name="index">The index at which to get or set the <see cref="T:System.ComponentModel.Design.DesignerVerb" />. </param>
		// Token: 0x17000DC2 RID: 3522
		public DesignerVerb this[int index]
		{
			get
			{
				return (DesignerVerb)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to the collection.</summary>
		/// <returns>The index in the collection at which the verb was added.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add to the collection. </param>
		// Token: 0x06003C4C RID: 15436 RVA: 0x00050F9E File Offset: 0x0004F19E
		public int Add(DesignerVerb value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified set of designer verbs to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003C4D RID: 15437 RVA: 0x000D7FF0 File Offset: 0x000D61F0
		public void AddRange(DesignerVerb[] value)
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

		/// <summary>Adds the specified collection of designer verbs to the collection.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06003C4E RID: 15438 RVA: 0x000D8024 File Offset: 0x000D6224
		public void AddRange(DesignerVerbCollection value)
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

		/// <summary>Inserts the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <param name="index">The index in the collection at which to insert the verb. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to insert in the collection. </param>
		// Token: 0x06003C4F RID: 15439 RVA: 0x00051063 File Offset: 0x0004F263
		public void Insert(int index, DesignerVerb value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" />.</summary>
		/// <returns>The index of the specified object if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> whose index to get in the collection. </param>
		// Token: 0x06003C50 RID: 15440 RVA: 0x00051055 File Offset: 0x0004F255
		public int IndexOf(DesignerVerb value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> exists in the collection.</summary>
		/// <returns>true if the specified object exists in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to search for in the collection. </param>
		// Token: 0x06003C51 RID: 15441 RVA: 0x00051038 File Offset: 0x0004F238
		public bool Contains(DesignerVerb value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove from the collection. </param>
		// Token: 0x06003C52 RID: 15442 RVA: 0x000510B5 File Offset: 0x0004F2B5
		public void Remove(DesignerVerb value)
		{
			base.List.Remove(value);
		}

		/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array beginning at the specified destination index.</summary>
		/// <param name="array">The array to copy collection members to. </param>
		/// <param name="index">The destination index to begin copying to. </param>
		// Token: 0x06003C53 RID: 15443 RVA: 0x00051046 File Offset: 0x0004F246
		public void CopyTo(DesignerVerb[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Raises the Set event.</summary>
		/// <param name="index">The index at which to set the item. </param>
		/// <param name="oldValue">The old object. </param>
		/// <param name="newValue">The new object. </param>
		// Token: 0x06003C54 RID: 15444 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Raises the Insert event.</summary>
		/// <param name="index">The index at which to insert an item. </param>
		/// <param name="value">The object to insert. </param>
		// Token: 0x06003C55 RID: 15445 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Raises the Clear event.</summary>
		// Token: 0x06003C56 RID: 15446 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnClear()
		{
		}

		/// <summary>Raises the Remove event.</summary>
		/// <param name="index">The index at which to remove the item. </param>
		/// <param name="value">The object to remove. </param>
		// Token: 0x06003C57 RID: 15447 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Raises the Validate event.</summary>
		/// <param name="value">The object to validate. </param>
		// Token: 0x06003C58 RID: 15448 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnValidate(object value)
		{
		}
	}
}
