using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AA RID: 426
	[UsedByNativeCode]
	[Serializable]
	public struct CustomRenderTextureUpdateZone
	{
		// Token: 0x040005BF RID: 1471
		public Vector3 updateZoneCenter;

		// Token: 0x040005C0 RID: 1472
		public Vector3 updateZoneSize;

		// Token: 0x040005C1 RID: 1473
		public float rotation;

		// Token: 0x040005C2 RID: 1474
		public int passIndex;

		// Token: 0x040005C3 RID: 1475
		public bool needSwap;
	}
}
