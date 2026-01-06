using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E4 RID: 228
	public struct PunctualLightData
	{
		// Token: 0x040005B8 RID: 1464
		public Vector3 wsPos;

		// Token: 0x040005B9 RID: 1465
		public float radius;

		// Token: 0x040005BA RID: 1466
		public Vector4 color;

		// Token: 0x040005BB RID: 1467
		public Vector4 attenuation;

		// Token: 0x040005BC RID: 1468
		public Vector3 spotDirection;

		// Token: 0x040005BD RID: 1469
		public int flags;

		// Token: 0x040005BE RID: 1470
		public Vector4 occlusionProbeInfo;

		// Token: 0x040005BF RID: 1471
		public uint layerMask;
	}
}
