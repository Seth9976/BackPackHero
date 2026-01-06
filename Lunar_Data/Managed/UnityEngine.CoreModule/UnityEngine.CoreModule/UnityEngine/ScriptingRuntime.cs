using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000213 RID: 531
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Export/Scripting/ScriptingRuntime.h")]
	internal class ScriptingRuntime
	{
		// Token: 0x06001752 RID: 5970
		[MethodImpl(4096)]
		public static extern string[] GetAllUserAssemblies();
	}
}
