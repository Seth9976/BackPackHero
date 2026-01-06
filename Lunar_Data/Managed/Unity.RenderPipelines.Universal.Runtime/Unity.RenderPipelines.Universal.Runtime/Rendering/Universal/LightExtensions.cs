using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D3 RID: 211
	public static class LightExtensions
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		public static UniversalAdditionalLightData GetUniversalAdditionalLightData(this Light light)
		{
			GameObject gameObject = light.gameObject;
			UniversalAdditionalLightData universalAdditionalLightData;
			if (!gameObject.TryGetComponent<UniversalAdditionalLightData>(out universalAdditionalLightData))
			{
				universalAdditionalLightData = gameObject.AddComponent<UniversalAdditionalLightData>();
			}
			return universalAdditionalLightData;
		}
	}
}
