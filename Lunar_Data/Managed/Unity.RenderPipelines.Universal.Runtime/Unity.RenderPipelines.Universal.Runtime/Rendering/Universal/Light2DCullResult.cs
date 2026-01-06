using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000025 RID: 37
	internal class Light2DCullResult : ILight2DCullResult
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public List<Light2D> visibleLights
		{
			get
			{
				return this.m_VisibleLights;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public bool IsSceneLit()
		{
			return Light2DManager.lights.Count > 0;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C300 File Offset: 0x0000A500
		public LightStats GetLightStatsByLayer(int layer)
		{
			LightStats lightStats = default(LightStats);
			foreach (Light2D light2D in this.visibleLights)
			{
				if (light2D.IsLitLayer(layer))
				{
					lightStats.totalLights++;
					if (light2D.normalMapQuality != Light2D.NormalMapQuality.Disabled)
					{
						lightStats.totalNormalMapUsage++;
					}
					if (light2D.volumeIntensity > 0f)
					{
						lightStats.totalVolumetricUsage++;
					}
					lightStats.blendStylesUsed |= 1U << light2D.blendStyleIndex;
					if (light2D.lightType != Light2D.LightType.Global)
					{
						lightStats.blendStylesWithLights |= 1U << light2D.blendStyleIndex;
					}
				}
			}
			return lightStats;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public void SetupCulling(ref ScriptableCullingParameters cullingParameters, Camera camera)
		{
			this.m_VisibleLights.Clear();
			foreach (Light2D light2D in Light2DManager.lights)
			{
				if ((camera.cullingMask & (1 << light2D.gameObject.layer)) != 0)
				{
					if (light2D.lightType == Light2D.LightType.Global)
					{
						this.m_VisibleLights.Add(light2D);
					}
					else
					{
						Vector3 position = light2D.boundingSphere.position;
						bool flag = false;
						for (int i = 0; i < cullingParameters.cullingPlaneCount; i++)
						{
							Plane cullingPlane = cullingParameters.GetCullingPlane(i);
							if (math.dot(position, cullingPlane.normal) + cullingPlane.distance < -light2D.boundingSphere.radius)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this.m_VisibleLights.Add(light2D);
						}
					}
				}
			}
			this.m_VisibleLights.Sort((Light2D l1, Light2D l2) => l1.lightOrder - l2.lightOrder);
		}

		// Token: 0x040000D8 RID: 216
		private List<Light2D> m_VisibleLights = new List<Light2D>();
	}
}
