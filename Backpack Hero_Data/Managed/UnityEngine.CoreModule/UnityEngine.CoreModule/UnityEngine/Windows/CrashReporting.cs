using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x02000286 RID: 646
	public static class CrashReporting
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C1A RID: 7194
		public static extern string crashReportFolder
		{
			[ThreadSafe]
			[NativeHeader("PlatformDependent/WinPlayer/Bindings/CrashReportingBindings.h")]
			[MethodImpl(4096)]
			get;
		}
	}
}
