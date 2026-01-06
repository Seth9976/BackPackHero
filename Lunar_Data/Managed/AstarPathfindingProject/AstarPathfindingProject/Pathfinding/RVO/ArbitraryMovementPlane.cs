using System;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200028E RID: 654
	public struct ArbitraryMovementPlane : IMovementPlaneWrapper
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x00061422 File Offset: 0x0005F622
		public float2 ToPlane(float3 p)
		{
			return this.plane.ToPlane(p);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00061430 File Offset: 0x0005F630
		public float2 ToPlane(float3 p, out float elevation)
		{
			return this.plane.ToPlane(p, out elevation);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0006143F File Offset: 0x0005F63F
		public float3 ToWorld(float2 p, float elevation = 0f)
		{
			return this.plane.ToWorld(p, elevation);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0006144E File Offset: 0x0005F64E
		public Bounds ToWorld(Bounds bounds)
		{
			return this.plane.ToWorld(bounds);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0006145C File Offset: 0x0005F65C
		public void Set(NativeMovementPlane plane)
		{
			this.plane = plane;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00061468 File Offset: 0x0005F668
		public float4x4 matrix
		{
			get
			{
				return math.mul(float4x4.TRS(0, this.plane.rotation, 1), new float4x4(new float4(1f, 0f, 0f, 0f), new float4(0f, 0f, 1f, 0f), new float4(0f, 1f, 0f, 0f), new float4(0f, 0f, 0f, 1f)));
			}
		}

		// Token: 0x04000BA1 RID: 2977
		private NativeMovementPlane plane;
	}
}
