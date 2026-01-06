using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000A9 RID: 169
	public struct CompactVoxelSpan
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x0002E665 File Offset: 0x0002C865
		public CompactVoxelSpan(ushort bottom, uint height)
		{
			this.con = 24U;
			this.y = bottom;
			this.h = height;
			this.reg = 0;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002E684 File Offset: 0x0002C884
		public void SetConnection(int dir, uint value)
		{
			int num = dir * 6;
			this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63U) << num));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002E6B8 File Offset: 0x0002C8B8
		public int GetConnection(int dir)
		{
			return ((int)this.con >> dir * 6) & 63;
		}

		// Token: 0x0400046F RID: 1135
		public ushort y;

		// Token: 0x04000470 RID: 1136
		public uint con;

		// Token: 0x04000471 RID: 1137
		public uint h;

		// Token: 0x04000472 RID: 1138
		public int reg;
	}
}
