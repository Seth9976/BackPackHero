using System;
using UnityEngine.UI;

namespace ES3Types
{
	// Token: 0x02000065 RID: 101
	public class ES3Type_RawImageArray : ES3ArrayType
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public ES3Type_RawImageArray()
			: base(typeof(RawImage[]), ES3Type_RawImage.Instance)
		{
			ES3Type_RawImageArray.Instance = this;
		}

		// Token: 0x04000097 RID: 151
		public static ES3Type Instance;
	}
}
