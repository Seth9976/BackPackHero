using System;

namespace ES3Types
{
	// Token: 0x0200002D RID: 45
	public class ES3Type_charArray : ES3ArrayType
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00008AD1 File Offset: 0x00006CD1
		public ES3Type_charArray()
			: base(typeof(char[]), ES3Type_char.Instance)
		{
			ES3Type_charArray.Instance = this;
		}

		// Token: 0x04000064 RID: 100
		public static ES3Type Instance;
	}
}
