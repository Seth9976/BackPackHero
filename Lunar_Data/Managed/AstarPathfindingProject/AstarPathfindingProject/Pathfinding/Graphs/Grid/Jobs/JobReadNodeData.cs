using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Pathfinding.Jobs;
using Unity.Collections;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000225 RID: 549
	public struct JobReadNodeData : IJobParallelForBatched
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00018013 File Offset: 0x00016213
		public bool allowBoundsChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00052048 File Offset: 0x00050248
		public void Execute(int startIndex, int count)
		{
			JobReadNodeData.Reader reader = new JobReadNodeData.Reader
			{
				nodes = (GridNodeBase[])this.nodesHandle.Target,
				nodePositions = this.nodePositions,
				nodePenalties = this.nodePenalties,
				nodeTags = this.nodeTags,
				nodeConnections = this.nodeConnections,
				nodeWalkableWithErosion = this.nodeWalkableWithErosion,
				nodeWalkable = this.nodeWalkable
			};
			GridIterationUtilities.ForEachCellIn3DSlice<JobReadNodeData.Reader>(this.slice, ref reader);
		}

		// Token: 0x04000A13 RID: 2579
		public GCHandle nodesHandle;

		// Token: 0x04000A14 RID: 2580
		public uint graphIndex;

		// Token: 0x04000A15 RID: 2581
		public Slice3D slice;

		// Token: 0x04000A16 RID: 2582
		[WriteOnly]
		public NativeArray<Vector3> nodePositions;

		// Token: 0x04000A17 RID: 2583
		[WriteOnly]
		public NativeArray<uint> nodePenalties;

		// Token: 0x04000A18 RID: 2584
		[WriteOnly]
		public NativeArray<int> nodeTags;

		// Token: 0x04000A19 RID: 2585
		[WriteOnly]
		public NativeArray<ulong> nodeConnections;

		// Token: 0x04000A1A RID: 2586
		[WriteOnly]
		public NativeArray<bool> nodeWalkableWithErosion;

		// Token: 0x04000A1B RID: 2587
		[WriteOnly]
		public NativeArray<bool> nodeWalkable;

		// Token: 0x02000226 RID: 550
		private struct Reader : GridIterationUtilities.ISliceAction
		{
			// Token: 0x06000CF5 RID: 3317 RVA: 0x000520D4 File Offset: 0x000502D4
			[MethodImpl(256)]
			public void Execute(uint outerIdx, uint innerIdx)
			{
				if ((ulong)outerIdx < (ulong)((long)this.nodes.Length))
				{
					GridNodeBase gridNodeBase = this.nodes[(int)outerIdx];
					if (gridNodeBase != null)
					{
						this.nodePositions[(int)innerIdx] = (Vector3)gridNodeBase.position;
						this.nodePenalties[(int)innerIdx] = gridNodeBase.Penalty;
						this.nodeTags[(int)innerIdx] = (int)gridNodeBase.Tag;
						GridNode gridNode = gridNodeBase as GridNode;
						this.nodeConnections[(int)innerIdx] = (ulong)((gridNode != null) ? ((long)gridNode.GetAllConnectionInternal()) : ((long)((ulong)(gridNodeBase as LevelGridNode).gridConnections)));
						this.nodeWalkableWithErosion[(int)innerIdx] = gridNodeBase.Walkable;
						this.nodeWalkable[(int)innerIdx] = gridNodeBase.WalkableErosion;
						return;
					}
				}
				this.nodePositions[(int)innerIdx] = Vector3.zero;
				this.nodePenalties[(int)innerIdx] = 0U;
				this.nodeTags[(int)innerIdx] = 0;
				this.nodeConnections[(int)innerIdx] = 0UL;
				this.nodeWalkableWithErosion[(int)innerIdx] = false;
				this.nodeWalkable[(int)innerIdx] = false;
			}

			// Token: 0x04000A1C RID: 2588
			public GridNodeBase[] nodes;

			// Token: 0x04000A1D RID: 2589
			public NativeArray<Vector3> nodePositions;

			// Token: 0x04000A1E RID: 2590
			public NativeArray<uint> nodePenalties;

			// Token: 0x04000A1F RID: 2591
			public NativeArray<int> nodeTags;

			// Token: 0x04000A20 RID: 2592
			public NativeArray<ulong> nodeConnections;

			// Token: 0x04000A21 RID: 2593
			public NativeArray<bool> nodeWalkableWithErosion;

			// Token: 0x04000A22 RID: 2594
			public NativeArray<bool> nodeWalkable;
		}
	}
}
