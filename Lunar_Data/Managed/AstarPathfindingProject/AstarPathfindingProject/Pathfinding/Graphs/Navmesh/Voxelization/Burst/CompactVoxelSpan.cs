using System;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001D5 RID: 469
	public struct CompactVoxelSpan
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0004747F File Offset: 0x0004567F
		public CompactVoxelSpan(ushort bottom, uint height)
		{
			this.con = 24U;
			this.y = bottom;
			this.h = height;
			this.reg = 0;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000474A0 File Offset: 0x000456A0
		public void SetConnection(int dir, uint value)
		{
			int num = dir * 6;
			this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63U) << num));
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000474D4 File Offset: 0x000456D4
		public int GetConnection(int dir)
		{
			return ((int)this.con >> dir * 6) & 63;
		}

		// Token: 0x04000888 RID: 2184
		public ushort y;

		// Token: 0x04000889 RID: 2185
		public uint con;

		// Token: 0x0400088A RID: 2186
		public uint h;

		// Token: 0x0400088B RID: 2187
		public int reg;
	}
}
