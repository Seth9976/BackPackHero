using System;
using System.Linq;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000040 RID: 64
	public static class TextShaderUtilities
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0001B4F0 File Offset: 0x000196F0
		internal static Shader ShaderRef_MobileSDF
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileSDF == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("Text/Mobile/Distance Field SSD");
					bool flag2 = TextShaderUtilities.k_ShaderRef_MobileSDF == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("Hidden/TextCore/Distance Field SSD");
					}
				}
				return TextShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0001B548 File Offset: 0x00019748
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileBitmap == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("Text/Bitmap");
					bool flag2 = TextShaderUtilities.k_ShaderRef_MobileBitmap == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("Hidden/Internal-GUITextureClipText");
					}
				}
				return TextShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0001B5A0 File Offset: 0x000197A0
		internal static Shader ShaderRef_Sprite
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_Sprite == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find("Text/Sprite");
					bool flag2 = TextShaderUtilities.k_ShaderRef_Sprite == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find("Hidden/TextCore/Sprite");
					}
				}
				return TextShaderUtilities.k_ShaderRef_Sprite;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0001B5F8 File Offset: 0x000197F8
		static TextShaderUtilities()
		{
			TextShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0001B680 File Offset: 0x00019880
		internal static void GetShaderPropertyIDs()
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.isInitialized = true;
				TextShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				TextShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				TextShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				TextShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				TextShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				TextShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				TextShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				TextShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				TextShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				TextShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				TextShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				TextShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				TextShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				TextShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				TextShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				TextShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				TextShaderUtilities.ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
				TextShaderUtilities.ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
				TextShaderUtilities.ID_Padding = Shader.PropertyToID("_Padding");
				TextShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				TextShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				TextShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				TextShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				TextShaderUtilities.ID_Sharpness = Shader.PropertyToID("_Sharpness");
				TextShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				TextShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				TextShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				TextShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				TextShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				TextShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				TextShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				TextShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				TextShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				TextShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				TextShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				TextShaderUtilities.ID_GlowInner = Shader.PropertyToID("_GlowInner");
				TextShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				TextShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				TextShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				TextShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				TextShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				TextShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				TextShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				TextShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				TextShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				TextShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				TextShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				TextShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				TextShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				TextShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				TextShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				TextShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0001B9B4 File Offset: 0x00019BB4
		private static void UpdateShaderRatios(Material mat)
		{
			bool flag = !Enumerable.Contains<string>(mat.shaderKeywords, TextShaderUtilities.Keyword_Ratios);
			bool flag2 = !mat.HasProperty(TextShaderUtilities.ID_GradientScale) || !mat.HasProperty(TextShaderUtilities.ID_FaceDilate);
			if (!flag2)
			{
				float @float = mat.GetFloat(TextShaderUtilities.ID_GradientScale);
				float float2 = mat.GetFloat(TextShaderUtilities.ID_FaceDilate);
				float float3 = mat.GetFloat(TextShaderUtilities.ID_OutlineWidth);
				float float4 = mat.GetFloat(TextShaderUtilities.ID_OutlineSoftness);
				float num = Mathf.Max(mat.GetFloat(TextShaderUtilities.ID_WeightNormal), mat.GetFloat(TextShaderUtilities.ID_WeightBold)) / 4f;
				float num2 = Mathf.Max(1f, num + float2 + float3 + float4);
				float num3 = (flag ? ((@float - TextShaderUtilities.m_clamp) / (@float * num2)) : 1f);
				mat.SetFloat(TextShaderUtilities.ID_ScaleRatio_A, num3);
				bool flag3 = mat.HasProperty(TextShaderUtilities.ID_GlowOffset);
				if (flag3)
				{
					float float5 = mat.GetFloat(TextShaderUtilities.ID_GlowOffset);
					float float6 = mat.GetFloat(TextShaderUtilities.ID_GlowOuter);
					float num4 = (num + float2) * (@float - TextShaderUtilities.m_clamp);
					num2 = Mathf.Max(1f, float5 + float6);
					float num5 = (flag ? (Mathf.Max(0f, @float - TextShaderUtilities.m_clamp - num4) / (@float * num2)) : 1f);
					mat.SetFloat(TextShaderUtilities.ID_ScaleRatio_B, num5);
				}
				bool flag4 = mat.HasProperty(TextShaderUtilities.ID_UnderlayOffsetX);
				if (flag4)
				{
					float float7 = mat.GetFloat(TextShaderUtilities.ID_UnderlayOffsetX);
					float float8 = mat.GetFloat(TextShaderUtilities.ID_UnderlayOffsetY);
					float float9 = mat.GetFloat(TextShaderUtilities.ID_UnderlayDilate);
					float float10 = mat.GetFloat(TextShaderUtilities.ID_UnderlaySoftness);
					float num6 = (num + float2) * (@float - TextShaderUtilities.m_clamp);
					num2 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(float7), Mathf.Abs(float8)) + float9 + float10);
					float num7 = (flag ? (Mathf.Max(0f, @float - TextShaderUtilities.m_clamp - num6) / (@float * num2)) : 1f);
					mat.SetFloat(TextShaderUtilities.ID_ScaleRatio_C, num7);
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		internal static Vector4 GetFontExtent(Material material)
		{
			return Vector4.zero;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		internal static bool IsMaskingEnabled(Material material)
		{
			bool flag = material == null || !material.HasProperty(TextShaderUtilities.ID_ClipRect);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = Enumerable.Contains<string>(material.shaderKeywords, TextShaderUtilities.Keyword_MASK_SOFT) || Enumerable.Contains<string>(material.shaderKeywords, TextShaderUtilities.Keyword_MASK_HARD) || Enumerable.Contains<string>(material.shaderKeywords, TextShaderUtilities.Keyword_MASK_TEX);
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0001BC64 File Offset: 0x00019E64
		internal static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.GetShaderPropertyIDs();
			}
			bool flag2 = material == null;
			float num;
			if (flag2)
			{
				num = 0f;
			}
			else
			{
				int num2 = (enableExtraPadding ? 4 : 0);
				bool flag3 = !material.HasProperty(TextShaderUtilities.ID_GradientScale);
				if (flag3)
				{
					bool flag4 = material.HasProperty(TextShaderUtilities.ID_Padding);
					if (flag4)
					{
						num2 += (int)material.GetFloat(TextShaderUtilities.ID_Padding);
					}
					num = (float)num2 + 1f;
				}
				else
				{
					Vector4 vector = Vector4.zero;
					Vector4 zero = Vector4.zero;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					float num6 = 0f;
					float num7 = 0f;
					float num8 = 0f;
					float num9 = 0f;
					float num10 = 0f;
					TextShaderUtilities.UpdateShaderRatios(material);
					string[] shaderKeywords = material.shaderKeywords;
					bool flag5 = material.HasProperty(TextShaderUtilities.ID_ScaleRatio_A);
					if (flag5)
					{
						num6 = material.GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
					}
					bool flag6 = material.HasProperty(TextShaderUtilities.ID_FaceDilate);
					if (flag6)
					{
						num3 = material.GetFloat(TextShaderUtilities.ID_FaceDilate) * num6;
					}
					bool flag7 = material.HasProperty(TextShaderUtilities.ID_OutlineSoftness);
					if (flag7)
					{
						num4 = material.GetFloat(TextShaderUtilities.ID_OutlineSoftness) * num6;
					}
					bool flag8 = material.HasProperty(TextShaderUtilities.ID_OutlineWidth);
					if (flag8)
					{
						num5 = material.GetFloat(TextShaderUtilities.ID_OutlineWidth) * num6;
					}
					float num11 = num5 + num4 + num3;
					bool flag9 = material.HasProperty(TextShaderUtilities.ID_GlowOffset) && Enumerable.Contains<string>(shaderKeywords, TextShaderUtilities.Keyword_Glow);
					if (flag9)
					{
						bool flag10 = material.HasProperty(TextShaderUtilities.ID_ScaleRatio_B);
						if (flag10)
						{
							num7 = material.GetFloat(TextShaderUtilities.ID_ScaleRatio_B);
						}
						num9 = material.GetFloat(TextShaderUtilities.ID_GlowOffset) * num7;
						num10 = material.GetFloat(TextShaderUtilities.ID_GlowOuter) * num7;
					}
					num11 = Mathf.Max(num11, num3 + num9 + num10);
					bool flag11 = material.HasProperty(TextShaderUtilities.ID_UnderlaySoftness) && Enumerable.Contains<string>(shaderKeywords, TextShaderUtilities.Keyword_Underlay);
					if (flag11)
					{
						bool flag12 = material.HasProperty(TextShaderUtilities.ID_ScaleRatio_C);
						if (flag12)
						{
							num8 = material.GetFloat(TextShaderUtilities.ID_ScaleRatio_C);
						}
						float num12 = material.GetFloat(TextShaderUtilities.ID_UnderlayOffsetX) * num8;
						float num13 = material.GetFloat(TextShaderUtilities.ID_UnderlayOffsetY) * num8;
						float num14 = material.GetFloat(TextShaderUtilities.ID_UnderlayDilate) * num8;
						float num15 = material.GetFloat(TextShaderUtilities.ID_UnderlaySoftness) * num8;
						vector.x = Mathf.Max(vector.x, num3 + num14 + num15 - num12);
						vector.y = Mathf.Max(vector.y, num3 + num14 + num15 - num13);
						vector.z = Mathf.Max(vector.z, num3 + num14 + num15 + num12);
						vector.w = Mathf.Max(vector.w, num3 + num14 + num15 + num13);
					}
					vector.x = Mathf.Max(vector.x, num11);
					vector.y = Mathf.Max(vector.y, num11);
					vector.z = Mathf.Max(vector.z, num11);
					vector.w = Mathf.Max(vector.w, num11);
					vector.x += (float)num2;
					vector.y += (float)num2;
					vector.z += (float)num2;
					vector.w += (float)num2;
					vector.x = Mathf.Min(vector.x, 1f);
					vector.y = Mathf.Min(vector.y, 1f);
					vector.z = Mathf.Min(vector.z, 1f);
					vector.w = Mathf.Min(vector.w, 1f);
					zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
					zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
					zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
					zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
					float @float = material.GetFloat(TextShaderUtilities.ID_GradientScale);
					vector *= @float;
					num11 = Mathf.Max(vector.x, vector.y);
					num11 = Mathf.Max(vector.z, num11);
					num11 = Mathf.Max(vector.w, num11);
					num = num11 + 4f;
				}
			}
			return num;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0001C0F8 File Offset: 0x0001A2F8
		internal static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.GetShaderPropertyIDs();
			}
			bool flag2 = materials == null;
			float num;
			if (flag2)
			{
				num = 0f;
			}
			else
			{
				int num2 = (enableExtraPadding ? 4 : 0);
				bool flag3 = materials[0].HasProperty(TextShaderUtilities.ID_Padding);
				if (flag3)
				{
					num = (float)num2 + materials[0].GetFloat(TextShaderUtilities.ID_Padding);
				}
				else
				{
					Vector4 vector = Vector4.zero;
					Vector4 zero = Vector4.zero;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					float num6 = 0f;
					float num7 = 0f;
					float num8 = 0f;
					float num9 = 0f;
					float num10 = 0f;
					float num11;
					for (int i = 0; i < materials.Length; i++)
					{
						TextShaderUtilities.UpdateShaderRatios(materials[i]);
						string[] shaderKeywords = materials[i].shaderKeywords;
						bool flag4 = materials[i].HasProperty(TextShaderUtilities.ID_ScaleRatio_A);
						if (flag4)
						{
							num6 = materials[i].GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
						}
						bool flag5 = materials[i].HasProperty(TextShaderUtilities.ID_FaceDilate);
						if (flag5)
						{
							num3 = materials[i].GetFloat(TextShaderUtilities.ID_FaceDilate) * num6;
						}
						bool flag6 = materials[i].HasProperty(TextShaderUtilities.ID_OutlineSoftness);
						if (flag6)
						{
							num4 = materials[i].GetFloat(TextShaderUtilities.ID_OutlineSoftness) * num6;
						}
						bool flag7 = materials[i].HasProperty(TextShaderUtilities.ID_OutlineWidth);
						if (flag7)
						{
							num5 = materials[i].GetFloat(TextShaderUtilities.ID_OutlineWidth) * num6;
						}
						num11 = num5 + num4 + num3;
						bool flag8 = materials[i].HasProperty(TextShaderUtilities.ID_GlowOffset) && Enumerable.Contains<string>(shaderKeywords, TextShaderUtilities.Keyword_Glow);
						if (flag8)
						{
							bool flag9 = materials[i].HasProperty(TextShaderUtilities.ID_ScaleRatio_B);
							if (flag9)
							{
								num7 = materials[i].GetFloat(TextShaderUtilities.ID_ScaleRatio_B);
							}
							num9 = materials[i].GetFloat(TextShaderUtilities.ID_GlowOffset) * num7;
							num10 = materials[i].GetFloat(TextShaderUtilities.ID_GlowOuter) * num7;
						}
						num11 = Mathf.Max(num11, num3 + num9 + num10);
						bool flag10 = materials[i].HasProperty(TextShaderUtilities.ID_UnderlaySoftness) && Enumerable.Contains<string>(shaderKeywords, TextShaderUtilities.Keyword_Underlay);
						if (flag10)
						{
							bool flag11 = materials[i].HasProperty(TextShaderUtilities.ID_ScaleRatio_C);
							if (flag11)
							{
								num8 = materials[i].GetFloat(TextShaderUtilities.ID_ScaleRatio_C);
							}
							float num12 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlayOffsetX) * num8;
							float num13 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlayOffsetY) * num8;
							float num14 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlayDilate) * num8;
							float num15 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlaySoftness) * num8;
							vector.x = Mathf.Max(vector.x, num3 + num14 + num15 - num12);
							vector.y = Mathf.Max(vector.y, num3 + num14 + num15 - num13);
							vector.z = Mathf.Max(vector.z, num3 + num14 + num15 + num12);
							vector.w = Mathf.Max(vector.w, num3 + num14 + num15 + num13);
						}
						vector.x = Mathf.Max(vector.x, num11);
						vector.y = Mathf.Max(vector.y, num11);
						vector.z = Mathf.Max(vector.z, num11);
						vector.w = Mathf.Max(vector.w, num11);
						vector.x += (float)num2;
						vector.y += (float)num2;
						vector.z += (float)num2;
						vector.w += (float)num2;
						vector.x = Mathf.Min(vector.x, 1f);
						vector.y = Mathf.Min(vector.y, 1f);
						vector.z = Mathf.Min(vector.z, 1f);
						vector.w = Mathf.Min(vector.w, 1f);
						zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
						zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
						zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
						zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
					}
					float @float = materials[0].GetFloat(TextShaderUtilities.ID_GradientScale);
					vector *= @float;
					num11 = Mathf.Max(vector.x, vector.y);
					num11 = Mathf.Max(vector.z, num11);
					num11 = Mathf.Max(vector.w, num11);
					num = num11 + 0.25f;
				}
			}
			return num;
		}

		// Token: 0x0400033D RID: 829
		public static int ID_MainTex;

		// Token: 0x0400033E RID: 830
		public static int ID_FaceTex;

		// Token: 0x0400033F RID: 831
		public static int ID_FaceColor;

		// Token: 0x04000340 RID: 832
		public static int ID_FaceDilate;

		// Token: 0x04000341 RID: 833
		public static int ID_Shininess;

		// Token: 0x04000342 RID: 834
		public static int ID_UnderlayColor;

		// Token: 0x04000343 RID: 835
		public static int ID_UnderlayOffsetX;

		// Token: 0x04000344 RID: 836
		public static int ID_UnderlayOffsetY;

		// Token: 0x04000345 RID: 837
		public static int ID_UnderlayDilate;

		// Token: 0x04000346 RID: 838
		public static int ID_UnderlaySoftness;

		// Token: 0x04000347 RID: 839
		public static int ID_WeightNormal;

		// Token: 0x04000348 RID: 840
		public static int ID_WeightBold;

		// Token: 0x04000349 RID: 841
		public static int ID_OutlineTex;

		// Token: 0x0400034A RID: 842
		public static int ID_OutlineWidth;

		// Token: 0x0400034B RID: 843
		public static int ID_OutlineSoftness;

		// Token: 0x0400034C RID: 844
		public static int ID_OutlineColor;

		// Token: 0x0400034D RID: 845
		public static int ID_Outline2Color;

		// Token: 0x0400034E RID: 846
		public static int ID_Outline2Width;

		// Token: 0x0400034F RID: 847
		public static int ID_Padding;

		// Token: 0x04000350 RID: 848
		public static int ID_GradientScale;

		// Token: 0x04000351 RID: 849
		public static int ID_ScaleX;

		// Token: 0x04000352 RID: 850
		public static int ID_ScaleY;

		// Token: 0x04000353 RID: 851
		public static int ID_PerspectiveFilter;

		// Token: 0x04000354 RID: 852
		public static int ID_Sharpness;

		// Token: 0x04000355 RID: 853
		public static int ID_TextureWidth;

		// Token: 0x04000356 RID: 854
		public static int ID_TextureHeight;

		// Token: 0x04000357 RID: 855
		public static int ID_BevelAmount;

		// Token: 0x04000358 RID: 856
		public static int ID_GlowColor;

		// Token: 0x04000359 RID: 857
		public static int ID_GlowOffset;

		// Token: 0x0400035A RID: 858
		public static int ID_GlowPower;

		// Token: 0x0400035B RID: 859
		public static int ID_GlowOuter;

		// Token: 0x0400035C RID: 860
		public static int ID_GlowInner;

		// Token: 0x0400035D RID: 861
		public static int ID_LightAngle;

		// Token: 0x0400035E RID: 862
		public static int ID_EnvMap;

		// Token: 0x0400035F RID: 863
		public static int ID_EnvMatrix;

		// Token: 0x04000360 RID: 864
		public static int ID_EnvMatrixRotation;

		// Token: 0x04000361 RID: 865
		public static int ID_MaskCoord;

		// Token: 0x04000362 RID: 866
		public static int ID_ClipRect;

		// Token: 0x04000363 RID: 867
		public static int ID_MaskSoftnessX;

		// Token: 0x04000364 RID: 868
		public static int ID_MaskSoftnessY;

		// Token: 0x04000365 RID: 869
		public static int ID_VertexOffsetX;

		// Token: 0x04000366 RID: 870
		public static int ID_VertexOffsetY;

		// Token: 0x04000367 RID: 871
		public static int ID_UseClipRect;

		// Token: 0x04000368 RID: 872
		public static int ID_StencilID;

		// Token: 0x04000369 RID: 873
		public static int ID_StencilOp;

		// Token: 0x0400036A RID: 874
		public static int ID_StencilComp;

		// Token: 0x0400036B RID: 875
		public static int ID_StencilReadMask;

		// Token: 0x0400036C RID: 876
		public static int ID_StencilWriteMask;

		// Token: 0x0400036D RID: 877
		public static int ID_ShaderFlags;

		// Token: 0x0400036E RID: 878
		public static int ID_ScaleRatio_A;

		// Token: 0x0400036F RID: 879
		public static int ID_ScaleRatio_B;

		// Token: 0x04000370 RID: 880
		public static int ID_ScaleRatio_C;

		// Token: 0x04000371 RID: 881
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x04000372 RID: 882
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x04000373 RID: 883
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x04000374 RID: 884
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x04000375 RID: 885
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x04000376 RID: 886
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x04000377 RID: 887
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x04000378 RID: 888
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x04000379 RID: 889
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x0400037A RID: 890
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x0400037B RID: 891
		private static float m_clamp = 1f;

		// Token: 0x0400037C RID: 892
		public static bool isInitialized = false;

		// Token: 0x0400037D RID: 893
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x0400037E RID: 894
		private static Shader k_ShaderRef_MobileBitmap;

		// Token: 0x0400037F RID: 895
		private static Shader k_ShaderRef_Sprite;
	}
}
