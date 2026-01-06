using System;

namespace ES3Types
{
	// Token: 0x0200021A RID: 538
	public class ES3UserType_SpecificConditionToUseArray : ES3ArrayType
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x000A9640 File Offset: 0x000A7840
		public ES3UserType_SpecificConditionToUseArray()
			: base(typeof(SpecificConditionToUse[]), ES3UserType_SpecificConditionToUse.Instance)
		{
			ES3UserType_SpecificConditionToUseArray.Instance = this;
		}

		// Token: 0x04000E46 RID: 3654
		public static ES3Type Instance;
	}
}
