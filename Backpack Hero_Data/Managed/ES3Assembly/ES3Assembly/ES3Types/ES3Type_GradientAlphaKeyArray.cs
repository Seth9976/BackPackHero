using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000084 RID: 132
	public class ES3Type_GradientAlphaKeyArray : ES3ArrayType
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public ES3Type_GradientAlphaKeyArray()
			: base(typeof(GradientAlphaKey[]), ES3Type_GradientAlphaKey.Instance)
		{
			ES3Type_GradientAlphaKeyArray.Instance = this;
		}

		// Token: 0x040000BA RID: 186
		public static ES3Type Instance;
	}
}
