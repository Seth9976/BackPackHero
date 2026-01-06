using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200009C RID: 156
	public class ES3Type_PhysicsMaterial2DArray : ES3ArrayType
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00019648 File Offset: 0x00017848
		public ES3Type_PhysicsMaterial2DArray()
			: base(typeof(PhysicsMaterial2D[]), ES3Type_PhysicsMaterial2D.Instance)
		{
			ES3Type_PhysicsMaterial2DArray.Instance = this;
		}

		// Token: 0x040000D2 RID: 210
		public static ES3Type Instance;
	}
}
