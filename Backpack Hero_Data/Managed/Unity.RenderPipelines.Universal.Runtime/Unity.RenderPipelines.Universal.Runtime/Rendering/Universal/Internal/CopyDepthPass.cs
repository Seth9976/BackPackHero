using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200010A RID: 266
	public class CopyDepthPass : ScriptableRenderPass
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00033D68 File Offset: 0x00031F68
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00033D70 File Offset: 0x00031F70
		private RenderTargetHandle source { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00033D79 File Offset: 0x00031F79
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00033D81 File Offset: 0x00031F81
		private RenderTargetHandle destination { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00033D8A File Offset: 0x00031F8A
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00033D92 File Offset: 0x00031F92
		internal bool AllocateRT { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00033D9B File Offset: 0x00031F9B
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00033DA3 File Offset: 0x00031FA3
		internal int MssaSamples { get; set; }

		// Token: 0x06000835 RID: 2101 RVA: 0x00033DAC File Offset: 0x00031FAC
		public CopyDepthPass(RenderPassEvent evt, Material copyDepthMaterial)
		{
			base.profilingSampler = new ProfilingSampler("CopyDepthPass");
			this.AllocateRT = true;
			this.m_CopyDepthMaterial = copyDepthMaterial;
			base.renderPassEvent = evt;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00033DD9 File Offset: 0x00031FD9
		public void Setup(RenderTargetHandle source, RenderTargetHandle destination)
		{
			this.source = source;
			this.destination = destination;
			this.AllocateRT = !destination.HasInternalRenderTargetId();
			this.MssaSamples = -1;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00033E00 File Offset: 0x00032000
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
			cameraTargetDescriptor.colorFormat = RenderTextureFormat.Depth;
			cameraTargetDescriptor.depthBufferBits = 32;
			cameraTargetDescriptor.msaaSamples = 1;
			if (this.AllocateRT)
			{
				cmd.GetTemporaryRT(this.destination.id, cameraTargetDescriptor, FilterMode.Point);
			}
			base.ConfigureTarget(new RenderTargetIdentifier(this.destination.Identifier(), 0, CubemapFace.Unknown, -1), cameraTargetDescriptor.depthStencilFormat, cameraTargetDescriptor.width, cameraTargetDescriptor.height, cameraTargetDescriptor.msaaSamples, true);
			base.ConfigureClear(ClearFlag.None, Color.black);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00033E98 File Offset: 0x00032098
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_CopyDepthMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_CopyDepthMaterial,
					base.GetType().Name
				});
				return;
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.CopyDepth)))
			{
				int num;
				if (this.MssaSamples == -1)
				{
					RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
					num = cameraTargetDescriptor.msaaSamples;
				}
				else
				{
					num = this.MssaSamples;
				}
				if (SystemInfo.supportsMultisampledTextures == 0)
				{
					num = 1;
				}
				CameraData cameraData = renderingData.cameraData;
				if (num != 2)
				{
					if (num != 4)
					{
						if (num == 8)
						{
							commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_2");
							commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_4");
							commandBuffer.EnableShaderKeyword("_DEPTH_MSAA_8");
						}
						else
						{
							commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_2");
							commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_4");
							commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_8");
						}
					}
					else
					{
						commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_2");
						commandBuffer.EnableShaderKeyword("_DEPTH_MSAA_4");
						commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_8");
					}
				}
				else
				{
					commandBuffer.EnableShaderKeyword("_DEPTH_MSAA_2");
					commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_4");
					commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_8");
				}
				commandBuffer.SetGlobalTexture("_CameraDepthAttachment", this.source.Identifier());
				if (renderingData.cameraData.xr.enabled)
				{
					float num2 = ((this.destination.Identifier() == cameraData.xr.renderTarget && !cameraData.xr.renderTargetIsRenderTexture && SystemInfo.graphicsUVStartsAtTop) ? (-1f) : 1f);
					Vector4 vector = ((num2 < 0f) ? new Vector4(num2, 1f, -1f, 1f) : new Vector4(num2, 0f, 1f, 1f));
					commandBuffer.SetGlobalVector(ShaderPropertyId.scaleBiasRt, vector);
					commandBuffer.DrawProcedural(Matrix4x4.identity, this.m_CopyDepthMaterial, 0, MeshTopology.Quads, 4);
				}
				else
				{
					bool flag = cameraData.cameraType == CameraType.Game && this.destination == RenderTargetHandle.CameraTarget;
					float num3 = ((cameraData.IsCameraProjectionMatrixFlipped() && !flag) ? (-1f) : 1f);
					Vector4 vector2 = ((num3 < 0f) ? new Vector4(num3, 1f, -1f, 1f) : new Vector4(num3, 0f, 1f, 1f));
					commandBuffer.SetGlobalVector(ShaderPropertyId.scaleBiasRt, vector2);
					if (flag)
					{
						commandBuffer.SetViewport(cameraData.pixelRect);
					}
					commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_CopyDepthMaterial);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00034180 File Offset: 0x00032380
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.AllocateRT)
			{
				cmd.ReleaseTemporaryRT(this.destination.id);
			}
			this.destination = RenderTargetHandle.CameraTarget;
		}

		// Token: 0x04000792 RID: 1938
		private Material m_CopyDepthMaterial;
	}
}
