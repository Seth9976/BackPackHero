using System;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001D8 RID: 472
	public struct LinkedVoxelSpan
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00047A6E File Offset: 0x00045C6E
		public LinkedVoxelSpan(uint bottom, uint top, int area)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = -1;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00047A8C File Offset: 0x00045C8C
		public LinkedVoxelSpan(uint bottom, uint top, int area, int next)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = next;
		}

		// Token: 0x0400089A RID: 2202
		public uint bottom;

		// Token: 0x0400089B RID: 2203
		public uint top;

		// Token: 0x0400089C RID: 2204
		public int next;

		// Token: 0x0400089D RID: 2205
		public int area;
	}
}
