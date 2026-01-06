using System;

namespace ES3Types
{
	// Token: 0x0200020E RID: 526
	public class ES3UserType_SpecialItemDoublePoisonArray : ES3ArrayType
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x000A8A0C File Offset: 0x000A6C0C
		public ES3UserType_SpecialItemDoublePoisonArray()
			: base(typeof(SpecialItemDoublePoison[]), ES3UserType_SpecialItemDoublePoison.Instance)
		{
			ES3UserType_SpecialItemDoublePoisonArray.Instance = this;
		}

		// Token: 0x04000E3A RID: 3642
		public static ES3Type Instance;
	}
}
