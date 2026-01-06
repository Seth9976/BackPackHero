using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000146 RID: 326
	internal sealed class BulkCopySimpleResultSet
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x00050DB9 File Offset: 0x0004EFB9
		internal BulkCopySimpleResultSet()
		{
			this._results = new List<Result>();
		}

		// Token: 0x170002E9 RID: 745
		internal Result this[int idx]
		{
			get
			{
				return this._results[idx];
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00050DDC File Offset: 0x0004EFDC
		internal void SetMetaData(_SqlMetaDataSet metadata)
		{
			this._resultSet = new Result(metadata);
			this._results.Add(this._resultSet);
			this._indexmap = new int[this._resultSet.MetaData.Length];
			for (int i = 0; i < this._indexmap.Length; i++)
			{
				this._indexmap[i] = i;
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00050E3D File Offset: 0x0004F03D
		internal int[] CreateIndexMap()
		{
			return this._indexmap;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00050E48 File Offset: 0x0004F048
		internal object[] CreateRowBuffer()
		{
			Row row = new Row(this._resultSet.MetaData.Length);
			this._resultSet.AddRow(row);
			return row.DataFields;
		}

		// Token: 0x04000AD8 RID: 2776
		private readonly List<Result> _results;

		// Token: 0x04000AD9 RID: 2777
		private Result _resultSet;

		// Token: 0x04000ADA RID: 2778
		private int[] _indexmap;
	}
}
