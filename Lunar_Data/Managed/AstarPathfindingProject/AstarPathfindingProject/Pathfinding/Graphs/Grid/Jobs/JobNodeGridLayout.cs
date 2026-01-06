using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000221 RID: 545
	[BurstCompile(FloatMode = FloatMode.Fast)]
	public struct JobNodeGridLayout : IJob, GridIterationUtilities.ICellAction
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00051D2C File Offset: 0x0004FF2C
		public static Vector3 NodePosition(Matrix4x4 graphToWorld, int x, int z)
		{
			return graphToWorld.MultiplyPoint3x4(new Vector3((float)x + 0.5f, 0f, (float)z + 0.5f));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00051D4F File Offset: 0x0004FF4F
		public void Execute()
		{
			GridIterationUtilities.ForEachCellIn3DArray<JobNodeGridLayout>(this.bounds.size, ref this);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00051D62 File Offset: 0x0004FF62
		public void Execute(uint innerIndex, int x, int y, int z)
		{
			this.nodePositions[(int)innerIndex] = JobNodeGridLayout.NodePosition(this.graphToWorld, x + this.bounds.min.x, z + this.bounds.min.z);
		}

		// Token: 0x040009F9 RID: 2553
		public Matrix4x4 graphToWorld;

		// Token: 0x040009FA RID: 2554
		public IntBounds bounds;

		// Token: 0x040009FB RID: 2555
		[WriteOnly]
		public NativeArray<Vector3> nodePositions;
	}
}
