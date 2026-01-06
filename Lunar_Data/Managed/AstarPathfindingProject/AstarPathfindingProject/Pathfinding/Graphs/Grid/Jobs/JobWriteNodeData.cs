using System;
using System.Runtime.InteropServices;
using Pathfinding.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Grid.Jobs
{
	// Token: 0x02000227 RID: 551
	public struct JobWriteNodeData : IJobParallelForBatched
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00018013 File Offset: 0x00016213
		public bool allowBoundsChecks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000521E0 File Offset: 0x000503E0
		public void Execute(int startIndex, int count)
		{
			GridNodeBase[] array = (GridNodeBase[])this.nodesHandle.Target;
			IntBounds intBounds = this.writeMask.Offset(-this.dataBounds.min);
			int3 size = this.writeMask.size;
			int num = startIndex / (size.x * size.y);
			int num2 = (startIndex + count) / (size.x * size.y);
			intBounds.min.z = this.writeMask.min.z + num - this.dataBounds.min.z;
			intBounds.max.z = this.writeMask.min.z + num2 - this.dataBounds.min.z;
			int3 size2 = this.dataBounds.size;
			for (int i = intBounds.min.y; i < intBounds.max.y; i++)
			{
				for (int j = intBounds.min.z; j < intBounds.max.z; j++)
				{
					int num3 = (i * size2.z + j) * size2.x;
					int num4 = (j + this.dataBounds.min.z) * this.nodeArrayBounds.x + this.dataBounds.min.x;
					int num5 = (i + this.dataBounds.min.y) * this.nodeArrayBounds.z * this.nodeArrayBounds.x + num4;
					for (int k = intBounds.min.x; k < intBounds.max.x; k++)
					{
						int num6 = num3 + k;
						int num7 = num5 + k;
						GridNodeBase gridNodeBase = array[num7];
						if (gridNodeBase != null)
						{
							gridNodeBase.GraphIndex = this.graphIndex;
							gridNodeBase.NodeInGridIndex = num4 + k;
							gridNodeBase.position = (Int3)this.nodePositions[num6];
							gridNodeBase.Penalty = this.nodePenalties[num6];
							gridNodeBase.Tag = (uint)this.nodeTags[num6];
							GridNode gridNode = gridNodeBase as GridNode;
							if (gridNode != null)
							{
								gridNode.SetAllConnectionInternal((int)this.nodeConnections[num6]);
							}
							else
							{
								LevelGridNode levelGridNode = gridNodeBase as LevelGridNode;
								if (levelGridNode != null)
								{
									levelGridNode.LayerCoordinateInGrid = i + this.dataBounds.min.y;
									levelGridNode.SetAllConnectionInternal(this.nodeConnections[num6]);
								}
							}
							gridNodeBase.Walkable = this.nodeWalkableWithErosion[num6];
							gridNodeBase.WalkableErosion = this.nodeWalkable[num6];
						}
					}
				}
			}
		}

		// Token: 0x04000A23 RID: 2595
		public GCHandle nodesHandle;

		// Token: 0x04000A24 RID: 2596
		public uint graphIndex;

		// Token: 0x04000A25 RID: 2597
		public int3 nodeArrayBounds;

		// Token: 0x04000A26 RID: 2598
		public IntBounds dataBounds;

		// Token: 0x04000A27 RID: 2599
		public IntBounds writeMask;

		// Token: 0x04000A28 RID: 2600
		[ReadOnly]
		public NativeArray<Vector3> nodePositions;

		// Token: 0x04000A29 RID: 2601
		[ReadOnly]
		public NativeArray<uint> nodePenalties;

		// Token: 0x04000A2A RID: 2602
		[ReadOnly]
		public NativeArray<int> nodeTags;

		// Token: 0x04000A2B RID: 2603
		[ReadOnly]
		public NativeArray<ulong> nodeConnections;

		// Token: 0x04000A2C RID: 2604
		[ReadOnly]
		public NativeArray<bool> nodeWalkableWithErosion;

		// Token: 0x04000A2D RID: 2605
		[ReadOnly]
		public NativeArray<bool> nodeWalkable;
	}
}
