using System;

namespace ES3Types
{
	// Token: 0x0200004D RID: 77
	public class ES3Type_ushortArray : ES3ArrayType
	{
		// Token: 0x06000288 RID: 648 RVA: 0x000095D6 File Offset: 0x000077D6
		public ES3Type_ushortArray()
			: base(typeof(ushort[]), ES3Type_ushort.Instance)
		{
			ES3Type_ushortArray.Instance = this;
		}

		// Token: 0x04000085 RID: 133
		public static ES3Type Instance;
	}
}
