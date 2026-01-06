using System;

namespace ES3Types
{
	// Token: 0x02000031 RID: 49
	public class ES3Type_decimalArray : ES3ArrayType
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008BB7 File Offset: 0x00006DB7
		public ES3Type_decimalArray()
			: base(typeof(decimal[]), ES3Type_decimal.Instance)
		{
			ES3Type_decimalArray.Instance = this;
		}

		// Token: 0x04000068 RID: 104
		public static ES3Type Instance;
	}
}
