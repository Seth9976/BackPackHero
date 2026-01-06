using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C7 RID: 199
	internal static class TileSizeExtensions
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x000207FB File Offset: 0x0001E9FB
		public static bool IsValid(this TileSize tileSize)
		{
			return tileSize == TileSize._8 || tileSize == TileSize._16 || tileSize == TileSize._32 || tileSize == TileSize._64;
		}
	}
}
