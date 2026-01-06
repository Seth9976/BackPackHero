using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200009E RID: 158
	public class ES3Type_QuaternionArray : ES3ArrayType
	{
		// Token: 0x0600036D RID: 877 RVA: 0x00019738 File Offset: 0x00017938
		public ES3Type_QuaternionArray()
			: base(typeof(Quaternion[]), ES3Type_Quaternion.Instance)
		{
			ES3Type_QuaternionArray.Instance = this;
		}

		// Token: 0x040000D4 RID: 212
		public static ES3Type Instance;
	}
}
