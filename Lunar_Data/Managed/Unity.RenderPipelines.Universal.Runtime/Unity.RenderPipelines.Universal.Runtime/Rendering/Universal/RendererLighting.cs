using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002E RID: 46
	internal static class RendererLighting
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000E94E File Offset: 0x0000CB4E
		private static GraphicsFormat GetRenderTextureFormat()
		{
			if (!RendererLighting.s_HasSetupRenderTextureFormatToUse)
			{
				if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Blend))
				{
					RendererLighting.s_RenderTextureFormatToUse = GraphicsFormat.B10G11R11_UFloatPack32;
				}
				else if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, FormatUsage.Blend))
				{
					RendererLighting.s_RenderTextureFormatToUse = GraphicsFormat.R16G16B16A16_SFloat;
				}
				RendererLighting.s_HasSetupRenderTextureFormatToUse = true;
			}
			return RendererLighting.s_RenderTextureFormatToUse;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000E988 File Offset: 0x0000CB88
		public static void CreateNormalMapRenderTexture(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, float renderScale)
		{
			if (renderScale != pass.rendererData.normalsRenderTargetScale)
			{
				if (pass.rendererData.isNormalsRenderTargetValid)
				{
					cmd.ReleaseTemporaryRT(pass.rendererData.normalsRenderTarget.id);
				}
				pass.rendererData.isNormalsRenderTargetValid = true;
				pass.rendererData.normalsRenderTargetScale = renderScale;
				RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor((int)((float)renderingData.cameraData.cameraTargetDescriptor.width * renderScale), (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * renderScale));
				renderTextureDescriptor.graphicsFormat = RendererLighting.GetRenderTextureFormat();
				renderTextureDescriptor.useMipMap = false;
				renderTextureDescriptor.autoGenerateMips = false;
				renderTextureDescriptor.depthBufferBits = (pass.rendererData.useDepthStencilBuffer ? 32 : 0);
				renderTextureDescriptor.msaaSamples = renderingData.cameraData.cameraTargetDescriptor.msaaSamples;
				renderTextureDescriptor.dimension = TextureDimension.Tex2D;
				cmd.GetTemporaryRT(pass.rendererData.normalsRenderTarget.id, renderTextureDescriptor, FilterMode.Bilinear);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000EA80 File Offset: 0x0000CC80
		public static RenderTextureDescriptor GetBlendStyleRenderTextureDesc(this IRenderPass2D pass, RenderingData renderingData)
		{
			float num = Mathf.Clamp(pass.rendererData.lightRenderTextureScale, 0.01f, 1f);
			int num2 = (int)((float)renderingData.cameraData.cameraTargetDescriptor.width * num);
			int num3 = (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * num);
			return new RenderTextureDescriptor(num2, num3)
			{
				graphicsFormat = RendererLighting.GetRenderTextureFormat(),
				useMipMap = false,
				autoGenerateMips = false,
				depthBufferBits = 0,
				msaaSamples = 1,
				dimension = TextureDimension.Tex2D
			};
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public static void CreateCameraSortingLayerRenderTexture(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, Downsampling downsamplingMethod)
		{
			float num = 1f;
			if (downsamplingMethod == Downsampling._2xBilinear)
			{
				num = 0.5f;
			}
			else if (downsamplingMethod == Downsampling._4xBox || downsamplingMethod == Downsampling._4xBilinear)
			{
				num = 0.25f;
			}
			int num2 = (int)((float)renderingData.cameraData.cameraTargetDescriptor.width * num);
			int num3 = (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * num);
			RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor(num2, num3);
			renderTextureDescriptor.graphicsFormat = renderingData.cameraData.cameraTargetDescriptor.graphicsFormat;
			renderTextureDescriptor.useMipMap = false;
			renderTextureDescriptor.autoGenerateMips = false;
			renderTextureDescriptor.depthBufferBits = 0;
			renderTextureDescriptor.msaaSamples = 1;
			renderTextureDescriptor.dimension = TextureDimension.Tex2D;
			cmd.GetTemporaryRT(pass.rendererData.cameraSortingLayerRenderTarget.id, renderTextureDescriptor, FilterMode.Bilinear);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public static void EnableBlendStyle(CommandBuffer cmd, int blendStyleIndex, bool enabled)
		{
			string text = RendererLighting.k_UseBlendStyleKeywords[blendStyleIndex];
			if (enabled)
			{
				cmd.EnableShaderKeyword(text);
				return;
			}
			cmd.DisableShaderKeyword(text);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public static void DisableAllKeywords(this IRenderPass2D pass, CommandBuffer cmd)
		{
			foreach (string text in RendererLighting.k_UseBlendStyleKeywords)
			{
				cmd.DisableShaderKeyword(text);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public static void ReleaseRenderTextures(this IRenderPass2D pass, CommandBuffer cmd)
		{
			pass.rendererData.isNormalsRenderTargetValid = false;
			pass.rendererData.normalsRenderTargetScale = 0f;
			cmd.ReleaseTemporaryRT(pass.rendererData.normalsRenderTarget.id);
			cmd.ReleaseTemporaryRT(pass.rendererData.shadowsRenderTarget.id);
			cmd.ReleaseTemporaryRT(pass.rendererData.cameraSortingLayerRenderTarget.id);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public static void DrawPointLight(CommandBuffer cmd, Light2D light, Mesh lightMesh, Material material)
		{
			Vector3 vector = new Vector3(light.pointLightOuterRadius, light.pointLightOuterRadius, light.pointLightOuterRadius);
			Matrix4x4 matrix4x = Matrix4x4.TRS(light.transform.position, light.transform.rotation, vector);
			cmd.DrawMesh(lightMesh, matrix4x, material);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		private static bool CanCastShadows(Light2D light, int layerToRender)
		{
			return light.shadowsEnabled && light.shadowIntensity > 0f && light.IsLitLayer(layerToRender);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		private static bool CanCastVolumetricShadows(Light2D light, int endLayerValue)
		{
			int topMostLitLayer = light.GetTopMostLitLayer();
			return light.volumetricShadowsEnabled && light.shadowVolumeIntensity > 0f && topMostLitLayer == endLayerValue;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000ED2B File Offset: 0x0000CF2B
		private static bool ShouldRenderLight(Light2D light, int blendStyleIndex, int layerToRender)
		{
			return light != null && light.lightType != Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && light.IsLitLayer(layerToRender);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000ED54 File Offset: 0x0000CF54
		private static void RenderLightSet(IRenderPass2D pass, RenderingData renderingData, int blendStyleIndex, CommandBuffer cmd, int layerToRender, RenderTargetIdentifier renderTexture, List<Light2D> lights)
		{
			uint num = ShadowRendering.maxTextureCount * 4U;
			bool flag = true;
			if (num < 1U)
			{
				Debug.LogError("maxShadowTextureCount cannot be less than 1");
				return;
			}
			NativeArray<bool> nativeArray = new NativeArray<bool>(lights.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int num3;
			for (int i = 0; i < lights.Count; i += num3)
			{
				long num2 = (long)((ulong)lights.Count - (ulong)((long)i));
				num3 = 0;
				int num4 = 0;
				while ((long)num3 < num2 && (long)num4 < (long)((ulong)num))
				{
					int num5 = i + num3;
					Light2D light2D = lights[num5];
					if (RendererLighting.ShouldRenderLight(light2D, blendStyleIndex, layerToRender) && RendererLighting.CanCastShadows(light2D, layerToRender))
					{
						nativeArray[num5] = false;
						if (ShadowRendering.PrerenderShadows(pass, renderingData, cmd, layerToRender, light2D, num4, light2D.shadowIntensity))
						{
							nativeArray[num5] = true;
							num4++;
						}
					}
					num3++;
				}
				if (num4 > 0 || flag)
				{
					cmd.SetRenderTarget(renderTexture, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					flag = false;
				}
				num4 = 0;
				for (int j = 0; j < num3; j++)
				{
					Light2D light2D2 = lights[i + j];
					if (RendererLighting.ShouldRenderLight(light2D2, blendStyleIndex, layerToRender))
					{
						Material lightMaterial = pass.rendererData.GetLightMaterial(light2D2, false);
						if (!(lightMaterial == null))
						{
							Mesh lightMesh = light2D2.lightMesh;
							if (!(lightMesh == null))
							{
								if (nativeArray[i + j])
								{
									ShadowRendering.SetGlobalShadowTexture(cmd, light2D2, num4++);
								}
								else
								{
									ShadowRendering.DisableGlobalShadowTexture(cmd);
								}
								if (light2D2.lightType == Light2D.LightType.Sprite && light2D2.lightCookieSprite != null && light2D2.lightCookieSprite.texture != null)
								{
									cmd.SetGlobalTexture(RendererLighting.k_CookieTexID, light2D2.lightCookieSprite.texture);
								}
								RendererLighting.SetGeneralLightShaderGlobals(pass, cmd, light2D2);
								if (light2D2.normalMapQuality != Light2D.NormalMapQuality.Disabled || light2D2.lightType == Light2D.LightType.Point)
								{
									RendererLighting.SetPointLightShaderGlobals(pass, cmd, light2D2);
								}
								if (light2D2.lightType == Light2D.LightType.Parametric || light2D2.lightType == Light2D.LightType.Freeform || light2D2.lightType == Light2D.LightType.Sprite)
								{
									cmd.DrawMesh(lightMesh, light2D2.transform.localToWorldMatrix, lightMaterial);
								}
								else if (light2D2.lightType == Light2D.LightType.Point)
								{
									RendererLighting.DrawPointLight(cmd, light2D2, lightMesh, lightMaterial);
								}
							}
						}
					}
				}
				for (int k = num4 - 1; k >= 0; k--)
				{
					ShadowRendering.ReleaseShadowRenderTexture(cmd, k);
				}
			}
			nativeArray.Dispose();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public static void RenderLightVolumes(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, int layerToRender, int endLayerValue, RenderTargetIdentifier renderTexture, RenderTargetIdentifier depthTexture, RenderBufferStoreAction intermediateStoreAction, RenderBufferStoreAction finalStoreAction, bool requiresRTInit, List<Light2D> lights)
		{
			uint num = ShadowRendering.maxTextureCount * 4U;
			NativeArray<bool> nativeArray = new NativeArray<bool>(lights.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
			if (num < 1U)
			{
				Debug.LogError("maxShadowLightCount cannot be less than 1");
				return;
			}
			int num2 = lights.Count;
			if (intermediateStoreAction != finalStoreAction)
			{
				for (int i = lights.Count - 1; i >= 0; i--)
				{
					if (lights[i].renderVolumetricShadows)
					{
						num2 = i;
						break;
					}
				}
			}
			int num4;
			for (int j = 0; j < lights.Count; j += num4)
			{
				long num3 = (long)((ulong)lights.Count - (ulong)((long)j));
				num4 = 0;
				int num5 = 0;
				while ((long)num4 < num3 && (long)num5 < (long)((ulong)num))
				{
					int num6 = j + num4;
					Light2D light2D = lights[num6];
					if (RendererLighting.CanCastVolumetricShadows(light2D, endLayerValue))
					{
						nativeArray[num6] = false;
						if (ShadowRendering.PrerenderShadows(pass, renderingData, cmd, layerToRender, light2D, num5, light2D.shadowVolumeIntensity))
						{
							nativeArray[num6] = true;
							num5++;
						}
					}
					num4++;
				}
				if (num5 > 0 || requiresRTInit)
				{
					RenderBufferStoreAction renderBufferStoreAction = ((j + num4 >= num2) ? finalStoreAction : intermediateStoreAction);
					cmd.SetRenderTarget(renderTexture, RenderBufferLoadAction.Load, renderBufferStoreAction, depthTexture, RenderBufferLoadAction.Load, renderBufferStoreAction);
					requiresRTInit = false;
				}
				num5 = 0;
				for (int k = 0; k < num4; k++)
				{
					Light2D light2D2 = lights[j + k];
					if (light2D2.lightType != Light2D.LightType.Global && light2D2.volumeIntensity > 0f && light2D2.volumeIntensityEnabled)
					{
						int topMostLitLayer = light2D2.GetTopMostLitLayer();
						if (endLayerValue == topMostLitLayer)
						{
							Material lightMaterial = pass.rendererData.GetLightMaterial(light2D2, true);
							Mesh lightMesh = light2D2.lightMesh;
							if (nativeArray[j + k])
							{
								ShadowRendering.SetGlobalShadowTexture(cmd, light2D2, num5++);
							}
							else
							{
								ShadowRendering.DisableGlobalShadowTexture(cmd);
							}
							if (light2D2.lightType == Light2D.LightType.Sprite && light2D2.lightCookieSprite != null && light2D2.lightCookieSprite.texture != null)
							{
								cmd.SetGlobalTexture(RendererLighting.k_CookieTexID, light2D2.lightCookieSprite.texture);
							}
							RendererLighting.SetGeneralLightShaderGlobals(pass, cmd, light2D2);
							if (light2D2.normalMapQuality != Light2D.NormalMapQuality.Disabled || light2D2.lightType == Light2D.LightType.Point)
							{
								RendererLighting.SetPointLightShaderGlobals(pass, cmd, light2D2);
							}
							if (light2D2.lightType == Light2D.LightType.Parametric || light2D2.lightType == Light2D.LightType.Freeform || light2D2.lightType == Light2D.LightType.Sprite)
							{
								cmd.DrawMesh(lightMesh, light2D2.transform.localToWorldMatrix, lightMaterial);
							}
							else if (light2D2.lightType == Light2D.LightType.Point)
							{
								RendererLighting.DrawPointLight(cmd, light2D2, lightMesh, lightMaterial);
							}
						}
					}
				}
				for (int l = num5 - 1; l >= 0; l--)
				{
					ShadowRendering.ReleaseShadowRenderTexture(cmd, l);
				}
			}
			nativeArray.Dispose();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F250 File Offset: 0x0000D450
		public static void SetShapeLightShaderGlobals(this IRenderPass2D pass, CommandBuffer cmd)
		{
			for (int i = 0; i < pass.rendererData.lightBlendStyles.Length; i++)
			{
				Light2DBlendStyle light2DBlendStyle = pass.rendererData.lightBlendStyles[i];
				if (i >= RendererLighting.k_BlendFactorsPropIDs.Length)
				{
					break;
				}
				cmd.SetGlobalVector(RendererLighting.k_BlendFactorsPropIDs[i], light2DBlendStyle.blendFactors);
				cmd.SetGlobalVector(RendererLighting.k_MaskFilterPropIDs[i], light2DBlendStyle.maskTextureChannelFilter.mask);
				cmd.SetGlobalVector(RendererLighting.k_InvertedFilterPropIDs[i], light2DBlendStyle.maskTextureChannelFilter.inverted);
			}
			cmd.SetGlobalTexture(RendererLighting.k_FalloffLookupID, pass.rendererData.fallOffLookup);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F300 File Offset: 0x0000D500
		private static float GetNormalizedInnerRadius(Light2D light)
		{
			return light.pointLightInnerRadius / light.pointLightOuterRadius;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F30F File Offset: 0x0000D50F
		private static float GetNormalizedAngle(float angle)
		{
			return angle / 360f;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F318 File Offset: 0x0000D518
		private static void GetScaledLightInvMatrix(Light2D light, out Matrix4x4 retMatrix)
		{
			float pointLightOuterRadius = light.pointLightOuterRadius;
			Vector3 one = Vector3.one;
			Vector3 vector = new Vector3(one.x * pointLightOuterRadius, one.y * pointLightOuterRadius, one.z * pointLightOuterRadius);
			Transform transform = light.transform;
			Matrix4x4 matrix4x = Matrix4x4.TRS(transform.position, transform.rotation, vector);
			retMatrix = Matrix4x4.Inverse(matrix4x);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F37C File Offset: 0x0000D57C
		private static void SetGeneralLightShaderGlobals(IRenderPass2D pass, CommandBuffer cmd, Light2D light)
		{
			Color color = light.intensity * light.color.a * light.color;
			color.a = 1f;
			float volumeIntensity = light.volumeIntensity;
			cmd.SetGlobalFloat(RendererLighting.k_FalloffIntensityID, light.falloffIntensity);
			cmd.SetGlobalFloat(RendererLighting.k_FalloffDistanceID, light.shapeLightFalloffSize);
			cmd.SetGlobalColor(RendererLighting.k_LightColorID, color);
			cmd.SetGlobalFloat(RendererLighting.k_VolumeOpacityID, volumeIntensity);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		private static void SetPointLightShaderGlobals(IRenderPass2D pass, CommandBuffer cmd, Light2D light)
		{
			Matrix4x4 matrix4x;
			RendererLighting.GetScaledLightInvMatrix(light, out matrix4x);
			float normalizedInnerRadius = RendererLighting.GetNormalizedInnerRadius(light);
			float normalizedAngle = RendererLighting.GetNormalizedAngle(light.pointLightInnerAngle);
			float normalizedAngle2 = RendererLighting.GetNormalizedAngle(light.pointLightOuterAngle);
			float num = 1f / (1f - normalizedInnerRadius);
			cmd.SetGlobalVector(RendererLighting.k_LightPositionID, light.transform.position);
			cmd.SetGlobalMatrix(RendererLighting.k_LightInvMatrixID, matrix4x);
			cmd.SetGlobalFloat(RendererLighting.k_InnerRadiusMultID, num);
			cmd.SetGlobalFloat(RendererLighting.k_OuterAngleID, normalizedAngle2);
			cmd.SetGlobalFloat(RendererLighting.k_InnerAngleMultID, 1f / (normalizedAngle2 - normalizedAngle));
			cmd.SetGlobalTexture(RendererLighting.k_LightLookupID, Light2DLookupTexture.GetLightLookupTexture());
			cmd.SetGlobalTexture(RendererLighting.k_FalloffLookupID, pass.rendererData.fallOffLookup);
			cmd.SetGlobalFloat(RendererLighting.k_FalloffIntensityID, light.falloffIntensity);
			cmd.SetGlobalFloat(RendererLighting.k_IsFullSpotlightID, (normalizedAngle == 1f) ? 1f : 0f);
			cmd.SetGlobalFloat(RendererLighting.k_LightZDistanceID, light.normalMapDistance);
			if (light.lightCookieSprite != null && light.lightCookieSprite.texture != null)
			{
				cmd.SetGlobalTexture(RendererLighting.k_PointLightCookieTexID, light.lightCookieSprite.texture);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F538 File Offset: 0x0000D738
		public static void ClearDirtyLighting(this IRenderPass2D pass, CommandBuffer cmd, uint blendStylesUsed)
		{
			for (int i = 0; i < pass.rendererData.lightBlendStyles.Length; i++)
			{
				if ((blendStylesUsed & (1U << i)) != 0U && pass.rendererData.lightBlendStyles[i].isDirty)
				{
					cmd.SetRenderTarget(pass.rendererData.lightBlendStyles[i].renderTargetHandle.Identifier());
					cmd.ClearRenderTarget(false, true, Color.black);
					pass.rendererData.lightBlendStyles[i].isDirty = false;
				}
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public static void RenderNormals(this IRenderPass2D pass, ScriptableRenderContext context, RenderingData renderingData, DrawingSettings drawSettings, FilteringSettings filterSettings, RenderTargetIdentifier depthTarget, CommandBuffer cmd, LightStats lightStats)
		{
			using (new ProfilingScope(cmd, RendererLighting.m_ProfilingSampler))
			{
				float num;
				if (depthTarget != BuiltinRenderTextureType.None)
				{
					num = 1f;
				}
				else
				{
					num = Mathf.Clamp(pass.rendererData.lightRenderTextureScale, 0.01f, 1f);
				}
				pass.CreateNormalMapRenderTexture(renderingData, cmd, num);
				RenderBufferStoreAction renderBufferStoreAction = ((renderingData.cameraData.cameraTargetDescriptor.msaaSamples > 1) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.Store);
				if (depthTarget != BuiltinRenderTextureType.None)
				{
					cmd.SetRenderTarget(pass.rendererData.normalsRenderTarget.Identifier(), RenderBufferLoadAction.DontCare, renderBufferStoreAction, depthTarget, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
				}
				else
				{
					cmd.SetRenderTarget(pass.rendererData.normalsRenderTarget.Identifier(), RenderBufferLoadAction.DontCare, renderBufferStoreAction);
				}
				cmd.ClearRenderTarget(pass.rendererData.useDepthStencilBuffer, true, RendererLighting.k_NormalClearColor);
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				drawSettings.SetShaderPassName(0, RendererLighting.k_NormalsRenderingPassName);
				context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettings);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public static void RenderLights(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, int layerToRender, ref LayerBatch layerBatch, ref RenderTextureDescriptor rtDesc)
		{
			List<Light2D> visibleLights = pass.rendererData.lightCullResult.visibleLights;
			for (int i = 0; i < visibleLights.Count; i++)
			{
				visibleLights[i].CacheValues();
			}
			ShadowCasterGroup2DManager.CacheValues();
			Light2DBlendStyle[] lightBlendStyles = pass.rendererData.lightBlendStyles;
			for (int j = 0; j < lightBlendStyles.Length; j++)
			{
				if ((layerBatch.lightStats.blendStylesUsed & (1U << j)) != 0U)
				{
					string name = lightBlendStyles[j].name;
					cmd.BeginSample(name);
					Color black;
					if (!Light2DManager.GetGlobalColor(layerToRender, j, out black))
					{
						black = Color.black;
					}
					bool flag = (layerBatch.lightStats.blendStylesWithLights & (1U << j)) > 0U;
					RenderTextureDescriptor renderTextureDescriptor = rtDesc;
					if (!flag)
					{
						renderTextureDescriptor.width = (renderTextureDescriptor.height = 4);
					}
					RenderTargetIdentifier rtid = layerBatch.GetRTId(cmd, renderTextureDescriptor, j);
					cmd.SetRenderTarget(rtid, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					cmd.ClearRenderTarget(false, true, black);
					if (flag)
					{
						RendererLighting.RenderLightSet(pass, renderingData, j, cmd, layerToRender, rtid, pass.rendererData.lightCullResult.visibleLights);
					}
					cmd.EndSample(name);
				}
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F80B File Offset: 0x0000DA0B
		private static void SetBlendModes(Material material, BlendMode src, BlendMode dst)
		{
			material.SetFloat(RendererLighting.k_SrcBlendID, (float)src);
			material.SetFloat(RendererLighting.k_DstBlendID, (float)dst);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000F828 File Offset: 0x0000DA28
		private static uint GetLightMaterialIndex(Light2D light, bool isVolume)
		{
			bool isPointLight = light.isPointLight;
			int num = 0;
			uint num2 = (isVolume ? (1U << num) : 0U);
			num++;
			uint num3 = ((!isPointLight) ? (1U << num) : 0U);
			num++;
			uint num4 = ((light.overlapOperation == Light2D.OverlapOperation.AlphaBlend) ? 0U : (1U << num));
			num++;
			uint num5 = ((light.lightType == Light2D.LightType.Sprite) ? (1U << num) : 0U);
			num++;
			uint num6 = ((isPointLight && light.lightCookieSprite != null && light.lightCookieSprite.texture != null) ? (1U << num) : 0U);
			num++;
			uint num7 = ((light.normalMapQuality == Light2D.NormalMapQuality.Fast) ? (1U << num) : 0U);
			num++;
			uint num8 = ((light.normalMapQuality != Light2D.NormalMapQuality.Disabled) ? (1U << num) : 0U);
			return num7 | num6 | num5 | num4 | num3 | num2 | num8;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		private static Material CreateLightMaterial(Renderer2DData rendererData, Light2D light, bool isVolume)
		{
			bool isPointLight = light.isPointLight;
			Material material;
			if (isVolume)
			{
				material = CoreUtils.CreateEngineMaterial(isPointLight ? rendererData.pointLightVolumeShader : rendererData.shapeLightVolumeShader);
			}
			else
			{
				material = CoreUtils.CreateEngineMaterial(isPointLight ? rendererData.pointLightShader : rendererData.shapeLightShader);
				if (light.overlapOperation == Light2D.OverlapOperation.Additive)
				{
					RendererLighting.SetBlendModes(material, BlendMode.One, BlendMode.One);
					material.EnableKeyword(RendererLighting.k_UseAdditiveBlendingKeyword);
				}
				else
				{
					RendererLighting.SetBlendModes(material, BlendMode.SrcAlpha, BlendMode.OneMinusSrcAlpha);
				}
			}
			if (light.lightType == Light2D.LightType.Sprite)
			{
				material.EnableKeyword(RendererLighting.k_SpriteLightKeyword);
			}
			if (isPointLight && light.lightCookieSprite != null && light.lightCookieSprite.texture != null)
			{
				material.EnableKeyword(RendererLighting.k_UsePointLightCookiesKeyword);
			}
			if (light.normalMapQuality == Light2D.NormalMapQuality.Fast)
			{
				material.EnableKeyword(RendererLighting.k_LightQualityFastKeyword);
			}
			if (light.normalMapQuality != Light2D.NormalMapQuality.Disabled)
			{
				material.EnableKeyword(RendererLighting.k_UseNormalMap);
			}
			return material;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		private static Material GetLightMaterial(this Renderer2DData rendererData, Light2D light, bool isVolume)
		{
			uint lightMaterialIndex = RendererLighting.GetLightMaterialIndex(light, isVolume);
			Material material;
			if (!rendererData.lightMaterials.TryGetValue(lightMaterialIndex, out material))
			{
				material = RendererLighting.CreateLightMaterial(rendererData, light, isVolume);
				rendererData.lightMaterials[lightMaterialIndex] = material;
			}
			return material;
		}

		// Token: 0x040000F9 RID: 249
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Draw Normals");

		// Token: 0x040000FA RID: 250
		private static readonly ShaderTagId k_NormalsRenderingPassName = new ShaderTagId("NormalsRendering");

		// Token: 0x040000FB RID: 251
		private static readonly Color k_NormalClearColor = new Color(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x040000FC RID: 252
		private static readonly string k_SpriteLightKeyword = "SPRITE_LIGHT";

		// Token: 0x040000FD RID: 253
		private static readonly string k_UsePointLightCookiesKeyword = "USE_POINT_LIGHT_COOKIES";

		// Token: 0x040000FE RID: 254
		private static readonly string k_LightQualityFastKeyword = "LIGHT_QUALITY_FAST";

		// Token: 0x040000FF RID: 255
		private static readonly string k_UseNormalMap = "USE_NORMAL_MAP";

		// Token: 0x04000100 RID: 256
		private static readonly string k_UseAdditiveBlendingKeyword = "USE_ADDITIVE_BLENDING";

		// Token: 0x04000101 RID: 257
		private static readonly string[] k_UseBlendStyleKeywords = new string[] { "USE_SHAPE_LIGHT_TYPE_0", "USE_SHAPE_LIGHT_TYPE_1", "USE_SHAPE_LIGHT_TYPE_2", "USE_SHAPE_LIGHT_TYPE_3" };

		// Token: 0x04000102 RID: 258
		private static readonly int[] k_BlendFactorsPropIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightBlendFactors0"),
			Shader.PropertyToID("_ShapeLightBlendFactors1"),
			Shader.PropertyToID("_ShapeLightBlendFactors2"),
			Shader.PropertyToID("_ShapeLightBlendFactors3")
		};

		// Token: 0x04000103 RID: 259
		private static readonly int[] k_MaskFilterPropIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightMaskFilter0"),
			Shader.PropertyToID("_ShapeLightMaskFilter1"),
			Shader.PropertyToID("_ShapeLightMaskFilter2"),
			Shader.PropertyToID("_ShapeLightMaskFilter3")
		};

		// Token: 0x04000104 RID: 260
		private static readonly int[] k_InvertedFilterPropIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightInvertedFilter0"),
			Shader.PropertyToID("_ShapeLightInvertedFilter1"),
			Shader.PropertyToID("_ShapeLightInvertedFilter2"),
			Shader.PropertyToID("_ShapeLightInvertedFilter3")
		};

		// Token: 0x04000105 RID: 261
		private static GraphicsFormat s_RenderTextureFormatToUse = GraphicsFormat.R8G8B8A8_UNorm;

		// Token: 0x04000106 RID: 262
		private static bool s_HasSetupRenderTextureFormatToUse;

		// Token: 0x04000107 RID: 263
		private static readonly int k_SrcBlendID = Shader.PropertyToID("_SrcBlend");

		// Token: 0x04000108 RID: 264
		private static readonly int k_DstBlendID = Shader.PropertyToID("_DstBlend");

		// Token: 0x04000109 RID: 265
		private static readonly int k_FalloffIntensityID = Shader.PropertyToID("_FalloffIntensity");

		// Token: 0x0400010A RID: 266
		private static readonly int k_FalloffDistanceID = Shader.PropertyToID("_FalloffDistance");

		// Token: 0x0400010B RID: 267
		private static readonly int k_LightColorID = Shader.PropertyToID("_LightColor");

		// Token: 0x0400010C RID: 268
		private static readonly int k_VolumeOpacityID = Shader.PropertyToID("_VolumeOpacity");

		// Token: 0x0400010D RID: 269
		private static readonly int k_CookieTexID = Shader.PropertyToID("_CookieTex");

		// Token: 0x0400010E RID: 270
		private static readonly int k_FalloffLookupID = Shader.PropertyToID("_FalloffLookup");

		// Token: 0x0400010F RID: 271
		private static readonly int k_LightPositionID = Shader.PropertyToID("_LightPosition");

		// Token: 0x04000110 RID: 272
		private static readonly int k_LightInvMatrixID = Shader.PropertyToID("_LightInvMatrix");

		// Token: 0x04000111 RID: 273
		private static readonly int k_InnerRadiusMultID = Shader.PropertyToID("_InnerRadiusMult");

		// Token: 0x04000112 RID: 274
		private static readonly int k_OuterAngleID = Shader.PropertyToID("_OuterAngle");

		// Token: 0x04000113 RID: 275
		private static readonly int k_InnerAngleMultID = Shader.PropertyToID("_InnerAngleMult");

		// Token: 0x04000114 RID: 276
		private static readonly int k_LightLookupID = Shader.PropertyToID("_LightLookup");

		// Token: 0x04000115 RID: 277
		private static readonly int k_IsFullSpotlightID = Shader.PropertyToID("_IsFullSpotlight");

		// Token: 0x04000116 RID: 278
		private static readonly int k_LightZDistanceID = Shader.PropertyToID("_LightZDistance");

		// Token: 0x04000117 RID: 279
		private static readonly int k_PointLightCookieTexID = Shader.PropertyToID("_PointLightCookieTex");
	}
}
