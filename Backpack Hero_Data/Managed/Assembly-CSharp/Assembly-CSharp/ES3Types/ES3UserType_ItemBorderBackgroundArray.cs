using System;

namespace ES3Types
{
	// Token: 0x020001D4 RID: 468
	public class ES3UserType_ItemBorderBackgroundArray : ES3ArrayType
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x000A4864 File Offset: 0x000A2A64
		public ES3UserType_ItemBorderBackgroundArray()
			: base(typeof(ItemBorderBackground[]), ES3UserType_ItemBorderBackground.Instance)
		{
			ES3UserType_ItemBorderBackgroundArray.Instance = this;
		}

		// Token: 0x04000E00 RID: 3584
		public static ES3Type Instance;
	}
}
