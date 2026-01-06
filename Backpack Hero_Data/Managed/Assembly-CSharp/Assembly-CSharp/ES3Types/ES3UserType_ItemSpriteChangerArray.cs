using System;

namespace ES3Types
{
	// Token: 0x020001DA RID: 474
	public class ES3UserType_ItemSpriteChangerArray : ES3ArrayType
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x000A53EC File Offset: 0x000A35EC
		public ES3UserType_ItemSpriteChangerArray()
			: base(typeof(ItemSpriteChanger[]), ES3UserType_ItemSpriteChanger.Instance)
		{
			ES3UserType_ItemSpriteChangerArray.Instance = this;
		}

		// Token: 0x04000E06 RID: 3590
		public static ES3Type Instance;
	}
}
