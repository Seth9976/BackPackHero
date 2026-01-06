using System;

namespace ES3Types
{
	// Token: 0x020001BA RID: 442
	public class ES3UserType_CurseArray : ES3ArrayType
	{
		// Token: 0x0600113C RID: 4412 RVA: 0x000A24E8 File Offset: 0x000A06E8
		public ES3UserType_CurseArray()
			: base(typeof(Curse[]), ES3UserType_Curse.Instance)
		{
			ES3UserType_CurseArray.Instance = this;
		}

		// Token: 0x04000DE6 RID: 3558
		public static ES3Type Instance;
	}
}
