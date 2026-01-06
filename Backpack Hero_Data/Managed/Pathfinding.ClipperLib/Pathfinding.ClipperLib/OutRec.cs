using System;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000013 RID: 19
	internal class OutRec
	{
		// Token: 0x04000049 RID: 73
		public int Idx;

		// Token: 0x0400004A RID: 74
		public bool IsHole;

		// Token: 0x0400004B RID: 75
		public bool IsOpen;

		// Token: 0x0400004C RID: 76
		public OutRec FirstLeft;

		// Token: 0x0400004D RID: 77
		public OutPt Pts;

		// Token: 0x0400004E RID: 78
		public OutPt BottomPt;

		// Token: 0x0400004F RID: 79
		public PolyNode PolyNode;
	}
}
