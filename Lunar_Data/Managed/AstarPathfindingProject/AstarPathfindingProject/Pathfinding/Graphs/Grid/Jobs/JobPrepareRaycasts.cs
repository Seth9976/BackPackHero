using System;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000224 RID: 548
	[BurstCompile]
	public struct JobPrepareRaycasts : IJob
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00051FD0 File Offset: 0x000501D0
		public void Execute()
		{
			Vector3 normalized = this.direction.normalized;
			this.raycastCommands.AsUnsafeSpan<RaycastCommand>();
			for (int i = 0; i < this.raycastCommands.Length; i++)
			{
				this.raycastCommands[i] = new RaycastCommand(this.origins[i] + this.originOffset, normalized, this.distance, this.mask, 1);
			}
		}

		// Token: 0x04000A0C RID: 2572
		public Vector3 direction;

		// Token: 0x04000A0D RID: 2573
		public Vector3 originOffset;

		// Token: 0x04000A0E RID: 2574
		public float distance;

		// Token: 0x04000A0F RID: 2575
		public LayerMask mask;

		// Token: 0x04000A10 RID: 2576
		public PhysicsScene physicsScene;

		// Token: 0x04000A11 RID: 2577
		[ReadOnly]
		public NativeArray<Vector3> origins;

		// Token: 0x04000A12 RID: 2578
		[WriteOnly]
		public NativeArray<RaycastCommand> raycastCommands;
	}
}
