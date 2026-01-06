using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000030 RID: 48
	public interface IRaycastableGraph
	{
		// Token: 0x060001F4 RID: 500
		bool Linecast(Vector3 start, Vector3 end);

		// Token: 0x060001F5 RID: 501
		[Obsolete]
		bool Linecast(Vector3 start, Vector3 end, GraphNode startNodeHint);

		// Token: 0x060001F6 RID: 502
		[Obsolete]
		bool Linecast(Vector3 start, Vector3 end, GraphNode startNodeHint, out GraphHitInfo hit);

		// Token: 0x060001F7 RID: 503
		bool Linecast(Vector3 start, Vector3 end, GraphNode startNodeHint, out GraphHitInfo hit, List<GraphNode> trace, Func<GraphNode, bool> filter = null);

		// Token: 0x060001F8 RID: 504
		bool Linecast(Vector3 start, Vector3 end, out GraphHitInfo hit, List<GraphNode> trace = null, Func<GraphNode, bool> filter = null);
	}
}
