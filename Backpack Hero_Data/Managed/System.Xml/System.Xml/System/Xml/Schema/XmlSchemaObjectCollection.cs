using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>A collection of <see cref="T:System.Xml.Schema.XmlSchemaObject" />s.</summary>
	// Token: 0x020005CB RID: 1483
	public class XmlSchemaObjectCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the XmlSchemaObjectCollection class.</summary>
		// Token: 0x06003B8D RID: 15245 RVA: 0x0009B578 File Offset: 0x00099778
		public XmlSchemaObjectCollection()
		{
		}

		/// <summary>Initializes a new instance of the XmlSchemaObjectCollection class that takes an <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <param name="parent">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x06003B8E RID: 15246 RVA: 0x0014E544 File Offset: 0x0014C744
		public XmlSchemaObjectCollection(XmlSchemaObject parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x17000B8A RID: 2954
		public virtual XmlSchemaObject this[int index]
		{
			get
			{
				return (XmlSchemaObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Returns an enumerator for iterating through the XmlSchemaObjects contained in the XmlSchemaObjectCollection.</summary>
		/// <returns>The iterator returns <see cref="T:System.Xml.Schema.XmlSchemaObjectEnumerator" />.</returns>
		// Token: 0x06003B91 RID: 15249 RVA: 0x0014E566 File Offset: 0x0014C766
		public new XmlSchemaObjectEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectEnumerator(base.InnerList.GetEnumerator());
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <returns>The index at which the item has been added.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter specified is not of type <see cref="T:System.Xml.Schema.XmlSchemaExternal" /> or its derived types <see cref="T:System.Xml.Schema.XmlSchemaImport" />, <see cref="T:System.Xml.Schema.XmlSchemaInclude" />, and <see cref="T:System.Xml.Schema.XmlSchemaRedefine" />.</exception>
		// Token: 0x06003B92 RID: 15250 RVA: 0x0009B522 File Offset: 0x00099722
		public int Add(XmlSchemaObject item)
		{
			return base.List.Add(item);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <param name="index">The zero-based index at which an item should be inserted. </param>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		// Token: 0x06003B93 RID: 15251 RVA: 0x0009B530 File Offset: 0x00099730
		public void Insert(int index, XmlSchemaObject item)
		{
			base.List.Insert(index, item);
		}

		/// <summary>Gets the collection index corresponding to the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The index corresponding to the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> whose index you want to return. </param>
		// Token: 0x06003B94 RID: 15252 RVA: 0x0009B53F File Offset: 0x0009973F
		public int IndexOf(XmlSchemaObject item)
		{
			return base.List.IndexOf(item);
		}

		/// <summary>Indicates if the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is in the XmlSchemaObjectCollection.</summary>
		/// <returns>true if the specified qualified name is in the collection; otherwise, returns false. If null is supplied, false is returned because there is no qualified name with a null name.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x06003B95 RID: 15253 RVA: 0x0009B54D File Offset: 0x0009974D
		public bool Contains(XmlSchemaObject item)
		{
			return base.List.Contains(item);
		}

		/// <summary>Removes an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> from the XmlSchemaObjectCollection.</summary>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to remove. </param>
		// Token: 0x06003B96 RID: 15254 RVA: 0x0009B55B File Offset: 0x0009975B
		public void Remove(XmlSchemaObject item)
		{
			base.List.Remove(item);
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchemaObject" />s from the collection into the given array, starting at the given index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the XmlSchemaObjectCollection. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the array at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multi-dimensional.- or - <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.- or - The number of elements in the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is greater than the available space from index to the end of the destination array. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> cannot be cast automatically to the type of the destination array. </exception>
		// Token: 0x06003B97 RID: 15255 RVA: 0x0009B569 File Offset: 0x00099769
		public void CopyTo(XmlSchemaObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>OnInsert is invoked before the standard Insert behavior. For more information, see OnInsert method <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x06003B98 RID: 15256 RVA: 0x0014E578 File Offset: 0x0014C778
		protected override void OnInsert(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnAdd(this, item);
			}
		}

		/// <summary>OnSet is invoked before the standard Set behavior. For more information, see the OnSet method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="oldValue">The old value. </param>
		/// <param name="newValue">The new value. </param>
		// Token: 0x06003B99 RID: 15257 RVA: 0x0014E58F File Offset: 0x0014C78F
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, oldValue);
				this.parent.OnAdd(this, newValue);
			}
		}

		/// <summary>OnClear is invoked before the standard Clear behavior. For more information, see OnClear method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		// Token: 0x06003B9A RID: 15258 RVA: 0x0014E5B3 File Offset: 0x0014C7B3
		protected override void OnClear()
		{
			if (this.parent != null)
			{
				this.parent.OnClear(this);
			}
		}

		/// <summary>OnRemove is invoked before the standard Remove behavior. For more information, see the OnRemove method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x06003B9B RID: 15259 RVA: 0x0014E5C9 File Offset: 0x0014C7C9
		protected override void OnRemove(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, item);
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x0014E5E0 File Offset: 0x0014C7E0
		internal XmlSchemaObjectCollection Clone()
		{
			return new XmlSchemaObjectCollection { this };
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x0014E5EE File Offset: 0x0014C7EE
		private void Add(XmlSchemaObjectCollection collToAdd)
		{
			base.InnerList.InsertRange(0, collToAdd);
		}

		// Token: 0x04002B87 RID: 11143
		private XmlSchemaObject parent;
	}
}
