using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001B6 RID: 438
	internal class SessionStateRecord
	{
		// Token: 0x04000E3F RID: 3647
		internal bool _recoverable;

		// Token: 0x04000E40 RID: 3648
		internal uint _version;

		// Token: 0x04000E41 RID: 3649
		internal int _dataLength;

		// Token: 0x04000E42 RID: 3650
		internal byte[] _data;
	}
}
