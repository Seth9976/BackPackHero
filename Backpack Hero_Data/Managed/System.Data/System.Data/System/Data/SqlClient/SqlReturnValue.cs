using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000218 RID: 536
	internal sealed class SqlReturnValue : SqlMetaDataPriv
	{
		// Token: 0x060018E3 RID: 6371 RVA: 0x0007DB5E File Offset: 0x0007BD5E
		internal SqlReturnValue()
		{
			this.value = new SqlBuffer();
		}

		// Token: 0x04001207 RID: 4615
		internal string parameter;

		// Token: 0x04001208 RID: 4616
		internal readonly SqlBuffer value;
	}
}
