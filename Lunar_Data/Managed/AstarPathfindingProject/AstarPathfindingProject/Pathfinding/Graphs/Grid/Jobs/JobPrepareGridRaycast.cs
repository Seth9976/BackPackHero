using System;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000223 RID: 547
	[BurstCompile]
	public struct JobPrepareGridRaycast : IJob
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x00051EF0 File Offset: 0x000500F0
		public unsafe void Execute()
		{
			float magnitude = this.raycastDirection.magnitude;
			int3 size = this.bounds.size;
			Vector3 normalized = this.raycastDirection.normalized;
			UnsafeSpan<RaycastCommand> unsafeSpan = this.raycastCommands.AsUnsafeSpan<RaycastCommand>();
			for (int i = 0; i < size.z; i++)
			{
				int num = i * size.x;
				for (int j = 0; j < size.x; j++)
				{
					int num2 = num + j;
					Vector3 vector = JobNodeGridLayout.NodePosition(this.graphToWorld, j + this.bounds.min.x, i + this.bounds.min.z);
					*unsafeSpan[num2] = new RaycastCommand(vector + this.raycastOffset, normalized, magnitude, this.raycastMask, 1);
				}
			}
		}

		// Token: 0x04000A05 RID: 2565
		public Matrix4x4 graphToWorld;

		// Token: 0x04000A06 RID: 2566
		public IntBounds bounds;

		// Token: 0x04000A07 RID: 2567
		public Vector3 raycastOffset;

		// Token: 0x04000A08 RID: 2568
		public Vector3 raycastDirection;

		// Token: 0x04000A09 RID: 2569
		public LayerMask raycastMask;

		// Token: 0x04000A0A RID: 2570
		public PhysicsScene physicsScene;

		// Token: 0x04000A0B RID: 2571
		[WriteOnly]
		public NativeArray<RaycastCommand> raycastCommands;
	}
}
