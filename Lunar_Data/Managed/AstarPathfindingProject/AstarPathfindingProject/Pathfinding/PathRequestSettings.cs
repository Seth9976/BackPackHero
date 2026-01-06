using System;

namespace Pathfinding
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public struct PathRequestSettings
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00016838 File Offset: 0x00014A38
		public static PathRequestSettings Default
		{
			get
			{
				return new PathRequestSettings
				{
					graphMask = GraphMask.everything,
					tagPenalties = new int[32],
					traversableTags = -1,
					traversalProvider = null
				};
			}
		}

		// Token: 0x040002E8 RID: 744
		public GraphMask graphMask;

		// Token: 0x040002E9 RID: 745
		public int[] tagPenalties;

		// Token: 0x040002EA RID: 746
		public int traversableTags;

		// Token: 0x040002EB RID: 747
		public ITraversalProvider traversalProvider;
	}
}
