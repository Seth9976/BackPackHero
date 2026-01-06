using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020000A6 RID: 166
	public class ES3Type_ShaderArray : ES3ArrayType
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0001AE30 File Offset: 0x00019030
		public ES3Type_ShaderArray()
			: base(typeof(Shader[]), ES3Type_Shader.Instance)
		{
			ES3Type_ShaderArray.Instance = this;
		}

		// Token: 0x040000DC RID: 220
		public static ES3Type Instance;
	}
}
