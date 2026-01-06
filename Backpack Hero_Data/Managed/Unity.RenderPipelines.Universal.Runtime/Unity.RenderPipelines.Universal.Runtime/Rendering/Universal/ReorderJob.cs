using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C3 RID: 195
	[BurstCompile]
	internal struct ReorderJob<T> : IJobFor where T : struct
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x000203D0 File Offset: 0x0001E5D0
		public void Execute(int index)
		{
			int num = this.indices[index];
			this.output[num] = this.input[index];
		}

		// Token: 0x04000491 RID: 1169
		[ReadOnly]
		public NativeArray<int> indices;

		// Token: 0x04000492 RID: 1170
		[ReadOnly]
		public NativeArray<T> input;

		// Token: 0x04000493 RID: 1171
		[NativeDisableParallelForRestriction]
		public NativeArray<T> output;
	}
}
