using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004C RID: 76
	public class UniversalRenderPipelineEditorResources : ScriptableObject
	{
		// Token: 0x0400021E RID: 542
		public UniversalRenderPipelineEditorResources.ShaderResources shaders;

		// Token: 0x0400021F RID: 543
		public UniversalRenderPipelineEditorResources.MaterialResources materials;

		// Token: 0x02000150 RID: 336
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040008DF RID: 2271
			[Reload("Shaders/Autodesk Interactive/Autodesk Interactive.shadergraph", ReloadAttribute.Package.Root)]
			public Shader autodeskInteractivePS;

			// Token: 0x040008E0 RID: 2272
			[Reload("Shaders/Autodesk Interactive/Autodesk Interactive Transparent.shadergraph", ReloadAttribute.Package.Root)]
			public Shader autodeskInteractiveTransparentPS;

			// Token: 0x040008E1 RID: 2273
			[Reload("Shaders/Autodesk Interactive/Autodesk Interactive Masked.shadergraph", ReloadAttribute.Package.Root)]
			public Shader autodeskInteractiveMaskedPS;

			// Token: 0x040008E2 RID: 2274
			[Reload("Shaders/Terrain/TerrainDetailLit.shader", ReloadAttribute.Package.Root)]
			public Shader terrainDetailLitPS;

			// Token: 0x040008E3 RID: 2275
			[Reload("Shaders/Terrain/WavingGrass.shader", ReloadAttribute.Package.Root)]
			public Shader terrainDetailGrassPS;

			// Token: 0x040008E4 RID: 2276
			[Reload("Shaders/Terrain/WavingGrassBillboard.shader", ReloadAttribute.Package.Root)]
			public Shader terrainDetailGrassBillboardPS;

			// Token: 0x040008E5 RID: 2277
			[Reload("Shaders/Nature/SpeedTree7.shader", ReloadAttribute.Package.Root)]
			public Shader defaultSpeedTree7PS;

			// Token: 0x040008E6 RID: 2278
			[Reload("Shaders/Nature/SpeedTree8.shader", ReloadAttribute.Package.Root)]
			public Shader defaultSpeedTree8PS;
		}

		// Token: 0x02000151 RID: 337
		[ReloadGroup]
		[Serializable]
		public sealed class MaterialResources
		{
			// Token: 0x040008E7 RID: 2279
			[Reload("Runtime/Materials/Lit.mat", ReloadAttribute.Package.Root)]
			public Material lit;

			// Token: 0x040008E8 RID: 2280
			[Reload("Runtime/Materials/ParticlesLit.mat", ReloadAttribute.Package.Root)]
			public Material particleLit;

			// Token: 0x040008E9 RID: 2281
			[Reload("Runtime/Materials/TerrainLit.mat", ReloadAttribute.Package.Root)]
			public Material terrainLit;

			// Token: 0x040008EA RID: 2282
			[Reload("Runtime/Materials/Decal.mat", ReloadAttribute.Package.Root)]
			public Material decal;
		}
	}
}
