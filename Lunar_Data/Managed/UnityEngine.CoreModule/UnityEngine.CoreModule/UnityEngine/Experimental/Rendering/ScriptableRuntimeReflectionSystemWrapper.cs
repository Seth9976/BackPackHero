using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000470 RID: 1136
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x00042C13 File Offset: 0x00040E13
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x00042C1B File Offset: 0x00040E1B
		internal IScriptableRuntimeReflectionSystem implementation { get; set; }

		// Token: 0x06002824 RID: 10276 RVA: 0x00042C24 File Offset: 0x00040E24
		[RequiredByNativeCode]
		private void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(out bool result)
		{
			result = this.implementation != null && this.implementation.TickRealtimeProbes();
		}
	}
}
