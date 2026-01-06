using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	[StaticAccessor("GetUncheckedRealGfxDevice().GetFrameTimingManager()", StaticAccessorType.Dot)]
	public static class FrameTimingManager
	{
		// Token: 0x0600098D RID: 2445
		[MethodImpl(4096)]
		public static extern void CaptureFrameTimings();

		// Token: 0x0600098E RID: 2446
		[MethodImpl(4096)]
		public static extern uint GetLatestTimings(uint numFrames, FrameTiming[] timings);

		// Token: 0x0600098F RID: 2447
		[MethodImpl(4096)]
		public static extern float GetVSyncsPerSecond();

		// Token: 0x06000990 RID: 2448
		[MethodImpl(4096)]
		public static extern ulong GetGpuTimerFrequency();

		// Token: 0x06000991 RID: 2449
		[MethodImpl(4096)]
		public static extern ulong GetCpuTimerFrequency();
	}
}
