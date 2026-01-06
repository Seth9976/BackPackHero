using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000143 RID: 323
	internal sealed class _ColumnMapping
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00050D32 File Offset: 0x0004EF32
		internal _ColumnMapping(int columnId, _SqlMetaData metadata)
		{
			this._sourceColumnOrdinal = columnId;
			this._metadata = metadata;
		}

		// Token: 0x04000AD3 RID: 2771
		internal int _sourceColumnOrdinal;

		// Token: 0x04000AD4 RID: 2772
		internal _SqlMetaData _metadata;
	}
}
