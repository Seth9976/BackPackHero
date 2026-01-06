using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000086 RID: 134
	internal sealed class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0002AA4E File Offset: 0x00028C4E
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dataViewManager)
		{
			this._dataViewManager = dataViewManager;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002AA5D File Offset: 0x00028C5D
		internal void Reset()
		{
			this._propsCollection = null;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002AA66 File Offset: 0x00028C66
		internal DataView GetDataView(DataTable table)
		{
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this._dataViewManager);
			return dataView;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00017DFA File Offset: 0x00015FFA
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00003DF6 File Offset: 0x00001FF6
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00003DF6 File Offset: 0x00001FF6
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00003DF6 File Offset: 0x00001FF6
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00003DF6 File Offset: 0x00001FF6
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00003DF6 File Offset: 0x00001FF6
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00003DF6 File Offset: 0x00001FF6
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00017E02 File Offset: 0x00016002
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00017E02 File Offset: 0x00016002
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00017E0A File Offset: 0x0001600A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002AA7C File Offset: 0x00028C7C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propsCollection == null)
			{
				PropertyDescriptor[] array = null;
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					int count = dataSet.Tables.Count;
					array = new PropertyDescriptor[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new DataTablePropertyDescriptor(dataSet.Tables[i]);
					}
				}
				this._propsCollection = new PropertyDescriptorCollection(array);
			}
			return this._propsCollection;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000565A File Offset: 0x0000385A
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000603 RID: 1539
		private DataViewManager _dataViewManager;

		// Token: 0x04000604 RID: 1540
		private PropertyDescriptorCollection _propsCollection;
	}
}
