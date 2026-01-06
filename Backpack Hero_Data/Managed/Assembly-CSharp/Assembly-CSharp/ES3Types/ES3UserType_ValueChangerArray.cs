using System;

namespace ES3Types
{
	// Token: 0x02000224 RID: 548
	public class ES3UserType_ValueChangerArray : ES3ArrayType
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x000ABA5C File Offset: 0x000A9C5C
		public ES3UserType_ValueChangerArray()
			: base(typeof(ValueChanger[]), ES3UserType_ValueChanger.Instance)
		{
			ES3UserType_ValueChangerArray.Instance = this;
		}

		// Token: 0x04000E50 RID: 3664
		public static ES3Type Instance;
	}
}
