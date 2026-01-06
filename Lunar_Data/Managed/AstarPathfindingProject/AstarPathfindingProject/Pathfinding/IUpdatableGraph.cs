using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x0200002A RID: 42
	public interface IUpdatableGraph
	{
		// Token: 0x060001EA RID: 490
		IGraphUpdatePromise ScheduleGraphUpdates(List<GraphUpdateObject> graphUpdates);
	}
}
