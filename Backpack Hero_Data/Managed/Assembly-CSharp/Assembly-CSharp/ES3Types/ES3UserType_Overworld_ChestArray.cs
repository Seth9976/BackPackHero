using System;

namespace ES3Types
{
	// Token: 0x020001E6 RID: 486
	public class ES3UserType_Overworld_ChestArray : ES3ArrayType
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x000A5F60 File Offset: 0x000A4160
		public ES3UserType_Overworld_ChestArray()
			: base(typeof(Overworld_Chest[]), ES3UserType_Overworld_Chest.Instance)
		{
			ES3UserType_Overworld_ChestArray.Instance = this;
		}

		// Token: 0x04000E12 RID: 3602
		public static ES3Type Instance;
	}
}
