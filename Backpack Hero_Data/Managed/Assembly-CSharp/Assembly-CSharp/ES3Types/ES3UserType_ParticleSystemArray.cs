using System;
using UnityEngine;

namespace ES3Types
{
	// Token: 0x020001F6 RID: 502
	public class ES3UserType_ParticleSystemArray : ES3ArrayType
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x000A6E10 File Offset: 0x000A5010
		public ES3UserType_ParticleSystemArray()
			: base(typeof(ParticleSystem[]), ES3UserType_ParticleSystem.Instance)
		{
			ES3UserType_ParticleSystemArray.Instance = this;
		}

		// Token: 0x04000E22 RID: 3618
		public static ES3Type Instance;
	}
}
