using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class PostProcessData : ScriptableObject
	{
		// Token: 0x04000189 RID: 393
		public PostProcessData.ShaderResources shaders;

		// Token: 0x0400018A RID: 394
		public PostProcessData.TextureResources textures;

		// Token: 0x0200014E RID: 334
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x040008CD RID: 2253
			[Reload("Shaders/PostProcessing/StopNaN.shader", ReloadAttribute.Package.Root)]
			public Shader stopNanPS;

			// Token: 0x040008CE RID: 2254
			[Reload("Shaders/PostProcessing/SubpixelMorphologicalAntialiasing.shader", ReloadAttribute.Package.Root)]
			public Shader subpixelMorphologicalAntialiasingPS;

			// Token: 0x040008CF RID: 2255
			[Reload("Shaders/PostProcessing/GaussianDepthOfField.shader", ReloadAttribute.Package.Root)]
			public Shader gaussianDepthOfFieldPS;

			// Token: 0x040008D0 RID: 2256
			[Reload("Shaders/PostProcessing/BokehDepthOfField.shader", ReloadAttribute.Package.Root)]
			public Shader bokehDepthOfFieldPS;

			// Token: 0x040008D1 RID: 2257
			[Reload("Shaders/PostProcessing/CameraMotionBlur.shader", ReloadAttribute.Package.Root)]
			public Shader cameraMotionBlurPS;

			// Token: 0x040008D2 RID: 2258
			[Reload("Shaders/PostProcessing/PaniniProjection.shader", ReloadAttribute.Package.Root)]
			public Shader paniniProjectionPS;

			// Token: 0x040008D3 RID: 2259
			[Reload("Shaders/PostProcessing/LutBuilderLdr.shader", ReloadAttribute.Package.Root)]
			public Shader lutBuilderLdrPS;

			// Token: 0x040008D4 RID: 2260
			[Reload("Shaders/PostProcessing/LutBuilderHdr.shader", ReloadAttribute.Package.Root)]
			public Shader lutBuilderHdrPS;

			// Token: 0x040008D5 RID: 2261
			[Reload("Shaders/PostProcessing/Bloom.shader", ReloadAttribute.Package.Root)]
			public Shader bloomPS;

			// Token: 0x040008D6 RID: 2262
			[Reload("Shaders/PostProcessing/LensFlareDataDriven.shader", ReloadAttribute.Package.Root)]
			public Shader LensFlareDataDrivenPS;

			// Token: 0x040008D7 RID: 2263
			[Reload("Shaders/PostProcessing/ScalingSetup.shader", ReloadAttribute.Package.Root)]
			public Shader scalingSetupPS;

			// Token: 0x040008D8 RID: 2264
			[Reload("Shaders/PostProcessing/EdgeAdaptiveSpatialUpsampling.shader", ReloadAttribute.Package.Root)]
			public Shader easuPS;

			// Token: 0x040008D9 RID: 2265
			[Reload("Shaders/PostProcessing/UberPost.shader", ReloadAttribute.Package.Root)]
			public Shader uberPostPS;

			// Token: 0x040008DA RID: 2266
			[Reload("Shaders/PostProcessing/FinalPost.shader", ReloadAttribute.Package.Root)]
			public Shader finalPostPassPS;
		}

		// Token: 0x0200014F RID: 335
		[ReloadGroup]
		[Serializable]
		public sealed class TextureResources
		{
			// Token: 0x040008DB RID: 2267
			[Reload("Textures/BlueNoise16/L/LDR_LLL1_{0}.png", 0, 32, ReloadAttribute.Package.Root)]
			public Texture2D[] blueNoise16LTex;

			// Token: 0x040008DC RID: 2268
			[Reload(new string[] { "Textures/FilmGrain/Thin01.png", "Textures/FilmGrain/Thin02.png", "Textures/FilmGrain/Medium01.png", "Textures/FilmGrain/Medium02.png", "Textures/FilmGrain/Medium03.png", "Textures/FilmGrain/Medium04.png", "Textures/FilmGrain/Medium05.png", "Textures/FilmGrain/Medium06.png", "Textures/FilmGrain/Large01.png", "Textures/FilmGrain/Large02.png" }, ReloadAttribute.Package.Root)]
			public Texture2D[] filmGrainTex;

			// Token: 0x040008DD RID: 2269
			[Reload("Textures/SMAA/AreaTex.tga", ReloadAttribute.Package.Root)]
			public Texture2D smaaAreaTex;

			// Token: 0x040008DE RID: 2270
			[Reload("Textures/SMAA/SearchTex.tga", ReloadAttribute.Package.Root)]
			public Texture2D smaaSearchTex;
		}
	}
}
