using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000112 RID: 274
	internal sealed class MotionVectorRenderPass : ScriptableRenderPass
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x00035DF8 File Offset: 0x00033FF8
		internal MotionVectorRenderPass(Material cameraMaterial, Material objectMaterial)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
			this.m_CameraMaterial = cameraMaterial;
			this.m_ObjectMaterial = objectMaterial;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00035E26 File Offset: 0x00034026
		internal void Setup(PreviousFrameData frameData)
		{
			this.m_MotionData = frameData;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00035E30 File Offset: 0x00034030
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			RenderTextureDescriptor renderTextureDescriptor = cameraTextureDescriptor;
			renderTextureDescriptor.graphicsFormat = GraphicsFormat.R16G16_SFloat;
			this.m_MotionVectorHandle.Init("_MotionVectorTexture");
			cmd.GetTemporaryRT(this.m_MotionVectorHandle.id, renderTextureDescriptor, FilterMode.Point);
			base.ConfigureTarget(this.m_MotionVectorHandle.Identifier(), this.m_MotionVectorHandle.Identifier());
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00035E88 File Offset: 0x00034088
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_CameraMaterial == null || this.m_ObjectMaterial == null)
			{
				return;
			}
			ref CameraData ptr = ref renderingData.cameraData;
			Camera camera = ptr.camera;
			MotionVectorsPersistentData motionVectorsPersistentData = null;
			UniversalAdditionalCameraData universalAdditionalCameraData;
			if (camera.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData))
			{
				motionVectorsPersistentData = universalAdditionalCameraData.motionVectorsPersistentData;
			}
			if (motionVectorsPersistentData == null)
			{
				return;
			}
			if (camera.cameraType == CameraType.Preview)
			{
				return;
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				motionVectorsPersistentData.GetXRMultiPassId(ref ptr);
				this.ExecuteCommand(context, commandBuffer);
				if (ptr.xr.enabled && ptr.xr.singlePassEnabled)
				{
					this.m_CameraMaterial.SetMatrixArray("_PrevViewProjMStereo", this.m_MotionData.previousViewProjectionMatrixStereo);
					this.m_ObjectMaterial.SetMatrixArray("_PrevViewProjMStereo", this.m_MotionData.previousViewProjectionMatrixStereo);
				}
				else
				{
					Shader.SetGlobalMatrix("_PrevViewProjMatrix", this.m_MotionData.previousViewProjectionMatrix);
				}
				camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
				this.DrawCameraMotionVectors(context, commandBuffer, camera);
				this.DrawObjectMotionVectors(context, ref renderingData, camera);
			}
			this.ExecuteCommand(context, commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00035FC0 File Offset: 0x000341C0
		private DrawingSettings GetDrawingSettings(ref RenderingData renderingData)
		{
			Camera camera = renderingData.cameraData.camera;
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = SortingCriteria.CommonOpaque
			};
			DrawingSettings drawingSettings = new DrawingSettings(ShaderTagId.none, sortingSettings)
			{
				perObjectData = PerObjectData.MotionVectors,
				enableDynamicBatching = renderingData.supportsDynamicBatching,
				enableInstancing = true
			};
			for (int i = 0; i < MotionVectorRenderPass.s_ShaderTags.Length; i++)
			{
				drawingSettings.SetShaderPassName(i, new ShaderTagId(MotionVectorRenderPass.s_ShaderTags[i]));
			}
			drawingSettings.fallbackMaterial = this.m_ObjectMaterial;
			return drawingSettings;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00036054 File Offset: 0x00034254
		private void DrawCameraMotionVectors(ScriptableRenderContext context, CommandBuffer cmd, Camera camera)
		{
			cmd.DrawProcedural(Matrix4x4.identity, this.m_CameraMaterial, 0, MeshTopology.Triangles, 3, 1);
			this.ExecuteCommand(context, cmd);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00036074 File Offset: 0x00034274
		private void DrawObjectMotionVectors(ScriptableRenderContext context, ref RenderingData renderingData, Camera camera)
		{
			DrawingSettings drawingSettings = this.GetDrawingSettings(ref renderingData);
			FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), camera.cullingMask, uint.MaxValue, 0);
			RenderStateBlock renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000360BD File Offset: 0x000342BD
		public override void FrameCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.m_MotionVectorHandle != RenderTargetHandle.CameraTarget)
			{
				cmd.ReleaseTemporaryRT(this.m_MotionVectorHandle.id);
				this.m_MotionVectorHandle = RenderTargetHandle.CameraTarget;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000360FB File Offset: 0x000342FB
		private void ExecuteCommand(ScriptableRenderContext context, CommandBuffer cmd)
		{
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		// Token: 0x040007C6 RID: 1990
		private const string kPreviousViewProjectionMatrix = "_PrevViewProjMatrix";

		// Token: 0x040007C7 RID: 1991
		private const string kPreviousViewProjectionMatrixStero = "_PrevViewProjMStereo";

		// Token: 0x040007C8 RID: 1992
		private const string kMotionVectorTexture = "_MotionVectorTexture";

		// Token: 0x040007C9 RID: 1993
		private const GraphicsFormat m_TargetFormat = GraphicsFormat.R16G16_SFloat;

		// Token: 0x040007CA RID: 1994
		private static readonly string[] s_ShaderTags = new string[] { "MotionVectors" };

		// Token: 0x040007CB RID: 1995
		private RenderTargetHandle m_MotionVectorHandle;

		// Token: 0x040007CC RID: 1996
		private readonly Material m_CameraMaterial;

		// Token: 0x040007CD RID: 1997
		private readonly Material m_ObjectMaterial;

		// Token: 0x040007CE RID: 1998
		private PreviousFrameData m_MotionData;

		// Token: 0x040007CF RID: 1999
		private ProfilingSampler m_ProfilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.MotionVectors);
	}
}
