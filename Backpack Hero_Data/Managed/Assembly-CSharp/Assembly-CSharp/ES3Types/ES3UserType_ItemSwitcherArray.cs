using System;

namespace ES3Types
{
	// Token: 0x020001DC RID: 476
	public class ES3UserType_ItemSwitcherArray : ES3ArrayType
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x000A5534 File Offset: 0x000A3734
		public ES3UserType_ItemSwitcherArray()
			: base(typeof(ItemSwitcher[]), ES3UserType_ItemSwitcher.Instance)
		{
			ES3UserType_ItemSwitcherArray.Instance = this;
		}

		// Token: 0x04000E08 RID: 3592
		public static ES3Type Instance;
	}
}
