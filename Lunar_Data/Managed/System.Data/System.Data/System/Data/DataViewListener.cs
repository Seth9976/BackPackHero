using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000084 RID: 132
	internal sealed class DataViewListener
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x0002A077 File Offset: 0x00028277
		internal DataViewListener(DataView dv)
		{
			this._objectID = dv.ObjectID;
			this._dvWeak = new WeakReference(dv);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002A098 File Offset: 0x00028298
		private void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ChildRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002A0CC File Offset: 0x000282CC
		private void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ParentRelationCollectionChanged(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002A100 File Offset: 0x00028300
		private void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.ColumnCollectionChangedInternal(sender, e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002A134 File Offset: 0x00028334
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.MaintainDataView(changedType, row, trackAddRemove);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002A168 File Offset: 0x00028368
		internal void IndexListChanged(ListChangedEventArgs e)
		{
			DataView dataView = (DataView)this._dvWeak.Target;
			if (dataView != null)
			{
				dataView.IndexListChangedInternal(e);
				return;
			}
			this.CleanUp(true);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002A198 File Offset: 0x00028398
		internal void RegisterMetaDataEvents(DataTable table)
		{
			this._table = table;
			if (table != null)
			{
				this.RegisterListener(table);
				CollectionChangeEventHandler collectionChangeEventHandler = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged += collectionChangeEventHandler;
				table.Columns.CollectionChanged += collectionChangeEventHandler;
				CollectionChangeEventHandler collectionChangeEventHandler2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged += collectionChangeEventHandler2;
				table.ChildRelations.CollectionChanged += collectionChangeEventHandler2;
				CollectionChangeEventHandler collectionChangeEventHandler3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged += collectionChangeEventHandler3;
				table.ParentRelations.CollectionChanged += collectionChangeEventHandler3;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002A232 File Offset: 0x00028432
		internal void UnregisterMetaDataEvents()
		{
			this.UnregisterMetaDataEvents(true);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002A23C File Offset: 0x0002843C
		private void UnregisterMetaDataEvents(bool updateListeners)
		{
			DataTable table = this._table;
			this._table = null;
			if (table != null)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = new CollectionChangeEventHandler(this.ColumnCollectionChanged);
				table.Columns.ColumnPropertyChanged -= collectionChangeEventHandler;
				table.Columns.CollectionChanged -= collectionChangeEventHandler;
				CollectionChangeEventHandler collectionChangeEventHandler2 = new CollectionChangeEventHandler(this.ChildRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ChildRelations).RelationPropertyChanged -= collectionChangeEventHandler2;
				table.ChildRelations.CollectionChanged -= collectionChangeEventHandler2;
				CollectionChangeEventHandler collectionChangeEventHandler3 = new CollectionChangeEventHandler(this.ParentRelationCollectionChanged);
				((DataRelationCollection.DataTableRelationCollection)table.ParentRelations).RelationPropertyChanged -= collectionChangeEventHandler3;
				table.ParentRelations.CollectionChanged -= collectionChangeEventHandler3;
				if (updateListeners)
				{
					List<DataViewListener> listeners = table.GetListeners();
					List<DataViewListener> list = listeners;
					lock (list)
					{
						listeners.Remove(this);
					}
				}
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002A318 File Offset: 0x00028518
		internal void RegisterListChangedEvent(Index index)
		{
			this._index = index;
			if (index != null)
			{
				lock (index)
				{
					index.AddRef();
					index.ListChangedAdd(this);
				}
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002A364 File Offset: 0x00028564
		internal void UnregisterListChangedEvent()
		{
			Index index = this._index;
			this._index = null;
			if (index != null)
			{
				Index index2 = index;
				lock (index2)
				{
					index.ListChangedRemove(this);
					if (index.RemoveRef() <= 1)
					{
						index.RemoveRef();
					}
				}
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002A3C4 File Offset: 0x000285C4
		private void CleanUp(bool updateListeners)
		{
			this.UnregisterMetaDataEvents(updateListeners);
			this.UnregisterListChangedEvent();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002A3D4 File Offset: 0x000285D4
		private void RegisterListener(DataTable table)
		{
			List<DataViewListener> listeners = table.GetListeners();
			List<DataViewListener> list = listeners;
			lock (list)
			{
				int num = listeners.Count - 1;
				while (0 <= num)
				{
					DataViewListener dataViewListener = listeners[num];
					if (!dataViewListener._dvWeak.IsAlive)
					{
						listeners.RemoveAt(num);
						dataViewListener.CleanUp(false);
					}
					num--;
				}
				listeners.Add(this);
			}
		}

		// Token: 0x040005F8 RID: 1528
		private readonly WeakReference _dvWeak;

		// Token: 0x040005F9 RID: 1529
		private DataTable _table;

		// Token: 0x040005FA RID: 1530
		private Index _index;

		// Token: 0x040005FB RID: 1531
		internal readonly int _objectID;
	}
}
