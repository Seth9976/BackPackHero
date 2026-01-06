using System;

namespace Pathfinding
{
	// Token: 0x020000DB RID: 219
	public interface INavmesh
	{
		// Token: 0x060006FA RID: 1786
		void GetNodes(Action<GraphNode> del);
	}
}
