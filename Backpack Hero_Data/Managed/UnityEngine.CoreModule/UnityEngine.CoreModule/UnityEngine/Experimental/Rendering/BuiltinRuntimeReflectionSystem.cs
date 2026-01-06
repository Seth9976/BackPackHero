using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200046C RID: 1132
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	internal class BuiltinRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x06002810 RID: 10256 RVA: 0x00042A8C File Offset: 0x00040C8C
		public bool TickRealtimeProbes()
		{
			return BuiltinRuntimeReflectionSystem.BuiltinUpdate();
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x00042AA3 File Offset: 0x00040CA3
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x00004557 File Offset: 0x00002757
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x06002813 RID: 10259
		[StaticAccessor("GetReflectionProbes()", Type = StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern bool BuiltinUpdate();

		// Token: 0x06002814 RID: 10260 RVA: 0x00042AB0 File Offset: 0x00040CB0
		[RequiredByNativeCode]
		private static BuiltinRuntimeReflectionSystem Internal_BuiltinRuntimeReflectionSystem_New()
		{
			return new BuiltinRuntimeReflectionSystem();
		}
	}
}
