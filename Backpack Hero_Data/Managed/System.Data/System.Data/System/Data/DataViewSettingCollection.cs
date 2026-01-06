using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Data
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Data.DataViewSetting" /> objects for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000089 RID: 137
	public class DataViewSettingCollection : ICollection, IEnumerable
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0002ABC7 File Offset: 0x00028DC7
		internal DataViewSettingCollection(DataViewManager dataViewManager)
		{
			this._list = new Hashtable();
			base..ctor();
			if (dataViewManager == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataViewManager");
			}
			this._dataViewManager = dataViewManager;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the specified <see cref="T:System.Data.DataTable" /> from the collection. </summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001C6 RID: 454
		public virtual DataViewSetting this[DataTable table]
		{
			get
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				DataViewSetting dataViewSetting = (DataViewSetting)this._list[table];
				if (dataViewSetting == null)
				{
					dataViewSetting = new DataViewSetting();
					this[table] = dataViewSetting;
				}
				return dataViewSetting;
			}
			set
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				value.SetDataViewManager(this._dataViewManager);
				value.SetDataTable(table);
				this._list[table] = value;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002AC60 File Offset: 0x00028E60
		private DataTable GetTable(string tableName)
		{
			DataTable dataTable = null;
			DataSet dataSet = this._dataViewManager.DataSet;
			if (dataSet != null)
			{
				dataTable = dataSet.Tables[tableName];
			}
			return dataTable;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002AC8C File Offset: 0x00028E8C
		private DataTable GetTable(int index)
		{
			DataTable dataTable = null;
			DataSet dataSet = this._dataViewManager.DataSet;
			if (dataSet != null)
			{
				dataTable = dataSet.Tables[index];
			}
			return dataTable;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> of the <see cref="T:System.Data.DataTable" /> specified by its name.</summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001C7 RID: 455
		public virtual DataViewSetting this[string tableName]
		{
			get
			{
				DataTable table = this.GetTable(tableName);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the <see cref="T:System.Data.DataTable" /> specified by its index.</summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001C8 RID: 456
		public virtual DataViewSetting this[int index]
		{
			get
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
			set
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					this[table] = value;
				}
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009B4 RID: 2484 RVA: 0x0002AD20 File Offset: 0x00028F20
		public void CopyTo(Array ar, int index)
		{
			foreach (object obj in this)
			{
				ar.SetValue(obj, index++);
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009B5 RID: 2485 RVA: 0x0002AD50 File Offset: 0x00028F50
		public void CopyTo(DataViewSetting[] ar, int index)
		{
			foreach (object obj in this)
			{
				ar.SetValue(obj, index++);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Data.DataViewSetting" /> objects in the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Data.DataViewSetting" /> objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002AD80 File Offset: 0x00028F80
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					return dataSet.Tables.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009B7 RID: 2487 RVA: 0x0002ADA9 File Offset: 0x00028FA9
		public IEnumerator GetEnumerator()
		{
			return new DataViewSettingCollection.DataViewSettingsEnumerator(this._dataViewManager);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataViewSettingCollection" /> is read-only.</summary>
		/// <returns>Returns true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0000CD07 File Offset: 0x0000AF07
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Data.DataViewSettingCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>This property is always false, unless overridden by a derived class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00005AE9 File Offset: 0x00003CE9
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0000565A File Offset: 0x0000385A
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002ADB6 File Offset: 0x00028FB6
		internal void Remove(DataTable table)
		{
			this._list.Remove(table);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal DataViewSettingCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000614 RID: 1556
		private readonly DataViewManager _dataViewManager;

		// Token: 0x04000615 RID: 1557
		private readonly Hashtable _list;

		// Token: 0x0200008A RID: 138
		private sealed class DataViewSettingsEnumerator : IEnumerator
		{
			// Token: 0x060009BD RID: 2493 RVA: 0x0002ADC4 File Offset: 0x00028FC4
			public DataViewSettingsEnumerator(DataViewManager dvm)
			{
				if (dvm.DataSet != null)
				{
					this._dataViewSettings = dvm.DataViewSettings;
					this._tableEnumerator = dvm.DataSet.Tables.GetEnumerator();
					return;
				}
				this._dataViewSettings = null;
				this._tableEnumerator = Array.Empty<DataTable>().GetEnumerator();
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x0002AE19 File Offset: 0x00029019
			public bool MoveNext()
			{
				return this._tableEnumerator.MoveNext();
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x0002AE26 File Offset: 0x00029026
			public void Reset()
			{
				this._tableEnumerator.Reset();
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002AE33 File Offset: 0x00029033
			public object Current
			{
				get
				{
					return this._dataViewSettings[(DataTable)this._tableEnumerator.Current];
				}
			}

			// Token: 0x04000616 RID: 1558
			private DataViewSettingCollection _dataViewSettings;

			// Token: 0x04000617 RID: 1559
			private IEnumerator _tableEnumerator;
		}
	}
}
