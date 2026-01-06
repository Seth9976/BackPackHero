using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001EF RID: 495
	[BurstCompile]
	public struct JobCalculateTriangleConnections : IJob
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0004CF7C File Offset: 0x0004B17C
		public unsafe void Execute()
		{
			NativeParallelHashMap<int2, uint> nativeParallelHashMap = new NativeParallelHashMap<int2, uint>(128, Allocator.Temp);
			bool flag = false;
			for (int i = 0; i < this.tileMeshes.Length; i++)
			{
				nativeParallelHashMap.Clear();
				TileMesh.TileMeshUnsafe tileMeshUnsafe = this.tileMeshes[i];
				int num = tileMeshUnsafe.triangles.Length / 4;
				UnsafeAppendBuffer unsafeAppendBuffer = new UnsafeAppendBuffer(num * 2 * 4, 4, Allocator.Persistent);
				UnsafeAppendBuffer unsafeAppendBuffer2 = new UnsafeAppendBuffer(num * 4, 4, Allocator.Persistent);
				int* ptr = (int*)tileMeshUnsafe.triangles.Ptr;
				int j = 0;
				int num2 = 0;
				while (j < num)
				{
					flag |= !nativeParallelHashMap.TryAdd(new int2(ptr[j], ptr[j + 1]), (uint)(num2 | 0));
					flag |= !nativeParallelHashMap.TryAdd(new int2(ptr[j + 1], ptr[j + 2]), (uint)(num2 | 268435456));
					flag |= !nativeParallelHashMap.TryAdd(new int2(ptr[j + 2], ptr[j]), (uint)(num2 | 536870912));
					j += 3;
					num2++;
				}
				for (int k = 0; k < num; k += 3)
				{
					int num3 = 0;
					for (int l = 0; l < 3; l++)
					{
						uint num4;
						if (nativeParallelHashMap.TryGetValue(new int2(ptr[k + (l + 1) % 3], ptr[k + l]), out num4))
						{
							uint num5 = num4 & 268435455U;
							int num6 = (int)(num4 >> 28);
							unsafeAppendBuffer.Add<uint>(num5);
							byte b = Connection.PackShapeEdgeInfo((byte)l, (byte)num6, true, true, true);
							unsafeAppendBuffer.Add<int>((int)b);
							num3++;
						}
					}
					unsafeAppendBuffer2.Add<int>(num3);
				}
				this.nodeConnections[i] = new JobCalculateTriangleConnections.TileNodeConnectionsUnsafe
				{
					neighbours = unsafeAppendBuffer,
					neighbourCounts = unsafeAppendBuffer2
				};
			}
			if (flag)
			{
				Debug.LogWarning("Duplicate triangle edges were found in the input mesh. These have been removed. Are you sure your mesh is suitable for being used as a navmesh directly?\nThis could be caused by the mesh's normals not being consistent.");
			}
		}

		// Token: 0x0400092A RID: 2346
		[ReadOnly]
		public NativeArray<TileMesh.TileMeshUnsafe> tileMeshes;

		// Token: 0x0400092B RID: 2347
		[WriteOnly]
		public NativeArray<JobCalculateTriangleConnections.TileNodeConnectionsUnsafe> nodeConnections;

		// Token: 0x020001F0 RID: 496
		public struct TileNodeConnectionsUnsafe
		{
			// Token: 0x0400092C RID: 2348
			public UnsafeAppendBuffer neighbours;

			// Token: 0x0400092D RID: 2349
			public UnsafeAppendBuffer neighbourCounts;
		}
	}
}
