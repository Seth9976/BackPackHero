using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000039 RID: 57
	public static class ComponentUtility
	{
		// Token: 0x0600024A RID: 586 RVA: 0x000122A3 File Offset: 0x000104A3
		public static bool IsUniversalCamera(Camera camera)
		{
			return camera.GetComponent<UniversalAdditionalCameraData>() != null;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000122B1 File Offset: 0x000104B1
		public static bool IsUniversalLight(Light light)
		{
			return light.GetComponent<UniversalAdditionalLightData>() != null;
		}
	}
}
