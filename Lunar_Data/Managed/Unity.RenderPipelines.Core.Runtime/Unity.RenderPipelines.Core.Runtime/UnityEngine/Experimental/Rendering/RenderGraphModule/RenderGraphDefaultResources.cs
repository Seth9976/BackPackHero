using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000025 RID: 37
	public class RenderGraphDefaultResources
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00009BE3 File Offset: 0x00007DE3
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00009BEB File Offset: 0x00007DEB
		public TextureHandle blackTexture { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00009BF4 File Offset: 0x00007DF4
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00009BFC File Offset: 0x00007DFC
		public TextureHandle whiteTexture { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00009C05 File Offset: 0x00007E05
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00009C0D File Offset: 0x00007E0D
		public TextureHandle clearTextureXR { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00009C16 File Offset: 0x00007E16
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00009C1E File Offset: 0x00007E1E
		public TextureHandle magentaTextureXR { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00009C27 File Offset: 0x00007E27
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00009C2F File Offset: 0x00007E2F
		public TextureHandle blackTextureXR { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00009C38 File Offset: 0x00007E38
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00009C40 File Offset: 0x00007E40
		public TextureHandle blackTextureArrayXR { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00009C49 File Offset: 0x00007E49
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00009C51 File Offset: 0x00007E51
		public TextureHandle blackUIntTextureXR { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00009C5A File Offset: 0x00007E5A
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00009C62 File Offset: 0x00007E62
		public TextureHandle blackTexture3DXR { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00009C6B File Offset: 0x00007E6B
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00009C73 File Offset: 0x00007E73
		public TextureHandle whiteTextureXR { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00009C7C File Offset: 0x00007E7C
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00009C84 File Offset: 0x00007E84
		public TextureHandle defaultShadowTexture { get; private set; }

		// Token: 0x06000168 RID: 360 RVA: 0x00009C90 File Offset: 0x00007E90
		internal RenderGraphDefaultResources()
		{
			this.m_BlackTexture2D = RTHandles.Alloc(Texture2D.blackTexture);
			this.m_WhiteTexture2D = RTHandles.Alloc(Texture2D.whiteTexture);
			this.m_ShadowTexture2D = RTHandles.Alloc(1, 1, 1, DepthBits.Depth32, GraphicsFormat.R8G8B8A8_SRGB, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, false, false, true, true, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009CEA File Offset: 0x00007EEA
		internal void Cleanup()
		{
			this.m_BlackTexture2D.Release();
			this.m_WhiteTexture2D.Release();
			this.m_ShadowTexture2D.Release();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009D10 File Offset: 0x00007F10
		internal void InitializeForRendering(RenderGraph renderGraph)
		{
			this.blackTexture = renderGraph.ImportTexture(this.m_BlackTexture2D);
			this.whiteTexture = renderGraph.ImportTexture(this.m_WhiteTexture2D);
			this.defaultShadowTexture = renderGraph.ImportTexture(this.m_ShadowTexture2D);
			this.clearTextureXR = renderGraph.ImportTexture(TextureXR.GetClearTexture());
			this.magentaTextureXR = renderGraph.ImportTexture(TextureXR.GetMagentaTexture());
			this.blackTextureXR = renderGraph.ImportTexture(TextureXR.GetBlackTexture());
			this.blackTextureArrayXR = renderGraph.ImportTexture(TextureXR.GetBlackTextureArray());
			this.blackUIntTextureXR = renderGraph.ImportTexture(TextureXR.GetBlackUIntTexture());
			this.blackTexture3DXR = renderGraph.ImportTexture(TextureXR.GetBlackTexture3D());
			this.whiteTextureXR = renderGraph.ImportTexture(TextureXR.GetWhiteTexture());
		}

		// Token: 0x04000100 RID: 256
		private bool m_IsValid;

		// Token: 0x04000101 RID: 257
		private RTHandle m_BlackTexture2D;

		// Token: 0x04000102 RID: 258
		private RTHandle m_WhiteTexture2D;

		// Token: 0x04000103 RID: 259
		private RTHandle m_ShadowTexture2D;
	}
}
