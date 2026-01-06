using System;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A5 RID: 165
	internal struct PostProcessPasses : IDisposable
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001DC61 File Offset: 0x0001BE61
		public ColorGradingLutPass colorGradingLutPass
		{
			get
			{
				return this.m_ColorGradingLutPass;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001DC69 File Offset: 0x0001BE69
		public PostProcessPass postProcessPass
		{
			get
			{
				return this.m_PostProcessPass;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001DC71 File Offset: 0x0001BE71
		public PostProcessPass finalPostProcessPass
		{
			get
			{
				return this.m_FinalPostProcessPass;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001DC79 File Offset: 0x0001BE79
		public RenderTargetHandle afterPostProcessColor
		{
			get
			{
				return this.m_AfterPostProcessColor;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001DC81 File Offset: 0x0001BE81
		public RenderTargetHandle colorGradingLut
		{
			get
			{
				return this.m_ColorGradingLut;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001DC89 File Offset: 0x0001BE89
		public bool isCreated
		{
			get
			{
				return this.m_CurrentPostProcessData != null;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001DC98 File Offset: 0x0001BE98
		public PostProcessPasses(PostProcessData rendererPostProcessData, Material blitMaterial)
		{
			this.m_ColorGradingLutPass = null;
			this.m_PostProcessPass = null;
			this.m_FinalPostProcessPass = null;
			this.m_AfterPostProcessColor = default(RenderTargetHandle);
			this.m_ColorGradingLut = default(RenderTargetHandle);
			this.m_CurrentPostProcessData = null;
			this.m_AfterPostProcessColor.Init("_AfterPostProcessTexture");
			this.m_ColorGradingLut.Init("_InternalGradingLut");
			this.m_RendererPostProcessData = rendererPostProcessData;
			this.m_BlitMaterial = blitMaterial;
			this.Recreate(rendererPostProcessData);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001DD10 File Offset: 0x0001BF10
		public void Recreate(PostProcessData data)
		{
			if (this.m_RendererPostProcessData)
			{
				data = this.m_RendererPostProcessData;
			}
			if (data == this.m_CurrentPostProcessData)
			{
				return;
			}
			if (this.m_CurrentPostProcessData != null)
			{
				ColorGradingLutPass colorGradingLutPass = this.m_ColorGradingLutPass;
				if (colorGradingLutPass != null)
				{
					colorGradingLutPass.Cleanup();
				}
				PostProcessPass postProcessPass = this.m_PostProcessPass;
				if (postProcessPass != null)
				{
					postProcessPass.Cleanup();
				}
				PostProcessPass finalPostProcessPass = this.m_FinalPostProcessPass;
				if (finalPostProcessPass != null)
				{
					finalPostProcessPass.Cleanup();
				}
				this.m_ColorGradingLutPass = null;
				this.m_PostProcessPass = null;
				this.m_FinalPostProcessPass = null;
				this.m_CurrentPostProcessData = null;
			}
			if (data != null)
			{
				this.m_ColorGradingLutPass = new ColorGradingLutPass(RenderPassEvent.BeforeRenderingPrePasses, data);
				this.m_PostProcessPass = new PostProcessPass(RenderPassEvent.BeforeRenderingPostProcessing, data, this.m_BlitMaterial);
				this.m_FinalPostProcessPass = new PostProcessPass(RenderPassEvent.AfterRenderingPostProcessing, data, this.m_BlitMaterial);
				this.m_CurrentPostProcessData = data;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001DDED File Offset: 0x0001BFED
		public void Dispose()
		{
			ColorGradingLutPass colorGradingLutPass = this.m_ColorGradingLutPass;
			if (colorGradingLutPass != null)
			{
				colorGradingLutPass.Cleanup();
			}
			PostProcessPass postProcessPass = this.m_PostProcessPass;
			if (postProcessPass != null)
			{
				postProcessPass.Cleanup();
			}
			PostProcessPass finalPostProcessPass = this.m_FinalPostProcessPass;
			if (finalPostProcessPass == null)
			{
				return;
			}
			finalPostProcessPass.Cleanup();
		}

		// Token: 0x040003F4 RID: 1012
		private ColorGradingLutPass m_ColorGradingLutPass;

		// Token: 0x040003F5 RID: 1013
		private PostProcessPass m_PostProcessPass;

		// Token: 0x040003F6 RID: 1014
		private PostProcessPass m_FinalPostProcessPass;

		// Token: 0x040003F7 RID: 1015
		private RenderTargetHandle m_AfterPostProcessColor;

		// Token: 0x040003F8 RID: 1016
		private RenderTargetHandle m_ColorGradingLut;

		// Token: 0x040003F9 RID: 1017
		private PostProcessData m_RendererPostProcessData;

		// Token: 0x040003FA RID: 1018
		private PostProcessData m_CurrentPostProcessData;

		// Token: 0x040003FB RID: 1019
		private Material m_BlitMaterial;
	}
}
