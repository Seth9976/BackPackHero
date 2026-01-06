using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000067 RID: 103
	public class ES3UserType_RigidbodyArray : ES3ArrayType
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000D444 File Offset: 0x0000B644
		public ES3UserType_RigidbodyArray()
			: base(typeof(Rigidbody[]), ES3Type_Rigidbody.Instance)
		{
			ES3UserType_RigidbodyArray.Instance = this;
		}

		// Token: 0x04000099 RID: 153
		public static ES3Type Instance;
	}
}
