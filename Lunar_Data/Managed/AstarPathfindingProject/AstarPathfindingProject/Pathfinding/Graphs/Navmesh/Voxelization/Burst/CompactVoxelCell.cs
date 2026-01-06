using System;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001D4 RID: 468
	public struct CompactVoxelCell
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0004746F File Offset: 0x0004566F
		public CompactVoxelCell(int i, int c)
		{
			this.index = i;
			this.count = c;
		}

		// Token: 0x04000886 RID: 2182
		public int index;

		// Token: 0x04000887 RID: 2183
		public int count;
	}
}
