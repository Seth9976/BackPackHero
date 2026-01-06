using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200005E RID: 94
	public class ES3Type_MeshFilterArray : ES3ArrayType
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000B574 File Offset: 0x00009774
		public ES3Type_MeshFilterArray()
			: base(typeof(MeshFilter[]), ES3Type_MeshFilter.Instance)
		{
			ES3Type_MeshFilterArray.Instance = this;
		}

		// Token: 0x04000090 RID: 144
		public static ES3Type Instance;
	}
}
