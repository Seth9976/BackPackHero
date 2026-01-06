using System;
using UnityEngine.Profiling;

namespace Pathfinding.Jobs
{
	// Token: 0x02000189 RID: 393
	internal static class JobDependencyAnalyzerAssociated
	{
		// Token: 0x04000751 RID: 1873
		internal static CustomSampler getDependenciesSampler = CustomSampler.Create("GetDependencies", false);

		// Token: 0x04000752 RID: 1874
		internal static CustomSampler iteratingSlotsSampler = CustomSampler.Create("IteratingSlots", false);

		// Token: 0x04000753 RID: 1875
		internal static CustomSampler initSampler = CustomSampler.Create("Init", false);

		// Token: 0x04000754 RID: 1876
		internal static CustomSampler combineSampler = CustomSampler.Create("Combining", false);

		// Token: 0x04000755 RID: 1877
		internal static int[] tempJobDependencyHashes = new int[16];

		// Token: 0x04000756 RID: 1878
		internal static int jobCounter = 1;
	}
}
