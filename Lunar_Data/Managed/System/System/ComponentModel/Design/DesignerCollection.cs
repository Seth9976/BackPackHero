using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of designers.</summary>
	// Token: 0x02000765 RID: 1893
	public class DesignerCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified designers.</summary>
		/// <param name="designers">An array of <see cref="T:System.ComponentModel.Design.IDesignerHost" /> objects to store. </param>
		// Token: 0x06003C65 RID: 15461 RVA: 0x000D83A0 File Offset: 0x000D65A0
		public DesignerCollection(IDesignerHost[] designers)
		{
			if (designers != null)
			{
				this._designers = new ArrayList(designers);
				return;
			}
			this._designers = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified set of designers.</summary>
		/// <param name="designers">A list that contains the collection of designers to add. </param>
		// Token: 0x06003C66 RID: 15462 RVA: 0x000D83C3 File Offset: 0x000D65C3
		public DesignerCollection(IList designers)
		{
			this._designers = designers;
		}

		/// <summary>Gets the number of designers in the collection.</summary>
		/// <returns>The number of designers in the collection.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000D83D2 File Offset: 0x000D65D2
		public int Count
		{
			get
			{
				return this._designers.Count;
			}
		}

		/// <summary>Gets the designer at the specified index.</summary>
		/// <returns>The designer at the specified index.</returns>
		/// <param name="index">The index of the designer to return. </param>
		// Token: 0x17000DC5 RID: 3525
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this._designers[index];
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x06003C69 RID: 15465 RVA: 0x000D83F2 File Offset: 0x000D65F2
		public IEnumerator GetEnumerator()
		{
			return this._designers.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000D83FF File Offset: 0x000D65FF
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06003C6D RID: 15469 RVA: 0x000D8407 File Offset: 0x000D6607
		void ICollection.CopyTo(Array array, int index)
		{
			this._designers.CopyTo(array, index);
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x06003C6E RID: 15470 RVA: 0x000D8416 File Offset: 0x000D6616
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400222F RID: 8751
		private IList _designers;
	}
}
