using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x02000092 RID: 146
	public class ES3Type_MaterialArray : ES3ArrayType
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00017B7C File Offset: 0x00015D7C
		public ES3Type_MaterialArray()
			: base(typeof(Material[]), ES3Type_Material.Instance)
		{
			ES3Type_MaterialArray.Instance = this;
		}

		// Token: 0x040000C8 RID: 200
		public static ES3Type Instance;
	}
}
