using System;

namespace ES3Types
{
	// Token: 0x02000043 RID: 67
	public class ES3Type_shortArray : ES3ArrayType
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000940A File Offset: 0x0000760A
		public ES3Type_shortArray()
			: base(typeof(short[]), ES3Type_short.Instance)
		{
			ES3Type_shortArray.Instance = this;
		}

		// Token: 0x0400007B RID: 123
		public static ES3Type Instance;
	}
}
