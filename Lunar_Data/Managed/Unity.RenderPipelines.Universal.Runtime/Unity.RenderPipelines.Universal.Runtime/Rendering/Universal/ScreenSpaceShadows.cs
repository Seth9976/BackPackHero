using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B4 RID: 180
	[DisallowMultipleRendererFeature(null)]
	[Tooltip("Screen Space Shadows")]
	internal class ScreenSpaceShadows : ScriptableRendererFeature
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		public override void Create()
		{
			if (this.m_SSShadowsPass == null)
			{
				this.m_SSShadowsPass = new ScreenSpaceShadows.ScreenSpaceShadowsPass();
			}
			if (this.m_SSShadowsPostPass == null)
			{
				this.m_SSShadowsPostPass = new ScreenSpaceShadows.ScreenSpaceShadowsPostPass();
			}
			this.LoadMaterial();
			this.m_SSShadowsPass.renderPassEvent = RenderPassEvent.AfterRenderingGbuffer;
			this.m_SSShadowsPostPass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001EA08 File Offset: 0x0001CC08
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (!this.LoadMaterial())
			{
				Debug.LogErrorFormat("{0}.AddRenderPasses(): Missing material. {1} render pass will not be added. Check for missing reference in the renderer resources.", new object[]
				{
					base.GetType().Name,
					base.name
				});
				return;
			}
			if (renderingData.shadowData.supportsMainLightShadows && renderingData.lightData.mainLightIndex != -1 && this.m_SSShadowsPass.Setup(this.m_Settings, this.m_Material))
			{
				bool flag = renderer is UniversalRenderer && ((UniversalRenderer)renderer).renderingMode == RenderingMode.Deferred;
				this.m_SSShadowsPass.renderPassEvent = (flag ? RenderPassEvent.AfterRenderingGbuffer : RenderPassEvent.AfterRenderingPrePasses);
				renderer.EnqueuePass(this.m_SSShadowsPass);
				renderer.EnqueuePass(this.m_SSShadowsPostPass);
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
		protected override void Dispose(bool disposing)
		{
			CoreUtils.Destroy(this.m_Material);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		private bool LoadMaterial()
		{
			if (this.m_Material != null)
			{
				return true;
			}
			if (this.m_Shader == null)
			{
				this.m_Shader = Shader.Find("Hidden/Universal Render Pipeline/ScreenSpaceShadows");
				if (this.m_Shader == null)
				{
					return false;
				}
			}
			this.m_Material = CoreUtils.CreateEngineMaterial(this.m_Shader);
			return this.m_Material != null;
		}

		// Token: 0x04000446 RID: 1094
		[SerializeField]
		[HideInInspector]
		private Shader m_Shader;

		// Token: 0x04000447 RID: 1095
		[SerializeField]
		private ScreenSpaceShadowsSettings m_Settings = new ScreenSpaceShadowsSettings();

		// Token: 0x04000448 RID: 1096
		private Material m_Material;

		// Token: 0x04000449 RID: 1097
		private ScreenSpaceShadows.ScreenSpaceShadowsPass m_SSShadowsPass;

		// Token: 0x0400044A RID: 1098
		private ScreenSpaceShadows.ScreenSpaceShadowsPostPass m_SSShadowsPostPass;

		// Token: 0x0400044B RID: 1099
		private const string k_ShaderName = "Hidden/Universal Render Pipeline/ScreenSpaceShadows";

		// Token: 0x0200017F RID: 383
		private class ScreenSpaceShadowsPass : ScriptableRenderPass
		{
			// Token: 0x060009D5 RID: 2517 RVA: 0x000417F5 File Offset: 0x0003F9F5
			internal ScreenSpaceShadowsPass()
			{
				this.m_CurrentSettings = new ScreenSpaceShadowsSettings();
				this.m_RenderTarget.Init("_ScreenSpaceShadowmapTexture");
			}

			// Token: 0x060009D6 RID: 2518 RVA: 0x00041818 File Offset: 0x0003FA18
			internal bool Setup(ScreenSpaceShadowsSettings featureSettings, Material material)
			{
				this.m_CurrentSettings = featureSettings;
				this.m_Material = material;
				base.ConfigureInput(ScriptableRenderPassInput.Depth);
				return this.m_Material != null;
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x0004183C File Offset: 0x0003FA3C
			public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
			{
				this.m_RenderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				this.m_RenderTextureDescriptor.depthBufferBits = 0;
				this.m_RenderTextureDescriptor.msaaSamples = 1;
				this.m_RenderTextureDescriptor.graphicsFormat = (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R8_UNorm, FormatUsage.Blend) ? GraphicsFormat.R8_UNorm : GraphicsFormat.B8G8R8A8_UNorm);
				cmd.GetTemporaryRT(this.m_RenderTarget.id, this.m_RenderTextureDescriptor, FilterMode.Point);
				RenderTargetIdentifier renderTargetIdentifier = this.m_RenderTarget.Identifier();
				base.ConfigureTarget(renderTargetIdentifier);
				base.ConfigureClear(ClearFlag.None, Color.white);
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x000418C4 File Offset: 0x0003FAC4
			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				if (this.m_Material == null)
				{
					Debug.LogErrorFormat("{0}.Execute(): Missing material. ScreenSpaceShadows pass will not execute. Check for missing reference in the renderer resources.", new object[] { base.GetType().Name });
					return;
				}
				Camera camera = renderingData.cameraData.camera;
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, ScreenSpaceShadows.ScreenSpaceShadowsPass.m_ProfilingSampler))
				{
					if (!renderingData.cameraData.xr.enabled)
					{
						commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
						commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_Material);
						commandBuffer.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
					}
					else
					{
						Vector4 vector = new Vector4(1f, 1f, 0f, 0f);
						Vector4 vector2 = new Vector4(1f, 1f, 0f, 0f);
						commandBuffer.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
						commandBuffer.SetGlobalVector(ShaderPropertyId.scaleBiasRt, vector2);
						RenderTargetIdentifier renderTargetIdentifier = this.m_RenderTarget.Identifier();
						commandBuffer.SetRenderTarget(new RenderTargetIdentifier(renderTargetIdentifier, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
						commandBuffer.DrawProcedural(Matrix4x4.identity, this.m_Material, 0, MeshTopology.Quads, 4, 1, null);
					}
					CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS", false);
					CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS_CASCADE", false);
					CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS_SCREEN", true);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x00041A44 File Offset: 0x0003FC44
			public override void OnCameraCleanup(CommandBuffer cmd)
			{
				if (cmd == null)
				{
					throw new ArgumentNullException("cmd");
				}
				cmd.ReleaseTemporaryRT(this.m_RenderTarget.id);
			}

			// Token: 0x040009D3 RID: 2515
			private static string m_ProfilerTag = "ScreenSpaceShadows";

			// Token: 0x040009D4 RID: 2516
			private static ProfilingSampler m_ProfilingSampler = new ProfilingSampler(ScreenSpaceShadows.ScreenSpaceShadowsPass.m_ProfilerTag);

			// Token: 0x040009D5 RID: 2517
			private Material m_Material;

			// Token: 0x040009D6 RID: 2518
			private ScreenSpaceShadowsSettings m_CurrentSettings;

			// Token: 0x040009D7 RID: 2519
			private RenderTextureDescriptor m_RenderTextureDescriptor;

			// Token: 0x040009D8 RID: 2520
			private RenderTargetHandle m_RenderTarget;

			// Token: 0x040009D9 RID: 2521
			private const string k_SSShadowsTextureName = "_ScreenSpaceShadowmapTexture";
		}

		// Token: 0x02000180 RID: 384
		private class ScreenSpaceShadowsPostPass : ScriptableRenderPass
		{
			// Token: 0x060009DB RID: 2523 RVA: 0x00041A80 File Offset: 0x0003FC80
			public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
			{
				base.ConfigureTarget(BuiltinRenderTextureType.CurrentActive);
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x00041A90 File Offset: 0x0003FC90
			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, ScreenSpaceShadows.ScreenSpaceShadowsPostPass.m_ProfilingSampler))
				{
					int mainLightShadowCascadesCount = renderingData.shadowData.mainLightShadowCascadesCount;
					bool supportsMainLightShadows = renderingData.shadowData.supportsMainLightShadows;
					bool flag = supportsMainLightShadows && mainLightShadowCascadesCount == 1;
					bool flag2 = supportsMainLightShadows && mainLightShadowCascadesCount > 1;
					CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS_SCREEN", false);
					CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS", flag);
					CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS_CASCADE", flag2);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}

			// Token: 0x040009DA RID: 2522
			private static string m_ProfilerTag = "ScreenSpaceShadows Post";

			// Token: 0x040009DB RID: 2523
			private static ProfilingSampler m_ProfilingSampler = new ProfilingSampler(ScreenSpaceShadows.ScreenSpaceShadowsPostPass.m_ProfilerTag);
		}
	}
}
