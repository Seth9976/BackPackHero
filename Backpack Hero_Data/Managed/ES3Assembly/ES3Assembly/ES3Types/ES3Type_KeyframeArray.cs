using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200008B RID: 139
	public class ES3Type_KeyframeArray : ES3ArrayType
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00010470 File Offset: 0x0000E670
		public ES3Type_KeyframeArray()
			: base(typeof(Keyframe[]), ES3Type_Keyframe.Instance)
		{
			ES3Type_KeyframeArray.Instance = this;
		}

		// Token: 0x040000C1 RID: 193
		public static ES3Type Instance;
	}
}
