using System;

namespace ES3Types
{
	// Token: 0x020001F0 RID: 496
	public class ES3UserType_Overworld_ShopKeeperArray : ES3ArrayType
	{
		// Token: 0x060011A8 RID: 4520 RVA: 0x000A6574 File Offset: 0x000A4774
		public ES3UserType_Overworld_ShopKeeperArray()
			: base(typeof(Overworld_ShopKeeper[]), ES3UserType_Overworld_ShopKeeper.Instance)
		{
			ES3UserType_Overworld_ShopKeeperArray.Instance = this;
		}

		// Token: 0x04000E1C RID: 3612
		public static ES3Type Instance;
	}
}
