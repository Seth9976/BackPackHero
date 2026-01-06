using System;

namespace ES3Types
{
	// Token: 0x02000216 RID: 534
	public class ES3UserType_SpecialItemToteArray : ES3ArrayType
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x000A90BC File Offset: 0x000A72BC
		public ES3UserType_SpecialItemToteArray()
			: base(typeof(SpecialItemTote[]), ES3UserType_SpecialItemTote.Instance)
		{
			ES3UserType_SpecialItemToteArray.Instance = this;
		}

		// Token: 0x04000E42 RID: 3650
		public static ES3Type Instance;
	}
}
