using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001C6 RID: 454
	internal class StreamDataFeed : DataFeed
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x0006B70D File Offset: 0x0006990D
		internal StreamDataFeed(Stream source)
		{
			this._source = source;
		}

		// Token: 0x04000EBD RID: 3773
		internal Stream _source;
	}
}
