using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000026 RID: 38
	internal static class Light2DManager
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000C50F File Offset: 0x0000A70F
		public static List<Light2D> lights { get; } = new List<Light2D>();

		// Token: 0x06000168 RID: 360 RVA: 0x0000C516 File Offset: 0x0000A716
		public static void RegisterLight(Light2D light)
		{
			Light2DManager.lights.Add(light);
			Light2DManager.ErrorIfDuplicateGlobalLight(light);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C529 File Offset: 0x0000A729
		public static void DeregisterLight(Light2D light)
		{
			Light2DManager.lights.Remove(light);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C538 File Offset: 0x0000A738
		public static void ErrorIfDuplicateGlobalLight(Light2D light)
		{
			if (light.lightType != Light2D.LightType.Global)
			{
				return;
			}
			foreach (int num in light.affectedSortingLayers)
			{
				if (Light2DManager.ContainsDuplicateGlobalLight(num, light.blendStyleIndex))
				{
					Debug.LogError("More than one global light on layer " + SortingLayer.IDToName(num) + " for light blend style index " + light.blendStyleIndex.ToString());
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public static bool GetGlobalColor(int sortingLayerIndex, int blendStyleIndex, out Color color)
		{
			bool flag = false;
			color = Color.black;
			foreach (Light2D light2D in Light2DManager.lights)
			{
				if (light2D.lightType == Light2D.LightType.Global && light2D.blendStyleIndex == blendStyleIndex && light2D.IsLitLayer(sortingLayerIndex))
				{
					if (true)
					{
						color = light2D.color * light2D.intensity;
						return true;
					}
					if (!flag)
					{
						color = light2D.color * light2D.intensity;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C650 File Offset: 0x0000A850
		private static bool ContainsDuplicateGlobalLight(int sortingLayerIndex, int blendStyleIndex)
		{
			int num = 0;
			foreach (Light2D light2D in Light2DManager.lights)
			{
				if (light2D.lightType == Light2D.LightType.Global && light2D.blendStyleIndex == blendStyleIndex && light2D.IsLitLayer(sortingLayerIndex))
				{
					if (num > 0)
					{
						return true;
					}
					num++;
				}
			}
			return false;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public static SortingLayer[] GetCachedSortingLayer()
		{
			if (Light2DManager.s_SortingLayers == null)
			{
				Light2DManager.s_SortingLayers = SortingLayer.layers;
			}
			return Light2DManager.s_SortingLayers;
		}

		// Token: 0x040000D9 RID: 217
		private static SortingLayer[] s_SortingLayers;
	}
}
