using System;

namespace ES3Types
{
	// Token: 0x020001CE RID: 462
	public class ES3UserType_FlowerWobbleArray : ES3ArrayType
	{
		// Token: 0x06001164 RID: 4452 RVA: 0x000A3A0C File Offset: 0x000A1C0C
		public ES3UserType_FlowerWobbleArray()
			: base(typeof(FlowerWobble[]), ES3UserType_FlowerWobble.Instance)
		{
			ES3UserType_FlowerWobbleArray.Instance = this;
		}

		// Token: 0x04000DFA RID: 3578
		public static ES3Type Instance;
	}
}
