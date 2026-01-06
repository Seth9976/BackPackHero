using System;

namespace ES3Types
{
	// Token: 0x020001D2 RID: 466
	public class ES3UserType_Item2Array : ES3ArrayType
	{
		// Token: 0x0600116C RID: 4460 RVA: 0x000A4778 File Offset: 0x000A2978
		public ES3UserType_Item2Array()
			: base(typeof(Item2[]), ES3UserType_Item2.Instance)
		{
			ES3UserType_Item2Array.Instance = this;
		}

		// Token: 0x04000DFE RID: 3582
		public static ES3Type Instance;
	}
}
