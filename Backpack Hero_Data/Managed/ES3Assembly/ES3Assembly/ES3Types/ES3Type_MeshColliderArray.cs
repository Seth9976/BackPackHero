using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200005C RID: 92
	public class ES3Type_MeshColliderArray : ES3ArrayType
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000B494 File Offset: 0x00009694
		public ES3Type_MeshColliderArray()
			: base(typeof(MeshCollider[]), ES3Type_MeshCollider.Instance)
		{
			ES3Type_MeshColliderArray.Instance = this;
		}

		// Token: 0x0400008E RID: 142
		public static ES3Type Instance;
	}
}
