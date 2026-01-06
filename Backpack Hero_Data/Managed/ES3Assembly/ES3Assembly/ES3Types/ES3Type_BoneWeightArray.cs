using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200006F RID: 111
	public class ES3Type_BoneWeightArray : ES3ArrayType
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000E527 File Offset: 0x0000C727
		public ES3Type_BoneWeightArray()
			: base(typeof(BoneWeight[]), ES3Type_BoneWeight.Instance)
		{
			ES3Type_BoneWeightArray.Instance = this;
		}

		// Token: 0x040000A2 RID: 162
		public static ES3Type Instance;
	}
}
