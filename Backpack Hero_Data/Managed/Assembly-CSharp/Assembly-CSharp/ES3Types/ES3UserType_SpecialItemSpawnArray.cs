using System;

namespace ES3Types
{
	// Token: 0x02000214 RID: 532
	public class ES3UserType_SpecialItemSpawnArray : ES3ArrayType
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x000A8EE4 File Offset: 0x000A70E4
		public ES3UserType_SpecialItemSpawnArray()
			: base(typeof(SpecialItemSpawn[]), ES3UserType_SpecialItemSpawn.Instance)
		{
			ES3UserType_SpecialItemSpawnArray.Instance = this;
		}

		// Token: 0x04000E40 RID: 3648
		public static ES3Type Instance;
	}
}
