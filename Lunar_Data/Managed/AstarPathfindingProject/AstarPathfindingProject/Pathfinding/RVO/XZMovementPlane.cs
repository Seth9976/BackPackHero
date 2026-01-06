using System;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200028D RID: 653
	public struct XZMovementPlane : IMovementPlaneWrapper
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x000613E3 File Offset: 0x0005F5E3
		public float2 ToPlane(float3 p)
		{
			return p.xz;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000613EC File Offset: 0x0005F5EC
		public float2 ToPlane(float3 p, out float elevation)
		{
			elevation = p.y;
			return p.xz;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000613FD File Offset: 0x0005F5FD
		public float3 ToWorld(float2 p, float elevation = 0f)
		{
			return new float3(p.x, elevation, p.y);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00024FAB File Offset: 0x000231AB
		public Bounds ToWorld(Bounds bounds)
		{
			return bounds;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000033F6 File Offset: 0x000015F6
		public void Set(NativeMovementPlane plane)
		{
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00061411 File Offset: 0x0005F611
		public float4x4 matrix
		{
			get
			{
				return float4x4.RotateX(math.radians(90f));
			}
		}
	}
}
