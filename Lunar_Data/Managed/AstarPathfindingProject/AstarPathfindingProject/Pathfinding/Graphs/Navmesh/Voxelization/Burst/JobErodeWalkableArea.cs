using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001E1 RID: 481
	[BurstCompile(CompileSynchronously = true)]
	internal struct JobErodeWalkableArea : IJob
	{
		// Token: 0x06000C47 RID: 3143 RVA: 0x0004A82C File Offset: 0x00048A2C
		public void Execute()
		{
			NativeArray<ushort> nativeArray = new NativeArray<ushort>(this.field.spans.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			VoxelUtilityBurst.CalculateDistanceField(this.field, nativeArray);
			for (int i = 0; i < nativeArray.Length; i++)
			{
				if ((int)nativeArray[i] < this.radius * 2)
				{
					this.field.areaTypes[i] = 0;
				}
			}
		}

		// Token: 0x040008C8 RID: 2248
		public CompactVoxelField field;

		// Token: 0x040008C9 RID: 2249
		public int radius;
	}
}
