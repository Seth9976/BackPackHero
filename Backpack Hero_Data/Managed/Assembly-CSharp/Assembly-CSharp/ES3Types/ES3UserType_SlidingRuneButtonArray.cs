using System;

namespace ES3Types
{
	// Token: 0x02000208 RID: 520
	public class ES3UserType_SlidingRuneButtonArray : ES3ArrayType
	{
		// Token: 0x060011D8 RID: 4568 RVA: 0x000A824C File Offset: 0x000A644C
		public ES3UserType_SlidingRuneButtonArray()
			: base(typeof(SlidingRuneButton[]), ES3UserType_SlidingRuneButton.Instance)
		{
			ES3UserType_SlidingRuneButtonArray.Instance = this;
		}

		// Token: 0x04000E34 RID: 3636
		public static ES3Type Instance;
	}
}
