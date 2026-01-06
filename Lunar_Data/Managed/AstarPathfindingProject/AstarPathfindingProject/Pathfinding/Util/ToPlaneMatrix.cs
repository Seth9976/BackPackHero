using System;
using Unity.Mathematics;

namespace Pathfinding.Util
{
	// Token: 0x02000268 RID: 616
	public readonly struct ToPlaneMatrix
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x0005ABF8 File Offset: 0x00058DF8
		public ToPlaneMatrix(NativeMovementPlane plane)
		{
			this.matrix = new float3x3(math.conjugate(plane.rotation));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0005AC10 File Offset: 0x00058E10
		public float2 ToPlane(float3 p)
		{
			return math.mul(this.matrix, p).xz;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0005AC31 File Offset: 0x00058E31
		public float3 ToXZPlane(float3 p)
		{
			return math.mul(this.matrix, p);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0005AC40 File Offset: 0x00058E40
		public float2 ToPlane(float3 p, out float elevation)
		{
			float3 @float = math.mul(this.matrix, p);
			elevation = @float.y;
			return @float.xz;
		}

		// Token: 0x04000AFD RID: 2813
		public readonly float3x3 matrix;
	}
}
