using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000111 RID: 273
	public class MainLightShadowCasterPass : ScriptableRenderPass
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x00035414 File Offset: 0x00033614
		public MainLightShadowCasterPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("MainLightShadowCasterPass");
			base.renderPassEvent = evt;
			this.m_MainLightShadowMatrices = new Matrix4x4[5];
			this.m_CascadeSlices = new ShadowSliceData[4];
			this.m_CascadeSplitDistances = new Vector4[4];
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._WorldToShadow = Shader.PropertyToID("_MainLightWorldToShadow");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams = Shader.PropertyToID("_MainLightShadowParams");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres0 = Shader.PropertyToID("_CascadeShadowSplitSpheres0");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres1 = Shader.PropertyToID("_CascadeShadowSplitSpheres1");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres2 = Shader.PropertyToID("_CascadeShadowSplitSpheres2");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres3 = Shader.PropertyToID("_CascadeShadowSplitSpheres3");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSphereRadii = Shader.PropertyToID("_CascadeShadowSplitSphereRadii");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset0 = Shader.PropertyToID("_MainLightShadowOffset0");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset1 = Shader.PropertyToID("_MainLightShadowOffset1");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset2 = Shader.PropertyToID("_MainLightShadowOffset2");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset3 = Shader.PropertyToID("_MainLightShadowOffset3");
			MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize = Shader.PropertyToID("_MainLightShadowmapSize");
			this.m_MainLightShadowmap.Init("_MainLightShadowmapTexture");
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00035538 File Offset: 0x00033738
		public bool Setup(ref RenderingData renderingData)
		{
			bool flag;
			using (new ProfilingScope(null, this.m_ProfilingSetupSampler))
			{
				if (!renderingData.shadowData.supportsMainLightShadows)
				{
					flag = this.SetupForEmptyRendering(ref renderingData);
				}
				else
				{
					this.Clear();
					int mainLightIndex = renderingData.lightData.mainLightIndex;
					if (mainLightIndex == -1)
					{
						flag = this.SetupForEmptyRendering(ref renderingData);
					}
					else
					{
						VisibleLight visibleLight = renderingData.lightData.visibleLights[mainLightIndex];
						Light light = visibleLight.light;
						if (light.shadows == LightShadows.None)
						{
							flag = this.SetupForEmptyRendering(ref renderingData);
						}
						else
						{
							if (visibleLight.lightType != LightType.Directional)
							{
								Debug.LogWarning("Only directional lights are supported as main light.");
							}
							Bounds bounds;
							if (!renderingData.cullResults.GetShadowCasterBounds(mainLightIndex, out bounds))
							{
								flag = this.SetupForEmptyRendering(ref renderingData);
							}
							else
							{
								this.m_ShadowCasterCascadesCount = renderingData.shadowData.mainLightShadowCascadesCount;
								int maxTileResolutionInAtlas = ShadowUtils.GetMaxTileResolutionInAtlas(renderingData.shadowData.mainLightShadowmapWidth, renderingData.shadowData.mainLightShadowmapHeight, this.m_ShadowCasterCascadesCount);
								base.renderTargetWidth = renderingData.shadowData.mainLightShadowmapWidth;
								base.renderTargetHeight = ((this.m_ShadowCasterCascadesCount == 2) ? (renderingData.shadowData.mainLightShadowmapHeight >> 1) : renderingData.shadowData.mainLightShadowmapHeight);
								for (int i = 0; i < this.m_ShadowCasterCascadesCount; i++)
								{
									if (!ShadowUtils.ExtractDirectionalLightMatrix(ref renderingData.cullResults, ref renderingData.shadowData, mainLightIndex, i, base.renderTargetWidth, base.renderTargetHeight, maxTileResolutionInAtlas, light.shadowNearPlane, out this.m_CascadeSplitDistances[i], out this.m_CascadeSlices[i]))
									{
										return this.SetupForEmptyRendering(ref renderingData);
									}
								}
								this.m_MainLightShadowmapTexture = ShadowUtils.GetTemporaryShadowTexture(base.renderTargetWidth, base.renderTargetHeight, 16);
								this.m_MaxShadowDistanceSq = renderingData.cameraData.maxShadowDistance * renderingData.cameraData.maxShadowDistance;
								this.m_CascadeBorder = renderingData.shadowData.mainLightShadowCascadeBorder;
								this.m_CreateEmptyShadowmap = false;
								base.useNativeRenderPass = true;
								flag = true;
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003574C File Offset: 0x0003394C
		private bool SetupForEmptyRendering(ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.renderer.stripShadowsOffVariants)
			{
				return false;
			}
			this.m_MainLightShadowmapTexture = ShadowUtils.GetTemporaryShadowTexture(1, 1, 16);
			this.m_CreateEmptyShadowmap = true;
			base.useNativeRenderPass = false;
			return true;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00035780 File Offset: 0x00033980
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			base.ConfigureTarget(new RenderTargetIdentifier(this.m_MainLightShadowmapTexture), this.m_MainLightShadowmapTexture.depthStencilFormat, base.renderTargetWidth, base.renderTargetHeight, 1, true);
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000357B8 File Offset: 0x000339B8
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_CreateEmptyShadowmap)
			{
				this.SetEmptyMainLightCascadeShadowmap(ref context);
				return;
			}
			this.RenderMainLightCascadeShadowmap(ref context, ref renderingData.cullResults, ref renderingData.lightData, ref renderingData.shadowData);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000357E5 File Offset: 0x000339E5
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.m_MainLightShadowmapTexture)
			{
				RenderTexture.ReleaseTemporary(this.m_MainLightShadowmapTexture);
				this.m_MainLightShadowmapTexture = null;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00035814 File Offset: 0x00033A14
		private void Clear()
		{
			this.m_MainLightShadowmapTexture = null;
			for (int i = 0; i < this.m_MainLightShadowMatrices.Length; i++)
			{
				this.m_MainLightShadowMatrices[i] = Matrix4x4.identity;
			}
			for (int j = 0; j < this.m_CascadeSplitDistances.Length; j++)
			{
				this.m_CascadeSplitDistances[j] = new Vector4(0f, 0f, 0f, 0f);
			}
			for (int k = 0; k < this.m_CascadeSlices.Length; k++)
			{
				this.m_CascadeSlices[k].Clear();
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000358A8 File Offset: 0x00033AA8
		private void SetEmptyMainLightCascadeShadowmap(ref ScriptableRenderContext context)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS", true);
			commandBuffer.SetGlobalTexture(this.m_MainLightShadowmap.id, this.m_MainLightShadowmapTexture);
			commandBuffer.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams, new Vector4(1f, 0f, 1f, 0f));
			commandBuffer.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize, new Vector4(1f / (float)this.m_MainLightShadowmapTexture.width, 1f / (float)this.m_MainLightShadowmapTexture.height, (float)this.m_MainLightShadowmapTexture.width, (float)this.m_MainLightShadowmapTexture.height));
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00035960 File Offset: 0x00033B60
		private void RenderMainLightCascadeShadowmap(ref ScriptableRenderContext context, ref CullingResults cullResults, ref LightData lightData, ref ShadowData shadowData)
		{
			int mainLightIndex = lightData.mainLightIndex;
			if (mainLightIndex == -1)
			{
				return;
			}
			VisibleLight visibleLight = lightData.visibleLights[mainLightIndex];
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.MainLightShadow)))
			{
				ShadowDrawingSettings shadowDrawingSettings = new ShadowDrawingSettings(cullResults, mainLightIndex);
				shadowDrawingSettings.useRenderingLayerMaskTest = UniversalRenderPipeline.asset.supportsLightLayers;
				for (int i = 0; i < this.m_ShadowCasterCascadesCount; i++)
				{
					shadowDrawingSettings.splitData = this.m_CascadeSlices[i].splitData;
					Vector4 shadowBias = ShadowUtils.GetShadowBias(ref visibleLight, mainLightIndex, ref shadowData, this.m_CascadeSlices[i].projectionMatrix, (float)this.m_CascadeSlices[i].resolution);
					ShadowUtils.SetupShadowCasterConstantBuffer(commandBuffer, ref visibleLight, shadowBias);
					CoreUtils.SetKeyword(commandBuffer, "_CASTING_PUNCTUAL_LIGHT_SHADOW", false);
					ShadowUtils.RenderShadowSlice(commandBuffer, ref context, ref this.m_CascadeSlices[i], ref shadowDrawingSettings, this.m_CascadeSlices[i].projectionMatrix, this.m_CascadeSlices[i].viewMatrix);
				}
				shadowData.isKeywordSoftShadowsEnabled = visibleLight.light.shadows == LightShadows.Soft && shadowData.supportsSoftShadows;
				CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS", shadowData.mainLightShadowCascadesCount == 1);
				CoreUtils.SetKeyword(commandBuffer, "_MAIN_LIGHT_SHADOWS_CASCADE", shadowData.mainLightShadowCascadesCount > 1);
				CoreUtils.SetKeyword(commandBuffer, "_SHADOWS_SOFT", shadowData.isKeywordSoftShadowsEnabled);
				this.SetupMainLightShadowReceiverConstants(commandBuffer, visibleLight, shadowData.supportsSoftShadows);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00035B14 File Offset: 0x00033D14
		private void SetupMainLightShadowReceiverConstants(CommandBuffer cmd, VisibleLight shadowLight, bool supportsSoftShadows)
		{
			Light light = shadowLight.light;
			bool flag = shadowLight.light.shadows == LightShadows.Soft && supportsSoftShadows;
			int shadowCasterCascadesCount = this.m_ShadowCasterCascadesCount;
			for (int i = 0; i < shadowCasterCascadesCount; i++)
			{
				this.m_MainLightShadowMatrices[i] = this.m_CascadeSlices[i].shadowTransform;
			}
			Matrix4x4 zero = Matrix4x4.zero;
			zero.m22 = (SystemInfo.usesReversedZBuffer ? 1f : 0f);
			for (int j = shadowCasterCascadesCount; j <= 4; j++)
			{
				this.m_MainLightShadowMatrices[j] = zero;
			}
			float num = 1f / (float)base.renderTargetWidth;
			float num2 = 1f / (float)base.renderTargetHeight;
			float num3 = 0.5f * num;
			float num4 = 0.5f * num2;
			float num5 = (flag ? 1f : 0f);
			float num6;
			float num7;
			ShadowUtils.GetScaleAndBiasForLinearDistanceFade(this.m_MaxShadowDistanceSq, this.m_CascadeBorder, out num6, out num7);
			cmd.SetGlobalTexture(this.m_MainLightShadowmap.id, this.m_MainLightShadowmapTexture);
			cmd.SetGlobalMatrixArray(MainLightShadowCasterPass.MainLightShadowConstantBuffer._WorldToShadow, this.m_MainLightShadowMatrices);
			cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowParams, new Vector4(light.shadowStrength, num5, num6, num7));
			if (this.m_ShadowCasterCascadesCount > 1)
			{
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres0, this.m_CascadeSplitDistances[0]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres1, this.m_CascadeSplitDistances[1]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres2, this.m_CascadeSplitDistances[2]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSpheres3, this.m_CascadeSplitDistances[3]);
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._CascadeShadowSplitSphereRadii, new Vector4(this.m_CascadeSplitDistances[0].w * this.m_CascadeSplitDistances[0].w, this.m_CascadeSplitDistances[1].w * this.m_CascadeSplitDistances[1].w, this.m_CascadeSplitDistances[2].w * this.m_CascadeSplitDistances[2].w, this.m_CascadeSplitDistances[3].w * this.m_CascadeSplitDistances[3].w));
			}
			if (supportsSoftShadows)
			{
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset0, new Vector4(-num3, -num4, 0f, 0f));
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset1, new Vector4(num3, -num4, 0f, 0f));
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset2, new Vector4(-num3, num4, 0f, 0f));
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowOffset3, new Vector4(num3, num4, 0f, 0f));
				cmd.SetGlobalVector(MainLightShadowCasterPass.MainLightShadowConstantBuffer._ShadowmapSize, new Vector4(num, num2, (float)base.renderTargetWidth, (float)base.renderTargetHeight));
			}
		}

		// Token: 0x040007BA RID: 1978
		private const int k_MaxCascades = 4;

		// Token: 0x040007BB RID: 1979
		private const int k_ShadowmapBufferBits = 16;

		// Token: 0x040007BC RID: 1980
		private float m_CascadeBorder;

		// Token: 0x040007BD RID: 1981
		private float m_MaxShadowDistanceSq;

		// Token: 0x040007BE RID: 1982
		private int m_ShadowCasterCascadesCount;

		// Token: 0x040007BF RID: 1983
		private RenderTargetHandle m_MainLightShadowmap;

		// Token: 0x040007C0 RID: 1984
		internal RenderTexture m_MainLightShadowmapTexture;

		// Token: 0x040007C1 RID: 1985
		private Matrix4x4[] m_MainLightShadowMatrices;

		// Token: 0x040007C2 RID: 1986
		private ShadowSliceData[] m_CascadeSlices;

		// Token: 0x040007C3 RID: 1987
		private Vector4[] m_CascadeSplitDistances;

		// Token: 0x040007C4 RID: 1988
		private bool m_CreateEmptyShadowmap;

		// Token: 0x040007C5 RID: 1989
		private ProfilingSampler m_ProfilingSetupSampler = new ProfilingSampler("Setup Main Shadowmap");

		// Token: 0x020001AA RID: 426
		private static class MainLightShadowConstantBuffer
		{
			// Token: 0x04000AD9 RID: 2777
			public static int _WorldToShadow;

			// Token: 0x04000ADA RID: 2778
			public static int _ShadowParams;

			// Token: 0x04000ADB RID: 2779
			public static int _CascadeShadowSplitSpheres0;

			// Token: 0x04000ADC RID: 2780
			public static int _CascadeShadowSplitSpheres1;

			// Token: 0x04000ADD RID: 2781
			public static int _CascadeShadowSplitSpheres2;

			// Token: 0x04000ADE RID: 2782
			public static int _CascadeShadowSplitSpheres3;

			// Token: 0x04000ADF RID: 2783
			public static int _CascadeShadowSplitSphereRadii;

			// Token: 0x04000AE0 RID: 2784
			public static int _ShadowOffset0;

			// Token: 0x04000AE1 RID: 2785
			public static int _ShadowOffset1;

			// Token: 0x04000AE2 RID: 2786
			public static int _ShadowOffset2;

			// Token: 0x04000AE3 RID: 2787
			public static int _ShadowOffset3;

			// Token: 0x04000AE4 RID: 2788
			public static int _ShadowmapSize;
		}
	}
}
