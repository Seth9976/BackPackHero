using System;

namespace System.Data
{
	// Token: 0x020000CF RID: 207
	internal sealed class RelatedView : DataView, IFilter
	{
		// Token: 0x06000C0A RID: 3082 RVA: 0x00036738 File Offset: 0x00034938
		public RelatedView(DataColumn[] columns, object[] values)
			: base(columns[0].Table, false)
		{
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			this._parentRowView = null;
			this._parentKey = null;
			this._childKey = new DataKey(columns, true);
			this._filterValues = values;
			base.ResetRowViewCache();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0003678F File Offset: 0x0003498F
		public RelatedView(DataRowView parentRowView, DataKey parentKey, DataColumn[] childKeyColumns)
			: base(childKeyColumns[0].Table, false)
		{
			this._filterValues = null;
			this._parentRowView = parentRowView;
			this._parentKey = new DataKey?(parentKey);
			this._childKey = new DataKey(childKeyColumns, true);
			base.ResetRowViewCache();
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000367D0 File Offset: 0x000349D0
		private object[] GetParentValues()
		{
			if (this._filterValues != null)
			{
				return this._filterValues;
			}
			if (!this._parentRowView.HasRecord())
			{
				return null;
			}
			return this._parentKey.Value.GetKeyValues(this._parentRowView.GetRecord());
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0003681C File Offset: 0x00034A1C
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			object[] parentValues = this.GetParentValues();
			if (parentValues == null)
			{
				return false;
			}
			object[] keyValues = row.GetKeyValues(this._childKey, version);
			bool flag = true;
			if (keyValues.Length != parentValues.Length)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < keyValues.Length; i++)
				{
					if (!keyValues[i].Equals(parentValues[i]))
					{
						flag = false;
						break;
					}
				}
			}
			IFilter filter = base.GetFilter();
			if (filter != null)
			{
				flag &= filter.Invoke(row, version);
			}
			return flag;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0000565A File Offset: 0x0000385A
		internal override IFilter GetFilter()
		{
			return this;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0003688C File Offset: 0x00034A8C
		public override DataRowView AddNew()
		{
			DataRowView dataRowView = base.AddNew();
			dataRowView.Row.SetKeyValues(this._childKey, this.GetParentValues());
			return dataRowView;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000368AB File Offset: 0x00034AAB
		internal override void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			base.SetIndex2(newSort, newRowStates, newRowFilter, false);
			base.Reset();
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000368C0 File Offset: 0x00034AC0
		public override bool Equals(DataView dv)
		{
			RelatedView relatedView = dv as RelatedView;
			if (relatedView == null)
			{
				return false;
			}
			if (!base.Equals(dv))
			{
				return false;
			}
			object[] array;
			if (this._filterValues != null)
			{
				array = this._childKey.ColumnsReference;
				object[] array2 = array;
				array = relatedView._childKey.ColumnsReference;
				return this.CompareArray(array2, array) && this.CompareArray(this._filterValues, relatedView._filterValues);
			}
			if (relatedView._filterValues != null)
			{
				return false;
			}
			array = this._childKey.ColumnsReference;
			object[] array3 = array;
			array = relatedView._childKey.ColumnsReference;
			if (this.CompareArray(array3, array))
			{
				array = this._parentKey.Value.ColumnsReference;
				object[] array4 = array;
				array = this._parentKey.Value.ColumnsReference;
				if (this.CompareArray(array4, array))
				{
					return this._parentRowView.Equals(relatedView._parentRowView);
				}
			}
			return false;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00036994 File Offset: 0x00034B94
		private bool CompareArray(object[] value1, object[] value2)
		{
			if (value1 == null || value2 == null)
			{
				return value1 == value2;
			}
			if (value1.Length != value2.Length)
			{
				return false;
			}
			for (int i = 0; i < value1.Length; i++)
			{
				if (value1[i] != value2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040007C7 RID: 1991
		private readonly DataKey? _parentKey;

		// Token: 0x040007C8 RID: 1992
		private readonly DataKey _childKey;

		// Token: 0x040007C9 RID: 1993
		private readonly DataRowView _parentRowView;

		// Token: 0x040007CA RID: 1994
		private readonly object[] _filterValues;
	}
}
