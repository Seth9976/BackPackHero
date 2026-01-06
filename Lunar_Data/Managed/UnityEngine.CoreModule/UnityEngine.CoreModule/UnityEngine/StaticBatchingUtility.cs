using System;
using Unity.Profiling;

namespace UnityEngine
{
	// Token: 0x0200023E RID: 574
	public sealed class StaticBatchingUtility
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x00028000 File Offset: 0x00026200
		public static void Combine(GameObject staticBatchRoot)
		{
			using (StaticBatchingUtility.s_CombineMarker.Auto())
			{
				InternalStaticBatchingUtility.CombineRoot(staticBatchRoot, null);
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00028044 File Offset: 0x00026244
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			using (StaticBatchingUtility.s_CombineMarker.Auto())
			{
				InternalStaticBatchingUtility.CombineGameObjects(gos, staticBatchRoot, false, null);
			}
		}

		// Token: 0x04000846 RID: 2118
		internal static ProfilerMarker s_CombineMarker = new ProfilerMarker("StaticBatching.Combine");

		// Token: 0x04000847 RID: 2119
		internal static ProfilerMarker s_SortMarker = new ProfilerMarker("StaticBatching.SortObjects");

		// Token: 0x04000848 RID: 2120
		internal static ProfilerMarker s_MakeBatchMarker = new ProfilerMarker("StaticBatching.MakeBatch");
	}
}
