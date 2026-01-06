using System;
using System.Runtime.InteropServices;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001F1 RID: 497
	public struct JobConnectTiles : IJob
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x0004D180 File Offset: 0x0004B380
		public static JobHandle ScheduleBatch(GCHandle tilesHandle, JobHandle dependency, IntRect tileRect, Vector2 tileWorldSize, float maxTileConnectionEdgeDistance)
		{
			int height = tileRect.Height;
			int width = tileRect.Width;
			JobHandle jobHandle = dependency;
			for (int i = 0; i <= 1; i++)
			{
				JobHandle jobHandle2 = jobHandle;
				for (int j = 0; j <= 1; j++)
				{
					for (int k = 0; k < height; k++)
					{
						for (int l = 0; l < width; l++)
						{
							if ((l + k) % 2 == i)
							{
								int num = l + k * width;
								int num2;
								if (j == 0 && l < width - 1)
								{
									num2 = l + 1 + k * width;
								}
								else
								{
									if (j != 1 || k >= height - 1)
									{
										goto IL_00D3;
									}
									num2 = l + (k + 1) * width;
								}
								JobHandle jobHandle3 = new JobConnectTiles
								{
									tiles = tilesHandle,
									tileIndex1 = num,
									tileIndex2 = num2,
									tileWorldSizeX = tileWorldSize.x,
									tileWorldSizeZ = tileWorldSize.y,
									maxTileConnectionEdgeDistance = maxTileConnectionEdgeDistance
								}.Schedule(jobHandle);
								jobHandle2 = JobHandle.CombineDependencies(jobHandle2, jobHandle3);
							}
							IL_00D3:;
						}
					}
					jobHandle = jobHandle2;
				}
			}
			return jobHandle;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0004D29C File Offset: 0x0004B49C
		public void Execute()
		{
			NavmeshTile[] array = (NavmeshTile[])this.tiles.Target;
			NavmeshBase.ConnectTiles(array[this.tileIndex1], array[this.tileIndex2], this.tileWorldSizeX, this.tileWorldSizeZ, this.maxTileConnectionEdgeDistance);
		}

		// Token: 0x0400092E RID: 2350
		public GCHandle tiles;

		// Token: 0x0400092F RID: 2351
		public int tileIndex1;

		// Token: 0x04000930 RID: 2352
		public int tileIndex2;

		// Token: 0x04000931 RID: 2353
		public float tileWorldSizeX;

		// Token: 0x04000932 RID: 2354
		public float tileWorldSizeZ;

		// Token: 0x04000933 RID: 2355
		public float maxTileConnectionEdgeDistance;
	}
}
