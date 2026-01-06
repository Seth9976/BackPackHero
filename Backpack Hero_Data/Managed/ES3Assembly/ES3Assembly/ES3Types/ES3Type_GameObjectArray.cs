using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000081 RID: 129
	public class ES3Type_GameObjectArray : ES3ArrayType
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000FF5B File Offset: 0x0000E15B
		public ES3Type_GameObjectArray()
			: base(typeof(GameObject[]), ES3Type_GameObject.Instance)
		{
			ES3Type_GameObjectArray.Instance = this;
		}

		// Token: 0x040000B7 RID: 183
		public static ES3Type Instance;
	}
}
