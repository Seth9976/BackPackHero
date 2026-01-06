using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E1 RID: 225
	public struct ShadowData
	{
		// Token: 0x040005A0 RID: 1440
		public bool supportsMainLightShadows;

		// Token: 0x040005A1 RID: 1441
		[Obsolete("Obsolete, this feature was replaced by new 'ScreenSpaceShadows' renderer feature")]
		public bool requiresScreenSpaceShadowResolve;

		// Token: 0x040005A2 RID: 1442
		public int mainLightShadowmapWidth;

		// Token: 0x040005A3 RID: 1443
		public int mainLightShadowmapHeight;

		// Token: 0x040005A4 RID: 1444
		public int mainLightShadowCascadesCount;

		// Token: 0x040005A5 RID: 1445
		public Vector3 mainLightShadowCascadesSplit;

		// Token: 0x040005A6 RID: 1446
		public float mainLightShadowCascadeBorder;

		// Token: 0x040005A7 RID: 1447
		public bool supportsAdditionalLightShadows;

		// Token: 0x040005A8 RID: 1448
		public int additionalLightsShadowmapWidth;

		// Token: 0x040005A9 RID: 1449
		public int additionalLightsShadowmapHeight;

		// Token: 0x040005AA RID: 1450
		public bool supportsSoftShadows;

		// Token: 0x040005AB RID: 1451
		public int shadowmapDepthBufferBits;

		// Token: 0x040005AC RID: 1452
		public List<Vector4> bias;

		// Token: 0x040005AD RID: 1453
		public List<int> resolution;

		// Token: 0x040005AE RID: 1454
		internal bool isKeywordAdditionalLightShadowsEnabled;

		// Token: 0x040005AF RID: 1455
		internal bool isKeywordSoftShadowsEnabled;
	}
}
