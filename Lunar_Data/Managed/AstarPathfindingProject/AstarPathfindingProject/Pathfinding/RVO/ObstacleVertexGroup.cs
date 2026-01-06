using System;
using Unity.Mathematics;

namespace Pathfinding.RVO
{
	// Token: 0x02000293 RID: 659
	public struct ObstacleVertexGroup
	{
		// Token: 0x04000BB0 RID: 2992
		public ObstacleType type;

		// Token: 0x04000BB1 RID: 2993
		public int vertexCount;

		// Token: 0x04000BB2 RID: 2994
		public float3 boundsMn;

		// Token: 0x04000BB3 RID: 2995
		public float3 boundsMx;
	}
}
