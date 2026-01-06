using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200047B RID: 1147
	[UsedByNativeCode]
	[Flags]
	[NativeHeader("Runtime/Export/Graphics/RayTracingAccelerationStructure.bindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	public enum RayTracingSubMeshFlags
	{
		// Token: 0x04000F80 RID: 3968
		Disabled = 0,
		// Token: 0x04000F81 RID: 3969
		Enabled = 1,
		// Token: 0x04000F82 RID: 3970
		ClosestHitOnly = 2,
		// Token: 0x04000F83 RID: 3971
		UniqueAnyHitCalls = 4
	}
}
