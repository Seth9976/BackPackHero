using System;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200028B RID: 651
	public interface IMovementPlaneWrapper
	{
		// Token: 0x06000F5D RID: 3933
		float2 ToPlane(float3 p);

		// Token: 0x06000F5E RID: 3934
		float2 ToPlane(float3 p, out float elevation);

		// Token: 0x06000F5F RID: 3935
		float3 ToWorld(float2 p, float elevation = 0f);

		// Token: 0x06000F60 RID: 3936
		Bounds ToWorld(Bounds bounds);

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000F61 RID: 3937
		float4x4 matrix { get; }

		// Token: 0x06000F62 RID: 3938
		void Set(NativeMovementPlane plane);
	}
}
