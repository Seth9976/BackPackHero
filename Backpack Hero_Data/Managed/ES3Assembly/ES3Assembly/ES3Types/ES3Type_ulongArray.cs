using System;

namespace ES3Types
{
	// Token: 0x0200004B RID: 75
	public class ES3Type_ulongArray : ES3ArrayType
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00009575 File Offset: 0x00007775
		public ES3Type_ulongArray()
			: base(typeof(ulong[]), ES3Type_ulong.Instance)
		{
			ES3Type_ulongArray.Instance = this;
		}

		// Token: 0x04000083 RID: 131
		public static ES3Type Instance;
	}
}
