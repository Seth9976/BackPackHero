using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200001A RID: 26
	internal class OutRec
	{
		// Token: 0x04000063 RID: 99
		internal int Idx;

		// Token: 0x04000064 RID: 100
		internal bool IsHole;

		// Token: 0x04000065 RID: 101
		internal bool IsOpen;

		// Token: 0x04000066 RID: 102
		internal OutRec FirstLeft;

		// Token: 0x04000067 RID: 103
		internal OutPt Pts;

		// Token: 0x04000068 RID: 104
		internal OutPt BottomPt;

		// Token: 0x04000069 RID: 105
		internal PolyNode PolyNode;
	}
}
