using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Extends the <see cref="T:System.ComponentModel.IBindingList" /> interface by providing advanced sorting and filtering capabilities.</summary>
	// Token: 0x020006C5 RID: 1733
	public interface IBindingListView : IBindingList, IList, ICollection, IEnumerable
	{
		/// <summary>Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</summary>
		/// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sorts to apply to the data source.</param>
		// Token: 0x06003770 RID: 14192
		void ApplySort(ListSortDescriptionCollection sorts);

		/// <summary>Gets or sets the filter to be used to exclude items from the collection of items returned by the data source</summary>
		/// <returns>The string used to filter items out in the item collection returned by the data source. </returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003771 RID: 14193
		// (set) Token: 0x06003772 RID: 14194
		string Filter { get; set; }

		/// <summary>Gets the collection of sort descriptions currently applied to the data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> currently applied to the data source.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003773 RID: 14195
		ListSortDescriptionCollection SortDescriptions { get; }

		/// <summary>Removes the current filter applied to the data source.</summary>
		// Token: 0x06003774 RID: 14196
		void RemoveFilter();

		/// <summary>Gets a value indicating whether the data source supports advanced sorting. </summary>
		/// <returns>true if the data source supports advanced sorting; otherwise, false. </returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003775 RID: 14197
		bool SupportsAdvancedSorting { get; }

		/// <summary>Gets a value indicating whether the data source supports filtering. </summary>
		/// <returns>true if the data source supports filtering; otherwise, false. </returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003776 RID: 14198
		bool SupportsFiltering { get; }
	}
}
