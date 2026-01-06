using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000087 RID: 135
	[VolumeComponentMenuForRenderPipeline("Post-processing/Color Lookup", new Type[] { typeof(UniversalRenderPipeline) })]
	[Serializable]
	public sealed class ColorLookup : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0001D067 File Offset: 0x0001B267
		public bool IsActive()
		{
			return this.contribution.value > 0f && this.ValidateLUT();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001D083 File Offset: 0x0001B283
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001D088 File Offset: 0x0001B288
		public bool ValidateLUT()
		{
			UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
			if (asset == null || this.texture.value == null)
			{
				return false;
			}
			int colorGradingLutSize = asset.colorGradingLutSize;
			if (this.texture.value.height != colorGradingLutSize)
			{
				return false;
			}
			bool flag = false;
			Texture value = this.texture.value;
			Texture2D texture2D = value as Texture2D;
			if (texture2D == null)
			{
				RenderTexture renderTexture = value as RenderTexture;
				if (renderTexture != null)
				{
					flag |= renderTexture.dimension == TextureDimension.Tex2D && renderTexture.width == colorGradingLutSize * colorGradingLutSize && !renderTexture.sRGB;
				}
			}
			else
			{
				flag |= texture2D.width == colorGradingLutSize * colorGradingLutSize && !GraphicsFormatUtility.IsSRGBFormat(texture2D.graphicsFormat);
			}
			return flag;
		}

		// Token: 0x04000388 RID: 904
		[Tooltip("A 2D Lookup Texture (LUT) to use for color grading.")]
		public TextureParameter texture = new TextureParameter(null, false);

		// Token: 0x04000389 RID: 905
		[Tooltip("How much of the lookup texture will contribute to the color grading effect.")]
		public ClampedFloatParameter contribution = new ClampedFloatParameter(0f, 0f, 1f, false);
	}
}
