using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000063 RID: 99
	public class ES3Type_PolygonCollider2DArray : ES3ArrayType
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000C814 File Offset: 0x0000AA14
		public ES3Type_PolygonCollider2DArray()
			: base(typeof(PolygonCollider2D[]), ES3Type_PolygonCollider2D.Instance)
		{
			ES3Type_PolygonCollider2DArray.Instance = this;
		}

		// Token: 0x04000095 RID: 149
		public static ES3Type Instance;
	}
}
