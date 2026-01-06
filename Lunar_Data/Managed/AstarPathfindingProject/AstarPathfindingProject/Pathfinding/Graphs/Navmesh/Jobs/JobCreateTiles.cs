using System;
using System.Runtime.InteropServices;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001F3 RID: 499
	public struct JobCreateTiles : IJob
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x0004D338 File Offset: 0x0004B538
		public unsafe void Execute()
		{
			NavmeshTile[] array = (NavmeshTile[])this.tiles.Target;
			int width = this.tileRect.Width;
			int height = this.tileRect.Height;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num = i * width + j;
					int num2 = (i + this.tileRect.ymin) * this.graphTileCount.x + (j + this.tileRect.xmin);
					TileMesh.TileMeshUnsafe tileMeshUnsafe = this.tileMeshes[num];
					UnsafeSpan<Int3> unsafeSpan = tileMeshUnsafe.verticesInTileSpace.AsUnsafeSpan<Int3>().Clone(Allocator.Persistent);
					UnsafeSpan<Int3> unsafeSpan2 = unsafeSpan.Clone(Allocator.Persistent);
					Int3 @int = (Int3)new Vector3(this.tileWorldSize.x * (float)(j + this.tileRect.xmin), 0f, this.tileWorldSize.y * (float)(i + this.tileRect.ymin));
					for (int k = 0; k < unsafeSpan.Length; k++)
					{
						Int3 int2 = *unsafeSpan[k] + @int;
						*unsafeSpan[k] = int2;
						*unsafeSpan2[k] = (Int3)this.graphToWorldSpace.MultiplyPoint3x4((Vector3)int2);
					}
					UnsafeSpan<int> unsafeSpan3 = tileMeshUnsafe.triangles.AsUnsafeSpan<int>().Clone(Allocator.Persistent);
					NavmeshTile navmeshTile = new NavmeshTile
					{
						x = j + this.tileRect.xmin,
						z = i + this.tileRect.ymin,
						w = 1,
						d = 1,
						tris = unsafeSpan3,
						vertsInGraphSpace = unsafeSpan,
						verts = unsafeSpan2,
						bbTree = new BBTree(unsafeSpan3, unsafeSpan),
						nodes = new TriangleMeshNode[unsafeSpan3.Length / 3],
						graph = null
					};
					NavmeshBase.CreateNodes(navmeshTile, navmeshTile.tris, num2, this.graphIndex, tileMeshUnsafe.tags.AsUnsafeSpan<uint>(), false, null, this.initialPenalty, this.recalculateNormals);
					array[num] = navmeshTile;
				}
			}
		}

		// Token: 0x04000935 RID: 2357
		[ReadOnly]
		public NativeArray<TileMesh.TileMeshUnsafe> tileMeshes;

		// Token: 0x04000936 RID: 2358
		public GCHandle tiles;

		// Token: 0x04000937 RID: 2359
		public uint graphIndex;

		// Token: 0x04000938 RID: 2360
		public Int2 graphTileCount;

		// Token: 0x04000939 RID: 2361
		public IntRect tileRect;

		// Token: 0x0400093A RID: 2362
		public uint initialPenalty;

		// Token: 0x0400093B RID: 2363
		public bool recalculateNormals;

		// Token: 0x0400093C RID: 2364
		public Vector2 tileWorldSize;

		// Token: 0x0400093D RID: 2365
		public Matrix4x4 graphToWorldSpace;
	}
}
