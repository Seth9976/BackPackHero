using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000215 RID: 533
	internal sealed class _SqlMetaDataSetCollection
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x0007D8A2 File Offset: 0x0007BAA2
		internal _SqlMetaDataSetCollection()
		{
			this._altMetaDataSetArray = new List<_SqlMetaDataSet>();
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0007D8B8 File Offset: 0x0007BAB8
		internal void SetAltMetaData(_SqlMetaDataSet altMetaDataSet)
		{
			int id = (int)altMetaDataSet.id;
			for (int i = 0; i < this._altMetaDataSetArray.Count; i++)
			{
				if ((int)this._altMetaDataSetArray[i].id == id)
				{
					this._altMetaDataSetArray[i] = altMetaDataSet;
					return;
				}
			}
			this._altMetaDataSetArray.Add(altMetaDataSet);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0007D910 File Offset: 0x0007BB10
		internal _SqlMetaDataSet GetAltMetaData(int id)
		{
			foreach (_SqlMetaDataSet sqlMetaDataSet in this._altMetaDataSetArray)
			{
				if ((int)sqlMetaDataSet.id == id)
				{
					return sqlMetaDataSet;
				}
			}
			return null;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0007D96C File Offset: 0x0007BB6C
		public object Clone()
		{
			_SqlMetaDataSetCollection sqlMetaDataSetCollection = new _SqlMetaDataSetCollection();
			sqlMetaDataSetCollection.metaDataSet = ((this.metaDataSet == null) ? null : ((_SqlMetaDataSet)this.metaDataSet.Clone()));
			foreach (_SqlMetaDataSet sqlMetaDataSet in this._altMetaDataSetArray)
			{
				sqlMetaDataSetCollection._altMetaDataSetArray.Add((_SqlMetaDataSet)sqlMetaDataSet.Clone());
			}
			return sqlMetaDataSetCollection;
		}

		// Token: 0x040011E1 RID: 4577
		private readonly List<_SqlMetaDataSet> _altMetaDataSetArray;

		// Token: 0x040011E2 RID: 4578
		internal _SqlMetaDataSet metaDataSet;
	}
}
