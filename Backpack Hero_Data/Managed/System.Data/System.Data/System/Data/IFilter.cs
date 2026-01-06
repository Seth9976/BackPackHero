using System;

namespace System.Data
{
	// Token: 0x020000A4 RID: 164
	internal interface IFilter
	{
		// Token: 0x06000AA0 RID: 2720
		bool Invoke(DataRow row, DataRowVersion version);
	}
}
