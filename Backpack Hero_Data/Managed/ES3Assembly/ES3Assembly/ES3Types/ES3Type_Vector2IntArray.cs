using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000BA RID: 186
	public class ES3Type_Vector2IntArray : ES3ArrayType
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
		public ES3Type_Vector2IntArray()
			: base(typeof(Vector2Int[]), ES3Type_Vector2Int.Instance)
		{
			ES3Type_Vector2IntArray.Instance = this;
		}

		// Token: 0x040000F0 RID: 240
		public static ES3Type Instance;
	}
}
