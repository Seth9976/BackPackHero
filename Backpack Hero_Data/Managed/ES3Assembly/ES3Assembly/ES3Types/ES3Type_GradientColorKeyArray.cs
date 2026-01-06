using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000086 RID: 134
	public class ES3Type_GradientColorKeyArray : ES3ArrayType
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0001018C File Offset: 0x0000E38C
		public ES3Type_GradientColorKeyArray()
			: base(typeof(GradientColorKey[]), ES3Type_GradientColorKey.Instance)
		{
			ES3Type_GradientColorKeyArray.Instance = this;
		}

		// Token: 0x040000BC RID: 188
		public static ES3Type Instance;
	}
}
