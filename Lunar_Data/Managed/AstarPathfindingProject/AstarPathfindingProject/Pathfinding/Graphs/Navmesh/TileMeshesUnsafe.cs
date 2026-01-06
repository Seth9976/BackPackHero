using System;
using Unity.Collections;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001CA RID: 458
	public struct TileMeshesUnsafe
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x00046274 File Offset: 0x00044474
		public TileMeshesUnsafe(NativeArray<TileMesh.TileMeshUnsafe> tileMeshes, IntRect tileRect, Vector2 tileWorldSize)
		{
			this.tileMeshes = tileMeshes;
			this.tileRect = tileRect;
			this.tileWorldSize = tileWorldSize;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0004628C File Offset: 0x0004448C
		public TileMeshes ToManaged()
		{
			TileMesh[] array = new TileMesh[this.tileMeshes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.tileMeshes[i].ToManaged();
			}
			return new TileMeshes
			{
				tileMeshes = array,
				tileRect = this.tileRect,
				tileWorldSize = this.tileWorldSize
			};
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00046300 File Offset: 0x00044500
		public void Dispose()
		{
			if (!this.tileMeshes.IsCreated)
			{
				return;
			}
			for (int i = 0; i < this.tileMeshes.Length; i++)
			{
				this.tileMeshes[i].Dispose();
			}
			this.tileMeshes.Dispose();
		}

		// Token: 0x04000866 RID: 2150
		public NativeArray<TileMesh.TileMeshUnsafe> tileMeshes;

		// Token: 0x04000867 RID: 2151
		public IntRect tileRect;

		// Token: 0x04000868 RID: 2152
		public Vector2 tileWorldSize;
	}
}
