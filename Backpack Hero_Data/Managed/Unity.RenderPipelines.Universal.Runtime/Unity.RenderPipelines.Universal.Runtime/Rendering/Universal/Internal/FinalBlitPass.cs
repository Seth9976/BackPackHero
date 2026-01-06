using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200010F RID: 271
	public class FinalBlitPass : ScriptableRenderPass
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x00034CD1 File Offset: 0x00032ED1
		public FinalBlitPass(RenderPassEvent evt, Material blitMaterial)
		{
			base.profilingSampler = new ProfilingSampler("FinalBlitPass");
			base.useNativeRenderPass = false;
			this.m_BlitMaterial = blitMaterial;
			base.renderPassEvent = evt;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00034CFE File Offset: 0x00032EFE
		public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle colorHandle)
		{
			this.m_Source = colorHandle.id;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00034D14 File Offset: 0x00032F14
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_BlitMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_BlitMaterial,
					base.GetType().Name
				});
				return;
			}
			ref CameraData ptr = ref renderingData.cameraData;
			RenderTargetIdentifier renderTargetIdentifier = ((ptr.targetTexture != null) ? new RenderTargetIdentifier(ptr.targetTexture) : BuiltinRenderTextureType.CameraTarget);
			bool isSceneViewCamera = ptr.isSceneViewCamera;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			if (this.m_Source == ptr.renderer.GetCameraColorFrontBuffer(commandBuffer))
			{
				this.m_Source = renderingData.cameraData.renderer.cameraColorTarget;
			}
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.FinalBlit)))
			{
				CoreUtils.SetKeyword(commandBuffer, "_LINEAR_TO_SRGB_CONVERSION", ptr.requireSrgbConversion);
				commandBuffer.SetGlobalTexture(ShaderPropertyId.sourceTex, this.m_Source);
				if (ptr.xr.enabled)
				{
					int num = (ptr.xr.singlePassEnabled ? (-1) : ptr.xr.GetTextureArraySlice(0));
					renderTargetIdentifier = new RenderTargetIdentifier(ptr.xr.renderTarget, 0, CubemapFace.Unknown, num);
					CoreUtils.SetRenderTarget(commandBuffer, renderTargetIdentifier, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, ClearFlag.None, Color.black);
					commandBuffer.SetViewport(ptr.pixelRect);
					Vector4 vector = ((!ptr.xr.renderTargetIsRenderTexture && SystemInfo.graphicsUVStartsAtTop) ? new Vector4(1f, -1f, 0f, 1f) : new Vector4(1f, 1f, 0f, 0f));
					commandBuffer.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
					commandBuffer.DrawProcedural(Matrix4x4.identity, this.m_BlitMaterial, 0, MeshTopology.Quads, 4);
				}
				else if (isSceneViewCamera || ptr.isDefaultViewport)
				{
					commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					commandBuffer.Blit(this.m_Source, renderTargetIdentifier, this.m_BlitMaterial);
					ptr.renderer.ConfigureCameraTarget(renderTargetIdentifier, renderTargetIdentifier);
				}
				else
				{
					CoreUtils.SetRenderTarget(commandBuffer, renderTargetIdentifier, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, ClearFlag.None, Color.black);
					Camera camera = ptr.camera;
					commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
					commandBuffer.SetViewport(ptr.pixelRect);
					commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_BlitMaterial);
					commandBuffer.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
					ptr.renderer.ConfigureCameraTarget(renderTargetIdentifier, renderTargetIdentifier);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x040007AC RID: 1964
		private RenderTargetIdentifier m_Source;

		// Token: 0x040007AD RID: 1965
		private Material m_BlitMaterial;
	}
}
