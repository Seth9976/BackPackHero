using System;
using TMPro;

namespace ES3Types
{
	// Token: 0x02000222 RID: 546
	public class ES3UserType_TextMeshProArray : ES3ArrayType
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x000AB5F0 File Offset: 0x000A97F0
		public ES3UserType_TextMeshProArray()
			: base(typeof(TextMeshPro[]), ES3UserType_TextMeshPro.Instance)
		{
			ES3UserType_TextMeshProArray.Instance = this;
		}

		// Token: 0x04000E4E RID: 3662
		public static ES3Type Instance;
	}
}
