using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B4 RID: 948
	internal static class CameraEventUtils
	{
		// Token: 0x06001F48 RID: 8008 RVA: 0x00032F34 File Offset: 0x00031134
		public static bool IsValid(CameraEvent value)
		{
			return value >= CameraEvent.BeforeDepthTexture && value <= CameraEvent.AfterHaloAndLensFlares;
		}

		// Token: 0x04000AF7 RID: 2807
		private const CameraEvent k_MinimumValue = CameraEvent.BeforeDepthTexture;

		// Token: 0x04000AF8 RID: 2808
		private const CameraEvent k_MaximumValue = CameraEvent.AfterHaloAndLensFlares;
	}
}
