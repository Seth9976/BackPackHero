using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000AB RID: 171
	public class ES3Type_SkinnedMeshRendererArray : ES3ArrayType
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0001C55C File Offset: 0x0001A75C
		public ES3Type_SkinnedMeshRendererArray()
			: base(typeof(SkinnedMeshRenderer[]), ES3Type_SkinnedMeshRenderer.Instance)
		{
			ES3Type_SkinnedMeshRendererArray.Instance = this;
		}

		// Token: 0x040000E1 RID: 225
		public static ES3Type Instance;
	}
}
