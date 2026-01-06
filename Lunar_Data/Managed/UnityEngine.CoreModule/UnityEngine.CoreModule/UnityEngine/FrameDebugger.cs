using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000256 RID: 598
	[StaticAccessor("FrameDebugger", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Profiler/PerformanceTools/FrameDebugger.h")]
	public static class FrameDebugger
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00029F02 File Offset: 0x00028102
		public static bool enabled
		{
			get
			{
				return FrameDebugger.IsLocalEnabled() || FrameDebugger.IsRemoteEnabled();
			}
		}

		// Token: 0x060019F2 RID: 6642
		[MethodImpl(4096)]
		internal static extern bool IsLocalEnabled();

		// Token: 0x060019F3 RID: 6643
		[MethodImpl(4096)]
		internal static extern bool IsRemoteEnabled();
	}
}
