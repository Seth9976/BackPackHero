using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000D RID: 13
	public static class ShaderInput
	{
		// Token: 0x0200000E RID: 14
		[Obsolete("ShaderInput.ShadowData was deprecated. Shadow slice matrices and per-light shadow parameters are now passed to the GPU using entries in buffers m_AdditionalLightsWorldToShadow_SSBO and m_AdditionalShadowParams_SSBO", false)]
		public struct ShadowData
		{
			// Token: 0x04000048 RID: 72
			public Matrix4x4 worldToShadowMatrix;

			// Token: 0x04000049 RID: 73
			public Vector4 shadowParams;
		}

		// Token: 0x0200000F RID: 15
		[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false, false, -1, "C:\\Users\\jaspe\\Backpack Hero\\Library\\PackageCache\\com.unity.render-pipelines.universal@12.1.10\\ShaderLibrary\\ShaderTypes.cs")]
		public struct LightData
		{
			// Token: 0x0400004A RID: 74
			public Vector4 position;

			// Token: 0x0400004B RID: 75
			public Vector4 color;

			// Token: 0x0400004C RID: 76
			public Vector4 attenuation;

			// Token: 0x0400004D RID: 77
			public Vector4 spotDirection;

			// Token: 0x0400004E RID: 78
			public Vector4 occlusionProbeChannels;

			// Token: 0x0400004F RID: 79
			public uint layerMask;
		}
	}
}
