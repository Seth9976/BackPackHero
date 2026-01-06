using System;

namespace ES3Types
{
	// Token: 0x02000206 RID: 518
	public class ES3UserType_SlidingRuneArray : ES3ArrayType
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x000A816C File Offset: 0x000A636C
		public ES3UserType_SlidingRuneArray()
			: base(typeof(SlidingRune[]), ES3UserType_SlidingRune.Instance)
		{
			ES3UserType_SlidingRuneArray.Instance = this;
		}

		// Token: 0x04000E32 RID: 3634
		public static ES3Type Instance;
	}
}
