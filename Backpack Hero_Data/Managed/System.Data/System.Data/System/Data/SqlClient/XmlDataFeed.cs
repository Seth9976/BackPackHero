using System;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001C8 RID: 456
	internal class XmlDataFeed : DataFeed
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0006B72B File Offset: 0x0006992B
		internal XmlDataFeed(XmlReader source)
		{
			this._source = source;
		}

		// Token: 0x04000EBF RID: 3775
		internal XmlReader _source;
	}
}
