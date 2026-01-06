using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200003D RID: 61
	[BurstCompile]
	internal struct WorldToLocalTransformAccessJob : IJobParallelForTransform
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00007337 File Offset: 0x00005537
		public void Execute(int index, TransformAccess transform)
		{
			this.outMatrix[index] = transform.worldToLocalMatrix;
		}

		// Token: 0x040000D2 RID: 210
		[WriteOnly]
		public NativeArray<float4x4> outMatrix;
	}
}
