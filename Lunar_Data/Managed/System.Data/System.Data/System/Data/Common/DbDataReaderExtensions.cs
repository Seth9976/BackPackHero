using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x0200033F RID: 831
	public static class DbDataReaderExtensions
	{
		// Token: 0x06002860 RID: 10336 RVA: 0x000B2094 File Offset: 0x000B0294
		public static ReadOnlyCollection<DbColumn> GetColumnSchema(this DbDataReader reader)
		{
			if (reader.CanGetColumnSchema())
			{
				return ((IDbColumnSchemaGenerator)reader).GetColumnSchema();
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000B20AF File Offset: 0x000B02AF
		public static bool CanGetColumnSchema(this DbDataReader reader)
		{
			return reader is IDbColumnSchemaGenerator;
		}
	}
}
