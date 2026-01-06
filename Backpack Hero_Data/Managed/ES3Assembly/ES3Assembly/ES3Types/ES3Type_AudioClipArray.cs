using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x0200006D RID: 109
	public class ES3Type_AudioClipArray : ES3ArrayType
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000E350 File Offset: 0x0000C550
		public ES3Type_AudioClipArray()
			: base(typeof(AudioClip[]), ES3Type_AudioClip.Instance)
		{
			ES3Type_AudioClipArray.Instance = this;
		}

		// Token: 0x040000A0 RID: 160
		public static ES3Type Instance;
	}
}
