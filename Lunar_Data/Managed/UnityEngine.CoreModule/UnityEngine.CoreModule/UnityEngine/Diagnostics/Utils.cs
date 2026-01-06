using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Diagnostics
{
	// Token: 0x0200044C RID: 1100
	[NativeHeader("Runtime/Export/Diagnostics/DiagnosticsUtils.bindings.h")]
	public static class Utils
	{
		// Token: 0x060026DA RID: 9946
		[FreeFunction("DiagnosticsUtils_Bindings::ForceCrash", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void ForceCrash(ForcedCrashCategory crashCategory);

		// Token: 0x060026DB RID: 9947
		[FreeFunction("DiagnosticsUtils_Bindings::NativeAssert")]
		[MethodImpl(4096)]
		public static extern void NativeAssert(string message);

		// Token: 0x060026DC RID: 9948
		[FreeFunction("DiagnosticsUtils_Bindings::NativeError")]
		[MethodImpl(4096)]
		public static extern void NativeError(string message);

		// Token: 0x060026DD RID: 9949
		[FreeFunction("DiagnosticsUtils_Bindings::NativeWarning")]
		[MethodImpl(4096)]
		public static extern void NativeWarning(string message);
	}
}
