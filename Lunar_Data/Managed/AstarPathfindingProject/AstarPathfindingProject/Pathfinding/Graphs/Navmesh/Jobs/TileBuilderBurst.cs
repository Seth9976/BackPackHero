using System;
using Pathfinding.Graphs.Navmesh.Voxelization.Burst;
using Pathfinding.Jobs;
using Unity.Collections;

namespace Pathfinding.Graphs.Navmesh.Jobs
{
	// Token: 0x020001ED RID: 493
	public struct TileBuilderBurst : IArenaDisposable
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		public TileBuilderBurst(int width, int depth, int voxelWalkableHeight, int maximumVoxelYCoord)
		{
			this.linkedVoxelField = new LinkedVoxelField(width, depth, maximumVoxelYCoord);
			this.compactVoxelField = new CompactVoxelField(width, depth, voxelWalkableHeight, Allocator.Persistent);
			this.tmpQueue1 = new NativeQueue<Int3>(Allocator.Persistent);
			this.tmpQueue2 = new NativeQueue<Int3>(Allocator.Persistent);
			this.distanceField = new NativeList<ushort>(0, Allocator.Persistent);
			this.contours = new NativeList<VoxelContour>(Allocator.Persistent);
			this.contourVertices = new NativeList<int>(Allocator.Persistent);
			this.voxelMesh = new VoxelMesh
			{
				verts = new NativeList<Int3>(Allocator.Persistent),
				tris = new NativeList<int>(Allocator.Persistent),
				areas = new NativeList<int>(Allocator.Persistent)
			};
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0004C8A4 File Offset: 0x0004AAA4
		void IArenaDisposable.DisposeWith(DisposeArena arena)
		{
			arena.Add<LinkedVoxelField>(this.linkedVoxelField);
			arena.Add<CompactVoxelField>(this.compactVoxelField);
			arena.Add<ushort>(this.distanceField);
			arena.Add<Int3>(this.tmpQueue1);
			arena.Add<Int3>(this.tmpQueue2);
			arena.Add<VoxelContour>(this.contours);
			arena.Add<int>(this.contourVertices);
			arena.Add<VoxelMesh>(this.voxelMesh);
		}

		// Token: 0x040008FF RID: 2303
		public LinkedVoxelField linkedVoxelField;

		// Token: 0x04000900 RID: 2304
		public CompactVoxelField compactVoxelField;

		// Token: 0x04000901 RID: 2305
		public NativeList<ushort> distanceField;

		// Token: 0x04000902 RID: 2306
		public NativeQueue<Int3> tmpQueue1;

		// Token: 0x04000903 RID: 2307
		public NativeQueue<Int3> tmpQueue2;

		// Token: 0x04000904 RID: 2308
		public NativeList<VoxelContour> contours;

		// Token: 0x04000905 RID: 2309
		public NativeList<int> contourVertices;

		// Token: 0x04000906 RID: 2310
		public VoxelMesh voxelMesh;
	}
}
