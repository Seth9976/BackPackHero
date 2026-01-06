using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000220 RID: 544
	[BurstCompile]
	public struct JobMergeRaycastCollisionHits : IJob
	{
		// Token: 0x06000CEC RID: 3308 RVA: 0x00051CBC File Offset: 0x0004FEBC
		public void Execute()
		{
			for (int i = 0; i < this.hit1.Length; i++)
			{
				this.result[i] = this.hit1[i].normal == Vector3.zero && this.hit2[i].normal == Vector3.zero;
			}
		}

		// Token: 0x040009F6 RID: 2550
		[ReadOnly]
		public NativeArray<RaycastHit> hit1;

		// Token: 0x040009F7 RID: 2551
		[ReadOnly]
		public NativeArray<RaycastHit> hit2;

		// Token: 0x040009F8 RID: 2552
		[WriteOnly]
		public NativeArray<bool> result;
	}
}
