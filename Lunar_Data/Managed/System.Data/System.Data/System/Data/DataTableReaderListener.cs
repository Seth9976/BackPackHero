using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200007F RID: 127
	internal sealed class DataTableReaderListener
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00027F38 File Offset: 0x00026138
		internal DataTableReaderListener(DataTableReader reader)
		{
			if (reader == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTableReader");
			}
			if (this._currentDataTable != null)
			{
				this.UnSubscribeEvents();
			}
			this._readerWeak = new WeakReference(reader);
			this._currentDataTable = reader.CurrentDataTable;
			if (this._currentDataTable != null)
			{
				this.SubscribeEvents();
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00027F8D File Offset: 0x0002618D
		internal void CleanUp()
		{
			this.UnSubscribeEvents();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00027F95 File Offset: 0x00026195
		internal void UpdataTable(DataTable datatable)
		{
			if (datatable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this.UnSubscribeEvents();
			this._currentDataTable = datatable;
			this.SubscribeEvents();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00027FB8 File Offset: 0x000261B8
		private void SubscribeEvents()
		{
			if (this._currentDataTable == null)
			{
				return;
			}
			if (this._isSubscribed)
			{
				return;
			}
			this._currentDataTable.Columns.ColumnPropertyChanged += this.SchemaChanged;
			this._currentDataTable.Columns.CollectionChanged += this.SchemaChanged;
			this._currentDataTable.RowChanged += this.DataChanged;
			this._currentDataTable.RowDeleted += this.DataChanged;
			this._currentDataTable.TableCleared += this.DataTableCleared;
			this._isSubscribed = true;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002805C File Offset: 0x0002625C
		private void UnSubscribeEvents()
		{
			if (this._currentDataTable == null)
			{
				return;
			}
			if (!this._isSubscribed)
			{
				return;
			}
			this._currentDataTable.Columns.ColumnPropertyChanged -= this.SchemaChanged;
			this._currentDataTable.Columns.CollectionChanged -= this.SchemaChanged;
			this._currentDataTable.RowChanged -= this.DataChanged;
			this._currentDataTable.RowDeleted -= this.DataChanged;
			this._currentDataTable.TableCleared -= this.DataTableCleared;
			this._isSubscribed = false;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00028100 File Offset: 0x00026300
		private void DataTableCleared(object sender, DataTableClearEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataTableCleared();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00028130 File Offset: 0x00026330
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.SchemaChanged();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00028160 File Offset: 0x00026360
		private void DataChanged(object sender, DataRowChangeEventArgs args)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataChanged(args);
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x040005D4 RID: 1492
		private DataTable _currentDataTable;

		// Token: 0x040005D5 RID: 1493
		private bool _isSubscribed;

		// Token: 0x040005D6 RID: 1494
		private WeakReference _readerWeak;
	}
}
