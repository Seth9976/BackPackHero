using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000A2 RID: 162
	public class ES3Type_RenderTextureArray : ES3ArrayType
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0001A62E File Offset: 0x0001882E
		public ES3Type_RenderTextureArray()
			: base(typeof(RenderTexture[]), ES3Type_RenderTexture.Instance)
		{
			ES3Type_RenderTextureArray.Instance = this;
		}

		// Token: 0x040000D8 RID: 216
		public static ES3Type Instance;
	}
}
