using System;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000007 RID: 7
	public struct IntRect
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002ACC File Offset: 0x00000CCC
		public IntRect(long l, long t, long r, long b)
		{
			this.left = l;
			this.top = t;
			this.right = r;
			this.bottom = b;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002AEC File Offset: 0x00000CEC
		public IntRect(IntRect ir)
		{
			this.left = ir.left;
			this.top = ir.top;
			this.right = ir.right;
			this.bottom = ir.bottom;
		}

		// Token: 0x0400000D RID: 13
		public long left;

		// Token: 0x0400000E RID: 14
		public long top;

		// Token: 0x0400000F RID: 15
		public long right;

		// Token: 0x04000010 RID: 16
		public long bottom;
	}
}
