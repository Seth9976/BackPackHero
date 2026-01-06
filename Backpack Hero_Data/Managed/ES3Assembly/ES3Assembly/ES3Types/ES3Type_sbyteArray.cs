using System;

namespace ES3Types
{
	// Token: 0x02000041 RID: 65
	public class ES3Type_sbyteArray : ES3ArrayType
	{
		// Token: 0x06000270 RID: 624 RVA: 0x000093A9 File Offset: 0x000075A9
		public ES3Type_sbyteArray()
			: base(typeof(sbyte[]), ES3Type_sbyte.Instance)
		{
			ES3Type_sbyteArray.Instance = this;
		}

		// Token: 0x04000079 RID: 121
		public static ES3Type Instance;
	}
}
