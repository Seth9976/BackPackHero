using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007B RID: 123
	public sealed class LensFlareCommonSRP
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00011F8E File Offset: 0x0001018E
		private LensFlareCommonSRP()
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011F98 File Offset: 0x00010198
		public static void Initialize()
		{
			if (LensFlareCommonSRP.occlusionRT == null && LensFlareCommonSRP.mergeNeeded > 0)
			{
				LensFlareCommonSRP.occlusionRT = RTHandles.Alloc(LensFlareCommonSRP.maxLensFlareWithOcclusion, LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded, 1, DepthBits.None, GraphicsFormat.R16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "");
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00011FEC File Offset: 0x000101EC
		public static void Dispose()
		{
			if (LensFlareCommonSRP.occlusionRT != null)
			{
				RTHandles.Release(LensFlareCommonSRP.occlusionRT);
				LensFlareCommonSRP.occlusionRT = null;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00012008 File Offset: 0x00010208
		public static LensFlareCommonSRP Instance
		{
			get
			{
				if (LensFlareCommonSRP.m_Instance == null)
				{
					object padlock = LensFlareCommonSRP.m_Padlock;
					lock (padlock)
					{
						if (LensFlareCommonSRP.m_Instance == null)
						{
							LensFlareCommonSRP.m_Instance = new LensFlareCommonSRP();
						}
					}
				}
				return LensFlareCommonSRP.m_Instance;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00012060 File Offset: 0x00010260
		private List<LensFlareComponentSRP> Data
		{
			get
			{
				return LensFlareCommonSRP.m_Data;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012067 File Offset: 0x00010267
		public List<LensFlareComponentSRP> GetData()
		{
			return this.Data;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001206F File Offset: 0x0001026F
		public bool IsEmpty()
		{
			return this.Data.Count == 0;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001207F File Offset: 0x0001027F
		public void AddData(LensFlareComponentSRP newData)
		{
			if (!LensFlareCommonSRP.m_Data.Contains(newData))
			{
				LensFlareCommonSRP.m_Data.Add(newData);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012099 File Offset: 0x00010299
		public static float ShapeAttenuationPointLight()
		{
			return 1f;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000120A0 File Offset: 0x000102A0
		public static float ShapeAttenuationDirLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Vector3.Dot(forward, wo), 0f);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000120B4 File Offset: 0x000102B4
		public static float ShapeAttenuationSpotConeLight(Vector3 forward, Vector3 wo, float spotAngle, float innerSpotPercent01)
		{
			float num = Mathf.Max(Mathf.Cos(0.5f * spotAngle * 0.017453292f), 0f);
			float num2 = Mathf.Max(Mathf.Cos(0.5f * spotAngle * 0.017453292f * innerSpotPercent01), 0f);
			return Mathf.Clamp01((Mathf.Max(Vector3.Dot(forward, wo), 0f) - num) / (num2 - num));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012119 File Offset: 0x00010319
		public static float ShapeAttenuationSpotBoxLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Mathf.Sign(Vector3.Dot(forward, wo)), 0f);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012131 File Offset: 0x00010331
		public static float ShapeAttenuationSpotPyramidLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationSpotBoxLight(forward, wo);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001213C File Offset: 0x0001033C
		public static float ShapeAttenuationAreaTubeLight(Vector3 lightPositionWS, Vector3 lightSide, float lightWidth, Camera cam)
		{
			Vector3 vector = lightPositionWS + lightSide * lightWidth * 0.5f;
			Vector3 vector2 = lightPositionWS - lightSide * lightWidth * 0.5f;
			Vector3 vector3 = lightPositionWS + cam.transform.right * lightWidth * 0.5f;
			Vector3 vector4 = lightPositionWS - cam.transform.right * lightWidth * 0.5f;
			Vector3 vector5 = cam.transform.InverseTransformPoint(vector);
			Vector3 vector6 = cam.transform.InverseTransformPoint(vector2);
			Vector3 vector7 = cam.transform.InverseTransformPoint(vector3);
			Vector3 vector8 = cam.transform.InverseTransformPoint(vector4);
			float num = LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|23_2(vector7, vector8);
			float num2 = LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|23_2(vector5, vector6);
			if (num <= 0f)
			{
				return 1f;
			}
			return num2 / num;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012217 File Offset: 0x00010417
		public static float ShapeAttenuationAreaRectangleLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationDirLight(forward, wo);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012220 File Offset: 0x00010420
		public static float ShapeAttenuationAreaDiscLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationDirLight(forward, wo);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001222C File Offset: 0x0001042C
		public static Vector4 GetFlareData0(Vector2 screenPos, Vector2 translationScale, Vector2 rayOff0, Vector2 vLocalScreenRatio, float angleDeg, float position, float angularOffset, Vector2 positionOffset, bool autoRotate)
		{
			if (!SystemInfo.graphicsUVStartsAtTop)
			{
				angleDeg *= -1f;
				positionOffset.y *= -1f;
			}
			float num = Mathf.Cos(-angularOffset * 0.017453292f);
			float num2 = Mathf.Sin(-angularOffset * 0.017453292f);
			Vector2 vector = -translationScale * (screenPos + screenPos * (position - 1f));
			vector = new Vector2(num * vector.x - num2 * vector.y, num2 * vector.x + num * vector.y);
			float num3 = angleDeg;
			num3 += 180f;
			if (autoRotate)
			{
				Vector2 vector2 = vector.normalized * vLocalScreenRatio * translationScale;
				num3 += -57.29578f * Mathf.Atan2(vector2.y, vector2.x);
			}
			num3 *= 0.017453292f;
			float num4 = Mathf.Cos(-num3);
			float num5 = Mathf.Sin(-num3);
			return new Vector4(num4, num5, positionOffset.x + rayOff0.x * translationScale.x, -positionOffset.y + rayOff0.y * translationScale.y);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012350 File Offset: 0x00010550
		private static Vector2 GetLensFlareRayOffset(Vector2 screenPos, float position, float globalCos0, float globalSin0)
		{
			Vector2 vector = -(screenPos + screenPos * (position - 1f));
			return new Vector2(globalCos0 * vector.x - globalSin0 * vector.y, globalSin0 * vector.x + globalCos0 * vector.y);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001239D File Offset: 0x0001059D
		private static Vector3 WorldToViewport(Camera camera, bool isLocalLight, bool isCameraRelative, Matrix4x4 viewProjMatrix, Vector3 positionWS)
		{
			if (isLocalLight)
			{
				return LensFlareCommonSRP.WorldToViewportLocal(isCameraRelative, viewProjMatrix, camera.transform.position, positionWS);
			}
			return LensFlareCommonSRP.WorldToViewportDistance(camera, positionWS);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000123C0 File Offset: 0x000105C0
		private static Vector3 WorldToViewportLocal(bool isCameraRelative, Matrix4x4 viewProjMatrix, Vector3 cameraPosWS, Vector3 positionWS)
		{
			Vector3 vector = positionWS;
			if (isCameraRelative)
			{
				vector -= cameraPosWS;
			}
			Vector4 vector2 = viewProjMatrix * vector;
			Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0f);
			vector3 /= vector2.w;
			vector3.x = vector3.x * 0.5f + 0.5f;
			vector3.y = vector3.y * 0.5f + 0.5f;
			vector3.y = 1f - vector3.y;
			vector3.z = vector2.w;
			return vector3;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012460 File Offset: 0x00010660
		private static Vector3 WorldToViewportDistance(Camera cam, Vector3 positionWS)
		{
			Vector4 vector = cam.worldToCameraMatrix * positionWS;
			Vector4 vector2 = cam.projectionMatrix * vector;
			Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0f);
			vector3 /= vector2.w;
			vector3.x = vector3.x * 0.5f + 0.5f;
			vector3.y = vector3.y * 0.5f + 0.5f;
			vector3.z = vector2.w;
			return vector3;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000124F4 File Offset: 0x000106F4
		public static void ComputeOcclusion(Material lensFlareShader, LensFlareCommonSRP lensFlares, Camera cam, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, bool taaEnabled, int _FlareOcclusionTex, int _FlareOcclusionIndex, int _FlareTex, int _FlareColorValue, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4)
		{
			if (lensFlares.IsEmpty() || LensFlareCommonSRP.occlusionRT == null)
			{
				return;
			}
			Vector2 vector = new Vector2(actualWidth, actualHeight);
			float num = vector.x / vector.y;
			Vector2 vector2 = new Vector2(num, 1f);
			CoreUtils.SetRenderTarget(cmd, LensFlareCommonSRP.occlusionRT, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			if (!taaEnabled)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			float num2 = 1f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion;
			float num3 = 1f / (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded);
			float num4 = 0.5f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion;
			float num5 = 0.5f / (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded);
			int num6 = (taaEnabled ? 1 : 0);
			int num7 = 0;
			foreach (LensFlareComponentSRP lensFlareComponentSRP in lensFlares.GetData())
			{
				if (!(lensFlareComponentSRP == null))
				{
					LensFlareDataSRP lensFlareData = lensFlareComponentSRP.lensFlareData;
					if (lensFlareComponentSRP.enabled && lensFlareComponentSRP.gameObject.activeSelf && lensFlareComponentSRP.gameObject.activeInHierarchy && !(lensFlareData == null) && lensFlareData.elements != null && lensFlareData.elements.Length != 0 && lensFlareComponentSRP.useOcclusion && (!lensFlareComponentSRP.useOcclusion || lensFlareComponentSRP.sampleCount != 0U) && lensFlareComponentSRP.intensity > 0f)
					{
						Light component = lensFlareComponentSRP.GetComponent<Light>();
						bool flag = false;
						Vector3 vector3;
						if (component != null && component.type == LightType.Directional)
						{
							vector3 = -component.transform.forward * cam.farClipPlane;
							flag = true;
						}
						else
						{
							vector3 = lensFlareComponentSRP.transform.position;
						}
						Vector3 vector4 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector3);
						if (usePanini && cam == Camera.main)
						{
							vector4 = LensFlareCommonSRP.DoPaniniProjection(vector4, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
						}
						if (vector4.z >= 0f && (lensFlareComponentSRP.allowOffScreen || (vector4.x >= 0f && vector4.x <= 1f && vector4.y >= 0f && vector4.y <= 1f)))
						{
							float magnitude = (vector3 - cameraPositionWS).magnitude;
							float num8 = magnitude / lensFlareComponentSRP.maxAttenuationDistance;
							float num9 = magnitude / lensFlareComponentSRP.maxAttenuationScale;
							float num10 = ((!flag && lensFlareComponentSRP.distanceAttenuationCurve.length > 0) ? lensFlareComponentSRP.distanceAttenuationCurve.Evaluate(num8) : 1f);
							if (!flag && lensFlareComponentSRP.scaleByDistanceCurve.length >= 1)
							{
								lensFlareComponentSRP.scaleByDistanceCurve.Evaluate(num9);
							}
							Vector3 normalized = (cam.transform.position - lensFlareComponentSRP.transform.position).normalized;
							Vector3 vector5 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector3 + normalized * lensFlareComponentSRP.occlusionOffset);
							float num11 = (flag ? lensFlareComponentSRP.celestialProjectedOcclusionRadius(cam) : lensFlareComponentSRP.occlusionRadius);
							Vector2 vector6 = vector4;
							float magnitude2 = (LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector3 + cam.transform.up * num11) - vector6).magnitude;
							cmd.SetGlobalVector(_FlareData1, new Vector4(magnitude2, lensFlareComponentSRP.sampleCount, vector5.z, actualHeight / actualWidth));
							cmd.EnableShaderKeyword("FLARE_COMPUTE_OCCLUSION");
							Vector2 vector7 = new Vector2(2f * vector4.x - 1f, 1f - 2f * vector4.y);
							Vector2 vector8 = new Vector2(Mathf.Abs(vector7.x), Mathf.Abs(vector7.y));
							float num12 = Mathf.Max(vector8.x, vector8.y);
							float num13 = ((lensFlareComponentSRP.radialScreenAttenuationCurve.length > 0) ? lensFlareComponentSRP.radialScreenAttenuationCurve.Evaluate(num12) : 1f);
							if (lensFlareComponentSRP.intensity * num13 * num10 > 0f)
							{
								cmd.SetGlobalVector(_FlareOcclusionIndex, new Vector4((float)num7 * num2 + num4, num5, 0f, (float)(LensFlareCommonSRP.frameIdx + 1)));
								float num14 = Mathf.Cos(0f);
								float num15 = Mathf.Sin(0f);
								float num16 = 0f;
								float num17 = Mathf.Clamp01(0.999999f);
								cmd.SetGlobalVector(_FlareData3, new Vector4(lensFlareComponentSRP.allowOffScreen ? 1f : (-1f), num17, Mathf.Exp(Mathf.Lerp(0f, 4f, 1f)), 0.33333334f));
								Vector2 lensFlareRayOffset = LensFlareCommonSRP.GetLensFlareRayOffset(vector7, num16, num14, num15);
								Vector4 flareData = LensFlareCommonSRP.GetFlareData0(vector7, Vector2.one, lensFlareRayOffset, vector2, 0f, num16, 0f, Vector2.zero, false);
								cmd.SetGlobalVector(_FlareData0, flareData);
								cmd.SetGlobalVector(_FlareData2, new Vector4(vector7.x, vector7.y, 0f, 0f));
								cmd.SetViewport(new Rect
								{
									x = (float)num7,
									y = (float)((LensFlareCommonSRP.frameIdx + LensFlareCommonSRP.mergeNeeded) * num6),
									width = 1f,
									height = 1f
								});
								Blitter.DrawQuad(cmd, lensFlareShader, 4);
								num7++;
							}
						}
					}
				}
			}
			LensFlareCommonSRP.frameIdx++;
			LensFlareCommonSRP.frameIdx %= LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012AE0 File Offset: 0x00010CE0
		public static void DoLensFlareDataDrivenCommon(Material lensFlareShader, LensFlareCommonSRP lensFlares, Camera cam, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, RenderTargetIdentifier colorBuffer, Func<Light, Camera, Vector3, float> GetLensFlareLightAttenuation, int _FlareOcclusionTex, int _FlareOcclusionIndex, int _FlareTex, int _FlareColorValue, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4, bool debugView)
		{
			if (lensFlares.IsEmpty())
			{
				return;
			}
			Vector2 vector = new Vector2(actualWidth, actualHeight);
			float num = vector.x / vector.y;
			Vector2 vector2 = new Vector2(num, 1f);
			CoreUtils.SetRenderTarget(cmd, colorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(new Rect
			{
				width = vector.x,
				height = vector.y
			});
			if (debugView)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			int num2 = 0;
			foreach (LensFlareComponentSRP lensFlareComponentSRP in lensFlares.GetData())
			{
				if (!(lensFlareComponentSRP == null))
				{
					LensFlareDataSRP lensFlareData = lensFlareComponentSRP.lensFlareData;
					if (lensFlareComponentSRP.enabled && lensFlareComponentSRP.gameObject.activeSelf && lensFlareComponentSRP.gameObject.activeInHierarchy && !(lensFlareData == null) && lensFlareData.elements != null && lensFlareData.elements.Length != 0 && lensFlareComponentSRP.intensity > 0f)
					{
						Light component = lensFlareComponentSRP.GetComponent<Light>();
						bool flag = false;
						Vector3 vector3;
						if (component != null && component.type == LightType.Directional)
						{
							vector3 = -component.transform.forward * cam.farClipPlane;
							flag = true;
						}
						else
						{
							vector3 = lensFlareComponentSRP.transform.position;
						}
						Vector3 vector4 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector3);
						if (usePanini && cam == Camera.main)
						{
							vector4 = LensFlareCommonSRP.DoPaniniProjection(vector4, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
						}
						if (vector4.z >= 0f && (lensFlareComponentSRP.allowOffScreen || (vector4.x >= 0f && vector4.x <= 1f && vector4.y >= 0f && vector4.y <= 1f)))
						{
							Vector3 vector5 = vector3 - cameraPositionWS;
							if (Vector3.Dot(cam.transform.forward, vector5) >= 0f)
							{
								float magnitude = vector5.magnitude;
								float num3 = magnitude / lensFlareComponentSRP.maxAttenuationDistance;
								float num4 = magnitude / lensFlareComponentSRP.maxAttenuationScale;
								float num5 = ((!flag && lensFlareComponentSRP.distanceAttenuationCurve.length > 0) ? lensFlareComponentSRP.distanceAttenuationCurve.Evaluate(num3) : 1f);
								float num6 = ((!flag && lensFlareComponentSRP.scaleByDistanceCurve.length >= 1) ? lensFlareComponentSRP.scaleByDistanceCurve.Evaluate(num4) : 1f);
								Color color = Color.white;
								if (component != null && lensFlareComponentSRP.attenuationByLightShape)
								{
									color *= GetLensFlareLightAttenuation(component, cam, -vector5.normalized);
								}
								color *= num5;
								Vector3 normalized = (cam.transform.position - lensFlareComponentSRP.transform.position).normalized;
								Vector3 vector6 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector3 + normalized * lensFlareComponentSRP.occlusionOffset);
								float num7 = (flag ? lensFlareComponentSRP.celestialProjectedOcclusionRadius(cam) : lensFlareComponentSRP.occlusionRadius);
								Vector2 vector7 = vector4;
								float magnitude2 = (LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector3 + cam.transform.up * num7) - vector7).magnitude;
								cmd.SetGlobalVector(_FlareData1, new Vector4(magnitude2, lensFlareComponentSRP.sampleCount, vector6.z, actualHeight / actualWidth));
								if (lensFlareComponentSRP.useOcclusion)
								{
									cmd.EnableShaderKeyword("FLARE_OCCLUSION");
								}
								else
								{
									cmd.DisableShaderKeyword("FLARE_OCCLUSION");
								}
								if (LensFlareCommonSRP.occlusionRT != null)
								{
									cmd.SetGlobalTexture(_FlareOcclusionTex, LensFlareCommonSRP.occlusionRT);
								}
								cmd.SetGlobalVector(_FlareOcclusionIndex, new Vector4((float)num2 / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion + 0.5f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion, 0.5f, 0f, 0f));
								if (lensFlareComponentSRP.useOcclusion && lensFlareComponentSRP.sampleCount > 0U)
								{
									num2++;
								}
								LensFlareDataElementSRP[] elements = lensFlareData.elements;
								for (int i = 0; i < elements.Length; i++)
								{
									LensFlareCommonSRP.<>c__DisplayClass32_0 CS$<>8__locals1;
									CS$<>8__locals1.element = elements[i];
									if (CS$<>8__locals1.element != null && CS$<>8__locals1.element.visible && (!(CS$<>8__locals1.element.lensFlareTexture == null) || CS$<>8__locals1.element.flareType != SRPLensFlareType.Image) && CS$<>8__locals1.element.localIntensity > 0f && CS$<>8__locals1.element.count > 0 && CS$<>8__locals1.element.localIntensity > 0f)
									{
										Color color2 = color;
										if (component != null && CS$<>8__locals1.element.modulateByLightColor)
										{
											if (component.useColorTemperature)
											{
												color2 *= component.color * Mathf.CorrelatedColorTemperatureToRGB(component.colorTemperature);
											}
											else
											{
												color2 *= component.color;
											}
										}
										Color color3 = color2;
										LensFlareCommonSRP.<>c__DisplayClass32_1 CS$<>8__locals2;
										CS$<>8__locals2.screenPos = new Vector2(2f * vector4.x - 1f, 1f - 2f * vector4.y);
										Vector2 vector8 = new Vector2(Mathf.Abs(CS$<>8__locals2.screenPos.x), Mathf.Abs(CS$<>8__locals2.screenPos.y));
										float num8 = Mathf.Max(vector8.x, vector8.y);
										float num9 = ((lensFlareComponentSRP.radialScreenAttenuationCurve.length > 0) ? lensFlareComponentSRP.radialScreenAttenuationCurve.Evaluate(num8) : 1f);
										float num10 = lensFlareComponentSRP.intensity * CS$<>8__locals1.element.localIntensity * num9 * num5;
										if (num10 > 0f)
										{
											Texture lensFlareTexture = CS$<>8__locals1.element.lensFlareTexture;
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Image)
											{
												CS$<>8__locals2.usedAspectRatio = (CS$<>8__locals1.element.preserveAspectRatio ? ((float)lensFlareTexture.height / (float)lensFlareTexture.width) : 1f);
											}
											else
											{
												CS$<>8__locals2.usedAspectRatio = 1f;
											}
											float rotation = CS$<>8__locals1.element.rotation;
											Vector2 vector9;
											if (CS$<>8__locals1.element.preserveAspectRatio)
											{
												if (CS$<>8__locals2.usedAspectRatio >= 1f)
												{
													vector9 = new Vector2(CS$<>8__locals1.element.sizeXY.x / CS$<>8__locals2.usedAspectRatio, CS$<>8__locals1.element.sizeXY.y);
												}
												else
												{
													vector9 = new Vector2(CS$<>8__locals1.element.sizeXY.x, CS$<>8__locals1.element.sizeXY.y * CS$<>8__locals2.usedAspectRatio);
												}
											}
											else
											{
												vector9 = new Vector2(CS$<>8__locals1.element.sizeXY.x, CS$<>8__locals1.element.sizeXY.y);
											}
											float num11 = 0.1f;
											Vector2 vector10 = new Vector2(vector9.x, vector9.y);
											CS$<>8__locals2.combinedScale = num6 * num11 * CS$<>8__locals1.element.uniformScale * lensFlareComponentSRP.scale;
											vector10 *= CS$<>8__locals2.combinedScale;
											color3 *= CS$<>8__locals1.element.tint;
											color3 *= num10;
											float num12 = (SystemInfo.graphicsUVStartsAtTop ? CS$<>8__locals1.element.angularOffset : (-CS$<>8__locals1.element.angularOffset));
											CS$<>8__locals2.globalCos0 = Mathf.Cos(-num12 * 0.017453292f);
											CS$<>8__locals2.globalSin0 = Mathf.Sin(-num12 * 0.017453292f);
											CS$<>8__locals2.position = 2f * CS$<>8__locals1.element.position;
											SRPLensFlareBlendMode blendMode = CS$<>8__locals1.element.blendMode;
											int num13;
											if (blendMode == SRPLensFlareBlendMode.Additive)
											{
												num13 = 0;
											}
											else if (blendMode == SRPLensFlareBlendMode.Screen)
											{
												num13 = 1;
											}
											else if (blendMode == SRPLensFlareBlendMode.Premultiply)
											{
												num13 = 2;
											}
											else if (blendMode == SRPLensFlareBlendMode.Lerp)
											{
												num13 = 3;
											}
											else
											{
												num13 = 0;
											}
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Image)
											{
												cmd.DisableShaderKeyword("FLARE_CIRCLE");
												cmd.DisableShaderKeyword("FLARE_POLYGON");
											}
											else if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Circle)
											{
												cmd.EnableShaderKeyword("FLARE_CIRCLE");
												cmd.DisableShaderKeyword("FLARE_POLYGON");
											}
											else if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												cmd.DisableShaderKeyword("FLARE_CIRCLE");
												cmd.EnableShaderKeyword("FLARE_POLYGON");
											}
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Circle || CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												if (CS$<>8__locals1.element.inverseSDF)
												{
													cmd.EnableShaderKeyword("FLARE_INVERSE_SDF");
												}
												else
												{
													cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
												}
											}
											else
											{
												cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
											}
											if (CS$<>8__locals1.element.lensFlareTexture != null)
											{
												cmd.SetGlobalTexture(_FlareTex, CS$<>8__locals1.element.lensFlareTexture);
											}
											float num14 = Mathf.Clamp01(1f - CS$<>8__locals1.element.edgeOffset - 1E-06f);
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												num14 = Mathf.Pow(num14 + 1f, 5f);
											}
											float sdfRoundness = CS$<>8__locals1.element.sdfRoundness;
											cmd.SetGlobalVector(_FlareData3, new Vector4(lensFlareComponentSRP.allowOffScreen ? 1f : (-1f), num14, Mathf.Exp(Mathf.Lerp(0f, 4f, Mathf.Clamp01(1f - CS$<>8__locals1.element.fallOff))), 1f / (float)CS$<>8__locals1.element.sideCount));
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												float num15 = 1f / (float)CS$<>8__locals1.element.sideCount;
												float num16 = Mathf.Cos(3.1415927f * num15);
												float num17 = num16 * sdfRoundness;
												float num18 = num16 - num17;
												float num19 = 6.2831855f * num15;
												float num20 = num18 * Mathf.Tan(0.5f * num19);
												cmd.SetGlobalVector(_FlareData4, new Vector4(sdfRoundness, num18, num19, num20));
											}
											else
											{
												cmd.SetGlobalVector(_FlareData4, new Vector4(sdfRoundness, 0f, 0f, 0f));
											}
											if (!CS$<>8__locals1.element.allowMultipleElement || CS$<>8__locals1.element.count == 1)
											{
												Vector2 vector11 = vector10;
												Vector2 lensFlareRayOffset = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, CS$<>8__locals2.position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
												if (CS$<>8__locals1.element.enableRadialDistortion)
												{
													Vector2 lensFlareRayOffset2 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
													vector11 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|32_0(lensFlareRayOffset, lensFlareRayOffset2, vector11, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
												}
												Vector4 flareData = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset, vector2, rotation, CS$<>8__locals2.position, num12, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
												cmd.SetGlobalVector(_FlareData0, flareData);
												cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector11.x, vector11.y));
												cmd.SetGlobalVector(_FlareColorValue, color3);
												Blitter.DrawQuad(cmd, lensFlareShader, num13);
											}
											else
											{
												float num21 = 2f * CS$<>8__locals1.element.lengthSpread / (float)(CS$<>8__locals1.element.count - 1);
												if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Uniform)
												{
													float num22 = 0f;
													for (int j = 0; j < CS$<>8__locals1.element.count; j++)
													{
														Vector2 vector12 = vector10;
														Vector2 lensFlareRayOffset3 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, CS$<>8__locals2.position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
														if (CS$<>8__locals1.element.enableRadialDistortion)
														{
															Vector2 lensFlareRayOffset4 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
															vector12 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|32_0(lensFlareRayOffset3, lensFlareRayOffset4, vector12, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
														}
														float num23 = ((CS$<>8__locals1.element.count >= 2) ? ((float)j / (float)(CS$<>8__locals1.element.count - 1)) : 0.5f);
														Color color4 = CS$<>8__locals1.element.colorGradient.Evaluate(num23);
														Vector4 flareData2 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset3, vector2, rotation + num22, CS$<>8__locals2.position, num12, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
														cmd.SetGlobalVector(_FlareData0, flareData2);
														cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector12.x, vector12.y));
														cmd.SetGlobalVector(_FlareColorValue, color3 * color4);
														Blitter.DrawQuad(cmd, lensFlareShader, num13);
														CS$<>8__locals2.position += num21;
														num22 += CS$<>8__locals1.element.uniformAngle;
													}
												}
												else if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Random)
												{
													Random.State state = Random.state;
													Random.InitState(CS$<>8__locals1.element.seed);
													Vector2 vector13 = new Vector2(CS$<>8__locals2.globalSin0, CS$<>8__locals2.globalCos0);
													vector13 *= CS$<>8__locals1.element.positionVariation.y;
													for (int k = 0; k < CS$<>8__locals1.element.count; k++)
													{
														float num24 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|32_1(-1f, 1f) * CS$<>8__locals1.element.intensityVariation + 1f;
														Vector2 lensFlareRayOffset5 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, CS$<>8__locals2.position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
														Vector2 vector14 = vector10;
														if (CS$<>8__locals1.element.enableRadialDistortion)
														{
															Vector2 lensFlareRayOffset6 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
															vector14 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|32_0(lensFlareRayOffset5, lensFlareRayOffset6, vector14, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
														}
														vector14 += vector14 * (CS$<>8__locals1.element.scaleVariation * LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|32_1(-1f, 1f));
														Color color5 = CS$<>8__locals1.element.colorGradient.Evaluate(LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|32_1(0f, 1f));
														Vector2 vector15 = CS$<>8__locals1.element.positionOffset + LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|32_1(-1f, 1f) * vector13;
														float num25 = rotation + LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|32_1(-3.1415927f, 3.1415927f) * CS$<>8__locals1.element.rotationVariation;
														if (num24 > 0f)
														{
															Vector4 flareData3 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset5, vector2, num25, CS$<>8__locals2.position, num12, vector15, CS$<>8__locals1.element.autoRotate);
															cmd.SetGlobalVector(_FlareData0, flareData3);
															cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector14.x, vector14.y));
															cmd.SetGlobalVector(_FlareColorValue, color3 * color5 * num24);
															Blitter.DrawQuad(cmd, lensFlareShader, num13);
														}
														CS$<>8__locals2.position += num21;
														CS$<>8__locals2.position += 0.5f * num21 * LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|32_1(-1f, 1f) * CS$<>8__locals1.element.positionVariation.x;
													}
													Random.state = state;
												}
												else if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Curve)
												{
													for (int l = 0; l < CS$<>8__locals1.element.count; l++)
													{
														float num26 = ((CS$<>8__locals1.element.count >= 2) ? ((float)l / (float)(CS$<>8__locals1.element.count - 1)) : 0.5f);
														Color color6 = CS$<>8__locals1.element.colorGradient.Evaluate(num26);
														float num27 = ((CS$<>8__locals1.element.positionCurve.length > 0) ? CS$<>8__locals1.element.positionCurve.Evaluate(num26) : 1f);
														float num28 = CS$<>8__locals2.position + 2f * CS$<>8__locals1.element.lengthSpread * num27;
														Vector2 lensFlareRayOffset7 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, num28, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
														Vector2 vector16 = vector10;
														if (CS$<>8__locals1.element.enableRadialDistortion)
														{
															Vector2 lensFlareRayOffset8 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
															vector16 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|32_0(lensFlareRayOffset7, lensFlareRayOffset8, vector16, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
														}
														float num29 = ((CS$<>8__locals1.element.scaleCurve.length > 0) ? CS$<>8__locals1.element.scaleCurve.Evaluate(num26) : 1f);
														vector16 *= num29;
														float num30 = CS$<>8__locals1.element.uniformAngleCurve.Evaluate(num26) * (180f - 180f / (float)CS$<>8__locals1.element.count);
														Vector4 flareData4 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset7, vector2, rotation + num30, num28, num12, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
														cmd.SetGlobalVector(_FlareData0, flareData4);
														cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector16.x, vector16.y));
														cmd.SetGlobalVector(_FlareColorValue, color3 * color6);
														Blitter.DrawQuad(cmd, lensFlareShader, num13);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00013D68 File Offset: 0x00011F68
		public void RemoveData(LensFlareComponentSRP data)
		{
			if (LensFlareCommonSRP.m_Data.Contains(data))
			{
				LensFlareCommonSRP.m_Data.Remove(data);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013D84 File Offset: 0x00011F84
		private static Vector2 DoPaniniProjection(Vector2 screenPos, float actualWidth, float actualHeight, float fieldOfView, float paniniProjectionCropToFit, float paniniProjectionDistance)
		{
			Vector2 vector = LensFlareCommonSRP.CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			Vector2 vector2 = LensFlareCommonSRP.CalcCropExtents(actualWidth, actualHeight, fieldOfView, paniniProjectionDistance);
			float num = vector2.x / vector.x;
			float num2 = vector2.y / vector.y;
			float num3 = Mathf.Min(num, num2);
			float num4 = Mathf.Lerp(1f, Mathf.Clamp01(num3), paniniProjectionCropToFit);
			Vector2 vector3 = LensFlareCommonSRP.Panini_Generic_Inv(new Vector2(2f * screenPos.x - 1f, 2f * screenPos.y - 1f) * vector, paniniProjectionDistance) / (vector * num4);
			return new Vector2(0.5f * vector3.x + 0.5f, 0.5f * vector3.y + 0.5f);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013E50 File Offset: 0x00012050
		private static Vector2 CalcViewExtents(float actualWidth, float actualHeight, float fieldOfView)
		{
			float num = fieldOfView * 0.017453292f;
			float num2 = actualWidth / actualHeight;
			float num3 = Mathf.Tan(0.5f * num);
			return new Vector2(num2 * num3, num3);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013E80 File Offset: 0x00012080
		private static Vector2 CalcCropExtents(float actualWidth, float actualHeight, float fieldOfView, float d)
		{
			float num = 1f + d;
			Vector2 vector = LensFlareCommonSRP.CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			float num2 = Mathf.Sqrt(vector.x * vector.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return vector * num3 * (num / num4);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00013ED4 File Offset: 0x000120D4
		private static Vector2 Panini_Generic_Inv(Vector2 projPos, float d)
		{
			float num = 1f + d;
			float num2 = Mathf.Sqrt(projPos.x * projPos.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return projPos * num3 * (num / num4);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013F5B File Offset: 0x0001215B
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__Fpo|23_0(float d, float l)
		{
			return l / (d * (d * d + l * l)) + Mathf.Atan(l / d) / (d * d);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013F75 File Offset: 0x00012175
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__Fwt|23_1(float d, float l)
		{
			return l * l / (d * (d * d + l * l));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013F84 File Offset: 0x00012184
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|23_2(Vector3 p1, Vector3 p2)
		{
			Vector3 normalized = (p2 - p1).normalized;
			float num;
			if ((double)p1.z <= 0.0 && (double)p2.z <= 0.0)
			{
				num = 0f;
			}
			else
			{
				if ((double)p1.z < 0.0)
				{
					p1 = (p1 * p2.z - p2 * p1.z) / (p2.z - p1.z);
				}
				if ((double)p2.z < 0.0)
				{
					p2 = (-p1 * p2.z + p2 * p1.z) / (-p2.z + p1.z);
				}
				float num2 = Vector3.Dot(p1, normalized);
				float num3 = Vector3.Dot(p2, normalized);
				Vector3 vector = p1 - num2 * normalized;
				float magnitude = vector.magnitude;
				num = ((LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fpo|23_0(magnitude, num3) - LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fpo|23_0(magnitude, num2)) * vector.z + (LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fwt|23_1(magnitude, num3) - LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fwt|23_1(magnitude, num2)) * normalized.z) / 3.1415927f;
			}
			return num;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000140C4 File Offset: 0x000122C4
		[CompilerGenerated]
		internal static Vector2 <DoLensFlareDataDrivenCommon>g__ComputeLocalSize|32_0(Vector2 rayOff, Vector2 rayOff0, Vector2 curSize, AnimationCurve distortionCurve, ref LensFlareCommonSRP.<>c__DisplayClass32_0 A_4, ref LensFlareCommonSRP.<>c__DisplayClass32_1 A_5)
		{
			LensFlareCommonSRP.GetLensFlareRayOffset(A_5.screenPos, A_5.position, A_5.globalCos0, A_5.globalSin0);
			float num;
			if (!A_4.element.distortionRelativeToCenter)
			{
				Vector2 vector = (rayOff - rayOff0) * 0.5f;
				num = Mathf.Clamp01(Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y)));
			}
			else
			{
				num = Mathf.Clamp01((A_5.screenPos + (rayOff + new Vector2(A_4.element.positionOffset.x, -A_4.element.positionOffset.y)) * A_4.element.translationScale).magnitude);
			}
			float num2 = Mathf.Clamp01(distortionCurve.Evaluate(num));
			return new Vector2(Mathf.Lerp(curSize.x, A_4.element.targetSizeDistortion.x * A_5.combinedScale / A_5.usedAspectRatio, num2), Mathf.Lerp(curSize.y, A_4.element.targetSizeDistortion.y * A_5.combinedScale, num2));
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000141F3 File Offset: 0x000123F3
		[CompilerGenerated]
		internal static float <DoLensFlareDataDrivenCommon>g__RandomRange|32_1(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x04000261 RID: 609
		private static LensFlareCommonSRP m_Instance = null;

		// Token: 0x04000262 RID: 610
		private static readonly object m_Padlock = new object();

		// Token: 0x04000263 RID: 611
		private static List<LensFlareComponentSRP> m_Data = new List<LensFlareComponentSRP>();

		// Token: 0x04000264 RID: 612
		public static int maxLensFlareWithOcclusion = 128;

		// Token: 0x04000265 RID: 613
		public static int maxLensFlareWithOcclusionTemporalSample = 8;

		// Token: 0x04000266 RID: 614
		public static int mergeNeeded = 1;

		// Token: 0x04000267 RID: 615
		public static RTHandle occlusionRT = null;

		// Token: 0x04000268 RID: 616
		private static int frameIdx = 0;
	}
}
