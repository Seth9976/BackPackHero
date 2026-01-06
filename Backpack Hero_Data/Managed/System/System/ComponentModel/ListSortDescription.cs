using System;

namespace System.ComponentModel
{
	/// <summary>Provides a description of the sort operation applied to a data source.</summary>
	// Token: 0x020006E5 RID: 1765
	public class ListSortDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescription" /> class with the specified property description and direction.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which the data source is sorted.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDescription" />  values.</param>
		// Token: 0x06003800 RID: 14336 RVA: 0x000C41EC File Offset: 0x000C23EC
		public ListSortDescription(PropertyDescriptor property, ListSortDirection direction)
		{
			this.PropertyDescriptor = property;
			this.SortDirection = direction;
		}

		/// <summary>Gets or sets the abstract description of a class property associated with this <see cref="T:System.ComponentModel.ListSortDescription" /></summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.ComponentModel.ListSortDescription" />. </returns>
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000C4202 File Offset: 0x000C2402
		// (set) Token: 0x06003802 RID: 14338 RVA: 0x000C420A File Offset: 0x000C240A
		public PropertyDescriptor PropertyDescriptor { get; set; }

		/// <summary>Gets or sets the direction of the sort operation associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </returns>
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000C4213 File Offset: 0x000C2413
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x000C421B File Offset: 0x000C241B
		public ListSortDirection SortDirection { get; set; }
	}
}
