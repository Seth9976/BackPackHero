using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001E2 RID: 482
	[BurstCompile(CompileSynchronously = true)]
	internal struct JobBuildDistanceField : IJob
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x0004A894 File Offset: 0x00048A94
		public void Execute()
		{
			NativeArray<ushort> nativeArray = new NativeArray<ushort>(this.field.spans.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			VoxelUtilityBurst.CalculateDistanceField(this.field, nativeArray);
			this.output.ResizeUninitialized(this.field.spans.Length);
			VoxelUtilityBurst.BoxBlur(this.field, nativeArray, this.output.AsArray());
		}

		// Token: 0x040008CA RID: 2250
		public CompactVoxelField field;

		// Token: 0x040008CB RID: 2251
		public NativeList<ushort> output;
	}
}
