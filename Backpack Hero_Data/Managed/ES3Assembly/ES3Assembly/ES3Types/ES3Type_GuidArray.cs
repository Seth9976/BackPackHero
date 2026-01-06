using System;

namespace ES3Types
{
	// Token: 0x02000088 RID: 136
	public class ES3Type_GuidArray : ES3ArrayType
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0001020C File Offset: 0x0000E40C
		public ES3Type_GuidArray()
			: base(typeof(Guid[]), ES3Type_Guid.Instance)
		{
			ES3Type_GuidArray.Instance = this;
		}

		// Token: 0x040000BE RID: 190
		public static ES3Type Instance;
	}
}
