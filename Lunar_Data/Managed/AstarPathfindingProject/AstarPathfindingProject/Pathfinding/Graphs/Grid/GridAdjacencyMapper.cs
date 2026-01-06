using System;
using Unity.Collections;

namespace Pathfinding.Graphs.Grid
{
	// Token: 0x020001F9 RID: 505
	public interface GridAdjacencyMapper
	{
		// Token: 0x06000C74 RID: 3188
		int LayerCount(IntBounds bounds);

		// Token: 0x06000C75 RID: 3189
		int GetNeighbourIndex(int nodeIndexXZ, int nodeIndex, int direction, NativeArray<ulong> nodeConnections, NativeArray<int> neighbourOffsets, int layerStride);

		// Token: 0x06000C76 RID: 3190
		bool HasConnection(int nodeIndex, int direction, NativeArray<ulong> nodeConnections);
	}
}
