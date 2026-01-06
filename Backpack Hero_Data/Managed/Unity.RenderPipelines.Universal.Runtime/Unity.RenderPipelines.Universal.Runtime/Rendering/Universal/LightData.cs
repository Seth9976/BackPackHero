using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DF RID: 223
	public struct LightData
	{
		// Token: 0x0400056F RID: 1391
		public int mainLightIndex;

		// Token: 0x04000570 RID: 1392
		public int additionalLightsCount;

		// Token: 0x04000571 RID: 1393
		public int maxPerObjectAdditionalLightsCount;

		// Token: 0x04000572 RID: 1394
		public NativeArray<VisibleLight> visibleLights;

		// Token: 0x04000573 RID: 1395
		internal NativeArray<int> originalIndices;

		// Token: 0x04000574 RID: 1396
		public bool shadeAdditionalLightsPerVertex;

		// Token: 0x04000575 RID: 1397
		public bool supportsMixedLighting;

		// Token: 0x04000576 RID: 1398
		public bool reflectionProbeBoxProjection;

		// Token: 0x04000577 RID: 1399
		public bool reflectionProbeBlending;

		// Token: 0x04000578 RID: 1400
		public bool supportsLightLayers;

		// Token: 0x04000579 RID: 1401
		public bool supportsAdditionalLights;
	}
}
