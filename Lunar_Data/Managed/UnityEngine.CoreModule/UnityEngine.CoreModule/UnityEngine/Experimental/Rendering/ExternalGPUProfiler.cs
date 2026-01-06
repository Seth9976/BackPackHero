using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000471 RID: 1137
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class ExternalGPUProfiler
	{
		// Token: 0x06002826 RID: 10278
		[FreeFunction("ExternalGPUProfilerBindings::BeginGPUCapture")]
		[MethodImpl(4096)]
		public static extern void BeginGPUCapture();

		// Token: 0x06002827 RID: 10279
		[FreeFunction("ExternalGPUProfilerBindings::EndGPUCapture")]
		[MethodImpl(4096)]
		public static extern void EndGPUCapture();

		// Token: 0x06002828 RID: 10280
		[FreeFunction("ExternalGPUProfilerBindings::IsAttached")]
		[MethodImpl(4096)]
		public static extern bool IsAttached();
	}
}
