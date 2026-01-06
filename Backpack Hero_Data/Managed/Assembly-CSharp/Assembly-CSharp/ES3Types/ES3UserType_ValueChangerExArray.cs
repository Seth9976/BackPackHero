using System;

namespace ES3Types
{
	// Token: 0x02000226 RID: 550
	public class ES3UserType_ValueChangerExArray : ES3ArrayType
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x000ABE1C File Offset: 0x000AA01C
		public ES3UserType_ValueChangerExArray()
			: base(typeof(ValueChangerEx[]), ES3UserType_ValueChanger.Instance)
		{
			ES3UserType_ValueChangerExArray.Instance = this;
		}

		// Token: 0x04000E52 RID: 3666
		public static ES3Type Instance;
	}
}
