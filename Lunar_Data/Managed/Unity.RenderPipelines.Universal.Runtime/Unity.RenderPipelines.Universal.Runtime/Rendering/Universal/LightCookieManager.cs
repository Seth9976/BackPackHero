using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Mathematics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000080 RID: 128
	internal class LightCookieManager : IDisposable
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		internal bool IsKeywordLightCookieEnabled { get; private set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001BAD9 File Offset: 0x00019CD9
		public LightCookieManager(ref LightCookieManager.Settings settings)
		{
			this.m_Settings = settings;
			this.m_WorkMem = new LightCookieManager.WorkMemory();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001BB08 File Offset: 0x00019D08
		private void InitAdditionalLights(int size)
		{
			Vector2Int vector2Int;
			if (this.m_Settings.atlas.useMips)
			{
				LightCookieManager.Settings.AtlasSettings atlas = this.m_Settings.atlas;
				if (atlas.isPow2)
				{
					vector2Int = this.m_Settings.atlas.resolution;
					this.m_AdditionalLightsCookieAtlas = new PowerOfTwoTextureAtlas(vector2Int.x, 4, this.m_Settings.atlas.format, FilterMode.Bilinear, "Universal Light Cookie Pow2 Atlas", true);
					goto IL_00B7;
				}
			}
			vector2Int = this.m_Settings.atlas.resolution;
			int x = vector2Int.x;
			vector2Int = this.m_Settings.atlas.resolution;
			this.m_AdditionalLightsCookieAtlas = new Texture2DAtlas(x, vector2Int.y, this.m_Settings.atlas.format, FilterMode.Bilinear, false, "Universal Light Cookie Atlas", false);
			IL_00B7:
			this.m_AdditionalLightsCookieShaderData = new LightCookieManager.LightCookieShaderData(size, this.m_Settings.useStructuredBuffer);
			this.m_VisibleLightIndexToShaderDataIndex = new int[this.m_Settings.maxAdditionalLights + 1];
			this.m_CookieSizeDivisor = 1;
			this.m_PrevCookieRequestPixelCount = uint.MaxValue;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001BC09 File Offset: 0x00019E09
		public bool isInitialized()
		{
			return this.m_AdditionalLightsCookieAtlas != null && this.m_AdditionalLightsCookieShaderData != null;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001BC1E File Offset: 0x00019E1E
		public void Dispose()
		{
			Texture2DAtlas additionalLightsCookieAtlas = this.m_AdditionalLightsCookieAtlas;
			if (additionalLightsCookieAtlas != null)
			{
				additionalLightsCookieAtlas.Release();
			}
			LightCookieManager.LightCookieShaderData additionalLightsCookieShaderData = this.m_AdditionalLightsCookieShaderData;
			if (additionalLightsCookieShaderData == null)
			{
				return;
			}
			additionalLightsCookieShaderData.Dispose();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001BC41 File Offset: 0x00019E41
		public int GetLightCookieShaderDataIndex(int visibleLightIndex)
		{
			if (!this.isInitialized())
			{
				return -1;
			}
			return this.m_VisibleLightIndexToShaderDataIndex[visibleLightIndex];
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001BC58 File Offset: 0x00019E58
		public void Setup(ScriptableRenderContext ctx, CommandBuffer cmd, ref LightData lightData)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.LightCookies)))
			{
				bool flag = lightData.mainLightIndex >= 0;
				if (flag)
				{
					VisibleLight visibleLight = lightData.visibleLights[lightData.mainLightIndex];
					flag = this.SetupMainLight(cmd, ref visibleLight);
				}
				bool flag2 = lightData.additionalLightsCount > 0;
				if (flag2)
				{
					flag2 = this.SetupAdditionalLights(cmd, ref lightData);
				}
				if (!flag2)
				{
					if (this.m_VisibleLightIndexToShaderDataIndex != null && this.m_AdditionalLightsCookieShaderData.isUploaded)
					{
						int num = Math.Min(this.m_VisibleLightIndexToShaderDataIndex.Length, lightData.visibleLights.Length);
						for (int i = 0; i < num; i++)
						{
							this.m_VisibleLightIndexToShaderDataIndex[i] = -1;
						}
					}
					LightCookieManager.LightCookieShaderData additionalLightsCookieShaderData = this.m_AdditionalLightsCookieShaderData;
					if (additionalLightsCookieShaderData != null)
					{
						additionalLightsCookieShaderData.Clear(cmd);
					}
				}
				this.IsKeywordLightCookieEnabled = flag || flag2;
				CoreUtils.SetKeyword(cmd, "_LIGHT_COOKIES", this.IsKeywordLightCookieEnabled);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001BD50 File Offset: 0x00019F50
		private bool SetupMainLight(CommandBuffer cmd, ref VisibleLight visibleMainLight)
		{
			Light light = visibleMainLight.light;
			Texture cookie = light.cookie;
			bool flag = cookie != null;
			if (flag)
			{
				Matrix4x4 identity = Matrix4x4.identity;
				float num = (float)this.GetLightCookieShaderFormat(cookie.graphicsFormat);
				UniversalAdditionalLightData universalAdditionalLightData;
				if (light.TryGetComponent<UniversalAdditionalLightData>(out universalAdditionalLightData))
				{
					this.GetLightUVScaleOffset(ref universalAdditionalLightData, ref identity);
				}
				Matrix4x4 matrix4x = LightCookieManager.s_DirLightProj * identity * visibleMainLight.localToWorldMatrix.inverse;
				cmd.SetGlobalTexture(LightCookieManager.ShaderProperty.mainLightTexture, cookie);
				cmd.SetGlobalMatrix(LightCookieManager.ShaderProperty.mainLightWorldToLight, matrix4x);
				cmd.SetGlobalFloat(LightCookieManager.ShaderProperty.mainLightCookieTextureFormat, num);
				return flag;
			}
			cmd.SetGlobalTexture(LightCookieManager.ShaderProperty.mainLightTexture, Texture2D.whiteTexture);
			cmd.SetGlobalMatrix(LightCookieManager.ShaderProperty.mainLightWorldToLight, Matrix4x4.identity);
			cmd.SetGlobalFloat(LightCookieManager.ShaderProperty.mainLightCookieTextureFormat, -1f);
			return flag;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001BE20 File Offset: 0x0001A020
		private LightCookieManager.LightCookieShaderFormat GetLightCookieShaderFormat(GraphicsFormat cookieFormat)
		{
			if (cookieFormat <= GraphicsFormat.R16_UInt)
			{
				if (cookieFormat <= GraphicsFormat.R8_UInt)
				{
					if (cookieFormat <= GraphicsFormat.R8_UNorm)
					{
						if (cookieFormat == GraphicsFormat.R8_SRGB || cookieFormat == GraphicsFormat.R8_UNorm)
						{
							return LightCookieManager.LightCookieShaderFormat.Red;
						}
					}
					else if (cookieFormat == GraphicsFormat.R8_SNorm || cookieFormat == GraphicsFormat.R8_UInt)
					{
						return LightCookieManager.LightCookieShaderFormat.Red;
					}
				}
				else if (cookieFormat <= GraphicsFormat.R16_UNorm)
				{
					if (cookieFormat == GraphicsFormat.R8_SInt || cookieFormat == GraphicsFormat.R16_UNorm)
					{
						return LightCookieManager.LightCookieShaderFormat.Red;
					}
				}
				else if (cookieFormat == GraphicsFormat.R16_SNorm || cookieFormat == GraphicsFormat.R16_UInt)
				{
					return LightCookieManager.LightCookieShaderFormat.Red;
				}
			}
			else if (cookieFormat <= GraphicsFormat.R16_SFloat)
			{
				if (cookieFormat <= GraphicsFormat.R32_UInt)
				{
					if (cookieFormat == GraphicsFormat.R16_SInt || cookieFormat == GraphicsFormat.R32_UInt)
					{
						return LightCookieManager.LightCookieShaderFormat.Red;
					}
				}
				else if (cookieFormat == GraphicsFormat.R32_SInt || cookieFormat == GraphicsFormat.R16_SFloat)
				{
					return LightCookieManager.LightCookieShaderFormat.Red;
				}
			}
			else if (cookieFormat <= (GraphicsFormat)55)
			{
				if (cookieFormat == GraphicsFormat.R32_SFloat)
				{
					return LightCookieManager.LightCookieShaderFormat.Red;
				}
				if (cookieFormat - (GraphicsFormat)54 <= 1)
				{
					return LightCookieManager.LightCookieShaderFormat.Alpha;
				}
			}
			else if (cookieFormat - GraphicsFormat.R_BC4_UNorm <= 1 || cookieFormat - GraphicsFormat.R_EAC_UNorm <= 1)
			{
				return LightCookieManager.LightCookieShaderFormat.Red;
			}
			return LightCookieManager.LightCookieShaderFormat.RGB;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		private void GetLightUVScaleOffset(ref UniversalAdditionalLightData additionalLightData, ref Matrix4x4 uvTransform)
		{
			Vector2 vector = Vector2.one / additionalLightData.lightCookieSize;
			Vector2 lightCookieOffset = additionalLightData.lightCookieOffset;
			if (Mathf.Abs(vector.x) < half.MinValue)
			{
				vector.x = Mathf.Sign(vector.x) * half.MinValue;
			}
			if (Mathf.Abs(vector.y) < half.MinValue)
			{
				vector.y = Mathf.Sign(vector.y) * half.MinValue;
			}
			uvTransform = Matrix4x4.Scale(new Vector3(vector.x, vector.y, 1f));
			uvTransform.SetColumn(3, new Vector4(-lightCookieOffset.x * vector.x, -lightCookieOffset.y * vector.y, 0f, 1f));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001BF88 File Offset: 0x0001A188
		private bool SetupAdditionalLights(CommandBuffer cmd, ref LightData lightData)
		{
			int num = Math.Min(this.m_Settings.maxAdditionalLights, lightData.visibleLights.Length);
			this.m_WorkMem.Resize(num);
			int num2 = this.FilterAndValidateAdditionalLights(ref lightData, this.m_WorkMem.lightMappings);
			if (num2 <= 0)
			{
				return false;
			}
			if (!this.isInitialized())
			{
				this.InitAdditionalLights(num2);
			}
			LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> workSlice = new LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping>(this.m_WorkMem.lightMappings, num2);
			int num3 = this.UpdateAdditionalLightsAtlas(cmd, ref workSlice, this.m_WorkMem.uvRects);
			LightCookieManager.WorkSlice<Vector4> workSlice2 = new LightCookieManager.WorkSlice<Vector4>(this.m_WorkMem.uvRects, num3);
			this.UploadAdditionalLights(cmd, ref lightData, ref workSlice, ref workSlice2);
			return workSlice2.length > 0;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001C038 File Offset: 0x0001A238
		private int FilterAndValidateAdditionalLights(ref LightData lightData, LightCookieManager.LightCookieMapping[] validLightMappings)
		{
			int mainLightIndex = lightData.mainLightIndex;
			int num = 0;
			int num2 = 0;
			int num3 = Math.Min(lightData.visibleLights.Length, validLightMappings.Length);
			for (int i = 0; i < num3; i++)
			{
				if (i == mainLightIndex)
				{
					num--;
				}
				else
				{
					Light light = lightData.visibleLights[i].light;
					if (!(light.cookie == null))
					{
						LightType lightType = lightData.visibleLights[i].lightType;
						if (lightType != LightType.Spot && lightType != LightType.Point)
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Additional ",
								lightType.ToString(),
								" light called '",
								light.name,
								"' has a light cookie which will not be visible."
							}), light);
						}
						else
						{
							LightCookieManager.LightCookieMapping lightCookieMapping;
							lightCookieMapping.visibleLightIndex = (ushort)i;
							lightCookieMapping.lightBufferIndex = (ushort)(i + num);
							lightCookieMapping.light = light;
							validLightMappings[num2++] = lightCookieMapping;
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001C148 File Offset: 0x0001A348
		private int UpdateAdditionalLightsAtlas(CommandBuffer cmd, ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings, Vector4[] textureAtlasUVRects)
		{
			validLightMappings.Sort(LightCookieManager.LightCookieMapping.s_CompareByCookieSize);
			uint num = this.ComputeCookieRequestPixelCount(ref validLightMappings);
			Vector2Int referenceSize = this.m_AdditionalLightsCookieAtlas.AtlasTexture.referenceSize;
			float num2 = num / (float)(referenceSize.x * referenceSize.y);
			int num3 = this.ApproximateCookieSizeDivisor(num2);
			if (num3 < this.m_CookieSizeDivisor && num < this.m_PrevCookieRequestPixelCount)
			{
				this.m_AdditionalLightsCookieAtlas.ResetAllocator();
				this.m_CookieSizeDivisor = num3;
			}
			int i = 0;
			while (i <= 0)
			{
				i = this.FetchUVRects(cmd, ref validLightMappings, textureAtlasUVRects, this.m_CookieSizeDivisor);
				if (i <= 0)
				{
					this.m_AdditionalLightsCookieAtlas.ResetAllocator();
					this.m_CookieSizeDivisor = Mathf.Max(this.m_CookieSizeDivisor + 1, num3);
					this.m_PrevCookieRequestPixelCount = num;
				}
			}
			return i;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001C204 File Offset: 0x0001A404
		private int FetchUVRects(CommandBuffer cmd, ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings, Vector4[] textureAtlasUVRects, int cookieSizeDivisor)
		{
			int num = 0;
			int i = 0;
			while (i < validLightMappings.length)
			{
				Texture cookie = validLightMappings[i].light.cookie;
				Vector4 vector = Vector4.zero;
				if (cookie.dimension == TextureDimension.Cube)
				{
					vector = this.FetchCube(cmd, cookie, cookieSizeDivisor);
				}
				else
				{
					vector = this.Fetch2D(cmd, cookie, cookieSizeDivisor);
				}
				if (!(vector != Vector4.zero))
				{
					if (cookieSizeDivisor > 16)
					{
						Debug.LogWarning("Light cookies atlas is extremely full! Some of the light cookies were discarded. Increase light cookie atlas space or reduce the amount of unique light cookies.");
						return num;
					}
					return 0;
				}
				else
				{
					if (!SystemInfo.graphicsUVStartsAtTop)
					{
						vector.w = 1f - vector.w - vector.y;
					}
					textureAtlasUVRects[num++] = vector;
					i++;
				}
			}
			return num;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		private uint ComputeCookieRequestPixelCount(ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings)
		{
			uint num = 0U;
			int num2 = 0;
			for (int i = 0; i < validLightMappings.length; i++)
			{
				Texture cookie = validLightMappings[i].light.cookie;
				int instanceID = cookie.GetInstanceID();
				if (instanceID != num2)
				{
					num2 = instanceID;
					int num3 = cookie.width * cookie.height;
					num += (uint)num3;
				}
			}
			return num;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001C30D File Offset: 0x0001A50D
		private int ApproximateCookieSizeDivisor(float requestAtlasRatio)
		{
			return (int)Mathf.Max(Mathf.Ceil(Mathf.Sqrt(requestAtlasRatio)), 1f);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001C328 File Offset: 0x0001A528
		private Vector4 Fetch2D(CommandBuffer cmd, Texture cookie, int cookieSizeDivisor = 1)
		{
			Vector4 zero = Vector4.zero;
			int num = Mathf.Max(cookie.width / cookieSizeDivisor, 4);
			int num2 = Mathf.Max(cookie.height / cookieSizeDivisor, 4);
			Vector2 vector = new Vector2((float)num, (float)num2);
			if (this.m_AdditionalLightsCookieAtlas.IsCached(out zero, cookie))
			{
				this.m_AdditionalLightsCookieAtlas.UpdateTexture(cmd, cookie, ref zero, true, true);
			}
			else
			{
				this.m_AdditionalLightsCookieAtlas.AllocateTexture(cmd, ref zero, cookie, num, num2, -1);
			}
			this.AdjustUVRect(ref zero, cookie, ref vector);
			return zero;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		private Vector4 FetchCube(CommandBuffer cmd, Texture cookie, int cookieSizeDivisor = 1)
		{
			Vector4 zero = Vector4.zero;
			int num = Mathf.Max(this.ComputeOctahedralCookieSize(cookie) / cookieSizeDivisor, 4);
			if (this.m_AdditionalLightsCookieAtlas.IsCached(out zero, cookie))
			{
				this.m_AdditionalLightsCookieAtlas.UpdateTexture(cmd, cookie, ref zero, true, true);
			}
			else
			{
				this.m_AdditionalLightsCookieAtlas.AllocateTexture(cmd, ref zero, cookie, num, num, -1);
			}
			Vector2 vector = Vector2.one * (float)num;
			this.AdjustUVRect(ref zero, cookie, ref vector);
			return zero;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001C41C File Offset: 0x0001A61C
		private int ComputeOctahedralCookieSize(Texture cookie)
		{
			int num = Math.Max(cookie.width, cookie.height);
			LightCookieManager.Settings.AtlasSettings atlas = this.m_Settings.atlas;
			if (atlas.isPow2)
			{
				num *= Mathf.NextPowerOfTwo((int)this.m_Settings.cubeOctahedralSizeScale);
			}
			else
			{
				num = (int)((float)num * this.m_Settings.cubeOctahedralSizeScale + 0.5f);
			}
			return num;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001C480 File Offset: 0x0001A680
		private void AdjustUVRect(ref Vector4 uvScaleOffset, Texture cookie, ref Vector2 cookieSize)
		{
			if (uvScaleOffset != Vector4.zero)
			{
				if (this.m_Settings.atlas.useMips)
				{
					PowerOfTwoTextureAtlas powerOfTwoTextureAtlas = this.m_AdditionalLightsCookieAtlas as PowerOfTwoTextureAtlas;
					int num = ((powerOfTwoTextureAtlas == null) ? 1 : powerOfTwoTextureAtlas.mipPadding);
					Vector2 vector = Vector2.one * (float)((int)Mathf.Pow(2f, (float)num)) * 2f;
					uvScaleOffset = PowerOfTwoTextureAtlas.GetPayloadScaleOffset(in cookieSize, in vector, in uvScaleOffset);
					return;
				}
				this.ShrinkUVRect(ref uvScaleOffset, 0.5f, ref cookieSize);
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001C50C File Offset: 0x0001A70C
		private void ShrinkUVRect(ref Vector4 uvScaleOffset, float amountPixels, ref Vector2 cookieSize)
		{
			Vector2 vector = Vector2.one * amountPixels / cookieSize;
			Vector2 vector2 = (cookieSize - Vector2.one * (amountPixels * 2f)) / cookieSize;
			uvScaleOffset.z += uvScaleOffset.x * vector.x;
			uvScaleOffset.w += uvScaleOffset.y * vector.y;
			uvScaleOffset.x *= vector2.x;
			uvScaleOffset.y *= vector2.y;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
		private void UploadAdditionalLights(CommandBuffer cmd, ref LightData lightData, ref LightCookieManager.WorkSlice<LightCookieManager.LightCookieMapping> validLightMappings, ref LightCookieManager.WorkSlice<Vector4> validUvRects)
		{
			cmd.SetGlobalTexture(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasTexture, this.m_AdditionalLightsCookieAtlas.AtlasTexture);
			cmd.SetGlobalFloat(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasTextureFormat, (float)this.GetLightCookieShaderFormat(this.m_AdditionalLightsCookieAtlas.AtlasTexture.rt.graphicsFormat));
			if (this.m_VisibleLightIndexToShaderDataIndex.Length < lightData.visibleLights.Length)
			{
				this.m_VisibleLightIndexToShaderDataIndex = new int[lightData.visibleLights.Length];
			}
			int num = Math.Min(this.m_VisibleLightIndexToShaderDataIndex.Length, lightData.visibleLights.Length);
			for (int i = 0; i < num; i++)
			{
				this.m_VisibleLightIndexToShaderDataIndex[i] = -1;
			}
			this.m_AdditionalLightsCookieShaderData.Resize(this.m_Settings.maxAdditionalLights);
			Matrix4x4[] worldToLights = this.m_AdditionalLightsCookieShaderData.worldToLights;
			LightCookieManager.ShaderBitArray cookieEnableBits = this.m_AdditionalLightsCookieShaderData.cookieEnableBits;
			Vector4[] atlasUVRects = this.m_AdditionalLightsCookieShaderData.atlasUVRects;
			float[] lightTypes = this.m_AdditionalLightsCookieShaderData.lightTypes;
			Array.Clear(atlasUVRects, 0, atlasUVRects.Length);
			cookieEnableBits.Clear();
			for (int j = 0; j < validUvRects.length; j++)
			{
				int visibleLightIndex = (int)validLightMappings[j].visibleLightIndex;
				int lightBufferIndex = (int)validLightMappings[j].lightBufferIndex;
				this.m_VisibleLightIndexToShaderDataIndex[visibleLightIndex] = lightBufferIndex;
				VisibleLight visibleLight = lightData.visibleLights[visibleLightIndex];
				lightTypes[lightBufferIndex] = (float)visibleLight.lightType;
				worldToLights[lightBufferIndex] = visibleLight.localToWorldMatrix.inverse;
				atlasUVRects[lightBufferIndex] = validUvRects[j];
				cookieEnableBits[lightBufferIndex] = true;
				if (visibleLight.lightType == LightType.Spot)
				{
					float spotAngle = visibleLight.spotAngle;
					float range = visibleLight.range;
					Matrix4x4 matrix4x = Matrix4x4.Perspective(spotAngle, 1f, 0.001f, range);
					matrix4x.SetColumn(2, matrix4x.GetColumn(2) * -1f);
					worldToLights[lightBufferIndex] = matrix4x * worldToLights[lightBufferIndex];
				}
			}
			this.m_AdditionalLightsCookieShaderData.Upload(cmd);
		}

		// Token: 0x0400035A RID: 858
		private static readonly Matrix4x4 s_DirLightProj = Matrix4x4.Ortho(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f, 0.5f);

		// Token: 0x0400035B RID: 859
		private Texture2DAtlas m_AdditionalLightsCookieAtlas;

		// Token: 0x0400035C RID: 860
		private LightCookieManager.LightCookieShaderData m_AdditionalLightsCookieShaderData;

		// Token: 0x0400035D RID: 861
		private readonly LightCookieManager.Settings m_Settings;

		// Token: 0x0400035E RID: 862
		private LightCookieManager.WorkMemory m_WorkMem;

		// Token: 0x0400035F RID: 863
		private int[] m_VisibleLightIndexToShaderDataIndex;

		// Token: 0x04000360 RID: 864
		private const int k_MaxCookieSizeDivisor = 16;

		// Token: 0x04000361 RID: 865
		private int m_CookieSizeDivisor = 1;

		// Token: 0x04000362 RID: 866
		private uint m_PrevCookieRequestPixelCount = uint.MaxValue;

		// Token: 0x02000172 RID: 370
		private static class ShaderProperty
		{
			// Token: 0x0400097A RID: 2426
			public static readonly int mainLightTexture = Shader.PropertyToID("_MainLightCookieTexture");

			// Token: 0x0400097B RID: 2427
			public static readonly int mainLightWorldToLight = Shader.PropertyToID("_MainLightWorldToLight");

			// Token: 0x0400097C RID: 2428
			public static readonly int mainLightCookieTextureFormat = Shader.PropertyToID("_MainLightCookieTextureFormat");

			// Token: 0x0400097D RID: 2429
			public static readonly int additionalLightsCookieAtlasTexture = Shader.PropertyToID("_AdditionalLightsCookieAtlasTexture");

			// Token: 0x0400097E RID: 2430
			public static readonly int additionalLightsCookieAtlasTextureFormat = Shader.PropertyToID("_AdditionalLightsCookieAtlasTextureFormat");

			// Token: 0x0400097F RID: 2431
			public static readonly int additionalLightsCookieEnableBits = Shader.PropertyToID("_AdditionalLightsCookieEnableBits");

			// Token: 0x04000980 RID: 2432
			public static readonly int additionalLightsCookieAtlasUVRectBuffer = Shader.PropertyToID("_AdditionalLightsCookieAtlasUVRectBuffer");

			// Token: 0x04000981 RID: 2433
			public static readonly int additionalLightsCookieAtlasUVRects = Shader.PropertyToID("_AdditionalLightsCookieAtlasUVRects");

			// Token: 0x04000982 RID: 2434
			public static readonly int additionalLightsWorldToLightBuffer = Shader.PropertyToID("_AdditionalLightsWorldToLightBuffer");

			// Token: 0x04000983 RID: 2435
			public static readonly int additionalLightsLightTypeBuffer = Shader.PropertyToID("_AdditionalLightsLightTypeBuffer");

			// Token: 0x04000984 RID: 2436
			public static readonly int additionalLightsWorldToLights = Shader.PropertyToID("_AdditionalLightsWorldToLights");

			// Token: 0x04000985 RID: 2437
			public static readonly int additionalLightsLightTypes = Shader.PropertyToID("_AdditionalLightsLightTypes");
		}

		// Token: 0x02000173 RID: 371
		private enum LightCookieShaderFormat
		{
			// Token: 0x04000987 RID: 2439
			None = -1,
			// Token: 0x04000988 RID: 2440
			RGB,
			// Token: 0x04000989 RID: 2441
			Alpha,
			// Token: 0x0400098A RID: 2442
			Red
		}

		// Token: 0x02000174 RID: 372
		public struct Settings
		{
			// Token: 0x060009A6 RID: 2470 RVA: 0x00040528 File Offset: 0x0003E728
			public static LightCookieManager.Settings GetDefault()
			{
				LightCookieManager.Settings settings;
				settings.atlas.resolution = new Vector2Int(1024, 1024);
				settings.atlas.format = GraphicsFormat.R8G8B8A8_SRGB;
				settings.atlas.useMips = false;
				settings.maxAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
				settings.cubeOctahedralSizeScale = ((settings.atlas.useMips && settings.atlas.isPow2) ? 2f : 2.5f);
				settings.useStructuredBuffer = RenderingUtils.useStructuredBuffer;
				return settings;
			}

			// Token: 0x0400098B RID: 2443
			public LightCookieManager.Settings.AtlasSettings atlas;

			// Token: 0x0400098C RID: 2444
			public int maxAdditionalLights;

			// Token: 0x0400098D RID: 2445
			public float cubeOctahedralSizeScale;

			// Token: 0x0400098E RID: 2446
			public bool useStructuredBuffer;

			// Token: 0x020001DF RID: 479
			public struct AtlasSettings
			{
				// Token: 0x17000241 RID: 577
				// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000434E8 File Offset: 0x000416E8
				public bool isPow2
				{
					get
					{
						return Mathf.IsPowerOfTwo(this.resolution.x) && Mathf.IsPowerOfTwo(this.resolution.y);
					}
				}

				// Token: 0x17000242 RID: 578
				// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0004350E File Offset: 0x0004170E
				public bool isSquare
				{
					get
					{
						return this.resolution.x == this.resolution.y;
					}
				}

				// Token: 0x04000B73 RID: 2931
				public Vector2Int resolution;

				// Token: 0x04000B74 RID: 2932
				public GraphicsFormat format;

				// Token: 0x04000B75 RID: 2933
				public bool useMips;
			}
		}

		// Token: 0x02000175 RID: 373
		private struct Sorting
		{
			// Token: 0x060009A7 RID: 2471 RVA: 0x000405B1 File Offset: 0x0003E7B1
			public static void QuickSort<T>(T[] data, Func<T, T, int> compare)
			{
				LightCookieManager.Sorting.QuickSort<T>(data, 0, data.Length - 1, compare);
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x000405C0 File Offset: 0x0003E7C0
			public static void QuickSort<T>(T[] data, int start, int end, Func<T, T, int> compare)
			{
				int num = end - start;
				if (num < 1)
				{
					return;
				}
				if (num < 8)
				{
					LightCookieManager.Sorting.InsertionSort<T>(data, start, end, compare);
					return;
				}
				if (start < end)
				{
					int num2 = LightCookieManager.Sorting.Partition<T>(data, start, end, compare);
					if (num2 >= 1)
					{
						LightCookieManager.Sorting.QuickSort<T>(data, start, num2, compare);
					}
					if (num2 + 1 < end)
					{
						LightCookieManager.Sorting.QuickSort<T>(data, num2 + 1, end, compare);
					}
				}
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x00040610 File Offset: 0x0003E810
			private static T Median3Pivot<T>(T[] data, int start, int pivot, int end, Func<T, T, int> compare)
			{
				LightCookieManager.Sorting.<>c__DisplayClass2_0<T> CS$<>8__locals1;
				CS$<>8__locals1.data = data;
				if (compare(CS$<>8__locals1.data[end], CS$<>8__locals1.data[start]) < 0)
				{
					LightCookieManager.Sorting.<Median3Pivot>g__Swap|2_0<T>(start, end, ref CS$<>8__locals1);
				}
				if (compare(CS$<>8__locals1.data[pivot], CS$<>8__locals1.data[start]) < 0)
				{
					LightCookieManager.Sorting.<Median3Pivot>g__Swap|2_0<T>(start, pivot, ref CS$<>8__locals1);
				}
				if (compare(CS$<>8__locals1.data[end], CS$<>8__locals1.data[pivot]) < 0)
				{
					LightCookieManager.Sorting.<Median3Pivot>g__Swap|2_0<T>(pivot, end, ref CS$<>8__locals1);
				}
				return CS$<>8__locals1.data[pivot];
			}

			// Token: 0x060009AA RID: 2474 RVA: 0x000406B4 File Offset: 0x0003E8B4
			private static int Partition<T>(T[] data, int start, int end, Func<T, T, int> compare)
			{
				int num = end - start;
				int num2 = start + num / 2;
				T t = LightCookieManager.Sorting.Median3Pivot<T>(data, start, num2, end, compare);
				for (;;)
				{
					if (compare(data[start], t) >= 0)
					{
						while (compare(data[end], t) > 0)
						{
							end--;
						}
						if (start >= end)
						{
							break;
						}
						T t2 = data[start];
						data[start++] = data[end];
						data[end--] = t2;
					}
					else
					{
						start++;
					}
				}
				return end;
			}

			// Token: 0x060009AB RID: 2475 RVA: 0x00040738 File Offset: 0x0003E938
			public static void InsertionSort<T>(T[] data, int start, int end, Func<T, T, int> compare)
			{
				for (int i = start + 1; i < end + 1; i++)
				{
					T t = data[i];
					int num = i - 1;
					while (num >= 0 && compare(t, data[num]) < 0)
					{
						data[num + 1] = data[num];
						num--;
					}
					data[num + 1] = t;
				}
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x00040798 File Offset: 0x0003E998
			[CompilerGenerated]
			internal static void <Median3Pivot>g__Swap|2_0<T>(int a, int b, ref LightCookieManager.Sorting.<>c__DisplayClass2_0<T> A_2)
			{
				T t = A_2.data[a];
				A_2.data[a] = A_2.data[b];
				A_2.data[b] = t;
			}
		}

		// Token: 0x02000176 RID: 374
		private struct LightCookieMapping
		{
			// Token: 0x0400098F RID: 2447
			public ushort visibleLightIndex;

			// Token: 0x04000990 RID: 2448
			public ushort lightBufferIndex;

			// Token: 0x04000991 RID: 2449
			public Light light;

			// Token: 0x04000992 RID: 2450
			public static Func<LightCookieManager.LightCookieMapping, LightCookieManager.LightCookieMapping, int> s_CompareByCookieSize = delegate(LightCookieManager.LightCookieMapping a, LightCookieManager.LightCookieMapping b)
			{
				Texture cookie = a.light.cookie;
				Texture cookie2 = b.light.cookie;
				int num = cookie.width * cookie.height;
				int num2 = cookie2.width * cookie2.height - num;
				if (num2 == 0)
				{
					int instanceID = cookie.GetInstanceID();
					int instanceID2 = cookie2.GetInstanceID();
					return instanceID - instanceID2;
				}
				return num2;
			};

			// Token: 0x04000993 RID: 2451
			public static Func<LightCookieManager.LightCookieMapping, LightCookieManager.LightCookieMapping, int> s_CompareByBufferIndex = (LightCookieManager.LightCookieMapping a, LightCookieManager.LightCookieMapping b) => (int)(a.lightBufferIndex - b.lightBufferIndex);
		}

		// Token: 0x02000177 RID: 375
		private readonly struct WorkSlice<T>
		{
			// Token: 0x060009AE RID: 2478 RVA: 0x00040803 File Offset: 0x0003EA03
			public WorkSlice(T[] src, int srcLen = -1)
			{
				this = new LightCookieManager.WorkSlice<T>(src, 0, srcLen);
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x0004080E File Offset: 0x0003EA0E
			public WorkSlice(T[] src, int srcStart, int srcLen = -1)
			{
				this.m_Data = src;
				this.m_Start = srcStart;
				this.m_Length = ((srcLen < 0) ? src.Length : Math.Min(srcLen, src.Length));
			}

			// Token: 0x17000225 RID: 549
			public T this[int index]
			{
				get
				{
					return this.m_Data[this.m_Start + index];
				}
				set
				{
					this.m_Data[this.m_Start + index] = value;
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00040861 File Offset: 0x0003EA61
			public int length
			{
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00040869 File Offset: 0x0003EA69
			public int capacity
			{
				get
				{
					return this.m_Data.Length;
				}
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x00040873 File Offset: 0x0003EA73
			public void Sort(Func<T, T, int> compare)
			{
				if (this.m_Length > 1)
				{
					LightCookieManager.Sorting.QuickSort<T>(this.m_Data, this.m_Start, this.m_Start + this.m_Length - 1, compare);
				}
			}

			// Token: 0x04000994 RID: 2452
			private readonly T[] m_Data;

			// Token: 0x04000995 RID: 2453
			private readonly int m_Start;

			// Token: 0x04000996 RID: 2454
			private readonly int m_Length;
		}

		// Token: 0x02000178 RID: 376
		private class WorkMemory
		{
			// Token: 0x060009B5 RID: 2485 RVA: 0x000408A0 File Offset: 0x0003EAA0
			public void Resize(int size)
			{
				int num = size;
				LightCookieManager.LightCookieMapping[] array = this.lightMappings;
				int? num2 = ((array != null) ? new int?(array.Length) : null);
				if ((num <= num2.GetValueOrDefault()) & (num2 != null))
				{
					return;
				}
				size = Math.Max(size, (size + 15) / 16 * 16);
				this.lightMappings = new LightCookieManager.LightCookieMapping[size];
				this.uvRects = new Vector4[size];
			}

			// Token: 0x04000997 RID: 2455
			public LightCookieManager.LightCookieMapping[] lightMappings;

			// Token: 0x04000998 RID: 2456
			public Vector4[] uvRects;
		}

		// Token: 0x02000179 RID: 377
		private struct ShaderBitArray
		{
			// Token: 0x17000228 RID: 552
			// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00040914 File Offset: 0x0003EB14
			public int elemLength
			{
				get
				{
					if (this.m_Data != null)
					{
						return this.m_Data.Length;
					}
					return 0;
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00040928 File Offset: 0x0003EB28
			public int bitCapacity
			{
				get
				{
					return this.elemLength * 32;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00040933 File Offset: 0x0003EB33
			public float[] data
			{
				get
				{
					return this.m_Data;
				}
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x0004093C File Offset: 0x0003EB3C
			public void Resize(int bitCount)
			{
				if (this.bitCapacity > bitCount)
				{
					return;
				}
				int num = (bitCount + 31) / 32;
				int num2 = num;
				float[] data = this.m_Data;
				int? num3 = ((data != null) ? new int?(data.Length) : null);
				if ((num2 == num3.GetValueOrDefault()) & (num3 != null))
				{
					return;
				}
				float[] array = new float[num];
				if (this.m_Data != null)
				{
					for (int i = 0; i < this.m_Data.Length; i++)
					{
						array[i] = this.m_Data[i];
					}
				}
				this.m_Data = array;
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x000409C8 File Offset: 0x0003EBC8
			public void Clear()
			{
				for (int i = 0; i < this.m_Data.Length; i++)
				{
					this.m_Data[i] = 0f;
				}
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x000409F5 File Offset: 0x0003EBF5
			private void GetElementIndexAndBitOffset(int index, out int elemIndex, out int bitOffset)
			{
				elemIndex = index >> 5;
				bitOffset = index & 31;
			}

			// Token: 0x1700022B RID: 555
			public unsafe bool this[int index]
			{
				get
				{
					int num;
					int num2;
					this.GetElementIndexAndBitOffset(index, out num, out num2);
					float[] data;
					float* ptr;
					if ((data = this.m_Data) == null || data.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &data[0];
					}
					uint* ptr2 = (uint*)(ptr + num);
					return (*ptr2 & (1U << num2)) > 0U;
				}
				set
				{
					int num;
					int num2;
					this.GetElementIndexAndBitOffset(index, out num, out num2);
					float[] array;
					float* ptr;
					if ((array = this.m_Data) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					uint* ptr2 = (uint*)(ptr + num);
					if (value)
					{
						*ptr2 |= 1U << num2;
					}
					else
					{
						*ptr2 &= ~(1U << num2);
					}
					array = null;
				}
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x00040AB0 File Offset: 0x0003ECB0
			public unsafe override string ToString()
			{
				int num = Math.Min(this.bitCapacity, 4096);
				byte* ptr = stackalloc byte[(UIntPtr)num];
				for (int i = 0; i < num; i++)
				{
					ptr[i] = (this[i] ? 49 : 48);
				}
				return new string((sbyte*)ptr, 0, num, Encoding.UTF8);
			}

			// Token: 0x04000999 RID: 2457
			private const int k_BitsPerElement = 32;

			// Token: 0x0400099A RID: 2458
			private const int k_ElementShift = 5;

			// Token: 0x0400099B RID: 2459
			private const int k_ElementMask = 31;

			// Token: 0x0400099C RID: 2460
			private float[] m_Data;
		}

		// Token: 0x0200017A RID: 378
		private class LightCookieShaderData : IDisposable
		{
			// Token: 0x1700022C RID: 556
			// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00040B00 File Offset: 0x0003ED00
			public Matrix4x4[] worldToLights
			{
				get
				{
					return this.m_WorldToLightCpuData;
				}
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00040B08 File Offset: 0x0003ED08
			public LightCookieManager.ShaderBitArray cookieEnableBits
			{
				get
				{
					return this.m_CookieEnableBitsCpuData;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00040B10 File Offset: 0x0003ED10
			public Vector4[] atlasUVRects
			{
				get
				{
					return this.m_AtlasUVRectCpuData;
				}
			}

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00040B18 File Offset: 0x0003ED18
			public float[] lightTypes
			{
				get
				{
					return this.m_LightTypeCpuData;
				}
			}

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00040B20 File Offset: 0x0003ED20
			// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00040B28 File Offset: 0x0003ED28
			public bool isUploaded { get; set; }

			// Token: 0x060009C6 RID: 2502 RVA: 0x00040B31 File Offset: 0x0003ED31
			public LightCookieShaderData(int size, bool useStructuredBuffer)
			{
				this.m_UseStructuredBuffer = useStructuredBuffer;
				this.Resize(size);
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x00040B47 File Offset: 0x0003ED47
			public void Dispose()
			{
				if (this.m_UseStructuredBuffer)
				{
					ComputeBuffer worldToLightBuffer = this.m_WorldToLightBuffer;
					if (worldToLightBuffer != null)
					{
						worldToLightBuffer.Dispose();
					}
					ComputeBuffer atlasUVRectBuffer = this.m_AtlasUVRectBuffer;
					if (atlasUVRectBuffer != null)
					{
						atlasUVRectBuffer.Dispose();
					}
					ComputeBuffer lightTypeBuffer = this.m_LightTypeBuffer;
					if (lightTypeBuffer == null)
					{
						return;
					}
					lightTypeBuffer.Dispose();
				}
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x00040B84 File Offset: 0x0003ED84
			public void Resize(int size)
			{
				if (size <= this.m_Size)
				{
					return;
				}
				if (this.m_Size > 0)
				{
					this.Dispose();
				}
				this.m_WorldToLightCpuData = new Matrix4x4[size];
				this.m_AtlasUVRectCpuData = new Vector4[size];
				this.m_LightTypeCpuData = new float[size];
				this.m_CookieEnableBitsCpuData.Resize(size);
				if (this.m_UseStructuredBuffer)
				{
					this.m_WorldToLightBuffer = new ComputeBuffer(size, Marshal.SizeOf<Matrix4x4>());
					this.m_AtlasUVRectBuffer = new ComputeBuffer(size, Marshal.SizeOf<Vector4>());
					this.m_LightTypeBuffer = new ComputeBuffer(size, Marshal.SizeOf<float>());
				}
				this.m_Size = size;
			}

			// Token: 0x060009C9 RID: 2505 RVA: 0x00040C1C File Offset: 0x0003EE1C
			public void Upload(CommandBuffer cmd)
			{
				if (this.m_UseStructuredBuffer)
				{
					this.m_WorldToLightBuffer.SetData(this.m_WorldToLightCpuData);
					this.m_AtlasUVRectBuffer.SetData(this.m_AtlasUVRectCpuData);
					this.m_LightTypeBuffer.SetData(this.m_LightTypeCpuData);
					cmd.SetGlobalBuffer(LightCookieManager.ShaderProperty.additionalLightsWorldToLightBuffer, this.m_WorldToLightBuffer);
					cmd.SetGlobalBuffer(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasUVRectBuffer, this.m_AtlasUVRectBuffer);
					cmd.SetGlobalBuffer(LightCookieManager.ShaderProperty.additionalLightsLightTypeBuffer, this.m_LightTypeBuffer);
				}
				else
				{
					cmd.SetGlobalMatrixArray(LightCookieManager.ShaderProperty.additionalLightsWorldToLights, this.m_WorldToLightCpuData);
					cmd.SetGlobalVectorArray(LightCookieManager.ShaderProperty.additionalLightsCookieAtlasUVRects, this.m_AtlasUVRectCpuData);
					cmd.SetGlobalFloatArray(LightCookieManager.ShaderProperty.additionalLightsLightTypes, this.m_LightTypeCpuData);
				}
				cmd.SetGlobalFloatArray(LightCookieManager.ShaderProperty.additionalLightsCookieEnableBits, this.m_CookieEnableBitsCpuData.data);
				this.isUploaded = true;
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x00040CE9 File Offset: 0x0003EEE9
			public void Clear(CommandBuffer cmd)
			{
				if (this.isUploaded)
				{
					this.m_CookieEnableBitsCpuData.Clear();
					cmd.SetGlobalFloatArray(LightCookieManager.ShaderProperty.additionalLightsCookieEnableBits, this.m_CookieEnableBitsCpuData.data);
					this.isUploaded = false;
				}
			}

			// Token: 0x0400099D RID: 2461
			private int m_Size;

			// Token: 0x0400099E RID: 2462
			private bool m_UseStructuredBuffer;

			// Token: 0x0400099F RID: 2463
			private Matrix4x4[] m_WorldToLightCpuData;

			// Token: 0x040009A0 RID: 2464
			private Vector4[] m_AtlasUVRectCpuData;

			// Token: 0x040009A1 RID: 2465
			private float[] m_LightTypeCpuData;

			// Token: 0x040009A2 RID: 2466
			private LightCookieManager.ShaderBitArray m_CookieEnableBitsCpuData;

			// Token: 0x040009A3 RID: 2467
			private ComputeBuffer m_WorldToLightBuffer;

			// Token: 0x040009A4 RID: 2468
			private ComputeBuffer m_AtlasUVRectBuffer;

			// Token: 0x040009A5 RID: 2469
			private ComputeBuffer m_LightTypeBuffer;
		}
	}
}
