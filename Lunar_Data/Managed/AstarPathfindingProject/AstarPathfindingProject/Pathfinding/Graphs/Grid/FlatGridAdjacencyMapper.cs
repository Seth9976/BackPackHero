using System;
using Unity.Collections;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001FA RID: 506
	public struct FlatGridAdjacencyMapper : GridAdjacencyMapper
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x00016F22 File Offset: 0x00015122
		public int LayerCount(IntBounds bounds)
		{
			return 1;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0004DCBF File Offset: 0x0004BEBF
		public int GetNeighbourIndex(int nodeIndexXZ, int nodeIndex, int direction, NativeArray<ulong> nodeConnections, NativeArray<int> neighbourOffsets, int layerStride)
		{
			return nodeIndex + neighbourOffsets[direction];
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0004DCCB File Offset: 0x0004BECB
		public bool HasConnection(int nodeIndex, int direction, NativeArray<ulong> nodeConnections)
		{
			return ((nodeConnections[nodeIndex] >> direction) & 1UL) > 0UL;
		}
	}
}
