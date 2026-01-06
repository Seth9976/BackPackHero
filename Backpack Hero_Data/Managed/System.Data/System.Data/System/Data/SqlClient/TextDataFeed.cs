using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001C7 RID: 455
	internal class TextDataFeed : DataFeed
	{
		// Token: 0x060015C6 RID: 5574 RVA: 0x0006B71C File Offset: 0x0006991C
		internal TextDataFeed(TextReader source)
		{
			this._source = source;
		}

		// Token: 0x04000EBE RID: 3774
		internal TextReader _source;
	}
}
