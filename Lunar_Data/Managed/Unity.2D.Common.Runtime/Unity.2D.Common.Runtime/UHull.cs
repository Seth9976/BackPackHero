using System;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x0200000C RID: 12
	internal struct UHull
	{
		// Token: 0x04000028 RID: 40
		public float2 a;

		// Token: 0x04000029 RID: 41
		public float2 b;

		// Token: 0x0400002A RID: 42
		public int idx;

		// Token: 0x0400002B RID: 43
		public ArraySlice<int> ilarray;

		// Token: 0x0400002C RID: 44
		public int ilcount;

		// Token: 0x0400002D RID: 45
		public ArraySlice<int> iuarray;

		// Token: 0x0400002E RID: 46
		public int iucount;
	}
}
