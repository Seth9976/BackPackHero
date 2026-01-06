using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D9 RID: 985
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public enum RenderingThreadingMode
	{
		// Token: 0x04000C0D RID: 3085
		Direct,
		// Token: 0x04000C0E RID: 3086
		SingleThreaded,
		// Token: 0x04000C0F RID: 3087
		MultiThreaded,
		// Token: 0x04000C10 RID: 3088
		LegacyJobified,
		// Token: 0x04000C11 RID: 3089
		NativeGraphicsJobs,
		// Token: 0x04000C12 RID: 3090
		NativeGraphicsJobsWithoutRenderThread
	}
}
