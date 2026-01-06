using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000113 RID: 275
	public class PostProcessPass : ScriptableRenderPass
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00036120 File Offset: 0x00034320
		public PostProcessPass(RenderPassEvent evt, PostProcessData data, Material blitMaterial)
		{
			base.profilingSampler = new ProfilingSampler("PostProcessPass");
			base.renderPassEvent = evt;
			this.m_Data = data;
			this.m_Materials = new PostProcessPass.MaterialLibrary(data);
			this.m_BlitMaterial = blitMaterial;
			if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Blend))
			{
				this.m_DefaultHDRFormat = GraphicsFormat.B10G11R11_UFloatPack32;
				this.m_UseRGBM = false;
			}
			else
			{
				this.m_DefaultHDRFormat = ((QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormat.R8G8B8A8_SRGB : GraphicsFormat.R8G8B8A8_UNorm);
				this.m_UseRGBM = true;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R8G8_UNorm, FormatUsage.Render) && SystemInfo.graphicsDeviceVendor.ToLowerInvariant().Contains("arm"))
			{
				this.m_SMAAEdgeFormat = GraphicsFormat.R8G8_UNorm;
			}
			else
			{
				this.m_SMAAEdgeFormat = GraphicsFormat.R8G8B8A8_UNorm;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_UNorm, FormatUsage.Blend))
			{
				this.m_GaussianCoCFormat = GraphicsFormat.R16_UNorm;
			}
			else if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, FormatUsage.Blend))
			{
				this.m_GaussianCoCFormat = GraphicsFormat.R16_SFloat;
			}
			else
			{
				this.m_GaussianCoCFormat = GraphicsFormat.R8_UNorm;
			}
			PostProcessPass.ShaderConstants._BloomMipUp = new int[16];
			PostProcessPass.ShaderConstants._BloomMipDown = new int[16];
			for (int i = 0; i < 16; i++)
			{
				PostProcessPass.ShaderConstants._BloomMipUp[i] = Shader.PropertyToID("_BloomMipUp" + i.ToString());
				PostProcessPass.ShaderConstants._BloomMipDown[i] = Shader.PropertyToID("_BloomMipDown" + i.ToString());
			}
			this.m_MRT2 = new RenderTargetIdentifier[2];
			this.m_ResetHistory = true;
			base.useNativeRenderPass = false;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0003626F File Offset: 0x0003446F
		public void Cleanup()
		{
			this.m_Materials.Cleanup();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0003627C File Offset: 0x0003447C
		public void Setup(in RenderTextureDescriptor baseDescriptor, in RenderTargetHandle source, bool resolveToScreen, in RenderTargetHandle depth, in RenderTargetHandle internalLut, bool hasFinalPass, bool enableSRGBConversion)
		{
			this.m_Descriptor = baseDescriptor;
			this.m_Descriptor.useMipMap = false;
			this.m_Descriptor.autoGenerateMips = false;
			this.m_Source = source.id;
			this.m_Depth = depth;
			this.m_InternalLut = internalLut;
			this.m_IsFinalPass = false;
			this.m_HasFinalPass = hasFinalPass;
			this.m_EnableSRGBConversionIfNeeded = enableSRGBConversion;
			this.m_ResolveToScreen = resolveToScreen;
			this.m_Destination = RenderTargetHandle.CameraTarget;
			this.m_UseSwapBuffer = true;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00036308 File Offset: 0x00034508
		public void Setup(in RenderTextureDescriptor baseDescriptor, in RenderTargetHandle source, RenderTargetHandle destination, in RenderTargetHandle depth, in RenderTargetHandle internalLut, bool hasFinalPass, bool enableSRGBConversion)
		{
			this.m_Descriptor = baseDescriptor;
			this.m_Descriptor.useMipMap = false;
			this.m_Descriptor.autoGenerateMips = false;
			this.m_Source = source.id;
			this.m_Destination = destination;
			this.m_Depth = depth;
			this.m_InternalLut = internalLut;
			this.m_IsFinalPass = false;
			this.m_HasFinalPass = hasFinalPass;
			this.m_EnableSRGBConversionIfNeeded = enableSRGBConversion;
			this.m_UseSwapBuffer = false;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00036389 File Offset: 0x00034589
		public void SetupFinalPass(in RenderTargetHandle source, bool useSwapBuffer = false)
		{
			this.m_Source = source.id;
			this.m_Destination = RenderTargetHandle.CameraTarget;
			this.m_IsFinalPass = true;
			this.m_HasFinalPass = false;
			this.m_EnableSRGBConversionIfNeeded = true;
			this.m_UseSwapBuffer = useSwapBuffer;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000363C4 File Offset: 0x000345C4
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.overrideCameraTarget = true;
			if (this.m_Destination == RenderTargetHandle.CameraTarget)
			{
				return;
			}
			if (this.m_Destination.HasInternalRenderTargetId())
			{
				return;
			}
			RenderTextureDescriptor compatibleDescriptor = this.GetCompatibleDescriptor();
			compatibleDescriptor.depthBufferBits = 0;
			cmd.GetTemporaryRT(this.m_Destination.id, compatibleDescriptor, FilterMode.Point);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0003641B File Offset: 0x0003461B
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (this.m_Destination == RenderTargetHandle.CameraTarget)
			{
				return;
			}
			if (this.m_Destination.HasInternalRenderTargetId())
			{
				return;
			}
			cmd.ReleaseTemporaryRT(this.m_Destination.id);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0003644F File Offset: 0x0003464F
		public void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00036458 File Offset: 0x00034658
		public bool CanRunOnTile()
		{
			return false;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0003645C File Offset: 0x0003465C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			VolumeStack stack = VolumeManager.instance.stack;
			this.m_DepthOfField = stack.GetComponent<DepthOfField>();
			this.m_MotionBlur = stack.GetComponent<MotionBlur>();
			this.m_PaniniProjection = stack.GetComponent<PaniniProjection>();
			this.m_Bloom = stack.GetComponent<Bloom>();
			this.m_LensDistortion = stack.GetComponent<LensDistortion>();
			this.m_ChromaticAberration = stack.GetComponent<ChromaticAberration>();
			this.m_Vignette = stack.GetComponent<Vignette>();
			this.m_ColorLookup = stack.GetComponent<ColorLookup>();
			this.m_ColorAdjustments = stack.GetComponent<ColorAdjustments>();
			this.m_Tonemapping = stack.GetComponent<Tonemapping>();
			this.m_FilmGrain = stack.GetComponent<FilmGrain>();
			this.m_UseDrawProcedural = renderingData.cameraData.xr.enabled;
			this.m_UseFastSRGBLinearConversion = renderingData.postProcessingData.useFastSRGBLinearConversion;
			if (this.m_IsFinalPass)
			{
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, PostProcessPass.m_ProfilingRenderFinalPostProcessing))
				{
					this.RenderFinalPass(commandBuffer, ref renderingData);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}
			else if (!this.CanRunOnTile())
			{
				CommandBuffer commandBuffer2 = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer2, PostProcessPass.m_ProfilingRenderPostProcessing))
				{
					this.Render(commandBuffer2, ref renderingData);
				}
				context.ExecuteCommandBuffer(commandBuffer2);
				CommandBufferPool.Release(commandBuffer2);
			}
			this.m_ResetHistory = false;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000365C8 File Offset: 0x000347C8
		private RenderTextureDescriptor GetCompatibleDescriptor()
		{
			return this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, 0);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000365F4 File Offset: 0x000347F4
		private RenderTextureDescriptor GetCompatibleDescriptor(int width, int height, GraphicsFormat format, int depthBufferBits = 0)
		{
			RenderTextureDescriptor descriptor = this.m_Descriptor;
			descriptor.depthBufferBits = depthBufferBits;
			descriptor.msaaSamples = 1;
			descriptor.width = width;
			descriptor.height = height;
			descriptor.graphicsFormat = format;
			return descriptor;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00036632 File Offset: 0x00034832
		private bool RequireSRGBConversionBlitToBackBuffer(CameraData cameraData)
		{
			return cameraData.requireSrgbConversion && this.m_EnableSRGBConversionIfNeeded;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00036648 File Offset: 0x00034848
		private new void Blit(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int passIndex = 0)
		{
			cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, source);
			if (this.m_UseDrawProcedural)
			{
				Vector4 vector = new Vector4(1f, 1f, 0f, 0f);
				cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
				cmd.SetRenderTarget(new RenderTargetIdentifier(destination, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
				cmd.DrawProcedural(Matrix4x4.identity, material, passIndex, MeshTopology.Quads, 4, 1, null);
				return;
			}
			cmd.Blit(source, destination, material, passIndex);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000366C4 File Offset: 0x000348C4
		private void DrawFullscreenMesh(CommandBuffer cmd, Material material, int passIndex)
		{
			if (this.m_UseDrawProcedural)
			{
				Vector4 vector = new Vector4(1f, 1f, 0f, 0f);
				cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
				cmd.DrawProcedural(Matrix4x4.identity, material, passIndex, MeshTopology.Quads, 4, 1, null);
				return;
			}
			cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, passIndex);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00036728 File Offset: 0x00034928
		private void Render(CommandBuffer cmd, ref RenderingData renderingData)
		{
			PostProcessPass.<>c__DisplayClass57_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cmd = cmd;
			ref CameraData ptr = ref renderingData.cameraData;
			ref ScriptableRenderer ptr2 = ref ptr.renderer;
			bool isSceneViewCamera = ptr.isSceneViewCamera;
			bool flag = ptr.isStopNaNEnabled && this.m_Materials.stopNaN != null;
			bool flag2 = ptr.antialiasing == AntialiasingMode.SubpixelMorphologicalAntiAliasing && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
			Material material = ((this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian) ? this.m_Materials.gaussianDepthOfField : this.m_Materials.bokehDepthOfField);
			bool flag3 = this.m_DepthOfField.IsActive() && !isSceneViewCamera && material != null;
			bool flag4 = !LensFlareCommonSRP.Instance.IsEmpty();
			bool flag5 = this.m_MotionBlur.IsActive() && !isSceneViewCamera;
			bool flag6 = this.m_PaniniProjection.IsActive() && !isSceneViewCamera;
			CS$<>8__locals1.amountOfPassesRemaining = (flag ? 1 : 0) + (flag2 ? 1 : 0) + (flag3 ? 1 : 0) + (flag4 ? 1 : 0) + (flag5 ? 1 : 0) + (flag6 ? 1 : 0);
			if (this.m_UseSwapBuffer && CS$<>8__locals1.amountOfPassesRemaining > 0)
			{
				ptr2.EnableSwapBufferMSAA(false);
			}
			CS$<>8__locals1.tempTargetUsed = false;
			CS$<>8__locals1.tempTarget2Used = false;
			CS$<>8__locals1.source = (this.m_UseSwapBuffer ? ptr2.cameraColorTarget : this.m_Source);
			CS$<>8__locals1.destination = (this.m_UseSwapBuffer ? ptr2.GetCameraColorFrontBuffer(CS$<>8__locals1.cmd) : (-1));
			CS$<>8__locals1.cmd.SetGlobalMatrix(PostProcessPass.ShaderConstants._FullscreenProjMat, GL.GetGPUProjectionMatrix(Matrix4x4.identity, true));
			if (flag)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.StopNaNs)))
				{
					RenderingUtils.Blit(CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|57_1(ref CS$<>8__locals1), this.m_Materials.stopNaN, 0, this.m_UseDrawProcedural, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					this.<Render>g__Swap|57_2(ref ptr2, ref CS$<>8__locals1);
				}
			}
			if (flag2)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.SMAA)))
				{
					this.DoSubpixelMorphologicalAntialiasing(ref ptr, CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|57_1(ref CS$<>8__locals1));
					this.<Render>g__Swap|57_2(ref ptr2, ref CS$<>8__locals1);
				}
			}
			if (flag3)
			{
				URPProfileId urpprofileId = ((this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian) ? URPProfileId.GaussianDepthOfField : URPProfileId.BokehDepthOfField);
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(urpprofileId)))
				{
					this.DoDepthOfField(ptr.camera, CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|57_1(ref CS$<>8__locals1), ptr.pixelRect);
					this.<Render>g__Swap|57_2(ref ptr2, ref CS$<>8__locals1);
				}
			}
			if (flag5)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.MotionBlur)))
				{
					this.DoMotionBlur(ptr, CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|57_1(ref CS$<>8__locals1));
					this.<Render>g__Swap|57_2(ref ptr2, ref CS$<>8__locals1);
				}
			}
			if (flag6)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.PaniniProjection)))
				{
					this.DoPaniniProjection(ptr.camera, CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|57_1(ref CS$<>8__locals1));
					this.<Render>g__Swap|57_2(ref ptr2, ref CS$<>8__locals1);
				}
			}
			if (flag4)
			{
				bool flag7;
				float num;
				float num2;
				if (this.m_PaniniProjection.IsActive())
				{
					flag7 = true;
					num = this.m_PaniniProjection.distance.value;
					num2 = this.m_PaniniProjection.cropToFit.value;
				}
				else
				{
					flag7 = false;
					num = 1f;
					num2 = 1f;
				}
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareDataDriven)))
				{
					this.DoLensFlareDatadriven(ptr.camera, CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), flag7, num, num2);
				}
			}
			using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.UberPostProcess)))
			{
				this.m_Materials.uber.shaderKeywords = null;
				bool flag8 = this.m_Bloom.IsActive();
				if (flag8)
				{
					using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.Bloom)))
					{
						this.SetupBloom(CS$<>8__locals1.cmd, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1), this.m_Materials.uber);
					}
				}
				this.SetupLensDistortion(this.m_Materials.uber, isSceneViewCamera);
				this.SetupChromaticAberration(this.m_Materials.uber);
				this.SetupVignette(this.m_Materials.uber);
				this.SetupColorGrading(CS$<>8__locals1.cmd, ref renderingData, this.m_Materials.uber);
				this.SetupGrain(in ptr, this.m_Materials.uber);
				this.SetupDithering(in ptr, this.m_Materials.uber);
				if (this.RequireSRGBConversionBlitToBackBuffer(ptr))
				{
					this.m_Materials.uber.EnableKeyword("_LINEAR_TO_SRGB_CONVERSION");
				}
				if (this.m_UseFastSRGBLinearConversion)
				{
					this.m_Materials.uber.EnableKeyword("_USE_FAST_SRGB_LINEAR_CONVERSION");
				}
				CS$<>8__locals1.cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, this.<Render>g__GetSource|57_0(ref CS$<>8__locals1));
				RenderBufferLoadAction renderBufferLoadAction = RenderBufferLoadAction.DontCare;
				if (this.m_Destination == RenderTargetHandle.CameraTarget && !ptr.isDefaultViewport)
				{
					renderBufferLoadAction = RenderBufferLoadAction.Load;
				}
				RenderTargetIdentifier renderTargetIdentifier = (this.m_UseSwapBuffer ? CS$<>8__locals1.destination : this.m_Destination.id);
				RenderTargetHandle cameraTarget = RenderTargetHandle.GetCameraTarget(ptr.xr);
				RenderTargetIdentifier renderTargetIdentifier2 = ((ptr.targetTexture != null && !ptr.xr.enabled) ? new RenderTargetIdentifier(ptr.targetTexture) : cameraTarget.Identifier());
				if (this.m_UseSwapBuffer)
				{
					renderTargetIdentifier2 = (this.m_ResolveToScreen ? renderTargetIdentifier2 : renderTargetIdentifier);
				}
				else
				{
					renderTargetIdentifier2 = ((this.m_Destination == RenderTargetHandle.CameraTarget) ? renderTargetIdentifier2 : this.m_Destination.Identifier());
					this.m_ResolveToScreen = ptr.resolveFinalTarget || this.m_Destination == cameraTarget || this.m_HasFinalPass;
				}
				if (ptr.xr.enabled)
				{
					CS$<>8__locals1.cmd.SetRenderTarget(new RenderTargetIdentifier(renderTargetIdentifier2, 0, CubemapFace.Unknown, -1), renderBufferLoadAction, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					bool flag9 = renderTargetIdentifier2 == ptr.xr.renderTarget && !ptr.xr.renderTargetIsRenderTexture;
					if (flag9)
					{
						CS$<>8__locals1.cmd.SetViewport(ptr.pixelRect);
					}
					Vector4 vector = ((flag9 && SystemInfo.graphicsUVStartsAtTop) ? new Vector4(1f, -1f, 0f, 1f) : new Vector4(1f, 1f, 0f, 0f));
					CS$<>8__locals1.cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
					CS$<>8__locals1.cmd.DrawProcedural(Matrix4x4.identity, this.m_Materials.uber, 0, MeshTopology.Quads, 4, 1, null);
					if (!this.m_ResolveToScreen && !this.m_UseSwapBuffer)
					{
						CS$<>8__locals1.cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, renderTargetIdentifier2);
						CS$<>8__locals1.cmd.SetRenderTarget(new RenderTargetIdentifier(this.m_Source, 0, CubemapFace.Unknown, -1), renderBufferLoadAction, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
						vector = new Vector4(1f, 1f, 0f, 0f);
						CS$<>8__locals1.cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector);
						CS$<>8__locals1.cmd.DrawProcedural(Matrix4x4.identity, this.m_BlitMaterial, 0, MeshTopology.Quads, 4, 1, null);
					}
				}
				else
				{
					CS$<>8__locals1.cmd.SetRenderTarget(renderTargetIdentifier2, renderBufferLoadAction, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					ptr.renderer.ConfigureCameraTarget(renderTargetIdentifier2, renderTargetIdentifier2);
					CS$<>8__locals1.cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
					if ((this.m_Destination == RenderTargetHandle.CameraTarget && !this.m_UseSwapBuffer) || (this.m_ResolveToScreen && this.m_UseSwapBuffer))
					{
						CS$<>8__locals1.cmd.SetViewport(ptr.pixelRect);
					}
					CS$<>8__locals1.cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_Materials.uber);
					if (!this.m_ResolveToScreen && !this.m_UseSwapBuffer)
					{
						CS$<>8__locals1.cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, renderTargetIdentifier2);
						CS$<>8__locals1.cmd.SetRenderTarget(this.m_Source, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
						CS$<>8__locals1.cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, this.m_BlitMaterial);
					}
					CS$<>8__locals1.cmd.SetViewProjectionMatrices(ptr.camera.worldToCameraMatrix, ptr.camera.projectionMatrix);
				}
				if (this.m_UseSwapBuffer && !this.m_ResolveToScreen)
				{
					ptr2.SwapColorBuffer(CS$<>8__locals1.cmd);
				}
				if (flag8)
				{
					CS$<>8__locals1.cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._BloomMipUp[0]);
				}
				if (CS$<>8__locals1.tempTargetUsed)
				{
					CS$<>8__locals1.cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._TempTarget);
				}
				if (CS$<>8__locals1.tempTarget2Used)
				{
					CS$<>8__locals1.cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._TempTarget2);
				}
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00037100 File Offset: 0x00035300
		private BuiltinRenderTextureType BlitDstDiscardContent(CommandBuffer cmd, RenderTargetIdentifier rt)
		{
			cmd.SetRenderTarget(new RenderTargetIdentifier(rt, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			return BuiltinRenderTextureType.CurrentActive;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00037118 File Offset: 0x00035318
		private void DoSubpixelMorphologicalAntialiasing(ref CameraData cameraData, CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			Camera camera = cameraData.camera;
			Rect pixelRect = cameraData.pixelRect;
			Material subpixelMorphologicalAntialiasing = this.m_Materials.subpixelMorphologicalAntialiasing;
			subpixelMorphologicalAntialiasing.SetVector(PostProcessPass.ShaderConstants._Metrics, new Vector4(1f / (float)this.m_Descriptor.width, 1f / (float)this.m_Descriptor.height, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height));
			subpixelMorphologicalAntialiasing.SetTexture(PostProcessPass.ShaderConstants._AreaTexture, this.m_Data.textures.smaaAreaTex);
			subpixelMorphologicalAntialiasing.SetTexture(PostProcessPass.ShaderConstants._SearchTexture, this.m_Data.textures.smaaSearchTex);
			subpixelMorphologicalAntialiasing.SetFloat(PostProcessPass.ShaderConstants._StencilRef, 64f);
			subpixelMorphologicalAntialiasing.SetFloat(PostProcessPass.ShaderConstants._StencilMask, 64f);
			subpixelMorphologicalAntialiasing.shaderKeywords = null;
			switch (cameraData.antialiasingQuality)
			{
			case AntialiasingQuality.Low:
				subpixelMorphologicalAntialiasing.EnableKeyword("_SMAA_PRESET_LOW");
				break;
			case AntialiasingQuality.Medium:
				subpixelMorphologicalAntialiasing.EnableKeyword("_SMAA_PRESET_MEDIUM");
				break;
			case AntialiasingQuality.High:
				subpixelMorphologicalAntialiasing.EnableKeyword("_SMAA_PRESET_HIGH");
				break;
			}
			RenderTargetIdentifier renderTargetIdentifier;
			int num;
			if (this.m_Depth == RenderTargetHandle.CameraTarget || this.m_Descriptor.msaaSamples > 1)
			{
				renderTargetIdentifier = PostProcessPass.ShaderConstants._EdgeTexture;
				num = 24;
			}
			else
			{
				renderTargetIdentifier = this.m_Depth.Identifier();
				num = 0;
			}
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._EdgeTexture, this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, this.m_SMAAEdgeFormat, num), FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._BlendTexture, this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.R8G8B8A8_UNorm, 0), FilterMode.Point);
			cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
			cmd.SetViewport(pixelRect);
			cmd.SetRenderTarget(new RenderTargetIdentifier(PostProcessPass.ShaderConstants._EdgeTexture, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, renderTargetIdentifier, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			cmd.ClearRenderTarget(RTClearFlags.ColorStencil, Color.clear, 1f, 0U);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, source);
			this.DrawFullscreenMesh(cmd, subpixelMorphologicalAntialiasing, 0);
			cmd.SetRenderTarget(new RenderTargetIdentifier(PostProcessPass.ShaderConstants._BlendTexture, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, renderTargetIdentifier, RenderBufferLoadAction.Load, RenderBufferStoreAction.DontCare);
			cmd.ClearRenderTarget(false, true, Color.clear);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, PostProcessPass.ShaderConstants._EdgeTexture);
			this.DrawFullscreenMesh(cmd, subpixelMorphologicalAntialiasing, 1);
			cmd.SetRenderTarget(new RenderTargetIdentifier(destination, 0, CubemapFace.Unknown, -1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, source);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._BlendTexture, PostProcessPass.ShaderConstants._BlendTexture);
			this.DrawFullscreenMesh(cmd, subpixelMorphologicalAntialiasing, 2);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._EdgeTexture);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._BlendTexture);
			cmd.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000373C4 File Offset: 0x000355C4
		private void DoDepthOfField(Camera camera, CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Rect pixelRect)
		{
			if (this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian)
			{
				this.DoGaussianDepthOfField(camera, cmd, source, destination, pixelRect);
				return;
			}
			if (this.m_DepthOfField.mode.value == DepthOfFieldMode.Bokeh)
			{
				this.DoBokehDepthOfField(cmd, source, destination, pixelRect);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00037414 File Offset: 0x00035614
		private void DoGaussianDepthOfField(Camera camera, CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Rect pixelRect)
		{
			int num = 2;
			Material gaussianDepthOfField = this.m_Materials.gaussianDepthOfField;
			int num2 = this.m_Descriptor.width / num;
			int num3 = this.m_Descriptor.height / num;
			float value = this.m_DepthOfField.gaussianStart.value;
			float num4 = Mathf.Max(value, this.m_DepthOfField.gaussianEnd.value);
			float num5 = this.m_DepthOfField.gaussianMaxRadius.value * ((float)num2 / 1080f);
			num5 = Mathf.Min(num5, 2f);
			CoreUtils.SetKeyword(gaussianDepthOfField, "_HIGH_QUALITY_SAMPLING", this.m_DepthOfField.highQualitySampling.value);
			gaussianDepthOfField.SetVector(PostProcessPass.ShaderConstants._CoCParams, new Vector3(value, num4, num5));
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._FullCoCTexture, this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, this.m_GaussianCoCFormat, 0), FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._HalfCoCTexture, this.GetCompatibleDescriptor(num2, num3, this.m_GaussianCoCFormat, 0), FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._PingTexture, this.GetCompatibleDescriptor(num2, num3, this.m_DefaultHDRFormat, 0), FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._PongTexture, this.GetCompatibleDescriptor(num2, num3, this.m_DefaultHDRFormat, 0), FilterMode.Bilinear);
			PostProcessUtils.SetSourceSize(cmd, this.m_Descriptor);
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._DownSampleScaleFactor, new Vector4(1f / (float)num, 1f / (float)num, (float)num, (float)num));
			this.Blit(cmd, source, PostProcessPass.ShaderConstants._FullCoCTexture, gaussianDepthOfField, 0);
			this.m_MRT2[0] = PostProcessPass.ShaderConstants._HalfCoCTexture;
			this.m_MRT2[1] = PostProcessPass.ShaderConstants._PingTexture;
			cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
			cmd.SetViewport(pixelRect);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, source);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._FullCoCTexture, PostProcessPass.ShaderConstants._FullCoCTexture);
			cmd.SetRenderTarget(this.m_MRT2, PostProcessPass.ShaderConstants._HalfCoCTexture, 0, CubemapFace.Unknown, -1);
			this.DrawFullscreenMesh(cmd, gaussianDepthOfField, 1);
			cmd.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._HalfCoCTexture, PostProcessPass.ShaderConstants._HalfCoCTexture);
			this.Blit(cmd, PostProcessPass.ShaderConstants._PingTexture, PostProcessPass.ShaderConstants._PongTexture, gaussianDepthOfField, 2);
			this.Blit(cmd, PostProcessPass.ShaderConstants._PongTexture, this.BlitDstDiscardContent(cmd, PostProcessPass.ShaderConstants._PingTexture), gaussianDepthOfField, 3);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, PostProcessPass.ShaderConstants._PingTexture);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._FullCoCTexture, PostProcessPass.ShaderConstants._FullCoCTexture);
			this.Blit(cmd, source, this.BlitDstDiscardContent(cmd, destination), gaussianDepthOfField, 4);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._FullCoCTexture);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._HalfCoCTexture);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._PingTexture);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._PongTexture);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000376FC File Offset: 0x000358FC
		private void PrepareBokehKernel(float maxRadius, float rcpAspect)
		{
			if (this.m_BokehKernel == null)
			{
				this.m_BokehKernel = new Vector4[42];
			}
			int num = 0;
			float num2 = (float)this.m_DepthOfField.bladeCount.value;
			float num3 = 1f - this.m_DepthOfField.bladeCurvature.value;
			float num4 = this.m_DepthOfField.bladeRotation.value * 0.017453292f;
			for (int i = 1; i < 4; i++)
			{
				float num5 = 0.14285715f;
				float num6 = ((float)i + num5) / (3f + num5);
				int num7 = i * 7;
				for (int j = 0; j < num7; j++)
				{
					float num8 = 6.2831855f * (float)j / (float)num7;
					float num9 = Mathf.Cos(3.1415927f / num2);
					float num10 = Mathf.Cos(num8 - 6.2831855f / num2 * Mathf.Floor((num2 * num8 + 3.1415927f) / 6.2831855f));
					float num11 = num6 * Mathf.Pow(num9 / num10, num3);
					float num12 = num11 * Mathf.Cos(num8 - num4);
					float num13 = num11 * Mathf.Sin(num8 - num4);
					float num14 = num12 * maxRadius;
					float num15 = num13 * maxRadius;
					float num16 = num14 * num14;
					float num17 = num15 * num15;
					float num18 = Mathf.Sqrt(num16 + num17);
					float num19 = num14 * rcpAspect;
					this.m_BokehKernel[num] = new Vector4(num14, num15, num18, num19);
					num++;
				}
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00037857 File Offset: 0x00035A57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float GetMaxBokehRadiusInPixels(float viewportHeight)
		{
			return Mathf.Min(0.05f, 14f / viewportHeight);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0003786C File Offset: 0x00035A6C
		private void DoBokehDepthOfField(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Rect pixelRect)
		{
			int num = 2;
			Material bokehDepthOfField = this.m_Materials.bokehDepthOfField;
			int num2 = this.m_Descriptor.width / num;
			int num3 = this.m_Descriptor.height / num;
			float num4 = this.m_DepthOfField.focalLength.value / 1000f;
			float num5 = this.m_DepthOfField.focalLength.value / this.m_DepthOfField.aperture.value;
			float value = this.m_DepthOfField.focusDistance.value;
			float num6 = num5 * num4 / (value - num4);
			float maxBokehRadiusInPixels = PostProcessPass.GetMaxBokehRadiusInPixels((float)this.m_Descriptor.height);
			float num7 = 1f / ((float)num2 / (float)num3);
			CoreUtils.SetKeyword(bokehDepthOfField, "_USE_FAST_SRGB_LINEAR_CONVERSION", this.m_UseFastSRGBLinearConversion);
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._CoCParams, new Vector4(value, num6, maxBokehRadiusInPixels, num7));
			int hashCode = this.m_DepthOfField.GetHashCode();
			if (hashCode != this.m_BokehHash || maxBokehRadiusInPixels != this.m_BokehMaxRadius || num7 != this.m_BokehRCPAspect)
			{
				this.m_BokehHash = hashCode;
				this.m_BokehMaxRadius = maxBokehRadiusInPixels;
				this.m_BokehRCPAspect = num7;
				this.PrepareBokehKernel(maxBokehRadiusInPixels, num7);
			}
			cmd.SetGlobalVectorArray(PostProcessPass.ShaderConstants._BokehKernel, this.m_BokehKernel);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._FullCoCTexture, this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.R8_UNorm, 0), FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._PingTexture, this.GetCompatibleDescriptor(num2, num3, GraphicsFormat.R16G16B16A16_SFloat, 0), FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._PongTexture, this.GetCompatibleDescriptor(num2, num3, GraphicsFormat.R16G16B16A16_SFloat, 0), FilterMode.Bilinear);
			PostProcessUtils.SetSourceSize(cmd, this.m_Descriptor);
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._DownSampleScaleFactor, new Vector4(1f / (float)num, 1f / (float)num, (float)num, (float)num));
			float num8 = 1f / (float)this.m_Descriptor.height * (float)num;
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._BokehConstants, new Vector4(num8, num8 * 2f));
			this.Blit(cmd, source, PostProcessPass.ShaderConstants._FullCoCTexture, bokehDepthOfField, 0);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._FullCoCTexture, PostProcessPass.ShaderConstants._FullCoCTexture);
			this.Blit(cmd, source, PostProcessPass.ShaderConstants._PingTexture, bokehDepthOfField, 1);
			this.Blit(cmd, PostProcessPass.ShaderConstants._PingTexture, PostProcessPass.ShaderConstants._PongTexture, bokehDepthOfField, 2);
			this.Blit(cmd, PostProcessPass.ShaderConstants._PongTexture, this.BlitDstDiscardContent(cmd, PostProcessPass.ShaderConstants._PingTexture), bokehDepthOfField, 3);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._DofTexture, PostProcessPass.ShaderConstants._PingTexture);
			this.Blit(cmd, source, this.BlitDstDiscardContent(cmd, destination), bokehDepthOfField, 4);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._FullCoCTexture);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._PingTexture);
			cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._PongTexture);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00037B2C File Offset: 0x00035D2C
		private static float GetLensFlareLightAttenuation(Light light, Camera cam, Vector3 wo)
		{
			if (!(light != null))
			{
				return 1f;
			}
			switch (light.type)
			{
			case LightType.Spot:
				return LensFlareCommonSRP.ShapeAttenuationSpotConeLight(light.transform.forward, wo, light.spotAngle, light.innerSpotAngle / 180f);
			case LightType.Directional:
				return LensFlareCommonSRP.ShapeAttenuationDirLight(light.transform.forward, wo);
			case LightType.Point:
				return LensFlareCommonSRP.ShapeAttenuationPointLight();
			default:
				return 1f;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00037BA4 File Offset: 0x00035DA4
		private void DoLensFlareDatadriven(Camera camera, CommandBuffer cmd, RenderTargetIdentifier source, bool usePanini, float paniniDistance, float paniniCropToFit)
		{
			Matrix4x4 worldToCameraMatrix = camera.worldToCameraMatrix;
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
			worldToCameraMatrix.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
			Matrix4x4 matrix4x = gpuprojectionMatrix * camera.worldToCameraMatrix;
			LensFlareCommonSRP.DoLensFlareDataDrivenCommon(this.m_Materials.lensFlareDataDriven, LensFlareCommonSRP.Instance, camera, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height, usePanini, paniniDistance, paniniCropToFit, true, camera.transform.position, matrix4x, cmd, source, (Light light, Camera cam, Vector3 wo) => PostProcessPass.GetLensFlareLightAttenuation(light, cam, wo), PostProcessPass.ShaderConstants._FlareOcclusionTex, PostProcessPass.ShaderConstants._FlareOcclusionIndex, PostProcessPass.ShaderConstants._FlareTex, PostProcessPass.ShaderConstants._FlareColorValue, PostProcessPass.ShaderConstants._FlareData0, PostProcessPass.ShaderConstants._FlareData1, PostProcessPass.ShaderConstants._FlareData2, PostProcessPass.ShaderConstants._FlareData3, PostProcessPass.ShaderConstants._FlareData4, false);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00037C84 File Offset: 0x00035E84
		private void UpdateMotionBlurMatrices(ref Material material, Camera camera, XRPass xr)
		{
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
			if (xr.enabled && xr.singlePassEnabled)
			{
				material.SetMatrixArray(PostProcessPass.kShaderPropertyId_ViewProjMStereo, motionVectorsPersistentData.viewProjectionStereo);
				if (this.m_ResetHistory)
				{
					material.SetMatrixArray(PostProcessPass.kShaderPropertyId_PrevViewProjMStereo, motionVectorsPersistentData.viewProjectionStereo);
					return;
				}
				material.SetMatrixArray(PostProcessPass.kShaderPropertyId_PrevViewProjMStereo, motionVectorsPersistentData.previousViewProjectionStereo);
				return;
			}
			else
			{
				if (xr.enabled)
				{
					int multipassId = xr.multipassId;
				}
				material.SetMatrix(PostProcessPass.kShaderPropertyId_ViewProjM, motionVectorsPersistentData.viewProjection);
				if (this.m_ResetHistory)
				{
					material.SetMatrix(PostProcessPass.kShaderPropertyId_PrevViewProjM, motionVectorsPersistentData.viewProjection);
					return;
				}
				material.SetMatrix(PostProcessPass.kShaderPropertyId_PrevViewProjM, motionVectorsPersistentData.previousViewProjection);
				return;
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00037D48 File Offset: 0x00035F48
		private void DoMotionBlur(CameraData cameraData, CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			Material cameraMotionBlur = this.m_Materials.cameraMotionBlur;
			this.UpdateMotionBlurMatrices(ref cameraMotionBlur, cameraData.camera, cameraData.xr);
			cameraMotionBlur.SetFloat("_Intensity", this.m_MotionBlur.intensity.value);
			cameraMotionBlur.SetFloat("_Clamp", this.m_MotionBlur.clamp.value);
			PostProcessUtils.SetSourceSize(cmd, this.m_Descriptor);
			this.Blit(cmd, source, this.BlitDstDiscardContent(cmd, destination), cameraMotionBlur, (int)this.m_MotionBlur.quality.value);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00037DE0 File Offset: 0x00035FE0
		private void DoPaniniProjection(Camera camera, CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			float value = this.m_PaniniProjection.distance.value;
			Vector2 vector = this.CalcViewExtents(camera);
			Vector2 vector2 = this.CalcCropExtents(camera, value);
			float num = vector2.x / vector.x;
			float num2 = vector2.y / vector.y;
			float num3 = Mathf.Min(num, num2);
			float num4 = value;
			float num5 = Mathf.Lerp(1f, Mathf.Clamp01(num3), this.m_PaniniProjection.cropToFit.value);
			Material paniniProjection = this.m_Materials.paniniProjection;
			paniniProjection.SetVector(PostProcessPass.ShaderConstants._Params, new Vector4(vector.x, vector.y, num4, num5));
			paniniProjection.EnableKeyword((1f - Mathf.Abs(num4) > float.Epsilon) ? "_GENERIC" : "_UNIT_DISTANCE");
			this.Blit(cmd, source, this.BlitDstDiscardContent(cmd, destination), paniniProjection, 0);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00037EC8 File Offset: 0x000360C8
		private Vector2 CalcViewExtents(Camera camera)
		{
			float num = camera.fieldOfView * 0.017453292f;
			float num2 = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
			float num3 = Mathf.Tan(0.5f * num);
			return new Vector2(num2 * num3, num3);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00037F10 File Offset: 0x00036110
		private Vector2 CalcCropExtents(Camera camera, float d)
		{
			float num = 1f + d;
			Vector2 vector = this.CalcViewExtents(camera);
			float num2 = Mathf.Sqrt(vector.x * vector.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return vector * num3 * (num / num4);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00037F64 File Offset: 0x00036164
		private void SetupBloom(CommandBuffer cmd, RenderTargetIdentifier source, Material uberMaterial)
		{
			int num = this.m_Descriptor.width >> 1;
			int num2 = this.m_Descriptor.height >> 1;
			int num3 = Mathf.Clamp(Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(num, num2), 2f) - 1f) - this.m_Bloom.skipIterations.value, 1, 16);
			float value = this.m_Bloom.clamp.value;
			float num4 = Mathf.GammaToLinearSpace(this.m_Bloom.threshold.value);
			float num5 = num4 * 0.5f;
			float num6 = Mathf.Lerp(0.05f, 0.95f, this.m_Bloom.scatter.value);
			Material bloom = this.m_Materials.bloom;
			bloom.SetVector(PostProcessPass.ShaderConstants._Params, new Vector4(num6, value, num4, num5));
			CoreUtils.SetKeyword(bloom, "_BLOOM_HQ", this.m_Bloom.highQualityFiltering.value);
			CoreUtils.SetKeyword(bloom, "_USE_RGBM", this.m_UseRGBM);
			RenderTextureDescriptor compatibleDescriptor = this.GetCompatibleDescriptor(num, num2, this.m_DefaultHDRFormat, 0);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._BloomMipDown[0], compatibleDescriptor, FilterMode.Bilinear);
			cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._BloomMipUp[0], compatibleDescriptor, FilterMode.Bilinear);
			this.Blit(cmd, source, PostProcessPass.ShaderConstants._BloomMipDown[0], bloom, 0);
			int num7 = PostProcessPass.ShaderConstants._BloomMipDown[0];
			for (int i = 1; i < num3; i++)
			{
				num = Mathf.Max(1, num >> 1);
				num2 = Mathf.Max(1, num2 >> 1);
				int num8 = PostProcessPass.ShaderConstants._BloomMipDown[i];
				int num9 = PostProcessPass.ShaderConstants._BloomMipUp[i];
				compatibleDescriptor.width = num;
				compatibleDescriptor.height = num2;
				cmd.GetTemporaryRT(num8, compatibleDescriptor, FilterMode.Bilinear);
				cmd.GetTemporaryRT(num9, compatibleDescriptor, FilterMode.Bilinear);
				this.Blit(cmd, num7, num9, bloom, 1);
				this.Blit(cmd, num9, num8, bloom, 2);
				num7 = num8;
			}
			for (int j = num3 - 2; j >= 0; j--)
			{
				int num10 = ((j == num3 - 2) ? PostProcessPass.ShaderConstants._BloomMipDown[j + 1] : PostProcessPass.ShaderConstants._BloomMipUp[j + 1]);
				int num11 = PostProcessPass.ShaderConstants._BloomMipDown[j];
				int num12 = PostProcessPass.ShaderConstants._BloomMipUp[j];
				cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._SourceTexLowMip, num10);
				this.Blit(cmd, num11, this.BlitDstDiscardContent(cmd, num12), bloom, 3);
			}
			for (int k = 0; k < num3; k++)
			{
				cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._BloomMipDown[k]);
				if (k > 0)
				{
					cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._BloomMipUp[k]);
				}
			}
			Color color = this.m_Bloom.tint.value.linear;
			float num13 = ColorUtils.Luminance(in color);
			color = ((num13 > 0f) ? (color * (1f / num13)) : Color.white);
			Vector4 vector = new Vector4(this.m_Bloom.intensity.value, color.r, color.g, color.b);
			uberMaterial.SetVector(PostProcessPass.ShaderConstants._Bloom_Params, vector);
			uberMaterial.SetFloat(PostProcessPass.ShaderConstants._Bloom_RGBM, this.m_UseRGBM ? 1f : 0f);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._Bloom_Texture, PostProcessPass.ShaderConstants._BloomMipUp[0]);
			Texture texture = ((this.m_Bloom.dirtTexture.value == null) ? Texture2D.blackTexture : this.m_Bloom.dirtTexture.value);
			float num14 = (float)texture.width / (float)texture.height;
			float num15 = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
			Vector4 vector2 = new Vector4(1f, 1f, 0f, 0f);
			float value2 = this.m_Bloom.dirtIntensity.value;
			if (num14 > num15)
			{
				vector2.x = num15 / num14;
				vector2.z = (1f - vector2.x) * 0.5f;
			}
			else if (num15 > num14)
			{
				vector2.y = num14 / num15;
				vector2.w = (1f - vector2.y) * 0.5f;
			}
			uberMaterial.SetVector(PostProcessPass.ShaderConstants._LensDirt_Params, vector2);
			uberMaterial.SetFloat(PostProcessPass.ShaderConstants._LensDirt_Intensity, value2);
			uberMaterial.SetTexture(PostProcessPass.ShaderConstants._LensDirt_Texture, texture);
			if (this.m_Bloom.highQualityFiltering.value)
			{
				uberMaterial.EnableKeyword((value2 > 0f) ? "_BLOOM_HQ_DIRT" : "_BLOOM_HQ");
				return;
			}
			uberMaterial.EnableKeyword((value2 > 0f) ? "_BLOOM_LQ_DIRT" : "_BLOOM_LQ");
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0003840C File Offset: 0x0003660C
		private void SetupLensDistortion(Material material, bool isSceneView)
		{
			float num = 1.6f * Mathf.Max(Mathf.Abs(this.m_LensDistortion.intensity.value * 100f), 1f);
			float num2 = 0.017453292f * Mathf.Min(160f, num);
			float num3 = 2f * Mathf.Tan(num2 * 0.5f);
			Vector2 vector = this.m_LensDistortion.center.value * 2f - Vector2.one;
			Vector4 vector2 = new Vector4(vector.x, vector.y, Mathf.Max(this.m_LensDistortion.xMultiplier.value, 0.0001f), Mathf.Max(this.m_LensDistortion.yMultiplier.value, 0.0001f));
			Vector4 vector3 = new Vector4((this.m_LensDistortion.intensity.value >= 0f) ? num2 : (1f / num2), num3, 1f / this.m_LensDistortion.scale.value, this.m_LensDistortion.intensity.value * 100f);
			material.SetVector(PostProcessPass.ShaderConstants._Distortion_Params1, vector2);
			material.SetVector(PostProcessPass.ShaderConstants._Distortion_Params2, vector3);
			if (this.m_LensDistortion.IsActive() && !isSceneView)
			{
				material.EnableKeyword("_DISTORTION");
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00038560 File Offset: 0x00036760
		private void SetupChromaticAberration(Material material)
		{
			material.SetFloat(PostProcessPass.ShaderConstants._Chroma_Params, this.m_ChromaticAberration.intensity.value * 0.05f);
			if (this.m_ChromaticAberration.IsActive())
			{
				material.EnableKeyword("_CHROMATIC_ABERRATION");
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003859C File Offset: 0x0003679C
		private void SetupVignette(Material material)
		{
			Color value = this.m_Vignette.color.value;
			Vector2 value2 = this.m_Vignette.center.value;
			float num = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
			Vector4 vector = new Vector4(value.r, value.g, value.b, this.m_Vignette.rounded.value ? num : 1f);
			Vector4 vector2 = new Vector4(value2.x, value2.y, this.m_Vignette.intensity.value * 3f, this.m_Vignette.smoothness.value * 5f);
			material.SetVector(PostProcessPass.ShaderConstants._Vignette_Params1, vector);
			material.SetVector(PostProcessPass.ShaderConstants._Vignette_Params2, vector2);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00038670 File Offset: 0x00036870
		private void SetupColorGrading(CommandBuffer cmd, ref RenderingData renderingData, Material material)
		{
			bool flag = renderingData.postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;
			int lutSize = renderingData.postProcessingData.lutSize;
			int num = lutSize * lutSize;
			float num2 = Mathf.Pow(2f, this.m_ColorAdjustments.postExposure.value);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._InternalLut, this.m_InternalLut.Identifier());
			material.SetVector(PostProcessPass.ShaderConstants._Lut_Params, new Vector4(1f / (float)num, 1f / (float)lutSize, (float)lutSize - 1f, num2));
			material.SetTexture(PostProcessPass.ShaderConstants._UserLut, this.m_ColorLookup.texture.value);
			material.SetVector(PostProcessPass.ShaderConstants._UserLut_Params, (!this.m_ColorLookup.IsActive()) ? Vector4.zero : new Vector4(1f / (float)this.m_ColorLookup.texture.value.width, 1f / (float)this.m_ColorLookup.texture.value.height, (float)this.m_ColorLookup.texture.value.height - 1f, this.m_ColorLookup.contribution.value));
			if (flag)
			{
				material.EnableKeyword("_HDR_GRADING");
				return;
			}
			TonemappingMode value = this.m_Tonemapping.mode.value;
			if (value == TonemappingMode.Neutral)
			{
				material.EnableKeyword("_TONEMAP_NEUTRAL");
				return;
			}
			if (value != TonemappingMode.ACES)
			{
				return;
			}
			material.EnableKeyword("_TONEMAP_ACES");
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000387D7 File Offset: 0x000369D7
		private void SetupGrain(in CameraData cameraData, Material material)
		{
			if (!this.m_HasFinalPass && this.m_FilmGrain.IsActive())
			{
				material.EnableKeyword("_FILM_GRAIN");
				PostProcessUtils.ConfigureFilmGrain(this.m_Data, this.m_FilmGrain, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00038818 File Offset: 0x00036A18
		private void SetupDithering(in CameraData cameraData, Material material)
		{
			if (!this.m_HasFinalPass && cameraData.isDitheringEnabled)
			{
				material.EnableKeyword("_DITHERING");
				this.m_DitheringTextureIndex = PostProcessUtils.ConfigureDithering(this.m_Data, this.m_DitheringTextureIndex, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00038864 File Offset: 0x00036A64
		private void RenderFinalPass(CommandBuffer cmd, ref RenderingData renderingData)
		{
			ref CameraData ptr = ref renderingData.cameraData;
			Material finalPass = this.m_Materials.finalPass;
			finalPass.shaderKeywords = null;
			PostProcessUtils.SetSourceSize(cmd, ptr.cameraTargetDescriptor);
			this.SetupGrain(in ptr, finalPass);
			this.SetupDithering(in ptr, finalPass);
			if (this.RequireSRGBConversionBlitToBackBuffer(ptr))
			{
				finalPass.EnableKeyword("_LINEAR_TO_SRGB_CONVERSION");
			}
			if (!this.m_UseSwapBuffer)
			{
				cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, this.m_Source);
			}
			else if (this.m_Source == ptr.renderer.GetCameraColorFrontBuffer(cmd))
			{
				this.m_Source = ptr.renderer.cameraColorTarget;
			}
			cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, this.m_Source);
			RenderBufferLoadAction renderBufferLoadAction = (ptr.isDefaultViewport ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = ptr.antialiasing == AntialiasingMode.FastApproximateAntialiasing;
			if (ptr.imageScalingMode != ImageScalingMode.None)
			{
				bool flag4 = ptr.imageScalingMode == ImageScalingMode.Upscaling && ptr.upscalingFilter == ImageUpscalingFilter.FSR;
				bool flag5 = flag3 || flag4;
				RenderTextureDescriptor cameraTargetDescriptor = ptr.cameraTargetDescriptor;
				cameraTargetDescriptor.msaaSamples = 1;
				cameraTargetDescriptor.depthBufferBits = 0;
				cameraTargetDescriptor.graphicsFormat = UniversalRenderPipeline.MakeUnormRenderTextureGraphicsFormat();
				this.m_Materials.scalingSetup.shaderKeywords = null;
				RenderTargetIdentifier renderTargetIdentifier = this.m_Source;
				if (flag5)
				{
					if (flag3)
					{
						this.m_Materials.scalingSetup.EnableKeyword("_FXAA");
					}
					if (flag4)
					{
						this.m_Materials.scalingSetup.EnableKeyword("_GAMMA_20");
					}
					cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._ScalingSetupTexture, cameraTargetDescriptor, FilterMode.Point);
					flag = true;
					this.Blit(cmd, this.m_Source, PostProcessPass.ShaderConstants._ScalingSetupTexture, this.m_Materials.scalingSetup, 0);
					cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, PostProcessPass.ShaderConstants._ScalingSetupTexture);
					renderTargetIdentifier = PostProcessPass.ShaderConstants._ScalingSetupTexture;
				}
				ImageScalingMode imageScalingMode = ptr.imageScalingMode;
				if (imageScalingMode != ImageScalingMode.Upscaling)
				{
					if (imageScalingMode != ImageScalingMode.Downscaling)
					{
					}
				}
				else
				{
					switch (ptr.upscalingFilter)
					{
					case ImageUpscalingFilter.Point:
						finalPass.EnableKeyword("_POINT_SAMPLING");
						break;
					case ImageUpscalingFilter.FSR:
					{
						this.m_Materials.easu.shaderKeywords = null;
						RenderTextureDescriptor renderTextureDescriptor = cameraTargetDescriptor;
						renderTextureDescriptor.width = ptr.pixelWidth;
						renderTextureDescriptor.height = ptr.pixelHeight;
						cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._UpscaledTexture, renderTextureDescriptor, FilterMode.Point);
						flag2 = true;
						Vector2 vector = new Vector2((float)ptr.cameraTargetDescriptor.width, (float)ptr.cameraTargetDescriptor.height);
						Vector2 vector2 = new Vector2((float)ptr.pixelWidth, (float)ptr.pixelHeight);
						FSRUtils.SetEasuConstants(cmd, vector, vector, vector2);
						this.Blit(cmd, renderTargetIdentifier, PostProcessPass.ShaderConstants._UpscaledTexture, this.m_Materials.easu, 0);
						float num = (ptr.fsrOverrideSharpness ? ptr.fsrSharpness : 0.92f);
						if (ptr.fsrSharpness > 0f)
						{
							finalPass.EnableKeyword("_RCAS");
							FSRUtils.SetRcasConstantsLinear(cmd, num);
						}
						cmd.SetGlobalTexture(ShaderPropertyId.sourceTex, PostProcessPass.ShaderConstants._UpscaledTexture);
						PostProcessUtils.SetSourceSize(cmd, renderTextureDescriptor);
						break;
					}
					}
				}
			}
			else if (flag3)
			{
				finalPass.EnableKeyword("_FXAA");
			}
			RenderTargetHandle cameraTarget = RenderTargetHandle.GetCameraTarget(ptr.xr);
			if (ptr.xr.enabled)
			{
				RenderTargetIdentifier renderTargetIdentifier2 = cameraTarget.Identifier();
				Vector4 vector3 = ((renderTargetIdentifier2 == ptr.xr.renderTarget && !ptr.xr.renderTargetIsRenderTexture && SystemInfo.graphicsUVStartsAtTop) ? new Vector4(1f, -1f, 0f, 1f) : new Vector4(1f, 1f, 0f, 0f));
				cmd.SetRenderTarget(new RenderTargetIdentifier(renderTargetIdentifier2, 0, CubemapFace.Unknown, -1), renderBufferLoadAction, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				cmd.SetViewport(ptr.pixelRect);
				cmd.SetGlobalVector(ShaderPropertyId.scaleBias, vector3);
				cmd.DrawProcedural(Matrix4x4.identity, finalPass, 0, MeshTopology.Quads, 4, 1, null);
			}
			else
			{
				RenderTargetIdentifier renderTargetIdentifier3 = ((ptr.targetTexture != null) ? new RenderTargetIdentifier(ptr.targetTexture) : cameraTarget.Identifier());
				cmd.SetRenderTarget(renderTargetIdentifier3, renderBufferLoadAction, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
				cmd.SetViewport(ptr.pixelRect);
				cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, finalPass);
				cmd.SetViewProjectionMatrices(ptr.camera.worldToCameraMatrix, ptr.camera.projectionMatrix);
				ptr.renderer.ConfigureCameraTarget(renderTargetIdentifier3, renderTargetIdentifier3);
			}
			if (flag2)
			{
				cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._UpscaledTexture);
			}
			if (flag)
			{
				cmd.ReleaseTemporaryRT(PostProcessPass.ShaderConstants._ScalingSetupTexture);
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00038D4F File Offset: 0x00036F4F
		[CompilerGenerated]
		private RenderTargetIdentifier <Render>g__GetSource|57_0(ref PostProcessPass.<>c__DisplayClass57_0 A_1)
		{
			return A_1.source;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00038D58 File Offset: 0x00036F58
		[CompilerGenerated]
		private RenderTargetIdentifier <Render>g__GetDestination|57_1(ref PostProcessPass.<>c__DisplayClass57_0 A_1)
		{
			if (this.m_UseSwapBuffer)
			{
				return A_1.destination;
			}
			if (A_1.destination == -1)
			{
				A_1.cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._TempTarget, this.GetCompatibleDescriptor(), FilterMode.Bilinear);
				A_1.destination = PostProcessPass.ShaderConstants._TempTarget;
				A_1.tempTargetUsed = true;
			}
			else if (A_1.destination == this.m_Source && this.m_Descriptor.msaaSamples > 1)
			{
				A_1.cmd.GetTemporaryRT(PostProcessPass.ShaderConstants._TempTarget2, this.GetCompatibleDescriptor(), FilterMode.Bilinear);
				A_1.destination = PostProcessPass.ShaderConstants._TempTarget2;
				A_1.tempTarget2Used = true;
			}
			return A_1.destination;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00038E0C File Offset: 0x0003700C
		[CompilerGenerated]
		private void <Render>g__Swap|57_2(ref ScriptableRenderer r, ref PostProcessPass.<>c__DisplayClass57_0 A_2)
		{
			int num = A_2.amountOfPassesRemaining - 1;
			A_2.amountOfPassesRemaining = num;
			if (this.m_UseSwapBuffer)
			{
				if (A_2.amountOfPassesRemaining == 0 && !this.m_HasFinalPass)
				{
					r.EnableSwapBufferMSAA(true);
				}
				r.SwapColorBuffer(A_2.cmd);
				A_2.source = r.cameraColorTarget;
				A_2.destination = r.GetCameraColorFrontBuffer(A_2.cmd);
				return;
			}
			CoreUtils.Swap<RenderTargetIdentifier>(ref A_2.source, ref A_2.destination);
		}

		// Token: 0x040007D0 RID: 2000
		private RenderTextureDescriptor m_Descriptor;

		// Token: 0x040007D1 RID: 2001
		private RenderTargetIdentifier m_Source;

		// Token: 0x040007D2 RID: 2002
		private RenderTargetHandle m_Destination;

		// Token: 0x040007D3 RID: 2003
		private RenderTargetHandle m_Depth;

		// Token: 0x040007D4 RID: 2004
		private RenderTargetHandle m_InternalLut;

		// Token: 0x040007D5 RID: 2005
		private const string k_RenderPostProcessingTag = "Render PostProcessing Effects";

		// Token: 0x040007D6 RID: 2006
		private const string k_RenderFinalPostProcessingTag = "Render Final PostProcessing Pass";

		// Token: 0x040007D7 RID: 2007
		private static readonly ProfilingSampler m_ProfilingRenderPostProcessing = new ProfilingSampler("Render PostProcessing Effects");

		// Token: 0x040007D8 RID: 2008
		private static readonly ProfilingSampler m_ProfilingRenderFinalPostProcessing = new ProfilingSampler("Render Final PostProcessing Pass");

		// Token: 0x040007D9 RID: 2009
		private PostProcessPass.MaterialLibrary m_Materials;

		// Token: 0x040007DA RID: 2010
		private PostProcessData m_Data;

		// Token: 0x040007DB RID: 2011
		private DepthOfField m_DepthOfField;

		// Token: 0x040007DC RID: 2012
		private MotionBlur m_MotionBlur;

		// Token: 0x040007DD RID: 2013
		private PaniniProjection m_PaniniProjection;

		// Token: 0x040007DE RID: 2014
		private Bloom m_Bloom;

		// Token: 0x040007DF RID: 2015
		private LensDistortion m_LensDistortion;

		// Token: 0x040007E0 RID: 2016
		private ChromaticAberration m_ChromaticAberration;

		// Token: 0x040007E1 RID: 2017
		private Vignette m_Vignette;

		// Token: 0x040007E2 RID: 2018
		private ColorLookup m_ColorLookup;

		// Token: 0x040007E3 RID: 2019
		private ColorAdjustments m_ColorAdjustments;

		// Token: 0x040007E4 RID: 2020
		private Tonemapping m_Tonemapping;

		// Token: 0x040007E5 RID: 2021
		private FilmGrain m_FilmGrain;

		// Token: 0x040007E6 RID: 2022
		private const int k_MaxPyramidSize = 16;

		// Token: 0x040007E7 RID: 2023
		private readonly GraphicsFormat m_DefaultHDRFormat;

		// Token: 0x040007E8 RID: 2024
		private bool m_UseRGBM;

		// Token: 0x040007E9 RID: 2025
		private readonly GraphicsFormat m_SMAAEdgeFormat;

		// Token: 0x040007EA RID: 2026
		private readonly GraphicsFormat m_GaussianCoCFormat;

		// Token: 0x040007EB RID: 2027
		private bool m_ResetHistory;

		// Token: 0x040007EC RID: 2028
		private int m_DitheringTextureIndex;

		// Token: 0x040007ED RID: 2029
		private RenderTargetIdentifier[] m_MRT2;

		// Token: 0x040007EE RID: 2030
		private Vector4[] m_BokehKernel;

		// Token: 0x040007EF RID: 2031
		private int m_BokehHash;

		// Token: 0x040007F0 RID: 2032
		private float m_BokehMaxRadius;

		// Token: 0x040007F1 RID: 2033
		private float m_BokehRCPAspect;

		// Token: 0x040007F2 RID: 2034
		private bool m_IsFinalPass;

		// Token: 0x040007F3 RID: 2035
		private bool m_HasFinalPass;

		// Token: 0x040007F4 RID: 2036
		private bool m_EnableSRGBConversionIfNeeded;

		// Token: 0x040007F5 RID: 2037
		private bool m_UseDrawProcedural;

		// Token: 0x040007F6 RID: 2038
		private bool m_UseFastSRGBLinearConversion;

		// Token: 0x040007F7 RID: 2039
		private bool m_ResolveToScreen;

		// Token: 0x040007F8 RID: 2040
		private bool m_UseSwapBuffer;

		// Token: 0x040007F9 RID: 2041
		private Material m_BlitMaterial;

		// Token: 0x040007FA RID: 2042
		private static readonly int kShaderPropertyId_ViewProjM = Shader.PropertyToID("_ViewProjM");

		// Token: 0x040007FB RID: 2043
		private static readonly int kShaderPropertyId_PrevViewProjM = Shader.PropertyToID("_PrevViewProjM");

		// Token: 0x040007FC RID: 2044
		private static readonly int kShaderPropertyId_ViewProjMStereo = Shader.PropertyToID("_ViewProjMStereo");

		// Token: 0x040007FD RID: 2045
		private static readonly int kShaderPropertyId_PrevViewProjMStereo = Shader.PropertyToID("_PrevViewProjMStereo");

		// Token: 0x020001AB RID: 427
		private class MaterialLibrary
		{
			// Token: 0x06000A2C RID: 2604 RVA: 0x00042810 File Offset: 0x00040A10
			public MaterialLibrary(PostProcessData data)
			{
				this.stopNaN = this.Load(data.shaders.stopNanPS);
				this.subpixelMorphologicalAntialiasing = this.Load(data.shaders.subpixelMorphologicalAntialiasingPS);
				this.gaussianDepthOfField = this.Load(data.shaders.gaussianDepthOfFieldPS);
				this.bokehDepthOfField = this.Load(data.shaders.bokehDepthOfFieldPS);
				this.cameraMotionBlur = this.Load(data.shaders.cameraMotionBlurPS);
				this.paniniProjection = this.Load(data.shaders.paniniProjectionPS);
				this.bloom = this.Load(data.shaders.bloomPS);
				this.scalingSetup = this.Load(data.shaders.scalingSetupPS);
				this.easu = this.Load(data.shaders.easuPS);
				this.uber = this.Load(data.shaders.uberPostPS);
				this.finalPass = this.Load(data.shaders.finalPostPassPS);
				this.lensFlareDataDriven = this.Load(data.shaders.LensFlareDataDrivenPS);
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x00042938 File Offset: 0x00040B38
			private Material Load(Shader shader)
			{
				if (shader == null)
				{
					Debug.LogErrorFormat("Missing shader. " + base.GetType().DeclaringType.Name + " render pass will not execute. Check for missing reference in the renderer resources.", Array.Empty<object>());
					return null;
				}
				if (!shader.isSupported)
				{
					return null;
				}
				return CoreUtils.CreateEngineMaterial(shader);
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0004298C File Offset: 0x00040B8C
			internal void Cleanup()
			{
				CoreUtils.Destroy(this.stopNaN);
				CoreUtils.Destroy(this.subpixelMorphologicalAntialiasing);
				CoreUtils.Destroy(this.gaussianDepthOfField);
				CoreUtils.Destroy(this.bokehDepthOfField);
				CoreUtils.Destroy(this.cameraMotionBlur);
				CoreUtils.Destroy(this.paniniProjection);
				CoreUtils.Destroy(this.bloom);
				CoreUtils.Destroy(this.scalingSetup);
				CoreUtils.Destroy(this.easu);
				CoreUtils.Destroy(this.uber);
				CoreUtils.Destroy(this.finalPass);
			}

			// Token: 0x04000AE5 RID: 2789
			public readonly Material stopNaN;

			// Token: 0x04000AE6 RID: 2790
			public readonly Material subpixelMorphologicalAntialiasing;

			// Token: 0x04000AE7 RID: 2791
			public readonly Material gaussianDepthOfField;

			// Token: 0x04000AE8 RID: 2792
			public readonly Material bokehDepthOfField;

			// Token: 0x04000AE9 RID: 2793
			public readonly Material cameraMotionBlur;

			// Token: 0x04000AEA RID: 2794
			public readonly Material paniniProjection;

			// Token: 0x04000AEB RID: 2795
			public readonly Material bloom;

			// Token: 0x04000AEC RID: 2796
			public readonly Material scalingSetup;

			// Token: 0x04000AED RID: 2797
			public readonly Material easu;

			// Token: 0x04000AEE RID: 2798
			public readonly Material uber;

			// Token: 0x04000AEF RID: 2799
			public readonly Material finalPass;

			// Token: 0x04000AF0 RID: 2800
			public readonly Material lensFlareDataDriven;
		}

		// Token: 0x020001AC RID: 428
		private static class ShaderConstants
		{
			// Token: 0x04000AF1 RID: 2801
			public static readonly int _TempTarget = Shader.PropertyToID("_TempTarget");

			// Token: 0x04000AF2 RID: 2802
			public static readonly int _TempTarget2 = Shader.PropertyToID("_TempTarget2");

			// Token: 0x04000AF3 RID: 2803
			public static readonly int _StencilRef = Shader.PropertyToID("_StencilRef");

			// Token: 0x04000AF4 RID: 2804
			public static readonly int _StencilMask = Shader.PropertyToID("_StencilMask");

			// Token: 0x04000AF5 RID: 2805
			public static readonly int _FullCoCTexture = Shader.PropertyToID("_FullCoCTexture");

			// Token: 0x04000AF6 RID: 2806
			public static readonly int _HalfCoCTexture = Shader.PropertyToID("_HalfCoCTexture");

			// Token: 0x04000AF7 RID: 2807
			public static readonly int _DofTexture = Shader.PropertyToID("_DofTexture");

			// Token: 0x04000AF8 RID: 2808
			public static readonly int _CoCParams = Shader.PropertyToID("_CoCParams");

			// Token: 0x04000AF9 RID: 2809
			public static readonly int _BokehKernel = Shader.PropertyToID("_BokehKernel");

			// Token: 0x04000AFA RID: 2810
			public static readonly int _BokehConstants = Shader.PropertyToID("_BokehConstants");

			// Token: 0x04000AFB RID: 2811
			public static readonly int _PongTexture = Shader.PropertyToID("_PongTexture");

			// Token: 0x04000AFC RID: 2812
			public static readonly int _PingTexture = Shader.PropertyToID("_PingTexture");

			// Token: 0x04000AFD RID: 2813
			public static readonly int _Metrics = Shader.PropertyToID("_Metrics");

			// Token: 0x04000AFE RID: 2814
			public static readonly int _AreaTexture = Shader.PropertyToID("_AreaTexture");

			// Token: 0x04000AFF RID: 2815
			public static readonly int _SearchTexture = Shader.PropertyToID("_SearchTexture");

			// Token: 0x04000B00 RID: 2816
			public static readonly int _EdgeTexture = Shader.PropertyToID("_EdgeTexture");

			// Token: 0x04000B01 RID: 2817
			public static readonly int _BlendTexture = Shader.PropertyToID("_BlendTexture");

			// Token: 0x04000B02 RID: 2818
			public static readonly int _ColorTexture = Shader.PropertyToID("_ColorTexture");

			// Token: 0x04000B03 RID: 2819
			public static readonly int _Params = Shader.PropertyToID("_Params");

			// Token: 0x04000B04 RID: 2820
			public static readonly int _SourceTexLowMip = Shader.PropertyToID("_SourceTexLowMip");

			// Token: 0x04000B05 RID: 2821
			public static readonly int _Bloom_Params = Shader.PropertyToID("_Bloom_Params");

			// Token: 0x04000B06 RID: 2822
			public static readonly int _Bloom_RGBM = Shader.PropertyToID("_Bloom_RGBM");

			// Token: 0x04000B07 RID: 2823
			public static readonly int _Bloom_Texture = Shader.PropertyToID("_Bloom_Texture");

			// Token: 0x04000B08 RID: 2824
			public static readonly int _LensDirt_Texture = Shader.PropertyToID("_LensDirt_Texture");

			// Token: 0x04000B09 RID: 2825
			public static readonly int _LensDirt_Params = Shader.PropertyToID("_LensDirt_Params");

			// Token: 0x04000B0A RID: 2826
			public static readonly int _LensDirt_Intensity = Shader.PropertyToID("_LensDirt_Intensity");

			// Token: 0x04000B0B RID: 2827
			public static readonly int _Distortion_Params1 = Shader.PropertyToID("_Distortion_Params1");

			// Token: 0x04000B0C RID: 2828
			public static readonly int _Distortion_Params2 = Shader.PropertyToID("_Distortion_Params2");

			// Token: 0x04000B0D RID: 2829
			public static readonly int _Chroma_Params = Shader.PropertyToID("_Chroma_Params");

			// Token: 0x04000B0E RID: 2830
			public static readonly int _Vignette_Params1 = Shader.PropertyToID("_Vignette_Params1");

			// Token: 0x04000B0F RID: 2831
			public static readonly int _Vignette_Params2 = Shader.PropertyToID("_Vignette_Params2");

			// Token: 0x04000B10 RID: 2832
			public static readonly int _Lut_Params = Shader.PropertyToID("_Lut_Params");

			// Token: 0x04000B11 RID: 2833
			public static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");

			// Token: 0x04000B12 RID: 2834
			public static readonly int _InternalLut = Shader.PropertyToID("_InternalLut");

			// Token: 0x04000B13 RID: 2835
			public static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04000B14 RID: 2836
			public static readonly int _DownSampleScaleFactor = Shader.PropertyToID("_DownSampleScaleFactor");

			// Token: 0x04000B15 RID: 2837
			public static readonly int _FlareOcclusionTex = Shader.PropertyToID("_FlareOcclusionTex");

			// Token: 0x04000B16 RID: 2838
			public static readonly int _FlareOcclusionIndex = Shader.PropertyToID("_FlareOcclusionIndex");

			// Token: 0x04000B17 RID: 2839
			public static readonly int _FlareTex = Shader.PropertyToID("_FlareTex");

			// Token: 0x04000B18 RID: 2840
			public static readonly int _FlareColorValue = Shader.PropertyToID("_FlareColorValue");

			// Token: 0x04000B19 RID: 2841
			public static readonly int _FlareData0 = Shader.PropertyToID("_FlareData0");

			// Token: 0x04000B1A RID: 2842
			public static readonly int _FlareData1 = Shader.PropertyToID("_FlareData1");

			// Token: 0x04000B1B RID: 2843
			public static readonly int _FlareData2 = Shader.PropertyToID("_FlareData2");

			// Token: 0x04000B1C RID: 2844
			public static readonly int _FlareData3 = Shader.PropertyToID("_FlareData3");

			// Token: 0x04000B1D RID: 2845
			public static readonly int _FlareData4 = Shader.PropertyToID("_FlareData4");

			// Token: 0x04000B1E RID: 2846
			public static readonly int _FlareData5 = Shader.PropertyToID("_FlareData5");

			// Token: 0x04000B1F RID: 2847
			public static readonly int _FullscreenProjMat = Shader.PropertyToID("_FullscreenProjMat");

			// Token: 0x04000B20 RID: 2848
			public static readonly int _ScalingSetupTexture = Shader.PropertyToID("_ScalingSetupTexture");

			// Token: 0x04000B21 RID: 2849
			public static readonly int _UpscaledTexture = Shader.PropertyToID("_UpscaledTexture");

			// Token: 0x04000B22 RID: 2850
			public static int[] _BloomMipUp;

			// Token: 0x04000B23 RID: 2851
			public static int[] _BloomMipDown;
		}
	}
}
