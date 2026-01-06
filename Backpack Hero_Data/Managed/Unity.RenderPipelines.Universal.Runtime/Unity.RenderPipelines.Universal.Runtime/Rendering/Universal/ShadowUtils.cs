using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BE RID: 190
	public static class ShadowUtils
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0001F950 File Offset: 0x0001DB50
		public static bool ExtractDirectionalLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out Vector4 cascadeSplitDistance, out ShadowSliceData shadowSliceData, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix)
		{
			bool flag = ShadowUtils.ExtractDirectionalLightMatrix(ref cullResults, ref shadowData, shadowLightIndex, cascadeIndex, shadowmapWidth, shadowmapHeight, shadowResolution, shadowNearPlane, out cascadeSplitDistance, out shadowSliceData);
			viewMatrix = shadowSliceData.viewMatrix;
			projMatrix = shadowSliceData.projectionMatrix;
			return flag;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001F990 File Offset: 0x0001DB90
		public static bool ExtractDirectionalLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out Vector4 cascadeSplitDistance, out ShadowSliceData shadowSliceData)
		{
			bool flag = cullResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(shadowLightIndex, cascadeIndex, shadowData.mainLightShadowCascadesCount, shadowData.mainLightShadowCascadesSplit, shadowResolution, shadowNearPlane, out shadowSliceData.viewMatrix, out shadowSliceData.projectionMatrix, out shadowSliceData.splitData);
			cascadeSplitDistance = shadowSliceData.splitData.cullingSphere;
			shadowSliceData.offsetX = cascadeIndex % 2 * shadowResolution;
			shadowSliceData.offsetY = cascadeIndex / 2 * shadowResolution;
			shadowSliceData.resolution = shadowResolution;
			shadowSliceData.shadowTransform = ShadowUtils.GetShadowTransform(shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
			shadowSliceData.splitData.shadowCascadeBlendCullingFactor = 1f;
			if (shadowData.mainLightShadowCascadesCount > 1)
			{
				ShadowUtils.ApplySliceTransform(ref shadowSliceData, shadowmapWidth, shadowmapHeight);
			}
			return flag;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001FA3F File Offset: 0x0001DC3F
		public static bool ExtractSpotLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, out Matrix4x4 shadowMatrix, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData splitData)
		{
			bool flag = cullResults.ComputeSpotShadowMatricesAndCullingPrimitives(shadowLightIndex, out viewMatrix, out projMatrix, out splitData);
			shadowMatrix = ShadowUtils.GetShadowTransform(projMatrix, viewMatrix);
			return flag;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001FA68 File Offset: 0x0001DC68
		public static bool ExtractPointLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 shadowMatrix, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData splitData)
		{
			bool flag = cullResults.ComputePointShadowMatricesAndCullingPrimitives(shadowLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out splitData);
			viewMatrix.m10 = -viewMatrix.m10;
			viewMatrix.m11 = -viewMatrix.m11;
			viewMatrix.m12 = -viewMatrix.m12;
			viewMatrix.m13 = -viewMatrix.m13;
			shadowMatrix = ShadowUtils.GetShadowTransform(projMatrix, viewMatrix);
			return flag;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001FADC File Offset: 0x0001DCDC
		public static void RenderShadowSlice(CommandBuffer cmd, ref ScriptableRenderContext context, ref ShadowSliceData shadowSliceData, ref ShadowDrawingSettings settings, Matrix4x4 proj, Matrix4x4 view)
		{
			cmd.SetGlobalDepthBias(1f, 2.5f);
			cmd.SetViewport(new Rect((float)shadowSliceData.offsetX, (float)shadowSliceData.offsetY, (float)shadowSliceData.resolution, (float)shadowSliceData.resolution));
			cmd.SetViewProjectionMatrices(view, proj);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			context.DrawShadows(ref settings);
			cmd.DisableScissorRect();
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			cmd.SetGlobalDepthBias(0f, 0f);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001FB61 File Offset: 0x0001DD61
		public static void RenderShadowSlice(CommandBuffer cmd, ref ScriptableRenderContext context, ref ShadowSliceData shadowSliceData, ref ShadowDrawingSettings settings)
		{
			ShadowUtils.RenderShadowSlice(cmd, ref context, ref shadowSliceData, ref settings, shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001FB78 File Offset: 0x0001DD78
		public static int GetMaxTileResolutionInAtlas(int atlasWidth, int atlasHeight, int tileCount)
		{
			int num = Mathf.Min(atlasWidth, atlasHeight);
			for (int i = atlasWidth / num * atlasHeight / num; i < tileCount; i = atlasWidth / num * atlasHeight / num)
			{
				num >>= 1;
			}
			return num;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		public static void ApplySliceTransform(ref ShadowSliceData shadowSliceData, int atlasWidth, int atlasHeight)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			float num = 1f / (float)atlasWidth;
			float num2 = 1f / (float)atlasHeight;
			identity.m00 = (float)shadowSliceData.resolution * num;
			identity.m11 = (float)shadowSliceData.resolution * num2;
			identity.m03 = (float)shadowSliceData.offsetX * num;
			identity.m13 = (float)shadowSliceData.offsetY * num2;
			shadowSliceData.shadowTransform = identity * shadowSliceData.shadowTransform;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001FC20 File Offset: 0x0001DE20
		public static Vector4 GetShadowBias(ref VisibleLight shadowLight, int shadowLightIndex, ref ShadowData shadowData, Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			if (shadowLightIndex < 0 || shadowLightIndex >= shadowData.bias.Count)
			{
				Debug.LogWarning(string.Format("{0} is not a valid light index.", shadowLightIndex));
				return Vector4.zero;
			}
			float num;
			if (shadowLight.lightType == LightType.Directional)
			{
				num = 2f / lightProjectionMatrix.m00;
			}
			else if (shadowLight.lightType == LightType.Spot)
			{
				num = Mathf.Tan(shadowLight.spotAngle * 0.5f * 0.017453292f) * shadowLight.range;
			}
			else if (shadowLight.lightType == LightType.Point)
			{
				float pointLightShadowFrustumFovBiasInDegrees = AdditionalLightsShadowCasterPass.GetPointLightShadowFrustumFovBiasInDegrees((int)shadowResolution, shadowLight.light.shadows == LightShadows.Soft);
				num = Mathf.Tan((90f + pointLightShadowFrustumFovBiasInDegrees) * 0.5f * 0.017453292f) * shadowLight.range;
			}
			else
			{
				Debug.LogWarning("Only point, spot and directional shadow casters are supported in universal pipeline");
				num = 0f;
			}
			float num2 = num / shadowResolution;
			float num3 = -shadowData.bias[shadowLightIndex].x * num2;
			float num4 = -shadowData.bias[shadowLightIndex].y * num2;
			if (shadowLight.lightType == LightType.Point)
			{
				num4 = 0f;
			}
			if (shadowData.supportsSoftShadows && shadowLight.light.shadows == LightShadows.Soft)
			{
				num3 *= 2.5f;
				num4 *= 2.5f;
			}
			return new Vector4(num3, num4, 0f, 0f);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001FD64 File Offset: 0x0001DF64
		internal static void GetScaleAndBiasForLinearDistanceFade(float fadeDistance, float border, out float scale, out float bias)
		{
			if (border < 0.0001f)
			{
				float num = 1000f;
				scale = num;
				bias = -fadeDistance * num;
				return;
			}
			border = 1f - border;
			border *= border;
			float num2 = border * fadeDistance;
			scale = 1f / (fadeDistance - num2);
			bias = -num2 / (fadeDistance - num2);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		public static void SetupShadowCasterConstantBuffer(CommandBuffer cmd, ref VisibleLight shadowLight, Vector4 shadowBias)
		{
			cmd.SetGlobalVector("_ShadowBias", shadowBias);
			Vector3 vector = -shadowLight.localToWorldMatrix.GetColumn(2);
			cmd.SetGlobalVector("_LightDirection", new Vector4(vector.x, vector.y, vector.z, 0f));
			Vector3 vector2 = shadowLight.localToWorldMatrix.GetColumn(3);
			cmd.SetGlobalVector("_LightPosition", new Vector4(vector2.x, vector2.y, vector2.z, 1f));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001FE48 File Offset: 0x0001E048
		public static RenderTexture GetTemporaryShadowTexture(int width, int height, int bits)
		{
			GraphicsFormat depthStencilFormat = GraphicsFormatUtility.GetDepthStencilFormat(bits, 0);
			RenderTexture temporary = RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, GraphicsFormat.None, depthStencilFormat)
			{
				shadowSamplingMode = ((RenderingUtils.SupportsRenderTextureFormat(RenderTextureFormat.Shadowmap) && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2) ? ShadowSamplingMode.CompareDepths : ShadowSamplingMode.None)
			});
			temporary.filterMode = (ShadowUtils.m_ForceShadowPointSampling ? FilterMode.Point : FilterMode.Bilinear);
			temporary.wrapMode = TextureWrapMode.Clamp;
			return temporary;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		private static Matrix4x4 GetShadowTransform(Matrix4x4 proj, Matrix4x4 view)
		{
			if (SystemInfo.usesReversedZBuffer)
			{
				proj.m20 = -proj.m20;
				proj.m21 = -proj.m21;
				proj.m22 = -proj.m22;
				proj.m23 = -proj.m23;
			}
			Matrix4x4 matrix4x = proj * view;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.m00 = 0.5f;
			identity.m11 = 0.5f;
			identity.m22 = 0.5f;
			identity.m03 = 0.5f;
			identity.m23 = 0.5f;
			identity.m13 = 0.5f;
			return identity * matrix4x;
		}

		// Token: 0x04000482 RID: 1154
		private static readonly bool m_ForceShadowPointSampling = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && GraphicsSettings.HasShaderDefine(Graphics.activeTier, BuiltinShaderDefine.UNITY_METAL_SHADOWS_USE_POINT_FILTERING);
	}
}
