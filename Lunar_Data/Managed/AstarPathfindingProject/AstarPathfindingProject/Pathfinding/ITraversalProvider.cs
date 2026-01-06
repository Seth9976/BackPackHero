using System;

namespace Pathfinding
{
	// Token: 0x020000A5 RID: 165
	public interface ITraversalProvider
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00016F22 File Offset: 0x00015122
		bool filterDiagonalGridConnections
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000197AC File Offset: 0x000179AC
		bool CanTraverse(Path path, GraphNode node)
		{
			return DefaultITraversalProvider.CanTraverse(path, node);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000197B5 File Offset: 0x000179B5
		bool CanTraverse(Path path, GraphNode from, GraphNode to)
		{
			return this.CanTraverse(path, to);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000197BF File Offset: 0x000179BF
		uint GetTraversalCost(Path path, GraphNode node)
		{
			return DefaultITraversalProvider.GetTraversalCost(path, node);
		}
	}
}
