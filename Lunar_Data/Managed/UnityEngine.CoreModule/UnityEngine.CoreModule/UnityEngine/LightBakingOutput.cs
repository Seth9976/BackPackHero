using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000154 RID: 340
	[NativeHeader("Runtime/Camera/SharedLightData.h")]
	public struct LightBakingOutput
	{
		// Token: 0x0400042A RID: 1066
		public int probeOcclusionLightIndex;

		// Token: 0x0400042B RID: 1067
		public int occlusionMaskChannel;

		// Token: 0x0400042C RID: 1068
		[NativeName("lightmapBakeMode.lightmapBakeType")]
		public LightmapBakeType lightmapBakeType;

		// Token: 0x0400042D RID: 1069
		[NativeName("lightmapBakeMode.mixedLightingMode")]
		public MixedLightingMode mixedLightingMode;

		// Token: 0x0400042E RID: 1070
		public bool isBaked;
	}
}
