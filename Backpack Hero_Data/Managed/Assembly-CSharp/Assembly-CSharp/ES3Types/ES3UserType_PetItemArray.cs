using System;

namespace ES3Types
{
	// Token: 0x020001F8 RID: 504
	public class ES3UserType_PetItemArray : ES3ArrayType
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x000A6EB8 File Offset: 0x000A50B8
		public ES3UserType_PetItemArray()
			: base(typeof(PetItem[]), ES3UserType_PetItem.Instance)
		{
			ES3UserType_PetItemArray.Instance = this;
		}

		// Token: 0x04000E24 RID: 3620
		public static ES3Type Instance;
	}
}
