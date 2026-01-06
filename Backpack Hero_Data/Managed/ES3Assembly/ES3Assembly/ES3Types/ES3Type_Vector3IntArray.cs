using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000BE RID: 190
	public class ES3Type_Vector3IntArray : ES3ArrayType
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0001E379 File Offset: 0x0001C579
		public ES3Type_Vector3IntArray()
			: base(typeof(Vector3Int[]), ES3Type_Vector3Int.Instance)
		{
			ES3Type_Vector3IntArray.Instance = this;
		}

		// Token: 0x040000F4 RID: 244
		public static ES3Type Instance;
	}
}
