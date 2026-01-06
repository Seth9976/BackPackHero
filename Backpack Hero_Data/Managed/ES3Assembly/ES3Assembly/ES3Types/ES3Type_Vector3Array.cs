using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000BC RID: 188
	public class ES3Type_Vector3Array : ES3ArrayType
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0001E2AE File Offset: 0x0001C4AE
		public ES3Type_Vector3Array()
			: base(typeof(Vector3[]), ES3Type_Vector3.Instance)
		{
			ES3Type_Vector3Array.Instance = this;
		}

		// Token: 0x040000F2 RID: 242
		public static ES3Type Instance;
	}
}
