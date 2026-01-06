using System;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200028C RID: 652
	public struct XYMovementPlane : IMovementPlaneWrapper
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x0006135B File Offset: 0x0005F55B
		public float2 ToPlane(float3 p)
		{
			return p.xy;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00061364 File Offset: 0x0005F564
		public float2 ToPlane(float3 p, out float elevation)
		{
			elevation = p.z;
			return p.xy;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00061375 File Offset: 0x0005F575
		public float3 ToWorld(float2 p, float elevation = 0f)
		{
			return new float3(p.x, p.y, elevation);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0006138C File Offset: 0x0005F58C
		public Bounds ToWorld(Bounds bounds)
		{
			Vector3 center = bounds.center;
			Vector3 size = bounds.size;
			return new Bounds(new Vector3(center.x, center.z, center.y), new Vector3(size.x, size.z, size.y));
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000613DC File Offset: 0x0005F5DC
		public float4x4 matrix
		{
			get
			{
				return float4x4.identity;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000033F6 File Offset: 0x000015F6
		public void Set(NativeMovementPlane plane)
		{
		}
	}
}
