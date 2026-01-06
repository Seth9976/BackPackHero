using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x02000121 RID: 289
	internal struct UHull
	{
		// Token: 0x04000839 RID: 2105
		public float2 a;

		// Token: 0x0400083A RID: 2106
		public float2 b;

		// Token: 0x0400083B RID: 2107
		public int idx;

		// Token: 0x0400083C RID: 2108
		public ArraySlice<int> ilarray;

		// Token: 0x0400083D RID: 2109
		public int ilcount;

		// Token: 0x0400083E RID: 2110
		public ArraySlice<int> iuarray;

		// Token: 0x0400083F RID: 2111
		public int iucount;
	}
}
