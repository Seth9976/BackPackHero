using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000269 RID: 617
	public readonly struct ToWorldMatrix
	{
		// Token: 0x06000E97 RID: 3735 RVA: 0x0005AC69 File Offset: 0x00058E69
		public ToWorldMatrix(NativeMovementPlane plane)
		{
			this.matrix = new float3x3(plane.rotation);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0005AC7C File Offset: 0x00058E7C
		public ToWorldMatrix(float3x3 matrix)
		{
			this.matrix = matrix;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0005AC85 File Offset: 0x00058E85
		public float3 ToWorld(float2 p, float elevation = 0f)
		{
			return math.mul(this.matrix, new float3(p.x, elevation, p.y));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0005ACA4 File Offset: 0x00058EA4
		public Bounds ToWorld(Bounds bounds)
		{
			return new Bounds
			{
				center = math.mul(this.matrix, bounds.center),
				extents = math.mul(new float3x3(math.abs(this.matrix.c0), math.abs(this.matrix.c1), math.abs(this.matrix.c2)), bounds.extents)
			};
		}

		// Token: 0x04000AFE RID: 2814
		public readonly float3x3 matrix;
	}
}
