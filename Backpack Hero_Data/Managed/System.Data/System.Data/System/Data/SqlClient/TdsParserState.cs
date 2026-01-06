using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020C RID: 524
	internal enum TdsParserState
	{
		// Token: 0x04001196 RID: 4502
		Closed,
		// Token: 0x04001197 RID: 4503
		OpenNotLoggedIn,
		// Token: 0x04001198 RID: 4504
		OpenLoggedIn,
		// Token: 0x04001199 RID: 4505
		Broken
	}
}
