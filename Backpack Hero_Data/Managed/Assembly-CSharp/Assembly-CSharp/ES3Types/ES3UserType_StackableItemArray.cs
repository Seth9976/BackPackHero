using System;

namespace ES3Types
{
	// Token: 0x02000220 RID: 544
	public class ES3UserType_StackableItemArray : ES3ArrayType
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x000AA180 File Offset: 0x000A8380
		public ES3UserType_StackableItemArray()
			: base(typeof(StackableItem[]), ES3UserType_StackableItem.Instance)
		{
			ES3UserType_StackableItemArray.Instance = this;
		}

		// Token: 0x04000E4C RID: 3660
		public static ES3Type Instance;
	}
}
