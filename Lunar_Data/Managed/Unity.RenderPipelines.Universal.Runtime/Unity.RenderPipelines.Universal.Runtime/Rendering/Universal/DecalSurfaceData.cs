using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A7 RID: 167
	internal enum DecalSurfaceData
	{
		// Token: 0x040003FD RID: 1021
		[Tooltip("Decals will affect only base color and emission.")]
		Albedo,
		// Token: 0x040003FE RID: 1022
		[Tooltip("Decals will affect only base color, normal and emission.")]
		AlbedoNormal,
		// Token: 0x040003FF RID: 1023
		[Tooltip("Decals will affect base color, normal, metallic, ambient occlusion, smoothness and emission.")]
		AlbedoNormalMAOS
	}
}
