using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x0200034F RID: 847
	public interface IDbColumnSchemaGenerator
	{
		// Token: 0x06002914 RID: 10516
		ReadOnlyCollection<DbColumn> GetColumnSchema();
	}
}
