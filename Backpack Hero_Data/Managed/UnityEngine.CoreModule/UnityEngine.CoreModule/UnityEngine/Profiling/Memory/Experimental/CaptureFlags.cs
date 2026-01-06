using System;

namespace UnityEngine.Profiling.Memory.Experimental
{
	// Token: 0x0200027C RID: 636
	[Flags]
	public enum CaptureFlags : uint
	{
		// Token: 0x04000912 RID: 2322
		ManagedObjects = 1U,
		// Token: 0x04000913 RID: 2323
		NativeObjects = 2U,
		// Token: 0x04000914 RID: 2324
		NativeAllocations = 4U,
		// Token: 0x04000915 RID: 2325
		NativeAllocationSites = 8U,
		// Token: 0x04000916 RID: 2326
		NativeStackTraces = 16U
	}
}
