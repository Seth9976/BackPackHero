using System;
using System.Diagnostics;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainCallbacks
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007F RID: 127 RVA: 0x00002360 File Offset: 0x00000560
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x00002394 File Offset: 0x00000594
		[field: DebuggerBrowsable(0)]
		public static event TerrainCallbacks.HeightmapChangedCallback heightmapChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000081 RID: 129 RVA: 0x000023C8 File Offset: 0x000005C8
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x000023FC File Offset: 0x000005FC
		[field: DebuggerBrowsable(0)]
		public static event TerrainCallbacks.TextureChangedCallback textureChanged;

		// Token: 0x06000083 RID: 131 RVA: 0x00002430 File Offset: 0x00000630
		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			bool flag = TerrainCallbacks.heightmapChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.heightmapChanged(terrain, heightRegion, synched);
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002474 File Offset: 0x00000674
		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			bool flag = TerrainCallbacks.textureChanged != null;
			if (flag)
			{
				foreach (Terrain terrain in terrainData.users)
				{
					TerrainCallbacks.textureChanged(terrain, textureName, texelRegion, synched);
				}
			}
		}

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000086 RID: 134
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600008A RID: 138
		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);
	}
}
