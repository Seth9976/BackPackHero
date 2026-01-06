using System;

namespace Pathfinding
{
	// Token: 0x0200008A RID: 138
	public interface IWorkItemContext : IGraphUpdateContext
	{
		// Token: 0x06000448 RID: 1096
		[Obsolete("Avoid using. This will force a full recalculation of the connected components. In most cases the HierarchicalGraph class takes care of things automatically behind the scenes now. In pretty much all cases you should be able to remove the call to this function.")]
		void QueueFloodFill();

		// Token: 0x06000449 RID: 1097
		void EnsureValidFloodFill();

		// Token: 0x0600044A RID: 1098
		void PreUpdate();

		// Token: 0x0600044B RID: 1099
		void SetGraphDirty(NavGraph graph);
	}
}
