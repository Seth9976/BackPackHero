using System;
using Unity.Mathematics;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000010 RID: 16
	internal struct UTriangle
	{
		// Token: 0x04000035 RID: 53
		public float2 va;

		// Token: 0x04000036 RID: 54
		public float2 vb;

		// Token: 0x04000037 RID: 55
		public float2 vc;

		// Token: 0x04000038 RID: 56
		public UCircle c;

		// Token: 0x04000039 RID: 57
		public float area;

		// Token: 0x0400003A RID: 58
		public int3 indices;
	}
}
