using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x020002D5 RID: 725
	[NativeHeader("Runtime/Scripting/GarbageCollector.h")]
	public static class GarbageCollector
	{
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001DE6 RID: 7654 RVA: 0x000309CC File Offset: 0x0002EBCC
		// (remove) Token: 0x06001DE7 RID: 7655 RVA: 0x00030A00 File Offset: 0x0002EC00
		[field: DebuggerBrowsable(0)]
		public static event Action<GarbageCollector.Mode> GCModeChanged;

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00030A34 File Offset: 0x0002EC34
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public static GarbageCollector.Mode GCMode
		{
			get
			{
				return GarbageCollector.GetMode();
			}
			set
			{
				bool flag = value == GarbageCollector.GetMode();
				if (!flag)
				{
					GarbageCollector.SetMode(value);
					bool flag2 = GarbageCollector.GCModeChanged != null;
					if (flag2)
					{
						GarbageCollector.GCModeChanged.Invoke(value);
					}
				}
			}
		}

		// Token: 0x06001DEA RID: 7658
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetMode(GarbageCollector.Mode mode);

		// Token: 0x06001DEB RID: 7659
		[MethodImpl(4096)]
		private static extern GarbageCollector.Mode GetMode();

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001DEC RID: 7660
		public static extern bool isIncremental
		{
			[NativeMethod("GetIncrementalEnabled")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001DED RID: 7661
		// (set) Token: 0x06001DEE RID: 7662
		public static extern ulong incrementalTimeSliceNanoseconds
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001DEF RID: 7663
		[NativeMethod("CollectIncrementalWrapper")]
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern bool CollectIncremental(ulong nanoseconds = 0UL);

		// Token: 0x020002D6 RID: 726
		public enum Mode
		{
			// Token: 0x040009CD RID: 2509
			Disabled,
			// Token: 0x040009CE RID: 2510
			Enabled,
			// Token: 0x040009CF RID: 2511
			Manual
		}
	}
}
