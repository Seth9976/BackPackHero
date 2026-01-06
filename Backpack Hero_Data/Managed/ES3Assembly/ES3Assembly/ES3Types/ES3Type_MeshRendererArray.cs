using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000060 RID: 96
	public class ES3Type_MeshRendererArray : ES3ArrayType
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public ES3Type_MeshRendererArray()
			: base(typeof(MeshRenderer[]), ES3Type_MeshRenderer.Instance)
		{
			ES3Type_MeshRendererArray.Instance = this;
		}

		// Token: 0x04000092 RID: 146
		public static ES3Type Instance;
	}
}
