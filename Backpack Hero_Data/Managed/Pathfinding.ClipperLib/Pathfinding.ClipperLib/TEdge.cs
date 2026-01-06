using System;

namespace Pathfinding.ClipperLib
{
	// Token: 0x0200000F RID: 15
	internal class TEdge
	{
		// Token: 0x0400002D RID: 45
		public IntPoint Bot;

		// Token: 0x0400002E RID: 46
		public IntPoint Curr;

		// Token: 0x0400002F RID: 47
		public IntPoint Top;

		// Token: 0x04000030 RID: 48
		public IntPoint Delta;

		// Token: 0x04000031 RID: 49
		public double Dx;

		// Token: 0x04000032 RID: 50
		public PolyType PolyTyp;

		// Token: 0x04000033 RID: 51
		public EdgeSide Side;

		// Token: 0x04000034 RID: 52
		public int WindDelta;

		// Token: 0x04000035 RID: 53
		public int WindCnt;

		// Token: 0x04000036 RID: 54
		public int WindCnt2;

		// Token: 0x04000037 RID: 55
		public int OutIdx;

		// Token: 0x04000038 RID: 56
		public TEdge Next;

		// Token: 0x04000039 RID: 57
		public TEdge Prev;

		// Token: 0x0400003A RID: 58
		public TEdge NextInLML;

		// Token: 0x0400003B RID: 59
		public TEdge NextInAEL;

		// Token: 0x0400003C RID: 60
		public TEdge PrevInAEL;

		// Token: 0x0400003D RID: 61
		public TEdge NextInSEL;

		// Token: 0x0400003E RID: 62
		public TEdge PrevInSEL;
	}
}
