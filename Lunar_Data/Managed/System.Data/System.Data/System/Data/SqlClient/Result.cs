using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000145 RID: 325
	internal sealed class Result
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x00050D6E File Offset: 0x0004EF6E
		internal Result(_SqlMetaDataSet metadata)
		{
			this._metadata = metadata;
			this._rowset = new List<Row>();
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00050D88 File Offset: 0x0004EF88
		internal int Count
		{
			get
			{
				return this._rowset.Count;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00050D95 File Offset: 0x0004EF95
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x170002E8 RID: 744
		internal Row this[int index]
		{
			get
			{
				return this._rowset[index];
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00050DAB File Offset: 0x0004EFAB
		internal void AddRow(Row row)
		{
			this._rowset.Add(row);
		}

		// Token: 0x04000AD6 RID: 2774
		private readonly _SqlMetaDataSet _metadata;

		// Token: 0x04000AD7 RID: 2775
		private readonly List<Row> _rowset;
	}
}
