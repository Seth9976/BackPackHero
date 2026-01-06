using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000105 RID: 261
	public class ForwardLights
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00031F2B File Offset: 0x0003012B
		public ForwardLights()
			: this(ForwardLights.InitParams.GetDefault())
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00031F38 File Offset: 0x00030138
		internal ForwardLights(ForwardLights.InitParams initParams)
		{
			bool clusteredRendering = initParams.clusteredRendering;
			this.m_UseStructuredBuffer = RenderingUtils.useStructuredBuffer;
			this.m_UseClusteredRendering = initParams.clusteredRendering;
			ForwardLights.LightConstantBuffer._MainLightPosition = Shader.PropertyToID("_MainLightPosition");
			ForwardLights.LightConstantBuffer._MainLightColor = Shader.PropertyToID("_MainLightColor");
			ForwardLights.LightConstantBuffer._MainLightOcclusionProbesChannel = Shader.PropertyToID("_MainLightOcclusionProbes");
			ForwardLights.LightConstantBuffer._MainLightLayerMask = Shader.PropertyToID("_MainLightLayerMask");
			ForwardLights.LightConstantBuffer._AdditionalLightsCount = Shader.PropertyToID("_AdditionalLightsCount");
			if (this.m_UseStructuredBuffer)
			{
				this.m_AdditionalLightsBufferId = Shader.PropertyToID("_AdditionalLightsBuffer");
				this.m_AdditionalLightsIndicesId = Shader.PropertyToID("_AdditionalLightsIndices");
			}
			else
			{
				ForwardLights.LightConstantBuffer._AdditionalLightsPosition = Shader.PropertyToID("_AdditionalLightsPosition");
				ForwardLights.LightConstantBuffer._AdditionalLightsColor = Shader.PropertyToID("_AdditionalLightsColor");
				ForwardLights.LightConstantBuffer._AdditionalLightsAttenuation = Shader.PropertyToID("_AdditionalLightsAttenuation");
				ForwardLights.LightConstantBuffer._AdditionalLightsSpotDir = Shader.PropertyToID("_AdditionalLightsSpotDir");
				ForwardLights.LightConstantBuffer._AdditionalLightOcclusionProbeChannel = Shader.PropertyToID("_AdditionalLightsOcclusionProbes");
				ForwardLights.LightConstantBuffer._AdditionalLightsLayerMasks = Shader.PropertyToID("_AdditionalLightsLayerMasks");
				int maxVisibleAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
				this.m_AdditionalLightPositions = new Vector4[maxVisibleAdditionalLights];
				this.m_AdditionalLightColors = new Vector4[maxVisibleAdditionalLights];
				this.m_AdditionalLightAttenuations = new Vector4[maxVisibleAdditionalLights];
				this.m_AdditionalLightSpotDirections = new Vector4[maxVisibleAdditionalLights];
				this.m_AdditionalLightOcclusionProbeChannels = new Vector4[maxVisibleAdditionalLights];
				this.m_AdditionalLightsLayerMasks = new float[maxVisibleAdditionalLights];
			}
			this.m_LightCookieManager = initParams.lightCookieManager;
			if (this.m_UseClusteredRendering)
			{
				this.m_ZBinBuffer = new ComputeBuffer(UniversalRenderPipeline.maxZBins / 4, UnsafeUtility.SizeOf<float4>(), ComputeBufferType.Constant, ComputeBufferMode.Dynamic);
				this.m_TileBuffer = new ComputeBuffer(UniversalRenderPipeline.maxTileVec4s, UnsafeUtility.SizeOf<float4>(), ComputeBufferType.Constant, ComputeBufferMode.Dynamic);
				this.m_RequestedTileWidth = initParams.tileSize;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000320DC File Offset: 0x000302DC
		internal void ProcessLights(ref RenderingData renderingData)
		{
			if (this.m_UseClusteredRendering)
			{
				Camera camera = renderingData.cameraData.camera;
				int2 @int = math.int2(renderingData.cameraData.pixelWidth, renderingData.cameraData.pixelHeight);
				int num = renderingData.lightData.visibleLights.Length;
				int num2 = 0;
				while (num2 < num && renderingData.lightData.visibleLights[num2].lightType == LightType.Directional)
				{
					num2++;
				}
				if (num2 == num)
				{
					num2 = 0;
				}
				num -= num2;
				this.m_DirectionalLightCount = num2;
				if (renderingData.lightData.mainLightIndex != -1)
				{
					this.m_DirectionalLightCount--;
				}
				NativeArray<VisibleLight> subArray = renderingData.lightData.visibleLights.GetSubArray(num2, num);
				int lightsPerTile = UniversalRenderPipeline.lightsPerTile;
				int num3 = lightsPerTile / 32;
				this.m_ActualTileWidth = this.m_RequestedTileWidth >> 1;
				do
				{
					this.m_ActualTileWidth <<= 1;
					this.m_TileResolution = (@int + this.m_ActualTileWidth - 1) / this.m_ActualTileWidth;
				}
				while (this.m_TileResolution.x * this.m_TileResolution.y * num3 > UniversalRenderPipeline.maxTileVec4s * 4);
				float num4 = math.tan(math.radians(camera.fieldOfView * 0.5f));
				float num5 = num4 * (float)@int.x / (float)@int.y;
				float num6 = (float)UniversalRenderPipeline.maxZBins / (math.sqrt(camera.farClipPlane) - math.sqrt(camera.nearClipPlane));
				this.m_ZBinFactor = num6;
				this.m_ZBinOffset = (int)(math.sqrt(camera.nearClipPlane) * this.m_ZBinFactor);
				int num7 = (int)(math.sqrt(camera.farClipPlane) * this.m_ZBinFactor) - this.m_ZBinOffset;
				num7 = (num7 + 3) / 4 * 4;
				num7 = math.min(UniversalRenderPipeline.maxZBins, num7);
				this.m_ZBins = new NativeArray<ZBin>(num7, Allocator.TempJob, NativeArrayOptions.ClearMemory);
				using (NativeArray<LightMinMaxZ> nativeArray = new NativeArray<LightMinMaxZ>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory))
				{
					using (NativeArray<float> nativeArray2 = new NativeArray<float>(num * 2, Allocator.TempJob, NativeArrayOptions.ClearMemory))
					{
						Matrix4x4 viewMatrix = renderingData.cameraData.GetViewMatrix(0);
						JobHandle jobHandle = new MinMaxZJob
						{
							worldToViewMatrix = viewMatrix,
							lights = subArray,
							minMaxZs = nativeArray,
							meanZs = nativeArray2
						}.ScheduleParallel(num, 32, default(JobHandle));
						using (NativeArray<int> nativeArray3 = new NativeArray<int>(num * 2, Allocator.TempJob, NativeArrayOptions.ClearMemory))
						{
							JobHandle jobHandle2 = new RadixSortJob
							{
								keys = nativeArray2.Reinterpret<uint>(),
								indices = nativeArray3
							}.Schedule(jobHandle);
							NativeArray<VisibleLight> nativeArray4 = new NativeArray<VisibleLight>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory);
							NativeArray<LightMinMaxZ> nativeArray5 = new NativeArray<LightMinMaxZ>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory);
							JobHandle jobHandle3 = new ReorderJob<VisibleLight>
							{
								indices = nativeArray3,
								input = subArray,
								output = nativeArray4
							}.ScheduleParallel(num, 32, jobHandle2);
							JobHandle jobHandle4 = new ReorderJob<LightMinMaxZ>
							{
								indices = nativeArray3,
								input = nativeArray,
								output = nativeArray5
							}.ScheduleParallel(num, 32, jobHandle2);
							JobHandle jobHandle5 = JobHandle.CombineDependencies(jobHandle3, jobHandle4);
							JobHandle.ScheduleBatchedJobs();
							LightExtractionJob lightExtractionJob;
							lightExtractionJob.lights = nativeArray4;
							NativeArray<LightType> nativeArray6 = (lightExtractionJob.lightTypes = new NativeArray<LightType>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory));
							NativeArray<float> nativeArray7 = (lightExtractionJob.radiuses = new NativeArray<float>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory));
							NativeArray<float3> nativeArray8 = (lightExtractionJob.directions = new NativeArray<float3>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory));
							NativeArray<float3> nativeArray9 = (lightExtractionJob.positions = new NativeArray<float3>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory));
							NativeArray<float> nativeArray10 = (lightExtractionJob.coneRadiuses = new NativeArray<float>(num, Allocator.TempJob, NativeArrayOptions.ClearMemory));
							JobHandle jobHandle6 = lightExtractionJob.ScheduleParallel(num, 32, jobHandle5);
							JobHandle jobHandle7 = new ZBinningJob
							{
								bins = this.m_ZBins,
								minMaxZs = nativeArray5,
								binOffset = this.m_ZBinOffset,
								zFactor = this.m_ZBinFactor
							}.ScheduleParallel((num7 + 64 - 1) / 64, 1, jobHandle5);
							nativeArray5.Dispose(jobHandle7);
							int2 int2 = (num3 * this.m_TileResolution + 3) / 4 * 4;
							NativeArray<uint> nativeArray11 = new NativeArray<uint>(int2.y, Allocator.TempJob, NativeArrayOptions.ClearMemory);
							NativeArray<uint> nativeArray12 = new NativeArray<uint>(int2.x, Allocator.TempJob, NativeArrayOptions.ClearMemory);
							SliceCullingJob sliceCullingJob = new SliceCullingJob
							{
								scale = (float)this.m_ActualTileWidth / (float)@int.x,
								viewOrigin = camera.transform.position,
								viewForward = camera.transform.forward,
								viewRight = camera.transform.right * num5,
								viewUp = camera.transform.up * num4,
								lightTypes = nativeArray6,
								radiuses = nativeArray7,
								directions = nativeArray8,
								positions = nativeArray9,
								coneRadiuses = nativeArray10,
								lightsPerTile = lightsPerTile,
								sliceLightMasks = nativeArray12
							};
							JobHandle jobHandle8 = sliceCullingJob.ScheduleParallel(this.m_TileResolution.x, 1, jobHandle6);
							SliceCullingJob sliceCullingJob2 = sliceCullingJob;
							sliceCullingJob2.scale = (float)this.m_ActualTileWidth / (float)@int.y;
							sliceCullingJob2.viewRight = camera.transform.up * num4;
							sliceCullingJob2.viewUp = -camera.transform.right * num5;
							sliceCullingJob2.sliceLightMasks = nativeArray11;
							JobHandle jobHandle9 = JobHandle.CombineDependencies(sliceCullingJob2.ScheduleParallel(this.m_TileResolution.y, 1, jobHandle6), jobHandle8);
							this.m_TileLightMasks = new NativeArray<uint>((this.m_TileResolution.x * this.m_TileResolution.y * num3 + 3) / 4 * 4, Allocator.TempJob, NativeArrayOptions.ClearMemory);
							JobHandle jobHandle10 = new SliceCombineJob
							{
								tileResolution = this.m_TileResolution,
								wordsPerTile = num3,
								sliceLightMasksH = nativeArray11,
								sliceLightMasksV = nativeArray12,
								lightMasks = this.m_TileLightMasks
							}.ScheduleParallel(this.m_TileResolution.y, 1, jobHandle9);
							this.m_CullingHandle = JobHandle.CombineDependencies(jobHandle10, jobHandle7);
							jobHandle5.Complete();
							NativeArray<VisibleLight>.Copy(nativeArray4, 0, renderingData.lightData.visibleLights, num2, num);
							NativeArray<Vector4> nativeArray13 = new NativeArray<Vector4>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
							NativeArray<int> nativeArray14 = new NativeArray<int>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
							NativeArray<int> nativeArray15 = new NativeArray<int>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
							for (int i = 0; i < num; i++)
							{
								nativeArray13[nativeArray3[i]] = renderingData.shadowData.bias[num2 + i];
								nativeArray14[nativeArray3[i]] = renderingData.shadowData.resolution[num2 + i];
								nativeArray15[nativeArray3[i]] = num2 + i;
							}
							for (int j = 0; j < num; j++)
							{
								renderingData.shadowData.bias[j + num2] = nativeArray13[j];
								renderingData.shadowData.resolution[j + num2] = nativeArray14[j];
								renderingData.lightData.originalIndices[j + num2] = nativeArray15[j];
							}
							nativeArray13.Dispose();
							nativeArray14.Dispose();
							nativeArray15.Dispose();
							nativeArray6.Dispose(this.m_CullingHandle);
							nativeArray7.Dispose(this.m_CullingHandle);
							nativeArray8.Dispose(this.m_CullingHandle);
							nativeArray9.Dispose(this.m_CullingHandle);
							nativeArray10.Dispose(this.m_CullingHandle);
							nativeArray4.Dispose(this.m_CullingHandle);
							nativeArray11.Dispose(this.m_CullingHandle);
							nativeArray12.Dispose(this.m_CullingHandle);
							JobHandle.ScheduleBatchedJobs();
						}
					}
				}
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000328F8 File Offset: 0x00030AF8
		public void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			int additionalLightsCount = renderingData.lightData.additionalLightsCount;
			bool shadeAdditionalLightsPerVertex = renderingData.lightData.shadeAdditionalLightsPerVertex;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(null, ForwardLights.m_ProfilingSampler))
			{
				bool useClusteredRendering = this.m_UseClusteredRendering;
				if (useClusteredRendering)
				{
					this.m_CullingHandle.Complete();
					this.m_ZBinBuffer.SetData<float4>(this.m_ZBins.Reinterpret<float4>(UnsafeUtility.SizeOf<ZBin>()), 0, 0, this.m_ZBins.Length / 4);
					this.m_TileBuffer.SetData<float4>(this.m_TileLightMasks.Reinterpret<float4>(UnsafeUtility.SizeOf<uint>()), 0, 0, this.m_TileLightMasks.Length / 4);
					commandBuffer.SetGlobalInteger("_AdditionalLightsDirectionalCount", this.m_DirectionalLightCount);
					commandBuffer.SetGlobalInteger("_AdditionalLightsZBinOffset", this.m_ZBinOffset);
					commandBuffer.SetGlobalFloat("_AdditionalLightsZBinScale", this.m_ZBinFactor);
					commandBuffer.SetGlobalVector("_AdditionalLightsTileScale", renderingData.cameraData.pixelRect.size / (float)this.m_ActualTileWidth);
					commandBuffer.SetGlobalInteger("_AdditionalLightsTileCountX", this.m_TileResolution.x);
					commandBuffer.SetGlobalConstantBuffer(this.m_ZBinBuffer, "AdditionalLightsZBins", 0, this.m_ZBins.Length * 4);
					commandBuffer.SetGlobalConstantBuffer(this.m_TileBuffer, "AdditionalLightsTiles", 0, this.m_TileLightMasks.Length * 4);
					this.m_ZBins.Dispose();
					this.m_TileLightMasks.Dispose();
				}
				this.SetupShaderLightConstants(commandBuffer, ref renderingData);
				bool flag = (renderingData.cameraData.renderer.stripAdditionalLightOffVariants && renderingData.lightData.supportsAdditionalLights) || additionalLightsCount > 0;
				CoreUtils.SetKeyword(commandBuffer, "_ADDITIONAL_LIGHTS_VERTEX", flag && shadeAdditionalLightsPerVertex && !useClusteredRendering);
				CoreUtils.SetKeyword(commandBuffer, "_ADDITIONAL_LIGHTS", flag && !shadeAdditionalLightsPerVertex && !useClusteredRendering);
				CoreUtils.SetKeyword(commandBuffer, "_CLUSTERED_RENDERING", useClusteredRendering);
				bool flag2 = renderingData.lightData.supportsMixedLighting && this.m_MixedLightingSetup == MixedLightingSetup.ShadowMask;
				bool flag3 = flag2 && QualitySettings.shadowmaskMode == ShadowmaskMode.Shadowmask;
				bool flag4 = renderingData.lightData.supportsMixedLighting && this.m_MixedLightingSetup == MixedLightingSetup.Subtractive;
				CoreUtils.SetKeyword(commandBuffer, "LIGHTMAP_SHADOW_MIXING", flag4 || flag3);
				CoreUtils.SetKeyword(commandBuffer, "SHADOWS_SHADOWMASK", flag2);
				CoreUtils.SetKeyword(commandBuffer, "_MIXED_LIGHTING_SUBTRACTIVE", flag4);
				CoreUtils.SetKeyword(commandBuffer, "_REFLECTION_PROBE_BLENDING", renderingData.lightData.reflectionProbeBlending);
				CoreUtils.SetKeyword(commandBuffer, "_REFLECTION_PROBE_BOX_PROJECTION", renderingData.lightData.reflectionProbeBoxProjection);
				bool supportsLightLayers = renderingData.lightData.supportsLightLayers;
				CoreUtils.SetKeyword(commandBuffer, "_LIGHT_LAYERS", supportsLightLayers);
				this.m_LightCookieManager.Setup(context, commandBuffer, ref renderingData.lightData);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00032BDC File Offset: 0x00030DDC
		internal void Cleanup()
		{
			if (this.m_UseClusteredRendering)
			{
				this.m_ZBinBuffer.Dispose();
				this.m_TileBuffer.Dispose();
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00032BFC File Offset: 0x00030DFC
		private void InitializeLightConstants(NativeArray<VisibleLight> lights, int lightIndex, out Vector4 lightPos, out Vector4 lightColor, out Vector4 lightAttenuation, out Vector4 lightSpotDir, out Vector4 lightOcclusionProbeChannel, out uint lightLayerMask)
		{
			UniversalRenderPipeline.InitializeLightConstants_Common(lights, lightIndex, out lightPos, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionProbeChannel);
			lightLayerMask = 0U;
			if (lightIndex < 0)
			{
				return;
			}
			VisibleLight visibleLight = lights[lightIndex];
			Light light = visibleLight.light;
			if (light == null)
			{
				return;
			}
			if (light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed && visibleLight.light.shadows != LightShadows.None && this.m_MixedLightingSetup == MixedLightingSetup.None)
			{
				MixedLightingMode mixedLightingMode = light.bakingOutput.mixedLightingMode;
				if (mixedLightingMode != MixedLightingMode.Subtractive)
				{
					if (mixedLightingMode == MixedLightingMode.Shadowmask)
					{
						this.m_MixedLightingSetup = MixedLightingSetup.ShadowMask;
					}
				}
				else
				{
					this.m_MixedLightingSetup = MixedLightingSetup.Subtractive;
				}
			}
			UniversalAdditionalLightData universalAdditionalLightData = light.GetUniversalAdditionalLightData();
			lightLayerMask = (uint)universalAdditionalLightData.lightLayerMask;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00032C97 File Offset: 0x00030E97
		private void SetupShaderLightConstants(CommandBuffer cmd, ref RenderingData renderingData)
		{
			this.m_MixedLightingSetup = MixedLightingSetup.None;
			this.SetupMainLightConstants(cmd, ref renderingData.lightData);
			this.SetupAdditionalLightConstants(cmd, ref renderingData);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00032CB8 File Offset: 0x00030EB8
		private void SetupMainLightConstants(CommandBuffer cmd, ref LightData lightData)
		{
			Vector4 vector;
			Vector4 vector2;
			Vector4 vector3;
			Vector4 vector4;
			Vector4 vector5;
			uint num;
			this.InitializeLightConstants(lightData.visibleLights, lightData.mainLightIndex, out vector, out vector2, out vector3, out vector4, out vector5, out num);
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._MainLightPosition, vector);
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._MainLightColor, vector2);
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._MainLightOcclusionProbesChannel, vector5);
			cmd.SetGlobalInt(ForwardLights.LightConstantBuffer._MainLightLayerMask, (int)num);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00032D18 File Offset: 0x00030F18
		private void SetupAdditionalLightConstants(CommandBuffer cmd, ref RenderingData renderingData)
		{
			ref LightData ptr = ref renderingData.lightData;
			CullingResults cullResults = renderingData.cullResults;
			NativeArray<VisibleLight> visibleLights = ptr.visibleLights;
			int maxVisibleAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
			int num = this.SetupPerObjectLightIndices(cullResults, ref ptr);
			if (num > 0)
			{
				if (this.m_UseStructuredBuffer)
				{
					NativeArray<ShaderInput.LightData> nativeArray = new NativeArray<ShaderInput.LightData>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
					int num2 = 0;
					int num3 = 0;
					while (num2 < visibleLights.Length && num3 < maxVisibleAdditionalLights)
					{
						VisibleLight visibleLight = visibleLights[num2];
						if (ptr.mainLightIndex != num2)
						{
							ShaderInput.LightData lightData;
							this.InitializeLightConstants(visibleLights, num2, out lightData.position, out lightData.color, out lightData.attenuation, out lightData.spotDirection, out lightData.occlusionProbeChannels, out lightData.layerMask);
							nativeArray[num3] = lightData;
							num3++;
						}
						num2++;
					}
					ComputeBuffer lightDataBuffer = ShaderData.instance.GetLightDataBuffer(num);
					lightDataBuffer.SetData<ShaderInput.LightData>(nativeArray);
					int lightAndReflectionProbeIndexCount = cullResults.lightAndReflectionProbeIndexCount;
					ComputeBuffer lightIndicesBuffer = ShaderData.instance.GetLightIndicesBuffer(lightAndReflectionProbeIndexCount);
					cmd.SetGlobalBuffer(this.m_AdditionalLightsBufferId, lightDataBuffer);
					cmd.SetGlobalBuffer(this.m_AdditionalLightsIndicesId, lightIndicesBuffer);
					nativeArray.Dispose();
				}
				else
				{
					int num4 = 0;
					int num5 = 0;
					while (num4 < visibleLights.Length && num5 < maxVisibleAdditionalLights)
					{
						VisibleLight visibleLight2 = visibleLights[num4];
						if (ptr.mainLightIndex != num4)
						{
							uint num6;
							this.InitializeLightConstants(visibleLights, num4, out this.m_AdditionalLightPositions[num5], out this.m_AdditionalLightColors[num5], out this.m_AdditionalLightAttenuations[num5], out this.m_AdditionalLightSpotDirections[num5], out this.m_AdditionalLightOcclusionProbeChannels[num5], out num6);
							this.m_AdditionalLightsLayerMasks[num5] = math.asfloat(num6);
							num5++;
						}
						num4++;
					}
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsPosition, this.m_AdditionalLightPositions);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsColor, this.m_AdditionalLightColors);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsAttenuation, this.m_AdditionalLightAttenuations);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsSpotDir, this.m_AdditionalLightSpotDirections);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightOcclusionProbeChannel, this.m_AdditionalLightOcclusionProbeChannels);
					cmd.SetGlobalFloatArray(ForwardLights.LightConstantBuffer._AdditionalLightsLayerMasks, this.m_AdditionalLightsLayerMasks);
				}
				cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._AdditionalLightsCount, new Vector4((float)ptr.maxPerObjectAdditionalLightsCount, 0f, 0f, 0f));
				return;
			}
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._AdditionalLightsCount, Vector4.zero);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00032F68 File Offset: 0x00031168
		private int SetupPerObjectLightIndices(CullingResults cullResults, ref LightData lightData)
		{
			if (lightData.additionalLightsCount == 0)
			{
				return lightData.additionalLightsCount;
			}
			NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
			NativeArray<int> lightIndexMap = cullResults.GetLightIndexMap(Allocator.Temp);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num3 < visibleLights.Length && num2 < UniversalRenderPipeline.maxVisibleAdditionalLights)
			{
				VisibleLight visibleLight = visibleLights[num3];
				if (num3 == lightData.mainLightIndex)
				{
					lightIndexMap[num3] = -1;
					num++;
				}
				else
				{
					ref NativeArray<int> ptr = ref lightIndexMap;
					int num4 = num3;
					ptr[num4] -= num;
					num2++;
				}
				num3++;
			}
			for (int i = num + num2; i < lightIndexMap.Length; i++)
			{
				lightIndexMap[i] = -1;
			}
			cullResults.SetLightIndexMap(lightIndexMap);
			if (this.m_UseStructuredBuffer && num2 > 0)
			{
				int lightAndReflectionProbeIndexCount = cullResults.lightAndReflectionProbeIndexCount;
				cullResults.FillLightAndReflectionProbeIndices(ShaderData.instance.GetLightIndicesBuffer(lightAndReflectionProbeIndexCount));
			}
			lightIndexMap.Dispose();
			return num2;
		}

		// Token: 0x04000762 RID: 1890
		private int m_AdditionalLightsBufferId;

		// Token: 0x04000763 RID: 1891
		private int m_AdditionalLightsIndicesId;

		// Token: 0x04000764 RID: 1892
		private const string k_SetupLightConstants = "Setup Light Constants";

		// Token: 0x04000765 RID: 1893
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Setup Light Constants");

		// Token: 0x04000766 RID: 1894
		private MixedLightingSetup m_MixedLightingSetup;

		// Token: 0x04000767 RID: 1895
		private Vector4[] m_AdditionalLightPositions;

		// Token: 0x04000768 RID: 1896
		private Vector4[] m_AdditionalLightColors;

		// Token: 0x04000769 RID: 1897
		private Vector4[] m_AdditionalLightAttenuations;

		// Token: 0x0400076A RID: 1898
		private Vector4[] m_AdditionalLightSpotDirections;

		// Token: 0x0400076B RID: 1899
		private Vector4[] m_AdditionalLightOcclusionProbeChannels;

		// Token: 0x0400076C RID: 1900
		private float[] m_AdditionalLightsLayerMasks;

		// Token: 0x0400076D RID: 1901
		private bool m_UseStructuredBuffer;

		// Token: 0x0400076E RID: 1902
		private bool m_UseClusteredRendering;

		// Token: 0x0400076F RID: 1903
		private int m_DirectionalLightCount;

		// Token: 0x04000770 RID: 1904
		private int m_ActualTileWidth;

		// Token: 0x04000771 RID: 1905
		private int2 m_TileResolution;

		// Token: 0x04000772 RID: 1906
		private int m_RequestedTileWidth;

		// Token: 0x04000773 RID: 1907
		private float m_ZBinFactor;

		// Token: 0x04000774 RID: 1908
		private int m_ZBinOffset;

		// Token: 0x04000775 RID: 1909
		private JobHandle m_CullingHandle;

		// Token: 0x04000776 RID: 1910
		private NativeArray<ZBin> m_ZBins;

		// Token: 0x04000777 RID: 1911
		private NativeArray<uint> m_TileLightMasks;

		// Token: 0x04000778 RID: 1912
		private ComputeBuffer m_ZBinBuffer;

		// Token: 0x04000779 RID: 1913
		private ComputeBuffer m_TileBuffer;

		// Token: 0x0400077A RID: 1914
		private LightCookieManager m_LightCookieManager;

		// Token: 0x020001A6 RID: 422
		private static class LightConstantBuffer
		{
			// Token: 0x04000AB1 RID: 2737
			public static int _MainLightPosition;

			// Token: 0x04000AB2 RID: 2738
			public static int _MainLightColor;

			// Token: 0x04000AB3 RID: 2739
			public static int _MainLightOcclusionProbesChannel;

			// Token: 0x04000AB4 RID: 2740
			public static int _MainLightLayerMask;

			// Token: 0x04000AB5 RID: 2741
			public static int _AdditionalLightsCount;

			// Token: 0x04000AB6 RID: 2742
			public static int _AdditionalLightsPosition;

			// Token: 0x04000AB7 RID: 2743
			public static int _AdditionalLightsColor;

			// Token: 0x04000AB8 RID: 2744
			public static int _AdditionalLightsAttenuation;

			// Token: 0x04000AB9 RID: 2745
			public static int _AdditionalLightsSpotDir;

			// Token: 0x04000ABA RID: 2746
			public static int _AdditionalLightOcclusionProbeChannel;

			// Token: 0x04000ABB RID: 2747
			public static int _AdditionalLightsLayerMasks;
		}

		// Token: 0x020001A7 RID: 423
		internal struct InitParams
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x00042608 File Offset: 0x00040808
			internal static ForwardLights.InitParams GetDefault()
			{
				LightCookieManager.Settings @default = LightCookieManager.Settings.GetDefault();
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				if (asset)
				{
					@default.atlas.format = asset.additionalLightsCookieFormat;
					@default.atlas.resolution = asset.additionalLightsCookieResolution;
				}
				ForwardLights.InitParams initParams;
				initParams.lightCookieManager = new LightCookieManager(ref @default);
				initParams.clusteredRendering = false;
				initParams.tileSize = 32;
				return initParams;
			}

			// Token: 0x04000ABC RID: 2748
			public LightCookieManager lightCookieManager;

			// Token: 0x04000ABD RID: 2749
			public bool clusteredRendering;

			// Token: 0x04000ABE RID: 2750
			public int tileSize;
		}
	}
}
