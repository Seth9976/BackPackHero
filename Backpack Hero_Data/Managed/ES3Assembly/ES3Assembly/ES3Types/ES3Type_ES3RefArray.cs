using System;

namespace ES3Types
{
	// Token: 0x02000036 RID: 54
	public class ES3Type_ES3RefArray : ES3ArrayType
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00009180 File Offset: 0x00007380
		public ES3Type_ES3RefArray()
			: base(typeof(ES3Ref[]), ES3Type_ES3Ref.Instance)
		{
			ES3Type_ES3RefArray.Instance = this;
		}

		// Token: 0x0400006E RID: 110
		public static ES3Type Instance = new ES3Type_ES3RefArray();
	}
}
