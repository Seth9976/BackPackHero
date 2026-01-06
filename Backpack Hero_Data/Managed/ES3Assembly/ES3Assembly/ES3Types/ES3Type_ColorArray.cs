using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000074 RID: 116
	public class ES3Type_ColorArray : ES3ArrayType
	{
		// Token: 0x060002EF RID: 751 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public ES3Type_ColorArray()
			: base(typeof(Color[]), ES3Type_Color.Instance)
		{
			ES3Type_ColorArray.Instance = this;
		}

		// Token: 0x040000A7 RID: 167
		public static ES3Type Instance;
	}
}
