using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032E RID: 814
	internal class MeshHandle : LinkedPoolItem<MeshHandle>
	{
		// Token: 0x04000C1A RID: 3098
		internal Alloc allocVerts;

		// Token: 0x04000C1B RID: 3099
		internal Alloc allocIndices;

		// Token: 0x04000C1C RID: 3100
		internal uint triangleCount;

		// Token: 0x04000C1D RID: 3101
		internal Page allocPage;

		// Token: 0x04000C1E RID: 3102
		internal uint allocTime;

		// Token: 0x04000C1F RID: 3103
		internal uint updateAllocID;
	}
}
