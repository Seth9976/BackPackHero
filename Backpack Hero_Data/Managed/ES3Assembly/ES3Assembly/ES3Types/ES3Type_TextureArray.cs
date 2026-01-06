using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000B1 RID: 177
	public class ES3Type_TextureArray : ES3ArrayType
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0001D188 File Offset: 0x0001B388
		public ES3Type_TextureArray()
			: base(typeof(Texture[]), ES3Type_Texture.Instance)
		{
			ES3Type_TextureArray.Instance = this;
		}

		// Token: 0x040000E7 RID: 231
		public static ES3Type Instance;
	}
}
