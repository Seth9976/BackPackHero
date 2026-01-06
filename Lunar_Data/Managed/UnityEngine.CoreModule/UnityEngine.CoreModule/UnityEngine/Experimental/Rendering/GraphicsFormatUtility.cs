using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200047A RID: 1146
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/Format.h")]
	[NativeHeader("Runtime/Graphics/TextureFormat.h")]
	public class GraphicsFormatUtility
	{
		// Token: 0x0600282D RID: 10285
		[FreeFunction("GetTextureGraphicsFormat")]
		[MethodImpl(4096)]
		internal static extern GraphicsFormat GetFormat([NotNull("NullExceptionObject")] Texture texture);

		// Token: 0x0600282E RID: 10286 RVA: 0x00042C40 File Offset: 0x00040E40
		public static GraphicsFormat GetGraphicsFormat(TextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_TextureFormat(format, isSRGB);
		}

		// Token: 0x0600282F RID: 10287
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_TextureFormat(TextureFormat format, bool isSRGB);

		// Token: 0x06002830 RID: 10288 RVA: 0x00042C5C File Offset: 0x00040E5C
		public static TextureFormat GetTextureFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.GetTextureFormat_Native_GraphicsFormat(format);
		}

		// Token: 0x06002831 RID: 10289
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern TextureFormat GetTextureFormat_Native_GraphicsFormat(GraphicsFormat format);

		// Token: 0x06002832 RID: 10290 RVA: 0x00042C74 File Offset: 0x00040E74
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_RenderTextureFormat(format, isSRGB);
		}

		// Token: 0x06002833 RID: 10291
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_RenderTextureFormat(RenderTextureFormat format, bool isSRGB);

		// Token: 0x06002834 RID: 10292 RVA: 0x00042C90 File Offset: 0x00040E90
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear;
			bool flag2 = ((readWrite == RenderTextureReadWrite.Default) ? flag : (readWrite == RenderTextureReadWrite.sRGB));
			return GraphicsFormatUtility.GetGraphicsFormat(format, flag2);
		}

		// Token: 0x06002835 RID: 10293
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern GraphicsFormat GetDepthStencilFormatFromBitsLegacy_Native(int minimumDepthBits);

		// Token: 0x06002836 RID: 10294 RVA: 0x00042CC0 File Offset: 0x00040EC0
		internal static GraphicsFormat GetDepthStencilFormat(int minimumDepthBits)
		{
			return GraphicsFormatUtility.GetDepthStencilFormatFromBitsLegacy_Native(minimumDepthBits);
		}

		// Token: 0x06002837 RID: 10295
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int GetDepthBits(GraphicsFormat format);

		// Token: 0x06002838 RID: 10296 RVA: 0x00042CD8 File Offset: 0x00040ED8
		public static GraphicsFormat GetDepthStencilFormat(int minimumDepthBits, int minimumStencilBits)
		{
			bool flag = minimumDepthBits == 0 && minimumStencilBits == 0;
			GraphicsFormat graphicsFormat;
			if (flag)
			{
				graphicsFormat = GraphicsFormat.None;
			}
			else
			{
				bool flag2 = minimumDepthBits < 0 || minimumStencilBits < 0;
				if (flag2)
				{
					throw new ArgumentException("Number of bits in DepthStencil format can't be negative.");
				}
				bool flag3 = minimumDepthBits > 32;
				if (flag3)
				{
					throw new ArgumentException("Number of depth buffer bits cannot exceed 32.");
				}
				bool flag4 = minimumStencilBits > 8;
				if (flag4)
				{
					throw new ArgumentException("Number of stencil buffer bits cannot exceed 8.");
				}
				bool flag5 = minimumDepthBits <= 16;
				if (flag5)
				{
					minimumDepthBits = 16;
				}
				else
				{
					bool flag6 = minimumDepthBits <= 24;
					if (flag6)
					{
						minimumDepthBits = 24;
					}
					else
					{
						minimumDepthBits = 32;
					}
				}
				bool flag7 = minimumStencilBits != 0;
				if (flag7)
				{
					minimumStencilBits = 8;
				}
				Debug.Assert(GraphicsFormatUtility.tableNoStencil.Length == GraphicsFormatUtility.tableStencil.Length);
				GraphicsFormat[] array = ((minimumStencilBits > 0) ? GraphicsFormatUtility.tableStencil : GraphicsFormatUtility.tableNoStencil);
				int num = minimumDepthBits / 8;
				for (int i = num; i < array.Length; i++)
				{
					GraphicsFormat graphicsFormat2 = array[i];
					bool flag8 = SystemInfo.IsFormatSupported(graphicsFormat2, FormatUsage.Render);
					if (flag8)
					{
						return graphicsFormat2;
					}
				}
				graphicsFormat = GraphicsFormat.None;
			}
			return graphicsFormat;
		}

		// Token: 0x06002839 RID: 10297
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSRGBFormat(GraphicsFormat format);

		// Token: 0x0600283A RID: 10298
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSwizzleFormat(GraphicsFormat format);

		// Token: 0x0600283B RID: 10299
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetSRGBFormat(GraphicsFormat format);

		// Token: 0x0600283C RID: 10300
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern GraphicsFormat GetLinearFormat(GraphicsFormat format);

		// Token: 0x0600283D RID: 10301
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern RenderTextureFormat GetRenderTextureFormat(GraphicsFormat format);

		// Token: 0x0600283E RID: 10302
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetColorComponentCount(GraphicsFormat format);

		// Token: 0x0600283F RID: 10303
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetAlphaComponentCount(GraphicsFormat format);

		// Token: 0x06002840 RID: 10304
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetComponentCount(GraphicsFormat format);

		// Token: 0x06002841 RID: 10305
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern string GetFormatString(GraphicsFormat format);

		// Token: 0x06002842 RID: 10306
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsCompressedFormat(GraphicsFormat format);

		// Token: 0x06002843 RID: 10307
		[FreeFunction("IsAnyCompressedTextureFormat", true)]
		[MethodImpl(4096)]
		internal static extern bool IsCompressedTextureFormat(TextureFormat format);

		// Token: 0x06002844 RID: 10308
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern bool CanDecompressFormat(GraphicsFormat format, bool wholeImage);

		// Token: 0x06002845 RID: 10309 RVA: 0x00042DE4 File Offset: 0x00040FE4
		internal static bool CanDecompressFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.CanDecompressFormat(format, true);
		}

		// Token: 0x06002846 RID: 10310
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsPackedFormat(GraphicsFormat format);

		// Token: 0x06002847 RID: 10311
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool Is16BitPackedFormat(GraphicsFormat format);

		// Token: 0x06002848 RID: 10312
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern GraphicsFormat ConvertToAlphaFormat(GraphicsFormat format);

		// Token: 0x06002849 RID: 10313
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsAlphaOnlyFormat(GraphicsFormat format);

		// Token: 0x0600284A RID: 10314
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsAlphaTestFormat(GraphicsFormat format);

		// Token: 0x0600284B RID: 10315
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool HasAlphaChannel(GraphicsFormat format);

		// Token: 0x0600284C RID: 10316
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsDepthFormat(GraphicsFormat format);

		// Token: 0x0600284D RID: 10317
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsStencilFormat(GraphicsFormat format);

		// Token: 0x0600284E RID: 10318
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsIEEE754Format(GraphicsFormat format);

		// Token: 0x0600284F RID: 10319
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsFloatFormat(GraphicsFormat format);

		// Token: 0x06002850 RID: 10320
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsHalfFormat(GraphicsFormat format);

		// Token: 0x06002851 RID: 10321
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsUnsignedFormat(GraphicsFormat format);

		// Token: 0x06002852 RID: 10322
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSignedFormat(GraphicsFormat format);

		// Token: 0x06002853 RID: 10323
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsNormFormat(GraphicsFormat format);

		// Token: 0x06002854 RID: 10324
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsUNormFormat(GraphicsFormat format);

		// Token: 0x06002855 RID: 10325
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSNormFormat(GraphicsFormat format);

		// Token: 0x06002856 RID: 10326
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsIntegerFormat(GraphicsFormat format);

		// Token: 0x06002857 RID: 10327
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsUIntFormat(GraphicsFormat format);

		// Token: 0x06002858 RID: 10328
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsSIntFormat(GraphicsFormat format);

		// Token: 0x06002859 RID: 10329
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsXRFormat(GraphicsFormat format);

		// Token: 0x0600285A RID: 10330
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsDXTCFormat(GraphicsFormat format);

		// Token: 0x0600285B RID: 10331
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsRGTCFormat(GraphicsFormat format);

		// Token: 0x0600285C RID: 10332
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsBPTCFormat(GraphicsFormat format);

		// Token: 0x0600285D RID: 10333
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsBCFormat(GraphicsFormat format);

		// Token: 0x0600285E RID: 10334
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsPVRTCFormat(GraphicsFormat format);

		// Token: 0x0600285F RID: 10335
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsETCFormat(GraphicsFormat format);

		// Token: 0x06002860 RID: 10336
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsEACFormat(GraphicsFormat format);

		// Token: 0x06002861 RID: 10337
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsASTCFormat(GraphicsFormat format);

		// Token: 0x06002862 RID: 10338 RVA: 0x00042E00 File Offset: 0x00041000
		public static bool IsCrunchFormat(TextureFormat format)
		{
			return format == TextureFormat.DXT1Crunched || format == TextureFormat.DXT5Crunched || format == TextureFormat.ETC_RGB4Crunched || format == TextureFormat.ETC2_RGBA8Crunched;
		}

		// Token: 0x06002863 RID: 10339
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleR(GraphicsFormat format);

		// Token: 0x06002864 RID: 10340
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleG(GraphicsFormat format);

		// Token: 0x06002865 RID: 10341
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleB(GraphicsFormat format);

		// Token: 0x06002866 RID: 10342
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern FormatSwizzle GetSwizzleA(GraphicsFormat format);

		// Token: 0x06002867 RID: 10343
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetBlockSize(GraphicsFormat format);

		// Token: 0x06002868 RID: 10344
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetBlockWidth(GraphicsFormat format);

		// Token: 0x06002869 RID: 10345
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern uint GetBlockHeight(GraphicsFormat format);

		// Token: 0x0600286A RID: 10346 RVA: 0x00042E2C File Offset: 0x0004102C
		public static uint ComputeMipmapSize(int width, int height, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize_Native_2D(width, height, format);
		}

		// Token: 0x0600286B RID: 10347
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern uint ComputeMipmapSize_Native_2D(int width, int height, GraphicsFormat format);

		// Token: 0x0600286C RID: 10348 RVA: 0x00042E48 File Offset: 0x00041048
		public static uint ComputeMipmapSize(int width, int height, int depth, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize_Native_3D(width, height, depth, format);
		}

		// Token: 0x0600286D RID: 10349
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern uint ComputeMipmapSize_Native_3D(int width, int height, int depth, GraphicsFormat format);

		// Token: 0x04000F7D RID: 3965
		private static readonly GraphicsFormat[] tableNoStencil = new GraphicsFormat[]
		{
			GraphicsFormat.None,
			GraphicsFormat.D16_UNorm,
			GraphicsFormat.D16_UNorm,
			GraphicsFormat.D24_UNorm,
			GraphicsFormat.D32_SFloat
		};

		// Token: 0x04000F7E RID: 3966
		private static readonly GraphicsFormat[] tableStencil = new GraphicsFormat[]
		{
			GraphicsFormat.None,
			GraphicsFormat.D24_UNorm_S8_UInt,
			GraphicsFormat.D24_UNorm_S8_UInt,
			GraphicsFormat.D24_UNorm_S8_UInt,
			GraphicsFormat.D32_SFloat_S8_UInt
		};
	}
}
