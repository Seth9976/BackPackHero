using System;

namespace Pathfinding
{
	// Token: 0x02000061 RID: 97
	public interface INavmesh
	{
		// Token: 0x06000520 RID: 1312
		void GetNodes(Action<GraphNode> del);
	}
}
