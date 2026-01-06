using System;

namespace ES3Types
{
	// Token: 0x020001BE RID: 446
	public class ES3UserType_DisableWaterTileArray : ES3ArrayType
	{
		// Token: 0x06001144 RID: 4420 RVA: 0x000A26E8 File Offset: 0x000A08E8
		public ES3UserType_DisableWaterTileArray()
			: base(typeof(DisableWaterTile[]), ES3UserType_DisableWaterTile.Instance)
		{
			ES3UserType_DisableWaterTileArray.Instance = this;
		}

		// Token: 0x04000DEA RID: 3562
		public static ES3Type Instance;
	}
}
