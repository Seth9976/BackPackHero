using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200021E RID: 542
	public class ES3UserType_SpriteRendererArray : ES3ArrayType
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x000A9FB8 File Offset: 0x000A81B8
		public ES3UserType_SpriteRendererArray()
			: base(typeof(SpriteRenderer[]), ES3UserType_SpriteRenderer.Instance)
		{
			ES3UserType_SpriteRendererArray.Instance = this;
		}

		// Token: 0x04000E4A RID: 3658
		public static ES3Type Instance;
	}
}
