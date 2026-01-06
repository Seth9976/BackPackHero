using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DC RID: 988
	public enum SynchronisationStageFlags
	{
		// Token: 0x04000C20 RID: 3104
		VertexProcessing = 1,
		// Token: 0x04000C21 RID: 3105
		PixelProcessing,
		// Token: 0x04000C22 RID: 3106
		ComputeProcessing = 4,
		// Token: 0x04000C23 RID: 3107
		AllGPUOperations = 7
	}
}
