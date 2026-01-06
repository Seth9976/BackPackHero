using System;

namespace Pathfinding
{
	// Token: 0x02000066 RID: 102
	public interface INavmeshHolder : ITransformedGraph, INavmesh
	{
		// Token: 0x06000573 RID: 1395
		Int3 GetVertex(int i);

		// Token: 0x06000574 RID: 1396
		Int3 GetVertexInGraphSpace(int i);

		// Token: 0x06000575 RID: 1397
		int GetVertexArrayIndex(int index);

		// Token: 0x06000576 RID: 1398
		void GetTileCoordinates(int tileIndex, out int x, out int z);
	}
}
