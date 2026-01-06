using System;

namespace ES3Types
{
	// Token: 0x020001D8 RID: 472
	public class ES3UserType_ItemPouchArray : ES3ArrayType
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x000A529C File Offset: 0x000A349C
		public ES3UserType_ItemPouchArray()
			: base(typeof(ItemPouch[]), ES3UserType_ItemPouch.Instance)
		{
			ES3UserType_ItemPouchArray.Instance = this;
		}

		// Token: 0x04000E04 RID: 3588
		public static ES3Type Instance;
	}
}
