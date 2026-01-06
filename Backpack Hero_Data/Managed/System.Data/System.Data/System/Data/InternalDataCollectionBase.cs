using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	/// <summary>Provides the base functionality for creating collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000039 RID: 57
	public class InternalDataCollectionBase : ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in a collection.</summary>
		/// <returns>The total number of elements in a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000CA92 File Offset: 0x0000AC92
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.InternalDataCollectionBase" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Data.InternalDataCollectionBase" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.InternalDataCollectionBase" /> object's elements into. </param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600024A RID: 586 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		public virtual void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600024B RID: 587 RVA: 0x0000CAAE File Offset: 0x0000ACAE
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00005AE9 File Offset: 0x00003CE9
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is synchonized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00005AE9 File Offset: 0x00003CE9
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000CABB File Offset: 0x0000ACBB
		internal int NamesEqual(string s1, string s2, bool fCaseSensitive, CultureInfo locale)
		{
			if (fCaseSensitive)
			{
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (locale.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0)
				{
					return 0;
				}
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return -1;
				}
				return 1;
			}
		}

		/// <summary>Gets an object that can be used to synchronize the collection.</summary>
		/// <returns>The <see cref="T:System.object" /> used to synchronize the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000565A File Offset: 0x0000385A
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the items of the collection as a list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the collection.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000478 RID: 1144
		internal static readonly CollectionChangeEventArgs s_refreshEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
	}
}
