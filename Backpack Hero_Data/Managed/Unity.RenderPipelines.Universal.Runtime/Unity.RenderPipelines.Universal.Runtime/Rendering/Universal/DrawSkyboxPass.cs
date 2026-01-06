using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009E RID: 158
	public class DrawSkyboxPass : ScriptableRenderPass
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001D9D1 File Offset: 0x0001BBD1
		public DrawSkyboxPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("DrawSkyboxPass");
			base.renderPassEvent = evt;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CameraData cameraData = renderingData.cameraData;
			Camera camera = cameraData.camera;
			DebugHandler activeDebugHandler = base.GetActiveDebugHandler(renderingData);
			if (activeDebugHandler != null && activeDebugHandler.IsScreenClearNeeded)
			{
				return;
			}
			if (!cameraData.xr.enabled)
			{
				context.DrawSkybox(camera);
				return;
			}
			if (cameraData.xr.singlePassEnabled)
			{
				camera.SetStereoProjectionMatrix(Camera.StereoscopicEye.Left, cameraData.GetProjectionMatrix(0));
				camera.SetStereoViewMatrix(Camera.StereoscopicEye.Left, cameraData.GetViewMatrix(0));
				camera.SetStereoProjectionMatrix(Camera.StereoscopicEye.Right, cameraData.GetProjectionMatrix(1));
				camera.SetStereoViewMatrix(Camera.StereoscopicEye.Right, cameraData.GetViewMatrix(1));
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				commandBuffer.SetSinglePassStereo(SystemInfo.supportsMultiview ? SinglePassStereoMode.Multiview : SinglePassStereoMode.Instancing);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				context.DrawSkybox(camera);
				commandBuffer.SetSinglePassStereo(SinglePassStereoMode.None);
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
				camera.ResetStereoProjectionMatrices();
				camera.ResetStereoViewMatrices();
				return;
			}
			camera.projectionMatrix = cameraData.GetProjectionMatrix(0);
			camera.worldToCameraMatrix = cameraData.GetViewMatrix(0);
			context.DrawSkybox(camera);
			context.Submit();
			camera.ResetProjectionMatrix();
			camera.ResetWorldToCameraMatrix();
		}
	}
}
