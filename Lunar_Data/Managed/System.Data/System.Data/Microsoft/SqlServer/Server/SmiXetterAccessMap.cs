using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A4 RID: 932
	internal class SmiXetterAccessMap
	{
		// Token: 0x06002CF3 RID: 11507 RVA: 0x000C2692 File Offset: 0x000C0892
		internal static bool IsSetterAccessValid(SmiMetaData metaData, SmiXetterTypeCode xetterType)
		{
			return SmiXetterAccessMap.s_isSetterAccessValid[(int)metaData.SqlDbType, (int)xetterType];
		}

		// Token: 0x04001B31 RID: 6961
		private const bool X = true;

		// Token: 0x04001B32 RID: 6962
		private const bool _ = false;

		// Token: 0x04001B33 RID: 6963
		private static bool[,] s_isSetterAccessValid = new bool[,]
		{
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, true, false, true, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true
			}
		};
	}
}
