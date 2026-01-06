using System;

namespace ES3Types
{
	// Token: 0x020001B4 RID: 436
	public class ES3UserType_CarvingArray : ES3ArrayType
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x000A2030 File Offset: 0x000A0230
		public ES3UserType_CarvingArray()
			: base(typeof(Carving[]), ES3UserType_Carving.Instance)
		{
			ES3UserType_CarvingArray.Instance = this;
		}

		// Token: 0x04000DE0 RID: 3552
		public static ES3Type Instance;
	}
}
