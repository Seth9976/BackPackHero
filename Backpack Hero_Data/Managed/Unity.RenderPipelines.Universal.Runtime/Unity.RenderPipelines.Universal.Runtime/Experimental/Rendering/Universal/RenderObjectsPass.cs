using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experimental.Rendering.Universal
{
	// Token: 0x02000003 RID: 3
	public class RenderObjectsPass : ScriptableRenderPass
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000025AC File Offset: 0x000007AC
		public Material overrideMaterial { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025B5 File Offset: 0x000007B5
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000025BD File Offset: 0x000007BD
		public int overrideMaterialPassIndex { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x000025C6 File Offset: 0x000007C6
		public void SetDetphState(bool writeEnabled, CompareFunction function = CompareFunction.Less)
		{
			this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Depth;
			this.m_RenderStateBlock.depthState = new DepthState(writeEnabled, function);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025F0 File Offset: 0x000007F0
		public void SetStencilState(int reference, CompareFunction compareFunction, StencilOp passOp, StencilOp failOp, StencilOp zFailOp)
		{
			StencilState defaultValue = StencilState.defaultValue;
			defaultValue.enabled = true;
			defaultValue.SetCompareFunction(compareFunction);
			defaultValue.SetPassOperation(passOp);
			defaultValue.SetFailOperation(failOp);
			defaultValue.SetZFailOperation(zFailOp);
			this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Stencil;
			this.m_RenderStateBlock.stencilReference = reference;
			this.m_RenderStateBlock.stencilState = defaultValue;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002658 File Offset: 0x00000858
		public RenderObjectsPass(string profilerTag, RenderPassEvent renderPassEvent, string[] shaderTags, RenderQueueType renderQueueType, int layerMask, RenderObjects.CustomCameraSettings cameraSettings)
		{
			base.profilingSampler = new ProfilingSampler("RenderObjectsPass");
			this.m_ProfilerTag = profilerTag;
			this.m_ProfilingSampler = new ProfilingSampler(profilerTag);
			base.renderPassEvent = renderPassEvent;
			this.renderQueueType = renderQueueType;
			this.overrideMaterial = null;
			this.overrideMaterialPassIndex = 0;
			RenderQueueRange renderQueueRange = ((renderQueueType == RenderQueueType.Transparent) ? RenderQueueRange.transparent : RenderQueueRange.opaque);
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			if (shaderTags != null && shaderTags.Length != 0)
			{
				foreach (string text in shaderTags)
				{
					this.m_ShaderTagIdList.Add(new ShaderTagId(text));
				}
			}
			else
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("SRPDefaultUnlit"));
				this.m_ShaderTagIdList.Add(new ShaderTagId("UniversalForward"));
				this.m_ShaderTagIdList.Add(new ShaderTagId("UniversalForwardOnly"));
			}
			this.m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			this.m_CameraSettings = cameraSettings;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000275E File Offset: 0x0000095E
		internal RenderObjectsPass(URPProfileId profileId, RenderPassEvent renderPassEvent, string[] shaderTags, RenderQueueType renderQueueType, int layerMask, RenderObjects.CustomCameraSettings cameraSettings)
			: this(profileId.GetType().Name, renderPassEvent, shaderTags, renderQueueType, layerMask, cameraSettings)
		{
			this.m_ProfilingSampler = ProfilingSampler.Get<URPProfileId>(profileId);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000278C File Offset: 0x0000098C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			SortingCriteria sortingCriteria = ((this.renderQueueType == RenderQueueType.Transparent) ? SortingCriteria.CommonTransparent : renderingData.cameraData.defaultOpaqueSortFlags);
			DrawingSettings drawingSettings = base.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, sortingCriteria);
			drawingSettings.overrideMaterial = this.overrideMaterial;
			drawingSettings.overrideMaterialPassIndex = this.overrideMaterialPassIndex;
			ref CameraData ptr = ref renderingData.cameraData;
			Camera camera = ptr.camera;
			Rect pixelRect = renderingData.cameraData.pixelRect;
			float num = pixelRect.width / pixelRect.height;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, this.m_ProfilingSampler))
			{
				if (this.m_CameraSettings.overrideCamera)
				{
					if (ptr.xr.enabled)
					{
						Debug.LogWarning("RenderObjects pass is configured to override camera matrices. While rendering in stereo camera matrices cannot be overridden.");
					}
					else
					{
						Matrix4x4 matrix4x = Matrix4x4.Perspective(this.m_CameraSettings.cameraFieldOfView, num, camera.nearClipPlane, camera.farClipPlane);
						matrix4x = GL.GetGPUProjectionMatrix(matrix4x, ptr.IsCameraProjectionMatrixFlipped());
						Matrix4x4 viewMatrix = ptr.GetViewMatrix(0);
						Vector4 column = viewMatrix.GetColumn(3);
						viewMatrix.SetColumn(3, column + this.m_CameraSettings.offset);
						RenderingUtils.SetViewAndProjectionMatrices(commandBuffer, viewMatrix, matrix4x, false);
					}
				}
				DebugHandler activeDebugHandler = base.GetActiveDebugHandler(renderingData);
				if (activeDebugHandler != null)
				{
					activeDebugHandler.DrawWithDebugRenderState(context, commandBuffer, ref renderingData, ref drawingSettings, ref this.m_FilteringSettings, ref this.m_RenderStateBlock, delegate(ScriptableRenderContext ctx, ref RenderingData data, ref DrawingSettings ds, ref FilteringSettings fs, ref RenderStateBlock rsb)
					{
						ctx.DrawRenderers(data.cullResults, ref ds, ref fs, ref rsb);
					});
				}
				else
				{
					context.ExecuteCommandBuffer(commandBuffer);
					commandBuffer.Clear();
					context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings, ref this.m_RenderStateBlock);
				}
				if (this.m_CameraSettings.overrideCamera && this.m_CameraSettings.restoreCamera && !ptr.xr.enabled)
				{
					RenderingUtils.SetViewAndProjectionMatrices(commandBuffer, ptr.GetViewMatrix(0), ptr.GetGPUProjectionMatrix(0), false);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x04000009 RID: 9
		private RenderQueueType renderQueueType;

		// Token: 0x0400000A RID: 10
		private FilteringSettings m_FilteringSettings;

		// Token: 0x0400000B RID: 11
		private RenderObjects.CustomCameraSettings m_CameraSettings;

		// Token: 0x0400000C RID: 12
		private string m_ProfilerTag;

		// Token: 0x0400000D RID: 13
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x04000010 RID: 16
		private List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();

		// Token: 0x04000011 RID: 17
		private RenderStateBlock m_RenderStateBlock;
	}
}
