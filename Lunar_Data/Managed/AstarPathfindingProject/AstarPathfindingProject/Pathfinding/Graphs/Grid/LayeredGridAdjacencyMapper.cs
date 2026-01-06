using System;
using Unity.Collections;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001FB RID: 507
	public struct LayeredGridAdjacencyMapper : GridAdjacencyMapper
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x0004DCE1 File Offset: 0x0004BEE1
		public int LayerCount(IntBounds bounds)
		{
			return bounds.size.y;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0004DCEF File Offset: 0x0004BEEF
		public int GetNeighbourIndex(int nodeIndexXZ, int nodeIndex, int direction, NativeArray<ulong> nodeConnections, NativeArray<int> neighbourOffsets, int layerStride)
		{
			return nodeIndexXZ + neighbourOffsets[direction] + (int)((nodeConnections[nodeIndex] >> 4 * direction) & 15UL) * layerStride;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0004DD13 File Offset: 0x0004BF13
		public bool HasConnection(int nodeIndex, int direction, NativeArray<ulong> nodeConnections)
		{
			return ((nodeConnections[nodeIndex] >> 4 * direction) & 15UL) != 15UL;
		}
	}
}
