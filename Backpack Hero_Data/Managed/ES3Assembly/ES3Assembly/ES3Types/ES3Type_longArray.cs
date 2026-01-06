using System;

namespace ES3Types
{
	// Token: 0x0200003F RID: 63
	public class ES3Type_longArray : ES3ArrayType
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00009348 File Offset: 0x00007548
		public ES3Type_longArray()
			: base(typeof(long[]), ES3Type_long.Instance)
		{
			ES3Type_longArray.Instance = this;
		}

		// Token: 0x04000077 RID: 119
		public static ES3Type Instance;
	}
}
