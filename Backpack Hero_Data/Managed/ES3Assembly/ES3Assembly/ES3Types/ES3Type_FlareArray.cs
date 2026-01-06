using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200007C RID: 124
	public class ES3Type_FlareArray : ES3ArrayType
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000F379 File Offset: 0x0000D579
		public ES3Type_FlareArray()
			: base(typeof(Flare[]), ES3Type_Flare.Instance)
		{
			ES3Type_FlareArray.Instance = this;
		}

		// Token: 0x040000AF RID: 175
		public static ES3Type Instance;
	}
}
