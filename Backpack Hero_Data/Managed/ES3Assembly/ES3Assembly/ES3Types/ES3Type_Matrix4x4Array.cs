using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000094 RID: 148
	public class ES3Type_Matrix4x4Array : ES3ArrayType
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00017CA3 File Offset: 0x00015EA3
		public ES3Type_Matrix4x4Array()
			: base(typeof(Matrix4x4[]), ES3Type_Matrix4x4.Instance)
		{
			ES3Type_Matrix4x4Array.Instance = this;
		}

		// Token: 0x040000CA RID: 202
		public static ES3Type Instance;
	}
}
