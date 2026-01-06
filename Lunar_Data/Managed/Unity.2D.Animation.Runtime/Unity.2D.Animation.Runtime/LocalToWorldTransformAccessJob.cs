using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200003C RID: 60
	[BurstCompile]
	internal struct LocalToWorldTransformAccessJob : IJobParallelForTransform
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000731D File Offset: 0x0000551D
		public void Execute(int index, TransformAccess transform)
		{
			this.outMatrix[index] = transform.localToWorldMatrix;
		}

		// Token: 0x040000D1 RID: 209
		[WriteOnly]
		public NativeArray<float4x4> outMatrix;
	}
}
