using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000A2 RID: 162
	public struct LinkedVoxelSpan
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		public LinkedVoxelSpan(uint bottom, uint top, int area)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = -1;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002E3F6 File Offset: 0x0002C5F6
		public LinkedVoxelSpan(uint bottom, uint top, int area, int next)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = next;
		}

		// Token: 0x04000455 RID: 1109
		public uint bottom;

		// Token: 0x04000456 RID: 1110
		public uint top;

		// Token: 0x04000457 RID: 1111
		public int next;

		// Token: 0x04000458 RID: 1112
		public int area;
	}
}
