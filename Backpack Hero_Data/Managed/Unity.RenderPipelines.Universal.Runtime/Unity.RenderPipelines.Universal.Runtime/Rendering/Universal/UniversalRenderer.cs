using System;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000D7 RID: 215
	public sealed class UniversalRenderer : ScriptableRenderer
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0002111C File Offset: 0x0001F31C
		public override int SupportedCameraStackingTypes()
		{
			RenderingMode renderingMode = this.m_RenderingMode;
			if (renderingMode == RenderingMode.Forward)
			{
				return 3;
			}
			if (renderingMode != RenderingMode.Deferred)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0002113E File Offset: 0x0001F33E
		internal RenderingMode renderingMode
		{
			get
			{
				return this.m_RenderingMode;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00021148 File Offset: 0x0001F348
		internal RenderingMode actualRenderingMode
		{
			get
			{
				if (!GL.wireframe && (base.DebugHandler == null || !base.DebugHandler.IsActiveModeUnsupportedForDeferred) && this.m_DeferredLights != null && this.m_DeferredLights.IsRuntimeSupportedThisFrame() && !this.m_DeferredLights.IsOverlay)
				{
					return this.renderingMode;
				}
				return RenderingMode.Forward;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0002119B File Offset: 0x0001F39B
		internal bool accurateGbufferNormals
		{
			get
			{
				return this.m_DeferredLights != null && this.m_DeferredLights.AccurateGbufferNormals;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000211B2 File Offset: 0x0001F3B2
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000211BA File Offset: 0x0001F3BA
		public DepthPrimingMode depthPrimingMode
		{
			get
			{
				return this.m_DepthPrimingMode;
			}
			set
			{
				this.m_DepthPrimingMode = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000211C3 File Offset: 0x0001F3C3
		internal ColorGradingLutPass colorGradingLutPass
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLutPass;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000211D0 File Offset: 0x0001F3D0
		internal PostProcessPass postProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.postProcessPass;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000211DD File Offset: 0x0001F3DD
		internal PostProcessPass finalPostProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.finalPostProcessPass;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000211EA File Offset: 0x0001F3EA
		internal RenderTargetHandle colorGradingLut
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLut;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000211F7 File Offset: 0x0001F3F7
		internal DeferredLights deferredLights
		{
			get
			{
				return this.m_DeferredLights;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00021200 File Offset: 0x0001F400
		public UniversalRenderer(UniversalRendererData data)
			: base(data)
		{
			UniversalRenderPipeline.m_XRSystem.InitializeXRSystemData(data.xrSystemData);
			Blitter.Initialize(data.shaders.coreBlitPS, data.shaders.coreBlitColorAndDepthPS);
			this.m_BlitMaterial = CoreUtils.CreateEngineMaterial(data.shaders.blitPS);
			this.m_CopyDepthMaterial = CoreUtils.CreateEngineMaterial(data.shaders.copyDepthPS);
			this.m_SamplingMaterial = CoreUtils.CreateEngineMaterial(data.shaders.samplingPS);
			this.m_StencilDeferredMaterial = CoreUtils.CreateEngineMaterial(data.shaders.stencilDeferredPS);
			this.m_CameraMotionVecMaterial = CoreUtils.CreateEngineMaterial(data.shaders.cameraMotionVector);
			this.m_ObjectMotionVecMaterial = CoreUtils.CreateEngineMaterial(data.shaders.objectMotionVector);
			StencilStateData defaultStencilState = data.defaultStencilState;
			this.m_DefaultStencilState = StencilState.defaultValue;
			this.m_DefaultStencilState.enabled = defaultStencilState.overrideStencilState;
			this.m_DefaultStencilState.SetCompareFunction(defaultStencilState.stencilCompareFunction);
			this.m_DefaultStencilState.SetPassOperation(defaultStencilState.passOperation);
			this.m_DefaultStencilState.SetFailOperation(defaultStencilState.failOperation);
			this.m_DefaultStencilState.SetZFailOperation(defaultStencilState.zFailOperation);
			this.m_IntermediateTextureMode = data.intermediateTextureMode;
			LightCookieManager.Settings @default = LightCookieManager.Settings.GetDefault();
			UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
			if (asset)
			{
				@default.atlas.format = asset.additionalLightsCookieFormat;
				@default.atlas.resolution = asset.additionalLightsCookieResolution;
			}
			this.m_LightCookieManager = new LightCookieManager(ref @default);
			base.stripShadowsOffVariants = true;
			base.stripAdditionalLightOffVariants = true;
			ForwardLights.InitParams initParams;
			initParams.lightCookieManager = this.m_LightCookieManager;
			initParams.clusteredRendering = data.clusteredRendering;
			initParams.tileSize = (int)data.tileSize;
			this.m_ForwardLights = new ForwardLights(initParams);
			this.m_RenderingMode = data.renderingMode;
			this.m_DepthPrimingMode = data.depthPrimingMode;
			this.m_CopyDepthMode = data.copyDepthMode;
			this.useRenderPassEnabled = data.useNativeRenderPass && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
			this.m_DepthPrimingRecommended = true;
			this.m_MainLightShadowCasterPass = new MainLightShadowCasterPass(RenderPassEvent.BeforeRenderingShadows);
			this.m_AdditionalLightsShadowCasterPass = new AdditionalLightsShadowCasterPass(RenderPassEvent.BeforeRenderingShadows);
			this.m_XROcclusionMeshPass = new XROcclusionMeshPass(RenderPassEvent.BeforeRenderingOpaques);
			this.m_XRCopyDepthPass = new CopyDepthPass((RenderPassEvent)1002, this.m_CopyDepthMaterial);
			this.m_DepthPrepass = new DepthOnlyPass(RenderPassEvent.BeforeRenderingPrePasses, RenderQueueRange.opaque, data.opaqueLayerMask);
			this.m_DepthNormalPrepass = new DepthNormalOnlyPass(RenderPassEvent.BeforeRenderingPrePasses, RenderQueueRange.opaque, data.opaqueLayerMask);
			this.m_MotionVectorPass = new MotionVectorRenderPass(this.m_CameraMotionVecMaterial, this.m_ObjectMotionVecMaterial);
			if (this.renderingMode == RenderingMode.Forward)
			{
				this.m_PrimedDepthCopyPass = new CopyDepthPass(RenderPassEvent.AfterRenderingPrePasses, this.m_CopyDepthMaterial);
			}
			if (this.renderingMode == RenderingMode.Deferred)
			{
				this.m_DeferredLights = new DeferredLights(new DeferredLights.InitParams
				{
					tileDepthInfoMaterial = this.m_TileDepthInfoMaterial,
					tileDeferredMaterial = this.m_TileDeferredMaterial,
					stencilDeferredMaterial = this.m_StencilDeferredMaterial,
					lightCookieManager = this.m_LightCookieManager
				}, this.useRenderPassEnabled);
				this.m_DeferredLights.AccurateGbufferNormals = data.accurateGbufferNormals;
				this.m_DeferredLights.TiledDeferredShading = false;
				this.m_GBufferPass = new GBufferPass(RenderPassEvent.BeforeRenderingGbuffer, RenderQueueRange.opaque, data.opaqueLayerMask, this.m_DefaultStencilState, defaultStencilState.stencilReference, this.m_DeferredLights);
				StencilState stencilState = DeferredLights.OverwriteStencil(this.m_DefaultStencilState, 96);
				ShaderTagId[] array = new ShaderTagId[]
				{
					new ShaderTagId("UniversalForwardOnly"),
					new ShaderTagId("SRPDefaultUnlit"),
					new ShaderTagId("LightweightForward")
				};
				int num = defaultStencilState.stencilReference | 0;
				this.m_GBufferCopyDepthPass = new CopyDepthPass((RenderPassEvent)211, this.m_CopyDepthMaterial);
				this.m_TileDepthRangePass = new TileDepthRangePass((RenderPassEvent)212, this.m_DeferredLights, 0);
				this.m_TileDepthRangeExtraPass = new TileDepthRangePass((RenderPassEvent)213, this.m_DeferredLights, 1);
				this.m_DeferredPass = new DeferredPass(RenderPassEvent.BeforeRenderingDeferredLights, this.m_DeferredLights);
				this.m_RenderOpaqueForwardOnlyPass = new DrawObjectsPass("Render Opaques Forward Only", array, true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, stencilState, num);
			}
			this.m_RenderOpaqueForwardPass = new DrawObjectsPass(URPProfileId.DrawOpaqueObjects, true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, this.m_DefaultStencilState, defaultStencilState.stencilReference);
			this.m_CopyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingSkybox, this.m_CopyDepthMaterial);
			this.m_DrawSkyboxPass = new DrawSkyboxPass(RenderPassEvent.BeforeRenderingSkybox);
			this.m_CopyColorPass = new CopyColorPass(RenderPassEvent.AfterRenderingSkybox, this.m_SamplingMaterial, this.m_BlitMaterial);
			this.m_TransparentSettingsPass = new TransparentSettingsPass(RenderPassEvent.BeforeRenderingTransparents, data.shadowTransparentReceive);
			this.m_RenderTransparentForwardPass = new DrawObjectsPass(URPProfileId.DrawTransparentObjects, false, RenderPassEvent.BeforeRenderingTransparents, RenderQueueRange.transparent, data.transparentLayerMask, this.m_DefaultStencilState, defaultStencilState.stencilReference);
			this.m_OnRenderObjectCallbackPass = new InvokeOnRenderObjectCallbackPass(RenderPassEvent.BeforeRenderingPostProcessing);
			this.m_PostProcessPasses = new PostProcessPasses(data.postProcessData, this.m_BlitMaterial);
			this.m_CapturePass = new CapturePass(RenderPassEvent.AfterRendering);
			this.m_FinalBlitPass = new FinalBlitPass((RenderPassEvent)1001, this.m_BlitMaterial);
			this.m_ColorBufferSystem = new RenderTargetBufferSystem("_CameraColorAttachment");
			this.m_CameraDepthAttachment.Init("_CameraDepthAttachment");
			this.m_DepthTexture.Init("_CameraDepthTexture");
			this.m_NormalsTexture.Init("_CameraNormalsTexture");
			this.m_OpaqueColor.Init("_CameraOpaqueTexture");
			this.m_DepthInfoTexture.Init("_DepthInfoTexture");
			this.m_TileDepthInfoTexture.Init("_TileDepthInfoTexture");
			base.supportedRenderingFeatures = new ScriptableRenderer.RenderingFeatures();
			if (this.renderingMode == RenderingMode.Deferred)
			{
				base.supportedRenderingFeatures.msaa = false;
				base.unsupportedGraphicsDeviceTypes = new GraphicsDeviceType[]
				{
					GraphicsDeviceType.OpenGLCore,
					GraphicsDeviceType.OpenGLES2,
					GraphicsDeviceType.OpenGLES3
				};
			}
			LensFlareCommonSRP.mergeNeeded = 0;
			LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample = 1;
			LensFlareCommonSRP.Initialize();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000217DC File Offset: 0x0001F9DC
		protected override void Dispose(bool disposing)
		{
			this.m_ForwardLights.Cleanup();
			this.m_PostProcessPasses.Dispose();
			CoreUtils.Destroy(this.m_BlitMaterial);
			CoreUtils.Destroy(this.m_CopyDepthMaterial);
			CoreUtils.Destroy(this.m_SamplingMaterial);
			CoreUtils.Destroy(this.m_TileDepthInfoMaterial);
			CoreUtils.Destroy(this.m_TileDeferredMaterial);
			CoreUtils.Destroy(this.m_StencilDeferredMaterial);
			CoreUtils.Destroy(this.m_CameraMotionVecMaterial);
			CoreUtils.Destroy(this.m_ObjectMotionVecMaterial);
			Blitter.Cleanup();
			LensFlareCommonSRP.Dispose();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00021864 File Offset: 0x0001FA64
		private void SetupFinalPassDebug(ref CameraData cameraData)
		{
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(ref cameraData))
			{
				DebugFullScreenMode debugFullScreenMode;
				int num;
				if (base.DebugHandler.TryGetFullscreenDebugMode(out debugFullScreenMode, out num))
				{
					Camera camera = cameraData.camera;
					float num2 = (float)camera.pixelWidth;
					float num3 = (float)camera.pixelHeight;
					float num4 = Mathf.Clamp01((float)num / 100f) * num3;
					float num5 = num4 * (num2 / num3) / num2;
					float num6 = num4 / num3;
					Rect rect = new Rect(1f - num5, 1f - num6, num5, num6);
					switch (debugFullScreenMode)
					{
					case DebugFullScreenMode.Depth:
						base.DebugHandler.SetDebugRenderTarget(this.m_DepthTexture.Identifier(), rect, true);
						return;
					case DebugFullScreenMode.AdditionalLightsShadowMap:
						base.DebugHandler.SetDebugRenderTarget(this.m_AdditionalLightsShadowCasterPass.m_AdditionalLightsShadowmapTexture, rect, false);
						return;
					case DebugFullScreenMode.MainLightShadowMap:
						base.DebugHandler.SetDebugRenderTarget(this.m_MainLightShadowCasterPass.m_MainLightShadowmapTexture, rect, false);
						return;
					default:
						return;
					}
				}
				else
				{
					base.DebugHandler.ResetDebugRenderTarget();
				}
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00021968 File Offset: 0x0001FB68
		private bool IsDepthPrimingEnabled(ref CameraData cameraData)
		{
			if (!this.CanCopyDepth(ref cameraData))
			{
				return false;
			}
			bool flag = (this.m_DepthPrimingRecommended && this.m_DepthPrimingMode == DepthPrimingMode.Auto) || this.m_DepthPrimingMode == DepthPrimingMode.Forced;
			bool flag2 = this.m_RenderingMode == RenderingMode.Forward;
			bool flag3 = cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth;
			bool flag4 = cameraData.cameraType != CameraType.Reflection;
			return flag && flag2 && flag3 && flag4;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000219D0 File Offset: 0x0001FBD0
		public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.m_ForwardLights.ProcessLights(ref renderingData);
			ref CameraData ptr = ref renderingData.cameraData;
			Camera camera = ptr.camera;
			RenderTextureDescriptor cameraTargetDescriptor = ptr.cameraTargetDescriptor;
			if (ptr.cameraType != CameraType.Game)
			{
				this.useRenderPassEnabled = false;
			}
			if (ptr.targetTexture != null && ptr.targetTexture.format == RenderTextureFormat.Depth)
			{
				base.ConfigureCameraTarget(BuiltinRenderTextureType.CameraTarget, BuiltinRenderTextureType.CameraTarget);
				base.AddRenderPasses(ref renderingData);
				base.EnqueuePass(this.m_RenderOpaqueForwardPass);
				base.EnqueuePass(this.m_DrawSkyboxPass);
				base.EnqueuePass(this.m_RenderTransparentForwardPass);
				return;
			}
			if (this.m_DeferredLights != null)
			{
				this.m_DeferredLights.ResolveMixedLightingMode(ref renderingData);
				this.m_DeferredLights.IsOverlay = ptr.renderType == CameraRenderType.Overlay;
			}
			bool isPreviewCamera = ptr.isPreviewCamera;
			bool flag = base.rendererFeatures.Count != 0 && this.m_IntermediateTextureMode == IntermediateTextureMode.Always && !isPreviewCamera;
			if (flag)
			{
				this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.GetBackBuffer();
				RenderTargetIdentifier renderTargetIdentifier = this.m_ActiveCameraColorAttachment.Identifier();
				if (ptr.xr.enabled)
				{
					renderTargetIdentifier = new RenderTargetIdentifier(renderTargetIdentifier, 0, CubemapFace.Unknown, -1);
				}
				base.ConfigureCameraColorTarget(renderTargetIdentifier);
			}
			this.isCameraColorTargetValid = true;
			base.AddRenderPasses(ref renderingData);
			this.isCameraColorTargetValid = false;
			UniversalRenderer.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(ref renderingData);
			bool flag2 = ptr.postProcessEnabled && this.m_PostProcessPasses.isCreated;
			bool flag3 = renderingData.postProcessingEnabled && this.m_PostProcessPasses.isCreated;
			bool flag4 = flag2 && ptr.postProcessingRequiresDepthTexture;
			bool flag5 = ptr.postProcessEnabled && this.m_PostProcessPasses.isCreated;
			bool isSceneViewCamera = ptr.isSceneViewCamera;
			base.useDepthPriming = this.IsDepthPrimingEnabled(ref ptr);
			bool flag6 = ptr.requiresDepthTexture || renderPassInputs.requiresDepthTexture || base.useDepthPriming;
			bool flag7 = false;
			bool flag8 = this.m_MainLightShadowCasterPass.Setup(ref renderingData);
			bool flag9 = this.m_AdditionalLightsShadowCasterPass.Setup(ref renderingData);
			bool flag10 = this.m_TransparentSettingsPass.Setup(ref renderingData);
			bool flag11 = this.m_CopyDepthMode == CopyDepthMode.ForcePrepass;
			bool flag12 = (flag6 || flag4) && (!this.CanCopyDepth(ref renderingData.cameraData) || flag11);
			flag12 = flag12 || isSceneViewCamera;
			flag12 = flag12 || flag7;
			flag12 = flag12 || isPreviewCamera;
			flag12 |= renderPassInputs.requiresDepthPrepass;
			flag12 |= renderPassInputs.requiresNormalsTexture;
			if (flag12 && this.actualRenderingMode == RenderingMode.Deferred && !renderPassInputs.requiresNormalsTexture)
			{
				flag12 = false;
			}
			flag12 |= base.useDepthPriming;
			if (flag6)
			{
				RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
				if (renderPassInputs.requiresDepthTexture)
				{
					renderPassEvent = (RenderPassEvent)Mathf.Min(500, renderPassInputs.requiresDepthTextureEarliestEvent - (RenderPassEvent)1);
				}
				this.m_CopyDepthPass.renderPassEvent = renderPassEvent;
			}
			else if (flag4 || isSceneViewCamera || flag7)
			{
				this.m_CopyDepthPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
			}
			flag |= this.RequiresIntermediateColorTexture(ref ptr);
			flag |= renderPassInputs.requiresColorTexture;
			flag |= renderPassInputs.requiresColorTextureCreated;
			flag &= !isPreviewCamera;
			bool flag13 = (flag6 || flag4) && !flag12;
			flag13 |= !ptr.resolveFinalTarget;
			flag13 |= this.actualRenderingMode == RenderingMode.Deferred && !this.useRenderPassEnabled;
			flag13 |= base.useDepthPriming;
			if (ptr.xr.enabled)
			{
				flag13 = flag13 || flag;
				flag = flag13;
			}
			base.useDepthPriming &= SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan || cameraTargetDescriptor.msaaSamples == 1;
			if (this.useRenderPassEnabled || base.useDepthPriming)
			{
				flag13 = flag13 || flag;
				flag = flag13;
			}
			RenderTextureDescriptor renderTextureDescriptor = cameraTargetDescriptor;
			renderTextureDescriptor.useMipMap = false;
			renderTextureDescriptor.autoGenerateMips = false;
			renderTextureDescriptor.depthBufferBits = 0;
			this.m_ColorBufferSystem.SetCameraSettings(renderTextureDescriptor, FilterMode.Bilinear);
			if (ptr.renderType == CameraRenderType.Base)
			{
				RenderTargetHandle cameraTarget = RenderTargetHandle.GetCameraTarget(ptr.xr);
				bool flag14 = camera.sceneViewFilterMode == Camera.SceneViewFilterMode.ShowFiltered;
				this.m_ActiveCameraColorAttachment = ((flag && !flag14) ? this.m_ColorBufferSystem.GetBackBuffer() : cameraTarget);
				this.m_ActiveCameraDepthAttachment = ((flag13 && !flag14) ? this.m_CameraDepthAttachment : cameraTarget);
				if (flag || flag13)
				{
					this.CreateCameraRenderTarget(context, ref cameraTargetDescriptor, base.useDepthPriming);
				}
			}
			else
			{
				this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.GetBackBuffer();
				this.m_ActiveCameraDepthAttachment = this.m_CameraDepthAttachment;
			}
			ptr.renderer.useDepthPriming = base.useDepthPriming;
			bool flag15 = !flag12 && (flag6 || flag4) && flag13;
			bool flag16 = renderingData.cameraData.requiresOpaqueTexture || renderPassInputs.requiresColorTexture;
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(ref ptr))
			{
				DebugFullScreenMode debugFullScreenMode;
				base.DebugHandler.TryGetFullscreenDebugMode(out debugFullScreenMode);
				if (debugFullScreenMode == DebugFullScreenMode.Depth)
				{
					flag12 = true;
				}
				if (!base.DebugHandler.IsLightingActive)
				{
					flag8 = false;
					flag9 = false;
					if (!isSceneViewCamera)
					{
						flag12 = false;
						flag5 = false;
						flag16 = false;
						flag15 = false;
					}
				}
				if (this.useRenderPassEnabled)
				{
					this.useRenderPassEnabled = base.DebugHandler.IsRenderPassSupported;
				}
			}
			RenderTargetIdentifier renderTargetIdentifier2 = this.m_ActiveCameraColorAttachment.Identifier();
			RenderTargetIdentifier renderTargetIdentifier3 = this.m_ActiveCameraDepthAttachment.Identifier();
			if (ptr.xr.enabled)
			{
				renderTargetIdentifier2 = new RenderTargetIdentifier(renderTargetIdentifier2, 0, CubemapFace.Unknown, -1);
				renderTargetIdentifier3 = new RenderTargetIdentifier(renderTargetIdentifier3, 0, CubemapFace.Unknown, -1);
			}
			base.ConfigureCameraTarget(renderTargetIdentifier2, renderTargetIdentifier3);
			bool flag17 = base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent == RenderPassEvent.AfterRenderingPostProcessing) != null;
			if (flag8)
			{
				base.EnqueuePass(this.m_MainLightShadowCasterPass);
			}
			if (flag9)
			{
				base.EnqueuePass(this.m_AdditionalLightsShadowCasterPass);
			}
			if (flag12)
			{
				if (renderPassInputs.requiresNormalsTexture)
				{
					if (this.actualRenderingMode == RenderingMode.Deferred)
					{
						int gbufferNormalSmoothnessIndex = this.m_DeferredLights.GBufferNormalSmoothnessIndex;
						this.m_DepthNormalPrepass.Setup(cameraTargetDescriptor, this.m_ActiveCameraDepthAttachment, this.m_DeferredLights.GbufferAttachments[gbufferNormalSmoothnessIndex]);
						RenderTextureDescriptor normalDescriptor = this.m_DepthNormalPrepass.normalDescriptor;
						normalDescriptor.graphicsFormat = this.m_DeferredLights.GetGBufferFormat(gbufferNormalSmoothnessIndex);
						this.m_DepthNormalPrepass.normalDescriptor = normalDescriptor;
						this.m_DepthNormalPrepass.allocateDepth = false;
						if (RenderPassEvent.AfterRenderingGbuffer <= renderPassInputs.requiresDepthNormalAtEvent && renderPassInputs.requiresDepthNormalAtEvent <= RenderPassEvent.BeforeRenderingOpaques)
						{
							this.m_DepthNormalPrepass.shaderTagIds = UniversalRenderer.k_DepthNormalsOnly;
						}
					}
					else
					{
						this.m_DepthNormalPrepass.Setup(cameraTargetDescriptor, this.m_DepthTexture, this.m_NormalsTexture);
					}
					base.EnqueuePass(this.m_DepthNormalPrepass);
				}
				else if (this.actualRenderingMode != RenderingMode.Deferred)
				{
					this.m_DepthPrepass.Setup(cameraTargetDescriptor, this.m_DepthTexture);
					base.EnqueuePass(this.m_DepthPrepass);
				}
			}
			if (base.useDepthPriming && (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan || cameraTargetDescriptor.msaaSamples == 1))
			{
				this.m_PrimedDepthCopyPass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DepthTexture);
				this.m_PrimedDepthCopyPass.AllocateRT = false;
				base.EnqueuePass(this.m_PrimedDepthCopyPass);
			}
			if (flag5)
			{
				ColorGradingLutPass colorGradingLutPass = this.colorGradingLutPass;
				RenderTargetHandle renderTargetHandle = this.colorGradingLut;
				colorGradingLutPass.Setup(in renderTargetHandle);
				base.EnqueuePass(this.colorGradingLutPass);
			}
			if (ptr.xr.hasValidOcclusionMesh)
			{
				base.EnqueuePass(this.m_XROcclusionMeshPass);
			}
			bool resolveFinalTarget = ptr.resolveFinalTarget;
			if (this.actualRenderingMode == RenderingMode.Deferred)
			{
				if (this.m_DeferredLights.UseRenderPass && (RenderPassEvent.AfterRenderingGbuffer == renderPassInputs.requiresDepthNormalAtEvent || !this.useRenderPassEnabled))
				{
					this.m_DeferredLights.DisableFramebufferFetchInput();
				}
				this.EnqueueDeferred(ref renderingData, flag12, renderPassInputs.requiresNormalsTexture, flag8, flag9);
			}
			else
			{
				RenderBufferStoreAction renderBufferStoreAction = RenderBufferStoreAction.Store;
				if (cameraTargetDescriptor.msaaSamples > 1)
				{
					renderBufferStoreAction = (flag16 ? RenderBufferStoreAction.StoreAndResolve : RenderBufferStoreAction.Store);
				}
				RenderBufferStoreAction renderBufferStoreAction2 = ((flag16 || flag15 || !resolveFinalTarget) ? RenderBufferStoreAction.Store : RenderBufferStoreAction.DontCare);
				if (ptr.xr.enabled && ptr.xr.copyDepth)
				{
					renderBufferStoreAction2 = RenderBufferStoreAction.Store;
				}
				this.m_RenderOpaqueForwardPass.ConfigureColorStoreAction(renderBufferStoreAction, 0U);
				this.m_RenderOpaqueForwardPass.ConfigureDepthStoreAction(renderBufferStoreAction2);
				base.EnqueuePass(this.m_RenderOpaqueForwardPass);
			}
			Skybox skybox;
			if (camera.clearFlags == CameraClearFlags.Skybox && ptr.renderType != CameraRenderType.Overlay && (RenderSettings.skybox != null || (camera.TryGetComponent<Skybox>(out skybox) && skybox.material != null)))
			{
				base.EnqueuePass(this.m_DrawSkyboxPass);
			}
			if (flag15)
			{
				this.m_CopyDepthPass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DepthTexture);
				if (this.actualRenderingMode == RenderingMode.Deferred && !this.useRenderPassEnabled)
				{
					this.m_CopyDepthPass.AllocateRT = false;
				}
				base.EnqueuePass(this.m_CopyDepthPass);
			}
			if (!flag12 && !flag15)
			{
				Shader.SetGlobalTexture(this.m_DepthTexture.id, SystemInfo.usesReversedZBuffer ? Texture2D.blackTexture : Texture2D.whiteTexture);
			}
			if (flag16)
			{
				Downsampling opaqueDownsampling = UniversalRenderPipeline.asset.opaqueDownsampling;
				this.m_CopyColorPass.Setup(this.m_ActiveCameraColorAttachment.Identifier(), this.m_OpaqueColor, opaqueDownsampling);
				base.EnqueuePass(this.m_CopyColorPass);
			}
			if (renderPassInputs.requiresMotionVectors && !ptr.xr.enabled)
			{
				SupportedRenderingFeatures.active.motionVectors = true;
				PreviousFrameData motionDataForCamera = MotionVectorRendering.instance.GetMotionDataForCamera(camera, ptr);
				this.m_MotionVectorPass.Setup(motionDataForCamera);
				base.EnqueuePass(this.m_MotionVectorPass);
			}
			if (flag10)
			{
				base.EnqueuePass(this.m_TransparentSettingsPass);
			}
			RenderBufferStoreAction renderBufferStoreAction3 = ((cameraTargetDescriptor.msaaSamples > 1 && resolveFinalTarget) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.Store);
			RenderBufferStoreAction renderBufferStoreAction4 = (resolveFinalTarget ? RenderBufferStoreAction.DontCare : RenderBufferStoreAction.Store);
			if (flag15 && this.m_CopyDepthPass.renderPassEvent >= RenderPassEvent.AfterRenderingTransparents)
			{
				renderBufferStoreAction4 = RenderBufferStoreAction.Store;
			}
			this.m_RenderTransparentForwardPass.ConfigureColorStoreAction(renderBufferStoreAction3, 0U);
			this.m_RenderTransparentForwardPass.ConfigureDepthStoreAction(renderBufferStoreAction4);
			base.EnqueuePass(this.m_RenderTransparentForwardPass);
			base.EnqueuePass(this.m_OnRenderObjectCallbackPass);
			bool flag18 = renderingData.cameraData.captureActions != null && resolveFinalTarget;
			bool flag19 = flag3 && resolveFinalTarget && (renderingData.cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing || (renderingData.cameraData.imageScalingMode == ImageScalingMode.Upscaling && renderingData.cameraData.upscalingFilter > ImageUpscalingFilter.Linear));
			bool flag20 = !flag18 && !flag17 && !flag19;
			if (resolveFinalTarget)
			{
				this.SetupFinalPassDebug(ref ptr);
				if (flag2)
				{
					bool flag21 = flag20;
					PostProcessPass postProcessPass = this.postProcessPass;
					bool flag22 = flag20;
					RenderTargetHandle renderTargetHandle = this.colorGradingLut;
					postProcessPass.Setup(in cameraTargetDescriptor, in this.m_ActiveCameraColorAttachment, flag22, in this.m_ActiveCameraDepthAttachment, in renderTargetHandle, flag19, flag21);
					base.EnqueuePass(this.postProcessPass);
				}
				RenderTargetHandle activeCameraColorAttachment = this.m_ActiveCameraColorAttachment;
				if (flag19)
				{
					this.finalPostProcessPass.SetupFinalPass(in activeCameraColorAttachment, true);
					base.EnqueuePass(this.finalPostProcessPass);
				}
				if (renderingData.cameraData.captureActions != null)
				{
					this.m_CapturePass.Setup(activeCameraColorAttachment);
					base.EnqueuePass(this.m_CapturePass);
				}
				if (!flag19 && (!flag2 || flag17 || flag18) && !(this.m_ActiveCameraColorAttachment == RenderTargetHandle.GetCameraTarget(ptr.xr)))
				{
					this.m_FinalBlitPass.Setup(cameraTargetDescriptor, activeCameraColorAttachment);
					base.EnqueuePass(this.m_FinalBlitPass);
				}
				if (ptr.xr.enabled && !(this.m_ActiveCameraDepthAttachment == RenderTargetHandle.GetCameraTarget(ptr.xr)) && ptr.xr.copyDepth)
				{
					this.m_XRCopyDepthPass.Setup(this.m_ActiveCameraDepthAttachment, RenderTargetHandle.GetCameraTarget(ptr.xr));
					base.EnqueuePass(this.m_XRCopyDepthPass);
					return;
				}
			}
			else if (flag2)
			{
				PostProcessPass postProcessPass2 = this.postProcessPass;
				bool flag23 = false;
				RenderTargetHandle renderTargetHandle = this.colorGradingLut;
				postProcessPass2.Setup(in cameraTargetDescriptor, in this.m_ActiveCameraColorAttachment, flag23, in this.m_ActiveCameraDepthAttachment, in renderTargetHandle, false, false);
				base.EnqueuePass(this.postProcessPass);
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00022521 File Offset: 0x00020721
		public override void SetupLights(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.m_ForwardLights.Setup(context, ref renderingData);
			if (this.actualRenderingMode == RenderingMode.Deferred)
			{
				this.m_DeferredLights.SetupLights(context, ref renderingData);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00022548 File Offset: 0x00020748
		public override void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
			bool flag = !UniversalRenderPipeline.asset.supportsMainLightShadows && !UniversalRenderPipeline.asset.supportsAdditionalLightShadows;
			bool flag2 = Mathf.Approximately(cameraData.maxShadowDistance, 0f);
			if (flag || flag2)
			{
				cullingParameters.cullingOptions &= ~CullingOptions.ShadowCasters;
			}
			if (this.actualRenderingMode == RenderingMode.Deferred)
			{
				cullingParameters.maximumVisibleLights = 65535;
			}
			else
			{
				cullingParameters.maximumVisibleLights = UniversalRenderPipeline.maxVisibleAdditionalLights + 1;
			}
			cullingParameters.shadowDistance = cameraData.maxShadowDistance;
			cullingParameters.conservativeEnclosingSphere = UniversalRenderPipeline.asset.conservativeEnclosingSphere;
			cullingParameters.numIterationsEnclosingSphere = UniversalRenderPipeline.asset.numIterationsEnclosingSphere;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000225E4 File Offset: 0x000207E4
		public override void FinishRendering(CommandBuffer cmd)
		{
			this.m_ColorBufferSystem.Clear(cmd);
			if (this.m_ActiveCameraColorAttachment != RenderTargetHandle.CameraTarget)
			{
				this.m_ActiveCameraColorAttachment = RenderTargetHandle.CameraTarget;
			}
			if (this.m_ActiveCameraDepthAttachment != RenderTargetHandle.CameraTarget)
			{
				cmd.ReleaseTemporaryRT(this.m_ActiveCameraDepthAttachment.id);
				this.m_ActiveCameraDepthAttachment = RenderTargetHandle.CameraTarget;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00022648 File Offset: 0x00020848
		private void EnqueueDeferred(ref RenderingData renderingData, bool hasDepthPrepass, bool hasNormalPrepass, bool applyMainShadow, bool applyAdditionalShadow)
		{
			this.m_DeferredLights.Setup(ref renderingData, applyAdditionalShadow ? this.m_AdditionalLightsShadowCasterPass : null, hasDepthPrepass, hasNormalPrepass, this.m_DepthTexture, this.m_DepthInfoTexture, this.m_TileDepthInfoTexture, this.m_ActiveCameraDepthAttachment, this.m_ActiveCameraColorAttachment);
			if (this.useRenderPassEnabled && this.m_DeferredLights.UseRenderPass)
			{
				this.m_GBufferPass.Configure(null, renderingData.cameraData.cameraTargetDescriptor);
				this.m_DeferredPass.Configure(null, renderingData.cameraData.cameraTargetDescriptor);
			}
			base.EnqueuePass(this.m_GBufferPass);
			if (!this.useRenderPassEnabled || !this.m_DeferredLights.UseRenderPass)
			{
				this.m_GBufferCopyDepthPass.Setup(this.m_CameraDepthAttachment, this.m_DepthTexture);
				base.EnqueuePass(this.m_GBufferCopyDepthPass);
			}
			if (this.m_DeferredLights.HasTileLights())
			{
				base.EnqueuePass(this.m_TileDepthRangePass);
				if (this.m_DeferredLights.HasTileDepthRangeExtraPass())
				{
					base.EnqueuePass(this.m_TileDepthRangeExtraPass);
				}
			}
			base.EnqueuePass(this.m_DeferredPass);
			base.EnqueuePass(this.m_RenderOpaqueForwardOnlyPass);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00022760 File Offset: 0x00020960
		private UniversalRenderer.RenderPassInputSummary GetRenderPassInputs(ref RenderingData renderingData)
		{
			RenderPassEvent renderPassEvent = ((this.m_RenderingMode == RenderingMode.Deferred) ? RenderPassEvent.BeforeRenderingGbuffer : RenderPassEvent.BeforeRenderingOpaques);
			UniversalRenderer.RenderPassInputSummary renderPassInputSummary = default(UniversalRenderer.RenderPassInputSummary);
			renderPassInputSummary.requiresDepthNormalAtEvent = RenderPassEvent.BeforeRenderingOpaques;
			renderPassInputSummary.requiresDepthTextureEarliestEvent = RenderPassEvent.BeforeRenderingPostProcessing;
			for (int i = 0; i < base.activeRenderPassQueue.Count; i++)
			{
				ScriptableRenderPass scriptableRenderPass = base.activeRenderPassQueue[i];
				bool flag = (scriptableRenderPass.input & ScriptableRenderPassInput.Depth) > ScriptableRenderPassInput.None;
				bool flag2 = (scriptableRenderPass.input & ScriptableRenderPassInput.Normal) > ScriptableRenderPassInput.None;
				bool flag3 = (scriptableRenderPass.input & ScriptableRenderPassInput.Color) > ScriptableRenderPassInput.None;
				bool flag4 = (scriptableRenderPass.input & ScriptableRenderPassInput.Motion) > ScriptableRenderPassInput.None;
				bool flag5 = scriptableRenderPass.renderPassEvent <= renderPassEvent;
				if (scriptableRenderPass is DBufferRenderPass)
				{
					renderPassInputSummary.requiresColorTextureCreated = true;
				}
				renderPassInputSummary.requiresDepthTexture = renderPassInputSummary.requiresDepthTexture || flag;
				renderPassInputSummary.requiresDepthPrepass |= flag2 || (flag && flag5);
				renderPassInputSummary.requiresNormalsTexture = renderPassInputSummary.requiresNormalsTexture || flag2;
				renderPassInputSummary.requiresColorTexture = renderPassInputSummary.requiresColorTexture || flag3;
				renderPassInputSummary.requiresMotionVectors = renderPassInputSummary.requiresMotionVectors || flag4;
				if (flag)
				{
					renderPassInputSummary.requiresDepthTextureEarliestEvent = (RenderPassEvent)Mathf.Min((int)scriptableRenderPass.renderPassEvent, (int)renderPassInputSummary.requiresDepthTextureEarliestEvent);
				}
				if (flag2 || flag)
				{
					renderPassInputSummary.requiresDepthNormalAtEvent = (RenderPassEvent)Mathf.Min((int)scriptableRenderPass.renderPassEvent, (int)renderPassInputSummary.requiresDepthNormalAtEvent);
				}
			}
			return renderPassInputSummary;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000228A5 File Offset: 0x00020AA5
		private bool IsGLESDevice()
		{
			return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000228BC File Offset: 0x00020ABC
		private void CreateCameraRenderTarget(ScriptableRenderContext context, ref RenderTextureDescriptor descriptor, bool primedDepth)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(null, UniversalRenderer.Profiling.createCameraRenderTarget))
			{
				if (this.m_ActiveCameraColorAttachment != RenderTargetHandle.CameraTarget)
				{
					bool flag = this.m_ActiveCameraDepthAttachment == RenderTargetHandle.CameraTarget;
					RenderTextureDescriptor renderTextureDescriptor = descriptor;
					renderTextureDescriptor.useMipMap = false;
					renderTextureDescriptor.autoGenerateMips = false;
					renderTextureDescriptor.depthBufferBits = (flag ? 32 : 0);
					this.m_ColorBufferSystem.SetCameraSettings(commandBuffer, renderTextureDescriptor, FilterMode.Bilinear);
					if (flag)
					{
						base.ConfigureCameraTarget(this.m_ColorBufferSystem.GetBackBuffer(commandBuffer).id, this.m_ColorBufferSystem.GetBufferA().id);
					}
					else
					{
						base.ConfigureCameraColorTarget(this.m_ColorBufferSystem.GetBackBuffer(commandBuffer).id);
					}
					this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.GetBackBuffer(commandBuffer);
					commandBuffer.SetGlobalTexture("_CameraColorTexture", this.m_ActiveCameraColorAttachment.id);
					commandBuffer.SetGlobalTexture("_AfterPostProcessTexture", this.m_ActiveCameraColorAttachment.id);
				}
				if (this.m_ActiveCameraDepthAttachment != RenderTargetHandle.CameraTarget)
				{
					RenderTextureDescriptor renderTextureDescriptor2 = descriptor;
					renderTextureDescriptor2.useMipMap = false;
					renderTextureDescriptor2.autoGenerateMips = false;
					renderTextureDescriptor2.bindMS = renderTextureDescriptor2.msaaSamples > 1 && SystemInfo.supportsMultisampledTextures != 0;
					if (this.IsGLESDevice())
					{
						renderTextureDescriptor2.bindMS = false;
					}
					renderTextureDescriptor2.colorFormat = RenderTextureFormat.Depth;
					renderTextureDescriptor2.depthBufferBits = 32;
					commandBuffer.GetTemporaryRT(this.m_ActiveCameraDepthAttachment.id, renderTextureDescriptor2, FilterMode.Point);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00022A94 File Offset: 0x00020C94
		private bool PlatformRequiresExplicitMsaaResolve()
		{
			return (!SystemInfo.supportsMultisampleAutoResolve || !Application.isMobilePlatform) && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Metal;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00022AB4 File Offset: 0x00020CB4
		private bool RequiresIntermediateColorTexture(ref CameraData cameraData)
		{
			if (cameraData.renderType == CameraRenderType.Base && !cameraData.resolveFinalTarget)
			{
				return true;
			}
			if (this.actualRenderingMode == RenderingMode.Deferred)
			{
				return true;
			}
			bool isSceneViewCamera = cameraData.isSceneViewCamera;
			RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
			int msaaSamples = cameraTargetDescriptor.msaaSamples;
			bool flag = cameraData.imageScalingMode > ImageScalingMode.None;
			bool flag2 = cameraTargetDescriptor.dimension == TextureDimension.Tex2D;
			bool flag3 = msaaSamples > 1 && this.PlatformRequiresExplicitMsaaResolve();
			bool flag4 = cameraData.targetTexture != null && !isSceneViewCamera;
			bool flag5 = cameraData.captureActions != null;
			if (cameraData.xr.enabled)
			{
				flag = false;
				flag2 = cameraData.xr.renderTargetDesc.dimension == cameraTargetDescriptor.dimension;
			}
			bool flag6 = cameraData.postProcessEnabled || cameraData.requiresOpaqueTexture || flag3 || !cameraData.isDefaultViewport;
			if (flag4)
			{
				return flag6;
			}
			return flag6 || isSceneViewCamera || flag || cameraData.isHdrEnabled || !flag2 || flag5 || cameraData.requireSrgbConversion;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00022BB0 File Offset: 0x00020DB0
		private bool CanCopyDepth(ref CameraData cameraData)
		{
			bool flag = cameraData.cameraTargetDescriptor.msaaSamples > 1;
			bool flag2 = SystemInfo.copyTextureSupport > CopyTextureSupport.None;
			bool flag3 = RenderingUtils.SupportsRenderTextureFormat(RenderTextureFormat.Depth);
			bool flag4 = !flag && (flag3 || flag2);
			bool flag5 = flag && SystemInfo.supportsMultisampledTextures != 0;
			return (!this.IsGLESDevice() || !flag5) && (flag4 || flag5);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00022C04 File Offset: 0x00020E04
		internal override void SwapColorBuffer(CommandBuffer cmd)
		{
			this.m_ColorBufferSystem.Swap();
			if (this.m_ActiveCameraDepthAttachment == RenderTargetHandle.CameraTarget)
			{
				base.ConfigureCameraTarget(this.m_ColorBufferSystem.GetBackBuffer(cmd).id, this.m_ColorBufferSystem.GetBufferA().id);
			}
			else
			{
				base.ConfigureCameraColorTarget(this.m_ColorBufferSystem.GetBackBuffer(cmd).id);
			}
			this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.GetBackBuffer();
			cmd.SetGlobalTexture("_CameraColorTexture", this.m_ActiveCameraColorAttachment.id);
			cmd.SetGlobalTexture("_AfterPostProcessTexture", this.m_ActiveCameraColorAttachment.id);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00022CD0 File Offset: 0x00020ED0
		internal override RenderTargetIdentifier GetCameraColorFrontBuffer(CommandBuffer cmd)
		{
			return this.m_ColorBufferSystem.GetFrontBuffer(cmd).id;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00022CF6 File Offset: 0x00020EF6
		internal override void EnableSwapBufferMSAA(bool enable)
		{
			this.m_ColorBufferSystem.EnableMSAA(enable);
		}

		// Token: 0x04000500 RID: 1280
		internal const int k_DepthStencilBufferBits = 32;

		// Token: 0x04000501 RID: 1281
		private static readonly List<ShaderTagId> k_DepthNormalsOnly = new List<ShaderTagId>
		{
			new ShaderTagId("DepthNormalsOnly")
		};

		// Token: 0x04000502 RID: 1282
		private DepthOnlyPass m_DepthPrepass;

		// Token: 0x04000503 RID: 1283
		private DepthNormalOnlyPass m_DepthNormalPrepass;

		// Token: 0x04000504 RID: 1284
		private CopyDepthPass m_PrimedDepthCopyPass;

		// Token: 0x04000505 RID: 1285
		private MotionVectorRenderPass m_MotionVectorPass;

		// Token: 0x04000506 RID: 1286
		private MainLightShadowCasterPass m_MainLightShadowCasterPass;

		// Token: 0x04000507 RID: 1287
		private AdditionalLightsShadowCasterPass m_AdditionalLightsShadowCasterPass;

		// Token: 0x04000508 RID: 1288
		private GBufferPass m_GBufferPass;

		// Token: 0x04000509 RID: 1289
		private CopyDepthPass m_GBufferCopyDepthPass;

		// Token: 0x0400050A RID: 1290
		private TileDepthRangePass m_TileDepthRangePass;

		// Token: 0x0400050B RID: 1291
		private TileDepthRangePass m_TileDepthRangeExtraPass;

		// Token: 0x0400050C RID: 1292
		private DeferredPass m_DeferredPass;

		// Token: 0x0400050D RID: 1293
		private DrawObjectsPass m_RenderOpaqueForwardOnlyPass;

		// Token: 0x0400050E RID: 1294
		private DrawObjectsPass m_RenderOpaqueForwardPass;

		// Token: 0x0400050F RID: 1295
		private DrawSkyboxPass m_DrawSkyboxPass;

		// Token: 0x04000510 RID: 1296
		private CopyDepthPass m_CopyDepthPass;

		// Token: 0x04000511 RID: 1297
		private CopyColorPass m_CopyColorPass;

		// Token: 0x04000512 RID: 1298
		private TransparentSettingsPass m_TransparentSettingsPass;

		// Token: 0x04000513 RID: 1299
		private DrawObjectsPass m_RenderTransparentForwardPass;

		// Token: 0x04000514 RID: 1300
		private InvokeOnRenderObjectCallbackPass m_OnRenderObjectCallbackPass;

		// Token: 0x04000515 RID: 1301
		private FinalBlitPass m_FinalBlitPass;

		// Token: 0x04000516 RID: 1302
		private CapturePass m_CapturePass;

		// Token: 0x04000517 RID: 1303
		private XROcclusionMeshPass m_XROcclusionMeshPass;

		// Token: 0x04000518 RID: 1304
		private CopyDepthPass m_XRCopyDepthPass;

		// Token: 0x04000519 RID: 1305
		internal RenderTargetBufferSystem m_ColorBufferSystem;

		// Token: 0x0400051A RID: 1306
		private RenderTargetHandle m_ActiveCameraColorAttachment;

		// Token: 0x0400051B RID: 1307
		private RenderTargetHandle m_ColorFrontBuffer;

		// Token: 0x0400051C RID: 1308
		private RenderTargetHandle m_ActiveCameraDepthAttachment;

		// Token: 0x0400051D RID: 1309
		private RenderTargetHandle m_CameraDepthAttachment;

		// Token: 0x0400051E RID: 1310
		private RenderTargetHandle m_DepthTexture;

		// Token: 0x0400051F RID: 1311
		private RenderTargetHandle m_NormalsTexture;

		// Token: 0x04000520 RID: 1312
		private RenderTargetHandle m_OpaqueColor;

		// Token: 0x04000521 RID: 1313
		private RenderTargetHandle m_DepthInfoTexture;

		// Token: 0x04000522 RID: 1314
		private RenderTargetHandle m_TileDepthInfoTexture;

		// Token: 0x04000523 RID: 1315
		private ForwardLights m_ForwardLights;

		// Token: 0x04000524 RID: 1316
		private DeferredLights m_DeferredLights;

		// Token: 0x04000525 RID: 1317
		private RenderingMode m_RenderingMode;

		// Token: 0x04000526 RID: 1318
		private DepthPrimingMode m_DepthPrimingMode;

		// Token: 0x04000527 RID: 1319
		private CopyDepthMode m_CopyDepthMode;

		// Token: 0x04000528 RID: 1320
		private bool m_DepthPrimingRecommended;

		// Token: 0x04000529 RID: 1321
		private StencilState m_DefaultStencilState;

		// Token: 0x0400052A RID: 1322
		private LightCookieManager m_LightCookieManager;

		// Token: 0x0400052B RID: 1323
		private IntermediateTextureMode m_IntermediateTextureMode;

		// Token: 0x0400052C RID: 1324
		private Material m_BlitMaterial;

		// Token: 0x0400052D RID: 1325
		private Material m_CopyDepthMaterial;

		// Token: 0x0400052E RID: 1326
		private Material m_SamplingMaterial;

		// Token: 0x0400052F RID: 1327
		private Material m_TileDepthInfoMaterial;

		// Token: 0x04000530 RID: 1328
		private Material m_TileDeferredMaterial;

		// Token: 0x04000531 RID: 1329
		private Material m_StencilDeferredMaterial;

		// Token: 0x04000532 RID: 1330
		private Material m_CameraMotionVecMaterial;

		// Token: 0x04000533 RID: 1331
		private Material m_ObjectMotionVecMaterial;

		// Token: 0x04000534 RID: 1332
		private PostProcessPasses m_PostProcessPasses;

		// Token: 0x02000188 RID: 392
		private static class Profiling
		{
			// Token: 0x040009EE RID: 2542
			private const string k_Name = "UniversalRenderer";

			// Token: 0x040009EF RID: 2543
			public static readonly ProfilingSampler createCameraRenderTarget = new ProfilingSampler("UniversalRenderer.CreateCameraRenderTarget");
		}

		// Token: 0x02000189 RID: 393
		private struct RenderPassInputSummary
		{
			// Token: 0x040009F0 RID: 2544
			internal bool requiresDepthTexture;

			// Token: 0x040009F1 RID: 2545
			internal bool requiresDepthPrepass;

			// Token: 0x040009F2 RID: 2546
			internal bool requiresNormalsTexture;

			// Token: 0x040009F3 RID: 2547
			internal bool requiresColorTexture;

			// Token: 0x040009F4 RID: 2548
			internal bool requiresColorTextureCreated;

			// Token: 0x040009F5 RID: 2549
			internal bool requiresMotionVectors;

			// Token: 0x040009F6 RID: 2550
			internal RenderPassEvent requiresDepthNormalAtEvent;

			// Token: 0x040009F7 RID: 2551
			internal RenderPassEvent requiresDepthTextureEarliestEvent;
		}
	}
}
