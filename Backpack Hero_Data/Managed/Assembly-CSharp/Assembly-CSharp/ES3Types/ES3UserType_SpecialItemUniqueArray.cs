using System;

namespace ES3Types
{
	// Token: 0x02000218 RID: 536
	public class ES3UserType_SpecialItemUniqueArray : ES3ArrayType
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x000A92C0 File Offset: 0x000A74C0
		public ES3UserType_SpecialItemUniqueArray()
			: base(typeof(SpecialItemUnique[]), ES3UserType_SpecialItemUnique.Instance)
		{
			ES3UserType_SpecialItemUniqueArray.Instance = this;
		}

		// Token: 0x04000E44 RID: 3652
		public static ES3Type Instance;
	}
}
