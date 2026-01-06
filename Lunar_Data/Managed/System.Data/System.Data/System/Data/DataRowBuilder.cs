using System;
using Unity;

namespace System.Data
{
	/// <summary>The DataRowBuilder type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000065 RID: 101
	public sealed class DataRowBuilder
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001761C File Offset: 0x0001581C
		internal DataRowBuilder(DataTable table, int record)
		{
			this._table = table;
			this._record = record;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal DataRowBuilder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400051C RID: 1308
		internal readonly DataTable _table;

		// Token: 0x0400051D RID: 1309
		internal int _record;
	}
}
