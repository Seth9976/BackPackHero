using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002A RID: 42
	internal class Render2DLightingPass : ScriptableRenderPass, IRenderPass2D
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000D845 File Offset: 0x0000BA45
		public Render2DLightingPass(Renderer2DData rendererData, Material blitMaterial, Material samplingMaterial)
		{
			this.m_Renderer2DData = rendererData;
			this.m_BlitMaterial = blitMaterial;
			this.m_SamplingMaterial = samplingMaterial;
			this.m_CameraSortingLayerBoundsIndex = this.GetCameraSortingLayerBoundsIndex();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000D86E File Offset: 0x0000BA6E
		internal void Setup(bool useDepth)
		{
			this.m_NeedsDepth = useDepth;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000D878 File Offset: 0x0000BA78
		private void GetTransparencySortingMode(Camera camera, ref SortingSettings sortingSettings)
		{
			TransparencySortMode transparencySortMode = this.m_Renderer2DData.transparencySortMode;
			if (transparencySortMode == TransparencySortMode.Default)
			{
				transparencySortMode = (camera.orthographic ? TransparencySortMode.Orthographic : TransparencySortMode.Perspective);
			}
			if (transparencySortMode == TransparencySortMode.Perspective)
			{
				sortingSettings.distanceMetric = DistanceMetric.Perspective;
				return;
			}
			if (transparencySortMode != TransparencySortMode.Orthographic)
			{
				sortingSettings.distanceMetric = DistanceMetric.CustomAxis;
				sortingSettings.customAxis = this.m_Renderer2DData.transparencySortAxis;
				return;
			}
			sortingSettings.distanceMetric = DistanceMetric.Orthographic;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		private void CopyCameraSortingLayerRenderTexture(ScriptableRenderContext context, RenderingData renderingData, RenderBufferStoreAction mainTargetStoreAction)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			commandBuffer.Clear();
			this.CreateCameraSortingLayerRenderTexture(renderingData, commandBuffer, this.m_Renderer2DData.cameraSortingLayerDownsamplingMethod);
			Material material = ((this.m_Renderer2DData.cameraSortingLayerDownsamplingMethod == Downsampling._4xBox) ? this.m_SamplingMaterial : this.m_BlitMaterial);
			RenderingUtils.Blit(commandBuffer, base.colorAttachment, this.m_Renderer2DData.cameraSortingLayerRenderTarget.id, material, 0, false, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			commandBuffer.SetRenderTarget(base.colorAttachment, RenderBufferLoadAction.Load, mainTargetStoreAction, base.depthAttachment, RenderBufferLoadAction.Load, mainTargetStoreAction);
			commandBuffer.SetGlobalTexture(Render2DLightingPass.k_CameraSortingLayerTextureID, this.m_Renderer2DData.cameraSortingLayerRenderTarget.id);
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000D98C File Offset: 0x0000BB8C
		private short GetCameraSortingLayerBoundsIndex()
		{
			SortingLayer[] cachedSortingLayer = Light2DManager.GetCachedSortingLayer();
			short num = 0;
			while ((int)num < cachedSortingLayer.Length)
			{
				if (cachedSortingLayer[(int)num].id == this.m_Renderer2DData.cameraSortingLayerTextureBound)
				{
					return (short)cachedSortingLayer[(int)num].value;
				}
				num += 1;
			}
			return short.MinValue;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private void DetermineWhenToResolve(int startIndex, int batchesDrawn, int batchCount, LayerBatch[] layerBatches, out int resolveDuringBatch, out bool resolveIsAfterCopy)
		{
			bool flag = false;
			List<Light2D> visibleLights = this.m_Renderer2DData.lightCullResult.visibleLights;
			for (int i = 0; i < visibleLights.Count; i++)
			{
				flag = visibleLights[i].renderVolumetricShadows;
				if (flag)
				{
					break;
				}
			}
			int num = -1;
			if (flag)
			{
				for (int j = startIndex + batchesDrawn - 1; j >= startIndex; j--)
				{
					if (layerBatches[j].lightStats.totalVolumetricUsage > 0)
					{
						num = j;
						break;
					}
				}
			}
			if (this.m_Renderer2DData.useCameraSortingLayerTexture)
			{
				short cameraSortingLayerBoundsIndex = this.GetCameraSortingLayerBoundsIndex();
				int num2 = -1;
				for (int k = startIndex; k < startIndex + batchesDrawn; k++)
				{
					LayerBatch layerBatch = layerBatches[k];
					if (cameraSortingLayerBoundsIndex >= layerBatch.layerRange.lowerBound && cameraSortingLayerBoundsIndex <= layerBatch.layerRange.upperBound)
					{
						num2 = k;
						break;
					}
				}
				resolveIsAfterCopy = num2 > num;
				resolveDuringBatch = (resolveIsAfterCopy ? num2 : num);
				return;
			}
			resolveDuringBatch = num;
			resolveIsAfterCopy = false;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000DACC File Offset: 0x0000BCCC
		private void Render(ScriptableRenderContext context, CommandBuffer cmd, ref RenderingData renderingData, ref FilteringSettings filterSettings, DrawingSettings drawSettings)
		{
			DebugHandler activeDebugHandler = base.GetActiveDebugHandler(renderingData);
			if (activeDebugHandler != null)
			{
				RenderStateBlock renderStateBlock = default(RenderStateBlock);
				activeDebugHandler.DrawWithDebugRenderState(context, cmd, ref renderingData, ref drawSettings, ref filterSettings, ref renderStateBlock, delegate(ScriptableRenderContext ctx, ref RenderingData data, ref DrawingSettings ds, ref FilteringSettings fs, ref RenderStateBlock rsb)
				{
					ctx.DrawRenderers(data.cullResults, ref ds, ref fs, ref rsb);
				});
				return;
			}
			context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettings);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000DB34 File Offset: 0x0000BD34
		private int DrawLayerBatches(LayerBatch[] layerBatches, int batchCount, int startIndex, CommandBuffer cmd, ScriptableRenderContext context, ref RenderingData renderingData, ref FilteringSettings filterSettings, ref DrawingSettings normalsDrawSettings, ref DrawingSettings drawSettings, ref RenderTextureDescriptor desc)
		{
			DebugHandler activeDebugHandler = base.GetActiveDebugHandler(renderingData);
			bool flag = activeDebugHandler == null || activeDebugHandler.IsLightingActive;
			int num = 0;
			uint num2 = 0U;
			using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawLights))
			{
				for (int i = startIndex; i < batchCount; i++)
				{
					ref LayerBatch ptr = ref layerBatches[i];
					uint num3 = ptr.lightStats.blendStylesUsed;
					uint num4 = 0U;
					while (num3 > 0U)
					{
						num4 += num3 & 1U;
						num3 >>= 1;
					}
					num2 += num4;
					if (num2 > LayerUtility.maxTextureCount)
					{
						break;
					}
					num++;
					if (ptr.lightStats.totalNormalMapUsage > 0)
					{
						filterSettings.sortingLayerRange = ptr.layerRange;
						RenderTargetIdentifier renderTargetIdentifier = (this.m_NeedsDepth ? base.depthAttachment : BuiltinRenderTextureType.None);
						this.RenderNormals(context, renderingData, normalsDrawSettings, filterSettings, renderTargetIdentifier, cmd, ptr.lightStats);
					}
					using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawLightTextures))
					{
						this.RenderLights(renderingData, cmd, ptr.startLayerID, ref ptr, ref desc);
					}
				}
			}
			bool flag2 = renderingData.cameraData.cameraTargetDescriptor.msaaSamples > 1;
			bool flag3 = startIndex + num >= batchCount;
			int num5 = -1;
			bool flag4 = false;
			if (flag2 && flag3)
			{
				this.DetermineWhenToResolve(startIndex, num, batchCount, layerBatches, out num5, out flag4);
			}
			int num6 = this.m_Renderer2DData.lightBlendStyles.Length;
			using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawRenderers))
			{
				RenderBufferStoreAction renderBufferStoreAction;
				if (flag2)
				{
					renderBufferStoreAction = ((num5 < startIndex) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.StoreAndResolve);
				}
				else
				{
					renderBufferStoreAction = RenderBufferStoreAction.Store;
				}
				cmd.SetRenderTarget(base.colorAttachment, RenderBufferLoadAction.Load, renderBufferStoreAction, base.depthAttachment, RenderBufferLoadAction.Load, renderBufferStoreAction);
				for (int j = startIndex; j < startIndex + num; j++)
				{
					using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawLayerBatch))
					{
						LayerBatch layerBatch = layerBatches[j];
						if (layerBatch.lightStats.totalLights > 0)
						{
							for (int k = 0; k < num6; k++)
							{
								uint num7 = 1U << k;
								bool flag5 = (layerBatch.lightStats.blendStylesUsed & num7) > 0U;
								if (flag5)
								{
									RenderTargetIdentifier rtid = layerBatch.GetRTId(cmd, desc, k);
									cmd.SetGlobalTexture(Render2DLightingPass.k_ShapeLightTextureIDs[k], rtid);
								}
								RendererLighting.EnableBlendStyle(cmd, k, flag5);
							}
						}
						else
						{
							for (int l = 0; l < Render2DLightingPass.k_ShapeLightTextureIDs.Length; l++)
							{
								cmd.SetGlobalTexture(Render2DLightingPass.k_ShapeLightTextureIDs[l], Texture2D.blackTexture);
								RendererLighting.EnableBlendStyle(cmd, l, l == 0);
							}
						}
						context.ExecuteCommandBuffer(cmd);
						cmd.Clear();
						short cameraSortingLayerBoundsIndex = this.GetCameraSortingLayerBoundsIndex();
						RenderBufferStoreAction renderBufferStoreAction2;
						if (flag2)
						{
							renderBufferStoreAction2 = ((num5 == j && flag4) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.StoreAndResolve);
						}
						else
						{
							renderBufferStoreAction2 = RenderBufferStoreAction.Store;
						}
						if (cameraSortingLayerBoundsIndex >= layerBatch.layerRange.lowerBound && cameraSortingLayerBoundsIndex < layerBatch.layerRange.upperBound && this.m_Renderer2DData.useCameraSortingLayerTexture)
						{
							filterSettings.sortingLayerRange = new SortingLayerRange(layerBatch.layerRange.lowerBound, cameraSortingLayerBoundsIndex);
							this.Render(context, cmd, ref renderingData, ref filterSettings, drawSettings);
							this.CopyCameraSortingLayerRenderTexture(context, renderingData, renderBufferStoreAction2);
							filterSettings.sortingLayerRange = new SortingLayerRange(cameraSortingLayerBoundsIndex + 1, layerBatch.layerRange.upperBound);
							this.Render(context, cmd, ref renderingData, ref filterSettings, drawSettings);
						}
						else
						{
							filterSettings.sortingLayerRange = new SortingLayerRange(layerBatch.layerRange.lowerBound, layerBatch.layerRange.upperBound);
							this.Render(context, cmd, ref renderingData, ref filterSettings, drawSettings);
							if (cameraSortingLayerBoundsIndex == layerBatch.layerRange.upperBound && this.m_Renderer2DData.useCameraSortingLayerTexture)
							{
								this.CopyCameraSortingLayerRenderTexture(context, renderingData, renderBufferStoreAction2);
							}
						}
						if (flag && layerBatch.lightStats.totalVolumetricUsage > 0)
						{
							string text = "Render 2D Light Volumes";
							cmd.BeginSample(text);
							RenderBufferStoreAction renderBufferStoreAction3;
							if (flag2)
							{
								renderBufferStoreAction3 = ((num5 == j && !flag4) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.StoreAndResolve);
							}
							else
							{
								renderBufferStoreAction3 = RenderBufferStoreAction.Store;
							}
							this.RenderLightVolumes(renderingData, cmd, layerBatch.startLayerID, layerBatch.endLayerValue, base.colorAttachment, base.depthAttachment, RenderBufferStoreAction.Store, renderBufferStoreAction3, false, this.m_Renderer2DData.lightCullResult.visibleLights);
							cmd.EndSample(text);
						}
					}
				}
			}
			for (int m = startIndex; m < startIndex + num; m++)
			{
				layerBatches[m].ReleaseRT(cmd);
			}
			return num;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000E038 File Offset: 0x0000C238
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			bool flag = true;
			Camera camera = renderingData.cameraData.camera;
			FilteringSettings filteringSettings = default(FilteringSettings);
			filteringSettings.renderQueueRange = RenderQueueRange.all;
			filteringSettings.layerMask = -1;
			filteringSettings.renderingLayerMask = uint.MaxValue;
			filteringSettings.sortingLayerRange = SortingLayerRange.all;
			LayerUtility.InitializeBudget(this.m_Renderer2DData.lightRenderTextureMemoryBudget);
			ShadowRendering.InitializeBudget(this.m_Renderer2DData.shadowRenderTextureMemoryBudget);
			if (this.m_Renderer2DData.lightCullResult.IsSceneLit())
			{
				DrawingSettings drawingSettings = base.CreateDrawingSettings(Render2DLightingPass.k_ShaderTags, ref renderingData, SortingCriteria.CommonTransparent);
				DrawingSettings drawingSettings2 = base.CreateDrawingSettings(Render2DLightingPass.k_NormalsRenderingPassName, ref renderingData, SortingCriteria.CommonTransparent);
				SortingSettings sortingSettings = drawingSettings.sortingSettings;
				this.GetTransparencySortingMode(camera, ref sortingSettings);
				drawingSettings.sortingSettings = sortingSettings;
				drawingSettings2.sortingSettings = sortingSettings;
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				commandBuffer.SetGlobalFloat(Render2DLightingPass.k_HDREmulationScaleID, this.m_Renderer2DData.hdrEmulationScale);
				commandBuffer.SetGlobalFloat(Render2DLightingPass.k_InverseHDREmulationScaleID, 1f / this.m_Renderer2DData.hdrEmulationScale);
				commandBuffer.SetGlobalFloat(Render2DLightingPass.k_UseSceneLightingID, flag ? 1f : 0f);
				commandBuffer.SetGlobalColor(Render2DLightingPass.k_RendererColorID, Color.white);
				this.SetShapeLightShaderGlobals(commandBuffer);
				RenderTextureDescriptor blendStyleRenderTextureDesc = this.GetBlendStyleRenderTextureDesc(renderingData);
				int num;
				LayerBatch[] array = LayerUtility.CalculateBatches(this.m_Renderer2DData.lightCullResult, out num);
				int num2;
				for (int i = 0; i < num; i += num2)
				{
					num2 = this.DrawLayerBatches(array, num, i, commandBuffer, context, ref renderingData, ref filteringSettings, ref drawingSettings2, ref drawingSettings, ref blendStyleRenderTextureDesc);
				}
				this.DisableAllKeywords(commandBuffer);
				this.ReleaseRenderTextures(commandBuffer);
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}
			else
			{
				DrawingSettings drawingSettings3 = base.CreateDrawingSettings(Render2DLightingPass.k_ShaderTags, ref renderingData, SortingCriteria.CommonTransparent);
				RenderBufferStoreAction renderBufferStoreAction = ((renderingData.cameraData.cameraTargetDescriptor.msaaSamples > 1) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.Store);
				SortingSettings sortingSettings2 = drawingSettings3.sortingSettings;
				this.GetTransparencySortingMode(camera, ref sortingSettings2);
				drawingSettings3.sortingSettings = sortingSettings2;
				CommandBuffer commandBuffer2 = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer2, Render2DLightingPass.m_ProfilingSamplerUnlit))
				{
					commandBuffer2.SetRenderTarget(base.colorAttachment, RenderBufferLoadAction.Load, renderBufferStoreAction, base.depthAttachment, RenderBufferLoadAction.Load, renderBufferStoreAction);
					commandBuffer2.SetGlobalFloat(Render2DLightingPass.k_UseSceneLightingID, flag ? 1f : 0f);
					commandBuffer2.SetGlobalColor(Render2DLightingPass.k_RendererColorID, Color.white);
					for (int j = 0; j < Render2DLightingPass.k_ShapeLightTextureIDs.Length; j++)
					{
						if (j == 0)
						{
							commandBuffer2.SetGlobalTexture(Render2DLightingPass.k_ShapeLightTextureIDs[j], Texture2D.blackTexture);
						}
						RendererLighting.EnableBlendStyle(commandBuffer2, j, j == 0);
					}
				}
				this.DisableAllKeywords(commandBuffer2);
				context.ExecuteCommandBuffer(commandBuffer2);
				if (this.m_Renderer2DData.useCameraSortingLayerTexture)
				{
					filteringSettings.sortingLayerRange = new SortingLayerRange(short.MinValue, this.m_CameraSortingLayerBoundsIndex);
					this.Render(context, commandBuffer2, ref renderingData, ref filteringSettings, drawingSettings3);
					this.CopyCameraSortingLayerRenderTexture(context, renderingData, renderBufferStoreAction);
					filteringSettings.sortingLayerRange = new SortingLayerRange(this.m_CameraSortingLayerBoundsIndex, short.MaxValue);
					this.Render(context, commandBuffer2, ref renderingData, ref filteringSettings, drawingSettings3);
				}
				else
				{
					this.Render(context, commandBuffer2, ref renderingData, ref filteringSettings, drawingSettings3);
				}
				CommandBufferPool.Release(commandBuffer2);
			}
			filteringSettings.sortingLayerRange = SortingLayerRange.all;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000E37C File Offset: 0x0000C57C
		Renderer2DData IRenderPass2D.rendererData
		{
			get
			{
				return this.m_Renderer2DData;
			}
		}

		// Token: 0x040000DC RID: 220
		private static readonly int k_HDREmulationScaleID = Shader.PropertyToID("_HDREmulationScale");

		// Token: 0x040000DD RID: 221
		private static readonly int k_InverseHDREmulationScaleID = Shader.PropertyToID("_InverseHDREmulationScale");

		// Token: 0x040000DE RID: 222
		private static readonly int k_UseSceneLightingID = Shader.PropertyToID("_UseSceneLighting");

		// Token: 0x040000DF RID: 223
		private static readonly int k_RendererColorID = Shader.PropertyToID("_RendererColor");

		// Token: 0x040000E0 RID: 224
		private static readonly int k_CameraSortingLayerTextureID = Shader.PropertyToID("_CameraSortingLayerTexture");

		// Token: 0x040000E1 RID: 225
		private static readonly int[] k_ShapeLightTextureIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightTexture0"),
			Shader.PropertyToID("_ShapeLightTexture1"),
			Shader.PropertyToID("_ShapeLightTexture2"),
			Shader.PropertyToID("_ShapeLightTexture3")
		};

		// Token: 0x040000E2 RID: 226
		private static readonly ShaderTagId k_CombinedRenderingPassName = new ShaderTagId("Universal2D");

		// Token: 0x040000E3 RID: 227
		private static readonly ShaderTagId k_NormalsRenderingPassName = new ShaderTagId("NormalsRendering");

		// Token: 0x040000E4 RID: 228
		private static readonly ShaderTagId k_LegacyPassName = new ShaderTagId("SRPDefaultUnlit");

		// Token: 0x040000E5 RID: 229
		private static readonly List<ShaderTagId> k_ShaderTags = new List<ShaderTagId>
		{
			Render2DLightingPass.k_LegacyPassName,
			Render2DLightingPass.k_CombinedRenderingPassName
		};

		// Token: 0x040000E6 RID: 230
		private static readonly ProfilingSampler m_ProfilingDrawLights = new ProfilingSampler("Draw 2D Lights");

		// Token: 0x040000E7 RID: 231
		private static readonly ProfilingSampler m_ProfilingDrawLightTextures = new ProfilingSampler("Draw 2D Lights Textures");

		// Token: 0x040000E8 RID: 232
		private static readonly ProfilingSampler m_ProfilingDrawRenderers = new ProfilingSampler("Draw All Renderers");

		// Token: 0x040000E9 RID: 233
		private static readonly ProfilingSampler m_ProfilingDrawLayerBatch = new ProfilingSampler("Draw Layer Batch");

		// Token: 0x040000EA RID: 234
		private static readonly ProfilingSampler m_ProfilingSamplerUnlit = new ProfilingSampler("Render Unlit");

		// Token: 0x040000EB RID: 235
		private Material m_BlitMaterial;

		// Token: 0x040000EC RID: 236
		private Material m_SamplingMaterial;

		// Token: 0x040000ED RID: 237
		private readonly Renderer2DData m_Renderer2DData;

		// Token: 0x040000EE RID: 238
		private bool m_NeedsDepth;

		// Token: 0x040000EF RID: 239
		private short m_CameraSortingLayerBoundsIndex;
	}
}
