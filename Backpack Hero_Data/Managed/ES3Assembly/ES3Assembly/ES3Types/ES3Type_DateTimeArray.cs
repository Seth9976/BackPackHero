using System;

namespace ES3Types
{
	// Token: 0x0200002F RID: 47
	public class ES3Type_DateTimeArray : ES3ArrayType
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00008B56 File Offset: 0x00006D56
		public ES3Type_DateTimeArray()
			: base(typeof(DateTime[]), ES3Type_DateTime.Instance)
		{
			ES3Type_DateTimeArray.Instance = this;
		}

		// Token: 0x04000066 RID: 102
		public static ES3Type Instance;
	}
}
