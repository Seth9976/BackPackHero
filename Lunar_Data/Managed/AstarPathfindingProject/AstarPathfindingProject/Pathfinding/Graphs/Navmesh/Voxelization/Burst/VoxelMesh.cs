using System;
using Pathfinding.Jobs;
using Unity.Collections;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001DB RID: 475
	public struct VoxelMesh : IArenaDisposable
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x00048FEE File Offset: 0x000471EE
		void IArenaDisposable.DisposeWith(DisposeArena arena)
		{
			arena.Add<Int3>(this.verts);
			arena.Add<int>(this.tris);
			arena.Add<int>(this.areas);
		}

		// Token: 0x040008A9 RID: 2217
		public NativeList<Int3> verts;

		// Token: 0x040008AA RID: 2218
		public NativeList<int> tris;

		// Token: 0x040008AB RID: 2219
		public NativeList<int> areas;
	}
}
