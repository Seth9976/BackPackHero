using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000C0 RID: 192
	public class ES3Type_Vector4Array : ES3ArrayType
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0001E4C1 File Offset: 0x0001C6C1
		public ES3Type_Vector4Array()
			: base(typeof(Vector4[]), ES3Type_Vector4.Instance)
		{
			ES3Type_Vector4Array.Instance = this;
		}

		// Token: 0x040000F6 RID: 246
		public static ES3Type Instance;
	}
}
