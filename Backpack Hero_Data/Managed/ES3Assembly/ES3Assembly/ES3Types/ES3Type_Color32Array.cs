using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000076 RID: 118
	public class ES3Type_Color32Array : ES3ArrayType
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000EDE1 File Offset: 0x0000CFE1
		public ES3Type_Color32Array()
			: base(typeof(Color32[]), ES3Type_Color32.Instance)
		{
			ES3Type_Color32Array.Instance = this;
		}

		// Token: 0x040000A9 RID: 169
		public static ES3Type Instance;
	}
}
