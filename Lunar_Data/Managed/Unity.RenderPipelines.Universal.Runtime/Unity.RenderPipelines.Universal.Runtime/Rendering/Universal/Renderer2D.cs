using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000031 RID: 49
	internal class Renderer2D : ScriptableRenderer
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000101B6 File Offset: 0x0000E3B6
		internal bool createColorTexture
		{
			get
			{
				return this.m_CreateColorTexture;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000101BE File Offset: 0x0000E3BE
		internal bool createDepthTexture
		{
			get
			{
				return this.m_CreateDepthTexture;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000101C6 File Offset: 0x0000E3C6
		internal ColorGradingLutPass colorGradingLutPass
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLutPass;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000101D3 File Offset: 0x0000E3D3
		internal PostProcessPass postProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.postProcessPass;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000101E0 File Offset: 0x0000E3E0
		internal PostProcessPass finalPostProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.finalPostProcessPass;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000101ED File Offset: 0x0000E3ED
		internal RenderTargetHandle afterPostProcessColorHandle
		{
			get
			{
				return this.m_PostProcessPasses.afterPostProcessColor;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000101FA File Offset: 0x0000E3FA
		internal RenderTargetHandle colorGradingLutHandle
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLut;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00010207 File Offset: 0x0000E407
		public override int SupportedCameraStackingTypes()
		{
			return 3;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0001020C File Offset: 0x0000E40C
		public Renderer2D(Renderer2DData data)
			: base(data)
		{
			this.m_BlitMaterial = CoreUtils.CreateEngineMaterial(data.blitShader);
			this.m_SamplingMaterial = CoreUtils.CreateEngineMaterial(data.samplingShader);
			this.m_Render2DLightingPass = new Render2DLightingPass(data, this.m_BlitMaterial, this.m_SamplingMaterial);
			this.m_PixelPerfectBackgroundPass = new PixelPerfectBackgroundPass(RenderPassEvent.AfterRenderingTransparents);
			this.m_FinalBlitPass = new FinalBlitPass((RenderPassEvent)1001, this.m_BlitMaterial);
			this.m_PostProcessPasses = new PostProcessPasses(data.postProcessData, this.m_BlitMaterial);
			this.m_UseDepthStencilBuffer = data.useDepthStencilBuffer;
			this.k_ColorTextureHandle.Init("_CameraColorTexture");
			this.k_DepthTextureHandle.Init("_CameraDepthAttachment");
			this.m_Renderer2DData = data;
			base.supportedRenderingFeatures = new ScriptableRenderer.RenderingFeatures();
			this.m_LightCullResult = new Light2DCullResult();
			this.m_Renderer2DData.lightCullResult = this.m_LightCullResult;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000102F8 File Offset: 0x0000E4F8
		protected override void Dispose(bool disposing)
		{
			this.m_PostProcessPasses.Dispose();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00010305 File Offset: 0x0000E505
		public Renderer2DData GetRenderer2DData()
		{
			return this.m_Renderer2DData;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00010310 File Offset: 0x0000E510
		private void CreateRenderTextures(ref CameraData cameraData, bool forceCreateColorTexture, FilterMode colorTextureFilterMode, CommandBuffer cmd, out RenderTargetHandle colorTargetHandle, out RenderTargetHandle depthTargetHandle)
		{
			ref RenderTextureDescriptor ptr = ref cameraData.cameraTargetDescriptor;
			if (cameraData.renderType == CameraRenderType.Base)
			{
				this.m_CreateColorTexture = forceCreateColorTexture || cameraData.postProcessEnabled || cameraData.isHdrEnabled || cameraData.isSceneViewCamera || !cameraData.isDefaultViewport || cameraData.requireSrgbConversion || !cameraData.resolveFinalTarget || this.m_Renderer2DData.useCameraSortingLayerTexture || !Mathf.Approximately(cameraData.renderScale, 1f);
				this.m_CreateDepthTexture = !cameraData.resolveFinalTarget && this.m_UseDepthStencilBuffer;
				colorTargetHandle = (this.m_CreateColorTexture ? this.k_ColorTextureHandle : RenderTargetHandle.CameraTarget);
				depthTargetHandle = (this.m_CreateDepthTexture ? this.k_DepthTextureHandle : colorTargetHandle);
				if (this.m_CreateColorTexture)
				{
					RenderTextureDescriptor renderTextureDescriptor = ptr;
					renderTextureDescriptor.depthBufferBits = ((this.m_CreateDepthTexture || !this.m_UseDepthStencilBuffer) ? 0 : 32);
					cmd.GetTemporaryRT(this.k_ColorTextureHandle.id, renderTextureDescriptor, colorTextureFilterMode);
				}
				if (this.m_CreateDepthTexture)
				{
					RenderTextureDescriptor renderTextureDescriptor2 = ptr;
					renderTextureDescriptor2.colorFormat = RenderTextureFormat.Depth;
					renderTextureDescriptor2.depthBufferBits = 32;
					renderTextureDescriptor2.bindMS = renderTextureDescriptor2.msaaSamples > 1 && !SystemInfo.supportsMultisampleAutoResolve && SystemInfo.supportsMultisampledTextures != 0;
					cmd.GetTemporaryRT(this.k_DepthTextureHandle.id, renderTextureDescriptor2, FilterMode.Point);
					return;
				}
			}
			else
			{
				this.m_CreateColorTexture = true;
				this.m_CreateDepthTexture = true;
				colorTargetHandle = this.k_ColorTextureHandle;
				depthTargetHandle = this.k_DepthTextureHandle;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001049C File Offset: 0x0000E69C
		public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ref CameraData ptr = ref renderingData.cameraData;
			ref RenderTextureDescriptor ptr2 = ref ptr.cameraTargetDescriptor;
			bool flag = renderingData.postProcessingEnabled;
			bool resolveFinalTarget = ptr.resolveFinalTarget;
			FilterMode filterMode = FilterMode.Bilinear;
			PixelPerfectCamera pixelPerfectCamera = null;
			bool flag2 = false;
			bool flag3 = false;
			if (base.DebugHandler != null && base.DebugHandler.AreAnySettingsActive)
			{
				flag = flag && base.DebugHandler.IsPostProcessingAllowed;
			}
			if (ptr.renderType == CameraRenderType.Base && resolveFinalTarget)
			{
				ptr.camera.TryGetComponent<PixelPerfectCamera>(out pixelPerfectCamera);
				if (pixelPerfectCamera != null && pixelPerfectCamera.enabled)
				{
					if (pixelPerfectCamera.offscreenRTSize != Vector2Int.zero)
					{
						flag2 = true;
						ptr2.width = pixelPerfectCamera.offscreenRTSize.x;
						ptr2.height = pixelPerfectCamera.offscreenRTSize.y;
					}
					filterMode = pixelPerfectCamera.finalBlitFilterMode;
					flag3 = pixelPerfectCamera.gridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
				}
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			RenderTargetHandle renderTargetHandle;
			RenderTargetHandle renderTargetHandle2;
			using (new ProfilingScope(commandBuffer, Renderer2D.m_ProfilingSampler))
			{
				this.CreateRenderTextures(ref ptr, flag2, filterMode, commandBuffer, out renderTargetHandle, out renderTargetHandle2);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
			base.ConfigureCameraTarget(renderTargetHandle.Identifier(), renderTargetHandle2.Identifier());
			this.isCameraColorTargetValid = true;
			base.AddRenderPasses(ref renderingData);
			this.isCameraColorTargetValid = false;
			if (flag && ptr.renderType == CameraRenderType.Base && this.m_PostProcessPasses.isCreated)
			{
				ColorGradingLutPass colorGradingLutPass = this.colorGradingLutPass;
				RenderTargetHandle renderTargetHandle3 = this.colorGradingLutHandle;
				colorGradingLutPass.Setup(in renderTargetHandle3);
				base.EnqueuePass(this.colorGradingLutPass);
			}
			bool flag4 = this.m_CreateDepthTexture || (!this.m_CreateColorTexture && this.m_UseDepthStencilBuffer);
			this.m_Render2DLightingPass.Setup(flag4);
			this.m_Render2DLightingPass.ConfigureTarget(renderTargetHandle.Identifier(), renderTargetHandle2.Identifier());
			base.EnqueuePass(this.m_Render2DLightingPass);
			bool flag5 = resolveFinalTarget && !flag3 && flag && ptr.antialiasing == AntialiasingMode.FastApproximateAntialiasing;
			bool flag6 = base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent == RenderPassEvent.AfterRenderingPostProcessing) != null;
			if (flag && this.m_PostProcessPasses.isCreated)
			{
				RenderTargetHandle renderTargetHandle4 = ((resolveFinalTarget && !flag3 && !flag5) ? RenderTargetHandle.CameraTarget : this.afterPostProcessColorHandle);
				PostProcessPass postProcessPass = this.postProcessPass;
				ref RenderTextureDescriptor ptr3 = ref ptr2;
				RenderTargetHandle renderTargetHandle5 = renderTargetHandle4;
				RenderTargetHandle renderTargetHandle3 = this.colorGradingLutHandle;
				postProcessPass.Setup(in ptr3, in renderTargetHandle, renderTargetHandle5, in renderTargetHandle2, in renderTargetHandle3, flag5, renderTargetHandle4 == RenderTargetHandle.CameraTarget);
				base.EnqueuePass(this.postProcessPass);
				renderTargetHandle = renderTargetHandle4;
			}
			if (pixelPerfectCamera != null && pixelPerfectCamera.enabled && (pixelPerfectCamera.cropFrame == PixelPerfectCamera.CropFrame.Pillarbox || pixelPerfectCamera.cropFrame == PixelPerfectCamera.CropFrame.Letterbox || pixelPerfectCamera.cropFrame == PixelPerfectCamera.CropFrame.Windowbox || pixelPerfectCamera.cropFrame == PixelPerfectCamera.CropFrame.StretchFill))
			{
				base.EnqueuePass(this.m_PixelPerfectBackgroundPass);
			}
			if (flag5 && this.m_PostProcessPasses.isCreated)
			{
				this.finalPostProcessPass.SetupFinalPass(in renderTargetHandle, flag6);
				base.EnqueuePass(this.finalPostProcessPass);
				return;
			}
			if (resolveFinalTarget && renderTargetHandle != RenderTargetHandle.CameraTarget)
			{
				this.m_FinalBlitPass.Setup(ptr2, renderTargetHandle);
				base.EnqueuePass(this.m_FinalBlitPass);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000107DC File Offset: 0x0000E9DC
		public override void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
			cullingParameters.cullingOptions = CullingOptions.None;
			cullingParameters.isOrthographic = cameraData.camera.orthographic;
			cullingParameters.shadowDistance = 0f;
			this.m_LightCullResult.SetupCulling(ref cullingParameters, cameraData.camera);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00010813 File Offset: 0x0000EA13
		public override void FinishRendering(CommandBuffer cmd)
		{
			if (this.m_CreateColorTexture)
			{
				cmd.ReleaseTemporaryRT(this.k_ColorTextureHandle.id);
			}
			if (this.m_CreateDepthTexture)
			{
				cmd.ReleaseTemporaryRT(this.k_DepthTextureHandle.id);
			}
		}

		// Token: 0x04000127 RID: 295
		internal const int k_DepthBufferBits = 32;

		// Token: 0x04000128 RID: 296
		private Render2DLightingPass m_Render2DLightingPass;

		// Token: 0x04000129 RID: 297
		private PixelPerfectBackgroundPass m_PixelPerfectBackgroundPass;

		// Token: 0x0400012A RID: 298
		private FinalBlitPass m_FinalBlitPass;

		// Token: 0x0400012B RID: 299
		private Light2DCullResult m_LightCullResult;

		// Token: 0x0400012C RID: 300
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Create Camera Textures");

		// Token: 0x0400012D RID: 301
		private bool m_UseDepthStencilBuffer = true;

		// Token: 0x0400012E RID: 302
		private bool m_CreateColorTexture;

		// Token: 0x0400012F RID: 303
		private bool m_CreateDepthTexture;

		// Token: 0x04000130 RID: 304
		private readonly RenderTargetHandle k_ColorTextureHandle;

		// Token: 0x04000131 RID: 305
		private readonly RenderTargetHandle k_DepthTextureHandle;

		// Token: 0x04000132 RID: 306
		private Material m_BlitMaterial;

		// Token: 0x04000133 RID: 307
		private Material m_SamplingMaterial;

		// Token: 0x04000134 RID: 308
		private Renderer2DData m_Renderer2DData;

		// Token: 0x04000135 RID: 309
		private PostProcessPasses m_PostProcessPasses;
	}
}
