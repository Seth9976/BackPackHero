using System;

namespace ES3Types
{
	// Token: 0x02000045 RID: 69
	public class ES3Type_StringArray : ES3ArrayType
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000945C File Offset: 0x0000765C
		public ES3Type_StringArray()
			: base(typeof(string[]), ES3Type_string.Instance)
		{
			ES3Type_StringArray.Instance = this;
		}

		// Token: 0x0400007D RID: 125
		public static ES3Type Instance;
	}
}
