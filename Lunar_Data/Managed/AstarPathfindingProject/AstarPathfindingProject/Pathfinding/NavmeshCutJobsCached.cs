using System;
using Unity.Burst;

namespace Pathfinding
{
	// Token: 0x02000123 RID: 291
	internal static class NavmeshCutJobsCached
	{
		// Token: 0x04000627 RID: 1575
		public static readonly NavmeshCutJobs.CalculateContourDelegate CalculateContourBurst = BurstCompiler.CompileFunctionPointer<NavmeshCutJobs.CalculateContourDelegate>(new NavmeshCutJobs.CalculateContourDelegate(NavmeshCutJobs.CalculateContour)).Invoke;
	}
}
