using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020000FE RID: 254
	internal static class DeferredConfig
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0002B231 File Offset: 0x00029431
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0002B238 File Offset: 0x00029438
		internal static bool IsOpenGL { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0002B240 File Offset: 0x00029440
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0002B247 File Offset: 0x00029447
		internal static bool IsDX10 { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0002B24F File Offset: 0x0002944F
		internal static bool UseCBufferForDepthRange
		{
			get
			{
				return DeferredConfig.IsOpenGL;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0002B256 File Offset: 0x00029456
		internal static bool UseCBufferForTileList
		{
			get
			{
				return DeferredConfig.IsOpenGL;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0002B25D File Offset: 0x0002945D
		internal static bool UseCBufferForLightData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0002B260 File Offset: 0x00029460
		internal static bool UseCBufferForLightList
		{
			get
			{
				return DeferredConfig.IsOpenGL;
			}
		}

		// Token: 0x040006E6 RID: 1766
		public const int kPreferredCBufferSize = 65536;

		// Token: 0x040006E7 RID: 1767
		public const int kPreferredStructuredBufferSize = 131072;

		// Token: 0x040006E8 RID: 1768
		public const int kTilePixelWidth = 16;

		// Token: 0x040006E9 RID: 1769
		public const int kTilePixelHeight = 16;

		// Token: 0x040006EA RID: 1770
		public const int kTilerDepth = 3;

		// Token: 0x040006EB RID: 1771
		public const int kTilerSubdivisions = 4;

		// Token: 0x040006EC RID: 1772
		public const int kAvgLightPerTile = 32;

		// Token: 0x040006ED RID: 1773
		public const int kTileDepthInfoIntermediateLevel = -1;

		// Token: 0x040006EE RID: 1774
		public const bool kHasNativeQuadSupport = false;
	}
}
