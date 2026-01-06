using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000339 RID: 825
	internal struct HeapStatistics
	{
		// Token: 0x04000C77 RID: 3191
		public uint numAllocs;

		// Token: 0x04000C78 RID: 3192
		public uint totalSize;

		// Token: 0x04000C79 RID: 3193
		public uint allocatedSize;

		// Token: 0x04000C7A RID: 3194
		public uint freeSize;

		// Token: 0x04000C7B RID: 3195
		public uint largestAvailableBlock;

		// Token: 0x04000C7C RID: 3196
		public uint availableBlocksCount;

		// Token: 0x04000C7D RID: 3197
		public uint blockCount;

		// Token: 0x04000C7E RID: 3198
		public uint highWatermark;

		// Token: 0x04000C7F RID: 3199
		public float fragmentation;

		// Token: 0x04000C80 RID: 3200
		public HeapStatistics[] subAllocators;
	}
}
