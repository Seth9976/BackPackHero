using System;

namespace ES3Types
{
	// Token: 0x02000210 RID: 528
	public class ES3UserType_specialItemPochetteArray : ES3ArrayType
	{
		// Token: 0x060011E8 RID: 4584 RVA: 0x000A8BE0 File Offset: 0x000A6DE0
		public ES3UserType_specialItemPochetteArray()
			: base(typeof(specialItemPochette[]), ES3UserType_specialItemPochette.Instance)
		{
			ES3UserType_specialItemPochetteArray.Instance = this;
		}

		// Token: 0x04000E3C RID: 3644
		public static ES3Type Instance;
	}
}
