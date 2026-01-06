using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents the default settings for <see cref="P:System.Data.DataView.ApplyDefaultSort" />, <see cref="P:System.Data.DataView.DataViewManager" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.RowStateFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="P:System.Data.DataView.Table" /> for DataViews created from the <see cref="T:System.Data.DataViewManager" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000088 RID: 136
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0002AAE7 File Offset: 0x00028CE7
		internal DataViewSetting()
		{
		}

		/// <summary>Gets or sets a value indicating whether to use the default sort.</summary>
		/// <returns>true if the default sort is used; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002AB0D File Offset: 0x00028D0D
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0002AB15 File Offset: 0x00028D15
		public bool ApplyDefaultSort
		{
			get
			{
				return this._applyDefaultSort;
			}
			set
			{
				if (this._applyDefaultSort != value)
				{
					this._applyDefaultSort = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> that contains this <see cref="T:System.Data.DataViewSetting" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002AB27 File Offset: 0x00028D27
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this._dataViewManager;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002AB2F File Offset: 0x00028D2F
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._dataViewManager != dataViewManager)
			{
				this._dataViewManager = dataViewManager;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the <see cref="T:System.Data.DataViewSetting" /> properties apply.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0002AB41 File Offset: 0x00028D41
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002AB49 File Offset: 0x00028D49
		internal void SetDataTable(DataTable table)
		{
			if (this._table != table)
			{
				this._table = table;
			}
		}

		/// <summary>Gets or sets the filter to apply in the <see cref="T:System.Data.DataView" />. See <see cref="P:System.Data.DataView.RowFilter" /> for a code sample using RowFilter.</summary>
		/// <returns>A string that contains the filter to apply.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0002AB5B File Offset: 0x00028D5B
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0002AB63 File Offset: 0x00028D63
		public string RowFilter
		{
			get
			{
				return this._rowFilter;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._rowFilter != value)
				{
					this._rowFilter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether to display Current, Deleted, Modified Current, ModifiedOriginal, New, Original, Unchanged, or no rows in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A value that indicates which rows to display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002AB84 File Offset: 0x00028D84
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0002AB8C File Offset: 0x00028D8C
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._rowStateFilter;
			}
			set
			{
				if (this._rowStateFilter != value)
				{
					this._rowStateFilter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating the sort to apply in the <see cref="T:System.Data.DataView" />. </summary>
		/// <returns>The sort to apply in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002AB9E File Offset: 0x00028D9E
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0002ABA6 File Offset: 0x00028DA6
		public string Sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._sort != value)
				{
					this._sort = value;
				}
			}
		}

		// Token: 0x0400060E RID: 1550
		private DataViewManager _dataViewManager;

		// Token: 0x0400060F RID: 1551
		private DataTable _table;

		// Token: 0x04000610 RID: 1552
		private string _sort = string.Empty;

		// Token: 0x04000611 RID: 1553
		private string _rowFilter = string.Empty;

		// Token: 0x04000612 RID: 1554
		private DataViewRowState _rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x04000613 RID: 1555
		private bool _applyDefaultSort;
	}
}
