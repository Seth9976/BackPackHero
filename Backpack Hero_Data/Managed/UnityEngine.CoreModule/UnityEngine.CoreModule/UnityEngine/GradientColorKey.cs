using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BF RID: 447
	[UsedByNativeCode]
	public struct GradientColorKey
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0001C409 File Offset: 0x0001A609
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x0400073D RID: 1853
		public Color color;

		// Token: 0x0400073E RID: 1854
		public float time;
	}
}
