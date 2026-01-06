using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200007E RID: 126
	public class ES3Type_FontArray : ES3ArrayType
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000F45B File Offset: 0x0000D65B
		public ES3Type_FontArray()
			: base(typeof(Font[]), ES3Type_Font.Instance)
		{
			ES3Type_FontArray.Instance = this;
		}

		// Token: 0x040000B1 RID: 177
		public static ES3Type Instance;
	}
}
