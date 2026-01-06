using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000B3 RID: 179
	public class ES3Type_Texture2DArray : ES3ArrayType
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0001D4E1 File Offset: 0x0001B6E1
		public ES3Type_Texture2DArray()
			: base(typeof(Texture2D[]), ES3Type_Texture2D.Instance)
		{
			ES3Type_Texture2DArray.Instance = this;
		}

		// Token: 0x040000E9 RID: 233
		public static ES3Type Instance;
	}
}
