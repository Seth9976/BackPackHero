using System;
using Pathfinding.Jobs;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x0200021C RID: 540
	internal struct JobCheckCollisions : IJobTimeSliced, IJob
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x000510D9 File Offset: 0x0004F2D9
		public void Execute()
		{
			this.Execute(TimeSlice.Infinite);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000510E8 File Offset: 0x0004F2E8
		public bool Execute(TimeSlice timeSlice)
		{
			for (int i = this.startIndex; i < this.nodePositions.Length; i++)
			{
				this.collisionResult[i] = this.collisionResult[i] && this.collision.Check(this.nodePositions[i]);
				if ((i & 127) == 0 && timeSlice.expired)
				{
					this.startIndex = i + 1;
					return false;
				}
			}
			return true;
		}

		// Token: 0x040009DE RID: 2526
		[ReadOnly]
		public NativeArray<Vector3> nodePositions;

		// Token: 0x040009DF RID: 2527
		public NativeArray<bool> collisionResult;

		// Token: 0x040009E0 RID: 2528
		public GraphCollision collision;

		// Token: 0x040009E1 RID: 2529
		private int startIndex;
	}
}
