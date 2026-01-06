using System;
using UnityEngine.UI;

namespace ES3Types
{
	// Token: 0x0200005A RID: 90
	public class ES3Type_ImageArray : ES3ArrayType
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000B290 File Offset: 0x00009490
		public ES3Type_ImageArray()
			: base(typeof(Image[]), ES3Type_Image.Instance)
		{
			ES3Type_ImageArray.Instance = this;
		}

		// Token: 0x0400008C RID: 140
		public static ES3Type Instance;
	}
}
