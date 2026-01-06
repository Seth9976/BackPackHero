using System;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000214 RID: 532
	internal sealed class _SqlMetaDataSet
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x0007D7A8 File Offset: 0x0007B9A8
		internal _SqlMetaDataSet(int count)
		{
			this._metaDataArray = new _SqlMetaData[count];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = new _SqlMetaData(i);
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0007D7E8 File Offset: 0x0007B9E8
		private _SqlMetaDataSet(_SqlMetaDataSet original)
		{
			this.id = original.id;
			this.indexMap = original.indexMap;
			this.visibleColumns = original.visibleColumns;
			this.dbColumnSchema = original.dbColumnSchema;
			if (original._metaDataArray == null)
			{
				this._metaDataArray = null;
				return;
			}
			this._metaDataArray = new _SqlMetaData[original._metaDataArray.Length];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = (_SqlMetaData)original._metaDataArray[i].Clone();
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0007D87B File Offset: 0x0007BA7B
		internal int Length
		{
			get
			{
				return this._metaDataArray.Length;
			}
		}

		// Token: 0x1700049C RID: 1180
		internal _SqlMetaData this[int index]
		{
			get
			{
				return this._metaDataArray[index];
			}
			set
			{
				this._metaDataArray[index] = value;
			}
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0007D89A File Offset: 0x0007BA9A
		public object Clone()
		{
			return new _SqlMetaDataSet(this);
		}

		// Token: 0x040011DB RID: 4571
		internal ushort id;

		// Token: 0x040011DC RID: 4572
		internal int[] indexMap;

		// Token: 0x040011DD RID: 4573
		internal int visibleColumns;

		// Token: 0x040011DE RID: 4574
		internal DataTable schemaTable;

		// Token: 0x040011DF RID: 4575
		private readonly _SqlMetaData[] _metaDataArray;

		// Token: 0x040011E0 RID: 4576
		internal ReadOnlyCollection<DbColumn> dbColumnSchema;
	}
}
