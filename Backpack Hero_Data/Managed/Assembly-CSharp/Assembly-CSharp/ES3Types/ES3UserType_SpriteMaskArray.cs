using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200021C RID: 540
	public class ES3UserType_SpriteMaskArray : ES3ArrayType
	{
		// Token: 0x06001200 RID: 4608 RVA: 0x000A9724 File Offset: 0x000A7924
		public ES3UserType_SpriteMaskArray()
			: base(typeof(SpriteMask[]), ES3UserType_SpriteMask.Instance)
		{
			ES3UserType_SpriteMaskArray.Instance = this;
		}

		// Token: 0x04000E48 RID: 3656
		public static ES3Type Instance;
	}
}
