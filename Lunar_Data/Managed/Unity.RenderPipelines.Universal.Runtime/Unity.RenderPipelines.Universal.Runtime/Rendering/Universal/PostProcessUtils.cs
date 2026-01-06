using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A6 RID: 166
	public static class PostProcessUtils
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x0001DE21 File Offset: 0x0001C021
		[Obsolete("This method is obsolete. Use ConfigureDithering override that takes camera pixel width and height instead.")]
		public static int ConfigureDithering(PostProcessData data, int index, Camera camera, Material material)
		{
			return PostProcessUtils.ConfigureDithering(data, index, camera.pixelWidth, camera.pixelHeight, material);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001DE38 File Offset: 0x0001C038
		public static int ConfigureDithering(PostProcessData data, int index, int cameraPixelWidth, int cameraPixelHeight, Material material)
		{
			Texture2D[] blueNoise16LTex = data.textures.blueNoise16LTex;
			if (blueNoise16LTex == null || blueNoise16LTex.Length == 0)
			{
				return 0;
			}
			if (++index >= blueNoise16LTex.Length)
			{
				index = 0;
			}
			float value = Random.value;
			float value2 = Random.value;
			Texture2D texture2D = blueNoise16LTex[index];
			material.SetTexture(PostProcessUtils.ShaderConstants._BlueNoise_Texture, texture2D);
			material.SetVector(PostProcessUtils.ShaderConstants._Dithering_Params, new Vector4((float)cameraPixelWidth / (float)texture2D.width, (float)cameraPixelHeight / (float)texture2D.height, value, value2));
			return index;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001DEAD File Offset: 0x0001C0AD
		[Obsolete("This method is obsolete. Use ConfigureFilmGrain override that takes camera pixel width and height instead.")]
		public static void ConfigureFilmGrain(PostProcessData data, FilmGrain settings, Camera camera, Material material)
		{
			PostProcessUtils.ConfigureFilmGrain(data, settings, camera.pixelWidth, camera.pixelHeight, material);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		public static void ConfigureFilmGrain(PostProcessData data, FilmGrain settings, int cameraPixelWidth, int cameraPixelHeight, Material material)
		{
			Texture texture = settings.texture.value;
			if (settings.type.value != FilmGrainLookup.Custom)
			{
				texture = data.textures.filmGrainTex[(int)settings.type.value];
			}
			float value = Random.value;
			float value2 = Random.value;
			Vector4 vector = ((texture == null) ? Vector4.zero : new Vector4((float)cameraPixelWidth / (float)texture.width, (float)cameraPixelHeight / (float)texture.height, value, value2));
			material.SetTexture(PostProcessUtils.ShaderConstants._Grain_Texture, texture);
			material.SetVector(PostProcessUtils.ShaderConstants._Grain_Params, new Vector2(settings.intensity.value * 4f, settings.response.value));
			material.SetVector(PostProcessUtils.ShaderConstants._Grain_TilingParams, vector);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001DF88 File Offset: 0x0001C188
		internal static void SetSourceSize(CommandBuffer cmd, RenderTextureDescriptor desc)
		{
			float num = (float)desc.width;
			float num2 = (float)desc.height;
			if (desc.useDynamicScale)
			{
				num *= ScalableBufferManager.widthScaleFactor;
				num2 *= ScalableBufferManager.heightScaleFactor;
			}
			cmd.SetGlobalVector(PostProcessUtils.ShaderConstants._SourceSize, new Vector4(num, num2, 1f / num, 1f / num2));
		}

		// Token: 0x0200017B RID: 379
		private static class ShaderConstants
		{
			// Token: 0x040009A7 RID: 2471
			public static readonly int _Grain_Texture = Shader.PropertyToID("_Grain_Texture");

			// Token: 0x040009A8 RID: 2472
			public static readonly int _Grain_Params = Shader.PropertyToID("_Grain_Params");

			// Token: 0x040009A9 RID: 2473
			public static readonly int _Grain_TilingParams = Shader.PropertyToID("_Grain_TilingParams");

			// Token: 0x040009AA RID: 2474
			public static readonly int _BlueNoise_Texture = Shader.PropertyToID("_BlueNoise_Texture");

			// Token: 0x040009AB RID: 2475
			public static readonly int _Dithering_Params = Shader.PropertyToID("_Dithering_Params");

			// Token: 0x040009AC RID: 2476
			public static readonly int _SourceSize = Shader.PropertyToID("_SourceSize");
		}
	}
}
