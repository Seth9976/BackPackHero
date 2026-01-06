using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A3 RID: 163
	public static class Blitter
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x00018DD0 File Offset: 0x00016FD0
		public static void Initialize(Shader blitPS, Shader blitColorAndDepthPS)
		{
			Blitter.s_Blit = CoreUtils.CreateEngineMaterial(blitPS);
			Blitter.s_BlitColorAndDepth = CoreUtils.CreateEngineMaterial(blitColorAndDepthPS);
			if (TextureXR.useTexArray)
			{
				Blitter.s_Blit.EnableKeyword("DISABLE_TEXTURE2D_X_ARRAY");
				Blitter.s_BlitTexArray = CoreUtils.CreateEngineMaterial(blitPS);
				Blitter.s_BlitTexArraySingleSlice = CoreUtils.CreateEngineMaterial(blitPS);
				Blitter.s_BlitTexArraySingleSlice.EnableKeyword("BLIT_SINGLE_SLICE");
			}
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				float num = -1f;
				if (SystemInfo.usesReversedZBuffer)
				{
					num = 1f;
				}
				if (!Blitter.s_TriangleMesh)
				{
					Blitter.s_TriangleMesh = new Mesh();
					Blitter.s_TriangleMesh.vertices = Blitter.<Initialize>g__GetFullScreenTriangleVertexPosition|8_0(num);
					Blitter.s_TriangleMesh.uv = Blitter.<Initialize>g__GetFullScreenTriangleTexCoord|8_1();
					Blitter.s_TriangleMesh.triangles = new int[] { 0, 1, 2 };
				}
				if (!Blitter.s_QuadMesh)
				{
					Blitter.s_QuadMesh = new Mesh();
					Blitter.s_QuadMesh.vertices = Blitter.<Initialize>g__GetQuadVertexPosition|8_2(num);
					Blitter.s_QuadMesh.uv = Blitter.<Initialize>g__GetQuadTexCoord|8_3();
					Blitter.s_QuadMesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
				}
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00018EEA File Offset: 0x000170EA
		public static void Cleanup()
		{
			CoreUtils.Destroy(Blitter.s_Blit);
			CoreUtils.Destroy(Blitter.s_BlitTexArray);
			CoreUtils.Destroy(Blitter.s_BlitTexArraySingleSlice);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00018F0A File Offset: 0x0001710A
		public static Material GetBlitMaterial(TextureDimension dimension, bool singleSlice = false)
		{
			if (dimension != TextureDimension.Tex2DArray)
			{
				return Blitter.s_Blit;
			}
			if (!singleSlice)
			{
				return Blitter.s_BlitTexArray;
			}
			return Blitter.s_BlitTexArraySingleSlice;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018F26 File Offset: 0x00017126
		private static void DrawTriangle(CommandBuffer cmd, Material material, int shaderPass)
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(Blitter.s_TriangleMesh, Matrix4x4.identity, material, 0, shaderPass, Blitter.s_PropertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, material, shaderPass, MeshTopology.Triangles, 3, 1, Blitter.s_PropertyBlock);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018F5F File Offset: 0x0001715F
		internal static void DrawQuad(CommandBuffer cmd, Material material, int shaderPass)
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(Blitter.s_QuadMesh, Matrix4x4.identity, material, 0, shaderPass, Blitter.s_PropertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, material, shaderPass, MeshTopology.Quads, 4, 1, Blitter.s_PropertyBlock);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018F98 File Offset: 0x00017198
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureXR.dimension, false), bilinear ? 1 : 0);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018FC5 File Offset: 0x000171C5
		public static void BlitTexture2D(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureDimension.Tex2D, false), bilinear ? 1 : 0);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018FF0 File Offset: 0x000171F0
		public static void BlitColorAndDepth(CommandBuffer cmd, Texture sourceColor, RenderTexture sourceDepth, Vector4 scaleBias, float mipLevel, bool blitDepth)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, sourceColor);
			if (blitDepth)
			{
				Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._InputDepth, sourceDepth, RenderTextureSubElement.Depth);
			}
			Blitter.DrawTriangle(cmd, Blitter.s_BlitColorAndDepth, blitDepth ? 1 : 0);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00019056 File Offset: 0x00017256
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00019088 File Offset: 0x00017288
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 vector = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, vector, mipLevel, bilinear);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000190D8 File Offset: 0x000172D8
		public static void BlitCameraTexture2D(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 vector = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture2D(cmd, source, vector, mipLevel, bilinear);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00019128 File Offset: 0x00017328
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Material material, int pass)
		{
			Vector2 vector = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, vector, material, pass);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00019177 File Offset: 0x00017377
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Vector4 scaleBias, float mipLevel = 0f, bool bilinear = false)
		{
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, scaleBias, mipLevel, bilinear);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019190 File Offset: 0x00017390
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Rect destViewport, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 vector = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(destViewport);
			Blitter.BlitTexture(cmd, source, vector, mipLevel, bilinear);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000191E8 File Offset: 0x000173E8
		public static void BlitQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 3 : 2);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00019254 File Offset: 0x00017454
		public static void BlitQuadWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 7 : 6);
				return;
			}
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 5 : 4);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00019308 File Offset: 0x00017508
		public static void BlitQuadWithPaddingMultiply(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 12 : 11);
				return;
			}
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 10 : 9);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000193C0 File Offset: 0x000175C0
		public static void BlitOctahedralWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), 8);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001944C File Offset: 0x0001764C
		public static void BlitOctahedralWithPaddingMultiply(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), 13);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000194D8 File Offset: 0x000176D8
		public static void BlitCubeToOctahedral2DQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasRT, int mipLevelTex)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), 14);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00019554 File Offset: 0x00017754
		public static void BlitCubeToOctahedral2DQuadSingleChannel(CommandBuffer cmd, Texture source, Vector4 scaleBiasRT, int mipLevelTex)
		{
			int num = 15;
			if (GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1U)
			{
				if (GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					num = 16;
				}
				if (GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == FormatSwizzle.FormatSwizzleR)
				{
					num = 17;
				}
			}
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), num);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00019600 File Offset: 0x00017800
		public static void BlitQuadSingleChannel(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex)
		{
			int num = 18;
			if (GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1U)
			{
				if (GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					num = 19;
				}
				if (GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == FormatSwizzle.FormatSwizzleR)
				{
					num = 20;
				}
			}
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), num);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000196A0 File Offset: 0x000178A0
		[CompilerGenerated]
		internal static Vector3[] <Initialize>g__GetFullScreenTriangleVertexPosition|8_0(float z)
		{
			Vector3[] array = new Vector3[3];
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = new Vector2((float)((i << 1) & 2), (float)(i & 2));
				array[i] = new Vector3(vector.x * 2f - 1f, vector.y * 2f - 1f, z);
			}
			return array;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00019704 File Offset: 0x00017904
		[CompilerGenerated]
		internal static Vector2[] <Initialize>g__GetFullScreenTriangleTexCoord|8_1()
		{
			Vector2[] array = new Vector2[3];
			for (int i = 0; i < 3; i++)
			{
				if (SystemInfo.graphicsUVStartsAtTop)
				{
					array[i] = new Vector2((float)((i << 1) & 2), 1f - (float)(i & 2));
				}
				else
				{
					array[i] = new Vector2((float)((i << 1) & 2), (float)(i & 2));
				}
			}
			return array;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00019760 File Offset: 0x00017960
		[CompilerGenerated]
		internal static Vector3[] <Initialize>g__GetQuadVertexPosition|8_2(float z)
		{
			Vector3[] array = new Vector3[4];
			for (uint num = 0U; num < 4U; num += 1U)
			{
				uint num2 = num >> 1;
				uint num3 = num & 1U;
				float num4 = num2;
				float num5 = (1U - (num2 + num3)) & 1U;
				array[(int)num] = new Vector3(num4, num5, z);
			}
			return array;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000197AC File Offset: 0x000179AC
		[CompilerGenerated]
		internal static Vector2[] <Initialize>g__GetQuadTexCoord|8_3()
		{
			Vector2[] array = new Vector2[4];
			for (uint num = 0U; num < 4U; num += 1U)
			{
				uint num2 = num >> 1;
				uint num3 = num & 1U;
				float num4 = num2;
				float num5 = (num2 + num3) & 1U;
				if (SystemInfo.graphicsUVStartsAtTop)
				{
					num5 = 1f - num5;
				}
				array[(int)num] = new Vector2(num4, num5);
			}
			return array;
		}

		// Token: 0x04000349 RID: 841
		private static Material s_Blit;

		// Token: 0x0400034A RID: 842
		private static Material s_BlitTexArray;

		// Token: 0x0400034B RID: 843
		private static Material s_BlitTexArraySingleSlice;

		// Token: 0x0400034C RID: 844
		private static Material s_BlitColorAndDepth;

		// Token: 0x0400034D RID: 845
		private static MaterialPropertyBlock s_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x0400034E RID: 846
		private static Mesh s_TriangleMesh;

		// Token: 0x0400034F RID: 847
		private static Mesh s_QuadMesh;

		// Token: 0x02000171 RID: 369
		private static class BlitShaderIDs
		{
			// Token: 0x04000587 RID: 1415
			public static readonly int _BlitTexture = Shader.PropertyToID("_BlitTexture");

			// Token: 0x04000588 RID: 1416
			public static readonly int _BlitCubeTexture = Shader.PropertyToID("_BlitCubeTexture");

			// Token: 0x04000589 RID: 1417
			public static readonly int _BlitScaleBias = Shader.PropertyToID("_BlitScaleBias");

			// Token: 0x0400058A RID: 1418
			public static readonly int _BlitScaleBiasRt = Shader.PropertyToID("_BlitScaleBiasRt");

			// Token: 0x0400058B RID: 1419
			public static readonly int _BlitMipLevel = Shader.PropertyToID("_BlitMipLevel");

			// Token: 0x0400058C RID: 1420
			public static readonly int _BlitTextureSize = Shader.PropertyToID("_BlitTextureSize");

			// Token: 0x0400058D RID: 1421
			public static readonly int _BlitPaddingSize = Shader.PropertyToID("_BlitPaddingSize");

			// Token: 0x0400058E RID: 1422
			public static readonly int _InputDepth = Shader.PropertyToID("_InputDepthTexture");
		}
	}
}
