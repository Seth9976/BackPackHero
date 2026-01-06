using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020001B6 RID: 438
	public class ES3UserType_CircleCollider2DArray : ES3ArrayType
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x000A2210 File Offset: 0x000A0410
		public ES3UserType_CircleCollider2DArray()
			: base(typeof(CircleCollider2D[]), ES3UserType_CircleCollider2D.Instance)
		{
			ES3UserType_CircleCollider2DArray.Instance = this;
		}

		// Token: 0x04000DE2 RID: 3554
		public static ES3Type Instance;
	}
}
