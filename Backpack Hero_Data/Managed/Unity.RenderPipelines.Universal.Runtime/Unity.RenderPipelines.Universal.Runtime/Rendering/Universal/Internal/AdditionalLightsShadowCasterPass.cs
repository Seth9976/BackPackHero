using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000104 RID: 260
	public class AdditionalLightsShadowCasterPass : ScriptableRenderPass
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x00030560 File Offset: 0x0002E760
		public AdditionalLightsShadowCasterPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("AdditionalLightsShadowCasterPass");
			base.renderPassEvent = evt;
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsWorldToShadow = Shader.PropertyToID("_AdditionalLightsWorldToShadow");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams = Shader.PropertyToID("_AdditionalShadowParams");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset0 = Shader.PropertyToID("_AdditionalShadowOffset0");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset1 = Shader.PropertyToID("_AdditionalShadowOffset1");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset2 = Shader.PropertyToID("_AdditionalShadowOffset2");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset3 = Shader.PropertyToID("_AdditionalShadowOffset3");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowFadeParams = Shader.PropertyToID("_AdditionalShadowFadeParams");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowmapSize = Shader.PropertyToID("_AdditionalShadowmapSize");
			this.m_AdditionalLightsShadowmap.Init("_AdditionalLightsShadowmapTexture");
			AdditionalLightsShadowCasterPass.m_AdditionalLightsWorldToShadow_SSBO = Shader.PropertyToID("_AdditionalLightsWorldToShadow_SSBO");
			AdditionalLightsShadowCasterPass.m_AdditionalShadowParams_SSBO = Shader.PropertyToID("_AdditionalShadowParams_SSBO");
			this.m_UseStructuredBuffer = RenderingUtils.useStructuredBuffer;
			int maxVisibleAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
			int num = UniversalRenderPipeline.maxVisibleAdditionalLights + 1;
			int num2 = (this.m_UseStructuredBuffer ? num : Math.Min(num, UniversalRenderPipeline.maxVisibleAdditionalLights));
			this.m_AdditionalLightIndexToVisibleLightIndex = new int[num2];
			this.m_VisibleLightIndexToAdditionalLightIndex = new int[num];
			this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex = new int[num];
			this.m_AdditionalLightIndexToShadowParams = new Vector4[num2];
			this.m_VisibleLightIndexToCameraSquareDistance = new float[num];
			if (!this.m_UseStructuredBuffer)
			{
				int maxVisibleAdditionalLights2 = UniversalRenderPipeline.maxVisibleAdditionalLights;
				this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix = new Matrix4x4[maxVisibleAdditionalLights2];
				this.m_UnusedAtlasSquareAreas.Capacity = maxVisibleAdditionalLights2;
				this.m_ShadowResolutionRequests.Capacity = maxVisibleAdditionalLights2;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00030718 File Offset: 0x0002E918
		private int GetPunctualLightShadowSlicesCount(in LightType lightType)
		{
			LightType lightType2 = lightType;
			if (lightType2 == LightType.Spot)
			{
				return 1;
			}
			if (lightType2 != LightType.Point)
			{
				return 0;
			}
			return 6;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00030738 File Offset: 0x0002E938
		internal static float CalcGuardAngle(float frustumAngleInDegrees, float guardBandSizeInTexels, float sliceResolutionInTexels)
		{
			float num = frustumAngleInDegrees * 0.017453292f / 2f;
			float num2 = Mathf.Tan(num);
			float num3 = sliceResolutionInTexels / 2f;
			float num4 = guardBandSizeInTexels / 2f;
			float num5 = 1f + num4 / num3;
			float num6 = Mathf.Atan(num2 * num5) - num;
			return 2f * num6 * 57.29578f;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0003078C File Offset: 0x0002E98C
		private int MinimalPunctualLightShadowResolution(bool softShadow)
		{
			if (!softShadow)
			{
				return 8;
			}
			return 16;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00030798 File Offset: 0x0002E998
		internal static float GetPointLightShadowFrustumFovBiasInDegrees(int shadowSliceResolution, bool shadowFiltering)
		{
			float num = 4f;
			if (shadowSliceResolution > 8)
			{
				if (shadowSliceResolution <= 16)
				{
					num = 43f;
				}
				else if (shadowSliceResolution <= 32)
				{
					num = 18.55f;
				}
				else if (shadowSliceResolution <= 64)
				{
					num = 8.63f;
				}
				else if (shadowSliceResolution <= 128)
				{
					num = 4.13f;
				}
				else if (shadowSliceResolution <= 256)
				{
					num = 2.03f;
				}
				else if (shadowSliceResolution <= 512)
				{
					num = 1f;
				}
				else if (shadowSliceResolution <= 1024)
				{
					num = 0.5f;
				}
			}
			if (shadowFiltering && shadowSliceResolution > 16)
			{
				if (shadowSliceResolution <= 32)
				{
					num += 9.35f;
				}
				else if (shadowSliceResolution <= 64)
				{
					num += 4.07f;
				}
				else if (shadowSliceResolution <= 128)
				{
					num += 1.77f;
				}
				else if (shadowSliceResolution <= 256)
				{
					num += 0.85f;
				}
				else if (shadowSliceResolution <= 512)
				{
					num += 0.39f;
				}
				else if (shadowSliceResolution <= 1024)
				{
					num += 0.17f;
				}
			}
			return num;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00030884 File Offset: 0x0002EA84
		internal void InsertionSort(AdditionalLightsShadowCasterPass.ShadowResolutionRequest[] array, int startIndex, int lastIndex)
		{
			for (int i = startIndex + 1; i < lastIndex; i++)
			{
				AdditionalLightsShadowCasterPass.ShadowResolutionRequest shadowResolutionRequest = array[i];
				int num = i - 1;
				while (num >= 0 && (shadowResolutionRequest.requestedResolution > array[num].requestedResolution || (shadowResolutionRequest.requestedResolution == array[num].requestedResolution && !shadowResolutionRequest.softShadow && array[num].softShadow) || (shadowResolutionRequest.requestedResolution == array[num].requestedResolution && shadowResolutionRequest.softShadow == array[num].softShadow && !shadowResolutionRequest.pointLightShadow && array[num].pointLightShadow) || (shadowResolutionRequest.requestedResolution == array[num].requestedResolution && shadowResolutionRequest.softShadow == array[num].softShadow && shadowResolutionRequest.pointLightShadow == array[num].pointLightShadow && this.m_VisibleLightIndexToCameraSquareDistance[shadowResolutionRequest.visibleLightIndex] < this.m_VisibleLightIndexToCameraSquareDistance[array[num].visibleLightIndex]) || (shadowResolutionRequest.requestedResolution == array[num].requestedResolution && shadowResolutionRequest.softShadow == array[num].softShadow && shadowResolutionRequest.pointLightShadow == array[num].pointLightShadow && this.m_VisibleLightIndexToCameraSquareDistance[shadowResolutionRequest.visibleLightIndex] == this.m_VisibleLightIndexToCameraSquareDistance[array[num].visibleLightIndex] && shadowResolutionRequest.visibleLightIndex < array[num].visibleLightIndex) || (shadowResolutionRequest.requestedResolution == array[num].requestedResolution && shadowResolutionRequest.softShadow == array[num].softShadow && shadowResolutionRequest.pointLightShadow == array[num].pointLightShadow && this.m_VisibleLightIndexToCameraSquareDistance[shadowResolutionRequest.visibleLightIndex] == this.m_VisibleLightIndexToCameraSquareDistance[array[num].visibleLightIndex] && shadowResolutionRequest.visibleLightIndex == array[num].visibleLightIndex && shadowResolutionRequest.perLightShadowSliceIndex < array[num].perLightShadowSliceIndex)))
				{
					array[num + 1] = array[num];
					num--;
				}
				array[num + 1] = shadowResolutionRequest;
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00030AB8 File Offset: 0x0002ECB8
		private int EstimateScaleFactorNeededToFitAllShadowsInAtlas(in AdditionalLightsShadowCasterPass.ShadowResolutionRequest[] shadowResolutionRequests, int endIndex, int atlasWidth)
		{
			long num = (long)(atlasWidth * atlasWidth);
			long num2 = 0L;
			for (int i = 0; i < endIndex; i++)
			{
				num2 += (long)(shadowResolutionRequests[i].requestedResolution * shadowResolutionRequests[i].requestedResolution);
			}
			int num3 = 1;
			while (num2 > num * (long)num3 * (long)num3)
			{
				num3 *= 2;
			}
			return num3;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00030B0C File Offset: 0x0002ED0C
		private void AtlasLayout(int atlasSize, int totalShadowSlicesCount, int estimatedScaleFactor)
		{
			bool flag = false;
			bool flag2 = false;
			int num = estimatedScaleFactor;
			while (!flag && !flag2)
			{
				this.m_UnusedAtlasSquareAreas.Clear();
				this.m_UnusedAtlasSquareAreas.Add(new RectInt(0, 0, atlasSize, atlasSize));
				flag = true;
				for (int i = 0; i < totalShadowSlicesCount; i++)
				{
					int num2 = this.m_SortedShadowResolutionRequests[i].requestedResolution / num;
					if (num2 < this.MinimalPunctualLightShadowResolution(this.m_SortedShadowResolutionRequests[i].softShadow))
					{
						flag2 = true;
						break;
					}
					bool flag3 = false;
					for (int j = 0; j < this.m_UnusedAtlasSquareAreas.Count; j++)
					{
						RectInt rectInt = this.m_UnusedAtlasSquareAreas[j];
						int width = rectInt.width;
						int height = rectInt.height;
						int x = rectInt.x;
						int y = rectInt.y;
						if (width >= num2)
						{
							this.m_SortedShadowResolutionRequests[i].offsetX = x;
							this.m_SortedShadowResolutionRequests[i].offsetY = y;
							this.m_SortedShadowResolutionRequests[i].allocatedResolution = num2;
							this.m_UnusedAtlasSquareAreas.RemoveAt(j);
							int num3 = totalShadowSlicesCount - i - 1;
							int k = 0;
							int num4 = num2;
							int num5 = num2;
							int num6 = x;
							int num7 = y;
							while (k < num3)
							{
								num6 += num4;
								if (num6 + num4 > x + width)
								{
									num6 = x;
									num7 += num5;
									if (num7 + num5 > y + height)
									{
										break;
									}
								}
								this.m_UnusedAtlasSquareAreas.Insert(j + k, new RectInt(num6, num7, num4, num5));
								k++;
							}
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						flag = false;
						break;
					}
				}
				if (!flag && !flag2)
				{
					num *= 2;
				}
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00030CC2 File Offset: 0x0002EEC2
		private ulong ResolutionLog2ForHash(int resolution)
		{
			if (resolution <= 1024)
			{
				if (resolution == 512)
				{
					return 9UL;
				}
				if (resolution == 1024)
				{
					return 10UL;
				}
			}
			else
			{
				if (resolution == 2048)
				{
					return 11UL;
				}
				if (resolution == 4096)
				{
					return 12UL;
				}
			}
			return 8UL;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00030D00 File Offset: 0x0002EF00
		private ulong ComputeShadowRequestHash(ref RenderingData renderingData)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			ulong num3 = 0UL;
			ulong num4 = 0UL;
			ulong num5 = 0UL;
			ulong num6 = 0UL;
			ulong num7 = 0UL;
			ulong num8 = 0UL;
			NativeArray<VisibleLight> visibleLights = renderingData.lightData.visibleLights;
			for (int i = 0; i < visibleLights.Length; i++)
			{
				if (this.IsValidShadowCastingLight(ref renderingData.lightData, i))
				{
					if (visibleLights[i].lightType == LightType.Point)
					{
						num += 1UL;
					}
					if (visibleLights[i].light.shadows == LightShadows.Soft)
					{
						num2 += 1UL;
					}
					if (renderingData.shadowData.resolution[i] == 128)
					{
						num3 += 1UL;
					}
					if (renderingData.shadowData.resolution[i] == 256)
					{
						num4 += 1UL;
					}
					if (renderingData.shadowData.resolution[i] == 512)
					{
						num5 += 1UL;
					}
					if (renderingData.shadowData.resolution[i] == 1024)
					{
						num6 += 1UL;
					}
					if (renderingData.shadowData.resolution[i] == 2048)
					{
						num7 += 1UL;
					}
					if (renderingData.shadowData.resolution[i] == 4096)
					{
						num8 += 1UL;
					}
				}
			}
			return (this.ResolutionLog2ForHash(renderingData.shadowData.additionalLightsShadowmapWidth) - 8UL) | (num << 3) | (num2 << 11) | (num3 << 19) | (num4 << 27) | (num5 << 35) | (num6 << 43) | (num7 << 50) | (num8 << 57);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00030E9C File Offset: 0x0002F09C
		public bool Setup(ref RenderingData renderingData)
		{
			bool flag3;
			using (new ProfilingScope(null, this.m_ProfilingSetupSampler))
			{
				this.Clear();
				base.renderTargetWidth = renderingData.shadowData.additionalLightsShadowmapWidth;
				base.renderTargetHeight = renderingData.shadowData.additionalLightsShadowmapHeight;
				NativeArray<VisibleLight> visibleLights = renderingData.lightData.visibleLights;
				int additionalLightsShadowmapWidth = renderingData.shadowData.additionalLightsShadowmapWidth;
				int num = 0;
				this.m_ShadowResolutionRequests.Clear();
				if (this.m_VisibleLightIndexToAdditionalLightIndex.Length < visibleLights.Length)
				{
					this.m_VisibleLightIndexToAdditionalLightIndex = new int[visibleLights.Length];
					this.m_VisibleLightIndexToCameraSquareDistance = new float[visibleLights.Length];
					this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex = new int[visibleLights.Length];
				}
				int num2 = (this.m_UseStructuredBuffer ? visibleLights.Length : Math.Min(visibleLights.Length, UniversalRenderPipeline.maxVisibleAdditionalLights));
				if (this.m_AdditionalLightIndexToVisibleLightIndex.Length < num2)
				{
					this.m_AdditionalLightIndexToVisibleLightIndex = new int[num2];
					this.m_AdditionalLightIndexToShadowParams = new Vector4[num2];
				}
				for (int i = 0; i < this.m_VisibleLightIndexToCameraSquareDistance.Length; i++)
				{
					this.m_VisibleLightIndexToCameraSquareDistance[i] = float.MaxValue;
				}
				for (int j = 0; j < visibleLights.Length; j++)
				{
					if (j != renderingData.lightData.mainLightIndex && this.IsValidShadowCastingLight(ref renderingData.lightData, j))
					{
						LightType lightType = visibleLights[j].lightType;
						int punctualLightShadowSlicesCount = this.GetPunctualLightShadowSlicesCount(in lightType);
						num += punctualLightShadowSlicesCount;
						for (int k = 0; k < punctualLightShadowSlicesCount; k++)
						{
							this.m_ShadowResolutionRequests.Add(new AdditionalLightsShadowCasterPass.ShadowResolutionRequest(j, k, renderingData.shadowData.resolution[j], visibleLights[j].light.shadows == LightShadows.Soft, visibleLights[j].lightType == LightType.Point));
						}
						this.m_VisibleLightIndexToCameraSquareDistance[j] = (renderingData.cameraData.camera.transform.position - visibleLights[j].light.transform.position).sqrMagnitude;
					}
				}
				if (this.m_SortedShadowResolutionRequests == null || this.m_SortedShadowResolutionRequests.Length < num)
				{
					this.m_SortedShadowResolutionRequests = new AdditionalLightsShadowCasterPass.ShadowResolutionRequest[num];
				}
				for (int l = 0; l < this.m_ShadowResolutionRequests.Count; l++)
				{
					this.m_SortedShadowResolutionRequests[l] = this.m_ShadowResolutionRequests[l];
				}
				for (int m = num; m < this.m_SortedShadowResolutionRequests.Length; m++)
				{
					this.m_SortedShadowResolutionRequests[m].requestedResolution = 0;
				}
				this.InsertionSort(this.m_SortedShadowResolutionRequests, 0, num);
				int num3 = (this.m_UseStructuredBuffer ? num : Math.Min(num, UniversalRenderPipeline.maxVisibleAdditionalLights));
				bool flag = false;
				int num4 = 1;
				while (!flag && num3 > 0)
				{
					num4 = this.EstimateScaleFactorNeededToFitAllShadowsInAtlas(in this.m_SortedShadowResolutionRequests, num3, additionalLightsShadowmapWidth);
					if (this.m_SortedShadowResolutionRequests[num3 - 1].requestedResolution >= num4 * this.MinimalPunctualLightShadowResolution(this.m_SortedShadowResolutionRequests[num3 - 1].softShadow))
					{
						flag = true;
					}
					else
					{
						int num5 = num3;
						LightType lightType = (this.m_SortedShadowResolutionRequests[num3 - 1].pointLightShadow ? LightType.Point : LightType.Spot);
						num3 = num5 - this.GetPunctualLightShadowSlicesCount(in lightType);
					}
				}
				for (int n = num3; n < this.m_SortedShadowResolutionRequests.Length; n++)
				{
					this.m_SortedShadowResolutionRequests[n].requestedResolution = 0;
				}
				for (int num6 = 0; num6 < this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex.Length; num6++)
				{
					this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[num6] = -1;
				}
				for (int num7 = num3 - 1; num7 >= 0; num7--)
				{
					this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[this.m_SortedShadowResolutionRequests[num7].visibleLightIndex] = num7;
				}
				this.AtlasLayout(additionalLightsShadowmapWidth, num3, num4);
				if (this.m_AdditionalLightsShadowSlices == null || this.m_AdditionalLightsShadowSlices.Length < num3)
				{
					this.m_AdditionalLightsShadowSlices = new ShadowSliceData[num3];
				}
				if (this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix == null || (this.m_UseStructuredBuffer && this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix.Length < num3))
				{
					this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix = new Matrix4x4[num3];
				}
				for (int num8 = 0; num8 < num2; num8++)
				{
					this.m_AdditionalLightIndexToShadowParams[num8] = AdditionalLightsShadowCasterPass.c_DefaultShadowParams;
				}
				int num9 = 0;
				bool supportsSoftShadows = renderingData.shadowData.supportsSoftShadows;
				int num10 = 0;
				int num11 = 0;
				while (num11 < visibleLights.Length && this.m_ShadowSliceToAdditionalLightIndex.Count < num3 && num10 < num2)
				{
					VisibleLight visibleLight = visibleLights[num11];
					if (num11 == renderingData.lightData.mainLightIndex)
					{
						this.m_VisibleLightIndexToAdditionalLightIndex[num11] = -1;
					}
					else
					{
						int num12 = num10++;
						this.m_AdditionalLightIndexToVisibleLightIndex[num12] = num11;
						this.m_VisibleLightIndexToAdditionalLightIndex[num11] = num12;
						LightType lightType2 = visibleLight.lightType;
						int punctualLightShadowSlicesCount2 = this.GetPunctualLightShadowSlicesCount(in lightType2);
						if (this.m_ShadowSliceToAdditionalLightIndex.Count + punctualLightShadowSlicesCount2 > num3 && this.IsValidShadowCastingLight(ref renderingData.lightData, num11))
						{
							break;
						}
						int num13 = renderingData.lightData.originalIndices[num11];
						int count = this.m_ShadowSliceToAdditionalLightIndex.Count;
						bool flag2 = false;
						for (int num14 = 0; num14 < punctualLightShadowSlicesCount2; num14++)
						{
							int count2 = this.m_ShadowSliceToAdditionalLightIndex.Count;
							Bounds bounds;
							if (renderingData.cullResults.GetShadowCasterBounds(num13, out bounds) && renderingData.shadowData.supportsAdditionalLightShadows && this.IsValidShadowCastingLight(ref renderingData.lightData, num11) && this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[num11] != -1)
							{
								if (lightType2 == LightType.Spot)
								{
									Matrix4x4 matrix4x;
									if (ShadowUtils.ExtractSpotLightMatrix(ref renderingData.cullResults, ref renderingData.shadowData, num13, out matrix4x, out this.m_AdditionalLightsShadowSlices[count2].viewMatrix, out this.m_AdditionalLightsShadowSlices[count2].projectionMatrix, out this.m_AdditionalLightsShadowSlices[count2].splitData))
									{
										this.m_ShadowSliceToAdditionalLightIndex.Add(num12);
										this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Add(num14);
										Light light = visibleLight.light;
										float shadowStrength = light.shadowStrength;
										float num15 = ((supportsSoftShadows && light.shadows == LightShadows.Soft) ? 1f : 0f);
										Vector4 vector = new Vector4(shadowStrength, num15, 0f, (float)count);
										this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[count2] = matrix4x;
										this.m_AdditionalLightIndexToShadowParams[num12] = vector;
										flag2 = true;
									}
								}
								else if (lightType2 == LightType.Point)
								{
									float pointLightShadowFrustumFovBiasInDegrees = AdditionalLightsShadowCasterPass.GetPointLightShadowFrustumFovBiasInDegrees(this.m_SortedShadowResolutionRequests[this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[num11]].allocatedResolution, visibleLight.light.shadows == LightShadows.Soft);
									Matrix4x4 matrix4x2;
									if (ShadowUtils.ExtractPointLightMatrix(ref renderingData.cullResults, ref renderingData.shadowData, num13, (CubemapFace)num14, pointLightShadowFrustumFovBiasInDegrees, out matrix4x2, out this.m_AdditionalLightsShadowSlices[count2].viewMatrix, out this.m_AdditionalLightsShadowSlices[count2].projectionMatrix, out this.m_AdditionalLightsShadowSlices[count2].splitData))
									{
										this.m_ShadowSliceToAdditionalLightIndex.Add(num12);
										this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Add(num14);
										Light light2 = visibleLight.light;
										float shadowStrength2 = light2.shadowStrength;
										float num16 = ((supportsSoftShadows && light2.shadows == LightShadows.Soft) ? 1f : 0f);
										Vector4 vector2 = new Vector4(shadowStrength2, num16, 1f, (float)count);
										this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[count2] = matrix4x2;
										this.m_AdditionalLightIndexToShadowParams[num12] = vector2;
										flag2 = true;
									}
								}
							}
						}
						if (flag2)
						{
							num9++;
						}
					}
					num11++;
				}
				if (num9 == 0)
				{
					flag3 = this.SetupForEmptyRendering(ref renderingData);
				}
				else
				{
					int count3 = this.m_ShadowSliceToAdditionalLightIndex.Count;
					int num17 = 0;
					int num18 = 0;
					for (int num19 = 0; num19 < num3; num19++)
					{
						AdditionalLightsShadowCasterPass.ShadowResolutionRequest shadowResolutionRequest = this.m_SortedShadowResolutionRequests[num19];
						num17 = Mathf.Max(num17, shadowResolutionRequest.offsetX + shadowResolutionRequest.allocatedResolution);
						num18 = Mathf.Max(num18, shadowResolutionRequest.offsetY + shadowResolutionRequest.allocatedResolution);
					}
					base.renderTargetWidth = Mathf.NextPowerOfTwo(num17);
					base.renderTargetHeight = Mathf.NextPowerOfTwo(num18);
					float num20 = 1f / (float)base.renderTargetWidth;
					float num21 = 1f / (float)base.renderTargetHeight;
					for (int num22 = 0; num22 < count3; num22++)
					{
						int num23 = this.m_ShadowSliceToAdditionalLightIndex[num22];
						if (!Mathf.Approximately(this.m_AdditionalLightIndexToShadowParams[num23].x, 0f) && !Mathf.Approximately(this.m_AdditionalLightIndexToShadowParams[num23].w, -1f))
						{
							int num24 = this.m_AdditionalLightIndexToVisibleLightIndex[num23];
							int num25 = this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[num24];
							int num26 = this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex[num22];
							AdditionalLightsShadowCasterPass.ShadowResolutionRequest shadowResolutionRequest2 = this.m_SortedShadowResolutionRequests[num25 + num26];
							int allocatedResolution = shadowResolutionRequest2.allocatedResolution;
							Matrix4x4 identity = Matrix4x4.identity;
							identity.m00 = (float)allocatedResolution * num20;
							identity.m11 = (float)allocatedResolution * num21;
							this.m_AdditionalLightsShadowSlices[num22].offsetX = shadowResolutionRequest2.offsetX;
							this.m_AdditionalLightsShadowSlices[num22].offsetY = shadowResolutionRequest2.offsetY;
							this.m_AdditionalLightsShadowSlices[num22].resolution = allocatedResolution;
							identity.m03 = (float)this.m_AdditionalLightsShadowSlices[num22].offsetX * num20;
							identity.m13 = (float)this.m_AdditionalLightsShadowSlices[num22].offsetY * num21;
							this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[num22] = identity * this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[num22];
						}
					}
					this.m_AdditionalLightsShadowmapTexture = ShadowUtils.GetTemporaryShadowTexture(base.renderTargetWidth, base.renderTargetHeight, 16);
					this.m_MaxShadowDistanceSq = renderingData.cameraData.maxShadowDistance * renderingData.cameraData.maxShadowDistance;
					this.m_CascadeBorder = renderingData.shadowData.mainLightShadowCascadeBorder;
					this.m_CreateEmptyShadowmap = false;
					base.useNativeRenderPass = true;
					flag3 = true;
				}
			}
			return flag3;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000318BC File Offset: 0x0002FABC
		private bool SetupForEmptyRendering(ref RenderingData renderingData)
		{
			if (!renderingData.cameraData.renderer.stripShadowsOffVariants)
			{
				return false;
			}
			this.m_AdditionalLightsShadowmapTexture = ShadowUtils.GetTemporaryShadowTexture(1, 1, 16);
			this.m_CreateEmptyShadowmap = true;
			base.useNativeRenderPass = false;
			for (int i = 0; i < this.m_AdditionalLightIndexToShadowParams.Length; i++)
			{
				this.m_AdditionalLightIndexToShadowParams[i] = AdditionalLightsShadowCasterPass.c_DefaultShadowParams;
			}
			return true;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0003191F File Offset: 0x0002FB1F
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			base.ConfigureTarget(new RenderTargetIdentifier(this.m_AdditionalLightsShadowmapTexture), this.m_AdditionalLightsShadowmapTexture.depthStencilFormat, base.renderTargetWidth, base.renderTargetHeight, 1, true);
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00031957 File Offset: 0x0002FB57
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_CreateEmptyShadowmap)
			{
				this.SetEmptyAdditionalShadowmapAtlas(ref context);
				return;
			}
			if (renderingData.shadowData.supportsAdditionalLightShadows)
			{
				this.RenderAdditionalShadowmapAtlas(ref context, ref renderingData.cullResults, ref renderingData.lightData, ref renderingData.shadowData);
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00031991 File Offset: 0x0002FB91
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (this.m_AdditionalLightsShadowmapTexture)
			{
				RenderTexture.ReleaseTemporary(this.m_AdditionalLightsShadowmapTexture);
				this.m_AdditionalLightsShadowmapTexture = null;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000319C0 File Offset: 0x0002FBC0
		public int GetShadowLightIndexFromLightIndex(int visibleLightIndex)
		{
			if (visibleLightIndex < 0 || visibleLightIndex >= this.m_VisibleLightIndexToAdditionalLightIndex.Length)
			{
				return -1;
			}
			return this.m_VisibleLightIndexToAdditionalLightIndex[visibleLightIndex];
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000319DB File Offset: 0x0002FBDB
		private void Clear()
		{
			this.m_ShadowSliceToAdditionalLightIndex.Clear();
			this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Clear();
			this.m_AdditionalLightsShadowmapTexture = null;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000319FC File Offset: 0x0002FBFC
		private void SetEmptyAdditionalShadowmapAtlas(ref ScriptableRenderContext context)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			CoreUtils.SetKeyword(commandBuffer, "_ADDITIONAL_LIGHT_SHADOWS", true);
			commandBuffer.SetGlobalTexture(this.m_AdditionalLightsShadowmap.id, this.m_AdditionalLightsShadowmapTexture);
			if (RenderingUtils.useStructuredBuffer)
			{
				ComputeBuffer additionalLightShadowParamsStructuredBuffer = ShaderData.instance.GetAdditionalLightShadowParamsStructuredBuffer(this.m_AdditionalLightIndexToShadowParams.Length);
				additionalLightShadowParamsStructuredBuffer.SetData(this.m_AdditionalLightIndexToShadowParams);
				commandBuffer.SetGlobalBuffer(AdditionalLightsShadowCasterPass.m_AdditionalShadowParams_SSBO, additionalLightShadowParamsStructuredBuffer);
			}
			else
			{
				commandBuffer.SetGlobalVectorArray(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams, this.m_AdditionalLightIndexToShadowParams);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00031A8C File Offset: 0x0002FC8C
		private void RenderAdditionalShadowmapAtlas(ref ScriptableRenderContext context, ref CullingResults cullResults, ref LightData lightData, ref ShadowData shadowData)
		{
			NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
			bool flag = false;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<URPProfileId>(URPProfileId.AdditionalLightsShadow)))
			{
				bool flag2 = false;
				int count = this.m_ShadowSliceToAdditionalLightIndex.Count;
				for (int i = 0; i < count; i++)
				{
					int num = this.m_ShadowSliceToAdditionalLightIndex[i];
					if (!Mathf.Approximately(this.m_AdditionalLightIndexToShadowParams[num].x, 0f) && !Mathf.Approximately(this.m_AdditionalLightIndexToShadowParams[num].w, -1f))
					{
						int num2 = this.m_AdditionalLightIndexToVisibleLightIndex[num];
						int num3 = lightData.originalIndices[num2];
						VisibleLight visibleLight = visibleLights[num2];
						ShadowSliceData shadowSliceData = this.m_AdditionalLightsShadowSlices[i];
						ShadowDrawingSettings shadowDrawingSettings = new ShadowDrawingSettings(cullResults, num3);
						shadowDrawingSettings.useRenderingLayerMaskTest = UniversalRenderPipeline.asset.supportsLightLayers;
						shadowDrawingSettings.splitData = shadowSliceData.splitData;
						Vector4 shadowBias = ShadowUtils.GetShadowBias(ref visibleLight, num2, ref shadowData, shadowSliceData.projectionMatrix, (float)shadowSliceData.resolution);
						ShadowUtils.SetupShadowCasterConstantBuffer(commandBuffer, ref visibleLight, shadowBias);
						CoreUtils.SetKeyword(commandBuffer, "_CASTING_PUNCTUAL_LIGHT_SHADOW", true);
						ShadowUtils.RenderShadowSlice(commandBuffer, ref context, ref shadowSliceData, ref shadowDrawingSettings);
						flag |= visibleLight.light.shadows == LightShadows.Soft;
						flag2 = true;
					}
				}
				bool flag3 = shadowData.supportsMainLightShadows && lightData.mainLightIndex != -1 && visibleLights[lightData.mainLightIndex].light.shadows == LightShadows.Soft;
				bool flag4 = shadowData.supportsSoftShadows && (flag3 || flag);
				shadowData.isKeywordAdditionalLightShadowsEnabled = flag2;
				shadowData.isKeywordSoftShadowsEnabled = flag4;
				CoreUtils.SetKeyword(commandBuffer, "_ADDITIONAL_LIGHT_SHADOWS", shadowData.isKeywordAdditionalLightShadowsEnabled);
				CoreUtils.SetKeyword(commandBuffer, "_SHADOWS_SOFT", shadowData.isKeywordSoftShadowsEnabled);
				if (flag2)
				{
					this.SetupAdditionalLightsShadowReceiverConstants(commandBuffer, flag4);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00031CA8 File Offset: 0x0002FEA8
		private void SetupAdditionalLightsShadowReceiverConstants(CommandBuffer cmd, bool softShadows)
		{
			cmd.SetGlobalTexture(this.m_AdditionalLightsShadowmap.id, this.m_AdditionalLightsShadowmapTexture);
			if (this.m_UseStructuredBuffer)
			{
				ComputeBuffer additionalLightShadowParamsStructuredBuffer = ShaderData.instance.GetAdditionalLightShadowParamsStructuredBuffer(this.m_AdditionalLightIndexToShadowParams.Length);
				additionalLightShadowParamsStructuredBuffer.SetData(this.m_AdditionalLightIndexToShadowParams);
				cmd.SetGlobalBuffer(AdditionalLightsShadowCasterPass.m_AdditionalShadowParams_SSBO, additionalLightShadowParamsStructuredBuffer);
				ComputeBuffer additionalLightShadowSliceMatricesStructuredBuffer = ShaderData.instance.GetAdditionalLightShadowSliceMatricesStructuredBuffer(this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix.Length);
				additionalLightShadowSliceMatricesStructuredBuffer.SetData(this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix);
				cmd.SetGlobalBuffer(AdditionalLightsShadowCasterPass.m_AdditionalLightsWorldToShadow_SSBO, additionalLightShadowSliceMatricesStructuredBuffer);
			}
			else
			{
				cmd.SetGlobalVectorArray(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams, this.m_AdditionalLightIndexToShadowParams);
				cmd.SetGlobalMatrixArray(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsWorldToShadow, this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix);
			}
			float num;
			float num2;
			ShadowUtils.GetScaleAndBiasForLinearDistanceFade(this.m_MaxShadowDistanceSq, this.m_CascadeBorder, out num, out num2);
			cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowFadeParams, new Vector4(num, num2, 0f, 0f));
			if (softShadows)
			{
				Vector2Int vector2Int = new Vector2Int(this.m_AdditionalLightsShadowmapTexture.width, this.m_AdditionalLightsShadowmapTexture.height);
				Vector2 vector = Vector2.one / vector2Int;
				Vector2 vector2 = vector * 0.5f;
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset0, new Vector4(-vector2.x, -vector2.y, vector2.x, vector2.y));
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset1, new Vector4(vector2.x, -vector2.y, vector2.x, vector2.y));
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset2, new Vector4(-vector2.x, vector2.y, vector2.x, vector2.y));
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset3, new Vector4(vector2.x, vector2.y, vector2.x, vector2.y));
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowmapSize, new Vector4(vector.x, vector.y, (float)vector2Int.x, (float)vector2Int.y));
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00031EAC File Offset: 0x000300AC
		private bool IsValidShadowCastingLight(ref LightData lightData, int i)
		{
			if (i == lightData.mainLightIndex)
			{
				return false;
			}
			VisibleLight visibleLight = lightData.visibleLights[i];
			if (visibleLight.lightType == LightType.Directional)
			{
				return false;
			}
			Light light = visibleLight.light;
			return light != null && light.shadows != LightShadows.None && !Mathf.Approximately(light.shadowStrength, 0f);
		}

		// Token: 0x04000744 RID: 1860
		[Obsolete("AdditionalLightsShadowCasterPass.m_AdditionalShadowsBufferId was deprecated. Shadow slice matrix is now passed to the GPU using an entry in buffer m_AdditionalLightsWorldToShadow_SSBO", false)]
		public static int m_AdditionalShadowsBufferId;

		// Token: 0x04000745 RID: 1861
		[Obsolete("AdditionalLightsShadowCasterPass.m_AdditionalShadowsIndicesId was deprecated. Shadow slice index is now passed to the GPU using last member of an entry in buffer m_AdditionalShadowParams_SSBO", false)]
		public static int m_AdditionalShadowsIndicesId;

		// Token: 0x04000746 RID: 1862
		private static readonly Vector4 c_DefaultShadowParams = new Vector4(0f, 0f, 0f, -1f);

		// Token: 0x04000747 RID: 1863
		private static int m_AdditionalLightsWorldToShadow_SSBO;

		// Token: 0x04000748 RID: 1864
		private static int m_AdditionalShadowParams_SSBO;

		// Token: 0x04000749 RID: 1865
		private bool m_UseStructuredBuffer;

		// Token: 0x0400074A RID: 1866
		private const int k_ShadowmapBufferBits = 16;

		// Token: 0x0400074B RID: 1867
		private RenderTargetHandle m_AdditionalLightsShadowmap;

		// Token: 0x0400074C RID: 1868
		internal RenderTexture m_AdditionalLightsShadowmapTexture;

		// Token: 0x0400074D RID: 1869
		private float m_MaxShadowDistanceSq;

		// Token: 0x0400074E RID: 1870
		private float m_CascadeBorder;

		// Token: 0x0400074F RID: 1871
		private ShadowSliceData[] m_AdditionalLightsShadowSlices;

		// Token: 0x04000750 RID: 1872
		private int[] m_VisibleLightIndexToAdditionalLightIndex;

		// Token: 0x04000751 RID: 1873
		private int[] m_AdditionalLightIndexToVisibleLightIndex;

		// Token: 0x04000752 RID: 1874
		private List<int> m_ShadowSliceToAdditionalLightIndex = new List<int>();

		// Token: 0x04000753 RID: 1875
		private List<int> m_GlobalShadowSliceIndexToPerLightShadowSliceIndex = new List<int>();

		// Token: 0x04000754 RID: 1876
		private Vector4[] m_AdditionalLightIndexToShadowParams;

		// Token: 0x04000755 RID: 1877
		private Matrix4x4[] m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix;

		// Token: 0x04000756 RID: 1878
		private List<AdditionalLightsShadowCasterPass.ShadowResolutionRequest> m_ShadowResolutionRequests = new List<AdditionalLightsShadowCasterPass.ShadowResolutionRequest>();

		// Token: 0x04000757 RID: 1879
		private float[] m_VisibleLightIndexToCameraSquareDistance;

		// Token: 0x04000758 RID: 1880
		private AdditionalLightsShadowCasterPass.ShadowResolutionRequest[] m_SortedShadowResolutionRequests;

		// Token: 0x04000759 RID: 1881
		private int[] m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex;

		// Token: 0x0400075A RID: 1882
		private List<RectInt> m_UnusedAtlasSquareAreas = new List<RectInt>();

		// Token: 0x0400075B RID: 1883
		private bool m_CreateEmptyShadowmap;

		// Token: 0x0400075C RID: 1884
		private ProfilingSampler m_ProfilingSetupSampler = new ProfilingSampler("Setup Additional Shadows");

		// Token: 0x0400075D RID: 1885
		private const float LightTypeIdentifierInShadowParams_Spot = 0f;

		// Token: 0x0400075E RID: 1886
		private const float LightTypeIdentifierInShadowParams_Point = 1f;

		// Token: 0x0400075F RID: 1887
		private const int kMinimumPunctualLightHardShadowResolution = 8;

		// Token: 0x04000760 RID: 1888
		private const int kMinimumPunctualLightSoftShadowResolution = 16;

		// Token: 0x04000761 RID: 1889
		private Dictionary<int, ulong> m_ShadowRequestsHashes = new Dictionary<int, ulong>();

		// Token: 0x020001A4 RID: 420
		private static class AdditionalShadowsConstantBuffer
		{
			// Token: 0x04000AA1 RID: 2721
			public static int _AdditionalLightsWorldToShadow;

			// Token: 0x04000AA2 RID: 2722
			public static int _AdditionalShadowParams;

			// Token: 0x04000AA3 RID: 2723
			public static int _AdditionalShadowOffset0;

			// Token: 0x04000AA4 RID: 2724
			public static int _AdditionalShadowOffset1;

			// Token: 0x04000AA5 RID: 2725
			public static int _AdditionalShadowOffset2;

			// Token: 0x04000AA6 RID: 2726
			public static int _AdditionalShadowOffset3;

			// Token: 0x04000AA7 RID: 2727
			public static int _AdditionalShadowFadeParams;

			// Token: 0x04000AA8 RID: 2728
			public static int _AdditionalShadowmapSize;
		}

		// Token: 0x020001A5 RID: 421
		internal struct ShadowResolutionRequest
		{
			// Token: 0x06000A26 RID: 2598 RVA: 0x000425CB File Offset: 0x000407CB
			public ShadowResolutionRequest(int _visibleLightIndex, int _perLightShadowSliceIndex, int _requestedResolution, bool _softShadow, bool _pointLightShadow)
			{
				this.visibleLightIndex = _visibleLightIndex;
				this.perLightShadowSliceIndex = _perLightShadowSliceIndex;
				this.requestedResolution = _requestedResolution;
				this.softShadow = _softShadow;
				this.pointLightShadow = _pointLightShadow;
				this.offsetX = 0;
				this.offsetY = 0;
				this.allocatedResolution = 0;
			}

			// Token: 0x04000AA9 RID: 2729
			public int visibleLightIndex;

			// Token: 0x04000AAA RID: 2730
			public int perLightShadowSliceIndex;

			// Token: 0x04000AAB RID: 2731
			public int requestedResolution;

			// Token: 0x04000AAC RID: 2732
			public bool softShadow;

			// Token: 0x04000AAD RID: 2733
			public bool pointLightShadow;

			// Token: 0x04000AAE RID: 2734
			public int offsetX;

			// Token: 0x04000AAF RID: 2735
			public int offsetY;

			// Token: 0x04000AB0 RID: 2736
			public int allocatedResolution;
		}
	}
}
