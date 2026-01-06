using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200009A RID: 154
	public class ES3Type_PhysicMaterialArray : ES3ArrayType
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00019504 File Offset: 0x00017704
		public ES3Type_PhysicMaterialArray()
			: base(typeof(PhysicMaterial[]), ES3Type_PhysicMaterial.Instance)
		{
			ES3Type_PhysicMaterialArray.Instance = this;
		}

		// Token: 0x040000D0 RID: 208
		public static ES3Type Instance;
	}
}
