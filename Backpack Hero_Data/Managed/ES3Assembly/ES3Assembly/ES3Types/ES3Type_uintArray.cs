using System;

namespace ES3Types
{
	// Token: 0x02000047 RID: 71
	public class ES3Type_uintArray : ES3ArrayType
	{
		// Token: 0x0600027C RID: 636 RVA: 0x000094BD File Offset: 0x000076BD
		public ES3Type_uintArray()
			: base(typeof(uint[]), ES3Type_uint.Instance)
		{
			ES3Type_uintArray.Instance = this;
		}

		// Token: 0x0400007F RID: 127
		public static ES3Type Instance;
	}
}
