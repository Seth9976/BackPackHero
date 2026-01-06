using System;
using Unity.Burst;

namespace Pathfinding.Drawing
{
	// Token: 0x02000026 RID: 38
	public static class SharedDrawingData
	{
		// Token: 0x04000077 RID: 119
		public static readonly SharedStatic<float> BurstTime = SharedStatic<float>.GetOrCreateUnsafe(4U, 4667476456522965744L, -7737948255972676495L);

		// Token: 0x02000027 RID: 39
		private class BurstTimeKey
		{
		}
	}
}
