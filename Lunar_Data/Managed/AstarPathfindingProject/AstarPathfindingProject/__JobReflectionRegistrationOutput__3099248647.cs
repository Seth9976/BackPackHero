using System;
using Pathfinding;
using Unity.Jobs;
using UnityEngine;

// Token: 0x020002BA RID: 698
[DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__3099248647
{
	// Token: 0x06001067 RID: 4199 RVA: 0x00066B08 File Offset: 0x00064D08
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobParallelForBatchExtensions.EarlyJobInit<NavmeshEdges.JobCalculateObstacles>();
		}
		catch (Exception ex)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex, typeof(NavmeshEdges.JobCalculateObstacles));
		}
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00066B48 File Offset: 0x00064D48
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		__JobReflectionRegistrationOutput__3099248647.CreateJobReflectionData();
	}
}
