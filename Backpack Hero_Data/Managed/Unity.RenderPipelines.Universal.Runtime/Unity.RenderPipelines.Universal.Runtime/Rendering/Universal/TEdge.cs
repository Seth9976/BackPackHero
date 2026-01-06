using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000014 RID: 20
	internal class TEdge
	{
		// Token: 0x04000045 RID: 69
		internal IntPoint Bot;

		// Token: 0x04000046 RID: 70
		internal IntPoint Curr;

		// Token: 0x04000047 RID: 71
		internal IntPoint Top;

		// Token: 0x04000048 RID: 72
		internal IntPoint Delta;

		// Token: 0x04000049 RID: 73
		internal double Dx;

		// Token: 0x0400004A RID: 74
		internal PolyType PolyTyp;

		// Token: 0x0400004B RID: 75
		internal EdgeSide Side;

		// Token: 0x0400004C RID: 76
		internal int WindDelta;

		// Token: 0x0400004D RID: 77
		internal int WindCnt;

		// Token: 0x0400004E RID: 78
		internal int WindCnt2;

		// Token: 0x0400004F RID: 79
		internal int OutIdx;

		// Token: 0x04000050 RID: 80
		internal TEdge Next;

		// Token: 0x04000051 RID: 81
		internal TEdge Prev;

		// Token: 0x04000052 RID: 82
		internal TEdge NextInLML;

		// Token: 0x04000053 RID: 83
		internal TEdge NextInAEL;

		// Token: 0x04000054 RID: 84
		internal TEdge PrevInAEL;

		// Token: 0x04000055 RID: 85
		internal TEdge NextInSEL;

		// Token: 0x04000056 RID: 86
		internal TEdge PrevInSEL;
	}
}
