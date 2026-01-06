using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000024 RID: 36
	internal interface ILight2DCullResult
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015F RID: 351
		List<Light2D> visibleLights { get; }

		// Token: 0x06000160 RID: 352
		LightStats GetLightStatsByLayer(int layer);

		// Token: 0x06000161 RID: 353
		bool IsSceneLit();
	}
}
