using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x02000125 RID: 293
	internal struct UTriangle
	{
		// Token: 0x04000846 RID: 2118
		public float2 va;

		// Token: 0x04000847 RID: 2119
		public float2 vb;

		// Token: 0x04000848 RID: 2120
		public float2 vc;

		// Token: 0x04000849 RID: 2121
		public UCircle c;

		// Token: 0x0400084A RID: 2122
		public float area;

		// Token: 0x0400084B RID: 2123
		public int3 indices;
	}
}
