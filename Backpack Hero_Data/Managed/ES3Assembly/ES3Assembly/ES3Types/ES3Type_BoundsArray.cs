using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000071 RID: 113
	public class ES3Type_BoundsArray : ES3ArrayType
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000E5CA File Offset: 0x0000C7CA
		public ES3Type_BoundsArray()
			: base(typeof(Bounds[]), ES3Type_Bounds.Instance)
		{
			ES3Type_BoundsArray.Instance = this;
		}

		// Token: 0x040000A4 RID: 164
		public static ES3Type Instance;
	}
}
