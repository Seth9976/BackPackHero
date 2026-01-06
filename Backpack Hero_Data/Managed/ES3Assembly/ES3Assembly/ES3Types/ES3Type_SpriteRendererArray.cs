using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000AE RID: 174
	public class ES3Type_SpriteRendererArray : ES3ArrayType
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		public ES3Type_SpriteRendererArray()
			: base(typeof(SpriteRenderer[]), ES3Type_SpriteRenderer.Instance)
		{
			ES3Type_SpriteRendererArray.Instance = this;
		}

		// Token: 0x040000E4 RID: 228
		public static ES3Type Instance;
	}
}
