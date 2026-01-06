using System;
using System.Diagnostics;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000AF RID: 175
	[DisallowMultipleRendererFeature("Decal")]
	[Tooltip("With this Renderer Feature, Unity can project specific Materials (decals) onto other objects in the Scene.")]
	internal class DecalRendererFeature : ScriptableRendererFeature
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001E1DF File Offset: 0x0001C3DF
		private static SharedDecalEntityManager sharedDecalEntityManager { get; } = new SharedDecalEntityManager();

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
		internal bool intermediateRendering
		{
			get
			{
				return this.m_Technique == DecalTechnique.DBuffer;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001E1F1 File Offset: 0x0001C3F1
		public override void Create()
		{
			this.m_DecalPreviewPass = new DecalPreviewPass();
			this.m_RecreateSystems = true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001E205 File Offset: 0x0001C405
		internal DBufferSettings GetDBufferSettings()
		{
			if (this.m_Settings.technique == DecalTechniqueOption.Automatic)
			{
				return new DBufferSettings
				{
					surfaceData = DecalSurfaceData.AlbedoNormalMAOS
				};
			}
			return this.m_Settings.dBufferSettings;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001E22C File Offset: 0x0001C42C
		internal DecalScreenSpaceSettings GetScreenSpaceSettings()
		{
			if (this.m_Settings.technique == DecalTechniqueOption.Automatic)
			{
				return new DecalScreenSpaceSettings
				{
					normalBlend = DecalNormalBlend.Low,
					useGBuffer = false
				};
			}
			return this.m_Settings.screenSpaceSettings;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001E25C File Offset: 0x0001C45C
		internal DecalTechnique GetTechnique(ScriptableRendererData renderer)
		{
			UniversalRendererData universalRendererData = renderer as UniversalRendererData;
			if (universalRendererData == null)
			{
				Debug.LogError("Only universal renderer supports Decal renderer feature.");
				return DecalTechnique.Invalid;
			}
			bool flag = universalRendererData.renderingMode == RenderingMode.Deferred;
			return this.GetTechnique(flag);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001E298 File Offset: 0x0001C498
		internal DecalTechnique GetTechnique(ScriptableRenderer renderer)
		{
			UniversalRenderer universalRenderer = renderer as UniversalRenderer;
			if (universalRenderer == null)
			{
				Debug.LogError("Only universal renderer supports Decal renderer feature.");
				return DecalTechnique.Invalid;
			}
			bool flag = universalRenderer.renderingMode == RenderingMode.Deferred;
			return this.GetTechnique(flag);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		private DecalTechnique GetTechnique(bool isDeferred)
		{
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2)
			{
				Debug.LogError("Decals are not supported with OpenGLES2.");
				return DecalTechnique.Invalid;
			}
			DecalTechnique decalTechnique = DecalTechnique.Invalid;
			switch (this.m_Settings.technique)
			{
			case DecalTechniqueOption.Automatic:
				if (this.IsAutomaticDBuffer())
				{
					decalTechnique = DecalTechnique.DBuffer;
				}
				else
				{
					decalTechnique = DecalTechnique.ScreenSpace;
				}
				break;
			case DecalTechniqueOption.DBuffer:
				decalTechnique = DecalTechnique.DBuffer;
				break;
			case DecalTechniqueOption.ScreenSpace:
				if (this.m_Settings.screenSpaceSettings.useGBuffer && isDeferred)
				{
					decalTechnique = DecalTechnique.GBuffer;
				}
				else
				{
					decalTechnique = DecalTechnique.ScreenSpace;
				}
				break;
			}
			bool flag = SystemInfo.supportedRenderTargetCount >= 4;
			if (decalTechnique == DecalTechnique.DBuffer && !flag)
			{
				Debug.LogError("Decal DBuffer technique requires MRT4 support.");
				return DecalTechnique.Invalid;
			}
			if (decalTechnique == DecalTechnique.GBuffer && !flag)
			{
				Debug.LogError("Decal useGBuffer option requires MRT4 support.");
				return DecalTechnique.Invalid;
			}
			return decalTechnique;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001E370 File Offset: 0x0001C570
		private bool IsAutomaticDBuffer()
		{
			return Application.platform != RuntimePlatform.WebGLPlayer && !GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.SHADER_API_MOBILE);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001E388 File Offset: 0x0001C588
		private void RecreateSystemsIfNeeded(ScriptableRenderer renderer, in CameraData cameraData)
		{
			if (!this.m_RecreateSystems)
			{
				return;
			}
			this.m_Technique = this.GetTechnique(renderer);
			this.m_DBufferSettings = this.GetDBufferSettings();
			this.m_ScreenSpaceSettings = this.GetScreenSpaceSettings();
			this.m_CopyDepthMaterial = CoreUtils.CreateEngineMaterial(this.m_CopyDepthPS);
			this.m_CopyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingPrePasses, this.m_CopyDepthMaterial);
			this.m_DBufferClearMaterial = CoreUtils.CreateEngineMaterial(this.m_DBufferClear);
			if (this.m_DecalEntityManager == null)
			{
				this.m_DecalEntityManager = DecalRendererFeature.sharedDecalEntityManager.Get();
			}
			this.m_DecalUpdateCachedSystem = new DecalUpdateCachedSystem(this.m_DecalEntityManager);
			this.m_DecalUpdateCulledSystem = new DecalUpdateCulledSystem(this.m_DecalEntityManager);
			this.m_DecalCreateDrawCallSystem = new DecalCreateDrawCallSystem(this.m_DecalEntityManager, this.m_Settings.maxDrawDistance);
			if (this.intermediateRendering)
			{
				this.m_DecalUpdateCullingGroupSystem = new DecalUpdateCullingGroupSystem(this.m_DecalEntityManager, this.m_Settings.maxDrawDistance);
			}
			else
			{
				this.m_DecalSkipCulledSystem = new DecalSkipCulledSystem(this.m_DecalEntityManager);
			}
			this.m_DrawErrorSystem = new DecalDrawErrorSystem(this.m_DecalEntityManager, this.m_Technique);
			UniversalRenderer universalRenderer = renderer as UniversalRenderer;
			switch (this.m_Technique)
			{
			case DecalTechnique.DBuffer:
				this.m_DecalDrawDBufferSystem = new DecalDrawDBufferSystem(this.m_DecalEntityManager);
				this.m_DBufferRenderPass = new DBufferRenderPass(this.m_DBufferClearMaterial, this.m_DBufferSettings, this.m_DecalDrawDBufferSystem);
				this.m_DecalDrawForwardEmissiveSystem = new DecalDrawFowardEmissiveSystem(this.m_DecalEntityManager);
				this.m_ForwardEmissivePass = new DecalForwardEmissivePass(this.m_DecalDrawForwardEmissiveSystem);
				if (universalRenderer.actualRenderingMode == RenderingMode.Deferred)
				{
					this.m_DBufferRenderPass.deferredLights = universalRenderer.deferredLights;
					this.m_DBufferRenderPass.deferredLights.DisableFramebufferFetchInput();
				}
				break;
			case DecalTechnique.ScreenSpace:
				this.m_CopyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingOpaques, this.m_DBufferClearMaterial);
				this.m_DecalDrawScreenSpaceSystem = new DecalDrawScreenSpaceSystem(this.m_DecalEntityManager);
				this.m_ScreenSpaceDecalRenderPass = new DecalScreenSpaceRenderPass(this.m_ScreenSpaceSettings, this.intermediateRendering ? this.m_DecalDrawScreenSpaceSystem : null);
				break;
			case DecalTechnique.GBuffer:
				this.m_DeferredLights = universalRenderer.deferredLights;
				this.m_CopyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingOpaques, this.m_DBufferClearMaterial);
				this.m_DrawGBufferSystem = new DecalDrawGBufferSystem(this.m_DecalEntityManager);
				this.m_GBufferRenderPass = new DecalGBufferRenderPass(this.m_ScreenSpaceSettings, this.intermediateRendering ? this.m_DrawGBufferSystem : null);
				break;
			}
			this.m_RecreateSystems = false;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		public override void OnCameraPreCull(ScriptableRenderer renderer, in CameraData cameraData)
		{
			if (cameraData.cameraType == CameraType.Preview)
			{
				return;
			}
			this.RecreateSystemsIfNeeded(renderer, in cameraData);
			this.m_DecalEntityManager.Update();
			this.m_DecalUpdateCachedSystem.Execute();
			if (this.intermediateRendering)
			{
				this.m_DecalUpdateCullingGroupSystem.Execute(cameraData.camera);
			}
			else
			{
				this.m_DecalSkipCulledSystem.Execute(cameraData.camera);
				this.m_DecalCreateDrawCallSystem.Execute();
				if (this.m_Technique == DecalTechnique.ScreenSpace)
				{
					this.m_DecalDrawScreenSpaceSystem.Execute(in cameraData);
				}
				else if (this.m_Technique == DecalTechnique.GBuffer)
				{
					this.m_DrawGBufferSystem.Execute(in cameraData);
				}
			}
			this.m_DrawErrorSystem.Execute(in cameraData);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001E690 File Offset: 0x0001C890
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (renderingData.cameraData.cameraType == CameraType.Preview)
			{
				renderer.EnqueuePass(this.m_DecalPreviewPass);
				return;
			}
			this.RecreateSystemsIfNeeded(renderer, in renderingData.cameraData);
			if (this.intermediateRendering)
			{
				this.m_DecalUpdateCulledSystem.Execute();
				this.m_DecalCreateDrawCallSystem.Execute();
			}
			switch (this.m_Technique)
			{
			case DecalTechnique.DBuffer:
				if ((renderer as UniversalRenderer).actualRenderingMode == RenderingMode.Deferred)
				{
					this.m_CopyDepthPass.Setup(new RenderTargetHandle(this.m_DBufferRenderPass.cameraDepthAttachmentIndentifier), new RenderTargetHandle(this.m_DBufferRenderPass.cameraDepthTextureIndentifier));
				}
				else
				{
					this.m_CopyDepthPass.Setup(new RenderTargetHandle(this.m_DBufferRenderPass.cameraDepthTextureIndentifier), new RenderTargetHandle(this.m_DBufferRenderPass.dBufferDepthIndentifier));
					this.m_CopyDepthPass.MssaSamples = 1;
				}
				renderer.EnqueuePass(this.m_CopyDepthPass);
				renderer.EnqueuePass(this.m_DBufferRenderPass);
				renderer.EnqueuePass(this.m_ForwardEmissivePass);
				return;
			case DecalTechnique.ScreenSpace:
				renderer.EnqueuePass(this.m_ScreenSpaceDecalRenderPass);
				return;
			case DecalTechnique.GBuffer:
				this.m_GBufferRenderPass.Setup(this.m_DeferredLights);
				renderer.EnqueuePass(this.m_GBufferRenderPass);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		internal override bool SupportsNativeRenderPass()
		{
			return this.m_Technique == DecalTechnique.GBuffer || this.m_Technique == DecalTechnique.ScreenSpace;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001E7D6 File Offset: 0x0001C9D6
		protected override void Dispose(bool disposing)
		{
			CoreUtils.Destroy(this.m_CopyDepthMaterial);
			CoreUtils.Destroy(this.m_DBufferClearMaterial);
			if (this.m_DecalEntityManager != null)
			{
				this.m_DecalEntityManager = null;
				DecalRendererFeature.sharedDecalEntityManager.Release(this.m_DecalEntityManager);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001E80D File Offset: 0x0001CA0D
		[Conditional("ADAPTIVE_PERFORMANCE_4_0_0_OR_NEWER")]
		private void ChangeAdaptivePerformanceDrawDistances()
		{
		}

		// Token: 0x04000417 RID: 1047
		[SerializeField]
		private DecalSettings m_Settings = new DecalSettings();

		// Token: 0x04000418 RID: 1048
		[SerializeField]
		[HideInInspector]
		[Reload("Shaders/Utils/CopyDepth.shader", ReloadAttribute.Package.Root)]
		private Shader m_CopyDepthPS;

		// Token: 0x04000419 RID: 1049
		[SerializeField]
		[HideInInspector]
		[Reload("Runtime/Decal/DBuffer/DBufferClear.shader", ReloadAttribute.Package.Root)]
		private Shader m_DBufferClear;

		// Token: 0x0400041A RID: 1050
		private DecalTechnique m_Technique;

		// Token: 0x0400041B RID: 1051
		private DBufferSettings m_DBufferSettings;

		// Token: 0x0400041C RID: 1052
		private DecalScreenSpaceSettings m_ScreenSpaceSettings;

		// Token: 0x0400041D RID: 1053
		private bool m_RecreateSystems;

		// Token: 0x0400041E RID: 1054
		private CopyDepthPass m_CopyDepthPass;

		// Token: 0x0400041F RID: 1055
		private DecalPreviewPass m_DecalPreviewPass;

		// Token: 0x04000420 RID: 1056
		private Material m_CopyDepthMaterial;

		// Token: 0x04000421 RID: 1057
		private DecalEntityManager m_DecalEntityManager;

		// Token: 0x04000422 RID: 1058
		private DecalUpdateCachedSystem m_DecalUpdateCachedSystem;

		// Token: 0x04000423 RID: 1059
		private DecalUpdateCullingGroupSystem m_DecalUpdateCullingGroupSystem;

		// Token: 0x04000424 RID: 1060
		private DecalUpdateCulledSystem m_DecalUpdateCulledSystem;

		// Token: 0x04000425 RID: 1061
		private DecalCreateDrawCallSystem m_DecalCreateDrawCallSystem;

		// Token: 0x04000426 RID: 1062
		private DecalDrawErrorSystem m_DrawErrorSystem;

		// Token: 0x04000427 RID: 1063
		private DBufferRenderPass m_DBufferRenderPass;

		// Token: 0x04000428 RID: 1064
		private DecalForwardEmissivePass m_ForwardEmissivePass;

		// Token: 0x04000429 RID: 1065
		private DecalDrawDBufferSystem m_DecalDrawDBufferSystem;

		// Token: 0x0400042A RID: 1066
		private DecalDrawFowardEmissiveSystem m_DecalDrawForwardEmissiveSystem;

		// Token: 0x0400042B RID: 1067
		private Material m_DBufferClearMaterial;

		// Token: 0x0400042C RID: 1068
		private DecalScreenSpaceRenderPass m_ScreenSpaceDecalRenderPass;

		// Token: 0x0400042D RID: 1069
		private DecalDrawScreenSpaceSystem m_DecalDrawScreenSpaceSystem;

		// Token: 0x0400042E RID: 1070
		private DecalSkipCulledSystem m_DecalSkipCulledSystem;

		// Token: 0x0400042F RID: 1071
		private DecalGBufferRenderPass m_GBufferRenderPass;

		// Token: 0x04000430 RID: 1072
		private DecalDrawGBufferSystem m_DrawGBufferSystem;

		// Token: 0x04000431 RID: 1073
		private DeferredLights m_DeferredLights;
	}
}
