using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000B8 RID: 184
	public class ES3Type_Vector2Array : ES3ArrayType
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0001E140 File Offset: 0x0001C340
		public ES3Type_Vector2Array()
			: base(typeof(Vector2[]), ES3Type_Vector2.Instance)
		{
			ES3Type_Vector2Array.Instance = this;
		}

		// Token: 0x040000EE RID: 238
		public static ES3Type Instance;
	}
}
