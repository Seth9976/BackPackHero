using System;

namespace ES3Types
{
	// Token: 0x0200020C RID: 524
	public class ES3UserType_SpecialItemCramArray : ES3ArrayType
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x000A88AC File Offset: 0x000A6AAC
		public ES3UserType_SpecialItemCramArray()
			: base(typeof(SpecialItemCram[]), ES3UserType_SpecialItemCram.Instance)
		{
			ES3UserType_SpecialItemCramArray.Instance = this;
		}

		// Token: 0x04000E38 RID: 3640
		public static ES3Type Instance;
	}
}
