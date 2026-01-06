using System;

namespace ES3Types
{
	// Token: 0x0200020A RID: 522
	public class ES3UserType_SpecialItemCleansingFlameArray : ES3ArrayType
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x000A8520 File Offset: 0x000A6720
		public ES3UserType_SpecialItemCleansingFlameArray()
			: base(typeof(SpecialItemCleansingFlame[]), ES3UserType_SpecialItemCleansingFlame.Instance)
		{
			ES3UserType_SpecialItemCleansingFlameArray.Instance = this;
		}

		// Token: 0x04000E36 RID: 3638
		public static ES3Type Instance;
	}
}
