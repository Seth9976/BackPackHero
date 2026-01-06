using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005C RID: 92
	internal class DBufferRenderPass : ScriptableRenderPass
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000146AE File Offset: 0x000128AE
		// (set) Token: 0x06000353 RID: 851 RVA: 0x000146B6 File Offset: 0x000128B6
		internal DeferredLights deferredLights { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000146BF File Offset: 0x000128BF
		private bool isDeferred
		{
			get
			{
				return this.deferredLights != null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000146CA File Offset: 0x000128CA
		// (set) Token: 0x06000356 RID: 854 RVA: 0x000146D2 File Offset: 0x000128D2
		internal RenderTargetIdentifier[] dBufferColorIndentifiers { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000146DB File Offset: 0x000128DB
		// (set) Token: 0x06000358 RID: 856 RVA: 0x000146E3 File Offset: 0x000128E3
		internal RenderTargetIdentifier dBufferDepthIndentifier { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000146EC File Offset: 0x000128EC
		// (set) Token: 0x0600035A RID: 858 RVA: 0x000146F4 File Offset: 0x000128F4
		internal RenderTargetIdentifier cameraDepthTextureIndentifier { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000146FD File Offset: 0x000128FD
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00014705 File Offset: 0x00012905
		internal RenderTargetIdentifier cameraDepthAttachmentIndentifier { get; private set; }

		// Token: 0x0600035D RID: 861 RVA: 0x00014710 File Offset: 0x00012910
		public DBufferRenderPass(Material dBufferClear, DBufferSettings settings, DecalDrawDBufferSystem drawSystem)
		{
			base.renderPassEvent = (RenderPassEvent)201;
			base.ConfigureInput(ScriptableRenderPassInput.Normal);
			this.m_DrawSystem = drawSystem;
			this.m_Settings = settings;
			this.m_DBufferClear = dBufferClear;
			this.m_ProfilingSampler = new ProfilingSampler("DBuffer Render");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			this.m_ShaderTagIdList.Add(new ShaderTagId("DBufferMesh"));
			int num = (int)(settings.surfaceData + 1);
			this.dBufferColorIndentifiers = new RenderTargetIdentifier[num];
			for (int i = 0; i < num; i++)
			{
				this.dBufferColorIndentifiers[i] = new RenderTargetIdentifier(DBufferRenderPass.s_DBufferNames[i]);
			}
			this.m_DBufferCount = num;
			this.dBufferDepthIndentifier = new RenderTargetIdentifier(DBufferRenderPass.s_DBufferDepthName);
			this.cameraDepthTextureIndentifier = new RenderTargetIdentifier("_CameraDepthTexture");
			this.cameraDepthAttachmentIndentifier = new RenderTargetIdentifier("_CameraDepthAttachment");
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00014804 File Offset: 0x00012A04
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.graphicsFormat = ((QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormat.R8G8B8A8_SRGB : GraphicsFormat.R8G8B8A8_UNorm);
			cameraTargetDescriptor.depthBufferBits = 0;
			cameraTargetDescriptor.msaaSamples = 1;
			cmd.GetTemporaryRT(Shader.PropertyToID(DBufferRenderPass.s_DBufferNames[0]), cameraTargetDescriptor);
			if (this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormal || this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
			{
				RenderTextureDescriptor cameraTargetDescriptor2 = renderingData.cameraData.cameraTargetDescriptor;
				cameraTargetDescriptor2.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
				cameraTargetDescriptor2.depthBufferBits = 0;
				cameraTargetDescriptor2.msaaSamples = 1;
				cmd.GetTemporaryRT(Shader.PropertyToID(DBufferRenderPass.s_DBufferNames[1]), cameraTargetDescriptor2);
			}
			if (this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
			{
				RenderTextureDescriptor cameraTargetDescriptor3 = renderingData.cameraData.cameraTargetDescriptor;
				cameraTargetDescriptor3.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
				cameraTargetDescriptor3.depthBufferBits = 0;
				cameraTargetDescriptor3.msaaSamples = 1;
				cmd.GetTemporaryRT(Shader.PropertyToID(DBufferRenderPass.s_DBufferNames[2]), cameraTargetDescriptor3);
			}
			RenderTargetIdentifier renderTargetIdentifier;
			if (!this.isDeferred)
			{
				RenderTextureDescriptor cameraTargetDescriptor4 = renderingData.cameraData.cameraTargetDescriptor;
				cameraTargetDescriptor4.graphicsFormat = GraphicsFormat.None;
				cameraTargetDescriptor4.depthStencilFormat = renderingData.cameraData.cameraTargetDescriptor.depthStencilFormat;
				cameraTargetDescriptor4.msaaSamples = 1;
				cmd.GetTemporaryRT(Shader.PropertyToID(DBufferRenderPass.s_DBufferDepthName), cameraTargetDescriptor4);
				renderTargetIdentifier = this.dBufferDepthIndentifier;
			}
			else
			{
				renderTargetIdentifier = this.deferredLights.DepthAttachmentIdentifier;
			}
			base.ConfigureTarget(this.dBufferColorIndentifiers, renderTargetIdentifier);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001495C File Offset: 0x00012B5C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria defaultOpaqueSortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, defaultOpaqueSortFlags);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				if (this.isDeferred)
				{
					commandBuffer.SetGlobalTexture("_CameraNormalsTexture", this.deferredLights.GbufferAttachmentIdentifiers[this.deferredLights.GBufferNormalSmoothnessIndex]);
				}
				CoreUtils.SetKeyword(commandBuffer, "_DBUFFER_MRT1", this.m_Settings.surfaceData == DecalSurfaceData.Albedo);
				CoreUtils.SetKeyword(commandBuffer, "_DBUFFER_MRT2", this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormal);
				CoreUtils.SetKeyword(commandBuffer, "_DBUFFER_MRT3", this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS);
				this.ClearDBuffers(commandBuffer, in renderingData.cameraData);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				this.m_DrawSystem.Execute(commandBuffer);
				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00014A8C File Offset: 0x00012C8C
		private void ClearDBuffers(CommandBuffer cmd, in CameraData cameraData)
		{
			string text = "Clear";
			cmd.BeginSample(text);
			Vector4 vector = new Vector4(1f, 1f, 0f, 0f);
			cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
			if (cameraData.xr.enabled)
			{
				cmd.DrawProcedural(Matrix4x4.identity, this.m_DBufferClear, 0, MeshTopology.Quads, 4, 1, null);
			}
			else
			{
				cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
				cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_DBufferClear, 0, 0);
				cmd.SetViewProjectionMatrices(cameraData.camera.worldToCameraMatrix, cameraData.camera.projectionMatrix);
			}
			cmd.EndSample(text);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00014B40 File Offset: 0x00012D40
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			CoreUtils.SetKeyword(cmd, "_DBUFFER_MRT1", false);
			CoreUtils.SetKeyword(cmd, "_DBUFFER_MRT2", false);
			CoreUtils.SetKeyword(cmd, "_DBUFFER_MRT3", false);
			for (int i = 0; i < this.m_DBufferCount; i++)
			{
				cmd.ReleaseTemporaryRT(Shader.PropertyToID(DBufferRenderPass.s_DBufferNames[i]));
			}
			if (!this.isDeferred)
			{
				cmd.ReleaseTemporaryRT(Shader.PropertyToID(DBufferRenderPass.s_DBufferDepthName));
			}
		}

		// Token: 0x0400026A RID: 618
		private static string[] s_DBufferNames = new string[] { "_DBufferTexture0", "_DBufferTexture1", "_DBufferTexture2", "_DBufferTexture3" };

		// Token: 0x0400026B RID: 619
		private static string s_DBufferDepthName = "DBufferDepth";

		// Token: 0x0400026C RID: 620
		private DecalDrawDBufferSystem m_DrawSystem;

		// Token: 0x0400026D RID: 621
		private DBufferSettings m_Settings;

		// Token: 0x0400026E RID: 622
		private Material m_DBufferClear;

		// Token: 0x0400026F RID: 623
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000270 RID: 624
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x04000271 RID: 625
		private int m_DBufferCount;

		// Token: 0x04000272 RID: 626
		private ProfilingSampler m_ProfilingSampler;
	}
}
