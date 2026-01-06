using System;
using Unity.Burst;
using Unity.Jobs;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001DF RID: 479
	[BurstCompile(CompileSynchronously = true)]
	internal struct JobBuildCompactField : IJob
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x0004A604 File Offset: 0x00048804
		public void Execute()
		{
			this.output.BuildFromLinkedField(this.input);
		}

		// Token: 0x040008C3 RID: 2243
		public LinkedVoxelField input;

		// Token: 0x040008C4 RID: 2244
		public CompactVoxelField output;
	}
}
