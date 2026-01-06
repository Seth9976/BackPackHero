using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000AA RID: 170
	public class VoxelSpan
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0002E6CA File Offset: 0x0002C8CA
		public VoxelSpan(uint b, uint t, int area)
		{
			this.bottom = b;
			this.top = t;
			this.area = area;
		}

		// Token: 0x04000473 RID: 1139
		public uint bottom;

		// Token: 0x04000474 RID: 1140
		public uint top;

		// Token: 0x04000475 RID: 1141
		public VoxelSpan next;

		// Token: 0x04000476 RID: 1142
		public int area;
	}
}
