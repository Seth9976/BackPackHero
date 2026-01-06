using System;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh.Voxelization.Burst
{
	// Token: 0x020001DD RID: 477
	public struct RasterizationMesh
	{
		// Token: 0x040008B0 RID: 2224
		public UnsafeSpan<float3> vertices;

		// Token: 0x040008B1 RID: 2225
		public UnsafeSpan<int> triangles;

		// Token: 0x040008B2 RID: 2226
		public int area;

		// Token: 0x040008B3 RID: 2227
		public Bounds bounds;

		// Token: 0x040008B4 RID: 2228
		public Matrix4x4 matrix;

		// Token: 0x040008B5 RID: 2229
		public bool solid;

		// Token: 0x040008B6 RID: 2230
		public bool doubleSided;

		// Token: 0x040008B7 RID: 2231
		public bool areaIsTag;

		// Token: 0x040008B8 RID: 2232
		public bool flatten;
	}
}
