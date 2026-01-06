using System;

namespace ES3Types
{
	// Token: 0x02000029 RID: 41
	public class ES3Type_boolArray : ES3ArrayType
	{
		// Token: 0x0600023B RID: 571 RVA: 0x000089ED File Offset: 0x00006BED
		public ES3Type_boolArray()
			: base(typeof(bool[]), ES3Type_bool.Instance)
		{
			ES3Type_boolArray.Instance = this;
		}

		// Token: 0x04000060 RID: 96
		public static ES3Type Instance;
	}
}
