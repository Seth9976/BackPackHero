using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000099 RID: 153
	public static class TextureXR
	{
		// Token: 0x17000092 RID: 146
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000177BE File Offset: 0x000159BE
		public static int maxViews
		{
			set
			{
				TextureXR.m_MaxViews = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000177C6 File Offset: 0x000159C6
		public static int slices
		{
			get
			{
				return TextureXR.m_MaxViews;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000177D0 File Offset: 0x000159D0
		public static bool useTexArray
		{
			get
			{
				GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
				if (graphicsDeviceType <= GraphicsDeviceType.PlayStation4)
				{
					if (graphicsDeviceType != GraphicsDeviceType.Direct3D11 && graphicsDeviceType != GraphicsDeviceType.PlayStation4)
					{
						return false;
					}
				}
				else if (graphicsDeviceType != GraphicsDeviceType.Direct3D12 && graphicsDeviceType != GraphicsDeviceType.Vulkan && graphicsDeviceType - GraphicsDeviceType.PlayStation5 > 1)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00017807 File Offset: 0x00015A07
		public static TextureDimension dimension
		{
			get
			{
				if (!TextureXR.useTexArray)
				{
					return TextureDimension.Tex2D;
				}
				return TextureDimension.Tex2DArray;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00017813 File Offset: 0x00015A13
		public static RTHandle GetBlackUIntTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackUIntTextureRTH;
			}
			return TextureXR.m_BlackUIntTexture2DArrayRTH;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00017827 File Offset: 0x00015A27
		public static RTHandle GetClearTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_ClearTextureRTH;
			}
			return TextureXR.m_ClearTexture2DArrayRTH;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001783B File Offset: 0x00015A3B
		public static RTHandle GetMagentaTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_MagentaTextureRTH;
			}
			return TextureXR.m_MagentaTexture2DArrayRTH;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001784F File Offset: 0x00015A4F
		public static RTHandle GetBlackTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackTextureRTH;
			}
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00017863 File Offset: 0x00015A63
		public static RTHandle GetBlackTextureArray()
		{
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001786A File Offset: 0x00015A6A
		public static RTHandle GetBlackTexture3D()
		{
			return TextureXR.m_BlackTexture3DRTH;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017871 File Offset: 0x00015A71
		public static RTHandle GetWhiteTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_WhiteTextureRTH;
			}
			return TextureXR.m_WhiteTexture2DArrayRTH;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00017888 File Offset: 0x00015A88
		public static void Initialize(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			if (TextureXR.m_BlackUIntTexture2DArray == null)
			{
				RTHandles.Release(TextureXR.m_BlackUIntTexture2DArrayRTH);
				TextureXR.m_BlackUIntTexture2DArray = TextureXR.CreateBlackUIntTextureArray(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackUIntTextureRTH);
				TextureXR.m_BlackUIntTexture = TextureXR.CreateBlackUintTexture(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTextureRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture);
				RTHandles.Release(TextureXR.m_ClearTextureRTH);
				TextureXR.m_ClearTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Clear Texture"
				};
				TextureXR.m_ClearTexture.SetPixel(0, 0, Color.clear);
				TextureXR.m_ClearTexture.Apply();
				TextureXR.m_ClearTextureRTH = RTHandles.Alloc(TextureXR.m_ClearTexture);
				RTHandles.Release(TextureXR.m_ClearTexture2DArrayRTH);
				TextureXR.m_ClearTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_ClearTexture, "Clear Texture2DArray");
				TextureXR.m_ClearTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_ClearTexture2DArray);
				RTHandles.Release(TextureXR.m_MagentaTextureRTH);
				TextureXR.m_MagentaTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Magenta Texture"
				};
				TextureXR.m_MagentaTexture.SetPixel(0, 0, Color.magenta);
				TextureXR.m_MagentaTexture.Apply();
				TextureXR.m_MagentaTextureRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture);
				RTHandles.Release(TextureXR.m_MagentaTexture2DArrayRTH);
				TextureXR.m_MagentaTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_MagentaTexture, "Magenta Texture2DArray");
				TextureXR.m_MagentaTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTextureRTH);
				TextureXR.m_BlackTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Black Texture"
				};
				TextureXR.m_BlackTexture.SetPixel(0, 0, Color.black);
				TextureXR.m_BlackTexture.Apply();
				TextureXR.m_BlackTextureRTH = RTHandles.Alloc(TextureXR.m_BlackTexture);
				RTHandles.Release(TextureXR.m_BlackTexture2DArrayRTH);
				TextureXR.m_BlackTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_BlackTexture, "Black Texture2DArray");
				TextureXR.m_BlackTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTexture3DRTH);
				TextureXR.m_BlackTexture3D = TextureXR.CreateBlackTexture3D("Black Texture3D");
				TextureXR.m_BlackTexture3DRTH = RTHandles.Alloc(TextureXR.m_BlackTexture3D);
				RTHandles.Release(TextureXR.m_WhiteTextureRTH);
				TextureXR.m_WhiteTextureRTH = RTHandles.Alloc(Texture2D.whiteTexture);
				RTHandles.Release(TextureXR.m_WhiteTexture2DArrayRTH);
				TextureXR.m_WhiteTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(Texture2D.whiteTexture, "White Texture2DArray");
				TextureXR.m_WhiteTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_WhiteTexture2DArray);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00017ACC File Offset: 0x00015CCC
		private static Texture2DArray CreateTexture2DArrayFromTexture2D(Texture2D source, string name)
		{
			Texture2DArray texture2DArray = new Texture2DArray(source.width, source.height, TextureXR.slices, source.format, false)
			{
				name = name
			};
			for (int i = 0; i < TextureXR.slices; i++)
			{
				Graphics.CopyTexture(source, 0, 0, texture2DArray, i, 0);
			}
			return texture2DArray;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00017B1C File Offset: 0x00015D1C
		private static Texture CreateBlackUIntTextureArray(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture renderTexture = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2DArray,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int num = clearR32_UIntShader.FindKernel("ClearUIntTextureArray");
			cmd.SetComputeTextureParam(clearR32_UIntShader, num, "_TargetArray", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, num, 1, 1, TextureXR.slices);
			return renderTexture;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017B9C File Offset: 0x00015D9C
		private static Texture CreateBlackUintTexture(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture renderTexture = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2D,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int num = clearR32_UIntShader.FindKernel("ClearUIntTexture");
			cmd.SetComputeTextureParam(clearR32_UIntShader, num, "_Target", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, num, 1, 1, TextureXR.slices);
			return renderTexture;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017C1C File Offset: 0x00015E1C
		private static Texture3D CreateBlackTexture3D(string name)
		{
			Texture3D texture3D = new Texture3D(1, 1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
			texture3D.name = name;
			texture3D.SetPixel(0, 0, 0, Color.black, 0);
			texture3D.Apply(false);
			return texture3D;
		}

		// Token: 0x04000329 RID: 809
		private static int m_MaxViews = 1;

		// Token: 0x0400032A RID: 810
		private static Texture m_BlackUIntTexture2DArray;

		// Token: 0x0400032B RID: 811
		private static Texture m_BlackUIntTexture;

		// Token: 0x0400032C RID: 812
		private static RTHandle m_BlackUIntTexture2DArrayRTH;

		// Token: 0x0400032D RID: 813
		private static RTHandle m_BlackUIntTextureRTH;

		// Token: 0x0400032E RID: 814
		private static Texture2DArray m_ClearTexture2DArray;

		// Token: 0x0400032F RID: 815
		private static Texture2D m_ClearTexture;

		// Token: 0x04000330 RID: 816
		private static RTHandle m_ClearTexture2DArrayRTH;

		// Token: 0x04000331 RID: 817
		private static RTHandle m_ClearTextureRTH;

		// Token: 0x04000332 RID: 818
		private static Texture2DArray m_MagentaTexture2DArray;

		// Token: 0x04000333 RID: 819
		private static Texture2D m_MagentaTexture;

		// Token: 0x04000334 RID: 820
		private static RTHandle m_MagentaTexture2DArrayRTH;

		// Token: 0x04000335 RID: 821
		private static RTHandle m_MagentaTextureRTH;

		// Token: 0x04000336 RID: 822
		private static Texture2D m_BlackTexture;

		// Token: 0x04000337 RID: 823
		private static Texture3D m_BlackTexture3D;

		// Token: 0x04000338 RID: 824
		private static Texture2DArray m_BlackTexture2DArray;

		// Token: 0x04000339 RID: 825
		private static RTHandle m_BlackTexture2DArrayRTH;

		// Token: 0x0400033A RID: 826
		private static RTHandle m_BlackTextureRTH;

		// Token: 0x0400033B RID: 827
		private static RTHandle m_BlackTexture3DRTH;

		// Token: 0x0400033C RID: 828
		private static Texture2DArray m_WhiteTexture2DArray;

		// Token: 0x0400033D RID: 829
		private static RTHandle m_WhiteTexture2DArrayRTH;

		// Token: 0x0400033E RID: 830
		private static RTHandle m_WhiteTextureRTH;
	}
}
