using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000A8 RID: 168
	public struct CompactVoxelCell
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0002E655 File Offset: 0x0002C855
		public CompactVoxelCell(uint i, uint c)
		{
			this.index = i;
			this.count = c;
		}

		// Token: 0x0400046D RID: 1133
		public uint index;

		// Token: 0x0400046E RID: 1134
		public uint count;
	}
}
