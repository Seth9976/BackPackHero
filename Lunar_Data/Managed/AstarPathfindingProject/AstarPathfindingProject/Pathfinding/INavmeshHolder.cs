using System;

namespace Pathfinding
{
	// Token: 0x020000E5 RID: 229
	public interface INavmeshHolder : ITransformedGraph, INavmesh
	{
		// Token: 0x06000789 RID: 1929
		Int3 GetVertex(int i);

		// Token: 0x0600078A RID: 1930
		Int3 GetVertexInGraphSpace(int i);

		// Token: 0x0600078B RID: 1931
		int GetVertexArrayIndex(int index);

		// Token: 0x0600078C RID: 1932
		void GetTileCoordinates(int tileIndex, out int x, out int z);
	}
}
