using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001C6 RID: 454
	public struct TileLayout
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x000459C0 File Offset: 0x00043BC0
		public float CellHeight
		{
			get
			{
				return Mathf.Max(this.boundsYSize / 64000f, 0.001f);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000459D8 File Offset: 0x00043BD8
		public float TileWorldSizeX
		{
			get
			{
				return (float)this.tileSizeInVoxels.x * this.cellSize;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x000459ED File Offset: 0x00043BED
		public float TileWorldSizeZ
		{
			get
			{
				return (float)this.tileSizeInVoxels.y * this.cellSize;
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00045A04 File Offset: 0x00043C04
		public Bounds GetTileBoundsInGraphSpace(int x, int z, int width = 1, int depth = 1)
		{
			Bounds bounds = default(Bounds);
			bounds.SetMinMax(new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ), new Vector3((float)(x + width) * this.TileWorldSizeX, this.boundsYSize, (float)(z + depth) * this.TileWorldSizeZ));
			return bounds;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00045A60 File Offset: 0x00043C60
		public IntRect GetTouchingTiles(Bounds bounds, float margin = 0f)
		{
			bounds = this.transform.InverseTransform(bounds);
			return new IntRect(Mathf.FloorToInt((bounds.min.x - margin) / this.TileWorldSizeX), Mathf.FloorToInt((bounds.min.z - margin) / this.TileWorldSizeZ), Mathf.FloorToInt((bounds.max.x + margin) / this.TileWorldSizeX), Mathf.FloorToInt((bounds.max.z + margin) / this.TileWorldSizeZ));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00045AE8 File Offset: 0x00043CE8
		public TileLayout(RecastGraph graph)
		{
			this = new TileLayout(new Bounds(graph.forcedBoundsCenter, graph.forcedBoundsSize), Quaternion.Euler(graph.rotation), graph.cellSize, graph.editorTileSize, graph.useTiles);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00045B20 File Offset: 0x00043D20
		public TileLayout(Bounds bounds, Quaternion rotation, float cellSize, int tileSizeInVoxels, bool useTiles)
		{
			this.transform = RecastGraph.CalculateTransform(bounds, rotation);
			this.cellSize = cellSize;
			Vector3 size = bounds.size;
			this.boundsYSize = size.y;
			int num = (int)(size.x / cellSize + 0.5f);
			int num2 = (int)(size.z / cellSize + 0.5f);
			if (!useTiles)
			{
				this.tileSizeInVoxels = new Int2(num, num2);
			}
			else
			{
				this.tileSizeInVoxels = new Int2(tileSizeInVoxels, tileSizeInVoxels);
			}
			this.tileCount = new Int2(Mathf.Max(0, (num + this.tileSizeInVoxels.x - 1) / this.tileSizeInVoxels.x), Mathf.Max(0, (num2 + this.tileSizeInVoxels.y - 1) / this.tileSizeInVoxels.y));
			if (this.tileCount.x * this.tileCount.y > 524288)
			{
				throw new Exception(string.Concat(new string[]
				{
					"Too many tiles (",
					(this.tileCount.x * this.tileCount.y).ToString(),
					") maximum is ",
					524288.ToString(),
					"\nTry disabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* inspector."
				}));
			}
		}

		// Token: 0x04000858 RID: 2136
		public Int2 tileCount;

		// Token: 0x04000859 RID: 2137
		public GraphTransform transform;

		// Token: 0x0400085A RID: 2138
		public Int2 tileSizeInVoxels;

		// Token: 0x0400085B RID: 2139
		public float boundsYSize;

		// Token: 0x0400085C RID: 2140
		public float cellSize;
	}
}
